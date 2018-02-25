using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    /// <summary>
    /// 进出记录统计操作
    /// </summary>
    public class T_SM_EnterOutLogStatistics
    {
        /// <summary>
        /// 添加记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddEnterOutLogStatistics(EnterOutLogStatistics model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddEnterOutStatistics(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加进出记录统计失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 获取列表Null为获取全部
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static List<EnterOutLogStatistics> GetEnterOutLogStatistics(List<string> roomNo, string seatNo, string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogStatisticsList(roomNo, cardNo, seatNo);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录统计列表失败：" + ex.Message);
                return new List<EnterOutLogStatistics>();
            }
            
        }
        /// <summary>
        /// 获取列表Null为获取全部
        /// </summary>
        /// <param name="roomNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static List<EnterOutLogStatistics> GetEnterOutLogStatisticsByDate(string roomNo, string starttime, string endtime)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogStatisticsListByDate(roomNo, starttime, endtime);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录统计列表失败：" + ex.Message);
                return new List<EnterOutLogStatistics>();
            }
            
        }
        /// <summary>
        /// 获取最后条进出记录统计
        /// </summary>
        /// <returns></returns>
        public static EnterOutLogStatistics GetLastEnterOutLogStatistics()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetLastLog();
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取最后的进出记录失败：" + ex.Message);
                return null;
            }
            
        }
    }
}
