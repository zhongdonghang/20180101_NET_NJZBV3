using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    public enum DeviceType
    {
        /// <summary>
        ///空
        /// </summary>
        None = -1,
        /// <summary>
        /// 触摸屏终端
        /// </summary>
        TouchScreen = 0,
        /// <summary>
        /// 台式电脑
        /// </summary>
        PC = -1,
        /// <summary>
        /// 暂离终端
        /// </summary>
        ShortLeaveDevice = 2,
    }
}
