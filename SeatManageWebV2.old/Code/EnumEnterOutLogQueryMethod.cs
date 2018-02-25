using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManageWebV2.Code
{
    /// <summary>
    /// 进出记录查询条件类型
    /// </summary>
    public enum EnumEnterOutLogQueryMethod
    {
        None = -1,
        /// <summary>
        /// 学号
        /// </summary>
        CardNo=0,
        /// <summary>
        /// 座位号
        /// </summary>
        SeatNum=1
    }
}