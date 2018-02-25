using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 阅览室座位使用情况
    /// </summary>
    public class JM_RoomSeatUsedState
    {
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNum
        {
            get;
            set;
        }
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string RoomName
        {
            get;
            set;
        }
        /// <summary>
        /// 使用中的座位数量
        /// </summary>
        public int SeatAmountUsed
        {
            get;
            set;
        }
        /// <summary>
        /// 座位总数
        /// </summary>
        public int SeatAmountAll
        {
            get;
            set;
        }
        /// <summary>
        /// 阅览室使用状态
        /// </summary>
        public string RoomSeatUsingState
        {
            get;
            set;
        }
    }
}
