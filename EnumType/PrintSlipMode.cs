using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum PrintSlipMode
    {
        None = -1,
        /// <summary>
        /// 不打印
        /// </summary>
        NotPrint = 0,
        /// <summary>
        /// 自动打印
        /// </summary>
        AutoPrint = 1,
        /// <summary>
        /// 用户选择
        /// </summary>
        UserChoose = 2,
    }
}
