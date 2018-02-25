using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_SeatMessageStatus
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
        /// 用户姓名
        /// </summary>
        private string _Name = "";
        /// <summary>
        /// 学号
        /// </summary>
        private string _stuedntNo = "";
        /// <summary>
        /// 可进行的操作
        /// </summary>
        private string _canOperation = "";
        /// <summary>
        /// 操作时间
        /// </summary>
        private string _operationTime = "";

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
        /// 用户姓名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        /// <summary>
        /// 学号
        /// </summary>
        public string StuedntNo
        {
            get { return _stuedntNo; }
            set { _stuedntNo = value; }
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
        /// 操作时间
        /// </summary>
        public string OperationTime
        {
            get { return _operationTime; }
            set { _operationTime = value; }
        }
    }
}
