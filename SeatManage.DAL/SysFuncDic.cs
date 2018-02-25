/*****************************************************
 * 作者：王昊天
 * 创建日期：2013-6-3
 * 说明：功能页面表操作DAL
 * 修改人：
 * 修改时间：
 * **************************************************/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
    public class SysFuncDic
    {
        /// <summary>
        /// 添加新的功能页面
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("insert into SysFuncDic(ModSeq,MCaption,MenuLink,OrderSeq)");
            sqlstr.Append(" values(@ModSeq,@MCaption,@MenuLink,@OrderSeq)");
            SqlParameter[] parameters = {
					new SqlParameter("@ModSeq", SqlDbType.NVarChar,50),
                    new SqlParameter("@MCaption",SqlDbType.NVarChar,60),
                    new SqlParameter("@MenuLink",SqlDbType.NVarChar,255),
                    new SqlParameter("@OrderSeq",SqlDbType.NVarChar,50)
        };
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.PageUrl;
            parameters[3].Value = model.Order;
            try
            {
                int rows = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("update SysFuncDic set MCaption=@MCaption,MenuLink=@MenuLink,OrderSeq=@OrderSeq");
            sqlstr.Append(" where ModSeq=@ModSeq");
            SqlParameter[] parameters = {
					new SqlParameter("@ModSeq", SqlDbType.NVarChar,50),
                    new SqlParameter("@MCaption",SqlDbType.NVarChar,60),
                    new SqlParameter("@MenuLink",SqlDbType.NVarChar,255),
                    new SqlParameter("@OrderSeq",SqlDbType.NVarChar,50)
        };
            parameters[0].Value = model.No;
            parameters[1].Value = model.Name;
            parameters[2].Value = model.PageUrl;
            parameters[3].Value = model.Order;
            try
            {
                int rows = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// 返回查出的数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public DataSet GetList(string strWhere)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("select ModSeq,MCaption,MenuLink,OrderSeq");
            sqlstr.Append(" from SysFuncDic");
            sqlstr.Append(" where ModSeq<>'0000'");
            if (!string.IsNullOrEmpty(strWhere))
            {
                sqlstr.Append(" and ");
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
        /// 删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(SeatManage.ClassModel.SysFuncDicInfo model)
        {
            StringBuilder sqlstr = new StringBuilder();
            sqlstr.Append("delete SysFuncDic");
            sqlstr.Append(" where ModSeq=@ModSeq");
            SqlParameter[] parameters = {
					new SqlParameter("@ModSeq", SqlDbType.NVarChar,50)
        };
            parameters[0].Value = model.No;
            try
            {
                int rows = DbHelperSQL.ExecuteSql(sqlstr.ToString(), parameters);
                if (rows == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
