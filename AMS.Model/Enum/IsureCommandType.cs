using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    /// <summary>
    /// 下发命令类型（新）
    /// </summary>
    [Serializable]
    public enum IsureCommandType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = -1,
        /// <summary>
        /// 获取广告
        /// </summary>
        Advertisement = 0,
        /// <summary>
        /// 获取使用情况（截图）
        /// </summary>
        State = 1,
        /// <summary>
        /// 座位使用情况
        /// </summary>
        EnterOutLog = 2,
        /// <summary>
        /// 广告使用情况（优惠券打印及查看次数）
        /// </summary>
        AdvertUsage = 3,
        /// <summary>
        /// 读者信息
        /// </summary>
        ReaderInfo = 4,
    }
}
