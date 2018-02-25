using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_ViolationRecordsLogInfo
    {
        private string _studentNo = "";
        private string _seatNo = "";
        private string _roomNo= "";
        private string _roomName = "";
        private string _enterOutTime = "";
        private string _violateType = "";
        /// <summary>
        /// 卡号
        /// </summary>
        public string StudentNo
        {
            get { return _studentNo; }
            set { _studentNo = value; }
        }
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }
        /// <summary>
        /// 阅览室ID
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
        /// 违规时间
        /// </summary>
        public string EnterOutTime
        {
            get { return _enterOutTime; }
            set { _enterOutTime = value; }
        }
        /// <summary>
        /// 标注
        /// </summary>
        public string Remark
        {
            get { return _violateType; }
            set { _violateType = value; }
        }
    }
}
