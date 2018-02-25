using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_Integration
	/// </summary>
	public partial class T_SM_Integration
	{
		public T_SM_Integration()
		{}
		#region  Method

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(string ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_Integration");
			strSql.Append(" where ID=@ID ");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.NVarChar,50)};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,CardNo,AddTime,Count,Operation,Type,Flag ");
            strSql.Append(" FROM T_SM_Integration ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,CardNo,AddTime,Count,Operation,Type,Flag ");
            strSql.Append(" FROM T_SM_Integration ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        ///// <summary>
        ///// 增加一条数据
        ///// </summary>
        //public bool Add(SeatManage.Model.T_SM_Integration model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("insert into T_SM_Integration(");
        //    strSql.Append("ID,CardNo,AddTime,Count,Operation,Type,Flag)");
        //    strSql.Append(" values (");
        //    strSql.Append("@ID,@CardNo,@AddTime,@Count,@Operation,@Type,@Flag)");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.NVarChar,50),
        //            new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
        //            new SqlParameter("@AddTime", SqlDbType.DateTime),
        //            new SqlParameter("@Count", SqlDbType.Int,4),
        //            new SqlParameter("@Operation", SqlDbType.Int,4),
        //            new SqlParameter("@Type", SqlDbType.Int,4),
        //            new SqlParameter("@Flag", SqlDbType.Bit,1)};
        //    parameters[0].Value = model.ID;
        //    parameters[1].Value = model.CardNo;
        //    parameters[2].Value = model.AddTime;
        //    parameters[3].Value = model.Count;
        //    parameters[4].Value = model.Operation;
        //    parameters[5].Value = model.Type;
        //    parameters[6].Value = model.Flag;

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
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SeatManage.Model.T_SM_Integration model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("update T_SM_Integration set ");
        //    strSql.Append("CardNo=@CardNo,");
        //    strSql.Append("AddTime=@AddTime,");
        //    strSql.Append("Count=@Count,");
        //    strSql.Append("Operation=@Operation,");
        //    strSql.Append("Type=@Type,");
        //    strSql.Append("Flag=@Flag");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@CardNo", SqlDbType.NVarChar,50),
        //            new SqlParameter("@AddTime", SqlDbType.DateTime),
        //            new SqlParameter("@Count", SqlDbType.Int,4),
        //            new SqlParameter("@Operation", SqlDbType.Int,4),
        //            new SqlParameter("@Type", SqlDbType.Int,4),
        //            new SqlParameter("@Flag", SqlDbType.Bit,1),
        //            new SqlParameter("@ID", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = model.CardNo;
        //    parameters[1].Value = model.AddTime;
        //    parameters[2].Value = model.Count;
        //    parameters[3].Value = model.Operation;
        //    parameters[4].Value = model.Type;
        //    parameters[5].Value = model.Flag;
        //    parameters[6].Value = model.ID;

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
        ///// 删除一条数据
        ///// </summary>
        //public bool Delete(string ID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_Integration ");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = ID;

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
        //public bool DeleteList(string IDlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_Integration ");
        //    strSql.Append(" where ID in ("+IDlist + ")  ");
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
        //public SeatManage.Model.T_SM_Integration GetModel(string ID)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 ID,CardNo,AddTime,Count,Operation,Type,Flag from T_SM_Integration ");
        //    strSql.Append(" where ID=@ID ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ID", SqlDbType.NVarChar,50)};
        //    parameters[0].Value = ID;

        //    SeatManage.Model.T_SM_Integration model=new SeatManage.Model.T_SM_Integration();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
        //        {
        //            model.ID=ds.Tables[0].Rows[0]["ID"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
        //        {
        //            model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["AddTime"]!=null && ds.Tables[0].Rows[0]["AddTime"].ToString()!="")
        //        {
        //            model.AddTime=DateTime.Parse(ds.Tables[0].Rows[0]["AddTime"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Count"]!=null && ds.Tables[0].Rows[0]["Count"].ToString()!="")
        //        {
        //            model.Count=int.Parse(ds.Tables[0].Rows[0]["Count"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Operation"]!=null && ds.Tables[0].Rows[0]["Operation"].ToString()!="")
        //        {
        //            model.Operation=int.Parse(ds.Tables[0].Rows[0]["Operation"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Type"]!=null && ds.Tables[0].Rows[0]["Type"].ToString()!="")
        //        {
        //            model.Type=int.Parse(ds.Tables[0].Rows[0]["Type"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["Flag"]!=null && ds.Tables[0].Rows[0]["Flag"].ToString()!="")
        //        {
        //            if((ds.Tables[0].Rows[0]["Flag"].ToString()=="1")||(ds.Tables[0].Rows[0]["Flag"].ToString().ToLower()=="true"))
        //            {
        //                model.Flag=true;
        //            }
        //            else
        //            {
        //                model.Flag=false;
        //            }
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
			parameters[0].Value = "T_SM_Integration";
			parameters[1].Value = "ID";
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

