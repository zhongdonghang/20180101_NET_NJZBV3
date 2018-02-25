using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_BesapeakRoom
    {
        private string _roomNo = "";
        private string _roomName = "";
        private List<AJM_BespeakSeat> _seatList = new List<AJM_BespeakSeat>();
        private List<string> _timeList = new List<string>();
        private string _bespeakDate = "";
        private bool _isUsedSpan = false;
        private bool _isCanSelectTime = false;
        private int _checkBeforeTime = 0;
        private int _checkLastTime = 0;
        private int _checkKeepTime = 0;
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
        /// 预约座位列表
        /// </summary>
        public List<AJM_BespeakSeat> SeatList
        {
            get { return _seatList; }
            set { _seatList = value; }
        }
        /// <summary>
        /// 预约时间列表
        /// </summary>
        public List<string> TimeList
        {
            get { return _timeList; }
            set { _timeList = value; }
        }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string BespeakDate
        {
            get { return _bespeakDate; }
            set { _bespeakDate = value; }
        }
        /// <summary>
        /// 是否只能选择指定时间段
        /// </summary>
        public bool IsUsedSpan
        {
            get { return _isUsedSpan; }
            set { _isUsedSpan = value; }
        }
        /// <summary>
        /// 是否可以选择预约时间段
        /// </summary>
        public bool IsCanSelectTime
        {
            get { return _isCanSelectTime; }
            set { _isCanSelectTime = value; }
        }
        /// <summary>
        /// 提前签到时间
        /// </summary>
        public int CheckBeforeTime
        {
            get { return _checkBeforeTime; }
            set { _checkBeforeTime = value; }
        }
        /// <summary>
        /// 推迟签到时间
        /// </summary>
        public int CheckLastTime
        {
            get { return _checkLastTime; }
            set { _checkLastTime = value; }
        }
        /// <summary>
        /// 立刻预约保留时间
        /// </summary>
        public int CheckKeepTime
        {
            get { return _checkKeepTime; }
            set { _checkKeepTime = value; }
        }
    }
}
