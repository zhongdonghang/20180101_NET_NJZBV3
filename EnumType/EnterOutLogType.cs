using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum EnterOutLogType
    {
        None = -1,
        /// <summary>
        /// 标示读者离开
        /// </summary>
        Leave = 0,
        /// <summary>
        /// 标示读者选座
        /// </summary>
        SelectSeat = 1,
        /// <summary>
        /// 读者取消预约
        /// </summary>
        BookingCancel = 3,
        /// <summary>
        /// 读者确认预约
        /// </summary>
        BookingConfirmation = 4,
        /// <summary>
        /// 读者暂离回来
        /// </summary>
        ComeBack = 5,
        /// <summary>
        /// 读者续时
        /// </summary>
        ContinuedTime = 6,
        /// <summary>
        /// 读者重新选座
        /// </summary>
        ReselectSeat = 7,
        /// <summary>
        /// 标示读者暂离中
        /// </summary>
        ShortLeave = 8,
        /// <summary>
        /// 读者等待座位
        /// </summary>
        Waiting = 9,
        /// <summary>
        /// 等待成功
        /// </summary>
        WaitingSuccess = 10,
        /// <summary>
        /// 取消等待
        /// </summary>
        WaitingCancel = 11,
        /// <summary>
        /// 预约等待
        /// </summary>
        BespeakWaiting = 12,
        /// <summary>
        /// 正在计时
        /// </summary>
        Timing = 13,
        /// <summary>
        /// 取消计时
        /// </summary>
        CancelTime=14,

    }

}
