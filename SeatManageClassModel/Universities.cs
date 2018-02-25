using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ClassModel
{
      [Serializable]
    public class Universities:Room
    {
        string _DTUip;
        /// <summary>
        /// DTUIp 
        /// </summary>
        public string DTUip
        {
            get { return _DTUip; }
            set { _DTUip = value; }
        }
        string _Describe;
        /// <summary>
        /// 描述
        /// </summary>
        public string Describe
        {
            get { return _Describe; }
            set { _Describe = value; }
        }
        string _ConnectionString;
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        int _PageView;
        /// <summary>
        /// 网站访问量
        /// </summary>
        public int PageView
        {
            get { return _PageView; }
            set { _PageView = value; }
        }
    }
}
