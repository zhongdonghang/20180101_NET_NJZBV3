/*****************************************************
 * 作者：王昊天
 * 创建日期：2013-6-3
 * 说明：用户角色操作DAL
 * 修改人：
 * 修改时间：
 * **************************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
using System.Collections.Generic;

namespace SeatManage.DAL
{
    public class sysEmpRoles
    {
        /// <summary>
        /// 查询权限
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select RoleID");
            sqlstr.Append(" from sysEmpRoles");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sqlstr.Append(" where ");
                sqlstr.Append(strWhere);
            }
            try
            {
                return DbHelperSQL.Query(sqlstr.ToString());
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 添加一个角色对应
        /// </summary>
        /// <param name="RoleID">角色ID</param>
        /// <param name="LoginID">用户ID</param>
        /// <returns></returns>
        public bool Add(int RoleID,string LoginID)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into sysEmpRoles(RoleID,LoginID)");
            sqlstr.Append(" values(@RoleID,@LoginID)");
            SqlParameter[] parameters = {
					new SqlParameter("@RoleID", SqlDbType.Int),
                    new SqlParameter("@LoginID",SqlDbType.NVarChar,60)
        };
            parameters[0].Value = RoleID;
            parameters[1].Value = LoginID;
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
        /// 删除用户对应的角色
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(UserInfo model)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("delete sysEmpRoles");
            sqlstr.Append(" where LoginID=@LoginID");
            SqlParameter[] parameters = {
					new SqlParameter("@LoginID", SqlDbType.NVarChar,50)
        };
            parameters[0].Value = model.LoginId;
            try
            {
                int row = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
