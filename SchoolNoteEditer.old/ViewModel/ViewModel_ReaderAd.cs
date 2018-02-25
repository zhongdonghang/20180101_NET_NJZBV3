using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_ReaderAd : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.ReaderAdvertInfo _ReaderAdModel = new SeatManage.ClassModel.ReaderAdvertInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.ReaderAdvertInfo ReaderAdModel
        {
            get { return _ReaderAdModel; }
            set { _ReaderAdModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _ReaderImage;
        /// <summary>
        /// 图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage ReaderImage
        {
            get { return _ReaderImage; }
            set
            {
                _ReaderImage = value;
                if (_ReaderImage != null)
                {
                    _ReaderAdModel.ReaderAdImagePath = _ReaderImage.UriSource.LocalPath;
                }
                OnPropertyChanged("ReaderImage");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _ReaderAdModel.Num; }
            set { _ReaderAdModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _ReaderAdModel.Name; }
            set { _ReaderAdModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_ReaderAdModel.EffectDate == null || _ReaderAdModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _ReaderAdModel.EffectDate = DateTime.Now.Date;
                }
                return _ReaderAdModel.EffectDate;
            }
            set { _ReaderAdModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_ReaderAdModel.EndDate == null || _ReaderAdModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _ReaderAdModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _ReaderAdModel.EndDate;
            }
            set { _ReaderAdModel.EndDate = value; OnPropertyChanged("EndDate"); }
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
                if (string.IsNullOrEmpty(_ReaderAdModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_ReaderAdModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }
                if (_ReaderImage == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (_ReaderAdModel.EffectDate == null)
                {
                    ErrorMessage = "开始时间不能为空！";
                    return false;
                }
                if (_ReaderAdModel.EndDate == null)
                {
                    ErrorMessage = "结束时间不能为空！";
                    return false;
                }
                if (_ReaderAdModel.EndDate < _ReaderAdModel.EffectDate)
                {
                    ErrorMessage = "结束时间要大于开始时间！";
                    return false;
                }

                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(_ReaderAdModel.Num, SeatManage.EnumType.AdType.ReaderAd) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的读者推广！";
                    return false;
                }

                string resultstr = "";                //TODO:保存&文件上传
                SeatManage.Bll.FileOperate fileOperation = new SeatManage.Bll.FileOperate();

                if (!fileOperation.UpdateFile(_ReaderAdModel.ReaderAdImagePath, _ReaderAdModel.Num + "_" + _ReaderAdModel.ReaderAdImagePath.Substring(_ReaderAdModel.ReaderAdImagePath.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.ReaderAd))
                {
                    ErrorMessage = string.Format("文件{0}上传失败!", _ReaderAdModel.ReaderAdImagePath);
                    return false;
                }
                //更换文件名
                _ReaderAdModel.ReaderAdImagePath = _ReaderAdModel.Num + "_" + _ReaderAdModel.ReaderAdImagePath.Substring(_ReaderAdModel.ReaderAdImagePath.LastIndexOf("\\") + 1);
                _ReaderAdModel.ImageFilePath.Clear();
                _ReaderAdModel.ImageFilePath.Add(_ReaderAdModel.ReaderAdImagePath);
                _ReaderAdModel.Type = SeatManage.EnumType.AdType.ReaderAd;

                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.ReaderAd;
                model.ID = _ReaderAdModel.ID;
                model.Name = _ReaderAdModel.Name;
                model.Num = _ReaderAdModel.Num;
                model.AdContent = _ReaderAdModel.ToXml();
                model.EffectDate = _ReaderAdModel.EffectDate;
                model.EndDate = _ReaderAdModel.EndDate;
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
            ReaderImage = ImageSaveLocation(_ReaderAdModel.ReaderAdImagePath);
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
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.ReaderAd))
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
