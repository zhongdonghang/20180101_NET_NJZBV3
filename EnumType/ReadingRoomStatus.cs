using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum ReadingRoomStatus
    {
        None = -1,
        /// <summary>
        /// 关闭状态
        /// </summary>
        Close = 0,
        /// <summary>
        /// 开启状态
        /// </summary>
        Open = 1,
        /// <summary>
        /// 开馆预处理
        /// </summary>
        BeforeOpen = 2,
        /// <summary>
        /// 闭馆预处理
        /// </summary>
        BeforeClose = 3,
    }
}
