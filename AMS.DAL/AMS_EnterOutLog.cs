using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:AMS_EnterOutLog
	/// </summary>
	public partial class AMS_EnterOutLog
	{
		public AMS_EnterOutLog()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("Id", "AMS_EnterOutLog"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int Id)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from AMS_EnterOutLog");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(AMS.Model.AMS_EnterOutLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into AMS_EnterOutLog(");
			strSql.Append("Schoolid,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,EnterOutTime,Operator,EnterOutType,Remark)");
			strSql.Append(" values (");
			strSql.Append("@Schoolid,@CardNo,@EnterOutNo,@EnterOutState,@TerminalNum,@ReadingRoomNum,@SeatNo,@EnterOutTime,@Operator,@EnterOutType,@Remark)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@Schoolid", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutState", SqlDbType.Int,4),
					new SqlParameter("@TerminalNum", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadingRoomNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@EnterOutType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500)};
			parameters[0].Value = model.Schoolid;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.EnterOutNo;
			parameters[3].Value = model.EnterOutState;
			parameters[4].Value = model.TerminalNum;
			parameters[5].Value = model.ReadingRoomNum;
			parameters[6].Value = model.SeatNo;
			parameters[7].Value = model.EnterOutTime;
			parameters[8].Value = model.Operator;
			parameters[9].Value = model.EnterOutType;
			parameters[10].Value = model.Remark;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(AMS.Model.AMS_EnterOutLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update AMS_EnterOutLog set ");
			strSql.Append("Schoolid=@Schoolid,");
			strSql.Append("CardNo=@CardNo,");
			strSql.Append("EnterOutNo=@EnterOutNo,");
			strSql.Append("EnterOutState=@EnterOutState,");
			strSql.Append("TerminalNum=@TerminalNum,");
			strSql.Append("ReadingRoomNum=@ReadingRoomNum,");
			strSql.Append("SeatNo=@SeatNo,");
			strSql.Append("EnterOutTime=@EnterOutTime,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("EnterOutType=@EnterOutType,");
			strSql.Append("Remark=@Remark");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Schoolid", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutState", SqlDbType.Int,4),
					new SqlParameter("@TerminalNum", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadingRoomNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@EnterOutType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@Id", SqlDbType.Int,4)};
			parameters[0].Value = model.Schoolid;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.EnterOutNo;
			parameters[3].Value = model.EnterOutState;
			parameters[4].Value = model.TerminalNum;
			parameters[5].Value = model.ReadingRoomNum;
			parameters[6].Value = model.SeatNo;
			parameters[7].Value = model.EnterOutTime;
			parameters[8].Value = model.Operator;
			parameters[9].Value = model.EnterOutType;
			parameters[10].Value = model.Remark;
			parameters[11].Value = model.Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_EnterOutLog ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string Idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from AMS_EnterOutLog ");
			strSql.Append(" where Id in ("+Idlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
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
		/// 得到一个对象实体
		/// </summary>
		public AMS.Model.AMS_EnterOutLog GetModel(int Id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,Schoolid,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,EnterOutTime,Operator,EnterOutType,Remark from AMS_EnterOutLog ");
			strSql.Append(" where Id=@Id");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4)
};
			parameters[0].Value = Id;

			AMS.Model.AMS_EnterOutLog model=new AMS.Model.AMS_EnterOutLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Schoolid"]!=null && ds.Tables[0].Rows[0]["Schoolid"].ToString()!="")
				{
					model.Schoolid=int.Parse(ds.Tables[0].Rows[0]["Schoolid"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
				{
					model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EnterOutNo"]!=null && ds.Tables[0].Rows[0]["EnterOutNo"].ToString()!="")
				{
					model.EnterOutNo=ds.Tables[0].Rows[0]["EnterOutNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EnterOutState"]!=null && ds.Tables[0].Rows[0]["EnterOutState"].ToString()!="")
				{
					model.EnterOutState=int.Parse(ds.Tables[0].Rows[0]["EnterOutState"].ToString());
				}
				if(ds.Tables[0].Rows[0]["TerminalNum"]!=null && ds.Tables[0].Rows[0]["TerminalNum"].ToString()!="")
				{
					model.TerminalNum=ds.Tables[0].Rows[0]["TerminalNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReadingRoomNum"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNum"].ToString()!="")
				{
					model.ReadingRoomNum=ds.Tables[0].Rows[0]["ReadingRoomNum"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SeatNo"]!=null && ds.Tables[0].Rows[0]["SeatNo"].ToString()!="")
				{
					model.SeatNo=ds.Tables[0].Rows[0]["SeatNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["EnterOutTime"]!=null && ds.Tables[0].Rows[0]["EnterOutTime"].ToString()!="")
				{
					model.EnterOutTime=DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EnterOutType"]!=null && ds.Tables[0].Rows[0]["EnterOutType"].ToString()!="")
				{
					model.EnterOutType=int.Parse(ds.Tables[0].Rows[0]["EnterOutType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select Id,Schoolid,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,EnterOutTime,Operator,EnterOutType,Remark ");
			strSql.Append(" FROM AMS_EnterOutLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" Id,Schoolid,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,EnterOutTime,Operator,EnterOutType,Remark ");
			strSql.Append(" FROM AMS_EnterOutLog ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

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
			parameters[0].Value = "AMS_EnterOutLog";
			parameters[1].Value = "Id";
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

