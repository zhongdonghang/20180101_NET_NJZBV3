using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    public enum FunPageType
    {
        /// <summary>
        /// 空
        /// </summary>
        None = -1,
        /// <summary>
        /// 功能页面
        /// </summary>
        FunctionPage = 0,
        /// <summary>
        /// 编辑页面
        /// </summary>
        EditPage = 1,
        /// <summary>
        /// 消息页面
        /// </summary>
        MessagePage = 2,
    }
}
