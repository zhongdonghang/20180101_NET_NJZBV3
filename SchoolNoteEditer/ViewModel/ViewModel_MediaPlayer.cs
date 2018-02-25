﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.IO;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_MediaPlayer : INotifyPropertyChanged
    {

        private SeatManage.ClassModel.PlaylistInfo _PlaylistModel = new SeatManage.ClassModel.PlaylistInfo();
        /// <summary>
        /// 广告model
        /// </summary>
        public SeatManage.ClassModel.PlaylistInfo PlaylistModel
        {
            get { return _PlaylistModel; }
            set { _PlaylistModel = value; }
        }
        private ObservableCollection<ViewModel_VideoItem> _VideoItems = new ObservableCollection<ViewModel_VideoItem>();
        /// <summary>
        /// 播放文件子项
        /// </summary>
        public ObservableCollection<ViewModel_VideoItem> VideoItems
        {
            get { return _VideoItems; }
            set { _VideoItems = value; }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _PlaylistModel.Num; }
            set
            {
                _PlaylistModel.Num = value;
                OnPropertyChanged("Num");
            }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _PlaylistModel.Name; }
            set { _PlaylistModel.Name = value; OnPropertyChanged("Name"); }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime EffectDate
        {
            get
            {
                if (_PlaylistModel.EffectDate == null || _PlaylistModel.EffectDate < DateTime.Parse("2000-1-1"))
                {
                    _PlaylistModel.EffectDate = DateTime.Now.Date;
                }
                return _PlaylistModel.EffectDate;
            }
            set { _PlaylistModel.EffectDate = value; OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get
            {
                if (_PlaylistModel.EndDate == null || _PlaylistModel.EndDate < DateTime.Parse("2000-1-1"))
                {
                    _PlaylistModel.EndDate = DateTime.Now.AddDays(30).Date;
                }
                return _PlaylistModel.EndDate;
            }
            set { _PlaylistModel.EndDate = value; OnPropertyChanged("EndDate"); }
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
        /// 添加新项
        /// </summary>
        /// <param name="filePath"></param>
        public ViewModel_VideoItem GetNewItme(string filePath)
        {
            try
            {
                ViewModel_VideoItem itemVideo = new ViewModel_VideoItem();
                itemVideo.VideoFliePath = filePath;
                if (filePath.Substring(filePath.LastIndexOf(".")).ToLower() == ".wmv")
                {

                    itemVideo.IsImage = false;
                    SeatManage.SeatManageComm.VideoOperation mediaOperation = new SeatManage.SeatManageComm.VideoOperation();
                    itemVideo.PlayTime = int.Parse(TimeSpan.Parse(mediaOperation.GetVideoDuration(filePath)).TotalSeconds.ToString().Split('.')[0]) + 1;
                    string imagePath = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                    imagePath = imagePath.Substring(0, imagePath.IndexOf('.')) + ".jpg";
                    imagePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, imagePath);
                    if (!Directory.Exists(string.Format(@"{0}temp", AppDomain.CurrentDomain.BaseDirectory)))
                    {
                        Directory.CreateDirectory(string.Format(@"{0}temp", AppDomain.CurrentDomain.BaseDirectory));
                    }
                    itemVideo.ShowImage = new BitmapImage(new Uri(mediaOperation.ConvertImage(filePath, imagePath, "200*200", itemVideo.PlayTime), UriKind.RelativeOrAbsolute));
                    itemVideo.Md5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(filePath);
                }
                else
                {
                    itemVideo.IsImage = true;
                    itemVideo.PlayTime = 10;
                    itemVideo.Md5 = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(filePath);
                    itemVideo.ShowImage = new BitmapImage(new Uri(filePath, UriKind.RelativeOrAbsolute));
                }
                return itemVideo;
            }
            catch
            {
                ErrorMessage = "获取媒体信息失败，请确保文件路径正确并且文件路径中没有空格";
                return null;
            }
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="index"></param>
        public void MoveLeft(int index)
        {
            if (index > 0)
            {
                VideoItems.Move(index, index - 1);
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="index"></param>
        public void MoveRight(int index)
        {
            if (index < VideoItems.Count - 1)
            {
                VideoItems.Move(index, index + 1);
            }
        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="index"></param>
        public void ItemDelete(int index)
        {
            VideoItems.RemoveAt(index);
        }
        public bool Save()
        {
            try
            {
                if (string.IsNullOrEmpty(PlaylistModel.Num))
                {
                    ErrorMessage = "播放列表的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(PlaylistModel.Name))
                {
                    ErrorMessage = "播放列表的名称不能为空！";
                    return false;
                }
                if (PlaylistModel.EffectDate == null)
                {
                    ErrorMessage = "播放列表的开始时间不能为空！";
                    return false;
                }
                if (PlaylistModel.EndDate == null)
                {
                    ErrorMessage = "播放列表的结束时间不能为空！";
                    return false;
                }
                if (PlaylistModel.EndDate < PlaylistModel.EffectDate)
                {
                    ErrorMessage = "播放列表的结束时间要大于开始时间！";
                    return false;
                }
                if (VideoItems.Count == 0)
                {
                    ErrorMessage = "当前没有需要播放的文件！";
                    return false;
                }
                if (!IsEdit && SeatManage.Bll.AdvertisementOperation.GetAdModel(PlaylistModel.Num, SeatManage.EnumType.AdType.TitleAd) != null)
                {
                    ErrorMessage = "已存在存在相同名称或编号的播放列表！";
                    return false;
                }
                PlaylistModel.ImageFilePath.Clear();
                PlaylistModel.MediaPlayList.Clear();
                foreach (ViewModel_VideoItem item in VideoItems)
                {
                    SeatManage.ClassModel.PlaylistItemInfo newItem = new SeatManage.ClassModel.PlaylistItemInfo();
                    newItem = item.VideoItemModel;
                    newItem.MediaFileName = PlaylistModel.Num + "_" + newItem.MediaFileName.Substring(newItem.MediaFileName.LastIndexOf("\\") + 1);
                    PlaylistModel.MediaPlayList.Add(newItem);
                    bool isnew = true;
                    foreach (string samefile in PlaylistModel.ImageFilePath)
                    {
                        if (samefile == item.VideoFliePath)
                        {
                            isnew = false;
                            break;
                        }
                    }
                    if (isnew)
                    {
                        PlaylistModel.ImageFilePath.Add(item.VideoFliePath);
                    }
                }
                string resultstr = "";
                //TODO:保存&文件上传
                SeatManage.Bll.FileOperate fileUpload = new SeatManage.Bll.FileOperate();
                foreach (string upFileItem in PlaylistModel.ImageFilePath)
                {
                    if (!fileUpload.UpdateFile(upFileItem, PlaylistModel.Num + "_" + upFileItem.Substring(upFileItem.LastIndexOf("\\") + 1), SeatManage.EnumType.SeatManageSubsystem.PlaylistAd))
                    {
                        ErrorMessage = string.Format("文件{0}上传失败！{1}", upFileItem, resultstr);
                        return false;
                    }
                }
                //更换文件名
                for (int i = 0; i < PlaylistModel.ImageFilePath.Count; i++)
                {
                    PlaylistModel.ImageFilePath[i] = PlaylistModel.Num + "_" + PlaylistModel.ImageFilePath[i].Substring(PlaylistModel.ImageFilePath[i].LastIndexOf("\\") + 1);
                }
                PlaylistModel.Type =SeatManage.EnumType.AdType.PlaylistAd;
                SeatManage.ClassModel.AMS_Advertisement model = new SeatManage.ClassModel.AMS_Advertisement();
                model.Type = SeatManage.EnumType.AdType.PlaylistAd;
                model.ID = PlaylistModel.ID;
                model.Name = PlaylistModel.Name;
                model.Num = PlaylistModel.Num;
                model.AdContent = PlaylistModel.ToXml();
                model.EffectDate = PlaylistModel.EffectDate;
                model.EndDate = PlaylistModel.EndDate;
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
        /// 获取更新数据
        /// </summary>
        public void GetData()
        {
            foreach (SeatManage.ClassModel.PlaylistItemInfo item in PlaylistModel.MediaPlayList)
            {
                if (!Directory.Exists(string.Format(@"{0}temp", AppDomain.CurrentDomain.BaseDirectory)))
                {
                    Directory.CreateDirectory(string.Format(@"{0}temp", AppDomain.CurrentDomain.BaseDirectory));
                }
                SeatManage.Bll.FileOperate fileDownload = new SeatManage.Bll.FileOperate();
                string filePath = string.Format(@"{0}temp\{1}", AppDomain.CurrentDomain.BaseDirectory, item.MediaFileName.Split('_')[1]);
                if (fileDownload.FileDownLoad(filePath, item.MediaFileName, SeatManage.EnumType.SeatManageSubsystem.PlaylistAd))
                {
                    VideoItems.Add(GetNewItme(filePath));
                }
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
    /// <summary>
    /// 播放子项
    /// </summary>
    public class ViewModel_VideoItem  : INotifyPropertyChanged
    {
        private SeatManage.ClassModel.PlaylistItemInfo _VideoItemModel = new SeatManage.ClassModel.PlaylistItemInfo();
        /// <summary>
        /// model
        /// </summary>
        public SeatManage.ClassModel.PlaylistItemInfo VideoItemModel
        {
            get { return _VideoItemModel; }
            set { _VideoItemModel = value; OnPropertyChanged("VideoItemModel"); }
        }

        private bool _IsImage = true;
        /// <summary>
        /// 是否是图片
        /// </summary>
        public bool IsImage
        {
            get { return _IsImage; }
            set { _IsImage = value; OnPropertyChanged("IsImage"); }
        }

        private System.Windows.Media.Imaging.BitmapImage _ShowImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 显示图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage ShowImage
        {
            get { return _ShowImage; }
            set { _ShowImage = value; OnPropertyChanged("ShowImage"); }
        }
        private string _VideoFliePath = "";
        /// <summary>
        /// 媒体文件地址
        /// </summary>
        public string VideoFliePath
        {
            get { return _VideoFliePath; }
            set
            {
                _VideoFliePath = value;
                _VideoItemModel.MediaFileName = value.Substring(value.LastIndexOf("\\") + 1);
                OnPropertyChanged("VideoFliePath");
            }
        }
        /// <summary>
        /// MD5码
        /// </summary>
        public string Md5
        {
            get { return _VideoItemModel.MD5Key; }
            set { _VideoItemModel.MD5Key = value; OnPropertyChanged("Md5"); }
        }
        /// <summary>
        /// 播放时长
        /// </summary>
        public int PlayTime
        {
            get { return _VideoItemModel.PlayTime; }
            set { _VideoItemModel.PlayTime = value; OnPropertyChanged("PlayTime"); }
        }

        #region
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
