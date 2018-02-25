using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    /// <summary>
    ///  提醒消息
    /// </summary>
   public class JM_ReaderNotice
    {
        private int _NoticeID = -1;
        /// <summary>
        /// 消息ID
        /// </summary>
        public int NoticeID
        {
            get { return _NoticeID; }
            set { _NoticeID = value; }
        }

        public string AddTime
        {
            get;
            set;
        }

        private string _CardNo = "";
        /// <summary>
        /// 读者学号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; }
        }


        private string _Note = "";
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }
       /// <summary>
       /// 学校编号
       /// </summary>
        public string SchoolNum
        {
            get;
            set;
        }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string NoticeTitle
        {
            get;
            set;
        }

        
        /// <summary>
        /// 标示是否已经阅读过
        /// </summary>
        public bool IsRead
        {
            get;
            set;
        }
    }
}
