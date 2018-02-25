using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum Printer
    {
        None,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
        /// <summary>
        /// 不正常
        /// </summary>
        Unusual,
        /// <summary>
        /// 没纸
        /// </summary>
        NoPaper,
        /// <summary>
        /// 不存在
        /// </summary>
        NotExist,
    }
}
