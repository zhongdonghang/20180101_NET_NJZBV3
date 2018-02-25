using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace AMS.ViewModel
{
    public class ViewModel_PopAdvert : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModel_PopAdvert";
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
            get { return _PopAdvertModel.CustomerID; }
            set { _PopAdvertModel.CustomerID = value; OnPropertyChanged("CustomerID"); }
        }
        private Model.PopAdvertInfo _PopAdvertModel = new Model.PopAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public Model.PopAdvertInfo PopAdvertModel
        {
            get { return _PopAdvertModel; }
            set { _PopAdvertModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _PopImageInfo;
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
                    _PopAdvertModel.PopImagePath = _PopImageInfo.UriSource.LocalPath;
                }
                OnPropertyChanged("PopImageInfo");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PopAdvertModel.Num; }
            set { _PopAdvertModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PopAdvertModel.Name; }
            set { _PopAdvertModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PopAdvertModel.EffectDate == null || _PopAdvertModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PopAdvertModel.EffectDate = DateTime.Now.Date;
                }
                return _PopAdvertModel.EffectDate;
            }
            set { _PopAdvertModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PopAdvertModel.EndDate == null || _PopAdvertModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PopAdvertModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PopAdvertModel.EndDate;
            }
            set { _PopAdvertModel.EndDate = value; OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_PopAdvertModel.OperatorName) && User != null)
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _PopAdvertModel.OperatorName;
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
                if (string.IsNullOrEmpty(PopAdvertModel.Num))
                {
                    ErrorMessage = "弹窗广告的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(PopAdvertModel.Name))
                {
                    ErrorMessage = "弹窗广告的名称不能为空！";
                    return false;
                }
                if (_PopImageInfo == null)
                {
                    ErrorMessage = "弹窗图片不能为空！";
                    return false;
                }
                if (PopAdvertModel.EffectDate == null)
                {
                    ErrorMessage = "弹窗广告的开始时间不能为空！";
                    return false;
                }
                if (PopAdvertModel.EndDate == null)
                {
                    ErrorMessage = "弹窗广告的结束时间不能为空！";
                    return false;
                }
                if (PopAdvertModel.EndDate < PopAdvertModel.EffectDate)
                {
                    ErrorMessage = "弹窗广告的结束时间要大于开始时间！";
                    return false;
                }
                if (PopAdvertModel.CustomerID < 0)
                {
                    ErrorMessage = "请选择优惠券客户！";
                    return false;
                }
                if (!IsEdit && AMS.ServiceProxy.AdvertisementOperationService.ExistSameAdvert(PopAdvertModel.Num, PopAdvertModel.Name, Model.Enum.AdType.PopAd))
                {
                    ErrorMessage = "已存在存在相同名称或编号的弹窗广告！";
                    return false;
                }
                PopAdvertModel.OperatorID = User.ID;
                PopAdvertModel.ImageFilePath.Clear();
                PopAdvertModel.ImageFilePath.Add(PopAdvertModel.PopImagePath);

                string resultstr = "";
                //TODO:保存&文件上传
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传媒体文件";
                Vm_ProgressBar.FullProgress = PopAdvertModel.ImageFilePath.Count;
                foreach (string upFileItem in PopAdvertModel.ImageFilePath)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + upFileItem + "\"……";
                    resultstr = fileUpload.UpdateFile(upFileItem, PopAdvertModel.Num + "_" + upFileItem.Substring(upFileItem.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PopAd);
                    if (!string.IsNullOrEmpty(resultstr))
                    {
                        ErrorMessage = string.Format("文件{0}上传失败！{1}", upFileItem, resultstr);
                        Vm_ProgressBar.IsFinish();
                        return false;
                    }
                }
                //更换文件名
                PopAdvertModel.PopImagePath = PopAdvertModel.Num + "_" + PopAdvertModel.PopImagePath.Substring(PopAdvertModel.PopImagePath.LastIndexOf("\\") + 1);
                for (int i = 0; i < PopAdvertModel.ImageFilePath.Count; i++)
                {
                    PopAdvertModel.ImageFilePath[i] = PopAdvertModel.Num + "_" + PopAdvertModel.ImageFilePath[i].Substring(PopAdvertModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                PopAdvertModel.Type = Model.Enum.AdType.PopAd;
                AMS.Model.AMS_Advertisement model = new Model.AMS_Advertisement();
                model.Type = Model.Enum.AdType.PopAd;
                model.ID = PopAdvertModel.ID;
                model.Name = PopAdvertModel.Name;
                model.Num = PopAdvertModel.Num;
                model.OperatorID = PopAdvertModel.OperatorID;
                model.CustomerID = PopAdvertModel.CustomerID;
                model.AdContent = PopAdvertModel.ToXml();
                model.EffectDate = PopAdvertModel.EffectDate;
                model.EndDate = PopAdvertModel.EndDate;
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
                    ErrorMessage = string.Format("保存播放列表失败！{0}", resultstr);
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
            PopImageInfo = ImageSaveLocation(_PopAdvertModel.PopImagePath);
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
                if (fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PopAd) != "")
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
