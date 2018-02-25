using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    /// <summary>
    /// 座位
    /// </summary>
    public class Seat : RoomLayoutElement
    {
        private bool havePower = false;
        private string _ReadingRoomNum = "";
        private ReadingRoomInfo _ReadingRoom = null;
        /// <summary>
        /// 阅览室
        /// </summary>
        public ReadingRoomInfo ReadingRoom
        {
            get { return _ReadingRoom; }
            set { _ReadingRoom = value; }
        }
        /// <summary>
        /// 所在阅览室编号
        /// </summary>
        public string ReadingRoomNum
        {
            get { return _ReadingRoomNum; }
            set { _ReadingRoomNum = value; }
        }
        /// <summary>
        /// 是否有电源
        /// </summary>
        public bool HavePower
        {
            get
            {
                return havePower;
            }
            set
            {
                havePower = value;
            }
        }
        private DateTime Lockedtime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 锁定时间
        /// </summary>
        public DateTime LockedTime
        {
            get
            {
                return Lockedtime;
            }
            set
            {
                Lockedtime = value;
            }
        }
        private bool isLocked=false;
        /// <summary>
        /// 是否被锁定
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return isLocked;
            }
            set
            {
                isLocked = value;
            }
        }
        private SeatManage.EnumType.EnterOutLogType _SeatUsedState = EnumType.EnterOutLogType.Leave;
        /// <summary>
        /// 使用状态
        /// </summary>
        public SeatManage.EnumType.EnterOutLogType SeatUsedState
        {
            get { return _SeatUsedState; }
            set { _SeatUsedState = value; }
        }
        private string shortseatNo="";
        /// <summary>
        /// 显示的座位号
        /// </summary>
        public string ShortSeatNo 
        {
            get
            {
                return shortseatNo;
            }
            set
            {
                shortseatNo = value;
            }
        }

        string _UserCardNo = "";
        /// <summary>
        /// 使用者学号
        /// </summary>
        public string UserCardNo
        {
            get { return _UserCardNo; }
            set { _UserCardNo = value; }
        }
        string _UserName = "";

        /// <summary>
        /// 使用者姓名
        /// </summary>
        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        DateTime _BeginUsedTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 使用开始时间
        /// </summary>
        public DateTime BeginUsedTime
        {
            get { return _BeginUsedTime; }
            set { _BeginUsedTime = value; }
        }
       
        private string seatNo="";
        /// <summary>
        /// 完整的座位号
        /// </summary>
        public string SeatNo
        {
            get { return seatNo; }
            set { seatNo = value; }
        }

        private bool canBeBook= false;
        /// <summary>
        /// 是否提供预约
        /// </summary>
        public bool CanBeBespeak
        {
            get { return canBeBook; }
            set { canBeBook = value; }
        }
        private DateTime markTime;
        /// <summary>
        /// 计时时间
        /// </summary>
        public DateTime MarkTime
        {
            get { return markTime; }
            set { markTime = value; }
        }
        private bool isSuspended = false;
        /// <summary>
        /// 是否暂停使用
        /// </summary>
        public bool IsSuspended
        {
            get { return isSuspended; }
            set { isSuspended = value; }
        }
        private List<DateTime> _CanBespeakSpan = new List<DateTime>();
        /// <summary>
        /// 可预约的时间段
        /// </summary>
        public List<DateTime> CanBespeakSpan
        {
            get { return _CanBespeakSpan; }
            set { _CanBespeakSpan = value; }
        }
        public string CanBespeakStr
        {
            get
            {
                string str = "";
                foreach (DateTime dt in _CanBespeakSpan)
                {
                    str += dt.ToShortTimeString() + ";";
                }
                return str;
            }
        }
        private bool _IsBooking = false;
        /// <summary>
        /// 是否被预约
        /// </summary>
        public bool IsBooking
        {
            get { return _IsBooking; }
            set { _IsBooking = value; }
        }
        
    }
}
