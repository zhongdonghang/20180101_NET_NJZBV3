using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    public enum CallBackResult
    {
        /// <summary>
        /// 未知
        /// </summary>
        None=-1,
        /// <summary>
        /// 等待解决
        /// </summary>
        Walting=0,
        /// <summary>
        /// 解决中
        /// </summary>
        Solving=1,
        /// <summary>
        /// 完成
        /// </summary>
        Finished=2,
    }
}
