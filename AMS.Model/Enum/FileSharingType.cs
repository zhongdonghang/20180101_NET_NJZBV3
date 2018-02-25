using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model.Enum
{
    public enum FileSharingType
    {
        None = -1,
        /// <summary>
        /// 读卡器接口
        /// </summary>
        CardReaderInterface = 0,
        /// <summary>
        /// 读者同步接口
        /// </summary>
        ReaderSyncInterface = 1,
        /// <summary>
        /// word，Excel文本
        /// </summary>
        Documentation = 2,
        /// <summary>
        /// 学校资料
        /// </summary>
        SchoolInforation = 3,
        /// <summary>
        /// 数据库
        /// </summary>
        DataBase = 4,
        /// <summary>
        /// 选座系统
        /// </summary>
        SeatManageSystem = 5,
        /// <summary>
        /// 其他
        /// </summary>
        Other = 6,
    }
}
