using System;
using System.Collections.Generic;
using System.Text;

namespace SeatManage.IPOS
{
    public class StuInfo
    {
        private string cardId = "";
        /// <summary>
        /// ��ƬID
        /// </summary>
        public string CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }
        private string cardNo = "";
        /// <summary>
        /// ѧ��
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        private string name = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string rank = "";
        /// <summary>
        /// ��������
        /// </summary>
        public string Rank
        {
            get { return rank; }
            set { rank = value; }
        }
        private string org = "";
        /// <summary>
        /// ���߲���
        /// </summary>
        public string Org
        {
            get { return org; }
            set { org = value; }
        }
        private int flag ;
        /// <summary>
        /// ״̬
        /// </summary>
        public int Flag
        {
            get { return flag; }
            set { flag = value; }
        }
    }
}
