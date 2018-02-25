using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 进出记录
    /// </summary>
    public class JM_EnterOutLog
    {
        string _id;

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        string enterOutTime;
        /// <summary>
        /// 记录时间
        /// </summary>
        public string EnterOutTime
        {
            get { return enterOutTime; }
            set { enterOutTime = value; }
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
        string enterOutType;
        /// <summary>
        /// 进出类型（枚举ToString()的值）
        /// </summary>
        public string EnterOutState
        {
            get { return enterOutType; }
            set { enterOutType = value; }
        }
        private string remark;
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private string markTime;
        /// <summary>
        /// 加入计时时间
        /// </summary>
        public string MarkTime
        {
            get { return markTime; }
            set { markTime = value; }
        }
    }
}
