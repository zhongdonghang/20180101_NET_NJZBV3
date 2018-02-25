using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.IWCFService;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace WcfServiceForSeatManage
{
    public partial class SeatManageDateService : ISeatManageService
    {
        private SeatManage.DAL.T_SM_RoomFlowStatistics t_sm_RoomFlowStatistics = new SeatManage.DAL.T_SM_RoomFlowStatistics();
        private SeatManage.DAL.T_SM_RoomUsageStatistics t_sm_RoomUsageStatistics = new SeatManage.DAL.T_SM_RoomUsageStatistics();
        private SeatManage.DAL.T_SM_TerminalFlowStatistics t_sm_TerminalFlowStatistics = new SeatManage.DAL.T_SM_TerminalFlowStatistics();
        private SeatManage.DAL.T_SM_TerminalUsageStatistics t_sm_TerminalUsageStatistics = new SeatManage.DAL.T_SM_TerminalUsageStatistics();
        /// <summary>
        /// 添加阅览室使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRoomUsageStatistics(RoomUsageStatistics model)
        {
            try
            {
                return t_sm_RoomUsageStatistics.Add(model) > 0;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 添加阅览室使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddRoomFlowStatistics(RoomFlowStatistics model)
        {
            try
            {
                return t_sm_RoomFlowStatistics.Add(model) > 0;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 添加设备使用情况记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddTerminalUsageStatistics(TerminalUsageStatistics model)
        {
            try
            {
                return t_sm_TerminalUsageStatistics.Add(model) > 0;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 添加设备使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool AddTerminalFlowStatistics(TerminalFlowStatistics model)
        {
            try
            {
                return t_sm_TerminalFlowStatistics.Add(model) > 0;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取阅览室使用情况记录
        /// </summary>
        /// <param name="roomsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RoomUsageStatistics> GetRoomUsageStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (roomsNo != null)
                {
                    for (int i = 0; i < roomsNo.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere.Append(string.Format(" ReadingRoomNo in ('{0}'", roomsNo[i]));
                        }
                        else if (i != roomsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}'  ", roomsNo[i]));
                        }
                        if (i == roomsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}')", roomsNo[i]));
                        }
                    }
                }
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate>='{0}'" : " and StatisticsDate>='{0}'", startDate);
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate<='{0}'" : " and StatisticsDate<='{0}'", endDate);
                List<RoomUsageStatistics> modelList = new List<RoomUsageStatistics>();
                DataSet ds = t_sm_RoomUsageStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(t_sm_RoomUsageStatistics.DataRowToModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取阅览室使用量记录
        /// </summary>
        /// <param name="roomsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<RoomFlowStatistics> GetRoomFlowStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (roomsNo != null)
                {
                    for (int i = 0; i < roomsNo.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere.Append(string.Format(" ReadingRoomNo in ('{0}'", roomsNo[i]));
                        }
                        else if (i != roomsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}'  ", roomsNo[i]));
                        }
                        if (i == roomsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}')", roomsNo[i]));
                        }
                    }
                }
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate>='{0}'" : " and StatisticsDate>='{0}'", startDate);
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate<='{0}'" : " and StatisticsDate<='{0}'", endDate);
                List<RoomFlowStatistics> modelList = new List<RoomFlowStatistics>();
                DataSet ds = t_sm_RoomFlowStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(t_sm_RoomFlowStatistics.DataRowToModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取使用情况
        /// </summary>
        /// <param name="terminalsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<TerminalUsageStatistics> GetTerminalUsageStatisticsist(List<string> terminalsNo, DateTime startDate, DateTime endDate)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (terminalsNo != null)
                {
                    for (int i = 0; i < terminalsNo.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere.Append(string.Format(" TerminalNo in ('{0}'", terminalsNo[i]));
                        }
                        else if (i != terminalsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}'  ", terminalsNo[i]));
                        }
                        if (i == terminalsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}')", terminalsNo[i]));
                        }
                    }
                }
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate>='{0}'" : " and StatisticsDate>='{0}'", startDate);
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate<='{0}'" : " and StatisticsDate<='{0}'", endDate);
                List<TerminalUsageStatistics> modelList = new List<TerminalUsageStatistics>();
                DataSet ds = t_sm_TerminalUsageStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(t_sm_TerminalUsageStatistics.DataRowToModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取设备使量
        /// </summary>
        /// <param name="terminalsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<TerminalFlowStatistics> GetTerminalFlowStatisticsList(List<string> terminalsNo, DateTime startDate, DateTime endDate)
        {
            try
            {
                StringBuilder strWhere = new StringBuilder();
                if (terminalsNo != null)
                {
                    for (int i = 0; i < terminalsNo.Count; i++)
                    {
                        if (i == 0)
                        {
                            strWhere.Append(string.Format(" TerminalNo in ('{0}'", terminalsNo[i]));
                        }
                        else if (i != terminalsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}'  ", terminalsNo[i]));
                        }
                        if (i == terminalsNo.Count - 1)
                        {
                            strWhere.Append(string.Format(",'{0}')", terminalsNo[i]));
                        }
                    }
                }
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate>='{0}'" : " and StatisticsDate>='{0}'", startDate);
                strWhere.AppendFormat(string.IsNullOrEmpty(strWhere.ToString()) ? " StatisticsDate<='{0}'" : " and StatisticsDate<='{0}'", endDate);
                List<TerminalFlowStatistics> modelList = new List<TerminalFlowStatistics>();
                DataSet ds = t_sm_TerminalFlowStatistics.GetList(strWhere.ToString());
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    modelList.Add(t_sm_TerminalFlowStatistics.DataRowToModel(ds.Tables[0].Rows[i]));
                }
                return modelList;
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastRoomUsageStatisticsDate()
        {
            try
            {
                DataSet ds = t_sm_RoomUsageStatistics.GetList(1, "", " id desc");
                return ds.Tables[0].Rows.Count > 0 ? t_sm_RoomUsageStatistics.DataRowToModel(ds.Tables[0].Rows[0]).StatisticsDate : GetFristLogDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastRoomFlowStatisticsDate()
        {
            try
            {
                DataSet ds = t_sm_RoomFlowStatistics.GetList(1, "", " id desc");
                return ds.Tables[0].Rows.Count > 0 ? t_sm_RoomFlowStatistics.DataRowToModel(ds.Tables[0].Rows[0]).StatisticsDate : GetFristLogDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastTerminalUsageStatisticsDate()
        {
            try
            {
                DataSet ds = t_sm_TerminalUsageStatistics.GetList(1, "", " id desc");
                return ds.Tables[0].Rows.Count > 0 ? t_sm_TerminalUsageStatistics.DataRowToModel(ds.Tables[0].Rows[0]).StatisticsDate : GetFristLogDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLastTerminalFlowStatisticsDate()
        {
            try
            {
                DataSet ds = t_sm_TerminalFlowStatistics.GetList(1, "", " id desc");
                return ds.Tables[0].Rows.Count > 0 ? t_sm_TerminalFlowStatistics.DataRowToModel(ds.Tables[0].Rows[0]).StatisticsDate : GetFristLogDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("WCF数据服务：执行遇到异常" + ex.Message);
                throw ex;
            }
        }
    }
}
