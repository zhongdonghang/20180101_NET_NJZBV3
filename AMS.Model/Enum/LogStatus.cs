using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    /// <summary>
    /// 记录状态
    /// </summary>
    public enum LogStatus
    {
        None=-1,
        /// <summary>
        /// 无效
        /// </summary>
        Fail=0,
        /// <summary>
        /// 有效
        /// </summary>
       Valid=1
    }
}
