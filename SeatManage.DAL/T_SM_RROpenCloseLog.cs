using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_RROpenCloseLog
	/// </summary>
	public partial class T_SM_RROpenCloseLog
	{
		public T_SM_RROpenCloseLog()
		{}
		#region  Method

		 

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_RROpenCloseLog");
			strSql.Append(" where id=@id ");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,SqlParameter[] parameters)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,ReadingRoomNo,OperateTime,OperateNo,OpenCloseState,OpenCloseType ");
            strSql.Append(" FROM T_SM_RROpenCloseLog ");
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
            strSql.Append(" id,ReadingRoomNo,OperateTime,OperateNo,OpenCloseState,OpenCloseType ");
            strSql.Append(" FROM T_SM_RROpenCloseLog ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(ReadingRoomOpenCloseLogInfo model, ref int newLogId)
        {
            //TODO:不跟据阅览室状态添加进出记录
            SqlParameter[] parameters = new SqlParameter[8];
            parameters[0] = new SqlParameter("@ExcResult", SqlDbType.Int);
            parameters[0].Direction = ParameterDirection.Output;

            parameters[1] = new SqlParameter("@id", model.ID);
            parameters[2] = new SqlParameter("@ReadingRoomNo", model.ReadingRoomNo);
            parameters[3] = new SqlParameter("@OperateTime", model.OperateTime);
            parameters[4] = new SqlParameter("@OperateNo", model.OperateNo);
            parameters[5] = new SqlParameter("@OpenCloseState", (int)model.OpenCloseState);
            parameters[6] = new SqlParameter("@OpenCloseType", (int)model.Logstatus);
            parameters[7] = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            parameters[7].Direction = ParameterDirection.ReturnValue;
            DbHelperSQL.Execute_Proc("Proc_AddRROpenCloseLog", parameters);
            string id = parameters[7].Value.ToString();
            if (!string.IsNullOrEmpty(id))
            {
                newLogId = int.Parse(id);
            }
            return (int)parameters[0].Value;
        }
        ///// <summary>
        ///// 更新一条数据
        ///// </summary>
        //public bool Update(SeatManage.Model.T_SM_RROpenCloseLog model)
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("update T_SM_RROpenCloseLog set ");
        //    strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
        //    strSql.Append("OperateTime=@OperateTime,");
        //    strSql.Append("OperateNo=@OperateNo,");
        //    strSql.Append("OpenCloseState=@OpenCloseState,");
        //    strSql.Append("OpenCloseType=@OpenCloseType");
        //    strSql.Append(" where id=@id ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@ReadingRoomNo", SqlDbType.Int,4),
        //            new SqlParameter("@OperateTime", SqlDbType.DateTime),
        //            new SqlParameter("@OperateNo", SqlDbType.NVarChar,100),
        //            new SqlParameter("@OpenCloseState", SqlDbType.Int,4),
        //            new SqlParameter("@OpenCloseType", SqlDbType.Int,4),
        //            new SqlParameter("@id", SqlDbType.Int,4)};
        //    parameters[0].Value = model.ReadingRoomNo;
        //    parameters[1].Value = model.OperateTime;
        //    parameters[2].Value = model.OperateNo;
        //    parameters[3].Value = model.OpenCloseState;
        //    parameters[4].Value = model.OpenCloseType;
        //    parameters[5].Value = model.id;

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
        //public bool Delete(int id)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_RROpenCloseLog ");
        //    strSql.Append(" where id=@id ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@id", SqlDbType.Int,4)};
        //    parameters[0].Value = id;

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
        //public bool DeleteList(string idlist )
        //{
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("delete from T_SM_RROpenCloseLog ");
        //    strSql.Append(" where id in ("+idlist + ")  ");
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
        //public SeatManage.Model.T_SM_RROpenCloseLog GetModel(int id)
        //{
			
        //    StringBuilder strSql=new StringBuilder();
        //    strSql.Append("select  top 1 id,ReadingRoomNo,OperateTime,OperateNo,OpenCloseState,OpenCloseType from T_SM_RROpenCloseLog ");
        //    strSql.Append(" where id=@id ");
        //    SqlParameter[] parameters = {
        //            new SqlParameter("@id", SqlDbType.Int,4)};
        //    parameters[0].Value = id;

        //    SeatManage.Model.T_SM_RROpenCloseLog model=new SeatManage.Model.T_SM_RROpenCloseLog();
        //    DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
        //    if(ds.Tables[0].Rows.Count>0)
        //    {
        //        if(ds.Tables[0].Rows[0]["id"]!=null && ds.Tables[0].Rows[0]["id"].ToString()!="")
        //        {
        //            model.id=int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["ReadingRoomNo"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString()!="")
        //        {
        //            model.ReadingRoomNo=int.Parse(ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["OperateTime"]!=null && ds.Tables[0].Rows[0]["OperateTime"].ToString()!="")
        //        {
        //            model.OperateTime=DateTime.Parse(ds.Tables[0].Rows[0]["OperateTime"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["OperateNo"]!=null && ds.Tables[0].Rows[0]["OperateNo"].ToString()!="")
        //        {
        //            model.OperateNo=ds.Tables[0].Rows[0]["OperateNo"].ToString();
        //        }
        //        if(ds.Tables[0].Rows[0]["OpenCloseState"]!=null && ds.Tables[0].Rows[0]["OpenCloseState"].ToString()!="")
        //        {
        //            model.OpenCloseState=int.Parse(ds.Tables[0].Rows[0]["OpenCloseState"].ToString());
        //        }
        //        if(ds.Tables[0].Rows[0]["OpenCloseType"]!=null && ds.Tables[0].Rows[0]["OpenCloseType"].ToString()!="")
        //        {
        //            model.OpenCloseType=int.Parse(ds.Tables[0].Rows[0]["OpenCloseType"].ToString());
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
			parameters[0].Value = "T_SM_RROpenCloseLog";
			parameters[1].Value = "id";
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

