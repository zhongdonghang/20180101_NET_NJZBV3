using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum ViolationRecordsType
    {
        None = -1,
        /// <summary>
        /// 预约超时
        /// </summary>
        BookingTimeOut = 0,
        /// <summary>
        /// 被管理员设置离开
        /// </summary>
        LeaveByAdmin = 1,
        /// <summary>
        /// 在座超时
        /// </summary>
        SeatOutTime = 2,
        /// <summary>
        /// 暂离超时
        /// </summary>
        ShortLeaveOutTime = 3,
        /// <summary>
        /// 被管理员设为暂离超时
        /// </summary>
        ShortLeaveByAdminOutTime = 4,
        /// <summary>
        /// 被读者设为暂离超时
        /// </summary>
        ShortLeaveByReaderOutTime = 5,
        /// <summary>
        /// 被监控服务设为暂离超时
        /// </summary>
        ShortLeaveByServiceOutTime = 6,
        /// <summary>
        /// 被管理员取消等待
        /// </summary>
        CancelWaitByAdmin = 7,
        /// <summary>
        /// 离开未刷卡
        /// </summary>
        LeaveNotReadCard = 8,
    }
}
