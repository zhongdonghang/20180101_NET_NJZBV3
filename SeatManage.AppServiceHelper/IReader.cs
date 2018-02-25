using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatManage.AppServiceHelper
{
    public partial interface IAppServiceHelper
    {/// <summary>
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        string GetUserInfo(string studentNo);
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        string GetUserNowState(string studentNo);
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        string CheckUser(string loginId, string password);
        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string GetUserInfo_WeiXin(string studentNo);
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        string GetUserNowStateV2(string studentNo, bool isCheckCode);
    }
}
