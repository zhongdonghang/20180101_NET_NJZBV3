using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_EnterOutLog
    {
        /// <summary>
        /// 获取Id大于参数值的进出记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<EnterOutLogInfo> GetEnterOutLogInfoGreaterThanId(int id,bool isorder)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogInfoGreaterThanId(id, isorder);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
            
        }
        /// <summary>
        /// 获取座位使用状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public static EnterOutLogType GetSeatUsedState(string seatNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                EnterOutLogInfo model = seatService.GetEnterOutLogInfoWithBookWaitBySeatNum(seatNo);
                if (model != null)
                {
                    SeatManage.EnumType.EnterOutLogType type = model.EnterOutState;
                    return type;
                }
                else
                {
                    List<BespeakLogInfo> bespeakLogs = seatService.GetBespeakLogInfoBySeatNo(seatNo, DateTime.Now);
                    if (bespeakLogs.Count > 0 && bespeakLogs[0].BsepeakState == SeatManage.EnumType.BookingStatus.Waiting)
                    {
                        return EnterOutLogType.BespeakWaiting;
                    }
                    return EnterOutLogType.None;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取座位状态失败：" + ex.Message);
                return EnterOutLogType.None;
            }
          
        }


        /// <summary>
        /// 根据条件获取进出记录
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="seatNo">座位号</param>
        /// <param name="logType">记录类型</param>
        /// <param name="logStatus">记录状态</param>
        /// <param name="beginDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public static List<EnterOutLogInfo> GetEnterOutLogByStatus(string cardNo, string roomNum, string seatNo, List<EnterOutLogType> logType, LogStatus logStatus, string beginDate, string endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogsByStatus(cardNo, roomNum, seatNo, logType, logStatus, beginDate, endDate);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
          
        }
        /// <summary>
        /// 根据进出记录编号查出对应的列表
        /// </summary>
        /// <param name="enterOutNo">编号</param>
        /// <param name="logType">记录类型</param>
        /// <param name="top">最后几条</param>
        /// <returns></returns>
        public static List<EnterOutLogInfo> GetEnterOutLogByNo(string enterOutNo, List<EnterOutLogType> logType, int top)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogsByNo(enterOutNo, logType, top);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
           
        }
        /// <summary>
        /// 通过座位编号获取有效的进出记录
        /// </summary>
        /// <param name="seatNo">座位号</param> 
        /// <returns></returns>
        public static EnterOutLogInfo GetUsingEnterOutLogBySeatNo(string seatNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogInfoBySeatNum(seatNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return null;
            }
           
        }
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
                return seatService.GetEnterOutLogInfoById(enterOutLogID);
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
                return seatService.GetEnterOutLogs(cardNo, roomNum, seatNo, beginTime.ToString(), endTime.ToString());
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
           
        }
        /// <summary>
        /// 根据学号模糊查询，获取进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNo"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static List<EnterOutLogInfo> GetEnterOutLogs_ByFuzzySearch(string cardNo, string roomNum, string seatNo, DateTime beginTime, DateTime endTime)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogs_ByFuzzySearch(cardNo, roomNum, seatNo, beginTime.ToString(), endTime.ToString());
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return new List<EnterOutLogInfo>();
            }
           
        }
        /// <summary>
        /// 根据学号获取读者进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static EnterOutLogInfo GetEnterOutLogInfoByCardNo(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetEnterOutLogInfoByCardNo(cardNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return null;
            }
            
        }

        public static List<EnterOutLogInfo> GetUsingSeatEnterOutLogInfo(string roomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetUsingSeatEnterOutLogInfo(roomNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取进出记录失败：" + ex.Message);
                return null;
            }
            
        }
    }
}
