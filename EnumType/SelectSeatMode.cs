using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum SelectSeatMode
    {
        None = -1,
        /// <summary>
        /// 自动选座模式
        /// </summary>
        AutomaticMode = 0,
        /// <summary>
        /// 手动选座模式
        /// </summary>
        ManualMode = 1,
        /// <summary>
        /// 自选模式
        /// </summary>
        OptionalMode = 2,
        /// <summary>
        /// 默认
        /// </summary>
        Default = 3
    }
}
