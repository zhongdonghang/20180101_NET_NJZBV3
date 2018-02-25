using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 阅览室
    /// </summary>
    [Serializable]
    public class ReadingRoomInfo : Room
    {
        AreaInfo area = new AreaInfo();
        /// <summary>
        /// 区域信息
        /// </summary>
        public AreaInfo Area
        {
            get { return area; }
            set { area = value; }
        }
        LibraryInfo libaray = new LibraryInfo();
        /// <summary>
        /// 所在图书馆的信息
        /// </summary>
        public LibraryInfo Libaray
        {
            get { return libaray; }
            set { libaray = value; }
        }

        private ReadingRoomSetting setting;
        /// <summary>
        /// 阅览室设置。
        /// </summary>
        public ReadingRoomSetting Setting
        {
            get
            {
                return setting;
            }
            set
            {
                setting = value;
            }
        }

        //private string libraryId = "";
        ///// <summary>
        ///// 图书馆Id
        ///// </summary>
        //public string LibraryNum
        //{
        //    get
        //    {
        //        return libraryId;
        //    }
        //    set
        //    {
        //        libraryId = value;
        //    }
        //}

        private SeatLayout seatList = null;
        /// <summary>
        /// 阅览室座位布局列表
        /// </summary>
        public SeatLayout SeatList
        {
            get
            {
                return seatList;
            }
            set
            {
                seatList = value;
            }
        } 
    }
}
