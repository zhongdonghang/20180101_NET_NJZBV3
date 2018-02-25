using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JsonModel
{
    /// <summary>
    /// 读者基础信息
    /// </summary>
    public class JM_ReaderInfo
    {
        string cardNo;
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return cardNo; }
            set { cardNo = value; }
        }
        string name;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        string sex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return sex; }
            set { sex = value; }
        }
        string cardId;
        /// <summary>
        /// 卡列号
        /// </summary>
        public string CardId
        {
            get { return cardId; }
            set { cardId = value; }
        }
    }
}
