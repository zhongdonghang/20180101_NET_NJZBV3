using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.IWCFService;
using SeatManage.ISystemTerminal.ILoginValidate;
using SeatManage.DAL;
using System.Data.SqlClient;
using System.Data;
using SeatManage.EnumType;
using SeatManage.ClassModel;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {

        SeatManage.DAL.Users_ALL dalUser_All = new Users_ALL();
        SeatManage.DAL.sysEmpRoles sysemprole_dal = new sysEmpRoles();
        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <returns></returns>
        public List<UserInfo> GetUsers()
        {
            List<UserInfo> list = new List<UserInfo>();
            try
            {
                DataSet ds = dalUser_All.GetList(null, null);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        SeatManage.ClassModel.UserInfo userInfo = new SeatManage.ClassModel.UserInfo();
                        userInfo.LoginId = dr["LoginID"].ToString();
                        userInfo.Password = dr["UsrPwd"].ToString();
                        userInfo.UserType = (UserType)int.Parse(dr["UsrType"].ToString());
                        userInfo.UserName = dr["UsrName"].ToString();
                        userInfo.IsUsing = (LogStatus)int.Parse(dr["UsrEnabled"].ToString());
                        userInfo.Remark = dr["Remark"].ToString();
                        if (dr["IPLockIPAdress"] != null)
                        {
                            userInfo.LockIPAdress = dr["IPLockIPAdress"].ToString();
                        }
                        list.Add(userInfo);
                    }
                }
                return list;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId">登录名</param>
        /// <param name="password">密码</param>
        /// <returns>学生信息</returns>
        public string CheckUser(string loginId, string password)
        {
            ILoginValidate loginValidate = SeatManage.InterfaceFactory.AssemblyFactory.CreateAssembly("ILoginValidate") as ILoginValidate; //SeatManage.InterfaceFactory.SystemTerminalFactory.CreateLoginValidate();
            string reader = loginValidate.CheckUser(loginId, password);
            return reader;
        }

        /// <summary>
        /// 根据学号获取读者的登录信息
        /// </summary>
        /// <param name="cardNo">学号</param> 
        /// <returns></returns> 
        public SeatManage.ClassModel.UserInfo GetUserInfo(string LoginId)
        {
            if (string.IsNullOrEmpty(LoginId))
            {
                return null;
            }
            string strWhere = "LoginId=@loginId";
            SqlParameter[] parameters = {
                                            new SqlParameter("@loginId",LoginId)
                                        };
            DataSet ds = dalUser_All.GetList(strWhere, parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                SeatManage.ClassModel.UserInfo userInfo = new SeatManage.ClassModel.UserInfo();
                userInfo.LoginId = ds.Tables[0].Rows[0]["LoginID"].ToString();
                userInfo.Password = ds.Tables[0].Rows[0]["UsrPwd"].ToString();
                userInfo.UserType = (UserType)int.Parse(ds.Tables[0].Rows[0]["UsrType"].ToString());
                userInfo.UserName = ds.Tables[0].Rows[0]["UsrName"].ToString();
                userInfo.IsUsing = (LogStatus)int.Parse(ds.Tables[0].Rows[0]["UsrEnabled"].ToString());
                userInfo.Remark = ds.Tables[0].Rows[0]["Remark"].ToString();
                if (ds.Tables[0].Rows[0]["IPLockIPAdress"] != null)
                {
                    userInfo.LockIPAdress = ds.Tables[0].Rows[0]["IPLockIPAdress"].ToString();
                }
                return userInfo;
            }
            else
            {
                return null;
            }
        }
        
        /// <summary>
        /// 添加新的用户
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool AddNewUser(UserInfo user)
        {
            HandleResult result = dalUser_All.Add(user);
            if (result == HandleResult.Successed)
            {
                bool error = false;
                foreach (int roleid in user.ReloID)
                {
                    if (!sysemprole_dal.Add(roleid, user.LoginId))
                    {
                        error = true;
                        break;
                    }
                }
                if (error)
                {
                    return false;
                }
                else
                {
                    if (UpdateManagerPotency(user.UserRoomRight))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 更新读者信息
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public bool UpdateUserInfo(UserInfo user)
        {
            try
            {
                if (dalUser_All.Update(user))
                {
                    if (sysemprole_dal.Delete(user))
                    {
                        bool error = false;
                        foreach (int roleid in user.ReloID)
                        {
                            if (!sysemprole_dal.Add(roleid, user.LoginId))
                            {
                                error = true;
                                break;
                            }
                        }
                        if (error)
                        {
                            return false;
                        }
                        else
                        {
                            if (UpdateManagerPotency(user.UserRoomRight))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DeleteUser(SeatManage.ClassModel.UserInfo user)
        {
            try
            {
                return dalUser_All.delete(user);
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 简单更新用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(SeatManage.ClassModel.UserInfo user)
        {
            try
            {
                return (dalUser_All.Update(user));
            }
            catch
            {
                throw;
            }
                
        }
    }
}
