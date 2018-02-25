using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_EnterOutLog_bak
    {
        /// <summary>
        /// 根据进出记录ID获取进出记录
        /// </summary>
        /// <param name="enterOutLogID"></param>
        /// <returns></returns>
        public static EnterOutLogInfo GetEnterOutLogInfoById(int enterOutLogID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogBakInfoById(enterOutLogID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<EnterOutLogInfo> GetEnterOutLogs(string cardNo, string roomNum, string seatNo, DateTime beginTime, DateTime endTime)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutBakLogs(cardNo, roomNum, seatNo, beginTime.ToString(), endTime.ToString());
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
            
            
        }
        public static List<EnterOutLogInfo> GetStatisticsLogs(int ID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutBakLogsByLastID(ID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
           
        }
        public static List<EnterOutLogInfo> GetStatisticsLogsByDate(DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutBakLogsByDate(date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
            
        }

        public static DateTime GetFristLogDate()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetFristLogDate();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                throw;
            }
        }
    }
}
