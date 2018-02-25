using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    public enum MessageBoxType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = -1,
        /// <summary>
        /// 消息
        /// </summary>
        Informatsion = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 失败
        /// </summary>
        Error = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 3,
        /// <summary>
        /// 询问
        /// </summary>
        Ask = 4,

    }
}
