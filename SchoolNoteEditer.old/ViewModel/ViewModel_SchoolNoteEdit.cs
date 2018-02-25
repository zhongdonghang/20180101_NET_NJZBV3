using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using System.IO;
using System.ComponentModel;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_SchoolNoteEdit : INotifyPropertyChanged
    {
        private SeatManage.ClassModel.SchoolNoteInfo _NoteModel = new SeatManage.ClassModel.SchoolNoteInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.SchoolNoteInfo NoteModel
        {
            get { return _NoteModel; }
            set { _NoteModel = value; }
        }
        private System.Windows.Media.Imaging.BitmapImage _NoteImage;
        /// <summary>
        /// 图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage NoteImage
        {
            get { return _NoteImage; }
            set
            {
                _NoteImage = value;
                if (_NoteImage != null)
                {
                    _NoteModel.NoteImagePath = _NoteImage.UriSource.LocalPath;
                }
                OnPropertyChanged("NoteImage");
            }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _NoteModel.Num; }
            set { _NoteModel.Num = value; OnPropertyChanged("Num"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _NoteModel.Name; }
            set { _NoteModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_NoteModel.EffectDate == null || _NoteModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _NoteModel.EffectDate = DateTime.Now.Date;
                }
                return _NoteModel.EffectDate;
            }
            set { _NoteModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_NoteModel.EndDate == null || _NoteModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _NoteModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _NoteModel.EndDate;
            }
            set { _NoteModel.EndDate = value; OnPropertyChanged("EndDate"); }
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
                if (string.IsNullOrEmpty(_NoteModel.Num))
                {
                    ErrorMessage = "编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_NoteModel.Name))
                {
                    ErrorMessage = "名称不能为空！";
                    return false;
                }
                if (_NoteImage == null)
                {
                    ErrorMessage = "图片不能为空！";
                    return false;
                }
                if (_NoteModel.EffectDate == null)
                {
                    ErrorMessage = "开始时间不能为空！";
                    return false;
                }
                if (_NoteModel.EndDate == null)
                {
                    ErrorMessage = "结束时间不能为空！";
                    return false;
                }
                if (_NoteModel.EndDate < _NoteModel.EffectDate)
                {
                    ErrorMessage = "结束时间要大于开始时间！";
                    return false;
                }

                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(_NoteModel.Num, SeatManage.EnumType.AdType.SchoolNotice) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的通知！";
                    return false;
                }

                string resultstr = "";                //TODO:保存&文件上传
                SeatManage.Bll.FileOperate fileOperation = new SeatManage.Bll.FileOperate();

                if (!fileOperation.UpdateFile(_NoteModel.NoteImagePath, _NoteModel.Num + "_" + _NoteModel.NoteImagePath.Substring(_NoteModel.NoteImagePath.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.SchoolNotice))
                {
                    ErrorMessage = string.Format("文件{0}上传失败!", _NoteModel.NoteImagePath);
                    return false;
                }
                //更换文件名
                _NoteModel.NoteImagePath = _NoteModel.Num + "_" + _NoteModel.NoteImagePath.Substring(_NoteModel.NoteImagePath.LastIndexOf("\\") + 1);
                _NoteModel.ImageFilePath.Clear();
                _NoteModel.ImageFilePath.Add(_NoteModel.NoteImagePath);
                _NoteModel.Type = SeatManage.EnumType.AdType.SchoolNotice;

                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.SchoolNotice;
                model.ID = _NoteModel.ID;
                model.Name = _NoteModel.Name;
                model.Num = _NoteModel.Num;
                model.AdContent = _NoteModel.ToXml();
                model.EffectDate = _NoteModel.EffectDate;
                model.EndDate = _NoteModel.EndDate;
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
            NoteImage = ImageSaveLocation(_NoteModel.NoteImagePath);
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
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.SchoolNotice))
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
