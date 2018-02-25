using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
    /// 数据访问类:T_SM_SystemSet
	/// </summary>
	public partial class T_SM_SystemSet
	{
		public T_SM_SystemSet()
		{}
		#region  Method

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ServiceSetID,SetName,ServiceSet ");
            strSql.Append(" FROM T_SM_SystemSet ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ServiceSetID,SetName,ServiceSet ");
            strSql.Append(" FROM T_SM_SystemSet ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ServiceSetID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from T_SM_SystemSet");
			strSql.Append(" where ServiceSetID=@ServiceSetID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
			parameters[0].Value = ServiceSetID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public bool Add(string strSet, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_SystemSet(");
            strSql.Append("ServiceSetID,SetName,ServiceSet)");
            strSql.Append(" values (");
            strSql.Append("@ServiceSetID,@SetName,@ServiceSet)");

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
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(string strSet,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_SystemSet set "); 
            strSql.Append("ServiceSet=@ServiceSet");
            strSql.Append(" where ServiceSetID=@ServiceSetID ");
            
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

        ///// <summary>
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(int ServiceSetID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_ServiceSet ");
        //    strSql.Append(" where ServiceSetID=@ServiceSetID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
        //    parameters[0].Value = ServiceSetID;

        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        ///// <summary>
        ///// 批量删除数据
        ///// </summary>
        //public bool DeleteList(string ServiceSetIDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_ServiceSet ");
        //    strSql.Append(" where ServiceSetID in ("+ServiceSetIDlist + ")  ");
        //    int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
        //    if (rows > 0)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}


        ///// <summary>
        ///// 得到一个对象实体
        ///// </summary>
        //public SeatManage.Model.T_SM_ServiceSet GetModel(int ServiceSetID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 ServiceSetID,SetName,ServiceSet from T_SM_ServiceSet ");
        //    strSql.Append(" where ServiceSetID=@ServiceSetID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ServiceSetID", SqlDbType.Int,4)};
        //    parameters[0].Value = ServiceSetID;

        //    SeatManage.Model.T_SM_ServiceSet model=new SeatManage.Model.T_SM_ServiceSet();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["ServiceSetID"]!=null && ds.Tables[0].Rows[0]["ServiceSetID"].ToString()!="")
        //        {
        //            model.ServiceSetID=int.Parse(ds.Tables[0].Rows[0]["ServiceSetID"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["SetName"]!=null && ds.Tables[0].Rows[0]["SetName"].ToString()!="")
        //        {
        //            model.SetName=ds.Tables[0].Rows[0]["SetName"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["ServiceSet"]!=null && ds.Tables[0].Rows[0]["ServiceSet"].ToString()!="")
        //        {
        //            model.ServiceSet=ds.Tables[0].Rows[0]["ServiceSet"].ToString();
        //        }
        //        return model;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

		

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.VarChar, 255),
					new SqlParameter("@fldName", SqlDbType.VarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
					};
			parameters[0].Value = "T_SM_ServiceSet";
			parameters[1].Value = "ServiceSetID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
	}
}

