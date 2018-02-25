using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.IO;
using System.Xml;
using SeatManage.EnumType;

namespace AdvertManageTools.Code
{
    public class IssueSlipViewModel : INotifyPropertyChanged
    {
        ObservableCollection<SlipNodes> _SchoolList = new ObservableCollection<SlipNodes>();
        /// <summary>
        /// 学校信息
        /// </summary>
        public ObservableCollection<SlipNodes> SchoolList
        {
            get { return _SchoolList; }
            set
            {
                _SchoolList = value;
                OnPropertyChanged("SchoolList");
            }
        }
        List<int> _ReleaseSlipid = new List<int>();
        /// <summary>
        /// 优惠劵列表
        /// </summary>
        public List<int> ReleaseSlipid
        {
            get { return _ReleaseSlipid; }
            set { _ReleaseSlipid = value; }
        }
        /// <summary>
        /// 获取学校信息
        /// </summary>
        public void GetSchoolInfo()
        {
            try
            {
                List<AdvertManage.Model.AMS_SchoolModel> list = AdvertManage.BLL.AMS_SchoolBLL.GetAllSchoolInfo();
                foreach (AdvertManage.Model.AMS_SchoolModel model in list)
                {
                    List<AdvertManage.Model.AMS_CampusModel> campuslist = AdvertManage.BLL.AMS_CampusBLL.GetCampusInfoListBySchoolId(model.Id);
                    if (campuslist.Count > 0)
                    {
                        SlipNodes node = new SlipNodes();
                        node.Id = model.Id;
                        node.Name = model.Name;
                        node.Number = model.Number;
                        foreach (AdvertManage.Model.AMS_CampusModel campusmodel in campuslist)
                        {
                            SlipNodes campusnode = new SlipNodes();
                            campusnode.Id = campusmodel.Id;
                            campusnode.Name = campusmodel.Name;
                            campusnode.Number = campusmodel.Number;
                            node.ChildNode.Add(campusnode);
                        }
                        _SchoolList.Add(node);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            } 
        }
        /// <summary>
        /// 下发优惠券
        /// </summary>
        public bool SCRelease()
        {
            try
            {
                foreach (SlipNodes schoolnode in _SchoolList)
                {
                    foreach (SlipNodes campusnode in schoolnode.ChildNode)
                    {
                        if (campusnode.IsChecked)
                        {
                            foreach (int slipid in ReleaseSlipid)
                            {
                                int new_id = AdvertManage.BLL.SlipReleaseCampusBLL.AddSlipRelease(slipid, campusnode.Id);
                                if (new_id > 0)
                                {
                                    AdvertManage.Model.AMS_CommandListModel commandmodel = new AdvertManage.Model.AMS_CommandListModel();
                                    commandmodel.Command = AdvertManage.Model.Enum.CommandType.SlipCustomer;
                                    commandmodel.CommandId = new_id;
                                    commandmodel.FinishFlag = AdvertManage.Model.Enum.CommandHandleResult.Wait;
                                    commandmodel.ReleaseTime = DateTime.Now;
                                    commandmodel.SchoolId = schoolnode.Id;
                                    if (AdvertManage.BLL.AMS_CommandBLL.AddAMS_CommandList(commandmodel) == AdvertManage.Model.Enum.HandleResult.Failed)
                                    {
                                        throw new Exception("获取优惠券命令发布失败！");
                                    }
                                }
                                else
                                {
                                    throw new Exception("优惠券下发失败！");
                                }
                            }
                        }
                    }


                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
        /// <summary>
        /// 导出离线版优惠券
        /// </summary>
        public bool SCDownLoad(string dripath)
        {
            try
            {
                foreach (SlipNodes schoolnode in _SchoolList)
                {
                    foreach (SlipNodes campusnode in schoolnode.ChildNode)
                    {
                        if (campusnode.IsChecked)
                        {
                            string DialogLocad = dripath + "\\" + DateTime.Now.ToShortDateString() + "_" + schoolnode.Name + "_" + campusnode.Name + "_优惠劵广告\\";
                            DirectoryInfo d = new DirectoryInfo(DialogLocad);
                            if (d.Exists)
                            {
                                throw new Exception("已存在相同的文件夹！");
                            }
                            d.Create();
                            //创建一个xml对象
                            XmlDocument xmlDoc = new XmlDocument();
                            //创建开头
                            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
                            xmlDoc.AppendChild(dec);
                            //创建根节点
                            XmlElement root = xmlDoc.CreateElement("Root");
                            //创建二级节点
                            XmlElement SecNode = xmlDoc.CreateElement("ADItems");
                            root.AppendChild(SecNode);
                            //遍历全部的项目
                            foreach (int itemid in _ReleaseSlipid)
                            {
                                AdvertManage.Model.AMS_SlipCustomerModel itemModel = AdvertManage.BLL.AMS_SlipCustomerBLL.GetSlipCustomerById(itemid);
                                XmlElement ThirdNode = xmlDoc.CreateElement("AD");
                                ThirdNode.SetAttribute("no", itemModel.Number);
                                ThirdNode.SetAttribute("CampusNum", campusnode.Number);
                                ThirdNode.SetAttribute("EffectDate", itemModel.EffectDate.ToString());
                                ThirdNode.SetAttribute("EndDate", itemModel.EndDate.ToString());
                                ThirdNode.SetAttribute("ImageUrl", itemModel.ImageUrl);
                                ThirdNode.SetAttribute("CustomerImage", itemModel.CustomerImage);
                                if (itemModel.IsPrint)
                                {
                                    ThirdNode.SetAttribute("IsPrint", "1");
                                }
                                else
                                {
                                    ThirdNode.SetAttribute("IsPrint", "0");
                                }
                                ThirdNode.SetAttribute("SlipTemplate", itemModel.SlipTemplate);
                                SecNode.AppendChild(ThirdNode);
                                //复制图片
                                AdvertManage.BLL.FileOperate fo = new AdvertManage.BLL.FileOperate();
                                if (itemModel.IsPrint)
                                {
                                    XmlDocument Templatedoc = new XmlDocument();
                                    Templatedoc.LoadXml(itemModel.SlipTemplate);
                                    XmlElement Templateroot = Templatedoc.DocumentElement;
                                    XmlNodeList Templatexnlist = ((XmlNode)Templateroot).ChildNodes;
                                    for (int j = 0; j < Templatexnlist.Count; j++)
                                    {
                                        if (Templatexnlist[j].Name == "Pic")
                                        {
                                            if (!fo.FileDownLoad((d.FullName + Templatexnlist[j].InnerText), Templatexnlist[j].InnerText, SeatManageSubsystem.SlipCustomer))
                                            {
                                                throw new Exception("优惠券图片下载失败！");
                                            }
                                        }
                                    }
                                }
                                if (!fo.FileDownLoad((d.FullName + itemModel.CustomerImage), itemModel.CustomerImage, SeatManageSubsystem.SlipCustomer))
                                {
                                    throw new Exception("优惠图片下载失败！");
                                }
                                if (!fo.FileDownLoad((d.FullName + itemModel.ImageUrl), itemModel.ImageUrl, SeatManageSubsystem.SlipCustomer))
                                {
                                    throw new Exception("logo图片下载失败！");
                                }
                            }
                            //在根节点中添加二级节点
                            root.AppendChild(SecNode);
                            //添加根节点
                            xmlDoc.AppendChild(root);
                            //写入XML
                            string xmlpath = DialogLocad + "\\SlipCustomerList.xml";
                            //写入文件
                            FileStream fs = new FileStream(xmlpath, FileMode.Create, FileAccess.Write);
                            StreamWriter sw = new StreamWriter(fs);
                            sw.Write(xmlDoc.OuterXml);
                            sw.Close();
                            fs.Close();
                        }
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            } 
        }
        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 学校节点
    /// </summary>
    public class SlipNodes : INotifyPropertyChanged
    {
        bool _IsChecked = false;
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChecked
        {
            get
            {
                return _IsChecked;
            }
            set
            {
                _IsChecked = value;
                foreach (SlipNodes sn in ChildNode)
                {
                    sn.IsChecked = value;
                }
                OnPropertyChanged("IsChecked");
            }
        }
        string _Name = "";
        /// <summary>
        /// 学校名字
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
        }
        int _Id = -1;
        /// <summary>
        /// 学校Id
        /// </summary>
        public int Id
        {
            get { return _Id; }
            set
            {
                _Id = value;
                OnPropertyChanged("Id");
            }
        }
        string _Number = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                OnPropertyChanged("SchoolNumber");
            }
        }
        ObservableCollection<SlipNodes> _ChildNode = new ObservableCollection<SlipNodes>();
        /// <summary>
        /// 校区列表
        /// </summary>
        public ObservableCollection<SlipNodes> ChildNode
        {
            get { return _ChildNode; }
            set
            {
                _ChildNode = value;
                OnPropertyChanged("ChildNode");
            }
        }

        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 校区节点
    /// </summary>
    public class SlipCampusNodes : INotifyPropertyChanged
    {
        bool _IsChecked = false;
        /// <summary>
        /// 是否被选择
        /// </summary>
        public bool IsChecked
        {
            get { return _IsChecked; }
            set
            {
                _IsChecked = value;
                OnPropertyChanged("IsChecked");
            }
        }
        string _CampusName = "";
        /// <summary>
        /// 学校名字
        /// </summary>
        public string CampusName
        {
            get { return _CampusName; }
            set
            {
                _CampusName = value;
                OnPropertyChanged("CampusName");
            }
        }
        int _CampusId = -1;
        /// <summary>
        /// 学校Id
        /// </summary>
        public int CampusId
        {
            get { return _CampusId; }
            set
            {
                _CampusId = value;
                OnPropertyChanged("CampusId");
            }
        }
        string _CampusNumber = "";
        /// <summary>
        /// 学校编号
        /// </summary>
        public string CampusNumber
        {
            get { return _CampusNumber; }
            set
            {
                _CampusNumber = value;
                OnPropertyChanged("CampusNumber");
            }
        }
        #region 通知事件
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
