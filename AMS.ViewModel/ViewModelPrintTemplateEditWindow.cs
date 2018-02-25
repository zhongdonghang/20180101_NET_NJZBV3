using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Drawing;
using System.IO;
using SeatManage.Bll;

namespace AMS.ViewModel
{
    public class ViewModelPrintTemplateEditWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPrintTemplateEditWindow";
        private ViewModelProgressBar _vm_ProgressBar = new ViewModelProgressBar();
        /// <summary>
        /// 进度条
        /// </summary>
        public ViewModelProgressBar Vm_ProgressBar
        {
            get { return _vm_ProgressBar; }
            set { _vm_ProgressBar = value; OnPropertyChanged("Vm_ProgressBar"); }
        }
        private ViewModelCustomerListWindow _CustomerList = new ViewModelCustomerListWindow();
        /// <summary>
        /// 客户列表
        /// </summary>
        public ViewModelCustomerListWindow CustomerList
        {
            get { return _CustomerList; }
            set { _CustomerList = value; OnPropertyChanged("CustomerList"); }
        }
        /// <summary>
        /// 客户ID
        /// </summary>
        public int CustomerID
        {
            get { return _PrintModel.CustomerId; }
            set { _PrintModel.CustomerId = value; OnPropertyChanged("CustomerID"); }
        }
        private bool _IsOldType = true;
        /// <summary>
        /// 是否煎肉锅旧版本
        /// </summary>
        public bool IsOldType
        {
            get { return _IsOldType; }
            set { _IsOldType = value; OnPropertyChanged("IsOldType"); }
        }

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private AMS.Model.AMS_PrintTemplate _PrintModel = new Model.AMS_PrintTemplate();
        /// <summary>
        /// 打印model
        /// </summary>
        public AMS.Model.AMS_PrintTemplate PrintModel
        {
            get { return _PrintModel; }
            set { _PrintModel = value; OnPropertyChanged("PrintModel"); }
        }
        private ViewModelPrintTemplateInfo _VM_Template = new ViewModelPrintTemplateInfo();
        /// <summary>
        /// 打印模板
        /// </summary>
        public ViewModelPrintTemplateInfo VM_Template
        {
            get { return _VM_Template;   }
            set { _VM_Template = value; OnPropertyChanged("VM_Template"); }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string No
        {
            get { return _PrintModel.Number; }
            set { _PrintModel.Number = value; VM_Template.TemplateNo = value; OnPropertyChanged("No"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PrintModel.Name; }
            set { _PrintModel.Name = value; OnPropertyChanged("Name"); }
        }
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string EffectDate
        {
            get
            {
                if (_PrintModel.EffectDate != null)
                {
                    return _PrintModel.EffectDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.ToLongDateString();
                }
            }
            set { _PrintModel.EffectDate = DateTime.Parse(value); OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            get
            {
                if (_PrintModel.EndDate != null)
                {
                    return _PrintModel.EndDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.AddDays(30).ToLongDateString();
                }
            }
            set { _PrintModel.EndDate = DateTime.Parse(value); OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return _PrintModel.Describe; }
            set { _PrintModel.Describe = value; OnPropertyChanged("Describe"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_PrintModel.OperatorName))
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _PrintModel.OperatorName;
                }
            }
        }

        private bool _IsEdit;
        /// <summary>
        /// 判断修改还是添加
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set
            {
                _IsEdit = value;
                OnPropertyChanged("IsEdit");
            }
        }

        public bool Save()
        {
            string functionName = "Save";
            try
            {
                if (_IsEdit)
                {
                    if (string.IsNullOrEmpty(_PrintModel.Number))
                    {
                        ErrorMessage = "凭条编号不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_PrintModel.Name))
                    {
                        ErrorMessage = "打印凭条名称不能为空！";
                        return false;
                    }
                    if (_PrintModel.EffectDate == null)
                    {
                        ErrorMessage = "请选择开始日期！";
                        return false;
                    }
                    if (_PrintModel.EndDate == null)
                    {
                        ErrorMessage = "请选择结束日期！";
                        return false;
                    }
                    if (_PrintModel.EndDate < _PrintModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期！";
                        return false;
                    }
                    if (_VM_Template.PrintIiemList.Count == 0)
                    {
                        ErrorMessage = "请添加打印项目！";
                        return false;
                    }
                    if (_PrintModel.CustomerId < 0)
                    {
                        ErrorMessage = "请选择凭条客户！";
                        return false;
                    }
                    _PrintModel.Operator = User.ID;
                    if (_IsOldType)
                    {
                        _PrintModel.Template = GetOldTypeTemp(VM_Template.XmlTxt);
                    }
                    else
                    {
                        _PrintModel.Template = VM_Template.XmlTxt;
                    }
                    //TODO:图片上传
                    string resultstr = "";
                    AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                    fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                    Vm_ProgressBar.ProgressType = "上传优惠券图片";
                    Vm_ProgressBar.FullProgress = 0;
                    foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                    {
                        if (item.IsImage)
                        {
                            Vm_ProgressBar.FullProgress++;
                        }
                    }
                    if (_VM_Template.PrintIiemList!=null)
                    {
                        foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                        {
                            if (item.IsImage)
                            {
                                Vm_ProgressBar.ProgressName = "正在上传\"" + item.ImageName + "\"……";
                                resultstr = fileUpload.UpdateFile(item.ImagePath, item.ImageName, SeatManage.EnumType.SeatManageSubsystem.SeatSlip);
                                if (!string.IsNullOrEmpty(resultstr))
                                {
                                    ErrorMessage = string.Format("图片{0}上传失败！{1}", item.ImageName, resultstr);
                                    return false;
                                }
                            }
                        }
                    }
                    resultstr = AMS.ServiceProxy.IPrintTemplateService.UpdatePrintTemplate(_PrintModel);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = string.Format("保存失败！{0}", resultstr);
                        return false;
                    }
                    return true;
                }
                else
                {
                    #region 添加
                    if (string.IsNullOrEmpty(_PrintModel.Number))
                    {
                        ErrorMessage = "凭条编号不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_PrintModel.Name))
                    {
                        ErrorMessage = "打印凭条名称不能为空！";
                        return false;
                    }
                    if (_PrintModel.EffectDate == null)
                    {
                        ErrorMessage = "请选择开始日期！";
                        return false;
                    }
                    if (_PrintModel.EndDate == null)
                    {
                        ErrorMessage = "请选择结束日期！";
                        return false;
                    }
                    if (_PrintModel.EndDate < _PrintModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期！";
                        return false;
                    }
                    if (_VM_Template.PrintIiemList.Count == 0)
                    {
                        ErrorMessage = "请添加打印项目！";
                        return false;
                    }
                    if (_PrintModel.CustomerId < 0)
                    {
                        ErrorMessage = "请选择凭条客户！";
                        return false;
                    }
                    _PrintModel.Operator = User.ID;
                    if (_IsOldType)
                    {
                        _PrintModel.Template = GetOldTypeTemp(VM_Template.XmlTxt);
                    }
                    else
                    {
                        _PrintModel.Template = VM_Template.XmlTxt;
                    }
                    //TODO:图片上传
                    string resultstr = "";
                    AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                    fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                    Vm_ProgressBar.ProgressType = "上传优惠券图片";
                    Vm_ProgressBar.FullProgress = 0;
                    foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                    {
                        if (item.IsImage)
                        {
                            Vm_ProgressBar.FullProgress++;
                        }
                    }
                    foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                    {
                        if (item.IsImage)
                        {
                            Vm_ProgressBar.ProgressName = "正在上传\"" + item.ImageName + "\"……";
                            resultstr = fileUpload.UpdateFile(item.ImagePath, item.ImageName, SeatManage.EnumType.SeatManageSubsystem.SeatSlip);
                            if (!string.IsNullOrEmpty(resultstr))
                            {
                                ErrorMessage = string.Format("图片{0}上传失败！{1}", item.ImageName, resultstr);
                                return false;
                            }
                        }
                    }
                    resultstr = AMS.ServiceProxy.IPrintTemplateService.AddNewPrintTemplate(_PrintModel);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = string.Format("保存失败！{0}", resultstr);
                        return false;
                    }
                    return true;
                    #endregion
                }

            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return false;
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return false;
            }
        }
        void fileUpload_HandleProgress(int message)
        {
            Vm_ProgressBar.NowProgress = message.ToString();
        }
        /// <summary>
        /// 旧版本的打印凭条
        /// </summary>
        /// <param name="advertTemlate"></param>
        /// <returns></returns>
        private string GetOldTypeTemp(string advertTemlate)
        {
             StringBuilder template = new StringBuilder();
                template.Append("<?xml version='1.0' encoding='utf-8'?>");
                template.Append("<Print>");
                template.Append("<Content font='黑体' size='14' bold='Y' italic='N'>    #SeatNo#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>阅览室名称：#ReadingRoomName#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>学号：#CardNo#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>姓名：#StuName#</Content>");
                template.Append("<Content font='宋体' size='8' bold='N' italic='N'>原读者：#SecCardNo#</Content>");
                template.Append("<Content font='宋体' size='7' bold='N' italic='N'>日期:#DateTime#</Content>");
                template.Append("<Content font='宋体' size='7' bold='N' italic='N'>等待结束:#EndDateTime#</Content>");
                template.Append("<Content font='宋体' size='7' bold='Y' italic='N'>离开请刷卡</Content>");
                template.Append("</Print>");
           
                XmlDocument defaultdoc = new XmlDocument();//原模板
                defaultdoc.LoadXml(template.ToString());
                XmlNodeList defaultnodes = defaultdoc.SelectSingleNode("//Print").ChildNodes;

                XmlDocument advertdoc = new XmlDocument();//广告模板
                advertdoc.LoadXml(advertTemlate);
                XmlNodeList advertnodes = advertdoc.SelectSingleNode("//Print").ChildNodes;

                StringBuilder newTemplate = new StringBuilder();
                newTemplate.Append("<?xml version='1.0' encoding='utf-8'?>");
                newTemplate.Append("<Print>");
                foreach (XmlNode item in defaultnodes)
                {
                    newTemplate.Append(item.OuterXml);
                }
                foreach (XmlNode item in advertnodes)
                {
                    newTemplate.Append(item.OuterXml);
                }
                newTemplate.Append("</Print>");
                return newTemplate.ToString();
        }

        public void ToListXML()
        {
            _VM_Template.PrintIiemList.Clear();
            XmlDocument Xmls = new XmlDocument();
            Xmls.LoadXml(_PrintModel.Template);
            XmlNode nodes = Xmls.SelectSingleNode("Print");
            if (nodes.ChildNodes[0].InnerText == "    #SeatNo#")
            {
                for (int i = 0; i < 8; i++)
                {
                    nodes.RemoveChild(nodes.ChildNodes[0]);
                }
                _IsOldType = true;
            }
            else
            {
                _IsOldType = false;
            }

            nodes.RemoveChild(nodes.ChildNodes[nodes.ChildNodes.Count-1]);
            foreach (XmlNode xmln in nodes.ChildNodes) 
            {
                ViewModel.ViewModelPrintItem ss = new ViewModelPrintItem();
                switch (xmln.LocalName)
                {
                    case "Content":
                        ss.FontSize = Convert.ToInt32(xmln.Attributes["size"].Value);
                        ss.TextInfo = xmln.InnerText;
                        ss.IsItalic = xmln.Attributes["italic"].Value;
                        ss.IsBold = xmln.Attributes["bold"].Value;
                        break;
                    case "Pic":
                        Image img = imageLocation(xmln.InnerText);
                        string url = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, xmln.InnerText);
                                string ssss = url;
                                ss.IsImage = true;
                                ss.ImageInfo = new System.Windows.Media.Imaging.BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));

                        break;
                    default:
                        break;
                }
                _VM_Template.PrintIiemList.Add(ss);
            }
            OnPropertyChanged("VM_Template"); 
        }
        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Image imageLocation(string imageName)
        {
            /**
             *  判断图片在临时文件夹中是否存在。如果存在则不下载。
             *  修改时间：2013-9-5 王随
             */
            string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imageName);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                AMS.ServiceProxy.FileOperate fileDownload = new AMS.ServiceProxy.FileOperate();
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.SeatSlip)!="")
                {
                    return
                        null;
                }
            }
            Image img = Image.FromFile(filePath);
            return img;
        }
    }
}
