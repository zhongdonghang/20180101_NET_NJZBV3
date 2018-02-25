using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
    [Serializable]
    public class AreaInfo
    {
        private int _AreaNo = -1;
        /// <summary>
        /// 区域编号
        /// </summary>
        public int AreaNo
        {
            get { return _AreaNo; }
            set { _AreaNo = value; }
        }
        private string _AreaName = "";
        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }
    }
}
