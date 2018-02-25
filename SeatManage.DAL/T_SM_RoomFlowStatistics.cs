/**  版本信息模板在安装目录下，可自行修改。
* T_SM_RoomFlowStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_RoomFlowStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/29 13:18:08   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
using SeatManage.ClassModel;

//Please add references
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_RoomFlowStatistics
	/// </summary>
	public partial class T_SM_RoomFlowStatistics
	{
		public T_SM_RoomFlowStatistics()
		{}
		#region  BasicMethod



		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(RoomFlowStatistics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SM_RoomFlowStatistics(");
			strSql.Append("ReadingRoomNo,StatisticsDate,EnterFlow,OutFlow,OnSeat,SelectFlow,ReselectFlow,BespeakCheckFlow,WaitSelectFlow,ShortLeaveFlow,ComeBackFlow,ContinueFlow,LeaveFlow)");
			strSql.Append(" values (");
			strSql.Append("@ReadingRoomNo,@StatisticsDate,@EnterFlow,@OutFlow,@OnSeat,@SelectFlow,@ReselectFlow,@BespeakCheckFlow,@WaitSelectFlow,@ShortLeaveFlow,@ComeBackFlow,@ContinueFlow,@LeaveFlow)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@EnterFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@OutFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@OnSeat", SqlDbType.NVarChar,200),
					new SqlParameter("@SelectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ReselectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@BespeakCheckFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@WaitSelectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ShortLeaveFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ComeBackFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ContinueFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@LeaveFlow", SqlDbType.NVarChar,200)};
			parameters[0].Value = model.ReadingRoomNo;
			parameters[1].Value = model.StatisticsDate;
			parameters[2].Value = model.EnterFlow;
			parameters[3].Value = model.OutFlow;
			parameters[4].Value = model.OnSeat;
			parameters[5].Value = model.SelectFlow;
			parameters[6].Value = model.ReselectFlow;
			parameters[7].Value = model.BespeakCheckFlow;
			parameters[8].Value = model.WaitSelectFlow;
			parameters[9].Value = model.ShortLeaveFlow;
			parameters[10].Value = model.ComeBackFlow;
			parameters[11].Value = model.ContinueFlow;
			parameters[12].Value = model.LeaveFlow;

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
		public bool Update(RoomFlowStatistics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SM_RoomFlowStatistics set ");
			strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
			strSql.Append("StatisticsDate=@StatisticsDate,");
			strSql.Append("EnterFlow=@EnterFlow,");
			strSql.Append("OutFlow=@OutFlow,");
			strSql.Append("OnSeat=@OnSeat,");
			strSql.Append("SelectFlow=@SelectFlow,");
			strSql.Append("ReselectFlow=@ReselectFlow,");
			strSql.Append("BespeakCheckFlow=@BespeakCheckFlow,");
			strSql.Append("WaitSelectFlow=@WaitSelectFlow,");
			strSql.Append("ShortLeaveFlow=@ShortLeaveFlow,");
			strSql.Append("ComeBackFlow=@ComeBackFlow,");
			strSql.Append("ContinueFlow=@ContinueFlow,");
			strSql.Append("LeaveFlow=@LeaveFlow");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@EnterFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@OutFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@OnSeat", SqlDbType.NVarChar,200),
					new SqlParameter("@SelectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ReselectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@BespeakCheckFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@WaitSelectFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ShortLeaveFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ComeBackFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@ContinueFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@LeaveFlow", SqlDbType.NVarChar,200),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.ReadingRoomNo;
			parameters[1].Value = model.StatisticsDate;
			parameters[2].Value = model.EnterFlow;
			parameters[3].Value = model.OutFlow;
			parameters[4].Value = model.OnSeat;
			parameters[5].Value = model.SelectFlow;
			parameters[6].Value = model.ReselectFlow;
			parameters[7].Value = model.BespeakCheckFlow;
			parameters[8].Value = model.WaitSelectFlow;
			parameters[9].Value = model.ShortLeaveFlow;
			parameters[10].Value = model.ComeBackFlow;
			parameters[11].Value = model.ContinueFlow;
			parameters[12].Value = model.LeaveFlow;
			parameters[13].Value = model.id;

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
		public bool Delete(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SM_RoomFlowStatistics ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

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
		public bool DeleteList(string idlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SM_RoomFlowStatistics ");
			strSql.Append(" where id in ("+idlist + ")  ");
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
		public RoomFlowStatistics GetModel(int id)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 id,ReadingRoomNo,StatisticsDate,EnterFlow,OutFlow,OnSeat,SelectFlow,ReselectFlow,BespeakCheckFlow,WaitSelectFlow,ShortLeaveFlow,ComeBackFlow,ContinueFlow,LeaveFlow from T_SM_RoomFlowStatistics ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
			parameters[0].Value = id;

			RoomFlowStatistics model=new RoomFlowStatistics();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public RoomFlowStatistics DataRowToModel(DataRow row)
		{
			RoomFlowStatistics model=new RoomFlowStatistics();
			if (row != null)
			{
				if(row["id"]!=null && row["id"].ToString()!="")
				{
					model.id=int.Parse(row["id"].ToString());
				}
				if(row["ReadingRoomNo"]!=null)
				{
					model.ReadingRoomNo=row["ReadingRoomNo"].ToString();
				}
				if(row["StatisticsDate"]!=null && row["StatisticsDate"].ToString()!="")
				{
					model.StatisticsDate=DateTime.Parse(row["StatisticsDate"].ToString());
				}
				if(row["EnterFlow"]!=null)
				{
					model.EnterFlow=row["EnterFlow"].ToString();
				}
				if(row["OutFlow"]!=null)
				{
					model.OutFlow=row["OutFlow"].ToString();
				}
				if(row["OnSeat"]!=null)
				{
					model.OnSeat=row["OnSeat"].ToString();
				}
				if(row["SelectFlow"]!=null)
				{
					model.SelectFlow=row["SelectFlow"].ToString();
				}
				if(row["ReselectFlow"]!=null)
				{
					model.ReselectFlow=row["ReselectFlow"].ToString();
				}
				if(row["BespeakCheckFlow"]!=null)
				{
					model.BespeakCheckFlow=row["BespeakCheckFlow"].ToString();
				}
				if(row["WaitSelectFlow"]!=null)
				{
					model.WaitSelectFlow=row["WaitSelectFlow"].ToString();
				}
				if(row["ShortLeaveFlow"]!=null)
				{
					model.ShortLeaveFlow=row["ShortLeaveFlow"].ToString();
				}
				if(row["ComeBackFlow"]!=null)
				{
					model.ComeBackFlow=row["ComeBackFlow"].ToString();
				}
				if(row["ContinueFlow"]!=null)
				{
					model.ContinueFlow=row["ContinueFlow"].ToString();
				}
				if(row["LeaveFlow"]!=null)
				{
					model.LeaveFlow=row["LeaveFlow"].ToString();
				}
			}
			return model;
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,ReadingRoomNo,StatisticsDate,EnterFlow,OutFlow,OnSeat,SelectFlow,ReselectFlow,BespeakCheckFlow,WaitSelectFlow,ShortLeaveFlow,ComeBackFlow,ContinueFlow,LeaveFlow ");
			strSql.Append(" FROM T_SM_RoomFlowStatistics ");
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
			strSql.Append(" id,ReadingRoomNo,StatisticsDate,EnterFlow,OutFlow,OnSeat,SelectFlow,ReselectFlow,BespeakCheckFlow,WaitSelectFlow,ShortLeaveFlow,ComeBackFlow,ContinueFlow,LeaveFlow ");
			strSql.Append(" FROM T_SM_RoomFlowStatistics ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) FROM T_SM_RoomFlowStatistics ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
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
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("SELECT * FROM ( ");
			strSql.Append(" SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append("order by T." + orderby );
			}
			else
			{
				strSql.Append("order by T.id desc");
			}
			strSql.Append(")AS Row, T.*  from T_SM_RoomFlowStatistics T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append(" WHERE " + strWhere);
			}
			strSql.Append(" ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
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
			parameters[0].Value = "T_SM_RoomFlowStatistics";
			parameters[1].Value = "id";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  BasicMethod
		#region  ExtensionMethod

		#endregion  ExtensionMethod
	}
}

