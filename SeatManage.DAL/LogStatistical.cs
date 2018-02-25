using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using DBUtility;

namespace SeatManage.DAL
{
    public class LogStatistical
    {
        /// <summary>
        /// 获取在座时长最多的人
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public DataSet GetTopSeatTimeList(int Top, string strWhere, string groupBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("   CardNo," + groupBy + ",Sum(SeatTime) as SeatTime");
            strSql.Append(" FROM ViewEnterOutLogStatistics ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by CardNo," + groupBy);
            strSql.Append(" order by SeatTime desc");
            return DbHelperSQL.Query(strSql.ToString(), null);
        }

        /// <summary>
        /// 获取选座次数最多的人
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public DataSet GetTopSelectTimeList(int Top, string strWhere, string groupBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("   CardNo," + groupBy + ",Count(ID) as SeatCount");
            strSql.Append(" FROM ViewEnterOutLogStatistics ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by CardNo," + groupBy);
            strSql.Append(" order by SeatCount desc");
            return DbHelperSQL.Query(strSql.ToString(), null);
        }

        /// <summary>
        /// 获取选座次数最多的人
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public DataSet GetTopBlacklistList(int Top, string strWhere, string groupBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("   CardNo," + groupBy + ",Count(BlacklistID) as BlacklistCount");
            strSql.Append(" FROM ViewBlacklist ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by CardNo," + groupBy);
            strSql.Append(" order by BlacklistCount desc");
            return DbHelperSQL.Query(strSql.ToString(), null);
        }

        /// <summary>
        /// 获取选座次数最多的人
        /// </summary>
        /// <param name="Top"></param>
        /// <param name="strWhere"></param>
        /// <param name="groupBy"></param>
        /// <returns></returns>
        public DataSet GetTopViolateDiscoplineList(int Top, string strWhere, string groupBy)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append("   CardNo," + groupBy + ",Count(violateID) as ViolateDiscoplineCount");
            strSql.Append(" FROM ViewViolateDiscopline ");
            if (!string.IsNullOrEmpty(strWhere))
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" group by CardNo," + groupBy);
            strSql.Append(" order by ViolateDiscoplineCount desc");
            return DbHelperSQL.Query(strSql.ToString(), null);
        }
    }
}
