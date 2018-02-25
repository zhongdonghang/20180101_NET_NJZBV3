using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.IWCFService
{
    public partial interface ISeatManageService : IExceptionService
    {
        /// <summary>
        /// 添加阅览室使用记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddRoomUsageStatistics(RoomUsageStatistics model);
        /// <summary>
        /// 添加阅览室使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddRoomFlowStatistics(RoomFlowStatistics model);
        /// <summary>
        /// 添加设备使用情况记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddTerminalUsageStatistics(TerminalUsageStatistics model);
        /// <summary>
        /// 添加设备使用量记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddTerminalFlowStatistics(TerminalFlowStatistics model);
        /// <summary>
        /// 获取阅览室使用情况记录
        /// </summary>
        /// <param name="roomsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<RoomUsageStatistics> GetRoomUsageStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate);
        /// <summary>
        /// 获取阅览室使用量记录
        /// </summary>
        /// <param name="roomsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<RoomFlowStatistics> GetRoomFlowStatisticsList(List<string> roomsNo, DateTime startDate, DateTime endDate);
        /// <summary>
        /// 获取使用情况
        /// </summary>
        /// <param name="terminalsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<TerminalUsageStatistics> GetTerminalUsageStatisticsist(List<string> terminalsNo, DateTime startDate, DateTime endDate);
        /// <summary>
        /// 获取设备使量
        /// </summary>
        /// <param name="terminalsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        [OperationContract]
        List<TerminalFlowStatistics> GetTerminalFlowStatisticsList(List<string> terminalsNo, DateTime startDate, DateTime endDate);
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetLastRoomUsageStatisticsDate();
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetLastRoomFlowStatisticsDate();
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetLastTerminalUsageStatisticsDate();
        /// <summary>
        /// 获取最后记录的时间
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        DateTime GetLastTerminalFlowStatisticsDate();


    }
}
