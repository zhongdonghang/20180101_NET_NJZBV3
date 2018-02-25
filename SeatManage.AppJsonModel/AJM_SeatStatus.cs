using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_SeatStatus : AJM_Seat
    {
        private Dictionary<string, List<string>> _BespeakDate = new Dictionary<string, List<string>>();
        /// <summary>
        /// 是否在使用中
        /// </summary>
        public bool IsUsing { get; set; }
        /// <summary>
        /// 是否暂停使用
        /// </summary>
        public bool IsStopUse { get; set; }
        /// <summary>
        /// 可预约的日期
        /// </summary>
        public Dictionary<string, List<string>> BespeakDate
        {
            get { return _BespeakDate; }
            set { _BespeakDate = value; }
        }
    }
}
