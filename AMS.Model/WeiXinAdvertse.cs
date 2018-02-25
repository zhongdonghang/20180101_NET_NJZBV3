using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.Model
{
    public  class WeiXinAdvertse
    {
        private int _id;
        private string _image;
        private string _title;
        private string _url;
        private string _contentxml;
        private AMS_UserInfo _loginid;
        private DateTime _datetime;
        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Image
        {
            set { _image = value; }
            get { return _image; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ContentXML
        {
            set { _contentxml = value; }
            get { return _contentxml; }
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
        public DateTime DateTime
        {
            set { _datetime = value; }
            get { return _datetime; }
        }
    }
}
