using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    public class PushMsgInfo
    {
        private string _studentNum = "";
        private EnumType.MsgPushType _msgType = EnumType.MsgPushType.None;
        private DateTime _addTime = DateTime.Parse("2000-1-1");
        private string _message = "";
        private string _operator = "";
        private string _client = "";
        private string _schoolNum = "";
        private string _title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }
        private string _roomName = "";
        private string _seatNum = "";
        //private string _vrCount = "";
        private ViolationRecordsType _vrType = ViolationRecordsType.None;
        private DateTime _leaveDate = DateTime.Parse("2000-1-1");
        private bool _isAutoLeaveBlack = true;
        //private EnterOutLogType _operationType = EnterOutLogType.None;
        //private DateTime _lastDate = DateTime.Parse("2000-1-1");
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNum
        {
            get { return _studentNum; }
            set { _studentNum = value; }
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgPushType MsgType
        {
            get { return _msgType; }
            set { _msgType = value; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime AddTime
        {
            get { return _addTime; }
            set { _addTime = value; }
        }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }
        /// <summary>
        /// 操作者
        /// </summary>
        public string Operator
        {
            get { return _operator; }
            set { _operator = value; }
        }
        /// <summary>
        /// 操作平台/终端
        /// </summary>
        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum
        {
            get { return _schoolNum; }
            set { _schoolNum = value; }
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
        /// 座位编号
        /// </summary>
        public string SeatNum
        {
            get { return _seatNum; }
            set { _seatNum = value; }
        }
        ///// <summary>
        ///// 剩余次数
        ///// </summary>
        //public string VrCount
        //{
        //    get { return _vrCount; }
        //    set { _vrCount = value; }
        //}
        /// <summary>
        /// 违规类型
        /// </summary>
        public ViolationRecordsType VrType
        {
            get { return _vrType; }
            set { _vrType = value; }
        }
        /// <summary>
        /// 离开日期
        /// </summary>
        public DateTime LeaveDate
        {
            get { return _leaveDate; }
            set { _leaveDate = value; }
        }
        /// <summary>
        /// 是否自动释放
        /// </summary>
        public bool IsAutoLeaveBlack
        {
            get { return _isAutoLeaveBlack; }
            set { _isAutoLeaveBlack = value; }
        }
        ///// <summary>
        ///// 操作类型
        ///// </summary>
        //public EnterOutLogType OperationType
        //{
        //    get { return _operationType; }
        //    set { _operationType = value; }
        //}
        ///// <summary>
        ///// 最后时间
        ///// </summary>
        //public DateTime LastDate
        //{
        //    get { return _lastDate; }
        //    set { _lastDate = value; }
        //}


        /// <summary>
        /// 转换成Url请求参数格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            //string str = "SchoolNum=" + SchoolNum + "&StudentNo=" + StudentNum + "&MsgType=" + MsgType.ToString()+"&Time=" + AddTime.ToString("yyyy-MM-dd HH:mm:ss");

            //switch (MsgType)
            //{
            //    case MsgPushType.AdminOperation:str+"&Operator=管理员"+ Operator
            //}

            //SchoolNum=&StudentNo=&MsgType=&Room=&SeatNo=&AddTime=&EndTime=&Days=VRType=&Msg=;
            string str = "SchoolNum=" + SchoolNum + "&StudentNo=" + StudentNum + "&MsgType=" + MsgType.ToString() + "&Room=" + RoomName + "&SeatNo=" + SeatNum + "&AddTime=" + AddTime.ToString("yyyy-MM-dd HH:mm:ss");

            string enddate = "";
            string days = "";
            if (MsgType == MsgPushType.EnterBlack)
            {
                enddate = IsAutoLeaveBlack ? LeaveDate.ToString("yyyy-MM-dd HH:mm:ss") : "管理员手动释放";
                days = IsAutoLeaveBlack ? (LeaveDate - AddTime).Days.ToString() : "N/A";
            }
            if (MsgType == MsgPushType.LeaveVrBlack)
            {
                days = (LeaveDate - AddTime).Days.ToString();
            }
            string VRType = "";
            if (MsgType == MsgPushType.EnterVR || MsgType == MsgPushType.LeaveVrBlack && VrType != ViolationRecordsType.None)
            {

                switch (VrType)
                {
                    case ViolationRecordsType.BookingTimeOut:
                        VRType = "预约超时";
                        break;
                    case ViolationRecordsType.CancelWaitByAdmin:
                        VRType = "被管理员取消等待";
                        break;
                    case ViolationRecordsType.LeaveByAdmin:
                        VRType = "被管理员释放座位";
                        break;
                    case ViolationRecordsType.LeaveNotReadCard:
                        VRType = "离开没有释放座位";
                        break;
                    case ViolationRecordsType.SeatOutTime:
                        VRType = "在座超时";
                        break;
                    case ViolationRecordsType.ShortLeaveByAdminOutTime:
                        VRType = "被管理员设置暂离超时";
                        break;
                    case ViolationRecordsType.ShortLeaveByReaderOutTime:
                        VRType = "被其他读者设置暂离超时";
                        break;
                    case ViolationRecordsType.ShortLeaveByServiceOutTime:
                        VRType = "暂离超时";
                        break;
                    case ViolationRecordsType.ShortLeaveOutTime:
                        VRType = "暂离超时";
                        break;
                }
            }


            return str + "&EndTime=" + enddate + "&Days=" + days + "VRType=" + VRType + "&Msg=" + Message;
        }

    }
}
