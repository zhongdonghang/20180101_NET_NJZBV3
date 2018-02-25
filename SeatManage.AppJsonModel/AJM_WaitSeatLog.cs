using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_WaitSeatLog
    {
        private string _studentNo_A;
        private string _studentNo_B;
        private string _roomNo;
        private string _roomName;
        private string _seatNo;
        private string _seatShortNo;
        private string _seatWaitId;
        private string _seatWaitTime;
        /// <summary>
        /// 原座位读者
        /// </summary>
        public string StudentNo_A
        {
            get { return _studentNo_A; }
            set { _studentNo_A = value; }
        }
        /// <summary>
        /// 等待座位读者
        /// </summary>
        public string StudentNo_B
        {
            get { return _studentNo_B; }
            set { _studentNo_B = value; }
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
        /// 完整座位号
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
        /// 等待座位记录ID
        /// </summary>
        public string SeatWaitId
        {
            get { return _seatWaitId; }
            set { _seatWaitId = value; }
        }
        /// <summary>
        /// 等待座位时间
        /// </summary>
        public string SeatWaitTime
        {
            get { return _seatWaitTime; }
            set { _seatWaitTime = value; }
        }
    }
}
