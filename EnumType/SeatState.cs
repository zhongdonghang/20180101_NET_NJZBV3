using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum SeatState
    {
        None, 
        /// <summary>
        /// 有电源
        /// </summary>
        HasPower,
        /// <summary>
        /// 无电源
        /// </summary>
        NoPower
    }
    [Serializable]
    /// <summary>
    /// 座位锁定状态
    /// </summary>
    public enum SeatLockState
    { 
        /// <summary>
        ///  
        /// </summary>
        None=-1,
        /// <summary>
        /// 未锁定
        /// </summary>
        UnLock=0,
        /// <summary>
        /// 已锁定
        /// </summary>
        Locked=1,
        /// <summary>
        /// 座位不存在
        /// </summary>
        NotExists=2

    }
 [Serializable]
    /// <summary>
    /// 座位使用状态
    /// </summary>
    public enum SeatUsedState
    { 
        None,
        /// <summary>
        /// 有电源空闲
        /// </summary>
        HasPowerFree,
        /// <summary>
        /// 有电源在座
        /// </summary>
        HasPowerUsed,
        /// <summary>
        /// 有电源暂离
        /// </summary>
        HasPowerLeave,
        /// <summary>
        /// 无电源空闲
        /// </summary>
        NoPowerFree,
        /// <summary>
        /// 无电源使用中
        /// </summary>
        NoPowerUsed,
        /// <summary>
        /// 无电源暂离
        /// </summary>
        NoPowerLeave,
        /// <summary>
        /// 有电源暂停使用
        /// </summary>
        HasPowerStop,
        /// <summary>
        /// 无电源暂停使用
        /// </summary>
        NoPowerStop,
    }
}
