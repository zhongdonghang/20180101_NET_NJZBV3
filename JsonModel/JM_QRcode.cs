using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 二维码Json
    /// </summary>
    public class JM_QRcode
    {
        private JM_Seat seatInfo = new JM_Seat();
        /// <summary>
        /// 扫描到的座位信息
        /// </summary>
        public JM_Seat SeatInfo
        {
            get { return seatInfo; }
            set { seatInfo = value; }
        }

        private List<JM_CanBespeakSeatInfo> beaspeakSeat = new List<JM_CanBespeakSeatInfo>();
        /// <summary>
        /// 可以预约的座位
        /// </summary>
        public List<JM_CanBespeakSeatInfo> BeaspeakSeat
        {
            get { return beaspeakSeat; }
            set { beaspeakSeat = value; }
        }
    }
    /// <summary>
    /// 可以预约的座位信息
    /// </summary>
    public class JM_CanBespeakSeatInfo
    {
        private string _SeatID;
        /// <summary>
        /// 座位ID
        /// </summary>
        public string SeatID
        {
            get { return _SeatID; }
            set { _SeatID = value; }
        }

        private string _SeatNum;
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNum
        {
            get { return _SeatNum; }
            set { _SeatNum = value; }
        }

        private string _ReadingRoomNo;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }

        private string _BespeakDate;
        /// <summary>
        /// 可预约的日期
        /// </summary>
        public string BespeakDate
        {
            get { return _BespeakDate; }
            set { _BespeakDate = value; }
        }
    }
}
