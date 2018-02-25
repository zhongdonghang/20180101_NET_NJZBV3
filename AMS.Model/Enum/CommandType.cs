using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    /// <summary>
    /// 下发命令类型
    /// </summary>
    [Serializable]
    public enum CommandType
    {
        /// <summary>
        /// 未知类型
        /// </summary>
        None = -1,
        /// <summary>
        /// 播放列表
        /// </summary>
        Playlist = 0,
        /// <summary>
        /// 打印模版
        /// </summary>
        PrintTemplate = 1,
        /// <summary>
        /// 优惠券广告
        /// </summary>
        SlipCustomer = 2,
        /// <summary>
        /// 冠名
        /// </summary>
        TitleAd = 3,
        /// <summary>
        /// 硬广
        /// </summary>
        HardAd = 4,
        /// <summary>
        /// 程序更新5
        /// </summary>
        ProgramUpgrade = 5,
        /// <summary>
        /// 截图
        /// </summary>
        Caputre = 6,
        /// <summary>
        /// 滚动
        /// </summary>
        RollTitles = 7,
    }
}
