using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.EnumType
{
    [Serializable]
    public enum IntegrationType
    {
        None,
        /// <summary>
        /// 积分消费
        /// </summary>
        Consume,
        /// <summary>
        /// 积分惩罚
        /// </summary>
        Punish,
        /// <summary>
        /// 积分奖励
        /// </summary>
        Reward,
    }
}
