﻿using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DBUtility;
namespace SeatManage.DAL
{
	/// <summary>
	/// 数据访问类:T_SM_EnterOutLogStatistics
	/// </summary>
	public partial class T_SM_EnterOutLogStatistics
	{
		public T_SM_EnterOutLogStatistics()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "T_SM_EnterOutLogStatistics"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select count(1) from T_SM_EnterOutLogStatistics");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(SeatManage.ClassModel.EnterOutLogStatistics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("insert into T_SM_EnterOutLogStatistics(");
			strSql.Append("EnterOutLogNo,LastEnterOutID,CardNo,SeatNo,ReadingRoomNo,SelectSeatMode,LeaveModel,SelectSeatTime,LeaveSeatTime,SeatTime,ShortLeaveCount,ContinueTimeCount,AllOperationCount,AdminOperationCount,ReaderOperationCount,OtherOperationCount,ServerOperationCount,IsViolation)");
			strSql.Append(" values (");
			strSql.Append("@EnterOutLogNo,@LastEnterOutID,@CardNo,@SeatNo,@ReadingRoomNo,@SelectSeatMode,@LeaveModel,@SelectSeatTime,@LeaveSeatTime,@SeatTime,@ShortLeaveCount,@ContinueTimeCount,@AllOperationCount,@AdminOperationCount,@ReaderOperationCount,@OtherOperationCount,@ServerOperationCount,@IsViolation)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterOutLogNo", SqlDbType.NVarChar,100),
					new SqlParameter("@LastEnterOutID", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@SelectSeatMode", SqlDbType.Int,4),
					new SqlParameter("@LeaveModel", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatTime", SqlDbType.DateTime),
					new SqlParameter("@LeaveSeatTime", SqlDbType.DateTime),
					new SqlParameter("@SeatTime", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@AllOperationCount", SqlDbType.Int,4),
					new SqlParameter("@AdminOperationCount", SqlDbType.Int,4),
					new SqlParameter("@ReaderOperationCount", SqlDbType.Int,4),
					new SqlParameter("@OtherOperationCount", SqlDbType.Int,4),
					new SqlParameter("@ServerOperationCount", SqlDbType.Int,4),
					new SqlParameter("@IsViolation", SqlDbType.Bit,1)};
			parameters[0].Value = model.EnterOutLogNo;
            parameters[1].Value = model.LastEnterOutLogID;
			parameters[2].Value = model.CardNo;
			parameters[3].Value = model.SeatNo;
			parameters[4].Value = model.ReadingRoomNo;
            parameters[5].Value = (int)model.SelectSeat;
            parameters[6].Value = (int)model.LeaveSeat;
			parameters[7].Value = model.SelectSeatTime;
			parameters[8].Value = model.LeaveSeatTime;
			parameters[9].Value = model.SeatTime;
			parameters[10].Value = model.ShortLeaveCount;
			parameters[11].Value = model.ContinueTimeCount;
			parameters[12].Value = model.AllOperationCount;
			parameters[13].Value = model.AdminOperationCount;
			parameters[14].Value = model.ReaderOperationCount;
			parameters[15].Value = model.OtherOperationCount;
			parameters[16].Value = model.ServerOperationCount;
			parameters[17].Value = model.IsViolation;

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
        public bool Update(SeatManage.ClassModel.EnterOutLogStatistics model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("update T_SM_EnterOutLogStatistics set ");
			strSql.Append("EnterOutLogNo=@EnterOutLogNo,");
			strSql.Append("LastEnterOutID=@LastEnterOutID,");
			strSql.Append("CardNo=@CardNo,");
			strSql.Append("SeatNo=@SeatNo,");
			strSql.Append("ReadingRoomNo=@ReadingRoomNo,");
			strSql.Append("SelectSeatMode=@SelectSeatMode,");
			strSql.Append("LeaveModel=@LeaveModel,");
			strSql.Append("SelectSeatTime=@SelectSeatTime,");
			strSql.Append("LeaveSeatTime=@LeaveSeatTime,");
			strSql.Append("SeatTime=@SeatTime,");
			strSql.Append("ShortLeaveCount=@ShortLeaveCount,");
			strSql.Append("ContinueTimeCount=@ContinueTimeCount,");
			strSql.Append("AllOperationCount=@AllOperationCount,");
			strSql.Append("AdminOperationCount=@AdminOperationCount,");
			strSql.Append("ReaderOperationCount=@ReaderOperationCount,");
			strSql.Append("OtherOperationCount=@OtherOperationCount,");
			strSql.Append("ServerOperationCount=@ServerOperationCount,");
			strSql.Append("IsViolation=@IsViolation");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@EnterOutLogNo", SqlDbType.NVarChar,100),
					new SqlParameter("@LastEnterOutID", SqlDbType.Int,4),
					new SqlParameter("@CardNo", SqlDbType.NVarChar,20),
					new SqlParameter("@SeatNo", SqlDbType.NVarChar,100),
					new SqlParameter("@ReadingRoomNo", SqlDbType.NVarChar,50),
					new SqlParameter("@SelectSeatMode", SqlDbType.Int,4),
					new SqlParameter("@LeaveModel", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatTime", SqlDbType.DateTime),
					new SqlParameter("@LeaveSeatTime", SqlDbType.DateTime),
					new SqlParameter("@SeatTime", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@AllOperationCount", SqlDbType.Int,4),
					new SqlParameter("@AdminOperationCount", SqlDbType.Int,4),
					new SqlParameter("@ReaderOperationCount", SqlDbType.Int,4),
					new SqlParameter("@OtherOperationCount", SqlDbType.Int,4),
					new SqlParameter("@ServerOperationCount", SqlDbType.Int,4),
					new SqlParameter("@IsViolation", SqlDbType.Bit,1),
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.EnterOutLogNo;
			parameters[1].Value = model.LastEnterOutLogID;
			parameters[2].Value = model.CardNo;
			parameters[3].Value = model.SeatNo;
			parameters[4].Value = model.ReadingRoomNo;
            parameters[5].Value = (int)model.SelectSeat;
            parameters[6].Value = (int)model.LeaveSeat;
			parameters[7].Value = model.SelectSeatTime;
			parameters[8].Value = model.LeaveSeatTime;
			parameters[9].Value = model.SeatTime;
			parameters[10].Value = model.ShortLeaveCount;
			parameters[11].Value = model.ContinueTimeCount;
			parameters[12].Value = model.AllOperationCount;
			parameters[13].Value = model.AdminOperationCount;
			parameters[14].Value = model.ReaderOperationCount;
			parameters[15].Value = model.OtherOperationCount;
			parameters[16].Value = model.ServerOperationCount;
			parameters[17].Value = model.IsViolation;
			parameters[18].Value = model.ID;

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
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SM_EnterOutLogStatistics ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

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
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("delete from T_SM_EnterOutLogStatistics ");
			strSql.Append(" where ID in ("+IDlist + ")  ");
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
        public SeatManage.ClassModel.EnterOutLogStatistics GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select  top 1 ID,EnterOutLogNo,LastEnterOutID,CardNo,SeatNo,ReadingRoomNo,SelectSeatMode,LeaveModel,SelectSeatTime,LeaveSeatTime,SeatTime,ShortLeaveCount,ContinueTimeCount,AllOperationCount,AdminOperationCount,ReaderOperationCount,OtherOperationCount,ServerOperationCount,IsViolation from T_SM_EnterOutLogStatistics ");
			strSql.Append(" where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
};
			parameters[0].Value = ID;

            SeatManage.ClassModel.EnterOutLogStatistics model = new SeatManage.ClassModel.EnterOutLogStatistics();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["EnterOutLogNo"]!=null && ds.Tables[0].Rows[0]["EnterOutLogNo"].ToString()!="")
				{
					model.EnterOutLogNo=ds.Tables[0].Rows[0]["EnterOutLogNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["LastEnterOutID"]!=null && ds.Tables[0].Rows[0]["LastEnterOutID"].ToString()!="")
				{
					model.LastEnterOutLogID=int.Parse(ds.Tables[0].Rows[0]["LastEnterOutID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["CardNo"]!=null && ds.Tables[0].Rows[0]["CardNo"].ToString()!="")
				{
					model.CardNo=ds.Tables[0].Rows[0]["CardNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SeatNo"]!=null && ds.Tables[0].Rows[0]["SeatNo"].ToString()!="")
				{
					model.SeatNo=ds.Tables[0].Rows[0]["SeatNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["ReadingRoomNo"]!=null && ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString()!="")
				{
					model.ReadingRoomNo=ds.Tables[0].Rows[0]["ReadingRoomNo"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SelectSeatMode"]!=null && ds.Tables[0].Rows[0]["SelectSeatMode"].ToString()!="")
				{
                    model.SelectSeat = (SeatManage.ClassModel.EnterOutLogSelectSeatMode)int.Parse(ds.Tables[0].Rows[0]["SelectSeatMode"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LeaveModel"]!=null && ds.Tables[0].Rows[0]["LeaveModel"].ToString()!="")
				{
                    model.LeaveSeat = (SeatManage.ClassModel.EnterOutLogLeaveSeatMode)int.Parse(ds.Tables[0].Rows[0]["LeaveModel"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SelectSeatTime"]!=null && ds.Tables[0].Rows[0]["SelectSeatTime"].ToString()!="")
				{
					model.SelectSeatTime=DateTime.Parse(ds.Tables[0].Rows[0]["SelectSeatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["LeaveSeatTime"]!=null && ds.Tables[0].Rows[0]["LeaveSeatTime"].ToString()!="")
				{
					model.LeaveSeatTime=DateTime.Parse(ds.Tables[0].Rows[0]["LeaveSeatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SeatTime"]!=null && ds.Tables[0].Rows[0]["SeatTime"].ToString()!="")
				{
					model.SeatTime=int.Parse(ds.Tables[0].Rows[0]["SeatTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ShortLeaveCount"]!=null && ds.Tables[0].Rows[0]["ShortLeaveCount"].ToString()!="")
				{
					model.ShortLeaveCount=int.Parse(ds.Tables[0].Rows[0]["ShortLeaveCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ContinueTimeCount"]!=null && ds.Tables[0].Rows[0]["ContinueTimeCount"].ToString()!="")
				{
					model.ContinueTimeCount=int.Parse(ds.Tables[0].Rows[0]["ContinueTimeCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AllOperationCount"]!=null && ds.Tables[0].Rows[0]["AllOperationCount"].ToString()!="")
				{
					model.AllOperationCount=int.Parse(ds.Tables[0].Rows[0]["AllOperationCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["AdminOperationCount"]!=null && ds.Tables[0].Rows[0]["AdminOperationCount"].ToString()!="")
				{
					model.AdminOperationCount=int.Parse(ds.Tables[0].Rows[0]["AdminOperationCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReaderOperationCount"]!=null && ds.Tables[0].Rows[0]["ReaderOperationCount"].ToString()!="")
				{
					model.ReaderOperationCount=int.Parse(ds.Tables[0].Rows[0]["ReaderOperationCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["OtherOperationCount"]!=null && ds.Tables[0].Rows[0]["OtherOperationCount"].ToString()!="")
				{
					model.OtherOperationCount=int.Parse(ds.Tables[0].Rows[0]["OtherOperationCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ServerOperationCount"]!=null && ds.Tables[0].Rows[0]["ServerOperationCount"].ToString()!="")
				{
					model.ServerOperationCount=int.Parse(ds.Tables[0].Rows[0]["ServerOperationCount"].ToString());
				}
				if(ds.Tables[0].Rows[0]["IsViolation"]!=null && ds.Tables[0].Rows[0]["IsViolation"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["IsViolation"].ToString()=="1")||(ds.Tables[0].Rows[0]["IsViolation"].ToString().ToLower()=="true"))
					{
						model.IsViolation=true;
					}
					else
					{
						model.IsViolation=false;
					}
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ID,EnterOutLogNo,LastEnterOutID,CardNo,SeatNo,ReadingRoomNo,SelectSeatMode,LeaveModel,SelectSeatTime,LeaveSeatTime,SeatTime,ShortLeaveCount,ContinueTimeCount,AllOperationCount,AdminOperationCount,ReaderOperationCount,OtherOperationCount,ServerOperationCount,IsViolation,ReadingRoomName,LibraryName,SchoolName,LibraryNo,SchoolNo,ReaderName,ReaderDeptName,ReaderTypeName ");
            strSql.Append(" FROM ViewEnterOutLogStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" ID,EnterOutLogNo,LastEnterOutID,CardNo,SeatNo,ReadingRoomNo,SelectSeatMode,LeaveModel,SelectSeatTime,LeaveSeatTime,SeatTime,ShortLeaveCount,ContinueTimeCount,AllOperationCount,AdminOperationCount,ReaderOperationCount,OtherOperationCount,ServerOperationCount,IsViolation,ReadingRoomName,LibraryName,SchoolName,LibraryNo,SchoolNo,ReaderName,ReaderDeptName,ReaderTypeName ");
            strSql.Append(" FROM ViewEnterOutLogStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
			parameters[0].Value = "T_SM_EnterOutLogStatistics";
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

