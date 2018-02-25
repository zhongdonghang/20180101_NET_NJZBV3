using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelPlayListEditWindow : ViewModelObject
    {
        #region 构造函数
        public ViewModelPlayListEditWindow()
        {
            _PlayListModel = new Model.AMS_PlayList();
            MediaFileList.Clear();
            PlayFileList.Clear();
            foreach (AMS.Model.AMS_VideoItem item in _PlayListModel.MediaFiles)
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.ReRelativeUrl;
                newItem.SunTime = item.SunTime;
                newItem.MD5Value = item.MD5Value;
                MediaFileList.Add(newItem);
            }
            foreach (AMS.Model.AMS_VideoItem item in _PlayListModel.PlayFileList)
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.ReRelativeUrl;
                newItem.SunTime = item.SunTime;
                newItem.MD5Value = item.MD5Value;
                PlayFileList.Add(newItem);
            }
        }
        #endregion

        #region 私有成员
        private static readonly string CLASSNAME = "ViewModelPlayListEditWindow";
        private AMS.Model.AMS_PlayList _PlayListModel;
        private string _ErrorMessage = "";
        private bool _IsEdit = false;
        private int _StartH = 6;
        private int _StartM = 0;
        private DateTime _MediaPlayFullTime = DateTime.Parse("0:00:00");
        private int _LoopTime = 60;
        private int _LoopPlayCount = 1;
        private ObservableCollection<ViewModelVideoItem> _MediaFileList = new ObservableCollection<ViewModelVideoItem>();
        private ObservableCollection<ViewModelVideoItem> _PlayFileList = new ObservableCollection<ViewModelVideoItem>();
        private ObservableCollection<ViewModelVideoItem> _LoopPlayFileList = new ObservableCollection<ViewModelVideoItem>();
        private ViewModelProgressBar _vm_ProgressBar = new ViewModelProgressBar();
        /// <summary>
        /// 进度条
        /// </summary>
        public ViewModelProgressBar Vm_ProgressBar
        {
            get { return _vm_ProgressBar; }
            set { _vm_ProgressBar = value; OnPropertyChanged("Vm_ProgressBar"); }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 开始小时
        /// </summary>
        public int StartH
        {
            get { return _StartH; }
            set
            {
                if (value < 0)
                {
                    _StartH = 0;
                }
                else if (value > 23)
                {
                    _StartH = 23;
                }
                else
                {
                    _StartH = value;
                }
                RefreshPlayList();
                OnPropertyChanged("StartH");
            }
        }
        /// <summary>
        /// 开始分钟
        /// </summary>
        public int StartM
        {
            get { return _StartM; }
            set
            {
                if (value < 0)
                {
                    _StartM = 0;
                }
                else if (value > 59)
                {
                    _StartM = 59;
                }
                else
                {
                    _StartM = value;
                }
                RefreshPlayList();
                OnPropertyChanged("StartM");
            }
        }
        /// <summary>
        /// model
        /// </summary>
        public AMS.Model.AMS_PlayList PlayListModel
        {
            get { return _PlayListModel; }
            set
            {
                _PlayListModel = value;
                OnPropertyChanged("PlayListModel");
            }
        }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        /// <summary>
        /// 是否是更新
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; OnPropertyChanged("IsEdit"); }
        }
        /// <summary>
        /// 编号
        /// </summary>
        public string PlayeListNo
        {
            get { return _PlayListModel.Number; }
            set { _PlayListModel.Number = value; OnPropertyChanged("PlayeListNo"); }
        }
        /// <summary>
        /// 名称
        /// </summary>
        public string PlayerListName
        {
            get { return _PlayListModel.PlayListName; }
            set { _PlayListModel.PlayListName = value; OnPropertyChanged("PlayerListName"); }
        }
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime ReleaseDate
        {
            get
            {
                if (_PlayListModel.ReleaseDate != null)
                {
                    return _PlayListModel.ReleaseDate.Value;
                }
                else
                {
                    return DateTime.Now;
                }
            }
            set { _PlayListModel.ReleaseDate = value; OnPropertyChanged("ReleaseDate"); }
        }
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string EffectDate
        {
            get
            {
                if (_PlayListModel.EffectDate != null)
                {
                    return _PlayListModel.EffectDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.ToLongDateString();
                }
            }
            set { _PlayListModel.EffectDate = DateTime.Parse(value); OnPropertyChanged("EffectDate"); }
        }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndDate
        {
            get
            {
                if (_PlayListModel.EndDate != null)
                {
                    return _PlayListModel.EndDate.Value.ToLongDateString();
                }
                else
                {
                    return DateTime.Now.AddDays(30).ToLongDateString();
                }
            }
            set { _PlayListModel.EndDate = DateTime.Parse(value); OnPropertyChanged("EndDate"); }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Describe
        {
            get { return _PlayListModel.Describe; }
            set { _PlayListModel.Describe = value; OnPropertyChanged("Describe"); }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get
            {
                if (string.IsNullOrEmpty(_PlayListModel.OperatorName))
                {
                    return "编辑人：" + User.UserName;
                }
                else
                {
                    return _PlayListModel.OperatorName;
                }
            }
        }

        /// <summary>
        /// 媒体文件列表
        /// </summary>
        public ObservableCollection<ViewModelVideoItem> MediaFileList
        {
            get { return _MediaFileList; }
            set { _MediaFileList = value; OnPropertyChanged("MediaFileList"); }
        }

        /// <summary>
        /// 播放列表
        /// </summary>
        public ObservableCollection<ViewModelVideoItem> PlayFileList
        {
            get { return _PlayFileList; }
            set
            {
                _PlayFileList = value;
                OnPropertyChanged("PlayFileList");
            }
        }
        /// <summary>
        /// 循环播放列表
        /// </summary>
        public ObservableCollection<ViewModelVideoItem> LoopPlayFileList
        {
            get { return _LoopPlayFileList; }
            set { _LoopPlayFileList = value; OnPropertyChanged("LoopPlayFileList"); }
        }
        /// <summary>
        /// 播放时长
        /// </summary>
        public string MediaPlayFullTime
        {
            get { return "总播放时长：" + _MediaPlayFullTime.Hour + "小时" + _MediaPlayFullTime.Minute + "分钟" + _MediaPlayFullTime.Second + "秒"; }
        }

        /// <summary>
        /// 循环播放时间
        /// </summary>
        public string LoopPlayTime
        {
            get
            {
                return "循环次数：" + _LoopPlayCount;
            }
        }
        /// <summary>
        /// 循环播放时间
        /// </summary>
        public int LoopTime
        {
            get { return _LoopTime; }
            set
            {
                _LoopTime = value;
                RefreshLoopTime();
                OnPropertyChanged("LoopTime");
            }
        }
        #endregion

        #region 方法
        public void RefreshModel()
        {
            string functionName = "RefreshModel";
            try
            {
                MediaFileList.Clear();
                PlayFileList.Clear();
                foreach (AMS.Model.AMS_VideoItem item in _PlayListModel.MediaFiles)
                {
                    ViewModelVideoItem newItem = new ViewModelVideoItem();
                    newItem.Name = item.Name;
                    newItem.PlayTime = item.PlayTime;
                    newItem.ReRelativeUrl = item.ReRelativeUrl;
                    newItem.SunTime = item.SunTime;
                    newItem.MD5Value = item.MD5Value;
                    MediaFileList.Add(newItem);
                }
                foreach (AMS.Model.AMS_VideoItem item in _PlayListModel.PlayFileList)
                {
                    ViewModelVideoItem newItem = new ViewModelVideoItem();
                    newItem.Name = item.Name;
                    newItem.PlayTime = item.PlayTime;
                    newItem.ReRelativeUrl = item.ReRelativeUrl;
                    newItem.SunTime = item.SunTime;
                    newItem.MD5Value = item.MD5Value;
                    PlayFileList.Add(newItem);
                }
                StartH = DateTime.Parse(_PlayListModel.PlayFileList[0].PlayTime).Hour;
                StartM = DateTime.Parse(_PlayListModel.PlayFileList[0].PlayTime).Minute;
                RefreshPlayList();
                RefreshFileList();
                OnPropertyChanged("PlayeListNo");
                OnPropertyChanged("PlayerListName");
                OnPropertyChanged("EffectDate");
                OnPropertyChanged("EndDate");
                OnPropertyChanged("Operator");

            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 添加新的媒体文件
        /// </summary>
        public void AddNewMediaFile(ViewModelVideoItem itemFile)
        {
            string functionName = "AddNewMediaFile";
            try
            {
                bool isNew = true;
                foreach (ViewModelVideoItem item in MediaFileList)
                {
                    if (item.MD5Value == itemFile.MD5Value)
                    {
                        isNew = false;
                        break;
                    }
                }
                if (isNew)
                {
                    MediaFileList.Add(itemFile);
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 添加新的播放文件
        /// </summary>
        public void AddNewPlayFile(ViewModelVideoItem item)
        {
            string functionName = "AddNewPlayFile";
            try
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.Name;
                newItem.MD5Value = item.MD5Value;
                if (item.Name.Substring(item.Name.LastIndexOf(".")) == ".wmv" || item.Name.Substring(item.Name.LastIndexOf(".")) == ".WMV")
                {
                    newItem.SunTime = item.SunTime;
                }
                else
                {
                    newItem.SunTime = GetImagePlayTime(PlayFileList.Count - 1);
                }
                PlayFileList.Add(newItem);
                RefreshPlayList();
                RefreshFileList();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 批量添加添加新的播放文件
        /// </summary>
        public void LoopAddNewPlayFile(ViewModelVideoItem item)
        {
            string functionName = "LoopAddNewPlayFile";
            try
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.Name;
                newItem.MD5Value = item.MD5Value;
                if (item.Name.Substring(item.Name.LastIndexOf(".")) == ".wmv" || item.Name.Substring(item.Name.LastIndexOf(".")) == ".WMV")
                {
                    newItem.SunTime = item.SunTime;
                }
                else
                {
                    newItem.SunTime = GetImagePlayTime(PlayFileList.Count - 1);
                }
                PlayFileList.Add(newItem);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 插入播放文件
        /// </summary>
        public void InsertPlayFile(ViewModelVideoItem item, int index)
        {
            string functionName = "InsertPlayFile";
            try
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.Name;
                newItem.MD5Value = item.MD5Value;
                if (item.Name.Substring(item.Name.LastIndexOf(".")) == ".wmv" || item.Name.Substring(item.Name.LastIndexOf(".")) == ".WMV")
                {
                    newItem.SunTime = item.SunTime;
                }
                else
                {
                    newItem.SunTime = GetImagePlayTime(index);
                }
                PlayFileList.Insert(index, newItem);
                RefreshPlayList();
                RefreshFileList();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 插入播放文件
        /// </summary>
        public void LoopInsertPlayFile(ViewModelVideoItem item, int index)
        {
            string functionName = "LoopInsertPlayFile";
            try
            {
                ViewModelVideoItem newItem = new ViewModelVideoItem();
                newItem.Name = item.Name;
                newItem.PlayTime = item.PlayTime;
                newItem.ReRelativeUrl = item.Name;
                newItem.MD5Value = item.MD5Value;
                if (item.Name.Substring(item.Name.LastIndexOf(".")) == ".wmv" || item.Name.Substring(item.Name.LastIndexOf(".")) == ".WMV")
                {
                    newItem.SunTime = item.SunTime;
                }
                else
                {
                    newItem.SunTime = GetImagePlayTime(index);
                }
                PlayFileList.Insert(index, newItem);
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="item"></param>
        public void DeletePlayFile(int index)
        {
            string functionName = "DeletePlayFile";
            try
            {
                PlayFileList.RemoveAt(index);
                RefreshPlayList();
                RefreshFileList();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 向上移动
        /// </summary>
        /// <param name="index"></param>
        public void UpMoveItem(int index)
        {
            string functionName = "UpMoveItem";
            try
            {
                if (index < 1)
                {
                    ErrorMessage = "已移动到队列最前！";
                    return;
                }
                PlayFileList.Move(index, index - 1);
                RefreshPlayList();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 向下移动
        /// </summary>
        /// <param name="index"></param>
        public void DownMoveItem(int index)
        {
            string functionName = "DownMoveItem";
            try
            {
                if (index == PlayFileList.Count - 1)
                {
                    ErrorMessage = "已移动到队列最后！";
                    return;
                }
                PlayFileList.Move(index, index + 1);
                RefreshPlayList();
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            string functionName = "RefreshPlayList";
            try
            {
                if (string.IsNullOrEmpty(_PlayListModel.Number))
                {
                    ErrorMessage = "播放列表的编号不能为空！";
                    return false;
                }
                if (string.IsNullOrEmpty(_PlayListModel.PlayListName))
                {
                    ErrorMessage = "播放列表的名称不能为空！";
                    return false;
                }
                if (_PlayListModel.EffectDate == null)
                {
                    ErrorMessage = "播放列表的开始时间不能为空！";
                    return false;
                }
                if (_PlayListModel.EndDate == null)
                {
                    ErrorMessage = "播放列表的结束时间不能为空！";
                    return false;
                }
                if (_PlayListModel.EndDate < _PlayListModel.EffectDate)
                {
                    ErrorMessage = "播放列表的结束时间要大于开始时间！";
                    return false;
                }
                if (PlayFileList.Count == 0)
                {
                    ErrorMessage = "当前没有需要播放的文件！";
                    return false;
                }
                _PlayListModel.Operator = User.ID;
                ClearFileList();
                _PlayListModel.PlayFileList.Clear();
                _PlayListModel.MediaFiles.Clear();
                foreach (ViewModelVideoItem item in PlayFileList)
                {
                    AMS.Model.AMS_VideoItem newItem = new Model.AMS_VideoItem();
                    newItem.Name = _PlayListModel.Number + "_" + item.Name;
                    newItem.PlayTime = item.PlayTime;
                    newItem.ReRelativeUrl = _PlayListModel.Number + "_" + item.ReRelativeUrl;
                    newItem.SunTime = item.SunTime;
                    newItem.MD5Value = item.MD5Value;
                    _PlayListModel.PlayFileList.Add(newItem);
                }
                foreach (ViewModelVideoItem item in MediaFileList)
                {
                    AMS.Model.AMS_VideoItem newItem = new Model.AMS_VideoItem();
                    if (!IsEdit)
                    {
                        newItem.Name = _PlayListModel.Number + "_" + item.Name;
                    }
                    else
                    {
                        string name=item.Name.Substring(0,_PlayListModel.Number.Length);
                        if (_PlayListModel.Number == name)
                        {
                            newItem.Name = item.Name;
                        }
                        else
                        {
                            newItem.Name = _PlayListModel.Number + "_" + item.Name;
                        }
                    }
                    newItem.PlayTime = item.PlayTime;
                    newItem.ReRelativeUrl = item.ReRelativeUrl;
                    newItem.SunTime = item.SunTime;
                    newItem.MD5Value = item.MD5Value;
                    _PlayListModel.MediaFiles.Add(newItem);
                }
                string resultstr = "";
                //TODO:保存&文件上传
                AMS.ServiceProxy.FileOperate fileUpload = new ServiceProxy.FileOperate();
                fileUpload.HandleProgress += new ServiceProxy.EventHandleFileTransport(fileUpload_HandleProgress);
                Vm_ProgressBar.ProgressType = "上传媒体文件";
                Vm_ProgressBar.FullProgress = _PlayListModel.MediaFiles.Count;
                foreach (AMS.Model.AMS_VideoItem upFileItem in _PlayListModel.MediaFiles)
                {
                    Vm_ProgressBar.ProgressName = "正在上传\"" + upFileItem.Name + "\"……";
                    if (upFileItem.Name != upFileItem.ReRelativeUrl)
                    {
                        resultstr = fileUpload.UpdateFile(upFileItem.ReRelativeUrl, upFileItem.Name, SeatManage.EnumType.SeatManageSubsystem.MediaFiles);
                        if (!string.IsNullOrEmpty(resultstr))
                        {
                            ErrorMessage = string.Format("文件{0}上传失败！{1}", upFileItem.Name, resultstr);
                            Vm_ProgressBar.IsFinish();
                            return false;
                        }
                    }
                }
                if (IsEdit)
                {
                    //TODO:更新                  
                    resultstr = AMS.ServiceProxy.IPlayListService.UpdatePlaylist(_PlayListModel);
                }
                else
                {
                    //DOTO:添加
                    resultstr = AMS.ServiceProxy.IPlayListService.AddNewPlaylist(_PlayListModel);
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
        /// 刷新播放列表
        /// </summary>
        public void RefreshPlayList()
        {
            string functionName = "RefreshPlayList";
            try
            {
                if (PlayFileList.Count > 0)
                {
                    DateTime startTime = DateTime.Parse(_StartH + ":" + _StartM);
                    _MediaPlayFullTime = DateTime.Parse("0:00:00");
                    PlayFileList[0].PlayTime = startTime.ToLongTimeString();
                    PlayFileList[0].ID = 0;
                    _MediaPlayFullTime = _MediaPlayFullTime.AddSeconds(PlayFileList[0].SunTime);
                    for (int i = 1; i < PlayFileList.Count; i++)
                    {
                        PlayFileList[i].ID = i;
                        PlayFileList[i].PlayTime = DateTime.Parse(PlayFileList[i - 1].PlayTime).AddSeconds(PlayFileList[i - 1].SunTime).ToLongTimeString();
                        _MediaPlayFullTime = _MediaPlayFullTime.AddSeconds(PlayFileList[i].SunTime);
                    }
                }
                OnPropertyChanged("MediaPlayFullTime");
                OnPropertyChanged("PlayFileList");
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 删除多余文件
        /// </summary>
        private void ClearFileList()
        {
            string functionName = "ClearFileList";
            try
            {
                for (int i = 0; i < MediaFileList.Count; i++)
                {
                    bool isExit = false;
                    foreach (ViewModelVideoItem item in PlayFileList)
                    {
                        if (MediaFileList[i].MD5Value == item.MD5Value)
                        {
                            isExit = true;
                            break;
                        }
                    }
                    if (!isExit)
                    {
                        MediaFileList.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 刷新文件列表
        /// </summary>
        public void RefreshFileList()
        {
            string functionName = "RefreshFileList";
            try
            {
                foreach (ViewModelVideoItem Fileitem in MediaFileList)
                {
                    Fileitem.UsedCount = 0;
                    Fileitem.AllSum = DateTime.Parse("0:00:00");
                    foreach (ViewModelVideoItem Playitem in PlayFileList)
                    {
                        if (Fileitem.MD5Value == Playitem.MD5Value)
                        {
                            Fileitem.UsedCount++;
                            Fileitem.AllSum = Fileitem.AllSum.AddSeconds(Playitem.SunTime);
                        }
                    }
                }
            }
            catch (AMS.Model.CustomerException ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, ex.ErrorSourcesClass, ex.ErrorSourcesFunction);
            }
            catch (Exception ex)
            {
                ErrorMessage = string.Format("{0} 出自{1}.{2}", ex.Message, CLASSNAME, functionName);
            }
        }
        /// <summary>
        /// 刷新循环时间
        /// </summary>
        public void RefreshLoopTime()
        {
            LoopPlayFileList.Clear();
            _LoopPlayCount = 0;
            int _loopse = _LoopTime * 60;
            while (true)
            {
                foreach (ViewModelVideoItem item in PlayFileList)
                {
                    LoopPlayFileList.Add(item);
                    _loopse -= item.SunTime;
                    if (_loopse < 0)
                    {
                        break;
                    }
                }
                if (_loopse < 0)
                {
                    break;
                }
                _LoopPlayCount++;
            }
            OnPropertyChanged("LoopPlayTime");
        }
        /// <summary>
        /// 获取MD5和播放时长
        /// </summary>
        /// <param name="FilePathName"></param>
        /// <returns></returns>
        public ViewModelVideoItem GetItem(string FilePathName)
        {
            ViewModelVideoItem newItem = new ViewModelVideoItem();
            newItem.Name = FilePathName.Substring(FilePathName.LastIndexOf("\\") + 1);
            newItem.ReRelativeUrl = FilePathName;
            int sum = 0;
            if (newItem.Name.Substring(newItem.Name.LastIndexOf(".")) == ".wmv" || newItem.Name.Substring(newItem.Name.LastIndexOf(".")) == ".WMV")
            {
                Shell32.Shell shell = new Shell32.Shell();
                Shell32.Folder folder = shell.NameSpace(FilePathName.Substring(0, FilePathName.LastIndexOf("\\")));
                Shell32.FolderItem folderitem = folder.ParseName(newItem.Name);
                string len;
                if (Environment.OSVersion.Version.Major >= 6)
                {
                    len = folder.GetDetailsOf(folderitem, 27);
                }
                else
                {
                    len = folder.GetDetailsOf(folderitem, 21);
                }
                
                string[] str = len.Split(new char[] { ':' });

                sum = int.Parse(str[0]) * 3600 + int.Parse(str[1]) * 60 + int.Parse(str[2]) + 1;

            }
            newItem.SunTime = sum;
            newItem.MD5Value = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(newItem.ReRelativeUrl);
            return newItem;
        }
        /// <summary>
        /// 获取图片播放时长
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetImagePlayTime(int index)
        {
            int sum = 0;
            if (PlayFileList.Count > 0)
            {
                for (int i = index; i >= 0; i--)
                {
                    if (PlayFileList[i].Name.Substring(PlayFileList[i].Name.LastIndexOf(".")) != ".WMV" && PlayFileList[i].Name.Substring(PlayFileList[i].Name.LastIndexOf(".")) != ".wmv")
                    {
                        sum = PlayFileList[i].SunTime;
                        break;
                    }
                }
            }
            if (sum == 0)
            {
                sum = 10;
            }
            return sum;
        }
        #endregion



    }
    public class ViewModelVideoItem : ViewModelObject
    {
        private int _ID = -1;
        /// <summary>
        /// 序列号
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; OnPropertyChanged("ID"); }
        }
        private string _Name = "";
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; OnPropertyChanged("Name"); }
        }
        private string _PlayTime = "";
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string PlayTime
        {
            get { return _PlayTime; }
            set { _PlayTime = value; OnPropertyChanged("PlayTime"); }
        }
        private string _ReRelativeUrl = "";
        /// <summary>
        /// 文件路径
        /// </summary>
        public string ReRelativeUrl
        {
            get { return _ReRelativeUrl; }
            set { _ReRelativeUrl = value; OnPropertyChanged("ReRelativeUrl"); }
        }
        private int _SunTime = 0;
        /// <summary>
        /// 播放时长（秒）
        /// </summary>
        public int SunTime
        {
            get { return _SunTime; }
            set
            {
                _SunTime = value;
                OnPropertyChanged("SunTime");
            }
        }
        private string _MD5Value = "";
        /// <summary>
        /// MD5码
        /// </summary>
        public string MD5Value
        {
            get { return _MD5Value; }
            set { _MD5Value = value; OnPropertyChanged("MD5Value"); }
        }
        private int _UsedCount = 0;
        /// <summary>
        /// 被使用次数
        /// </summary>
        public int UsedCount
        {
            get { return _UsedCount; }
            set { _UsedCount = value; OnPropertyChanged("UsedCount"); }
        }
        private DateTime _AllSum = DateTime.Parse("0:00:00");
        /// <summary>
        /// 累计使用时长
        /// </summary>
        public DateTime AllSum
        {
            get { return _AllSum; }
            set { _AllSum = value; OnPropertyChanged("AllSum"); OnPropertyChanged("AllSumShow"); }
        }
        public string AllSumShow
        {
            get { return AllSum.ToLongTimeString(); }
        }
    }
}
