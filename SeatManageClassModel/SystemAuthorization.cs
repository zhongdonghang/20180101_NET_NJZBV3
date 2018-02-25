using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    public class SystemAuthorization
    {
        private string _schoolNum;
        private int _SeatClientCount = 0;
        private bool _isOnline = true;
        private Dictionary<string, DateTime> seatClientList = new Dictionary<string, DateTime>();
        /// <summary>
        /// 学校编号
        /// </summary>
        public string SchoolNum
        {
            get { return _schoolNum; }
            set { _schoolNum = value; }
        }
        /// <summary>
        /// 终端数目
        /// </summary>
        public int SeatClientCount
        {
            get { return _SeatClientCount; }
            set { _SeatClientCount = value; }
        }
        /// <summary>
        /// 终端列表
        /// </summary>
        public Dictionary<string, DateTime> SeatClientList
        {
            get { return seatClientList; }
            set { seatClientList = value; }
        }
    }

}
