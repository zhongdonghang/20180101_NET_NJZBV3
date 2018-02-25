using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.IBllService
{
    public partial interface IAdvertManageBllService
    {
        /// <summary>
        /// 获取全部读者
        /// *异常处理：catch后向上抛出
        /// </summary>
        /// <returns>成功返回读者model的list</returns>
        [OperationContract]
        List<AMS.Model.AMS_UserInfo> GetUserInfo();

        /// <summary>
        /// 获取单独用户信息
        /// *异常处理：catch后向上抛出
        /// </summary>
        /// <param name="loginID">登录ID</param>
        /// <returns>成功返回model</returns>
        [OperationContract]
        AMS.Model.AMS_UserInfo GetSingleUserInfo(string loginID);

        /// <summary>
        /// 新增用户
        /// *异常处理：catch后return异常信息
        /// *判断：读者的姓名和LoginID不重复就行了
        /// </summary>
        /// <param name="model">用户信息的model</param>
        /// <returns>成功返回""或null，失败返回错误信息</returns>
        [OperationContract]
        string AddNewUser(AMS.Model.AMS_UserInfo model);

        /// <summary>
        /// 更新用户
        /// *异常处理：catch后return异常信息
        /// *判断：读者的姓名和LoginID除了当前用户不能和其他重复就行了
        /// </summary>
        /// <param name="model">用户信息的model</param>
        /// <returns>成功返回""或null，失败返回错误信息</returns>
        [OperationContract]
        string UpdateUser(AMS.Model.AMS_UserInfo model);

        /// <summary>
        /// 删除用户
        /// *异常处理：catch后return异常信息
        /// </summary>
        /// <param name="model">用户信息的model，只需要ID其他属性可以为空</param>
        /// <returns>成功返回""或null，失败返回错误信息</returns>
        [OperationContract]
        string DeleteUser(AMS.Model.AMS_UserInfo model);
    }
}
