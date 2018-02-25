using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_UserNowStatus
    {
        /// <summary>
        /// 学号
        /// </summary>
        private string _studentNum = "";
        /// <summary>
        /// 姓名
        /// </summary>
        private string _name = "";
        /// <summary>
        /// 所在阅览室
        /// </summary>
        private string _inRoom = "";
        /// <summary>
        /// 座位编号
        /// </summary>
        private string _seatNum = "";
        /// <summary>
        /// 状态
        /// </summary>
        private string _status = "";
        /// <summary>
        /// 可进行的操作
        /// </summary>
        private string _canOperation = "";
        /// <summary>
        /// 当前状态信息
        /// </summary>
        private string _nowStatusRemark = "";
        /// <summary>
        /// 操作时间
        /// </summary>
        private string _time = "";
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNum
        {
            get { return _studentNum; }
            set { _studentNum = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 所在阅览室
        /// </summary>
        public string InRoom
        {
            get { return _inRoom; }
            set { _inRoom = value; }
        }

        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNum
        {
            get { return _seatNum; }
            set { _seatNum = value; }
        }

        /// <summary>
        /// 状态
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
        /// 当前状态信息
        /// </summary>
        public string NowStatusRemark
        {
            get { return _nowStatusRemark; }
            set { _nowStatusRemark = value; }
        }

        /// <summary>
        /// 操作时间
        /// </summary>
        public string Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
