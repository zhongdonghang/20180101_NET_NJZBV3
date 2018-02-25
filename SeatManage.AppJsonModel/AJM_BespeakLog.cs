using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.AppJsonModel
{
    public class AJM_BespeakLog
    {
        string _id;
        string _bookTime; 
        string _roomNo;   
        string _roomName;  
        string _seatNo;
        string _seatShortNo;
        bool _isValid;
        string _operator=Operation.None.ToString();
        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }
        /// <summary>
        /// 预约日期
        /// </summary>
        public string BookTime
        {
            get { return _bookTime; }
            set { _bookTime = value; }
        }

        /// <summary>
        /// 所在的阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        /// <summary>
        /// 所在的阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }
        /// <summary>
        /// 预约的座位编号
        /// </summary>
        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }
        /// <summary>
        /// 预约的座位的短座位号
        /// </summary>
        public string SeatShortNo
        {
            get { return _seatShortNo; }
            set { _seatShortNo = value; }
        }
        /// <summary>
        /// 记录是否有效
        /// </summary>
        public bool IsValid
        {
            get { return _isValid; }
            set { _isValid = value; }
        }
        /// <summary>
        /// 预约提交时间
        /// </summary>
        public string SubmitDateTime
        {
            get;
            set;
        }
        /// <summary>
        /// 预约取消时间
        /// </summary>
        public string CancelTime
        {
            get;
            set;
        }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator {
            get { return _operator; }
            set { _operator = value; }
        }
    }
}
