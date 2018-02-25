/********************************
 * 作者：王昊天
 * 创建时间：2013-6-3
 * 说明：功能菜单数据操作的DAL
 * 修改人：
 * 修改时间：
 * ******************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;

namespace SeatManage.DAL
{
    public class SysMenu
    {
        /// <summary>
        /// 获取权限对应的菜单列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetRoleMenuList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ROLEID, ROLENAME, MenuLv, MCaption,MenuID, Mainnum, ModSeq, MenuLink, MenuImagePath, ItemSeq");
            strSql.Append(" FROM ViewRoleMenu");
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
        /// 获取功能菜单
        /// </summary>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select MenuID, Mainnum, ModSeq, ItemSeq, MCaption, MenuImagePath, MenuLv");
            strSql.Append(" FROM SysMenu");
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
        /// 添加功能菜单
        /// </summary>
        /// <returns></returns>
        public bool Add(SeatManage.ClassModel.SysMenuInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into SysMenu(Mainnum, ModSeq, ItemSeq, MCaption, MenuImagePath, MenuLv)");
            strSql.Append(" values(@Mainnum, @ModSeq, @ItemSeq, @MCaption, @MenuImagePath, @MenuLv)");
            SqlParameter[] parameters = {
                    new SqlParameter("@Mainnum",SqlDbType.NVarChar,50),
                    new SqlParameter("@ModSeq",SqlDbType.NVarChar,50),
                    new SqlParameter("@ItemSeq",SqlDbType.NVarChar,50),
                    new SqlParameter("@MCaption",SqlDbType.NVarChar,50),
                    new SqlParameter("@MenuImagePath",SqlDbType.NVarChar,50),
                    new SqlParameter("@MenuLv",SqlDbType.Int)
        };
            parameters[0].Value = model.MainNum;
            parameters[1].Value = model.FuncPageNum;
            parameters[2].Value = model.Index;
            parameters[3].Value = model.MenuName;
            parameters[4].Value = model.ImageUrl;
            parameters[5].Value = model.MenuLv;
            try
            {
                int row = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// <summary>
        /// 更新功能菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(SeatManage.ClassModel.SysMenuInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update SysMenu ");
            strSql.Append(" set Mainnum=@Mainnum, ModSeq=@ModSeq, ItemSeq=@ItemSeq, MCaption=@MCaption, MenuImagePath=@MenuImagePath, MenuLv=@MenuLv");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
                    new SqlParameter("@Mainnum",SqlDbType.NVarChar,50),
                    new SqlParameter("@ModSeq",SqlDbType.NVarChar,50),
                    new SqlParameter("@ItemSeq",SqlDbType.NVarChar,50),
                    new SqlParameter("@MCaption",SqlDbType.NVarChar,50),
                    new SqlParameter("@MenuImagePath",SqlDbType.NVarChar,50),
                    new SqlParameter("@MenuLv",SqlDbType.Int),
                    new SqlParameter("@MenuID",SqlDbType.Int)
        };
            parameters[0].Value = model.MainNum;
            parameters[1].Value = model.FuncPageNum;
            parameters[2].Value = model.Index;
            parameters[3].Value = model.MenuName;
            parameters[4].Value = model.ImageUrl;
            parameters[5].Value = model.MenuLv;
            parameters[6].Value = model.MenuID;
            try
            {
                int row = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        /// <summary>
        /// 删除功能菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool delete(SeatManage.ClassModel.SysMenuInfo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete SysMenu ");
            strSql.Append(" where MenuID=@MenuID");
            SqlParameter[] parameters = {
                    new SqlParameter("@MenuID",SqlDbType.Int)
        };
            parameters[0].Value = model.MenuID;
            try
            {
                int row = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
