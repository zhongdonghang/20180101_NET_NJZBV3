using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    /// <summary>
    /// 终端操作
    /// </summary>
    public enum ClientOperation
    {
        None = -1,
        /// <summary>
        /// 续时
        /// </summary>
        ContinuedTime = 0,
        /// <summary>
        /// 本次离开
        /// </summary>
        Leave = 1,
        /// <summary>
        /// 选座操作
        /// </summary>
        SelectSeat = 2,
        /// <summary>
        /// 重新选座
        /// </summary>
        ReSelectSeat = 3,
        /// <summary>
        /// 暂离
        /// </summary>
        ShortLeave = 4,
        /// <summary>
        /// 返回
        /// </summary>
        Back = 5,
        /// <summary>
        /// 等待座位
        /// </summary>
        WaitSeat = 6,
        /// <summary>
        /// 退出
        /// </summary>
        Exit = 7,
        /// <summary>
        /// 返回上级
        /// </summary>
        SystemBack = 8,
        /// <summary>
        /// 随机选座
        /// </summary>
        RandonSelect = 9,
    }
}
