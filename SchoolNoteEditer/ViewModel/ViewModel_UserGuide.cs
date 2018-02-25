using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_UserGuide : INotifyPropertyChanged
    {
        private ObservableCollection<GuideImageItem> _GuideImages = new ObservableCollection<GuideImageItem>();
        /// <summary>
        /// 图片列表
        /// </summary>
        public ObservableCollection<GuideImageItem> GuideImages
        {
            get { return _GuideImages; }
            set { _GuideImages = value; OnPropertyChanged("GuideImages"); }
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
            GuideImageItem item = new GuideImageItem();
            item.ImageInfo = image;
            GuideImages.Add(item);
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="itemNum"></param>
        public void MoveItemLeft(int itemNum)
        {
            if (itemNum > 0)
            {
                GuideImages.Move(itemNum, itemNum - 1);
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="itemNum"></param>
        public void MoveItemRight(int itemNum)
        {
            if (itemNum < GuideImages.Count - 1)
            {
                GuideImages.Move(itemNum, itemNum + 1);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="itemNum"></param>
        public void DeleteItem(int itemNum)
        {
            GuideImages.RemoveAt(itemNum);
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            SeatManage.Bll.FileOperate fileOperation = new SeatManage.Bll.FileOperate();
            SeatManage.ClassModel.UserGuideInfo model = new SeatManage.ClassModel.UserGuideInfo();
            foreach (GuideImageItem item in _GuideImages)
            {
                if (!fileOperation.UpdateFile(item.ImageUrl, item.ImageNmae, SeatManage.EnumType.SeatManageSubsystem.UserGuide))
                {
                    ErrorMessage = string.Format("文件{0}上传失败!", item.ImageNmae);
                    return false;
                }
                model.ImageFilePath.Add(item.ImageNmae);
            }
            model.XMLContent = model.ToXml();
            if (SeatManage.Bll.T_SM_SystemSet.UpdateUserGuide(model))
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "保存成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
                return true;
            }
            else
            {
                ErrorMessage = string.Format("保存失败！");
                return false;
            }
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        public void GetData()
        {
            SeatManage.ClassModel.UserGuideInfo model = SeatManage.Bll.T_SM_SystemSet.GetUserGuide();
            if (model == null)
            {
                return;
            }
            GuideImages.Clear();
            foreach (string file in model.ImageFilePath)
            {
                GuideImageItem item = new GuideImageItem();
                item.ImageInfo = ImageSaveLocation(file);
                GuideImages.Add(item);
            }
        }

        /// <summary>
        /// 下载图片并且获取本地图片路径
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private BitmapImage ImageSaveLocation(string imageName)
        {
            string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imageName);
            if (!File.Exists(filePath))
            {//如果本地文件不存在，则下载
                SeatManage.Bll.FileOperate fileDownload = new SeatManage.Bll.FileOperate();
                if (!fileDownload.FileDownLoad(filePath, imageName, SeatManage.EnumType.SeatManageSubsystem.UserGuide))
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
    public class GuideImageItem : INotifyPropertyChanged
    {
        private string _ImageUrl = "";
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl
        {
            get { return _ImageUrl; }
            set { _ImageUrl = value; OnPropertyChanged("ImageUrl"); }
        }
        private string _ImageNmae = "";
        /// <summary>
        /// 图片名称
        /// </summary>
        public string ImageNmae
        {
            get { return _ImageNmae; }
            set { _ImageNmae = value; OnPropertyChanged("ImageNmae"); }
        }
        private System.Windows.Media.Imaging.BitmapImage _ImageInfo;
        /// <summary>
        /// 图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage ImageInfo
        {
            get { return _ImageInfo; }
            set
            {
                _ImageInfo = value;
                if (_ImageInfo != null)
                {
                    _ImageUrl = _ImageInfo.UriSource.LocalPath;
                    _ImageNmae = _ImageUrl.Substring(_ImageUrl.LastIndexOf("\\") + 1);
                }
                OnPropertyChanged("ImageInfo");
            }
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
