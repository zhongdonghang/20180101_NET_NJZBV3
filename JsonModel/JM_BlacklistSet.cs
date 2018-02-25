using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_BlacklistSet
    {
        bool isUsed;
        /// <summary>
        /// 是否启用黑名单
        /// </summary>
        public bool IsUsed
        {
            get { return isUsed; }
            set { isUsed = value; }
        }

        bool isUseViolationBookingTimeOut;
        /// <summary>
        /// 是否启用预约超时违规
        /// </summary>
        public bool IsUseViolationBookingTimeOut
        {
            get { return isUseViolationBookingTimeOut; }
            set { isUseViolationBookingTimeOut = value; }
        }
        bool isUseViolationLeaveByAdmin;
        /// <summary>
        /// 是否启用被管理员释放座位的违规
        /// </summary>
        public bool IsUseViolationLeaveByAdmin
        {
            get { return isUseViolationLeaveByAdmin; }
            set { isUseViolationLeaveByAdmin = value; }
        }
        bool isUseViolationSeatOutTime;
        /// <summary>
        /// 是否启用在座超时的违规
        /// </summary>
        public bool IsUseViolationSeatOutTime
        {
            get { return isUseViolationSeatOutTime; }
            set { isUseViolationSeatOutTime = value; }
        }
        bool isUseViolationShortLeaveByAdminOutTime;
        /// <summary>
        /// 是否启用被管理员暂离暂离超时的违规
        /// </summary>
        public bool IsUseViolationShortLeaveByAdminOutTime
        {
            get { return isUseViolationShortLeaveByAdminOutTime; }
            set { isUseViolationShortLeaveByAdminOutTime = value; }
        }
        bool isUseViolationShortLeaveByReaderOutTime;
        /// <summary>
        /// 是否启用被其他读者设置暂离暂离超时的违规
        /// </summary>
        public bool IsUseViolationShortLeaveByReaderOutTime
        {
            get { return isUseViolationShortLeaveByReaderOutTime; }
            set { isUseViolationShortLeaveByReaderOutTime = value; }
        }
        //bool isUseViolationShortLeaveByServiceOutTime;
        ///// <summary>
        ///// 是否启用
        ///// </summary>
        //public bool IsUseViolationShortLeaveByServiceOutTime
        //{
        //    get { return isUseViolationShortLeaveByServiceOutTime; }
        //    set { isUseViolationShortLeaveByServiceOutTime = value; }
        //}
        bool isUseViolationShortLeaveOutTime;
        /// <summary>
        /// 是否启用暂离超时的违规
        /// </summary>
        public bool IsUseViolationShortLeaveOutTime
        {
            get { return isUseViolationShortLeaveOutTime; }
            set { isUseViolationShortLeaveOutTime = value; }
        }
        //bool isUseViolationCancelWaitByAdmin;
        ///// <summary>
        ///// 是否启用被
        ///// </summary>
        //public bool IsUseViolationCancelWaitByAdmin
        //{
        //    get { return isUseViolationCancelWaitByAdmin; }
        //    set { isUseViolationCancelWaitByAdmin = value; }
        //}

        int violateCountWithEnterBlacklist;
        /// <summary>
        /// 进入黑名单的违规次数
        /// </summary>
        public int ViolateCountWithEnterBlacklist
        {
            get { return violateCountWithEnterBlacklist; }
            set { violateCountWithEnterBlacklist = value; }
        }
        string leaveBlacklistModel;
        /// <summary>
        /// 离开黑名单的模式
        /// </summary>
        public string LeaveBlacklistModel
        {
            get { return leaveBlacklistModel; }
            set { leaveBlacklistModel = value; }
        }
        int autoLeaveDays;
        /// <summary>
        /// 自动离开黑名单的天数
        /// </summary>
        public int AutoLeaveDays
        {
            get { return autoLeaveDays; }
            set { autoLeaveDays = value; }
        }
        int violateFailDays;
        /// <summary>
        /// 违规记录失效时间
        /// </summary>
        public int ViolateFailDays
        {
            get { return violateFailDays; }
            set { violateFailDays = value; }
        }

    }

}
