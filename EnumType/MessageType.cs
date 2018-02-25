using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum MessageType
    {
        /// <summary>
        /// 空类型
        /// </summary>
        None,
        /// <summary>
        /// 预约账号激活（提示）
        /// </summary>
        ActivationSuccess,
        /// <summary>
        /// 自动续时剩余次数不足（提示）
        /// </summary>
        AutoContinueWhenNoCount,
        /// <summary>
        /// 自动续时成功无需再次续时（提示）
        /// </summary>
        AutoContinueWhenNotAgain,
        /// <summary>
        /// 自动续时成功（提示）
        /// </summary>
        AutoContinueWhenSuccess,
        /// <summary>
        /// 取消预约成功,判断是否选择座位（判断）
        /// </summary>
        CancelBespeakSuccess,
        /// <summary>
        /// 取消等待确认（判断）
        /// </summary>
        CancelWaitConfirm,
        /// <summary>
        /// 取消等待成功（提示）
        /// </summary>
        CancleWaitSuccess,
        /// <summary>
        /// 卡无效（提示）
        /// </summary>
        CardDisable,
        /// <summary>
        /// 预约签到的阅览室不在设备的管辖范围内（提示）
        /// </summary>
        CheckBeapeakRoomNotExists,
        /// <summary>
        /// 预约签到确认（判断）
        /// </summary>
        CheckBespeakConfirm,
        /// <summary>
        /// 预约签到时间未到，预约取消确认（判断）
        /// </summary>
        CheckBespeakNotTime,
        /// <summary>
        /// 预约签到成功（提示）
        /// </summary>
        CheckBespeakSuccess,
        /// <summary>
        /// 暂离回来（提示）
        /// </summary>
        ComeBack,
        /// <summary>
        /// 续时次数不足（提示）
        /// </summary>
        ContinueWhenNoCount,
        /// <summary>
        /// 续时成功不用再次续时（提示）
        /// </summary>
        ContinueWhenNotAgain,
        /// <summary>
        /// 续时的时间可以使用到闭馆，没必要续时（提示）
        /// </summary>
        ContinueWhenNotNeed,
        /// <summary>
        /// 续时的时间段未到（提示）
        /// </summary>
        ContinueWhenNotSpan,
        /// <summary>
        /// 续时成功（提示）
        /// </summary>
        ContinueWhenSuccess,
        /// <summary>
        /// 账号注销提示（判断）
        /// </summary>
        DeactivationComfrim,
        /// <summary>
        /// 账号注销成功（提示）
        /// </summary>
        DeactivationSuccess,
        /// <summary>
        /// 系统异常（提示）
        /// </summary>
        Exception,
        /// <summary>
        /// 释放座位（提示）
        /// </summary>
        Leave,
        /// <summary>
        /// 不是本人刷卡（提示）
        /// </summary>
        NotReaderSelf,
        /// <summary>
        /// 打印确认（判断）
        /// </summary>
        PrintConfirm,
        /// <summary>
        /// 阅览室有黑名单（提示）
        /// </summary>
        RoomBlacklist,
        /// <summary>
        /// 阅览室已满（提示）
        /// </summary>
        RoomFull,
        /// <summary>
        /// 阅览室没有开放（提示）
        /// </summary>
        RoomNotOpen,
        /// <summary>
        /// 阅览室不允许此读者类型进入（提示）
        /// </summary>
        RoomNotReaderType,
        /// <summary>
        /// 座位已被预约（提示）
        /// </summary>
        SeatIsBespaeked,
        /// <summary>
        /// 座位被锁定（提示）
        /// </summary>
        SeatIsLocked,
        /// <summary>
        /// 座位暂停使用（提示）
        /// </summary>
        SeatIsStopping,
        /// <summary>
        /// 座位正在被使用（提示）
        /// </summary>
        SeatIsUsing,
        /// <summary>
        /// 座位不存在（提示）
        /// </summary>
        SeatNotExist,
        /// <summary>
        /// 确认选择预约的座位（判断）
        /// </summary>
        SelectBespeakSeatConfrim,
        /// <summary>
        /// 选择即将签到的座位（提示）
        /// </summary>
        SelectBespeakSeatNoTime,
        /// <summary>
        /// 确认座位选择（判断）
        /// </summary>
        SelectSeatConfirm,
        /// <summary>
        /// 选座次数过于频繁（提示）
        /// </summary>
        SelectSeatFrequent,
        /// <summary>
        /// 选座成功（提示）
        /// </summary>
        SelectSeatSuccess,
        /// <summary>
        /// 未刷门禁（提示）
        /// </summary>
        SelectSeatWithoutAccess,
        /// <summary>
        /// 暂离（提示）
        /// </summary>
        ShortLeave,
        /// <summary>
        /// 暂离期间在座超时（提示）
        /// </summary>
        ShortLeaveSeatOverTime,
        /// <summary>
        /// 等待座位确认（判断）
        /// </summary>
        WaitSeatConfirm,
        /// <summary>
        /// 等待座位次数频繁（提示）
        /// </summary>
        WaitSeatFrequent,
        /// <summary>
        /// 等待座位成功（提示）
        /// </summary>
        WaitSeatSuccess,
        /// <summary>
        /// 有座位的情况下等待座位（提示）
        /// </summary>
        WaitSeatWithSeat,

    }
}
