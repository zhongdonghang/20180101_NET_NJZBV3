using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public class WeiXinResponse
    {
        private int _id;
        private string _text;
        private int _type;
        private AMS_UserInfo _loginid;
        private DateTime _adddatetime;
        private int _isused;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            set { _text = value; }
            get { return _text; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public AMS_UserInfo LoginID
        {
            set { _loginid = value; }
            get { return _loginid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime AddDateTime
        {
            set { _adddatetime = value; }
            get { return _adddatetime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int IsUsed
        {
            set { _isused = value; }
            get { return _isused; }
        }

    }
}
