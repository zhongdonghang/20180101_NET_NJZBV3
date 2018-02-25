using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_SeatBespeakInfo
    {
        private string _seatNo = "";
        private string _seatShortNo = "";
        private List<string> _timeList = new List<string>();
        private string _bespeakDate = "";
        private bool _isUsedSpan = false;
        private bool _isCanSelectTime = false;
        private int _checkBeforeTime = 0;
        private int _checkLastTime = 0;
        private int _checkKeepTime = 0;
        private string _roomName = "";
        private string _roomNo = "";
        private bool _isCanNowBook = true;
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }
        /// <summary>
        /// 短座位号
        /// </summary>
        public string SeatShortNo
        {
            get { return _seatShortNo; }
            set { _seatShortNo = value; }
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
        /// 座位保留时间
        /// </summary>
        public int CheckKeepTime
        {
            get { return _checkKeepTime; }
            set { _checkKeepTime = value; }
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
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        /// <summary>
        /// 是否开启立即预约
        /// </summary>
        public bool IsCanNowBook
        {
            get { return _isCanNowBook; }
            set { _isCanNowBook = value; }
        }
    }
}
