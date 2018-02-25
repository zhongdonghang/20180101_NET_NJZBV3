using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum LeaveBlacklistMode
    {
        None = -1,
        /// <summary>
        /// 自动离开
        /// </summary>
        AutomaticMode = 0,
        /// <summary>
        /// 手动操作
        /// </summary>
        ManuallyMode = 1,
    }
}
