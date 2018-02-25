using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;//Please add references
namespace AMS.DAL
{
	/// <summary>
	/// 数据访问类:View_EnterOutLog
	/// </summary>
	public partial class View_EnterOutLog
	{
		public View_EnterOutLog()
		{}
		#region  Method



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(AMS.Model.View_EnterOutLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into View_EnterOutLog(");
			strSql.Append("Id,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,Operator,EnterOutTime,EnterOutType,Remark,SchoolName,SchoolNum)");
			strSql.Append(" values (");
			strSql.Append("@Id,@CardNo,@EnterOutNo,@EnterOutState,@TerminalNum,@ReadingRoomNum,@SeatNo,@Operator,@EnterOutTime,@EnterOutType,@Remark,@SchoolName,@SchoolNum)");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutState", SqlDbType.Int,4),
					new SqlParameter("@TerminalNum", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadingRoomNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
					new SqlParameter("@EnterOutType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.EnterOutNo;
			parameters[3].Value = model.EnterOutState;
			parameters[4].Value = model.TerminalNum;
			parameters[5].Value = model.ReadingRoomNum;
			parameters[6].Value = model.SeatNo;
			parameters[7].Value = model.Operator;
			parameters[8].Value = model.EnterOutTime;
			parameters[9].Value = model.EnterOutType;
			parameters[10].Value = model.Remark;
			parameters[11].Value = model.SchoolName;
			parameters[12].Value = model.SchoolNum;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(AMS.Model.View_EnterOutLog model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update View_EnterOutLog set ");
			strSql.Append("Id=@Id,");
			strSql.Append("CardNo=@CardNo,");
			strSql.Append("EnterOutNo=@EnterOutNo,");
			strSql.Append("EnterOutState=@EnterOutState,");
			strSql.Append("TerminalNum=@TerminalNum,");
			strSql.Append("ReadingRoomNum=@ReadingRoomNum,");
			strSql.Append("SeatNo=@SeatNo,");
			strSql.Append("Operator=@Operator,");
			strSql.Append("EnterOutTime=@EnterOutTime,");
			strSql.Append("EnterOutType=@EnterOutType,");
			strSql.Append("Remark=@Remark,");
			strSql.Append("SchoolName=@SchoolName,");
			strSql.Append("SchoolNum=@SchoolNum");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
					new SqlParameter("@Id", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutNo", SqlDbType.NVarChar,100),
					new SqlParameter("@EnterOutState", SqlDbType.Int,4),
					new SqlParameter("@TerminalNum", SqlDbType.NVarChar,50),
					new SqlParameter("@ReadingRoomNum", SqlDbType.NVarChar,50),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@Operator", SqlDbType.Int,4),
					new SqlParameter("@EnterOutTime", SqlDbType.DateTime),
					new SqlParameter("@EnterOutType", SqlDbType.Int,4),
					new SqlParameter("@Remark", SqlDbType.NVarChar,500),
					new SqlParameter("@SchoolName", SqlDbType.NVarChar,30),
					new SqlParameter("@SchoolNum", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.Id;
			parameters[1].Value = model.CardNo;
			parameters[2].Value = model.EnterOutNo;
			parameters[3].Value = model.EnterOutState;
			parameters[4].Value = model.TerminalNum;
			parameters[5].Value = model.ReadingRoomNum;
			parameters[6].Value = model.SeatNo;
			parameters[7].Value = model.Operator;
			parameters[8].Value = model.EnterOutTime;
			parameters[9].Value = model.EnterOutType;
			parameters[10].Value = model.Remark;
			parameters[11].Value = model.SchoolName;
			parameters[12].Value = model.SchoolNum;

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
		public bool Delete()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from View_EnterOutLog ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

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
		/// 得到一个对象实体
		/// </summary>
		public AMS.Model.View_EnterOutLog GetModel()
		{
			//该表无主键信息，请自定义主键/条件字段
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 Id,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,Operator,EnterOutTime,EnterOutType,Remark,SchoolName,SchoolNum from View_EnterOutLog ");
			strSql.Append(" where ");
			SqlParameter[] parameters = {
};

			AMS.Model.View_EnterOutLog model=new AMS.Model.View_EnterOutLog();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["Id"]!=null && ds.Tables[0].Rows[0]["Id"].ToString()!="")
				{
					model.Id=int.Parse(ds.Tables[0].Rows[0]["Id"].ToString());
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
				if(ds.Tables[0].Rows[0]["Operator"]!=null && ds.Tables[0].Rows[0]["Operator"].ToString()!="")
				{
					model.Operator=int.Parse(ds.Tables[0].Rows[0]["Operator"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EnterOutTime"]!=null && ds.Tables[0].Rows[0]["EnterOutTime"].ToString()!="")
				{
					model.EnterOutTime=DateTime.Parse(ds.Tables[0].Rows[0]["EnterOutTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EnterOutType"]!=null && ds.Tables[0].Rows[0]["EnterOutType"].ToString()!="")
				{
					model.EnterOutType=int.Parse(ds.Tables[0].Rows[0]["EnterOutType"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Remark"]!=null && ds.Tables[0].Rows[0]["Remark"].ToString()!="")
				{
					model.Remark=ds.Tables[0].Rows[0]["Remark"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolName"]!=null && ds.Tables[0].Rows[0]["SchoolName"].ToString()!="")
				{
					model.SchoolName=ds.Tables[0].Rows[0]["SchoolName"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SchoolNum"]!=null && ds.Tables[0].Rows[0]["SchoolNum"].ToString()!="")
				{
					model.SchoolNum=ds.Tables[0].Rows[0]["SchoolNum"].ToString();
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
			strSql.Append("select Id,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,Operator,EnterOutTime,EnterOutType,Remark,SchoolName,SchoolNum ");
			strSql.Append(" FROM View_EnterOutLog ");
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
			strSql.Append(" Id,CardNo,EnterOutNo,EnterOutState,TerminalNum,ReadingRoomNum,SeatNo,Operator,EnterOutTime,EnterOutType,Remark,SchoolName,SchoolNum ");
			strSql.Append(" FROM View_EnterOutLog ");
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
			parameters[0].Value = "View_EnterOutLog";
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

