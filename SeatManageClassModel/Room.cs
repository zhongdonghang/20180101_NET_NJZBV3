using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    
    /// <summary>
    /// 房间
    /// </summary>
   [Serializable]
    public class Room
    { 

        private string _roomNo = "";
        /// <summary>
        /// 房间编号
        /// </summary>
        public string No
        {
            get
            {
                return _roomNo;
            }
            set
            {
                _roomNo = value;
            }
        }

        private string _roomName = "";
        /// <summary>
        /// 房间名称
        /// </summary>
        public string Name
        {
            get
            {
                return _roomName;
            }
            set
            {
                _roomName = value;
            }
        }
    }
}
