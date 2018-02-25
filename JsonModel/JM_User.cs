using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.JsonModel
{
    public class JM_User
    {
        private string _loginId = "";
        /// <summary>
        /// 登录ID
        /// </summary>
        public string LoginId
        {
            get
            { return _loginId; }
            set
            { _loginId = value; }
        }

        private string _userName = " ";
        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName
        {
            get
            { return _userName; }
            set
            { _userName = value; }
        }
          
        private string _password = "";
        /// <summary>
        /// 登录密码
        /// </summary>
        public string Password
        {
            get
            { return _password; }
            set
            { _password = value; }
        }
        /// <summary>
        /// App消息推送的通道Id
        /// </summary>
        public string App_ChannelId
        {
            get;
            set;
        }
        /// <summary>
        /// 用户app的Id
        /// </summary>
        public string App_UserId
        {
            get;
            set;
        }
         
      
         
    }
}
