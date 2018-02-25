using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum SeatOutTimeOperation
    {
        None = -1,
        /// <summary>
        /// 释放座位
        /// </summary>
        Leave = 0,
        /// <summary>
        /// 暂离
        /// </summary>
        ShortLeave = 1,
    }
}
