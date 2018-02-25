using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace AMS.ViewModel
{
    public class ViewModelPlaylistListUC : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelPlaylistListUC";
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private ObservableCollection<ViewModelPlayListShow> _PlaylistList = new ObservableCollection<ViewModelPlayListShow>();
        /// <summary>
        /// 列表
        /// </summary>
        public ObservableCollection<ViewModelPlayListShow> PlaylistList
        {
            get { return _PlaylistList; }
            set { _PlaylistList = value; OnPropertyChanged("PlaylistList"); }
        }
        public bool GetDataList()
        {
            string functionName = "GetDataList";
            try
            {
                List<AMS.Model.AMS_PlayList> modellist = new List<AMS.Model.AMS_PlayList>();
                //TODO:获取数据
                modellist = AMS.ServiceProxy.IPlayListService.GetPlaylistList();
                //测试数据
                PlaylistList.Clear();
                foreach (AMS.Model.AMS_PlayList model in modellist)
                {
                    PlaylistList.Add(new ViewModelPlayListShow(model));
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


    }
    public class ViewModelPlayListShow : ViewModelObject
    {
        public ViewModelPlayListShow(AMS.Model.AMS_PlayList model)
        {
            ModelList = model;
            _StartTime = model.PlayFileList[0].PlayTime;
            _MediaFileCount = model.MediaFiles.Count;
            _PlayFileCount = model.PlayFileList.Count;
            _AllPlayTime = (DateTime.Parse("0:00:00").AddSeconds((DateTime.Parse(model.PlayFileList[model.PlayFileList.Count - 1].PlayTime) - DateTime.Parse(model.PlayFileList[0].PlayTime)).Seconds + model.PlayFileList[model.PlayFileList.Count - 1].SunTime)).ToLongTimeString();
            for (int i = 0; i < model.MediaFiles.Count; i++)
            {
                _MediaFiles += model.MediaFiles[i].Name + " ";
            }
        }
        private AMS.Model.AMS_PlayList _ModelList = new Model.AMS_PlayList();
        /// <summary>
        /// 
        /// </summary>
        public AMS.Model.AMS_PlayList ModelList
        {
            get { return _ModelList; }
            set { _ModelList = value; }
        }
        private string _StartTime = "";
        /// <summary>
        /// 开始播放时间
        /// </summary>
        public string StartTime
        {
            get { return _StartTime; }
        }
        private string _AllPlayTime = "";
        /// <summary>
        /// 全部播放时间
        /// </summary>
        public string AllPlayTime
        {
            get { return _AllPlayTime; }
        }
        private int _MediaFileCount = 0;
        /// <summary>
        /// 媒体文件数
        /// </summary>
        public int MediaFileCount
        {
            get { return _MediaFileCount; }
        }
        private int _PlayFileCount = 0;
        /// <summary>
        /// 播放文件数
        /// </summary>
        public int PlayFileCount
        {
            get { return _PlayFileCount; }
        }
        private string _MediaFiles = "";
        /// <summary>
        /// 播放文件
        /// </summary>
        public string MediaFiles
        {
            get { return _MediaFiles; }
        }
        public string ReleaseDate
        {
            get { return _ModelList.ReleaseDate.Value.ToLongDateString(); }
        }
        public string EffectDate
        {
            get { return _ModelList.EffectDate.Value.ToLongDateString(); }
        }
        public string EndDate
        {
            get { return _ModelList.EndDate.Value.ToLongDateString(); }
        }

    }
}
