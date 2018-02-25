using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    /// <summary>
    /// 预约状态
    /// </summary>
    [Serializable]
    public enum BookingStatus
    {
        None = -1,
        /// <summary>
        /// 预约取消
        /// </summary>
        Cencaled = 0,
        /// <summary>
        /// 预约确认
        /// </summary>
        Confinmed = 1,
        /// <summary>
        /// 预约等待中
        /// </summary>
        Waiting = 2,
    }
}
