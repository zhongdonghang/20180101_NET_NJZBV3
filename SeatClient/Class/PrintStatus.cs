using System;
using System.Collections.Generic;
using System.Text;

namespace SeatClient.Class
{
    public enum PrintStatus
    {
        /// <summary>
        /// 一般选座凭条
        /// </summary>
        General,
        /// <summary>
        /// 预约凭条
        /// </summary>
        Book,
        /// <summary>
        /// 等待座位凭条
        /// </summary>
        Wait

    }
}
