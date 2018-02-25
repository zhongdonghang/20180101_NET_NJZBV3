using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;

namespace AMS.ViewModel
{
    public class ViewModel_PromotionEdit : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_PromotionEdit";
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
            get { return _PromotionModel.CustomerID; }
            set { _PromotionModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.PromotionAdvertInfo _PromotionModel = new Model.PromotionAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.PromotionAdvertInfo PromotionModel
        {
            get { return _PromotionModel; }
            set { _PromotionModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _AdImageInfo;
        /// <summary>
        /// 滚动logo图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage AdImageInfo
        {
            get { return _AdImageInfo; }
            set
            {
                _AdImageInfo = value;
                if (_AdImageInfo != null)
                {
                    _PromotionModel.AdImagePath = _AdImageInfo.UriSource.LocalPath;
                }
                OnPropertyChanged("AdImageInfo");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PromotionModel.Num; }
            set { _PromotionModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PromotionModel.Name; }
            set { _PromotionModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PromotionModel.EffectDate == null || _PromotionModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PromotionModel.EffectDate = DateTime.Now.Date;
                }
                return _PromotionModel.EffectDate;
            }
            set { _PromotionModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PromotionModel.EndDate == null || _PromotionModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PromotionModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PromotionModel.EndDate;
            }
            set { _PromotionModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_PromotionModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _PromotionModel.OperatorName;
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

        public bool Save()
        {
            string functionName = "save";
            try
            {
                if (string.IsNullOrEmpty(PromotionModel.Num))
                {
                    ErrorMessage = "广告的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(PromotionModel.Name))
                {
                    ErrorMessage = "广告的名称不能为空！";
                    return false;
                }
                if (_AdImageInfo == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (PromotionModel.EffectDate == null)
                {
                    ErrorMessage = "广告的开始时间不能为空！";
                    return false;
                }
                if (PromotionModel.EndDate == null)
                {
                    ErrorMessage = "广告的结束时间不能为空！";
                    return false;
                }
                if (PromotionModel.EndDate < PromotionModel.EffectDate)
                {
                    ErrorMessage = "广告的结束时间要大于开始时间！";
                    return false;
                }
                if (PromotionModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }
                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(PromotionModel.Num, PromotionModel.Name, Model.Enum.AdType.PopAd))
                {
                    ErrorMessage = "已存在存在相同名称或编号的弹窗广告！";
                    return false;
                }
                PromotionModel.OperatorID = User.ID;
                PromotionModel.ImageFilePath.Clear();
                PromotionModel.ImageFilePath.Add(PromotionModel.AdImagePath);

                string resultstr = "";
                //TODO:保存&文件上传
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传媒体文件";
                Vm_ProgressBar.FullProgress = PromotionModel.ImageFilePath.Count;
                foreach (string upFileItem in PromotionModel.ImageFilePath)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + upFileItem + "\"……";
                    resultstr = fileUpload.UpdateFile(upFileItem, PromotionModel.Num + "_" + upFileItem.Substring(upFileItem.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PromotionAd);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = string.Format("文件{0}上传失败！{1}", upFileItem, resultstr);
                        Vm_ProgressBar.IsFinish();
                        return false;
                    }
                }
                //更换文件名
                PromotionModel.AdImagePath = PromotionModel.Num + "_" + PromotionModel.AdImagePath.Substring(PromotionModel.AdImagePath.LastIndexOf("\\") + 1);
                for (int i = 0; i < PromotionModel.ImageFilePath.Count; i++)
                {
                    PromotionModel.ImageFilePath[i] = PromotionModel.Num + "_" + PromotionModel.ImageFilePath[i].Substring(PromotionModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                PromotionModel.Type = Model.Enum.AdType.PromotionAd;
                AMS.Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.Type = Model.Enum.AdType.PromotionAd;
                model.ID = PromotionModel.ID;
                model.Name = PromotionModel.Name;
                model.Num = PromotionModel.Num;
                model.OperatorID = PromotionModel.OperatorID;
                model.CustomerID = PromotionModel.CustomerID;
                model.AdContent = PromotionModel.ToXml();
                model.EffectDate = PromotionModel.EffectDate;
                model.EndDate = PromotionModel.EndDate;
                if (IsEdit)
                {
                    //TODO:更新                  
                    return AMS.ServiceProxy.AdvertisementOperationService.UpdateAdvertisement(model);
                }
                else
                {
                    //DOTO:添加
                    return AMS.ServiceProxy.AdvertisementOperationService.AddAdvertisement(model);
                }
                if (!string.IsNullOrEmpty(resultstr))
                {
                    ErrorMessage = string.Format("保存失败！{0}", resultstr);
                    return false;
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
        void fileUpload_HandleProgress(int message)
        {
            Vm_ProgressBar.NowProgress = message.ToString();
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        public void GetData()
        {
            AdImageInfo = ImageSaveLocation(_PromotionModel.AdImagePath);
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
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PromotionAd) != "")
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
