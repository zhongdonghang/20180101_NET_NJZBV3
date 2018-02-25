using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    /// <summary>
    /// 命令执行结果
    /// </summary>
    public enum CommandHandleResult
    {
        /// <summary>
        /// 未知
        /// </summary>
        None=-1,
        /// <summary>
        /// 等待获取
        /// </summary>
        Wait=0,
        /// <summary>
        /// 正在获取
        /// </summary>
        Getting=1,
        /// <summary>
        /// 成功获取
        /// </summary>
        Success=2,
        /// <summary>
        /// 获取失败
        /// </summary>
        Failed=3
    }
}
