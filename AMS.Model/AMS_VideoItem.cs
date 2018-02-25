using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    [Serializable]
    public class AMS_VideoItem
    {
        private string _Name = "";
        /// <summary>
        /// 文件名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _PlayTime = "";
        /// <summary>
        /// 播放开始时间
        /// </summary>
        public string PlayTime
        {
            get { return _PlayTime; }
            set { _PlayTime = value; }
        }
        private string _ReRelativeUrl = "";
        /// <summary>
        /// 文件路径
        /// </summary>
        public string ReRelativeUrl
        {
            get { return _ReRelativeUrl; }
            set { _ReRelativeUrl = value; }
        }
        private int _SunTime = 0;
        /// <summary>
        /// 播放时长（秒）
        /// </summary>
        public int SunTime
        {
            get { return _SunTime; }
            set { _SunTime = value; }
        }
        private string _MD5Value = "";
        /// <summary>
        /// MD5码
        /// </summary>
        public string MD5Value
        {
            get { return _MD5Value; }
            set { _MD5Value = value; }
        }
       
    }
}
