using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{ 
    /// <summary>
    /// 阅览室图标类型
    /// </summary>
    [Serializable]
    public enum RoomICOType
    {
        None,
        /// <summary>
        /// 大图标
        /// </summary>
        Big,
        /// <summary>
        /// 小图标
        /// </summary>
        Small
    }
}
