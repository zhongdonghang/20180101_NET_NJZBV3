using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 读者使用座位状态信息
    /// </summary>
   public class JM_ReaderUsedSeatStateInfo
    {
       /// <summary>
       /// 在座
       /// </summary>
       public const string AtSeat = "AtSeat";
       /// <summary>
       /// 暂离
       /// </summary>
       public const string ShortLeave = "ShortLeave";
       /// <summary>
       /// 离开
       /// </summary>
       public const string Leave = "Leave";

        private string seatNo;//座位号
        private string readingRoomName;//阅览室编号
        private bool isContinuedTime;//是否为续时模式
        private string beginTime;//开始使用时间 
        private double? usedTimeLength;//当前使用时长（秒）
        private double? canUsedTimeLength;//剩余可用时长（秒）。
        private double? shortLeaveTimeLength;//座位保留剩余时长（秒）

        private string state;
       /// <summary>
       /// 当前状态：三种状态：在座、暂离、离开
       /// </summary>
        public string State
        {
            get { return state; }
            set { state = value; }
        }

       /// <summary>
        /// 座位号
       /// </summary>
        public string SeatNo
        {
            get { return seatNo; }
            set { seatNo = value; }
        }
       /// <summary>
        /// 阅览室编号
       /// </summary>
        public string ReadingRoomName
        {
            get { return readingRoomName; }
            set { readingRoomName = value; }
        }
       /// <summary>
        /// 是否限制使用时长模式
       /// </summary>
        public bool IsSeatUsedTimeLimit
        {
            get { return isContinuedTime; }
            set { isContinuedTime = value; }
        }
       /// <summary>
        /// 开始使用时间 
       /// </summary>
        public string BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
       /// <summary>
        /// 当前使用时长（秒）
       /// </summary>
        public double? UsedTimeLength
        {
            get { return usedTimeLength; }
            set { usedTimeLength = value; }
        }
       /// <summary>
        /// 剩余可用时长（秒）。
       /// </summary>
        public double? CanUsedTimeLength
        {
            get { return canUsedTimeLength; }
            set { canUsedTimeLength = value; }
        }
       /// <summary>
        /// 座位保留剩余时长（秒）
       /// </summary>
        public double? ShortLeaveTimeLength
        {
            get { return shortLeaveTimeLength; }
            set { shortLeaveTimeLength = value; }
        }


    }
}
