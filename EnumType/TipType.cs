using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    /// <summary>
    /// 提示类型
    /// </summary>
    public enum TipType
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,
        /// <summary>
        /// 选座频繁
        /// </summary>
        SelectSeatFrequent = 1,
        /// <summary>
        /// 黑名单限制
        /// </summary>
        IsBlacklist = 2,
        /// <summary>
        /// 选座结束提示
        /// </summary>
        SelectSeatResult = 3,
        /// <summary>
        /// 暂时离开
        /// </summary>
        ShortLeave = 4,
        /// <summary>
        /// 释放座位
        /// </summary>
        Leave = 5,
        /// <summary>
        /// 暂离回来
        /// </summary>
        ComeToBack = 6,
        /// <summary>
        /// 座位锁定
        /// </summary>
        SeatLocking = 7,
        /// <summary>
        /// 座位不存在
        /// </summary>
        SeatNotExists = 8,
        /// <summary>
        /// 座位正在使用中
        /// </summary>
        SeatUsing = 9,
        /// <summary>
        /// 阅览室关闭中
        /// </summary>
        ReadingRoomClosing = 10,
        /// <summary>
        /// 座位等待成功提示
        /// </summary>
        WaitSeatSuccess = 11,
        /// <summary>
        /// 等待操作频繁
        /// </summary>
        WaitSeatFrequent = 12,
        /// <summary>
        /// 读者类型不符合设置提示
        /// </summary>
        ReaderTypeInconformity = 13,
        /// <summary>
        /// 预约确认成功
        /// </summary>
        BespeatSeatConfirmSuccess = 14,
        /// <summary>
        /// 预约确认失败
        /// </summary>
        BespeatSeatConfirmFild = 15,
        /// <summary>
        /// 续时提示
        /// </summary>
        ContinuedTime = 16,
        /// <summary>
        /// 预约的阅览室不不是该触摸屏所管理的阅览室
        /// </summary>
        BeapeatRoomNotExists = 17,
        /// <summary>
        /// 在座设置等待提示
        /// </summary>
        WaitSeatWithSeat = 18,
        /// <summary>
        /// 续时次数不足提示
        /// </summary>
        ContinuedTimeNoCount = 19,
        /// <summary>
        /// 没有到续时时间
        /// </summary>
        ContinuedTimeNotTime = 20,
        /// <summary>
        /// 阅览室人满
        /// </summary>
        ReadingRoomFull = 21,
        /// <summary>
        /// 自动续时次数不足提示
        /// </summary>
        AutoContinuedTimeNoCount = 22,
        /// <summary>
        /// 自动续时
        /// </summary>
        AutoContinuedTime = 23,
        /// <summary>
        /// 暂离期间在座超时
        /// </summary>
        ShortLeaveSeatOverTime = 24,
        /// <summary>
        /// 系统异常提示
        /// </summary>
        Exception = 25,
        /// <summary>
        /// 无需续时
        /// </summary>
        ContinuedTimeWithout = 26,
        /// <summary>
        /// 预约激活成功
        /// </summary>
        ActivationSuccess = 29,
        /// <summary>
        /// 预约取消窗口
        /// </summary>
        BookConfirmWarn = 30,
        /// <summary>
        /// 预约取消成功
        /// </summary>
        BookCancelSuccess = 31,
        /// <summary>
        /// 预约注销询问
        /// </summary>
        CancelActivationWarn = 32,
        /// <summary>
        /// 预约激活取消成功
        /// </summary>
        CancelActivationSuccess = 33,
        /// <summary>
        /// 预约激活刷卡提示
        /// </summary>
        ActivationReadCard = 34,
        /// <summary>
        /// 座位等待提示
        /// </summary>
        SetShortWarning = 35,
        /// <summary>
        /// 选座确认界面
        /// </summary>
        SelectSeatConfinmed = 36,
        /// <summary>
        /// 预约取消确认界面
        /// </summary>
        WaitSeatCancelWarn = 37,
        /// <summary>
        /// 预约取消
        /// </summary>
        WaitSeatCancel = 38,
        /// <summary>
        /// 座位暂停使用
        /// </summary>
        SeatStopping = 39,
        /// <summary>
        /// 选择被预约的座位
        /// </summary>
        SelectBookingSeatWarn = 40,
        /// <summary>
        /// 座位已被预约
        /// </summary>
        IsBookingSeat = 41,
        /// <summary>
        /// 非本人卡片（没刷门禁）
        /// </summary>
        NotReaderSelf = 42,
        /// <summary>
        /// 打印确认
        /// </summary>
        PrintConfinmed=43,
        /// <summary>
        /// 续时失败，下个时段已被预约
        /// </summary>
        ContinueWithBookLog = 44,
        /// <summary>
        /// 卡片无效
        /// </summary>
        CardDisable=45,
        /// <summary>
        /// 打印确认
        /// </summary>
        PrintConfIrm=46,
    }
}
