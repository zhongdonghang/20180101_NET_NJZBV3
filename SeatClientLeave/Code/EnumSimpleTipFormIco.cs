using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatClientLeave.Code
{
    /// <summary>
    /// 提示框的图标类型
    /// </summary>
    public enum EnumSimpleTipFormIco
    {
        None=0,
        Cry=1,
        Question=2,
        Small=3,
        Warm=4,
    }
    /// <summary>
    /// 离开处理方式
    /// </summary>
    public enum LeaveState
    { 
        None=-1,
        /// <summary>
        /// 选择离开方式
        /// </summary>
        Choose=0,
        //暂时离开
        ShortLeave=1,
        /// <summary>
        /// 释放座位
        /// </summary>
        FreeSeat =2,
        /// <summary>
        /// 离开
        /// </summary>
        ContinuedTime=3,

    }
}
