using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Media.Imaging;

namespace AMS.ViewModel
{
    public class ViewModel_PrintReceipt : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_PrintReceipt";
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
            get { return _PrintReceiptModel.CustomerID; }
            set { _PrintReceiptModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.PrintReceiptInfo _PrintReceiptModel = new Model.PrintReceiptInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.PrintReceiptInfo PrintReceiptModel
        {
            get { return _PrintReceiptModel; }
            set { _PrintReceiptModel = value; }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PrintReceiptModel.Num; }
            set
            {
                _PrintReceiptModel.Num = value;
                TemplateItem.TemplateNo = value;
                OnPropertyChanged("Num");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PrintReceiptModel.Name; }
            set { _PrintReceiptModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PrintReceiptModel.EffectDate == null || _PrintReceiptModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PrintReceiptModel.EffectDate = DateTime.Now.Date;
                }
                return _PrintReceiptModel.EffectDate;
            }
            set { _PrintReceiptModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PrintReceiptModel.EndDate == null || _PrintReceiptModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PrintReceiptModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PrintReceiptModel.EndDate;
            }
            set { _PrintReceiptModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_PrintReceiptModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _PrintReceiptModel.OperatorName;
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

        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
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
            PrintReceiptModel.TemplateItem.Clear();
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
                PrintReceiptModel.TemplateItem.Add(model);

            }
        }
        public void ToViewModel()
        {
            TemplateItem.PrintIiemList.Clear();
            foreach (Model.PrintItem item in PrintReceiptModel.TemplateItem)
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
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PrintReceiptAd) != "")
                {
                    return
                        null;
                }
            }
            BitmapImage img = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            return img;
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

                if (string.IsNullOrEmpty(PrintReceiptModel.Num))
                {
                    ErrorMessage = "优惠券编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(PrintReceiptModel.Name))
                {
                    ErrorMessage = "优惠券名称不能为空！";
                    return false;
                }

                if (TemplateItem.PrintIiemList.Count < 1)
                {
                    ErrorMessage = "打印项目不能为空！";
                    return false;
                }

                if (PrintReceiptModel.EffectDate == null)
                {
                    ErrorMessage = "请选择开始日期！";
                    return false;
                }
                if (PrintReceiptModel.EndDate == null)
                {
                    ErrorMessage = "请选择结束日期！";
                    return false;
                }
                if (PrintReceiptModel.EndDate < PrintReceiptModel.EffectDate)
                {
                    ErrorMessage = "结束日期不能小于开始日期！";
                    return false;
                }

                if (PrintReceiptModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }

                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(PrintReceiptModel.Num, PrintReceiptModel.Name, Model.Enum.AdType.PrintReceiptAd))
                {
                    ErrorMessage = "存在相同的编号或名称！";
                    return false;
                }
                PrintReceiptModel.OperatorID = User.ID;
                //添加上传图片
                PrintReceiptModel.ImageFilePath.Clear();

                foreach (AMS.ViewModel.ViewModelPrintItem pitem in TemplateItem.PrintIiemList)
                {
                    if (pitem.IsImage)
                    {
                        bool isnew = true;
                        foreach (string samefile in PrintReceiptModel.ImageFilePath)
                        {
                            if (samefile == pitem.ImagePath)
                            {
                                isnew = false;
                                break;
                            }
                        }
                        if (isnew)
                        {
                            PrintReceiptModel.ImageFilePath.Add(pitem.ImagePath);
                        }
                    }
                }

                //TODO:图片上传，优惠券保存
                string result = "";
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传打印图片";
                Vm_ProgressBar.FullProgress = PrintReceiptModel.ImageFilePath.Count;
                for (int i = 0; i < PrintReceiptModel.ImageFilePath.Count; i++)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + PrintReceiptModel.ImageFilePath[i].Substring(PrintReceiptModel.ImageFilePath[i].LastIndexOf("\\") + 1) + "\"……";
                    result = fileUpload.UpdateFile(PrintReceiptModel.ImageFilePath[i], PrintReceiptModel.Num + "_" + PrintReceiptModel.ImageFilePath[i].Substring(PrintReceiptModel.ImageFilePath[i].LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PrintReceiptAd);
                    if (!string.IsNullOrEmpty(result))
                    {
                        ErrorMessage = string.Format("上传图片失败！{0}", result);
                        return false;
                    }
                    PrintReceiptModel.ImageFilePath[i] = PrintReceiptModel.Num + "_" + PrintReceiptModel.ImageFilePath[i].Substring(PrintReceiptModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                Getmodel();
                PrintReceiptModel.Type = Model.Enum.AdType.PrintReceiptAd;
                PrintReceiptModel.AdContent = AMS.Model.PrintReceiptInfo.ToXml(PrintReceiptModel);
                Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.AdContent = PrintReceiptModel.AdContent;
                model.CustomerID = PrintReceiptModel.CustomerID;
                model.EffectDate = PrintReceiptModel.EffectDate;
                model.EndDate = PrintReceiptModel.EndDate;
                model.ID = PrintReceiptModel.ID;
                model.ImageFilePath = PrintReceiptModel.ImageFilePath;
                model.Name = PrintReceiptModel.Name;
                model.Num = PrintReceiptModel.Num;
                model.OperatorID = PrintReceiptModel.OperatorID;
                model.Type = Model.Enum.AdType.PrintReceiptAd;

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
    }
}
