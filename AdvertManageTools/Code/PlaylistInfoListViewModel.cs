using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using AdvertManage.Model;
using AdvertManage.BLL;
using System.IO;

namespace AdvertManageTools.Code
{
    /// <summary>
    /// 播放列表列表
    /// </summary>
    public class PlaylistInfoListViewModel : INotifyPropertyChanged
    {
        ObservableCollection<PlaylistInfoViewModel> _PlaylistList = new ObservableCollection<PlaylistInfoViewModel>();
        /// <summary>
        /// 播放列表列表
        /// </summary>
        public ObservableCollection<PlaylistInfoViewModel> PlaylistList
        {
            get { return _PlaylistList; }
            set
            {
                _PlaylistList = value;
                Changed("PlaylistList");
            }
        }
        /// <summary>
        /// 播放列表获取
        /// </summary>
        public void DataBinding()
        {
            try
            {
                _PlaylistList.Clear();
                List<AMS_PlayListMd5Model> modellist = AMS_PlayListBLL.GetMd5Playlist();
                foreach (AMS_PlayListMd5Model model in modellist)
                {
                    PlaylistInfoViewModel viewmodel = new PlaylistInfoViewModel();
                    viewmodel.ViewModelParse(model);
                    _PlaylistList.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 播放列表
    /// </summary>
    public class PlaylistInfoViewModel : INotifyPropertyChanged
    {
        private string _id;
        /// <summary>
        /// id
        /// </summary>
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _Number;
        /// <summary>
        /// 编号
        /// </summary>
        public string Number
        {
            get { return _Number; }
            set
            {
                _Number = value;
                Changed("Number");
            }
        }
        private string _BeginH;
        /// <summary>
        /// 开始的小时
        /// </summary>
        public string BeginH
        {
            get { return _BeginTime.Hour.ToString(); }
            set
            {
                _BeginTime = _BeginTime.AddHours(int.Parse(value) - _BeginTime.Hour);
                Changed("BeginH");
            }
        }
        private int _LoopTime = 60;
        /// <summary>
        /// 循环时间
        /// </summary>
        public int LoopTime
        {
            get { return _LoopTime; }
            set
            {
                _LoopTime = value;
                Changed("LoopTime");
            }
        }
        private string _BeginM;
        /// <summary>
        /// 开始的分钟
        /// </summary>
        public string BeginM
        {
            get { return _BeginTime.Minute.ToString(); }
            set
            {
                _BeginTime = _BeginTime.AddMinutes(int.Parse(value) - _BeginTime.Minute);
                Changed("BeginM");
            }
        }
        private DateTime _SubmitDate = ServerDateTime.Now.Value;
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime SubmitDate
        {
            get { return _SubmitDate; }
            set
            {
                _SubmitDate = value;
                Changed("SubmitDate");
            }
        }
        /// <summary>
        /// 开始播放时间
        /// </summary>
        private DateTime _BeginTime = DateTime.Parse("6:00");

        private DateTime _BeginDate = ServerDateTime.Now.Value;
        /// <summary>
        /// 播放开始日期
        /// </summary>
        public DateTime BeginDate
        {
            get { return _BeginDate; }
            set
            {
                _BeginDate = value;
                Changed("BeginDate");
            }
        }
        private DateTime _EndDate = ServerDateTime.Now.Value.AddMonths(1);
        /// <summary>
        /// 结束播放日期
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set
            {
                _EndDate = value;
                Changed("EndDate");
            }
        }
        ObservableCollection<PlaylistItemViewModel> _ItemList = new ObservableCollection<PlaylistItemViewModel>();
        /// <summary>
        /// 媒体文件列表
        /// </summary>
        public ObservableCollection<PlaylistItemViewModel> ItemList
        {
            get { return _ItemList; }
            set
            {
                _ItemList = value;
                Changed("Name");
            }
        }
        ObservableCollection<PlaylistItemViewModel> _LoopList = new ObservableCollection<PlaylistItemViewModel>();
        /// <summary>
        /// 循环列表
        /// </summary>
        public ObservableCollection<PlaylistItemViewModel> LoopList
        {
            get { return _LoopList; }
            set
            {
                _LoopList = value;
                Changed("LoopList");
            }
        }
        /// <summary>
        /// 媒体文件转换为VM
        /// </summary>
        /// <param name="itemlist"></param>
        /// <returns></returns>
        public void ViewModelParse(AMS_PlayListMd5Model playlist)
        {
            _id = playlist.Id;
            _Number = playlist.PlayListNo;
            _SubmitDate = playlist.ReleaseDate;
            _BeginDate = playlist.EffectDate;
            _EndDate = playlist.EndDate;
            _BeginTime = DateTime.Parse(playlist.PlayVideoItems[0].PlayTime);
            _ItemList.Clear();
            for (int i = 0; i < playlist.PlayVideoItems.Count; i++)
            {
                PlaylistItemViewModel itemvm = new PlaylistItemViewModel();
                itemvm.Name = playlist.PlayVideoItems[i].Name;
                itemvm.FilePath = playlist.PlayVideoItems[i].RelativeUrl;
                itemvm.Md5Value = playlist.PlayVideoItems[i].md5value;
                if (i + 1 >= playlist.PlayVideoItems.Count)
                {
                    itemvm.SunTime = playlist.PlayElapsed;
                }
                else
                {
                    itemvm.SunTime = int.Parse((DateTime.Parse(playlist.PlayVideoItems[i + 1].PlayTime) - DateTime.Parse(playlist.PlayVideoItems[i].PlayTime)).TotalSeconds.ToString().Split('.')[0]);
                }
                _ItemList.Add(itemvm);
            }
        }
        /// <summary>
        /// VM转换为文件列表
        /// </summary>
        /// <param name="itemlist"></param>
        /// <returns></returns>
        public AMS_PlayListMd5Model ToModel()
        {
            AMS_PlayListMd5Model model = new AMS_PlayListMd5Model();
            model.Id = _id;
            model.PlayListNo = _Number;
            model.ReleaseDate = ServerDateTime.Now.Value;
            model.EffectDate = _BeginDate;
            model.EndDate = _EndDate;
            model.PlayVideoItems.Clear();
            model.PlayElapsed = _ItemList[_ItemList.Count - 1].SunTime;
            for (int i = 0; i < _ItemList.Count; i++)
            {
                AMS_VideoMd5Item item = new AMS_VideoMd5Item();
                if (i == 0)
                {
                    item.PlayTime = _BeginTime.ToLongTimeString();
                }
                else
                {
                    item.PlayTime = (DateTime.Parse(model.PlayVideoItems[i - 1].PlayTime).AddSeconds(_ItemList[i - 1].SunTime)).ToLongTimeString();
                }
                if (_ItemList[i].FilePath != _ItemList[i].Name)
                {
                    item.Name = _Number + _ItemList[i].Name;
                    item.RelativeUrl = _ItemList[i].FilePath;
                }
                else
                {
                    item.Name = _ItemList[i].Name;
                    item.RelativeUrl = _ItemList[i].FilePath;
                }
                item.md5value = _ItemList[i].Md5Value;
                model.PlayVideoItems.Add(item);
            }
            return model;
        }
        /// <summary>
        /// 添加播放列表
        /// </summary>
        public bool AddPlaylist()
        {
            AMS_PlayListMd5Model model = AMS_PlayListMd5Model.Parse(ToModel().ToXml());
            model.Id = _id;
            model.ReleaseDate = _SubmitDate;
            try
            {
                if (!string.IsNullOrEmpty(_Number) && _ItemList.Count > 0 && (_BeginDate < _EndDate))
                {
                    AMS_PlayListMd5Model samemodel = AMS_PlayListBLL.GetMd5PlaylistByNum(_Number);
                    if (samemodel == null)
                    {
                        foreach (AMS_VideoMd5Item item in model.VideoFiles)
                        {
                            if (item.RelativeUrl != item.Name)
                            {
                                FileOperate fo = new FileOperate();
                                if (!fo.UpdateFile(item.RelativeUrl, item.Name, SeatManage.EnumType.SeatManageSubsystem.MediaFiles))
                                {
                                    throw new Exception("文件" + item.Name + "上传失败！");
                                }
                            }
                        }
                        foreach (AMS_VideoMd5Item videoitem in model.PlayVideoItems)
                        {
                            videoitem.RelativeUrl = videoitem.Name;
                        }
                        if (AMS_PlayListBLL.AddMd5PlayList(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                        {
                            throw new Exception("发布播放列表失败，具体详情请查看错误日志！");
                        }
                        return true;
                    }
                    else
                    {
                        throw new Exception("已存在重复的编号！");
                    }
                }
                else
                {
                    throw new Exception("信息填写错误！请仔细检查！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 循环添加媒体文件
        /// </summary>
        public bool LoopMedia()
        {
            try
            {
                if (_ItemList.Count < 1)
                {
                    throw new Exception("媒体文件不能为空！");
                }
                if (_LoopTime < 1)
                {
                    throw new Exception("信息填写错误！请仔细检查！");
                }
                else
                {
                    foreach (PlaylistItemViewModel item in _ItemList)
                    {
                        if (item.SunTime < 1)
                        {
                            throw new Exception("请填入有效的播放时长！");
                        }
                    }

                    int loopsetime = _LoopTime * 60;
                    bool flish = false;
                    while (!flish)
                    {
                        foreach (PlaylistItemViewModel item in _ItemList)
                        {
                            if (loopsetime < 0)
                            {
                                flish = true;
                                break;
                            }
                            else
                            {
                                loopsetime = loopsetime - item.SunTime;
                            }
                            PlaylistItemViewModel loopitme = new PlaylistItemViewModel();
                            loopitme.FilePath = item.FilePath;
                            loopitme.Name = item.Name;
                            loopitme.SunTime = item.SunTime;
                            loopitme.Md5Value = item.Md5Value;
                            _LoopList.Add(loopitme);
                        }
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 更新播放列表
        /// </summary>
        public bool UpdatePlaylist()
        {
            AMS_PlayListMd5Model model = AMS_PlayListMd5Model.Parse(ToModel().ToXml());
            model.Id = _id;
            model.ReleaseDate = _SubmitDate;
            try
            {
                if (!string.IsNullOrEmpty(_Number) && _ItemList.Count > 0 && (_BeginDate < _EndDate))
                {
                    AMS_PlayListMd5Model samemodel = AMS_PlayListBLL.GetMd5PlaylistByNum(_Number);
                    if (samemodel == null || samemodel.Id == _id)
                    {
                        foreach (AMS_VideoMd5Item item in model.VideoFiles)
                        {
                            if (item.RelativeUrl != item.Name)
                            {
                                FileOperate fo = new FileOperate();
                                if (!fo.UpdateFile(item.RelativeUrl, item.Name, SeatManage.EnumType.SeatManageSubsystem.MediaFiles))
                                {
                                    throw new Exception("文件" + item.Name + "上传失败！");
                                }
                            }
                        }
                        foreach (AMS_VideoMd5Item videoitem in model.PlayVideoItems)
                        {
                            videoitem.RelativeUrl = videoitem.Name;
                            videoitem.md5value = videoitem.md5value;
                        }
                        if (AMS_PlayListBLL.UpdateMd5PlayList(model) == AdvertManage.Model.Enum.HandleResult.Failed)
                        {
                            throw new Exception("更新播放列表失败，具体详情请查看错误日志！");
                        }
                        return true;
                    }
                    else
                    {
                        throw new Exception("已存在重复的编号！");
                    }
                }
                else
                {
                    throw new Exception("信息填写错误！请仔细检查！");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// 导出离线版本
        /// </summary>
        public bool DownloadPlaylist(string downloadpath)
        {
            try
            {
                downloadpath = downloadpath + "\\MediaPlaylist_" + ServerDateTime.Now.Value.ToShortDateString();
                DirectoryInfo dir = new DirectoryInfo(downloadpath);
                if (!dir.Exists)
                {
                    dir.Create();
                }
                else
                {
                    throw new Exception("存在相同的文件夹，请重新选择目录！");
                }
                AMS_PlayListMd5Model model = AMS_PlayListMd5Model.Parse(ToModel().ToXml());
                model.Id = _id;
                model.ReleaseDate = _SubmitDate;
                if (!string.IsNullOrEmpty(_Number) && _ItemList.Count > 0 && (_BeginDate < _EndDate))
                {
                    foreach (AMS_VideoMd5Item item in model.VideoFiles)
                    {
                        if (item.RelativeUrl == item.Name)
                        {
                            FileOperate fo = new FileOperate();
                            if (!fo.FileDownLoad(downloadpath + "\\" + item.RelativeUrl, item.RelativeUrl, SeatManage.EnumType.SeatManageSubsystem.MediaFiles))
                            {
                                throw new Exception("文件" + item.Name + "离线保存失败！");
                            }
                        }
                        else
                        {
                            File.Copy(item.RelativeUrl, downloadpath + "\\" + item.Name);
                        }
                        foreach (AMS_VideoMd5Item videoitem in model.PlayVideoItems)
                        {
                            videoitem.RelativeUrl = videoitem.Name;
                            videoitem.md5value = videoitem.md5value;
                        }
                    }
                    string xml = model.ToXml();
                    string xmlpath = downloadpath + "\\playList.xml";
                    //写入文件
                    FileStream fs = new FileStream(xmlpath, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(fs);
                    sw.Write(xml);
                    sw.Close();
                    fs.Close();
                    return true;
                }
                else
                {
                    throw new Exception("信息填写错误！请仔细检查！");

                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
                return false;
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
    /// <summary>
    /// 媒体文件
    /// </summary>
    public class PlaylistItemViewModel : INotifyPropertyChanged
    {

        private string _Name;
        /// <summary>
        /// 视频名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                Changed("Name");
            }
        }

        private string _FilePath;
        /// <summary>
        /// 上传文件路径
        /// </summary>
        public string FilePath
        {
            get { return _FilePath; }
            set
            {
                _FilePath = value;
                Changed("FilePath");
            }
        }

        private int _sunTime;
        /// <summary>
        /// 播放时长
        /// </summary>
        public int SunTime
        {
            get { return _sunTime; }
            set
            {
                _sunTime = value;
                Changed("SunTime");
            }
        }
        /// <summary>
        /// md5校验值
        /// </summary>
        private string _md5Value;

        public string Md5Value
        {
            get { return _md5Value; }
            set { _md5Value = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
