using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum ClientType
    {
        /// <summary>
        /// 未知设备
        /// </summary>
        None = -1,
        /// <summary>
        /// 选座终端
        /// </summary>
        SeatClient = 0,
        /// <summary>
        /// 离开终端
        /// </summary>
        LeaveClient = 1,
        /// <summary>
        /// 预约网站
        /// </summary>
        PCWeb = 2,
        /// <summary>
        /// 手机预约网站
        /// </summary>
        PocketWeb = 3,
        /// <summary>
        /// 微信
        /// </summary>
        WeiChar = 4,
        /// <summary>
        /// 手机APP
        /// </summary>
        PocketApp = 5,
        /// <summary>
        /// 门禁
        /// </summary>
        Access = 6,
    }
}
