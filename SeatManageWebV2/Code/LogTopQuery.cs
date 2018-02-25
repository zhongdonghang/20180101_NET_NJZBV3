using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SeatManageWebV2.Code
{
    public class LogTopQuery
    {
        /// <summary>
        /// 获取选座时间排行榜
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="readerType"></param>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public static DataTable GetSeatTimeTop(DateTime startDate, DateTime endDate, int readerType, int topNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TopNum", typeof(int));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("LogCount", typeof(string));
            try
            {

                DataTable dtx = SeatManage.Bll.LogStatistical.TopSeatTimeList(topNum, startDate.ToShortDateString() + " 0:00:00", endDate.ToShortDateString() + " 23:59:59", readerType);
                if (dtx == null || dtx.Rows.Count < 1)
                {
                    return dt;
                }
                for (int i = 0; i < dtx.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["TopNum"] = i + 1;
                    dr["CardNo"] = dtx.Rows[i]["CardNo"].ToString();
                    dr["ReaderName"] = dtx.Columns.Contains("ReaderName") ? dtx.Rows[i]["ReaderName"].ToString() : "";
                    dr["TypeName"] = dtx.Columns.Contains("ReaderTypeName") ? dtx.Rows[i]["ReaderTypeName"].ToString(): "";
                    dr["DeptName"] = dtx.Columns.Contains("ReaderDeptName") ? dtx.Rows[i]["ReaderDeptName"].ToString() : "";
                    dr["LogCount"] = dtx.Rows[i]["SeatTime"].ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return dt;
            }
        }

        /// <summary>
        /// 获取选座次数排行榜
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="readerType"></param>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public static DataTable GetSeatCountTop(DateTime startDate, DateTime endDate, int readerType, int topNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TopNum", typeof(int));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("LogCount", typeof(string));
            try
            {

                DataTable dtx = SeatManage.Bll.LogStatistical.TopSeatCountList(topNum, startDate.ToShortDateString() + " 0:00:00", endDate.ToShortDateString() + " 23:59:59", readerType);
                if (dtx == null || dtx.Rows.Count < 1)
                {
                    return dt;
                }
                for (int i = 0; i < dtx.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["TopNum"] = i + 1;
                    dr["CardNo"] = dtx.Rows[i]["CardNo"].ToString();
                    dr["ReaderName"] = dtx.Columns.Contains("ReaderName") ? dtx.Rows[i]["ReaderName"].ToString() : "";
                    dr["TypeName"] = dtx.Columns.Contains("ReaderTypeName") ? dtx.Rows[i]["ReaderTypeName"].ToString() : "";
                    dr["DeptName"] = dtx.Columns.Contains("ReaderDeptName") ? dtx.Rows[i]["ReaderDeptName"].ToString() : "";
                    dr["LogCount"] = dtx.Rows[i]["SeatCount"].ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return dt;
            }
        }

        /// <summary>
        /// 获取黑名单次数排行榜
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="readerType"></param>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public static DataTable GetBlacklistTop(DateTime startDate, DateTime endDate, int readerType, int topNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TopNum", typeof(int));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("LogCount", typeof(string));
            try
            {

                DataTable dtx = SeatManage.Bll.LogStatistical.TopBlacklistList(topNum, startDate.ToShortDateString() + " 0:00:00", endDate.ToShortDateString() + " 23:59:59", readerType);
                if (dtx == null || dtx.Rows.Count < 1)
                {
                    return dt;
                }
                for (int i = 0; i < dtx.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["TopNum"] = i + 1;
                    dr["CardNo"] = dtx.Rows[i]["CardNo"].ToString();
                    dr["ReaderName"] = dtx.Columns.Contains("ReaderName") ? dtx.Rows[i]["ReaderName"].ToString() : "";
                    dr["TypeName"] = dtx.Columns.Contains("ReaderTypeName") ? dtx.Rows[i]["ReaderTypeName"].ToString() : "";
                    dr["DeptName"] = dtx.Columns.Contains("ReaderDeptName") ? dtx.Rows[i]["ReaderDeptName"].ToString() : "";
                    dr["LogCount"] = dtx.Rows[i]["BlacklistCount"].ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return dt;
            }
        }

        /// <summary>
        /// 获取违规次数排行榜
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="readerType"></param>
        /// <param name="topNum"></param>
        /// <returns></returns>
        public static DataTable GetViolateDisciplineTop(DateTime startDate, DateTime endDate, int readerType, int topNum)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TopNum", typeof(int));
            dt.Columns.Add("CardNo", typeof(string));
            dt.Columns.Add("ReaderName", typeof(string));
            dt.Columns.Add("TypeName", typeof(string));
            dt.Columns.Add("DeptName", typeof(string));
            dt.Columns.Add("LogCount", typeof(string));
            try
            {

                DataTable dtx = SeatManage.Bll.LogStatistical.TopViolateDisciplineList(topNum, startDate.ToShortDateString() + " 0:00:00", endDate.ToShortDateString() + " 23:59:59", readerType);
                if (dtx == null || dtx.Rows.Count < 1)
                {
                    return dt;
                }
                for (int i = 0; i < dtx.Rows.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["TopNum"] = i + 1;
                    dr["CardNo"] = dtx.Rows[i]["CardNo"].ToString();
                    dr["ReaderName"] = dtx.Columns.Contains("ReaderName") ? dtx.Rows[i]["ReaderName"].ToString() : "";
                    dr["TypeName"] = dtx.Columns.Contains("ReaderTypeName") ? dtx.Rows[i]["ReaderTypeName"].ToString() : "";
                    dr["DeptName"] = dtx.Columns.Contains("ReaderDeptName") ? dtx.Rows[i]["ReaderDeptName"].ToString() : "";
                    dr["LogCount"] = dtx.Rows[i]["ViolateDiscoplineCount"].ToString();
                    dt.Rows.Add(dr);
                }
                return dt;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                return dt;
            }
        }
    }
}