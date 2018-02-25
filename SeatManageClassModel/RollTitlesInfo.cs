using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    public class RollTitlesInfo
    {
        public RollTitlesInfo()
        {}
        private DateTime? _EffectDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? EffectDate
        {
            get { return _EffectDate; }
            set { _EffectDate = value; }
        }
        private DateTime? _EndDate = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }
        private string _Type;
        /// <summary>
        /// 内容
        /// </summary>
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        private string _Num;
        /// <summary>
        /// 编号
        /// </summary>
        public string Num
        {
            get { return _Num; }
            set { _Num = value; }
        }
    }
}
