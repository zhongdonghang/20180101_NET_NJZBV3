using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;
using System.Data;
using System.Data.SqlClient;
using SeatManage.EnumType;

namespace SeatManage.SeatClient.Config.Code
{
    public class DataStatistics
    {
        public delegate void EventHanleProgress(string message);
        public event EventHanleProgress Progress;
        private string _New_connectionString = "";
        /// <summary>
        /// 获取的进出记录总数
        /// </summary>
        private int _AllLogCount = 0;
        /// <summary>
        /// 统计完有效的进出记录
        /// </summary>
        private int _validFullEnterOutLogCount = 0;
        /// <summary>
        /// 无效的进出记录
        /// </summary>
        private int _ErrorEnterOutLogCount = 0;
        /// <summary>
        /// 进出记录列表
        /// </summary>
        public DataStatistics(string sqlconn)
        {
            _New_connectionString = sqlconn;
        }
        /// <summary>
        /// 开始计算
        /// </summary>
        /// <returns></returns>
        public void StartStatistics()
        {
            
            try
            {
                Statistics();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        private DataSet GetEnterOutLogs()
        {
            try
            {
                if (Progress != null)
                {
                    Progress("正在获取进出记录……");
                }
                SeatManage.ClassModel.EnterOutLogStatistics lastStatisticsLog = SeatManage.Bll.T_SM_EnterOutLogStatistics.GetLastEnterOutLogStatistics();
                int id = 0;
                if (lastStatisticsLog != null)
                {
                    id = lastStatisticsLog.LastEnterOutLogID;
                }

                StringBuilder strSql = new StringBuilder();
                strSql.Append("select EnterOutLogID,CardNo,EnterOutLogNo,TerminalNum,ReadingRoomNo,SeatNo,EnterOutState,EnterOutTime,EnterOutType,EnterFlag,Remark,MarkTime ");
                strSql.Append(" FROM T_SM_EnterOutLog_bak ");
                strSql.AppendFormat(" where EnterOutLogID>{0} order by EnterOutLogNo,EnterOutLogID", id.ToString());
                DataSet ds = Query(strSql.ToString(), _New_connectionString, null);
                return ds;
            }
            catch (Exception e)
            {
                if (Progress != null)
                {
                    Progress("获取进出记录失败！" + e.Message);
                }
                return null;
            }
        }
        /// <summary>
        /// 执行查询语句，返回DataSet
        /// </summary>
        /// <param name="SQLString">查询语句</param>
        /// <returns>DataSet</returns>
        public DataSet Query(string SQLString, string connectionString, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, SQLString, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {


                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }
        private SeatManage.ClassModel.EnterOutLogInfo DataRowToEnterOutLogBakInfo(DataRow dr)
        {
            SeatManage.ClassModel.EnterOutLogInfo model = new SeatManage.ClassModel.EnterOutLogInfo();
            model.CardNo = dr["CardNo"].ToString();
            model.EnterOutLogNo = dr["EnterOutLogNo"].ToString();
            model.EnterOutTime = Convert.ToDateTime(dr["EnterOutTime"]);
            model.Flag = (Operation)int.Parse(dr["EnterFlag"].ToString());
            model.EnterOutType = (LogStatus)int.Parse(dr["EnterOutType"].ToString());
            model.SeatNo = dr["SeatNo"].ToString();
            model.EnterOutState = (EnterOutLogType)int.Parse(dr["EnterOutState"].ToString());
            model.Remark = dr["Remark"].ToString();
            string strtemp = dr["MarkTime"].ToString();
            if (!string.IsNullOrEmpty(strtemp))
            {
                model.MarkTime = DateTime.Parse(strtemp);
            }
            else
            {
                model.MarkTime = DateTime.Parse("1900-1-1");
            }
            model.ReadingRoomNo = dr["ReadingRoomNo"].ToString();
            model.EnterOutLogID = dr["EnterOutLogID"].ToString();
            model.TerminalNum = dr["TerminalNum"].ToString();
            return model;
        }

        /// <summary>
        /// 开始计算
        /// </summary>
        private void Statistics()
        {
            DataSet ds = GetEnterOutLogs();
            if (ds == null)
            {
                return;
            }
            if (ds.Tables[0].Rows.Count == 0)
            {
                if (Progress != null)
                {
                    Progress("没有获取到进出记录！");
                }
                return;
            }
            List<SeatManage.ClassModel.EnterOutLogInfo> enterOutLogList = new List<SeatManage.ClassModel.EnterOutLogInfo>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                enterOutLogList.Add(DataRowToEnterOutLogBakInfo(ds.Tables[0].Rows[i]));
                if (Progress != null)
                {
                    Progress("转换中……" + i + "/" + ds.Tables[0].Rows.Count);
                }
            }
            SeatManage.ClassModel.EnterOutLogStatistics newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
            for (int i = 0; i < enterOutLogList.Count; i++)
            {
                if (Progress != null)
                {
                    Progress("统计中……" + i + "/" + enterOutLogList.Count + "成功" + _validFullEnterOutLogCount + "条，失败" + _ErrorEnterOutLogCount + "条");
                }
                //判断状态
                switch (enterOutLogList[i].EnterOutState)
                {
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.BookAdmission;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.WaitAdmission;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReSelect;
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        if (enterOutLogList[i].Flag == SeatManage.EnumType.Operation.Admin)
                        {
                            newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.AdminAllocation;
                        }
                        else
                        {
                            newStatistics.SelectSeat = SeatManage.ClassModel.EnterOutLogSelectSeatMode.ReadCardSelect;
                        }
                        newStatistics.CardNo = enterOutLogList[i].CardNo;
                        newStatistics.SeatNo = enterOutLogList[i].SeatNo;
                        newStatistics.ReadingRoomNo = enterOutLogList[i].ReadingRoomNo;
                        newStatistics.EnterOutLogNo = enterOutLogList[i].EnterOutLogNo;
                        newStatistics.SelectSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        newStatistics.ContinueTimeCount++;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        newStatistics.ShortLeaveCount++;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        newStatistics.AllOperationCount++;
                        continue;
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        switch (enterOutLogList[i].Flag)
                        {
                            case SeatManage.EnumType.Operation.Admin:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.AdminReleased;
                                break;
                            case SeatManage.EnumType.Operation.Reader:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ReaderReleased;
                                break;
                            case SeatManage.EnumType.Operation.Service:
                                newStatistics.LeaveSeat = SeatManage.ClassModel.EnterOutLogLeaveSeatMode.ServerReleased;
                                break;
                        }
                        newStatistics.LastEnterOutLogID = int.Parse(enterOutLogList[i].EnterOutLogID);
                        newStatistics.LeaveSeatTime = enterOutLogList[i].EnterOutTime;
                        break;
                }
                //操作次数赋值
                newStatistics.AllOperationCount++;
                switch (enterOutLogList[i].Flag)
                {
                    case SeatManage.EnumType.Operation.Admin: newStatistics.AdminOperationCount++; break;
                    case SeatManage.EnumType.Operation.OtherReader: newStatistics.OtherOperationCount++; break;
                    case SeatManage.EnumType.Operation.Reader: newStatistics.ReaderOperationCount++; break;
                    case SeatManage.EnumType.Operation.Service: newStatistics.ServerOperationCount++; break;
                }
                //判断日期是否正确
                if (newStatistics.AllOperationCount > 1)
                {
                    if (enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.Leave || i + 1 >= enterOutLogList.Count || enterOutLogList[i + 1].EnterOutLogNo != newStatistics.EnterOutLogNo)
                    {
                        if (newStatistics.LeaveSeatTime < newStatistics.SelectSeatTime && newStatistics.SelectSeatTime < enterOutLogList[i].EnterOutTime)
                        {
                            newStatistics.LeaveSeatTime = enterOutLogList[i].EnterOutTime;
                        }
                        if ((newStatistics.LeaveSeatTime.Date - newStatistics.SelectSeatTime.Date).TotalDays != 0)
                        {
                            newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                            _ErrorEnterOutLogCount++;
                            continue;
                        }
                        newStatistics.SeatTime = int.Parse((newStatistics.LeaveSeatTime - newStatistics.SelectSeatTime).TotalMinutes.ToString().Split('.')[0]);
                        try
                        {
                            if (SeatManage.Bll.T_SM_EnterOutLogStatistics.AddEnterOutLogStatistics(newStatistics) == SeatManage.EnumType.HandleResult.Failed)
                            {
                                throw new Exception("添加进出记录统计失败！");
                            }
                            _validFullEnterOutLogCount++;
                        }
                        catch (Exception ex)
                        {
                            WriteLog.Write(ex.Message);
                        }
                        newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                    }
                }
                else if (enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.Leave
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ComeBack
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ContinuedTime
                    || enterOutLogList[i].EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                {
                    newStatistics = new SeatManage.ClassModel.EnterOutLogStatistics();
                    _ErrorEnterOutLogCount++;
                    continue;
                }
            }
            if (Progress != null)
            {
                Progress(string.Format("计算进出记录数据{0}条，有效计算记录{1}条，无效数据{2}条", _AllLogCount, _validFullEnterOutLogCount, _ErrorEnterOutLogCount));
            }
        }
    }
}
