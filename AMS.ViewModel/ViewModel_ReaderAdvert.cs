using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;

namespace AMS.ViewModel
{
    public class ViewModel_ReaderAdvert : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_ReaderAdvert";
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
            get { return _ReaderAdvertModel.CustomerID; }
            set { _ReaderAdvertModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.ReaderAdvertInfo _ReaderAdvertModel = new Model.ReaderAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.ReaderAdvertInfo ReaderAdvertModel
        {
            get { return _ReaderAdvertModel; }
            set { _ReaderAdvertModel = value; }
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
                    _ReaderAdvertModel.ReaderAdImagePath = _AdImageInfo.UriSource.LocalPath;
                }
                OnPropertyChanged("AdImageInfo");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _ReaderAdvertModel.Num; }
            set { _ReaderAdvertModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _ReaderAdvertModel.Name; }
            set { _ReaderAdvertModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_ReaderAdvertModel.EffectDate == null || _ReaderAdvertModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _ReaderAdvertModel.EffectDate = DateTime.Now.Date;
                }
                return _ReaderAdvertModel.EffectDate;
            }
            set { _ReaderAdvertModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_ReaderAdvertModel.EndDate == null || _ReaderAdvertModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _ReaderAdvertModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _ReaderAdvertModel.EndDate;
            }
            set { _ReaderAdvertModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_ReaderAdvertModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _ReaderAdvertModel.OperatorName;
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
                if (string.IsNullOrEmpty(ReaderAdvertModel.Num))
                {
                    ErrorMessage = "广告的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(ReaderAdvertModel.Name))
                {
                    ErrorMessage = "广告的名称不能为空！";
                    return false;
                }
                if (_AdImageInfo == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (ReaderAdvertModel.EffectDate == null)
                {
                    ErrorMessage = "广告的开始时间不能为空！";
                    return false;
                }
                if (ReaderAdvertModel.EndDate == null)
                {
                    ErrorMessage = "广告的结束时间不能为空！";
                    return false;
                }
                if (ReaderAdvertModel.EndDate < ReaderAdvertModel.EffectDate)
                {
                    ErrorMessage = "广告的结束时间要大于开始时间！";
                    return false;
                }
                if (ReaderAdvertModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }
                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(ReaderAdvertModel.Num, ReaderAdvertModel.Name, Model.Enum.AdType.ReaderAd))
                {
                    ErrorMessage = "已存在存在相同名称或编号的弹窗广告！";
                    return false;
                }
                ReaderAdvertModel.OperatorID = User.ID;
                ReaderAdvertModel.ImageFilePath.Clear();
                ReaderAdvertModel.ImageFilePath.Add(ReaderAdvertModel.ReaderAdImagePath);

                string resultstr = "";
                //TODO:保存&文件上传
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传媒体文件";
                Vm_ProgressBar.FullProgress = ReaderAdvertModel.ImageFilePath.Count;
                foreach (string upFileItem in ReaderAdvertModel.ImageFilePath)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + upFileItem + "\"……";
                    resultstr = fileUpload.UpdateFile(upFileItem, ReaderAdvertModel.Num + "_" + upFileItem.Substring(upFileItem.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.ReaderAd);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = string.Format("文件{0}上传失败！{1}", upFileItem, resultstr);
                        Vm_ProgressBar.IsFinish();
                        return false;
                    }
                }
                //更换文件名
                ReaderAdvertModel.ReaderAdImagePath = ReaderAdvertModel.Num + "_" + ReaderAdvertModel.ReaderAdImagePath.Substring(ReaderAdvertModel.ReaderAdImagePath.LastIndexOf("\\") + 1);
                for (int i = 0; i < ReaderAdvertModel.ImageFilePath.Count; i++)
                {
                    ReaderAdvertModel.ImageFilePath[i] = ReaderAdvertModel.Num + "_" + ReaderAdvertModel.ImageFilePath[i].Substring(ReaderAdvertModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                ReaderAdvertModel.Type = Model.Enum.AdType.ReaderAd;
                AMS.Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.Type = Model.Enum.AdType.ReaderAd;
                model.ID = ReaderAdvertModel.ID;
                model.Name = ReaderAdvertModel.Name;
                model.Num = ReaderAdvertModel.Num;
                model.OperatorID = ReaderAdvertModel.OperatorID;
                model.CustomerID = ReaderAdvertModel.CustomerID;
                model.AdContent = ReaderAdvertModel.ToXml();
                model.EffectDate = ReaderAdvertModel.EffectDate;
                model.EndDate = ReaderAdvertModel.EndDate;
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
            AdImageInfo = ImageSaveLocation(_ReaderAdvertModel.ReaderAdImagePath);
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
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.ReaderAd) != "")
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
