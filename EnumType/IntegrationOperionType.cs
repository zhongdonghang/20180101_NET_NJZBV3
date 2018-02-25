using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum IntegrationOperationType
    {
        None,
        /// <summary>
        /// 预约
        /// </summary>
        Booking,
        /// <summary>
        /// 预约取消
        /// </summary>
        BookingCancel,
        /// <summary>
        /// 预约确认
        /// </summary>
        BookingConfirmation,
        /// <summary>
        /// 预约超时
        /// </summary>
        BookingOutTime,
        /// <summary>
        /// 暂离回来
        /// </summary>
        ComeBack,
        /// <summary>
        /// 删除黑名单记录
        /// </summary>
        DeleteBlacklist,
        /// <summary>
        /// 删除违规记录
        /// </summary>
        DeleteViolationRecords,
        /// <summary>
        /// 读者刷卡离开
        /// </summary>
        Leave,
        /// <summary>
        /// 被管理员设为离开
        /// </summary>
        LeaveByAdmin,
        /// <summary>
        /// 被监控服务设为离开
        /// </summary>
        LeaveByService,
        /// <summary>
        /// 超出选座次数强行选座
        /// </summary>
        OutConutSelectSeat,
        /// <summary>
        /// 重新选座
        /// </summary>
        ReSelectSeat,
        /// <summary>
        /// 在座超时
        /// </summary>
        SeatOutTime,
        /// <summary>
        /// 选座
        /// </summary>
        SelectSeat,
        /// <summary>
        /// 暂时离开
        /// </summary>
        ShortLeave,
        /// <summary>
        /// 暂离超时
        /// </summary>
        ShortLeaveTimeOut,
        /// <summary>
        /// 被管理员设置暂离超时
        /// </summary>
        ShortLeaveByAdminTimeOut,
        /// <summary>
        /// 被读者设置暂离超时
        /// </summary>
        ShortLeaveByReaderTimeOut,
        /// <summary>
        /// 被监控服务设置暂离超时
        /// </summary>
        ShortLeaveByServiceTimeOut,
        /// <summary>
        /// 等待座位
        /// </summary>
        Waiting,
        /// <summary>
        /// 被管理员取消座位等待
        /// </summary>
        WaitingCenaclByAdmin,
    }
}
