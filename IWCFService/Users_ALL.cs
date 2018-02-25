/************************************
 * 作者：王随
 * 创建时间：2013-5-23 14:41
 * 说明：用户表相关操作的服务。
 * 修改人：
 * 修改时间：
 * **********************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 获取全部用户信息
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        List<UserInfo> GetUsers();
        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddNewUser(UserInfo user);
        /// <summary>
        /// 验证读者身份
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        string CheckUser(string loginId, string password);
        /// <summary>
        /// 根据学号获取读者的登录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <returns></returns>
        [OperationContract]
        SeatManage.ClassModel.UserInfo GetUserInfo(string loginId);
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
       [OperationContract]
        bool UpdateUserInfo(SeatManage.ClassModel.UserInfo user);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
       bool DeleteUser(SeatManage.ClassModel.UserInfo user);
        /// <summary>
        /// 简单更新
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [OperationContract]
        bool UpdateUser(SeatManage.ClassModel.UserInfo user);
    
    }
}
