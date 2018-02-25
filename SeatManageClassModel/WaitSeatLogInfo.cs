
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 读者等待记录
    /// </summary>
    [Serializable]
    public class WaitSeatLogInfo
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
        private string _ReadingRoomNo;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; }
        }

        private int _EnterOutLogID =-1;
        /// <summary>
        /// 相关进出记录编号
        /// </summary>
        public int EnterOutLogID
        {
            get { return _EnterOutLogID; }
            set { _EnterOutLogID = value; }
        }

        private DateTime _SeatWaitTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 记录添加时间
        /// </summary>
        public DateTime SeatWaitTime
        {
            get { return _SeatWaitTime; }
            set { _SeatWaitTime = value; }
        }
        private DateTime _StatsChangeTime = DateTime.Parse("1900-1-1");
        /// <summary>
        /// 状态修改时间
        /// </summary>
        public DateTime StatsChangeTime
        {
            get { return _StatsChangeTime; }
            set { _StatsChangeTime = value; }
        }
        private EnterOutLogType _WaitingState = EnterOutLogType.Waiting;
        /// <summary>
        /// 等待状态
        /// </summary>
        public EnterOutLogType WaitingState
        {
            get { return _WaitingState; }
            set { _WaitingState = value; }
        }

        private Operation _OperateType = Operation.Reader;
        /// <summary>
        /// 操作者
        /// </summary>
        public Operation OperateType
        {
            get { return _OperateType; }
            set { _OperateType = value; }
        }

        private LogStatus _NowState = LogStatus.Valid;
        /// <summary>
        /// 标示是否是有效记录
        /// </summary>
        public LogStatus NowState
        {
            get { return _NowState; }
            set { _NowState = value; }
        }

        private string _SeatNo = "";
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; }
        }
        private EnterOutLogInfo _EnterOutLog = new EnterOutLogInfo();
        /// <summary>
        /// 相关进出记录
        /// </summary>
        public EnterOutLogInfo EnterOutLog
        {
            get { return _EnterOutLog; }
            set { _EnterOutLog = value; }
        }
    }
}
