using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_Prommotion : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.PromotionAdvertInfo _PrommotionModel = new SeatManage.ClassModel.PromotionAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.PromotionAdvertInfo PrommotionModel
        {
            get { return _PrommotionModel; }
            set { _PrommotionModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _PrommotionImage;
        /// <summary>
        /// 图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PrommotionImage
        {
            get { return _PrommotionImage; }
            set
            {
                _PrommotionImage = value;
                if (_PrommotionImage != null)
                {
                    _PrommotionModel.AdImagePath = _PrommotionImage.UriSource.LocalPath;
                }
                OnPropertyChanged("PrommotionImage");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PrommotionModel.Num; }
            set { _PrommotionModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PrommotionModel.Name; }
            set { _PrommotionModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PrommotionModel.EffectDate == null || _PrommotionModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PrommotionModel.EffectDate = DateTime.Now.Date;
                }
                return _PrommotionModel.EffectDate;
            }
            set { _PrommotionModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PrommotionModel.EndDate == null || _PrommotionModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PrommotionModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PrommotionModel.EndDate;
            }
            set { _PrommotionModel.EndDate = value; OnPropertyChanged("EndDate"); }
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
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(_PrommotionModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_PrommotionModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }
                if (_PrommotionImage == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (_PrommotionModel.EffectDate == null)
                {
                    ErrorMessage = "开始时间不能为空！";
                    return false;
                }
                if (_PrommotionModel.EndDate == null)
                {
                    ErrorMessage = "结束时间不能为空！";
                    return false;
                }
                if (_PrommotionModel.EndDate < _PrommotionModel.EffectDate)
                {
                    ErrorMessage = "结束时间要大于开始时间！";
                    return false;
                }

                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(_PrommotionModel.Num, SeatManage.EnumType.AdType.PromotionAd) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的推广！";
                    return false;
                }

                string resultstr = "";                //TODO:保存&文件上传
                SeatManage.Bll.FileOperate fileOperation = new SeatManage.Bll.FileOperate();

                if (!fileOperation.UpdateFile(_PrommotionModel.AdImagePath, _PrommotionModel.Num + "_" + _PrommotionModel.AdImagePath.Substring(_PrommotionModel.AdImagePath.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PromotionAd))
                {
                    ErrorMessage = string.Format("文件{0}上传失败!", _PrommotionModel.AdImagePath);
                    return false;
                }
                //更换文件名
                _PrommotionModel.AdImagePath = _PrommotionModel.Num + "_" + _PrommotionModel.AdImagePath.Substring(_PrommotionModel.AdImagePath.LastIndexOf("\\") + 1);
                _PrommotionModel.ImageFilePath.Clear();
                _PrommotionModel.ImageFilePath.Add(_PrommotionModel.AdImagePath);
                _PrommotionModel.Type = SeatManage.EnumType.AdType.SchoolNotice;

                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.PromotionAd;
                model.ID = _PrommotionModel.ID;
                model.Name = _PrommotionModel.Name;
                model.Num = _PrommotionModel.Num;
                model.AdContent = _PrommotionModel.ToXml();
                model.EffectDate = _PrommotionModel.EffectDate;
                model.EndDate = _PrommotionModel.EndDate;
                if (IsEdit)
                {
                    //TODO:更新                  
                    resultstr = SeatManage.Bll.AdvertisementOperation.UpdateAdModel(model);
                }
                else
                {
                    //DOTO:添加
                    resultstr = SeatManage.Bll.AdvertisementOperation.AddAdModel(model);
                }
                if (!string.IsNullOrEmpty(resultstr))
                {
                    ErrorMessage = string.Format("保存失败！{0}", resultstr);
                    return false;
                }
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "保存成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        public void GetData()
        {
            PrommotionImage = ImageSaveLocation(_PrommotionModel.AdImagePath);
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
                SeatManage.Bll.FileOperate fileDownload = new SeatManage.Bll.FileOperate();
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PromotionAd))
                {
                    return
                        null;
                }
            }
            BitmapImage img = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
            return img;
        }



        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
