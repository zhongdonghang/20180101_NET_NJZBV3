using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace AdvertManage.DataTransfer
{
    public class SlipCustomer
    {
        /// <summary>
        /// 根据ID获取优惠券信息
        /// </summary>
        /// <returns></returns>
        public static bool GetSlipCustomer(int id)
        {
            //TODO:未测试
            try
            {
                 Model.AMS_SlipCustomerModel  advertModel = BLL.SlipReleaseCampusBLL.GetSlipReleaseListById(id);
                AdvertManage.BLL.FileOperate advertfileOperate = new AdvertManage.BLL.FileOperate();
                SeatManage.Bll.FileOperate seatmanagefileOperate = new SeatManage.Bll.FileOperate();
                if(advertModel!=null )
                {
                    SeatManage.ClassModel.AMS_SlipCustomer seatModel = SeatManage.Bll.AMS_SlipCustomer.GetSlipCustomerByNum(advertModel.Number);
                    if (seatModel == null)
                    {
                        //优惠券在数据库中不存在：先执行下载操作，然后更新
                        string ImageUrlFullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, advertModel.ImageUrl);
                        try
                        {
                            advertfileOperate.FileDownLoad(ImageUrlFullName, advertModel.ImageUrl, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("优惠券{0}下载失败：{1}", advertModel.Number, ex.Message));
                        }
                        try
                        {
                            if (seatmanagefileOperate.UpdateFile(ImageUrlFullName, advertModel.ImageUrl, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                            {
                                //上传完成，执行删除操作
                                File.Delete(ImageUrlFullName);
                            }
                            else
                            {
                                return false;
                            }
                        }
                        catch (Exception ex)
                        {
                            SeatManage.SeatManageComm.WriteLog.Write(string.Format("优惠券{0}上传失败：{1}", advertModel.Number, ex.Message));
                        }
                        string CustomerImagefullName = string.Format(@"{0}{1}", ServiceSet.TempFilePath, advertModel.CustomerImage);
                        advertfileOperate.FileDownLoad(CustomerImagefullName, advertModel.CustomerImage, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                        if (seatmanagefileOperate.UpdateFile(CustomerImagefullName, advertModel.CustomerImage, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                        {
                            File.Delete(CustomerImagefullName);
                        }
                        else
                        {
                            return false;
                        }
                        if (advertModel.IsPrint)
                        {
                            XmlDocument Templatedoc = new XmlDocument();
                            Templatedoc.LoadXml(advertModel.SlipTemplate);
                            XmlElement Templateroot = Templatedoc.DocumentElement;
                            XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                            for (int j = 0; j < Templatexnlist.Count; j++)
                            {
                                if (Templatexnlist[j].Name == "Pic")
                                {
                                    try
                                    {
                                        advertfileOperate.FileDownLoad((ServiceSet.TempFilePath + Templatexnlist[j].InnerText), Templatexnlist[j].InnerText, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                                    }
                                    catch (Exception ex)
                                    {
                                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("优惠券{0}下载失败：{1}", advertModel.Number, ex.Message));
                                    }
                                    try
                                    {
                                        if (seatmanagefileOperate.UpdateFile((ServiceSet.TempFilePath + Templatexnlist[j].InnerText), Templatexnlist[j].InnerText, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer))
                                        {
                                            //上传完成，执行删除操作
                                            File.Delete(ServiceSet.TempFilePath + Templatexnlist[j].InnerText);
                                        }
                                        else
                                        {
                                            return false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("优惠券{0}上传失败：{1}", advertModel.Number, ex.Message));
                                    }
                                }
                            }
                        }
                    }
                    
                    if (seatModel != null && seatModel.CampusNum == advertModel.CampusNum)
                    {
                        //获取学校数据库中的优惠券。如果存在并且校区编号相同执行更新。 
                        seatModel.CampusNum = advertModel.CampusNum;
                        seatModel.CustomerLogo = advertModel.CustomerImage;
                        seatModel.EffectDate = advertModel.EffectDate;
                        seatModel.EndDate = advertModel.EndDate;
                        seatModel.ImageName = advertModel.ImageUrl;
                        seatModel.No = advertModel.Number;
                        seatModel.SlipTemplate = advertModel.SlipTemplate;
                        seatModel.IsPrint = advertModel.IsPrint;
                        SeatManage.Bll.AMS_SlipCustomer.UpdateSlipCustomer(seatModel); 
                    }
                    else  
                    { 
                        seatModel = new SeatManage.ClassModel.AMS_SlipCustomer();
                        seatModel.CampusNum = advertModel.CampusNum;
                        seatModel.CustomerLogo = advertModel.CustomerImage;
                        seatModel.EffectDate = advertModel.EffectDate;
                        seatModel.EndDate = advertModel.EndDate;
                        seatModel.ImageName = advertModel.ImageUrl;
                        seatModel.No = advertModel.Number;
                        seatModel.SlipTemplate = advertModel.SlipTemplate;
                        seatModel.IsPrint = advertModel.IsPrint;
                        SeatManage.Bll.AMS_SlipCustomer.AddSlipCustomer(seatModel); 
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取优惠券客户失败：{0}",ex.Message));
                return false;
            }
        }
        /// <summary>
        /// 添加优惠券打印信息
        /// 根据具体日期，每天上传一次，上传完成后把本地的打印次数和查看次数改为0.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UploadSlipPrintInfo()
        {
            try
            {
                List<SeatManage.ClassModel.AMS_SlipCustomer> slipCustomerList = SeatManage.Bll.AMS_SlipCustomer.GetSlipCustomerList(null);
                List<Model.AMS_SlipPrintInfoModel> advertPrintInfo = new List<Model.AMS_SlipPrintInfoModel>();
                //TODO:改为直接传递List，
                foreach (SeatManage.ClassModel.AMS_SlipCustomer model in slipCustomerList)
                {
                    Model.AMS_SlipPrintInfoModel slipPrintModel = new Model.AMS_SlipPrintInfoModel();
                    slipPrintModel.CompusNum = model.CampusNum;
                    slipPrintModel.SlipCustomerNum = model.No;
                    slipPrintModel.PrintAmount = model.PrintAmount;
                    slipPrintModel.LookOverAmount = model.LookOverAmount;
                    slipPrintModel.Date = DateTime.Now;
                    advertPrintInfo.Add(slipPrintModel);
                }
                if (BLL.AMS_SlipPrintInfoBLL.AddSlipPrintInfo(advertPrintInfo) == Model.Enum.HandleResult.Successed)
                {
                    foreach (SeatManage.ClassModel.AMS_SlipCustomer model in slipCustomerList)
                    {
                        model.PrintAmount = 0;
                        model.LookOverAmount = 0;
                        SeatManage.Bll.AMS_SlipCustomer.UpdateSlipCustomer(model);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
