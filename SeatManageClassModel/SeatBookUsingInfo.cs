using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    public class SeatBookUsingInfo
    {
        private Seat _SeatInfo = null;
        /// <summary>
        /// 座位信息
        /// </summary>
        public Seat SeatInfo
        {
            get { return _SeatInfo; }
            set { _SeatInfo = value; }
        }
        private Dictionary<DateTime, Seat> _BookSeatInfo = new Dictionary<DateTime, Seat>();
        /// <summary>
        /// 座位预约信息
        /// </summary>
        public Dictionary<DateTime, Seat> BookSeatInfo
        {
            get { return _BookSeatInfo; }
            set { _BookSeatInfo = value; }
        }
        private ReadingRoomInfo _InReadingRoom = null;
        /// <summary>
        /// 所在阅览室信息
        /// </summary>
        public ReadingRoomInfo InReadingRoom
        {
            get { return _InReadingRoom; }
            set { _InReadingRoom = value; }
        }
        private EnterOutLogInfo _EnterOutLog = null;
        /// <summary>
        /// 进出记录
        /// </summary>
        public EnterOutLogInfo EnterOutLog
        {
            get { return _EnterOutLog; }
            set { _EnterOutLog = value; }
        }
    }
}
