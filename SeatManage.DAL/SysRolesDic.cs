/********************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：角色的操作dal
 * 修改人：
 * 修改时间：
 * ******************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
using System.Collections.Generic;

namespace SeatManage.DAL
{
    public class SysRolesDic
    {
        public DataSet GetList(string strWhere)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROLEID, ROLENAME, IsLock");
            strSql.Append(" FROM SysRolesDic");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            try
            {
                return DbHelperSQL.Query(strSql.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <returns></returns>
        public int Add(SeatManage.ClassModel.SysRolesDicInfo model)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@RoleID", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@RoleName", model.RoleName);
            parameters[2] = new SqlParameter("@IsLock", model.IsLock);
            try
            {
                DbHelperSQL.Execute_Proc("Proc_AddRole", parameters);
                return int.Parse(parameters[0].Value.ToString());
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(SeatManage.ClassModel.SysRolesDicInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysRolesDic set  ROLENAME=@ROLENAME, IsLock=@IsLock");
            strSql.Append(" where ROLEID=@ROLEID");
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@ROLEID", model.RoleID);
            parameters[1] = new SqlParameter("@RoleName", model.RoleName);
            parameters[2] = new SqlParameter("@IsLock", model.IsLock);
            try
            {
                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
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
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(SeatManage.ClassModel.SysRolesDicInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SysRolesDic");
            strSql.Append(" where ROLEID=@ROLEID");
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ROLEID", model.RoleID);
            try
            {
                int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
                if (rows > 0)
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
