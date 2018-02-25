using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
namespace SeatManage.ClassModel
{
    [Serializable]
    public class ReaderInfo
    {
        string _CardID = "";
        string _CardNo = "";
        string _Name = "";
        string _Dept = "";
        string _Sex = "";
        string _Type = "";
        string _Pro = "";
        string _Flag = "";
        
        
        int _ChooseSeatTimes = 0;
        /// <summary>
        /// 选座次数
        /// </summary>
        public int ChooseSeatTimes
        {
            get { return _ChooseSeatTimes; }
            set { _ChooseSeatTimes = value; }
        }
        int _ContinuedTimeCount = 0;
        /// <summary>
        /// 续时次数
        /// </summary>
        public int ContinuedTimeCount
        {
            get { return _ContinuedTimeCount; }
            set { _ContinuedTimeCount = value; }
        }

        DateTime _CanContinuedTime = new DateTime();
        /// <summary>
        /// 可以续时的时间
        /// </summary>
        public DateTime CanContinuedTime
        {
            get { return _CanContinuedTime; }
            set { _CanContinuedTime = value; }
        }
    
        List<BlackListInfo> _BlacklistLog = null;
        /// <summary>
        /// 读者的有效黑名单记录
        /// </summary>
        public List<BlackListInfo> BlacklistLog
        {
            get { return _BlacklistLog; }
            set { _BlacklistLog = value; }
        }
        private List<BespeakLogInfo> _BespeakLog = null;
        /// <summary>
        /// 读者的有效预约记录
        /// </summary>
        public List<BespeakLogInfo> BespeakLog
        {
            get { return _BespeakLog; }
            set { _BespeakLog = value; }
        }
        private WaitSeatLogInfo _WaitSeatLog = null;
        /// <summary>
        /// 读者有效的等待记录
        /// </summary>
        public WaitSeatLogInfo WaitSeatLog
        {
            get { return _WaitSeatLog; }
            set { _WaitSeatLog = value; }
        }

        private EnterOutLogInfo _EnterOutLog = null;
        /// <summary>
        /// 当前的进出记录
        /// </summary>
        public EnterOutLogInfo EnterOutLog
        {
            get { return _EnterOutLog; }
            set { _EnterOutLog = value; }
        }

        private ReadingRoomInfo _AtReadingRoom = null;
        /// <summary>
        /// 读者所在的阅览室，如果读者为离开状态，该字段为空。
        /// </summary>
        public ReadingRoomInfo AtReadingRoom
        {
            get { return _AtReadingRoom; }
            set { _AtReadingRoom = value; }
        }

        private List<ReaderNoticeInfo> _NoticeInfo = new List<ReaderNoticeInfo>();
        /// <summary>
        /// 消息提醒
        /// </summary>
        public List<ReaderNoticeInfo> NoticeInfo
        {
            get { return _NoticeInfo; }
            set { _NoticeInfo = value; }
        }

        private List<Seat> _OftenUsedSeat = new List<Seat>();
        /// <summary>
        /// 常用座位
        /// </summary>
        public List<Seat> OftenUsedSeats
        {
            get { return _OftenUsedSeat; }
            set { _OftenUsedSeat = value; }
        }
        /// <summary>
        /// 卡片物理编号
        /// </summary>
        public string CardID
        {
            get { return _CardID; }
            set { _CardID = value; }
        }
        /// <summary>
        /// 证件号
        /// </summary>
        public string CardNo
        {
            get
            {
                return _CardNo;
            }
            set
            {
                _CardNo = value;
            }
        }

        /// <summary>
        /// 读者姓名
        /// </summary>
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }

        /// <summary>
        /// 读者院系
        /// </summary>
        public string Dept
        {
            get
            {
                return _Dept;
            }
            set
            {
                _Dept = value;
            }
        }
        /// <summary>
        /// 读者专业
        /// </summary>
        public string Pro
        {
            get { return _Pro; }
            set { _Pro = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get
            {
                return _Sex;
            }
            set
            {
                _Sex = value;

            }
        }

        /// <summary>
        /// 读者类型
        /// </summary>
        public string ReaderType
        {
            get
            {
                return _Type;
            }
            set
            {
                _Type = value;
            }
        }
        /// <summary>
        /// 读者标示符
        /// </summary>
        public string Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private PecketBookWebSetting _PecketWebSetting = new PecketBookWebSetting();
        /// <summary>
        /// 手机网站设置
        /// </summary>
        public PecketBookWebSetting PecketWebSetting
        {
            get { return _PecketWebSetting; }
            set { _PecketWebSetting = value; }
        }
    }
}
