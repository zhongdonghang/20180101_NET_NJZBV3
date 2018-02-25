using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    /// <summary>
    /// 读者状态
    /// </summary>
    public enum ReaderStatus
    {
        None,
        /// <summary>
        /// 标示读者在座
        /// </summary>
        Seating = 1,
        /// <summary>
        /// 标示读者没有座位
        /// </summary>
        Leave = 3,
        /// <summary>
        /// 标示读者在预约等待中
        /// </summary>
        Booking = 5,
        /// <summary>
        /// 标示读者等待中
        /// </summary>
        Waiting = 6,
        /// <summary>
        /// 标示读者暂离中
        /// </summary>
        ShortLeave = 2,
    }
}
