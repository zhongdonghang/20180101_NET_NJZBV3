using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum Operation
    {
        None=-1,
        /// <summary>
        /// 管理员操作
        /// </summary>
        Admin=0,
        /// <summary>
        /// 读者自己操作
        /// </summary>
        Reader=1,
        /// <summary>
        /// 其他读者操作
        /// </summary>
        OtherReader=2,
        /// <summary>
        /// 监控服务操作
        /// </summary>
        Service=3,
    }
}
