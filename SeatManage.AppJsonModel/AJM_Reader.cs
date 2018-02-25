using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppJsonModel
{
    public class AJM_Reader
    {
        private string _cardId;
        private string _studentNo;
        private string _name;
        private string _sex;
        private string _department;
        private string _readerType;


        /// <summary>
        /// 卡号
        /// </summary>
        public string CardId
        {
            get { return _cardId; }
            set { _cardId = value; }
        }
        /// <summary>
        /// 学号
        /// </summary>
        public string StudentNo
        {
            get { return _studentNo; }
            set { _studentNo = value; }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return _sex; }
            set { _sex = value; }
        }
        /// <summary>
        /// 所在系别
        /// </summary>
        public string Department
        {
            get { return _department; }
            set { _department = value; }
        }
        /// <summary>
        /// 读者类型
        /// </summary>
        public string ReaderType
        {
            get { return _readerType; }
            set { _readerType = value; }
        }
        
    }
}
