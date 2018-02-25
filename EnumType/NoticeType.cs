using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum NoticeType
    {
        None = -1,
        /// <summary>
        /// 管理员设置读者暂离
        /// </summary>
        ManagerSetShortLeaveWarning = 0,
        /// <summary>
        /// 被其他读者设置暂离
        /// </summary>
        OtherSetShortLeaveWarning = 1,
        /// <summary>
        /// 暂离到时间提醒
        /// </summary>
        ShortLeaveTimeEndWarning = 2,
        /// <summary>
        /// 暂离到时间之前提醒
        /// </summary>
        ShortLeaveTimeEndBeforeWarning = 3,
        /// <summary>
        /// 管理员释放座位提醒
        /// </summary>
        ManagerFreeSetWarning = 4,
        /// <summary>
        /// 被加入黑名单提醒
        /// </summary>
        AddBlacklistWarning = 5,
        /// <summary>
        /// 删除黑名单的提醒
        /// </summary>
        DeleteBlacklistWarning = 6,
        /// <summary>
        /// 座位超时提醒
        /// </summary>
        SeatUsedTimeEnd = 7,
        /// <summary>
        /// 超时之前提醒
        /// </summary>
        SeatUsedTimeEndBefore = 8,
        /// <summary>
        /// 预约到期
        /// </summary>
        BespeakExpiration = 9,
        /// <summary>
        /// 预约到期之前提醒
        /// </summary>
        BespeakExpirationBefore = 10,
        /// <summary>
        /// 座位紧缺提醒
        /// </summary>
        RoomSeatCrampedWarning = 11,
        /// <summary>
        /// 违规提醒
        /// </summary>
        ViolationWarning = 12,
        /// <summary>
        /// 恢复座位（一般为从暂离状态恢复在座状态）
        /// </summary>
        RecoverSeat = 13,
        /// <summary>
        /// 等待座位成功
        /// </summary>
        WaitSeatSuccess = 14,
        /// <summary>
        /// 等待座位失败
        /// </summary>
        WaitSeatFail = 15,
        /// <summary>
        /// 删除违规记录
        /// </summary>
        DeleteViolation = 16
    }

    public static class NoticeTypeValue
    {
        public static string valueOf(NoticeType type)
        {
            switch (type)
            {
                /// <summary>
                /// 管理员设置读者暂离
                /// </summary>
                case NoticeType.ManagerSetShortLeaveWarning:
                    return "被管理员设置暂离";
                /// <summary>
                /// 被其他读者设置暂离
                /// </summary>
                case NoticeType.OtherSetShortLeaveWarning:
                    return "被其他读者设置为暂离";
                /// <summary>
                /// 暂离到时间提醒
                /// </summary>
                case NoticeType.ShortLeaveTimeEndWarning:
                    return "座位使用时间已结束";
                /// <summary>
                /// 暂离到时间之前提醒
                /// </summary>
                case NoticeType.ShortLeaveTimeEndBeforeWarning:
                    return "暂离即将超时";
                /// <summary>
                /// 管理员释放座位提醒
                /// </summary>
                case NoticeType.ManagerFreeSetWarning:
                    return "座位被管理员释放";
                /// <summary>
                /// 被加入黑名单提醒
                /// </summary>
                case NoticeType.AddBlacklistWarning:
                    return "被加入黑名单";
                /// <summary>
                /// 删除黑名单的提醒
                /// </summary>
                case NoticeType.DeleteBlacklistWarning:
                    return "黑名单已过期";
                /// <summary>
                /// 座位超时提醒
                /// </summary>
                case NoticeType.SeatUsedTimeEnd:
                    return "在座超时";
                /// <summary>
                /// 超时之前提醒
                /// </summary>
                case NoticeType.SeatUsedTimeEndBefore:
                    return "座位即将超时";
                /// <summary>
                /// 预约到期
                /// </summary>
                case NoticeType.BespeakExpiration:
                    return "预约超时";
                /// <summary>
                /// 预约到期之前提醒
                /// </summary>
                case NoticeType.BespeakExpirationBefore:
                    return "预约即将超时";
                /// <summary>
                /// 座位紧缺提醒
                /// </summary>
                case NoticeType.RoomSeatCrampedWarning:
                    return "座位紧张";
                /// <summary>
                /// 违规提醒
                /// </summary>
                case NoticeType.ViolationWarning:
                    return "违规";
                /// <summary>
                /// 恢复座位（一般为从暂离状态恢复在座状态）
                /// </summary>
                case NoticeType.RecoverSeat:
                    return "座位已恢复";
                case NoticeType.WaitSeatSuccess:
                    return "等待座位成功";
                case NoticeType.WaitSeatFail:
                    return "等待座位失败";
                default:
                    return "未知";
            }
        }

    }
}
