using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;
using SeatManage.SeatManageComm;

namespace SeatManage.Bll
{
    public class SeatUsageDataOperating
    {
        /// <summary>
        /// 添加阅览室使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>]
        public static bool AddRoomUsageStatistics(RoomUsageStatistics model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddRoomUsageStatistics(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加阅览室使用记录失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 添加阅览室使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddRoomFlowStatistics(RoomFlowStatistics model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddRoomFlowStatistics(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加阅览室使用量记录失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 添加设备使用情况记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddTerminalUsageStatistics(TerminalUsageStatistics model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddTerminalUsageStatistics(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加设备使用情况记录失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 添加设备使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool AddTerminalFlowStatistics(TerminalFlowStatistics model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddTerminalFlowStatistics(model);
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加设备使用量记录失败：" + ex.Message);
                return false;
            }
          
        }
        /// <summary>
        /// 获取阅览室使用情况记录
        /// </summary>
        /// <param name="roomsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static List<RoomUsageStatistics> GetRoomUsageStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetRoomUsageStatisticsList(roomsNo, startDate, endDate);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取阅览室使用情况记录失败：" + ex.Message);
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
        public static List<RoomFlowStatistics> GetRoomFlowStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetRoomFlowStatisticsList(roomsNo, startDate, endDate);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取阅览室使用量记录失败：" + ex.Message);
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
        public static List<TerminalUsageStatistics> GetTerminalUsageStatisticsist(List<string> terminalsNo, DateTime startDate, DateTime endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetTerminalUsageStatisticsist(terminalsNo, startDate, endDate);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取使用情况失败：" + ex.Message);
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
        public static List<TerminalFlowStatistics> GetTerminalFlowStatisticsList(List<string> terminalsNo, DateTime startDate, DateTime endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetTerminalFlowStatisticsList(terminalsNo, startDate, endDate);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取设备使量失败：" + ex.Message);
                throw ex;
            }
           
        }

        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastRoomUsageStatisticsDate()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetLastRoomUsageStatisticsDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取最后记录的时间失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastRoomFlowStatisticsDate()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetLastRoomFlowStatisticsDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取最后记录的时间失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastTerminalUsageStatisticsDate()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetLastTerminalUsageStatisticsDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取最后记录的时间失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastTerminalFlowStatisticsDate()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetLastTerminalFlowStatisticsDate();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取最后记录的时间失败：" + ex.Message);
                throw ex;
            }
           
        }
    }
}
