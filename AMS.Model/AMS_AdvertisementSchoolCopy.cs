using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public class AMS_AdvertisementSchoolCopy : AMS_Advertisement
    {
        #region 属性
        
        private int _SchoolID = -1;
        /// <summary>
        /// 学校ID
        /// </summary>
        public int SchoolID
        {
            get { return _SchoolID; }
            set { _SchoolID = value; }
        }
        private string _SchoolName = "";
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set { _SchoolName = value; }
        }
        private int _OriginalID = -1;
        /// <summary>
        /// 正本ID
        /// </summary>
        public int OriginalID
        {
            get { return _OriginalID; }
            set { _OriginalID = value; }
        }
        private bool _IsNew = true;
        /// <summary>
        /// 是否是新的广告
        /// </summary>
        public bool IsNew
        {
            get { return _IsNew; }
            set { _IsNew = value; }
        }
        #endregion
    }
}
