using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{

    public class ReadingRoomSeatBespeakState
    {
        private int _CanBespeakAmcount;
        /// <summary>
        /// 可预约的座位数量
        /// </summary>
        public int CanBespeakAmcount
        {
            get { return _CanBespeakAmcount; }
            set { _CanBespeakAmcount = value; }
        }
        private int _BespeakedAmcount;
        /// <summary>
        /// 已经被预约的座位数量
        /// </summary>
        public int BespeakedAmcount
        {
            get { return _BespeakedAmcount; }
            set { _BespeakedAmcount = value; }
        }
    }

    /// <summary>
    /// 当前阅览室座位使用状态
    /// </summary>
    [Serializable]
    public class ReadingRoomSeatUsedState
    {
        private string _LibraryNum;
        private string _LibraryName;  
        private int _SeatAmountShortLeave;
        private int _SeatAmountUsed;
        private int _SeatAmountAll;
        private int _SeatPersonTimes;
        private int _SeatBookCount;
        private int _SeatTemUseCount;
        private string _RoomNum;
        /// <summary>
        /// 所在图书馆编号
        /// </summary>
        public string LibraryNum
        {
            get { return _LibraryNum; }
            set { _LibraryNum = value; }
        }
        /// <summary>
        /// 所在图书馆名字,格式为：校区/图书馆
        /// </summary>
        public string LibraryName
        {
            get { return _LibraryName; }
            set { _LibraryName = value; }
        }
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNum
        {
            get { return _RoomNum; }
            set { _RoomNum = value; }
        }
        private string _RoomName;
        /// <summary>
        ///阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        /// <summary>
        /// 被临时使用的预约座位数目
        /// </summary>
        public int SeatTemUseCount
        {
            get { return _SeatTemUseCount; }
            set { _SeatTemUseCount = value; }
        }
        /// <summary>
        /// 当日进出人次
        /// </summary>
        public int PersonTimes
        {
            get { return _SeatPersonTimes; }
            set { _SeatPersonTimes = value; }
        }
        /// <summary>
        /// 阅览室座位使用状态
        /// </summary>
        public ReadingRoomUsingStatus RoomSeatUsingState
        {
            get
            {
                if (SeatAmountAll == 0)
                {
                    return ReadingRoomUsingStatus.Full;
                }
                double state = double.Parse(SeatAmountUsed.ToString()) / double.Parse(SeatAmountAll.ToString());
                if (state < 0.6)
                    return ReadingRoomUsingStatus.Normal;
                if (state < 1)
                    return ReadingRoomUsingStatus.Crowd;
                if (state == 1)
                    return ReadingRoomUsingStatus.Full;
                return ReadingRoomUsingStatus.Normal;
            }
        }
        /// <summary>
        /// 暂离座位数量
        /// </summary>
        public int SeatAmountShortLeave
        {
            get { return _SeatAmountShortLeave; }
            set { _SeatAmountShortLeave = value; }
        }
        /// <summary>
        /// 使用中的座位数量
        /// </summary>
        public int SeatAmountUsed
        {
            get { return _SeatAmountUsed; }
            set { _SeatAmountUsed = value; }
        }
        /// <summary>
        /// 座位总数
        /// </summary>
        public int SeatAmountAll
        {
            get { return _SeatAmountAll; }
            set { _SeatAmountAll = value; }
        }
        /// <summary>
        /// 空闲座位数
        /// </summary>
        public int SeatAmountFree
        {
            get { return SeatAmountAll - SeatAmountUsed; }
        }
        /// <summary>
        /// 座位预约数目
        /// </summary>
        public int SeatBookingCount
        {
            get { return _SeatBookCount; }
            set { _SeatBookCount = value; }
        }

    }
    /// <summary>
    /// 阅览室使用状态扩展
    /// </summary>
    [Serializable]
    public class ReadingRoomSeatUsedState_Ex : ReadingRoomSeatUsedState
    {
        ReadingRoomInfo _ReadingRoom;

        public ReadingRoomInfo ReadingRoom
        {
            get { return _ReadingRoom; }
            set { _ReadingRoom = value; }
        }
    }
    /// <summary>
    /// 座位预约信息
    /// </summary>
    public class SeatBepeakState
    {
        private int bookSeatCount_All;
        /// <summary>
        /// 提供预约的座位数
        /// </summary>
        public int BespeakSeatAmount
        {
            get
            {
                return bookSeatCount_All;
            }
            set
            {
                bookSeatCount_All = value;
            }
        }
        private int bookSeatCount_Booked = 0;
        /// <summary>
        /// 已被预约的座位数
        /// </summary>
        public int BespeakedSeatAmount
        {
            get
            {
                return bookSeatCount_Booked;
            }
            set
            {
                bookSeatCount_Booked = value;
            }
        }
    }
}
