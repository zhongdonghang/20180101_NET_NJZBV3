using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace AMS.ViewModel
{
    public class ViewModel_Coupons : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelCouponsEditWindow";
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
            get { return _CouponsModel.CustomerID; }
            set { _CouponsModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.CouponsInfo _CouponsModel = new Model.CouponsInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.CouponsInfo CouponsModel
        {
            get { return _CouponsModel; }
            set { _CouponsModel = value; }
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
                if (_LogoImageInfo != null)
                {
                    _CouponsModel.LogoImage = _LogoImageInfo.UriSource.LocalPath;
                }
                OnPropertyChanged("LogoImageInfo");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string CouponsNo
        {
            get { return _CouponsModel.Num; }
            set
            {
                _CouponsModel.Num = value;
                foreach (ViewModelCouponsItem item in CouponsItemList)
                {
                    item.TemplateItem.TemplateNo = value;
                }
                OnPropertyChanged("CouponsNo");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string CouponsName
        {
            get { return _CouponsModel.Name; }
            set { _CouponsModel.Name = value; OnPropertyChanged("CouponsName"); }
        }
        /// <summary>
        /// 广告位
        /// </summary>
        public int Station
        {
            get { return _CouponsModel.Station; }
            set { _CouponsModel.Station = value; OnPropertyChanged("Station"); }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_CouponsModel.EffectDate == null || _CouponsModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _CouponsModel.EffectDate = DateTime.Now.Date;
                }
                return _CouponsModel.EffectDate;
            }
            set { _CouponsModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_CouponsModel.EndDate == null || _CouponsModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _CouponsModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _CouponsModel.EndDate;
            }
            set { _CouponsModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_CouponsModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _CouponsModel.OperatorName;
                }
            }
        }

        private bool _IsEdit = false;
        /// <summary>
        /// 是否是编辑模式
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
        }

        private ObservableCollection<ViewModelCouponsItem> _CouponsItemList = new ObservableCollection<ViewModelCouponsItem>();
        /// <summary>
        /// 广告子项列表
        /// </summary>
        public ObservableCollection<ViewModelCouponsItem> CouponsItemList
        {
            get { return _CouponsItemList; }
            set { _CouponsItemList = value; OnPropertyChanged("ConponsItemList"); }
        }

        private ViewModelCouponsItem _NowEdiretItem = new ViewModelCouponsItem();
        /// <summary>
        /// 当期编辑的子项
        /// </summary>
        public ViewModelCouponsItem NowEdiretItem
        {
            get { return _NowEdiretItem; }
            set { _NowEdiretItem = value; OnPropertyChanged("NowEdiretItem"); }
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
        /// <summary>
        /// 添加新的优惠信息
        /// </summary>
        public void AddNewItem(BitmapImage image)
        {
            CouponsItemList.Add(new ViewModelCouponsItem());
            CouponsItemList[CouponsItemList.Count - 1].PopImageInfo = image;
            NowEdiretItem = CouponsItemList[CouponsItemList.Count - 1];
            NowEdiretItem.TemplateItem.TemplateNo = CouponsNo;
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="itemNum"></param>
        public void MoveItemLeft(int itemNum)
        {
            if (itemNum > 0)
            {
                CouponsItemList.Move(itemNum, itemNum - 1);
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="itemNum"></param>
        public void MoveItemRight(int itemNum)
        {
            if (itemNum < CouponsItemList.Count - 1)
            {
                CouponsItemList.Move(itemNum, itemNum + 1);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="itemNum"></param>
        public void DeleteItem(int itemNum)
        {
            CouponsItemList.RemoveAt(itemNum);
        }
        void fileUpload_HandleProgress(int message)
        {
            Vm_ProgressBar.NowProgress = message.ToString();
        }
        /// <summary>
        /// 保存
        /// </summary>
        public bool Save()
        {
            string functionName = "Save";
            try
            {

                if (string.IsNullOrEmpty(CouponsModel.Num))
                {
                    ErrorMessage = "优惠券编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(CouponsModel.Name))
                {
                    ErrorMessage = "优惠券名称不能为空！";
                    return false;
                }
                if (LogoImageInfo == null)
                {
                    ErrorMessage = "滚动LOGO图片不能为空！";
                    return false;
                }
                if (CouponsItemList.Count < 1 && Station != 8)
                {
                    ErrorMessage = "弹窗图片不能为空！";
                    return false;
                }
                foreach (ViewModelCouponsItem item in CouponsItemList)
                {
                    if (item.EffectDate == null)
                    {
                        ErrorMessage = "请选择开始日期！";
                        return false;
                    }
                    if (item.EndDate == null)
                    {
                        ErrorMessage = "请选择结束日期！";
                        return false;
                    }
                    if (item.EndDate < item.EffectDate)
                    {
                        ErrorMessage = "结束日期不能小于开始日期！";
                        return false;
                    }
                }
                if (CouponsModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }
                if (CouponsModel.Station < 1)
                {
                    ErrorMessage = "请选择优惠券位置！";
                    return false;
                }
                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(CouponsModel.Num, CouponsModel.Name, Model.Enum.AdType.SlipCustomerAd))
                {
                    ErrorMessage = "存在相同的编号或名称！";
                    return false;
                }
                CouponsModel.OperatorID = User.ID;
                //添加上传图片
                CouponsModel.ImageFilePath.Clear();
                CouponsModel.ImageFilePath.Add(_CouponsModel.LogoImage);
                foreach (ViewModelCouponsItem item in CouponsItemList)
                {
                    if (item.PopImageInfo != null)
                    {
                        bool isnew = true;
                        foreach (string samefile in CouponsModel.ImageFilePath)
                        {
                            if (samefile == item.PopImagePath)
                            {
                                isnew = false;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            CouponsModel.ImageFilePath.Add(item.PopImagePath);
                        }
                    }
                    else
                    {
                        ErrorMessage = "优惠信息图片不能为空！";
                        return false;
                    }
                    foreach (AMS.ViewModel.ViewModelPrintItem pitem in item.TemplateItem.PrintIiemList)
                    {
                        if (pitem.IsImage)
                        {
                            bool isnew = true;
                            foreach (string samefile in CouponsModel.ImageFilePath)
                            {
                                if (samefile == pitem.ImagePath)
                                {
                                    isnew = false;
                                    break;
                                }
                            }
                            if (isnew)
                            {
                                CouponsModel.ImageFilePath.Add(pitem.ImagePath);
                            }
                        }
                    }
                }
                //TODO:图片上传，优惠券保存
                string result = "";
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传优惠券图片";
                Vm_ProgressBar.FullProgress = CouponsModel.ImageFilePath.Count;
                for (int i = 0; i < CouponsModel.ImageFilePath.Count; i++)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + CouponsModel.ImageFilePath[i].Substring(CouponsModel.ImageFilePath[i].LastIndexOf("\\") + 1) + "\"……";
                    result = fileUpload.UpdateFile(CouponsModel.ImageFilePath[i], CouponsModel.Num + "_" + CouponsModel.ImageFilePath[i].Substring(CouponsModel.ImageFilePath[i].LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SlipCustomerAd);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("上传图片失败！{0}", result);
                        return false;
                    }
                    CouponsModel.ImageFilePath[i] = CouponsModel.Num + "_" + CouponsModel.ImageFilePath[i].Substring(CouponsModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                //修改文件名
                CouponsModel.LogoImage = CouponsModel.Num + "_" + CouponsModel.LogoImage.Substring(CouponsModel.LogoImage.LastIndexOf("\\") + 1);
                foreach (ViewModelCouponsItem item in CouponsItemList)
                {
                    if (item.PopImageInfo != null)
                    {
                        item.PopImagePath = CouponsModel.Num + "_" + item.PopImagePath.Substring(item.PopImagePath.LastIndexOf("\\") + 1);
                        item.Getmodel();
                        if (item.CouponsItem.ID == "")
                        {
                            item.CouponsItem.ID = Guid.NewGuid().ToString() + item.CouponsItem.PpoImagePath;
                        }
                        if (CouponsModel.EffectDate < DateTime.Parse("2000-1-1") || CouponsModel.EffectDate > item.EffectDate)
                        {
                            CouponsModel.EffectDate = item.EffectDate;
                        }
                        if (CouponsModel.EndDate < DateTime.Parse("2000-1-1") || CouponsModel.EndDate < item.EndDate)
                        {
                            CouponsModel.EndDate = item.EndDate;
                        }
                        CouponsModel.PopItemList.Add(item.CouponsItem);
                    }
                }
                CouponsModel.Type = Model.Enum.AdType.SlipCustomerAd;
                CouponsModel.AdContent = AMS.Model.CouponsInfo.ToXml(CouponsModel);
                Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.AdContent = CouponsModel.AdContent;
                model.CustomerID = CouponsModel.CustomerID;
                model.EffectDate = CouponsModel.EffectDate;
                model.EndDate = CouponsModel.EndDate;
                model.ID = CouponsModel.ID;
                model.ImageFilePath = CouponsModel.ImageFilePath;
                model.Name = CouponsModel.Name;
                model.Num = CouponsModel.Num;
                model.OperatorID = CouponsModel.OperatorID;
                model.Type = Model.Enum.AdType.SlipCustomerAd;

                if (IsEdit)
                {
                    return AMS.ServiceProxy.AdvertisementOperationService.UpdateAdvertisement(model);
                }
                else
                {
                    return AMS.ServiceProxy.AdvertisementOperationService.AddAdvertisement(model);
                }
                return true;

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
        /// <summary>
        /// 显示
        /// </summary>
        public void GetUpdateInfo()
        {
            LogoImageInfo = ImageSaveLocation(_CouponsModel.LogoImage);
            foreach (AMS.Model.CouponsInfoItem item in _CouponsModel.PopItemList)
            {
                ViewModelCouponsItem viewModel = new ViewModelCouponsItem();
                viewModel.CouponsItem = item;
                viewModel.PopImageInfo = ImageSaveLocation(item.PpoImagePath);
                viewModel.ToViewModel();
                CouponsItemList.Add(viewModel);
            }
        }
        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private BitmapImage ImageSaveLocation(string imageName)
        {

            string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imageName.Split('_')[1]);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                AMS.ServiceProxy.FileOperate fileDownload = new AMS.ServiceProxy.FileOperate();
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomerAd) != "")
                {
                    return
                        null;
                }
            }
            BitmapImage img = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            return img;
        }
    }

    /// <summary>
    /// 优惠券子项
    /// </summary>
    public class ViewModelCouponsItem : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelCouponsItem";
        private AMS.Model.CouponsInfoItem _CouponsItem = new AMS.Model.CouponsInfoItem();
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.CouponsInfoItem CouponsItem
        {
            get { return _CouponsItem; }
            set { _CouponsItem = value; }
        }
        /// <summary>
        /// 弹窗广告图片
        /// </summary>
        public string PopImagePath
        {
            get { return _CouponsItem.PpoImagePath; }
            set { _CouponsItem.PpoImagePath = value; OnPropertyChanged("PopImagePath"); }
        }
        private System.Windows.Media.Imaging.BitmapImage _PopImageInfo = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 滚动logo图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PopImageInfo
        {
            get { return _PopImageInfo; }
            set
            {
                _PopImageInfo = value;
                if (_PopImageInfo != null)
                {
                    PopImagePath = _PopImageInfo.UriSource.LocalPath;
                }
                OnPropertyChanged("PopImageInfo");
            }
        }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_CouponsItem.EffectDate == null || _CouponsItem.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _CouponsItem.EffectDate = DateTime.Now.Date;
                }
                return _CouponsItem.EffectDate;
            }
            set { _CouponsItem.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_CouponsItem.EndDate == null || _CouponsItem.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _CouponsItem.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _CouponsItem.EndDate;
            }
            set { _CouponsItem.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 是否打印
        /// </summary>
        public bool IsPrint
        {
            get { return _CouponsItem.IsPrint; }
            set
            {
                _CouponsItem.IsPrint = value;
                if (_CouponsItem.IsPrint)
                {
                    _Visibility = "Visible";
                }
                else
                {
                    _Visibility = "Collapsed";
                }
                OnPropertyChanged("IsPrint");
                OnPropertyChanged("Visibility");
            }
        }
        private string _Visibility = "Collapsed";
        /// <summary>
        /// 显示编辑
        /// </summary>
        public string Visibility
        {
            get { return _Visibility; }
            set { _Visibility = value; }
        }


        private ViewModelPrintTemplateInfo _TemplateItem = new ViewModelPrintTemplateInfo();
        /// <summary>
        /// 打印子项
        /// </summary>
        public ViewModelPrintTemplateInfo TemplateItem
        {
            get { return _TemplateItem; }
            set { _TemplateItem = value; OnPropertyChanged("TemplateItem"); }
        }
        /// <summary>
        /// 获取
        /// </summary>
        public void Getmodel()
        {
            _CouponsItem.TemplateItem.Clear();
            foreach (ViewModelPrintItem item in _TemplateItem.PrintIiemList)
            {
                AMS.Model.PrintItem model = new Model.PrintItem();
                model.IsImage = item.IsImage;
                if (model.IsImage)
                {
                    model.ImageHeight = item.ImageHeight * (130 / item.ImageWidth);
                    model.ImageWidth = 130;
                    model.ImagePath = item.ImageName;
                }
                else
                {
                    model.FontSize = item.FontSize;
                    model.IsBold = item.IsBold == "Bold" ? true : false;
                    model.IsItalic = item.IsItalic == "Italic" ? true : false;
                    model.IsImage = item.IsImage;
                    model.TextInfo = item.TextInfo;
                }
                _CouponsItem.TemplateItem.Add(model);

            }
        }
        public void ToViewModel()
        {
            TemplateItem.PrintIiemList.Clear();
            foreach (Model.PrintItem item in _CouponsItem.TemplateItem)
            {
                ViewModelPrintItem viewmodel = new ViewModelPrintItem();
                viewmodel.IsImage = item.IsImage;
                if (viewmodel.IsImage)
                {
                    viewmodel.ImageInfo = ImageSaveLocation(item.ImagePath);
                }
                else
                {
                    viewmodel.FontSize = item.FontSize;
                    viewmodel.IsBold = item.IsBold ? "Bold" : "Normal";
                    viewmodel.IsItalic = item.IsItalic ? "Italic" : "Normal";
                    viewmodel.IsImage = item.IsImage;
                    viewmodel.TextInfo = item.TextInfo;
                }
                TemplateItem.PrintIiemList.Add(viewmodel);

            }
        }
        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private BitmapImage ImageSaveLocation(string imageName)
        {

            string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imageName.Split('_')[1]);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                AMS.ServiceProxy.FileOperate fileDownload = new AMS.ServiceProxy.FileOperate();
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.SlipCustomerAd) != "")
                {
                    return
                        null;
                }
            }
            BitmapImage img = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            return img;
        }
    }


}
