using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinJK
{
    public class WeiXinJKPram
    {
        /// <summary>
        /// 预留在微信服务器的字符串
        /// </summary>
        public static string TOKEN
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["TOKEN"];
            }
        }

        public static string AppID
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppID"];
            }
        }

        public static string AppSecret
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["AppSecret"];
            }
        }
        /// <summary>
        /// 发送主动信息的凭据
        /// </summary>
        
               
    }

    
}
