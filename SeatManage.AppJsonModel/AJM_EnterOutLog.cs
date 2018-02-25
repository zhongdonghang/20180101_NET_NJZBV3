using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppJsonModel
{
    public class AJM_EnterOutLog
    {
        private string _id;
        private string _roomNo;
        private string _roomName;
        private string _seatNo;
        private string _seatShortNo;
        private string _enterOutType;
        private string _operator;
        private string _remark; 
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 产生记录时间
        /// </summary>
        public string EnterOutTime { get; set; }

        /// <summary>
        /// 房间编号
        /// </summary>
        public string RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        /// <summary>
        /// 房间名称
        /// </summary>
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
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
        /// 短座位号
        /// </summary>
        public string SeatShortNo
        {
            get { return _seatShortNo; }
            set { _seatShortNo = value; }
        }
        /// <summary>
        /// 进出类型（枚举ToString()的值）
        /// </summary>
        public string EnterOutState
        {
            get { return _enterOutType; }
            set { _enterOutType = value; }
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
    }
}
