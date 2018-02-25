using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum SlipType
    {
        /// <summary>
        /// 一般选座凭条
        /// </summary>
        General = 0,
        /// <summary>
        /// 预约凭条
        /// </summary>
        Book = 1,
        /// <summary>
        /// 等待座位凭条
        /// </summary>
        Wait = 2
    }
}
