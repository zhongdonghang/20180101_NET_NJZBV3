using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    /// 图书馆座位信息
    /// 以图书馆为单位，下面包含的阅览室座位使用情况
    /// </summary>
   public class JM_LibrarySeatsInfo
    {
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string LibraryNum
        {
            get;
            set;
        }
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string LibraryName
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
        List<JM_RoomSeatUsedState> _ReadingRoomSeatUsedState = new List<JM_RoomSeatUsedState>();

        public List<JM_RoomSeatUsedState> ReadingRoomSeatUsedState
        {
            get { return _ReadingRoomSeatUsedState; }
            set { _ReadingRoomSeatUsedState = value; }
        }
         
    }
}
