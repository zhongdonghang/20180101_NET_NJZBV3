using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_SeatNowStatus
    {
        /// <summary>
        /// 座位号
        /// </summary>
        private string _seatNo = "";
        /// <summary>
        /// 短座位号
        /// </summary>
        private string _seatShortNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        private string _roomNo = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        private string _roomName = "";
        /// <summary>
        /// 座位状态
        /// </summary>
        private string _status = "";
        /// <summary>
        /// 可进行的操作
        /// </summary>
        private string _canOperation = "";
        /// <summary>
        /// 可以预约的时间
        /// </summary>
        private List<string> _canBookingDate = new List<string>();

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
        /// 座位状态
        /// </summary>
        public string Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// 可进行的操作
        /// </summary>
        public string CanOperation
        {
            get { return _canOperation; }
            set { _canOperation = value; }
        }

        /// <summary>
        /// 可以预约的时间
        /// </summary>
        public List<string> CanBookingDate
        {
            get { return _canBookingDate; }
            set { _canBookingDate = value; }
        }
    }
}
