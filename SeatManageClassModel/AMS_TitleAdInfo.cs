using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    public class  TitleAdvertInfo
    {
        int _ID = 0;
        /// <summary>
        /// ID
        /// </summary>
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _TitleAdvertInfo = "";
        /// <summary>
        /// 编号
        /// </summary>
        public string TitleAdvertNo
        { 
            get { return _TitleAdvertInfo; } 
            set { _TitleAdvertInfo = value; }
        } 
        DateTime _EffectDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 生效时间
        /// </summary>
        public DateTime EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        DateTime _EndDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        string _TitleAdvert = null;
        /// <summary>
        /// 广告内容
        /// </summary>
        public string TitleAdvert
        {
            get { return _TitleAdvert; }
            set { _TitleAdvert = value; } 
        }
    }
}
