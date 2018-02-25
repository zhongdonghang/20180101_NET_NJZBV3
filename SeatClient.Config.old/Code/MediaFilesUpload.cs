using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;

namespace SeatManage.SeatClient.Config.Code
{
    public class MediaFilesUpload
    {
        public MediaFilesUpload(string FilesPath, string type)
        {
            _FilesPath = FilesPath;
            _type = type;
        }
        public delegate void EventHanleProgress(string message);
        public event EventHanleProgress Progress;
        private string _FilesPath = "";
        private string _type = "";
        public void Upload()
        {

            try
            {
                switch (_type)
                {
                    case "PlayList": PlayListUpload(); break;
                    case "SlipCustomer": SlipCustomerUpload(); break;
                    case "PrintTemplate": PrintTemplate(); break;
                    default: throw new Exception("媒体类型信息错误！");
                }
            }
            catch (Exception ex)
            {
                if (Progress != null)
                {
                    Progress("上传失败！" + ex.Message);
                }
            }
        }
        private void PlayListUpload()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_FilesPath + "playList.xml");
                SeatManage.ClassModel.AMS_PlayList playerlist = SeatManage.ClassModel.AMS_PlayList.Parse(doc.OuterXml);
                playerlist.ReleaseDate = SeatManage.Bll.ServiceDateTime.Now;
                foreach (SeatManage.ClassModel.AMS_VideoItem itme in playerlist.VideoFiles)
                {
                    if (Progress != null)
                    {
                        Progress("验证媒体文件" + itme.RelativeUrl);
                    }
                    if (!File.Exists(_FilesPath + itme.RelativeUrl))
                    {
                        throw new Exception("验证媒体文件失败！" + itme.RelativeUrl + "不存在！");
                    }
                }
                SeatManage.Bll.FileOperate upload = new Bll.FileOperate();
                foreach (SeatManage.ClassModel.AMS_VideoItem itme in playerlist.VideoFiles)
                {
                    if (Progress != null)
                    {
                        Progress("正在上传媒体文件" + itme.RelativeUrl);
                    }
                    if (!upload.UpdateFile(_FilesPath + itme.RelativeUrl, itme.RelativeUrl, SeatManage.EnumType.SeatManageSubsystem.MediaFiles))
                    {
                        throw new Exception("上传媒体文件" + itme.RelativeUrl + "失败！请检查WCF服务配置是否正确！");
                    }
                }
                if (SeatManage.Bll.AMS_PlayList.GetPlayListByNum(playerlist.PlayListNo) == null)
                {
                    if (SeatManage.Bll.AMS_PlayList.AddPlaylist(playerlist) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        throw new Exception("播放添加失败！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("播放列表发布成功：添加播放列表" + playerlist.PlayListNo + "，上传媒体文件" + playerlist.VideoFiles.Count + "个");
                        }
                    }
                }
                else
                {
                    if (SeatManage.Bll.AMS_PlayList.UpdatePlaylist(playerlist) == SeatManage.EnumType.HandleResult.Failed)
                    {
                        throw new Exception("播放更新失败！");
                    }
                    else
                    {
                        if (Progress != null)
                        {
                            Progress("播放列表发布成功：更新播放列表" + playerlist.PlayListNo + "，上传媒体文件" + playerlist.VideoFiles.Count + "个");
                        }
                    }
                }

            }
            catch
            {
                throw;
            }
        }
        private void SlipCustomerUpload()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_FilesPath + "SlipCustomerList.xml");
                List<SeatManage.ClassModel.AMS_SlipCustomer> list = FromXML(doc);
                SeatManage.Bll.FileOperate upload = new Bll.FileOperate();
                foreach (SeatManage.ClassModel.AMS_SlipCustomer item in list)
                {
                    if (item.IsPrint)
                    {
                        XmlDocument Templatedoc = new XmlDocument();
                        Templatedoc.LoadXml(item.SlipTemplate);
                        XmlElement Templateroot = Templatedoc.DocumentElement;
                        XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                        for (int j = 0; j < Templatexnlist.Count; j++)
                        {
                            if (Templatexnlist[j].Name == "Pic")
                            {
                                if (Progress != null)
                                {
                                    Progress("正在验证媒体文件" + Templatexnlist[j].InnerText);
                                }
                                if (!File.Exists(_FilesPath + Templatexnlist[j].InnerText))
                                {
                                    throw new Exception("优惠券图片" + Templatexnlist[j].InnerText + "不存在！");
                                }
                            }
                        }
                    }
                    if (Progress != null)
                    {
                        Progress("正在验证媒体文件" + item.ImageName);
                    }
                    if (!File.Exists(_FilesPath + item.ImageName))
                    {
                        throw new Exception("优惠券图片" + item.ImageName + "不存在！");
                    }
                    if (Progress != null)
                    {
                        Progress("正在验证媒体文件" + item.CustomerLogo);
                    }
                    if (!File.Exists(_FilesPath + item.CustomerLogo))
                    {
                        throw new Exception("优惠券图片" + item.CustomerLogo + "不存在！");
                    }

                }
                foreach (SeatManage.ClassModel.AMS_SlipCustomer item in list)
                {
                    if (item.IsPrint)
                    {
                        XmlDocument Templatedoc = new XmlDocument();
                        Templatedoc.LoadXml(item.SlipTemplate);
                        XmlElement Templateroot = Templatedoc.DocumentElement;
                        XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                        for (int j = 0; j < Templatexnlist.Count; j++)
                        {
                            if (Templatexnlist[j].Name == "Pic")
                            {
                                if (Progress != null)
                                {
                                    Progress("正在上传媒体文件" + Templatexnlist[j].InnerText);
                                }
                                if (!upload.UpdateFile((_FilesPath + Templatexnlist[j].InnerText), Templatexnlist[j].InnerText, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                                {
                                    throw new Exception("优惠券图片" + Templatexnlist[j].InnerText + "上传失败！");
                                }
                            }
                        }
                    }
                    if (Progress != null)
                    {
                        Progress("正在上传媒体文件" + item.CustomerLogo);
                    }
                    if (!upload.UpdateFile((_FilesPath + item.CustomerLogo), item.CustomerLogo, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                    {
                        throw new Exception("优惠券图片" + item.CustomerLogo + "上传失败！");
                    }
                    if (Progress != null)
                    {
                        Progress("正在上传媒体文件" + item.ImageName);
                    }
                    if (!upload.UpdateFile((_FilesPath + item.ImageName), item.ImageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                    {
                        throw new Exception("优惠券图片" + item.ImageName + "上传失败！");
                    }
                    List<SeatManage.ClassModel.AMS_SlipCustomer> oldslips = SeatManage.Bll.AMS_SlipCustomer.GetSlipCustomerList(item.CampusNum);
                    bool isupdate = false;
                    foreach (SeatManage.ClassModel.AMS_SlipCustomer sl in oldslips)
                    {
                        if (sl.No == item.No)
                        {
                            isupdate = true;
                            item.Id = sl.Id;
                            break;
                        }
                    }

                    if (!isupdate)
                    {
                        if (SeatManage.Bll.AMS_SlipCustomer.AddSlipCustomer(item) == SeatManage.EnumType.HandleResult.Failed)
                        {
                            throw new Exception("优惠券" + item.No + "添加失败！");
                        }
                        else
                        {
                            if (Progress != null)
                            {
                                Progress("优惠券" + item.No + "添加成功！");
                            }
                        }
                    }
                    else
                    {
                        if (SeatManage.Bll.AMS_SlipCustomer.UpdateSlipCustomer(item) == SeatManage.EnumType.HandleResult.Failed)
                        {
                            throw new Exception("优惠券" + item.No + "更新失败");
                        }
                        else
                        {
                            if (Progress != null)
                            {
                                Progress("优惠券" + item.No + "更新成功");
                            }
                        }
                    }
                }
                if (Progress != null)
                {
                    Progress("发布成功，共计" + list.Count + "个优惠券");
                }
            }
            catch
            {
                throw;
            }

        }
        private void PrintTemplate()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_FilesPath + "TemplateInfo.xml");
                XmlNode nodes = doc.SelectSingleNode("//Root/PrintTemplate");
                SeatManage.ClassModel.AMS_PrintTemplateModel priTem = new ClassModel.AMS_PrintTemplateModel();
                priTem.Describe = nodes.Attributes["Describe"].Value;
                priTem.EffectDate = DateTime.Parse(nodes.Attributes["StartTime"].Value);
                priTem.EndDate = DateTime.Parse(nodes.Attributes["EndTime"].Value);
                priTem.Template = nodes.Attributes["Template"].Value;
                List<string> files = GetImagesName(priTem.Template);
                foreach (string file in files)
                {
                    if (Progress != null)
                    {
                        Progress("验证打印模板图片" + file);
                    }
                    if (!File.Exists(_FilesPath + file))
                    {
                        throw new Exception("打印模板图片" + file + "不存在！");
                    }
                }
                SeatManage.Bll.FileOperate upload = new Bll.FileOperate();
                foreach (string file in files)
                {
                    if (Progress != null)
                    {
                        Progress("正在上传打印模板图片" + file);
                    }
                    if (!upload.UpdateFile(_FilesPath + file, file, SeatManage.EnumType.SeatManageSubsystem.SeatSlip))
                    {
                        throw new Exception("上传打印模板图片" + file + "失败！");
                    }
                }
                if (SeatManage.Bll.T_SM_PrintTemplate.AddPrintTemplate(priTem) == SeatManage.EnumType.HandleResult.Failed)
                {
                    throw new Exception("添加打印模板失败！");
                }
                else
                {
                    if (Progress != null)
                    {
                        Progress("添加打印模板" + priTem.Describe + "成功！上传" + files.Count + "个文件！");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        private List<SeatManage.ClassModel.AMS_SlipCustomer> FromXML(XmlDocument xmllist)
        {
            //查找根节点
            XmlNode node = xmllist.SelectSingleNode("//Root");
            List<SeatManage.ClassModel.AMS_SlipCustomer> lists = new List<SeatManage.ClassModel.AMS_SlipCustomer>();
            XmlNodeList nodes = xmllist.SelectNodes("//Root/ADItems/AD");
            //遍历找到的视频项
            foreach (XmlNode n in nodes)
            {
                SeatManage.ClassModel.AMS_SlipCustomer cust = new SeatManage.ClassModel.AMS_SlipCustomer();
                cust.No = n.Attributes["no"].Value;
                cust.EffectDate = DateTime.Parse(n.Attributes["EffectDate"].Value);
                cust.EndDate = DateTime.Parse(n.Attributes["EndDate"].Value);
                cust.ImageName = n.Attributes["ImageUrl"].Value;
                cust.SlipTemplate = n.Attributes["SlipTemplate"].Value;
                cust.CampusNum = n.Attributes["CampusNum"].Value;
                cust.CustomerLogo = n.Attributes["CustomerImage"].Value;
                if (n.Attributes["IsPrint"].Value == "0")
                {
                    cust.IsPrint = false;
                }
                else
                {
                    cust.IsPrint = true;
                }
                lists.Add(cust);
            }
            return lists;

        }
        /// <summary>
        /// 获取打印模板里的图片名称
        /// </summary>
        /// <param name="slipTemplate"></param>
        /// <returns></returns>
        static List<string> GetImagesName(string slipTemplate)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(slipTemplate);
            XmlNodeList nodes = xmlDoc.SelectNodes("//Print/Pic");
            List<string> imagesName = new List<string>();
            foreach (XmlNode node in nodes)
            {
                imagesName.Add(node.InnerText);
            }
            return imagesName;
        }
    }
}
