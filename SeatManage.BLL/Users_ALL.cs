using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class Users_ALL
    {
        /// <summary>
        /// 添加新的用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool AddNewUser(UserInfo user)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddNewUser(user);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加用户失败：" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> GetUsers()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetUsers();
            }
            catch (Exception EX)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户信息失败：" + EX.Message);
                return new List<UserInfo>();
            }
          
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="LoginId"></param>
        /// <returns></returns>
        public static UserInfo GetUserInfo(string LoginId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetUserInfo(LoginId);
            }
            catch (Exception EX)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户信息失败：" + EX.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUserInfo(UserInfo user)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateUserInfo(user);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新读者信息失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool DeleteUser(UserInfo user)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteUser(user);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除读者信息失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 验证用户登录
        /// </summary>
        /// <param name="loginId">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns>返回null为登录失败</returns>
        public string CheckUser(string loginId, string passWord)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.CheckUser(loginId, passWord);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("密码验证失败：" + ex.Message);
                throw ex;
            }
            
        }
        
        public static List<int> GetRoleID(string loginId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetRoleID(loginId);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取角色失败：" + ex.Message);
                return new List<int>();
            }
            
        }
        /// <summary>
        /// 简单更新，更新密码用户名等，不更新权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool UpdateUserOnlyInfo(UserInfo user)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateUser(user);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新读者信息失败：" + ex.Message);
                return false;
            }
           
        }
    }
}
