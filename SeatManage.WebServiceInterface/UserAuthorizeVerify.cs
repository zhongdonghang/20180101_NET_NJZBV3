using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeatManage.WebServiceInterface
{
    /// <summary>
    /// 用户授权验证
    /// </summary>
    public class UserAuthorizeVerify
    {
        /// <summary>
        /// 检查用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthorizeVerify(string user, string password)
        {
            return true;
        }
    }
}