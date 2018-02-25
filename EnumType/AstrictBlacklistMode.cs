using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum AstrictBlacklistMode
    {
        None,
        /// <summary>
        /// 限制所有阅览室
        /// </summary>
        AllReadingRoom,
        /// <summary>
        /// 只限制本阅览室
        /// </summary> 
        OnlyThisReadingRoom,
    }
}
