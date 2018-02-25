using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class AMS_VideoMd5Item : AMS_VideoItem
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


        /// <summary>
        /// MD5校验值
        /// </summary>
        public string md5Value { set; get; }

    }
}

