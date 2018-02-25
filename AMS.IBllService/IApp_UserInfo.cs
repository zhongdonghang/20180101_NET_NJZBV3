using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AMS.Model;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 根据学号和学校编号获取用户信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
         [OperationContract]
        AppUserInfo GetAppUserInfoByCardNoAndSchoolNum(string cardNo, string schoolNum);
        /// <summary>
        /// 绑定app相关的用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         [OperationContract]
        bool BindAppUserInfo(AppUserInfo model);
    }
}
