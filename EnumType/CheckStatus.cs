using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum CheckStatus
    {
        /// <summary>
        /// 空值
        /// </summary>
        None = -1,
        /// <summary>
        /// 审核中
        /// </summary>
        Checking = 0,
        /// <summary>
        /// 通过审核
        /// </summary>
        Adopt = 1,
        /// <summary>
        /// 未通过
        /// </summary>
        Failure = 2,
        /// <summary>
        /// 取消审核
        /// </summary>
        Cancel=3,
    }
}
