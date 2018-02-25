using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum UserType
    {
        None=-1,
        /// <summary>
        /// 标示管理员
        /// </summary>
        Admin=0,
        /// <summary>
        /// 标示读者
        /// </summary>
        Reader=1,
    }
}
