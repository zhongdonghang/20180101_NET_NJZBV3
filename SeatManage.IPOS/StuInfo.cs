using System;
using System.Collections.Generic;
using System.Text;

namespace SeatManage.IPOS
{
    public class StuInfo
    {
        private string cardId = "";
        /// <summary>
        /// 卡片ID
        /// </summary>
        public string CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }
        private string cardNo = "";
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private string name = "";
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string rank = "";
        /// <summary>
        /// 读者类型
        /// </summary>
        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        private string org = "";
        /// <summary>
        /// 读者部门
        /// </summary>
        public string Org
        {
            get { return org; }
            set { org = value; }
        }
        private int flag ;
        /// <summary>
        /// 状态
        /// </summary>
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
    }
}
