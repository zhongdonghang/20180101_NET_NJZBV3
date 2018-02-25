using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_PopImage : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.PopAdvertInfo _PopModel = new SeatManage.ClassModel.PopAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.PopAdvertInfo PopModel
        {
            get { return _PopModel; }
            set { _PopModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _PopImage;
        /// <summary>
        /// 图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PopImage
        {
            get { return _PopImage; }
            set
            {
                _PopImage = value;
                if (_PopImage != null)
                {
                    _PopModel.PopImagePath = _PopImage.UriSource.LocalPath;
                }
                OnPropertyChanged("PopImage");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PopModel.Num; }
            set { _PopModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PopModel.Name; }
            set { _PopModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PopModel.EffectDate == null || _PopModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PopModel.EffectDate = DateTime.Now.Date;
                }
                return _PopModel.EffectDate;
            }
            set { _PopModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PopModel.EndDate == null || _PopModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PopModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PopModel.EndDate;
            }
            set { _PopModel.EndDate = value; OnPropertyChanged("EndDate"); }
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
                if (string.IsNullOrEmpty(_PopModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_PopModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }
                if (_PopImage == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (_PopModel.EffectDate == null)
                {
                    ErrorMessage = "开始时间不能为空！";
                    return false;
                }
                if (_PopModel.EndDate == null)
                {
                    ErrorMessage = "结束时间不能为空！";
                    return false;
                }
                if (_PopModel.EndDate < _PopModel.EffectDate)
                {
                    ErrorMessage = "结束时间要大于开始时间！";
                    return false;
                }

                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(_PopModel.Num, SeatManage.EnumType.AdType.PopAd) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的弹窗图片！";
                    return false;
                }

                string resultstr = "";                //TODO:保存&文件上传
                SeatManage.Bll.FileOperate fileOperation = new SeatManage.Bll.FileOperate();

                if (!fileOperation.UpdateFile(_PopModel.PopImagePath, _PopModel.Num + "_" + _PopModel.PopImagePath.Substring(_PopModel.PopImagePath.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PopAd))
                {
                    ErrorMessage = string.Format("文件{0}上传失败!", _PopModel.PopImagePath);
                    return false;
                }
                //更换文件名
                _PopModel.PopImagePath = _PopModel.Num + "_" + _PopModel.PopImagePath.Substring(_PopModel.PopImagePath.LastIndexOf("\\") + 1);
                _PopModel.ImageFilePath.Clear();
                _PopModel.ImageFilePath.Add(_PopModel.PopImagePath);
                _PopModel.Type = SeatManage.EnumType.AdType.SchoolNotice;

                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.PopAd;
                model.ID = _PopModel.ID;
                model.Name = _PopModel.Name;
                model.Num = _PopModel.Num;
                model.AdContent = _PopModel.ToXml();
                model.EffectDate = _PopModel.EffectDate;
                model.EndDate = _PopModel.EndDate;
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
            PopImage = ImageSaveLocation(_PopModel.PopImagePath);
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
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.PopAd))
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
