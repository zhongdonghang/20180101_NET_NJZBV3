using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;

namespace SeatManage.ClassModel
{
    /// <summary>
    /// 座位消息
    /// </summary>
    public class SeatNotice
    {
        public SeatNotice(string cardNo, string schoolNum)
        {
            this.cardNo = cardNo;
            this.schoolNum = schoolNum;
        }
        string cardNo;
        /// <summary>
        /// 学生学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string schoolNum;
        /// <summary>
        /// 学校编号，用于标识用户所在学校
        /// </summary>
        public string SchoolNum
        {
            get { return schoolNum; }
            set { schoolNum = value; }
        }
            
        NoticeType type;
        /// <summary>
        /// 消息类型
        /// </summary>
        public NoticeType Type
        {
            get { return type; }
            set { type = value; }
        }

        string context;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Context
        {
            get { return context; }
            set { context = value; }
        }

    }
}
