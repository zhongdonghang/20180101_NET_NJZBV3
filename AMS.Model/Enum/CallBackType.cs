using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    public enum CallBackType
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 硬件问题
        /// </summary>
        Hardware = 0,
        /// <summary>
        /// 软件问题
        /// </summary>
        Software = 1,
        /// <summary>
        /// 广告问题
        /// </summary>
        Advertisement = 2,
        /// <summary>
        /// 未知问题
        /// </summary>
        Unknown = 3,
    }
}
