using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeiXinJK.Model
{
    /// <summary>
    /// 属性Access_token自动判断是否有效。
    /// </summary>
    public class WeiXinAccessToken
    {
        private string _access_token;

        /// <summary>
        /// 获取到的凭证,如果超时，则返回空。
        /// </summary>
        public string Access_token
        {
            get
            {
                if ((_createTime - DateTime.Now).TotalSeconds <= _Expires_in)
                {
                    return _access_token;
                }
                else
                {
                    return string.Empty;
                }

            }
            set
            {
                _access_token = value;
                _createTime = DateTime.Now;
            }
        }

        private int _Expires_in;
        

        private DateTime _createTime;
        /// <summary>
        /// token
        /// </summary>
        /// <param name="token">token</param>
        /// <param name="expires">有效时间(秒)</param>
        public WeiXinAccessToken(string token, int expires)
        {
            Access_token = token;
            _Expires_in = expires;
        }

        /// <summary>
        /// token
        /// </summary> 
        /// <param name="expires">有效时间(秒)</param>
        public WeiXinAccessToken(  int expires)
        { 
            _Expires_in = expires;
        }
    }
}
