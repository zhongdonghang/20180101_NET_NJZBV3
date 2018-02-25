using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.IO;
using System.Xml;

namespace AMS.ViewModel
{
    public class ViewModelSlipCustomerEditWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelSlipCustomerEditWindow";
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
            get { return _SlipModel.CustomerId; }
            set { _SlipModel.CustomerId = value; OnPropertyChanged("CustomerID"); }
        }
        private ViewModelPrintItem _EditItem = new ViewModelPrintItem();
        /// <summary>
        /// 编辑项
        /// </summary>
        public ViewModelPrintItem EditItem
        {
            get { return _EditItem; }
            set { _EditItem = value; OnPropertyChanged("VM_Template"); }
        }

        private ViewModelPrintTemplateInfo _VM_Template = new ViewModelPrintTemplateInfo();
        /// <summary>
        /// 打印模板
        /// </summary>
        public ViewModelPrintTemplateInfo VM_Template
        {
            get { return _VM_Template; }
            set { _VM_Template = value; OnPropertyChanged("VM_Template"); }
        }
        private AMS.Model.AMS_SlipCustomer _SlipModel = new Model.AMS_SlipCustomer();
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.AMS_SlipCustomer SlipModel
        {
            get { return _SlipModel; }
            set { _SlipModel = value; OnPropertyChanged("SlipModel"); }
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
        private System.Windows.Media.Imaging.BitmapImage _LogoImageInfo;
        /// <summary>
        /// 滚动logo图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage LogoImageInfo
        {
            get { return _LogoImageInfo; }
            set
            {
                _LogoImageInfo = value;
                _SlipModel.CustomerImage = _LogoImageInfo.UriSource.LocalPath;

                OnPropertyChanged("LogoImageInfo");
            }
        }
        private string LogoImageInfoPath;
        private System.Windows.Media.Imaging.BitmapImage _PrintWindowImageInfo;
        /// <summary>
        /// 打印窗体图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PrintWindowImageInfo
        {
            get { return _PrintWindowImageInfo; }
            set
            {
                _PrintWindowImageInfo = value;
                _SlipModel.ImageUrl = _PrintWindowImageInfo.UriSource.LocalPath;
                OnPropertyChanged("PrintWindowImageInfo");
            }
        }
        private string PrintWindowImageInfoPath;
        /// <summary>
        /// 编号
        /// </summary>
        public string SlipNo
        {
            get { return _SlipModel.Number; }
            set { _SlipModel.Number = value; VM_Template.TemplateNo = value; OnPropertyChanged("SlipNo"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string SlipName
        {
            get { return _SlipModel.SlipName; }
            set { _SlipModel.SlipName = value; OnPropertyChanged("SlipName"); }
        }
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string EffectDate
        {
            get
            {
                if (_SlipModel.EffectDate != null)
                {
                    return _SlipModel.EffectDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.ToLongDateString();
                }
            }
            set { _SlipModel.EffectDate = DateTime.Parse(value); OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            get
            {
                if (_SlipModel.EndDate != null)
                {
                    return _SlipModel.EndDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.AddDays(30).ToLongDateString();
                }
            }
            set { _SlipModel.EndDate = DateTime.Parse(value); OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return _SlipModel.Describe; }
            set { _SlipModel.Describe = value; OnPropertyChanged("Describe"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_SlipModel.OperatorName))
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _SlipModel.OperatorName;
                }
            }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get
            {
                if (_SlipModel.IsPrint == null)
                {
                    return true;
                }
                else
                {
                    return _SlipModel.IsPrint.Value;
                }
            }
            set { _SlipModel.IsPrint = value; OnPropertyChanged("IsPrint"); }
        }
        private bool _IsEdit;

        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; }
        }

        public bool Save()
        {
            string functionName = "Save";
            try
            {
                if (IsEdit)
                {
                    if (string.IsNullOrEmpty(_SlipModel.Number))
                    {
                        ErrorMessage = "优惠券编号不能为空！";
                        return false;
                    }
                    if (string.IsNullOrEmpty(_SlipModel.SlipName))
                    {
                        ErrorMessage = "优惠券名称不能为空！";
                        return false;
                    }
                    if (LogoImageInfo == null)
                    {
                        ErrorMessage = "滚动LOGO图片不能为空！";
                        return false;
                    }
                    if (PrintWindowImageInfo == null)
                    {
                        ErrorMessage = "打印弹窗图片不能为空！";
                        return false;
                    }
                    if (_SlipModel.EffectDate == null)
                    {
                        ErrorMessage = "请选择开始日期！";
                        return false;
                    }
                    if (_SlipModel.EndDate == null)
                    {
                        ErrorMessage = "请选择结束日期！";
                        return false;
                    }
                    if (_SlipModel.EndDate < _SlipModel.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期！";
                        return false;
                    }
                    if (_VM_Template.PrintIiemList.Count == 0)
                    {
                        ErrorMessage = "请添加打印项目！";
                        return false;
                    }
                    if (_SlipModel.CustomerId < 0)
                    {
                        ErrorMessage = "请选择优惠券客户！";
                        return false;
                    }
                    _SlipModel.Operator = User.ID;
                    _SlipModel.SlipTemplate = VM_Template.XmlTxt;
                    //TODO:图片上传，优惠券保存
                    string result = "";
                    AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                    fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                    Vm_ProgressBar.ProgressType = "上传优惠券图片";
                    Vm_ProgressBar.FullProgress = 2;
                    foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                    {
                        if (item.IsImage)
                        {
                            Vm_ProgressBar.FullProgress++;
                        }
                    }
                    Vm_ProgressBar.ProgressName = "正在上传\"" + _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1) + "\"……";
                    result = fileUpload.UpdateFile(_SlipModel.CustomerImage, _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("上传滚动Logo图片失败！{0}", result);
                        return false;
                    }
                    _SlipModel.CustomerImage = _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1);
                    Vm_ProgressBar.ProgressName = "正在上传\"" + _SlipModel.ImageUrl + "\"……";
                    result = fileUpload.UpdateFile(_SlipModel.ImageUrl, _SlipModel.Number + "_" + _SlipModel.ImageUrl.Substring(_SlipModel.ImageUrl.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("上传打印界面图片失败！{0}", result);
                        return false;
                    }
                    _SlipModel.ImageUrl = _SlipModel.Number + "_" + _SlipModel.ImageUrl.Substring(_SlipModel.ImageUrl.LastIndexOf("\\") + 1);
                    foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                    {
                        if (item.IsImage)
                        {
                            Vm_ProgressBar.ProgressName = "正在上传\"" + item.ImageName + "\"……";
                            result = fileUpload.UpdateFile(item.ImagePath, item.ImageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                            if (!string.IsNullOrEmpty(result))
                            {
                                ErrorMessage = string.Format("图片{0}上传失败！{1}", item.ImageName, result);
                                return false;
                            }
                        }
                    }
                    result = AMS.ServiceProxy.ISlipCustomerService.UpdateSlipCustomer(_SlipModel);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("保存优惠券失败！{0}", result);
                        return false;
                    }
                    return true; 
                }
                else
                {
                #region 添加
                if (string.IsNullOrEmpty(_SlipModel.Number))
                {
                    ErrorMessage = "优惠券编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_SlipModel.SlipName))
                {
                    ErrorMessage = "优惠券名称不能为空！";
                    return false;
                }
                if (LogoImageInfo == null)
                {
                    ErrorMessage = "滚动LOGO图片不能为空！";
                    return false;
                }
                if (PrintWindowImageInfo == null)
                {
                    ErrorMessage = "打印弹窗图片不能为空！";
                    return false;
                }
                if (_SlipModel.EffectDate == null)
                {
                    ErrorMessage = "请选择开始日期！";
                    return false;
                }
                if (_SlipModel.EndDate == null)
                {
                    ErrorMessage = "请选择结束日期！";
                    return false;
                }
                if (_SlipModel.EndDate < _SlipModel.EffectDate)
                {
                    ErrorMessage = "结束日期不能小于开始日期！";
                    return false;
                }
                if (_VM_Template.PrintIiemList.Count == 0)
                {
                    ErrorMessage = "请添加打印项目！";
                    return false;
                }
                if (_SlipModel.CustomerId < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }
                _SlipModel.Operator = User.ID;
                _SlipModel.SlipTemplate = VM_Template.XmlTxt;
                //TODO:图片上传，优惠券保存
                string result = "";
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传优惠券图片";
                Vm_ProgressBar.FullProgress = 2;
                foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                {
                    if (item.IsImage)
                    {
                        Vm_ProgressBar.FullProgress++;
                    }
                }
                Vm_ProgressBar.ProgressName = "正在上传\"" + _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1) + "\"……";
                result = fileUpload.UpdateFile(_SlipModel.CustomerImage, _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("上传滚动Logo图片失败！{0}", result);
                    return false;
                }
                _SlipModel.CustomerImage = _SlipModel.Number + "_" + _SlipModel.CustomerImage.Substring(_SlipModel.CustomerImage.LastIndexOf("\\") + 1);
                Vm_ProgressBar.ProgressName = "正在上传\"" + _SlipModel.ImageUrl + "\"……";
                result = fileUpload.UpdateFile(_SlipModel.ImageUrl, _SlipModel.Number + "_" + _SlipModel.ImageUrl.Substring(_SlipModel.ImageUrl.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("上传打印界面图片失败！{0}", result);
                    return false;
                }
                _SlipModel.ImageUrl = _SlipModel.Number + "_" + _SlipModel.ImageUrl.Substring(_SlipModel.ImageUrl.LastIndexOf("\\") + 1);
                foreach (AMS.ViewModel.ViewModelPrintItem item in _VM_Template.PrintIiemList)
                {
                    if (item.IsImage)
                    {
                        Vm_ProgressBar.ProgressName = "正在上传\"" + item.ImageName + "\"……";
                        result = fileUpload.UpdateFile(item.ImagePath, item.ImageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer);
                        if (!string.IsNullOrEmpty(result))
                        {
                            ErrorMessage = string.Format("图片{0}上传失败！{1}", item.ImageName, result);
                            return false;
                        }
                    }
                }
                result = AMS.ServiceProxy.ISlipCustomerService.AddNewSlipCustomer(_SlipModel);
                if (!string.IsNullOrEmpty(result))
                {
                    ErrorMessage = string.Format("保存优惠券失败！{0}", result);
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

        public  void ToXML()
        {
            _VM_Template.PrintIiemList.Clear();
            XmlDocument Xmls = new XmlDocument();
            Xmls.LoadXml(_SlipModel.SlipTemplate);
            XmlNode nodes = Xmls.SelectSingleNode("Print");
            if (nodes.ChildNodes[0].InnerText == "    #SeatNo#")
            {
                for (int i = 0; i < 8; i++)
                {
                    nodes.RemoveChild(nodes.ChildNodes[0]);
                }
                //_IsOldType = true;
            }
            else
            {
                //_IsOldType = false;
            }

            nodes.RemoveChild(nodes.ChildNodes[nodes.ChildNodes.Count - 1]);
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
                        ss.IsImage = true;
                        ss.ImageInfo = new System.Windows.Media.Imaging.BitmapImage(new Uri(url, UriKind.RelativeOrAbsolute));
                        break;
                    default:
                        break;
                }
                //滚动logo图片
                Image Logo=imageLocation(_SlipModel.ImageUrl);
                string logourl = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, _SlipModel.ImageUrl);
                _LogoImageInfo = new System.Windows.Media.Imaging.BitmapImage(new Uri(logourl, UriKind.RelativeOrAbsolute));
                //客户图片
                Image KH = imageLocation(_SlipModel.ImageUrl);
                string KHurl = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, _SlipModel.ImageUrl);
                _PrintWindowImageInfo = new System.Windows.Media.Imaging.BitmapImage(new Uri(KHurl, UriKind.RelativeOrAbsolute));
                
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
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomer) != "")
                {
                    return
                        null;
                }
            }
            Image img = Image.FromFile(filePath);
            return img;
        }

    }
    public class ViewModelPrintTemplateInfo : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPrintTemplateInfo";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<ViewModelPrintItem> _PrintIiemList = new ObservableCollection<ViewModelPrintItem>();
        /// <summary>
        /// 项目列表
        /// </summary>
        public ObservableCollection<ViewModelPrintItem> PrintIiemList
        {
            get { return _PrintIiemList; }
            set { _PrintIiemList = value; OnPropertyChanged("PrintIiemList"); }
        }
        private string _TemplateNo = "";
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            get { return _TemplateNo; }
            set
            {
                _TemplateNo = value;
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    item.TemplateNo = value;
                }
                OnPropertyChanged("TemplateNo");
            }
        }
        /// <summary>
        /// 打印图片信息
        /// </summary>
        public Dictionary<string, string> PrintImages
        {
            get
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    if (item.IsImage)
                    {
                        dic.Add(item.ImageName, item.ImagePath);
                    }
                }
                return dic;
            }
        }
        /// <summary>
        /// 打印信息
        /// </summary>
        public string XmlTxt
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if ((!PrintIiemList[PrintIiemList.Count - 1].IsImage) || PrintIiemList[PrintIiemList.Count - 1].IsImage && (PrintIiemList[PrintIiemList.Count - 1].ImageName != _TemplateNo + "_校园GO购logo.png"))
                {
                    AddItem(AddImage(AppDomain.CurrentDomain.BaseDirectory + "\\校园GO购logo.png"));
                }
                sb.Append("<?xml version='1.0' encoding='utf-8'?>");
                sb.Append("<Print>");
                foreach (ViewModelPrintItem item in PrintIiemList)
                {
                    if (item.IsImage || !item.IsImage && !string.IsNullOrEmpty(item.TextInfo))
                    {
                        sb.Append(item.XmlTxt);
                    }
                }
                sb.Append("</Print>");
                return sb.ToString();
            }
        }
        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="index"></param>
        public void UpMoveItem(int index)
        {
            string functionName = "UpMoveItem";
            try
            {
                if (index < 1)
                {
                    ErrorMessage = "已移动到队列最前！";
                    return;
                }
                PrintIiemList.Move(index, index - 1);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="index"></param>
        public void DownMoveItem(int index)
        {
            string functionName = "DownMoveItem";
            try
            {
                if (index == PrintIiemList.Count - 1)
                {
                    ErrorMessage = "已移动到队列最后！";
                    return;
                }
                PrintIiemList.Move(index, index + 1);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="index"></param>
        public void DeleteItem(int index)
        {
            string functionName = "DeleteItem";
            try
            {
                PrintIiemList.RemoveAt(index);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 添加项目
        /// </summary>
        /// <param name="index"></param>
        public void AddItem(ViewModelPrintItem item)
        {
            string functionName = "AddItem";
            try
            {
                item.TemplateNo = TemplateNo;
                PrintIiemList.Add(item);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 插入项目
        /// </summary>
        /// <param name="index"></param>
        public void InsertItem(ViewModelPrintItem item, int index)
        {
            string functionName = "InsertItem";
            try
            {
                item.TemplateNo = TemplateNo;
                PrintIiemList.Insert(index, item);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        public ViewModelPrintItem AddImage(string imagePath)
        {
            string functionName = "AddImage";
            try
            {
                ViewModelPrintItem item = new ViewModelPrintItem();
                item.IsImage = true;
                item.ImageInfo = new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                return item;
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return new ViewModelPrintItem();
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return new ViewModelPrintItem();
            }
        }
    }
    public class ViewModelPrintItem : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPrintItem";

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }

        private string _TemplateNo = "";
        /// <summary>
        /// 模板编号
        /// </summary>
        public string TemplateNo
        {
            get { return _TemplateNo; }
            set
            {
                _TemplateNo = value;
                if (IsImage)
                {
                    _ImageName = _TemplateNo + "_" + _ImageInfo.UriSource.LocalPath.Substring(_ImageInfo.UriSource.LocalPath.LastIndexOf("\\") + 1);
                }
                OnPropertyChanged("TemplateNo");
            }
        }

        private string _TextInfo = "";
        /// <summary>
        /// 文本内容
        /// </summary>
        public string TextInfo
        {
            get
            {
                return _TextInfo;
            }
            set
            {
                _TextInfo = Temptext(value);
                OnPropertyChanged("TextInfo");
            }
        }

        private double _ImageHeight;
        /// <summary>
        /// 图片高度
        /// </summary>
        public double ImageHeight
        {
            get { return _ImageHeight; }
        }
        private double _ImageWidth;
        /// <summary>
        /// 图片宽度
        /// </summary>
        public double ImageWidth
        {
            get { return _ImageWidth; }
        }
        private BitmapImage _ImageInfo;
        /// <summary>
        /// 图片
        /// </summary>
        public BitmapImage ImageInfo
        {
            get { return _ImageInfo; }
            set
            {
                _ImageInfo = value;
                _ImageName = _TemplateNo + "_" + _ImageInfo.UriSource.LocalPath.Substring(_ImageInfo.UriSource.LocalPath.LastIndexOf("\\") + 1);
                _ImagePath = _ImageInfo.UriSource.LocalPath;
                _ImageHeight = _ImageInfo.Height;
                _ImageWidth = _ImageInfo.Width;
                OnPropertyChanged("ImageInfo");
            }
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
        }
        /// <summary>
        /// 图片名称
        /// </summary>
        private string _ImageName;
        public string ImageName
        {
            get { return _ImageName; }
        }
        private bool _IsImage = false;
        /// <summary>
        /// 是否是图片
        /// </summary>
        public bool IsImage
        {
            get { return _IsImage; }
            set { _IsImage = value; OnPropertyChanged("IsImage"); OnPropertyChanged("ImageControlVisible"); OnPropertyChanged("TextControlVisible"); }
        }
        private int _FontSize = 8;
        /// <summary>
        /// 文字大小
        /// </summary>
        public int FontSize
        {
            get { return _FontSize; }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value < 1)
                {
                    _FontSize = 1;
                }
                else if (value > 26)
                {
                    _FontSize = 26;
                }
                else
                {
                    _FontSize = value;
                }
                OnPropertyChanged("FontSize"); OnPropertyChanged("TextInfo");
            }
        }
        private bool _IsBold = false;
        /// <summary>
        /// 是否加粗
        /// </summary>
        public string IsBold
        {
            get
            {
                if (_IsBold)
                {
                    return "Bold";
                }
                else
                {
                    return "Normal";
                }
            }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value == "Bold")
                {
                    _IsBold = true;
                }
                else
                {
                    _IsBold = false;
                }
                OnPropertyChanged("IsBold");
                OnPropertyChanged("TextInfo");
            }
        }
        private bool _IsItalic = false;
        /// <summary>
        /// 是否斜体
        /// </summary>
        public string IsItalic
        {
            get
            {
                if (_IsItalic)
                {
                    return "Italic";
                }
                else
                {
                    return "Normal";
                }
            }
            set
            {
                _TextInfo = Temptext(_TextInfo);
                if (value == "Italic")
                {
                    _IsItalic = true;
                }
                else
                {
                    _IsItalic = false;
                }
                OnPropertyChanged("IsItalic");
                OnPropertyChanged("TextInfo");
            }
        }
        /// <summary>
        /// 图片空间显示
        /// </summary>
        public string ImageControlVisible
        {
            get
            {
                if (_IsImage)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        /// <summary>
        /// 文本空间显示
        /// </summary>
        public string TextControlVisible
        {
            get
            {
                if (!_IsImage)
                {
                    return "Visible";
                }
                else
                {
                    return "Collapsed";
                }
            }
        }
        /// <summary>
        /// Xml格式
        /// </summary>
        public string XmlTxt
        {
            get { return ToXml(); }
        }
        public string ToXml()
        {
            string functionName = "ToXml";
            try
            {
                if (_IsImage)
                {
                    return string.Format("<Pic width=\"130\" height=\"{0}\">{1}</Pic>",
                        (_ImageHeight * (130 / _ImageWidth)).ToString().Split('.')[0],
                        _ImageName);
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    string[] tmps = _TextInfo.Split('\n');
                    foreach (string str in tmps)
                    {
                        sb.AppendFormat("<Content font=\"宋体\" size=\"{0}\" bold=\"{1}\" italic=\"{2}\">{3}</Content>", _FontSize.ToString(), (_IsBold) ? "Y" : "N", (_IsItalic) ? "Y" : "N", str);
                    }
                    return sb.ToString();
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
                return "";
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
                return "";
            }
        }
        private string Temptext(string value)
        {
            string temptxt = value.Replace("\n", "");
            string txt = "";
            string str = "";
            for (int i = 0; i < temptxt.Length; i++)
            {
                str += temptxt.Substring(i, 1);
                if (System.Text.Encoding.Default.GetByteCount(str) >= 200 / (_IsBold ? _FontSize + 1 : _FontSize))
                {
                    if (txt == "")
                    {
                        txt = str;
                    }
                    else
                    {
                        txt += "\n" + str;
                    }
                    str = "";
                }
                else if (i == temptxt.Length - 1)
                {
                    if (txt == "")
                    {
                        txt = str;
                    }
                    else
                    {
                        txt += "\n" + str;
                    }
                    str = "";
                }
            }
            return txt;
        }
    }
}
