using System;

namespace SeatManage.DataModel
{
    /// <summary>
    /// 视频
    /// </summary>
    [Serializable]
    public  class AMS_VideoItem
    {

        /// <summary>
        /// 视频名称
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        /// <summary>
        /// 播放时间
        /// </summary>
        public string PlayTime
        {
            get;
            set;
        }
        /// <summary>
        /// 文件相对路径
        /// </summary>
        public string RelativeUrl
        {
            get;
            set;
        }

        /// <summary>
        /// 播放时长
        /// </summary>
        public int sunTime { set; get; }

    }
}
