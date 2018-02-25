using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.AppJsonModel
{
    public class AJM_ReadingRoomState
    {
        private string _roomNo = "";
        private string _roomName = "";
        private int _seatAmount_All = 0;
        private int _seatAmount_Used = 0;
        private int _seatAmount_Bespeak = 0;
        private int _seatAmount_last = 0;
        private string _openCloseState=EnumType.ReadingRoomStatus.None.ToString();
        private bool _isCanBookNowSeat = false;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }
        /// <summary>
        /// 阅览室座位总数
        /// </summary>
        public int SeatAmount_All
        {
            get { return _seatAmount_All; }
            set { _seatAmount_All = value; }
        }
        /// <summary>
        /// 阅览室已被使用座位数
        /// </summary>
        public int SeatAmount_Used
        {
            get { return _seatAmount_Used; }
            set { _seatAmount_Used = value; }
        }
        /// <summary>
        /// 阅览室已被预约座位数
        /// </summary>
        public int SeatAmount_Bespeak
        {
            get { return _seatAmount_Bespeak; }
            set { _seatAmount_Bespeak = value; }
        }
        /// <summary>
        /// 阅览室开闭状态
        /// </summary>
        public string OpenCloseState
        {
            get { return _openCloseState; }
            set { _openCloseState = value; }
        }
        /// <summary>
        /// 剩余座位
        /// </summary>
        public int SeatAmount_Last
        {
            get { return _seatAmount_last; }
            set { _seatAmount_last = value; }
        }
        /// <summary>
        /// 是否可以预约当天座位
        /// </summary>
        public bool IsCanBookNowSeat
        {
            get { return _isCanBookNowSeat; }
            set { _isCanBookNowSeat = value; }
        }
    }
}
