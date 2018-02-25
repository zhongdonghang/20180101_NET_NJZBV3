using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    /// <summary>
    /// 内容明细
    /// </summary>
    public class AMS_CommandDetail : View_CommandList
    {

        private string _ContentName;
        /// <summary>
        /// 下发内容名称
        /// </summary>
        public string  ContentName
        {
            get { return _ContentName; }
            set { _ContentName = value; }
        }
        private string _ContentNumber;
        /// <summary>
        /// 下发内容编号
        /// </summary>
        public string ContentNumber
        {
            get { return _ContentNumber; }
            set { _ContentNumber = value; }
        }

        private string _Describe;
        /// <summary>
        /// 内容描述
        /// </summary>
        public string ContentDescribe
        {
            get { return _Describe; }
            set { _Describe = value; }
        }

        private int _contentID;

        public int ContentID
        {
            get { return _contentID; }
            set { _contentID = value; }
        }
    }
}
