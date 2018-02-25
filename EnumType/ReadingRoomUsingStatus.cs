using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum ReadingRoomUsingStatus
    {
        None,
        /// <summary>
        /// 拥挤
        /// </summary>
        Crowd,
        /// <summary>
        /// 人满
        /// </summary>
        Full,
        /// <summary>
        /// 正常
        /// </summary>
        Normal,
    }
}
