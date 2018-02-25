using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_BespeakLog
    {
        string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        string _DateTime;
        /// <summary>
        /// 预约日期
        /// </summary>
        public string DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
        string _RoomNum;
        /// <summary>
        /// 所在的房间编号
        /// </summary>
        public string RoomNum
        {
            get { return _RoomNum; }
            set { _RoomNum = value; }
        }
        string _RoomName;
        /// <summary>
        /// 所在的房间名称
        /// </summary>
        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value; }
        }
        string _SeatNum;
        /// <summary>
        /// 预约的座位编号
        /// </summary>
        public string SeatNum
        {
            get { return _SeatNum; }
            set { _SeatNum = value; }
        }
        string _SeatId;
        /// <summary>
        /// 预约的座位ID
        /// </summary>
        public string SeatId
        {
            get { return _SeatId; }
            set { _SeatId = value; }
        }
        bool isValid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }
        /// <summary>
        /// 预约提交时间
        /// </summary>
        public string SubmitDateTime
        {
            get;
            set;
        }

        public string Remark { get; set; }
    }
}
