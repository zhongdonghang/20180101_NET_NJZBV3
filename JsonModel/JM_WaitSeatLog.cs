using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_WaitSeatLog
    {
        private string _SeatWaitingID = "";
        /// <summary>
        /// 等待记录编号
        /// </summary>
        public string SeatWaitingID
        {
            get { return _SeatWaitingID; }
            set { _SeatWaitingID = value; }
        }

        private string _CardNo = "";
        /// <summary>
        /// 读者卡号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }
        private string _CardNoB = "";
        /// <summary>
        /// 被等待读者卡号
        /// </summary>
        public string CardNoB
        {
            get { return _CardNoB; }
            set { _CardNoB = value; }
        }

        

        private string _SeatWaitTime;
        /// <summary>
        /// 记录添加时间
        /// </summary>
        public string SeatWaitTime
        {
            get { return _SeatWaitTime; }
            set { _SeatWaitTime = value; }
        }

        string roomNum;
        /// <summary>
        /// 房间编号
        /// </summary>
        public string RoomNum
        {
            get { return roomNum; }
            set { roomNum = value; }
        }
        string roomName;
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName
        {
            get { return roomName; }
            set { roomName = value; }
        }
        string seatNum;
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNum
        {
            get { return seatNum; }
            set { seatNum = value; }
        }
        string _SeatId;
        /// <summary>
        /// 座位ID
        /// </summary>
        public string SeatId
        {
            get { return _SeatId; }
            set { _SeatId = value; }
        }
    }
}
