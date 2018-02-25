/**  版本信息模板在安装目录下，可自行修改。
* T_SM_TerminalUsageStatistics.cs
*
* 功 能： N/A
* 类 名： T_SM_TerminalUsageStatistics
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2015/7/29 13:18:09   N/A    初版
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
	/// 数据访问类:T_SM_TerminalUsageStatistics
	/// </summary>
	public partial class T_SM_TerminalUsageStatistics
	{
		public T_SM_TerminalUsageStatistics()
		{}
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(TerminalUsageStatistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into T_SM_TerminalUsageStatistics(");
            strSql.Append("TerminalNo,StatisticsDate,RushCardCount,TodayPrintCount,IsChangePage,NowPagePrintCount,BeforePagePrintCount,SelectSeatCount,ReselectSeatCount,CheckBespeakCount,WaitSeatCount,ShortLeaveCount,ComeBackCount,ContinueTimeCount,LeaveCount)");
            strSql.Append(" values (");
            strSql.Append("@TerminalNo,@StatisticsDate,@RushCardCount,@TodayPrintCount,@IsChangePage,@NowPagePrintCount,@BeforePagePrintCount,@SelectSeatCount,@ReselectSeatCount,@CheckBespeakCount,@WaitSeatCount,@ShortLeaveCount,@ComeBackCount,@ContinueTimeCount,@LeaveCount)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@TerminalNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@RushCardCount", SqlDbType.Int,4),
					new SqlParameter("@TodayPrintCount", SqlDbType.Int,4),
					new SqlParameter("@IsChangePage", SqlDbType.Int,4),
					new SqlParameter("@NowPagePrintCount", SqlDbType.Int,4),
					new SqlParameter("@BeforePagePrintCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@WaitSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ComeBackCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveCount", SqlDbType.Int,4)};
            parameters[0].Value = model.TerminalNo;
            parameters[1].Value = model.StatisticsDate;
            parameters[2].Value = model.RushCardCount;
            parameters[3].Value = model.TodayPrintCount;
            parameters[4].Value = model.IsChangePage;
            parameters[5].Value = model.NowPagePrintCount;
            parameters[6].Value = model.BeforePagePrintCount;
            parameters[7].Value = model.SelectSeatCount;
            parameters[8].Value = model.ReselectSeatCount;
            parameters[9].Value = model.CheckBespeakCount;
            parameters[10].Value = model.WaitSeatCount;
            parameters[11].Value = model.ShortLeaveCount;
            parameters[12].Value = model.ComeBackCount;
            parameters[13].Value = model.ContinueTimeCount;
            parameters[14].Value = model.LeaveCount;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        public bool Update(TerminalUsageStatistics model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update T_SM_TerminalUsageStatistics set ");
            strSql.Append("TerminalNo=@TerminalNo,");
            strSql.Append("StatisticsDate=@StatisticsDate,");
            strSql.Append("RushCardCount=@RushCardCount,");
            strSql.Append("TodayPrintCount=@TodayPrintCount,");
            strSql.Append("IsChangePage=@IsChangePage,");
            strSql.Append("NowPagePrintCount=@NowPagePrintCount,");
            strSql.Append("BeforePagePrintCount=@BeforePagePrintCount,");
            strSql.Append("SelectSeatCount=@SelectSeatCount,");
            strSql.Append("ReselectSeatCount=@ReselectSeatCount,");
            strSql.Append("CheckBespeakCount=@CheckBespeakCount,");
            strSql.Append("WaitSeatCount=@WaitSeatCount,");
            strSql.Append("ShortLeaveCount=@ShortLeaveCount,");
            strSql.Append("ComeBackCount=@ComeBackCount,");
            strSql.Append("ContinueTimeCount=@ContinueTimeCount,");
            strSql.Append("LeaveCount=@LeaveCount");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@TerminalNo", SqlDbType.NVarChar,50),
					new SqlParameter("@StatisticsDate", SqlDbType.DateTime),
					new SqlParameter("@RushCardCount", SqlDbType.Int,4),
					new SqlParameter("@TodayPrintCount", SqlDbType.Int,4),
					new SqlParameter("@IsChangePage", SqlDbType.Int,4),
					new SqlParameter("@NowPagePrintCount", SqlDbType.Int,4),
					new SqlParameter("@BeforePagePrintCount", SqlDbType.Int,4),
					new SqlParameter("@SelectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ReselectSeatCount", SqlDbType.Int,4),
					new SqlParameter("@CheckBespeakCount", SqlDbType.Int,4),
					new SqlParameter("@WaitSeatCount", SqlDbType.Int,4),
					new SqlParameter("@ShortLeaveCount", SqlDbType.Int,4),
					new SqlParameter("@ComeBackCount", SqlDbType.Int,4),
					new SqlParameter("@ContinueTimeCount", SqlDbType.Int,4),
					new SqlParameter("@LeaveCount", SqlDbType.Int,4),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.TerminalNo;
            parameters[1].Value = model.StatisticsDate;
            parameters[2].Value = model.RushCardCount;
            parameters[3].Value = model.TodayPrintCount;
            parameters[4].Value = model.IsChangePage;
            parameters[5].Value = model.NowPagePrintCount;
            parameters[6].Value = model.BeforePagePrintCount;
            parameters[7].Value = model.SelectSeatCount;
            parameters[8].Value = model.ReselectSeatCount;
            parameters[9].Value = model.CheckBespeakCount;
            parameters[10].Value = model.WaitSeatCount;
            parameters[11].Value = model.ShortLeaveCount;
            parameters[12].Value = model.ComeBackCount;
            parameters[13].Value = model.ContinueTimeCount;
            parameters[14].Value = model.LeaveCount;
            parameters[15].Value = model.id;

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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_TerminalUsageStatistics ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

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
        /// 批量删除数据
        /// </summary>
        public bool DeleteList(string idlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from T_SM_TerminalUsageStatistics ");
            strSql.Append(" where id in (" + idlist + ")  ");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public TerminalUsageStatistics GetModel(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,TerminalNo,StatisticsDate,RushCardCount,TodayPrintCount,IsChangePage,NowPagePrintCount,BeforePagePrintCount,SelectSeatCount,ReselectSeatCount,CheckBespeakCount,WaitSeatCount,ShortLeaveCount,ComeBackCount,ContinueTimeCount,LeaveCount from T_SM_TerminalUsageStatistics ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)
			};
            parameters[0].Value = id;

            TerminalUsageStatistics model = new TerminalUsageStatistics();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
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
        public TerminalUsageStatistics DataRowToModel(DataRow row)
        {
            TerminalUsageStatistics model = new TerminalUsageStatistics();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["TerminalNo"] != null)
                {
                    model.TerminalNo = row["TerminalNo"].ToString();
                }
                if (row["StatisticsDate"] != null && row["StatisticsDate"].ToString() != "")
                {
                    model.StatisticsDate = DateTime.Parse(row["StatisticsDate"].ToString());
                }
                if (row["RushCardCount"] != null && row["RushCardCount"].ToString() != "")
                {
                    model.RushCardCount = int.Parse(row["RushCardCount"].ToString());
                }
                if (row["TodayPrintCount"] != null && row["TodayPrintCount"].ToString() != "")
                {
                    model.TodayPrintCount = int.Parse(row["TodayPrintCount"].ToString());
                }
                if (row["IsChangePage"] != null && row["IsChangePage"].ToString() != "")
                {
                    model.IsChangePage = int.Parse(row["IsChangePage"].ToString());
                }
                if (row["NowPagePrintCount"] != null && row["NowPagePrintCount"].ToString() != "")
                {
                    model.NowPagePrintCount = int.Parse(row["NowPagePrintCount"].ToString());
                }
                if (row["BeforePagePrintCount"] != null && row["BeforePagePrintCount"].ToString() != "")
                {
                    model.BeforePagePrintCount = int.Parse(row["BeforePagePrintCount"].ToString());
                }
                if (row["SelectSeatCount"] != null && row["SelectSeatCount"].ToString() != "")
                {
                    model.SelectSeatCount = int.Parse(row["SelectSeatCount"].ToString());
                }
                if (row["ReselectSeatCount"] != null && row["ReselectSeatCount"].ToString() != "")
                {
                    model.ReselectSeatCount = int.Parse(row["ReselectSeatCount"].ToString());
                }
                if (row["CheckBespeakCount"] != null && row["CheckBespeakCount"].ToString() != "")
                {
                    model.CheckBespeakCount = int.Parse(row["CheckBespeakCount"].ToString());
                }
                if (row["WaitSeatCount"] != null && row["WaitSeatCount"].ToString() != "")
                {
                    model.WaitSeatCount = int.Parse(row["WaitSeatCount"].ToString());
                }
                if (row["ShortLeaveCount"] != null && row["ShortLeaveCount"].ToString() != "")
                {
                    model.ShortLeaveCount = int.Parse(row["ShortLeaveCount"].ToString());
                }
                if (row["ComeBackCount"] != null && row["ComeBackCount"].ToString() != "")
                {
                    model.ComeBackCount = int.Parse(row["ComeBackCount"].ToString());
                }
                if (row["ContinueTimeCount"] != null && row["ContinueTimeCount"].ToString() != "")
                {
                    model.ContinueTimeCount = int.Parse(row["ContinueTimeCount"].ToString());
                }
                if (row["LeaveCount"] != null && row["LeaveCount"].ToString() != "")
                {
                    model.LeaveCount = int.Parse(row["LeaveCount"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,TerminalNo,StatisticsDate,RushCardCount,TodayPrintCount,IsChangePage,NowPagePrintCount,BeforePagePrintCount,SelectSeatCount,ReselectSeatCount,CheckBespeakCount,WaitSeatCount,ShortLeaveCount,ComeBackCount,ContinueTimeCount,LeaveCount ");
            strSql.Append(" FROM T_SM_TerminalUsageStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,TerminalNo,StatisticsDate,RushCardCount,TodayPrintCount,IsChangePage,NowPagePrintCount,BeforePagePrintCount,SelectSeatCount,ReselectSeatCount,CheckBespeakCount,WaitSeatCount,ShortLeaveCount,ComeBackCount,ContinueTimeCount,LeaveCount ");
            strSql.Append(" FROM T_SM_TerminalUsageStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM T_SM_TerminalUsageStatistics ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.id desc");
            }
            strSql.Append(")AS Row, T.*  from T_SM_TerminalUsageStatistics T ");
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
            parameters[0].Value = "T_SM_TerminalUsageStatistics";
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

