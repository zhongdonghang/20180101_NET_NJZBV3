using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    /// <summary>
    /// 读者库同步方式
    /// </summary>
    public enum StudentSyncMode
    {
        None=-1,
        /// <summary>
        /// 自动同步
        /// </summary>
        OptionalSync = 0,
        /// <summary>
        /// 手动
        /// </summary>
        ManualSync = 1,
    }
}
