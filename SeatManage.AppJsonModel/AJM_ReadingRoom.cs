using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.AppJsonModel
{
    public class AJM_ReadingRoom
    {
        private string _roomNo = "";
        private string _roomName = "";
        private string _libraryName = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string RoomNo
        {
            get { return _roomNo; }
            set { _roomNo = value; }
        }
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _roomName; }
            set { _roomName = value; }
        }
        /// <summary>
        /// 图书馆名称
        /// </summary>
        public string LibraryName
        {
            get { return _libraryName; }
            set { _libraryName = value; }
        }
    }
}
