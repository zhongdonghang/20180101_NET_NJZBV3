/********************************
 * 作者：王昊天
 * 创建时间：2013-6-5
 * 说明：角色的菜单操作dal
 * 修改人：
 * 修改时间：
 * ******************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;

namespace SeatManage.DAL
{
    public class SysFuncRights
    {
        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool DeleteFuncRights(SysRolesDicInfo model)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("delete SysFuncRights");
            sqlstr.Append(" where RoleID=@RoleID");
            SqlParameter[] parameters ={
                                          new SqlParameter("@RoleID",model.RoleID)
                                      };
            try
            {
                int row = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                if (row > 0)
                {
                    return true;
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
        /// 添加权限
        /// </summary>
        /// <param name="RoleID"></param>
        /// <param name="MenuID"></param>
        /// <returns></returns>
        public bool AddFuncRight(string RoleID, int MenuID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into SysFuncRights(RoleID,MenuID)");
            sqlstr.Append(" values(@RoleID,@MenuID)");
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@RoleID", RoleID);
            parameters[1] = new SqlParameter("@MenuID", MenuID);
            try
            {
                int row = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                if (row > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
