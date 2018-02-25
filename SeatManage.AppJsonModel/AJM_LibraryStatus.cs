using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_LibraryStatus
    {
        /// <summary>
        /// 图书馆编号
        /// </summary>
        private string _libraryNo = "";
        /// <summary>
        /// 图书馆名称
        /// </summary>
        private string _libraryName = "";
        /// <summary>
        /// 总座位数
        /// </summary>
        private int _allSeats = 0;
        /// <summary>
        /// 总使用数
        /// </summary>
        private int _allUsed = 0;
        /// <summary>
        /// 总预约数
        /// </summary>
        private int _allBooked = 0;
        /// <summary>
        /// 使用百分比
        /// </summary>
        private int _usedPercentage = 0;
        /// <summary>
        /// 每个阅览室情况
        /// </summary>
        private List<AJM_ReadingRoomState> _roomStatus = new List<AJM_ReadingRoomState>();


        /// <summary>
        /// 图书馆编号
        /// </summary>
        public string LibraryNo
        {
            get { return _libraryNo; }
            set { _libraryNo = value; }
        }

        /// <summary>
        /// 图书馆名称
        /// </summary>
        public string LibraryName
        {
            get { return _libraryName; }
            set { _libraryName = value; }
        }

        /// <summary>
        /// 总座位数
        /// </summary>
        public int AllSeats
        {
            get { return _allSeats; }
            set { _allSeats = value; }
        }

        /// <summary>
        /// 总使用数
        /// </summary>
        public int AllUsed
        {
            get { return _allUsed; }
            set { _allUsed = value; }
        }

        /// <summary>
        /// 总预约数
        /// </summary>
        public int AllBooked
        {
            get { return _allBooked; }
            set { _allBooked = value; }
        }

        /// <summary>
        /// 使用百分比
        /// </summary>
        public int UsedPercentage
        {
            get { return _usedPercentage; }
            set { _usedPercentage = value; }
        }

        /// <summary>
        /// 每个阅览室情况
        /// </summary>
        public List<AJM_ReadingRoomState> RoomStatus
        {
            get { return _roomStatus; }
            set { _roomStatus = value; }
        }
    }
}
