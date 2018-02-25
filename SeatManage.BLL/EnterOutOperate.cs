using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IWCFService;
using SeatManage.WcfAccessProxy;
using SeatManage.SeatManageComm;
using System.ServiceModel;
namespace SeatManage.Bll
{
    /// <summary>
    /// 进出相关操作
    /// </summary>
    public class EnterOutOperate
    {
        /// <summary>
        /// 获取读者相关信息
        /// </summary>
        /// <returns></returns>
        public static ReaderInfo GetReaderInfo(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReader(cardNo, true);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者信息失败：" + ex.Message);
                throw new Exception("获取读者信息失败：" + ex.Message);
                //return null;
            }
           
        }
        /// <summary>
        /// 获取简单的读者信息，不包含记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static ReaderInfo GetSimpleReaderInfo(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReader(cardNo, false);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者信息失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 获取读者选座次数
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int GetReaderChooseTimes(string cardNo, int minutes)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {

                return seatService.GetReaderChooseSeatTimes(cardNo, minutes);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者选座次数失败：" + ex.Message);
                return -1;
            }
            
        }
        /// <summary>
        /// 获取读者选座次数
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public static int GetReaderChooseTimes(string cardNo, int minutes, string roomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {

                return seatService.GetReaderChooseSeatTimesByReadingRoom(cardNo, minutes, roomNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者选座次数失败：" + ex.Message);
                return -1;
            }
            
        }
        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        public static Dictionary<string, ReadingRoomSeatUsedState> GetReadingRoomSeatUsingState(List<string> roomNums)
        {
            Dictionary<string, ReadingRoomSeatUsedState> seatUsingState = new Dictionary<string, ReadingRoomSeatUsedState>();
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                seatUsingState = seatService.GetRoomSeatUsedState(roomNums);
                return seatUsingState;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取阅览室使用状态失败：" + ex.Message);
                return new Dictionary<string, ReadingRoomSeatUsedState>();
            }
            
        }
        /// <summary>
        /// 验证选座次数是否超过次数限制
        /// </summary>
        /// <param name="ChooseSeatTimesRestrict">刷卡次数设置</param>
        /// <returns></returns>
        public static bool CheckReaderChooseSeatTimes(string cardNo, POSRestrict chooseSeatTimesRestrict)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                int times = seatService.GetReaderChooseSeatTimes(cardNo, chooseSeatTimesRestrict.Minutes);
                if (times >= chooseSeatTimesRestrict.Times && chooseSeatTimesRestrict.IsUsed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("判断是否选座超过次数失败:" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 验证选座次数是否超过次数限制
        /// </summary>
        /// <param name="ChooseSeatTimesRestrict">刷卡次数设置</param>
        /// <returns></returns>
        public static bool CheckReaderChooseSeatTimesByReadingRoom(string cardNo, POSRestrict chooseSeatTimesRestrict, string roomNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                int times = seatService.GetReaderChooseSeatTimesByReadingRoom(cardNo, chooseSeatTimesRestrict.Minutes, roomNo);
                if (times >= chooseSeatTimesRestrict.Times && chooseSeatTimesRestrict.IsUsed)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("判断是否选座超过次数失败:" + ex.Message);
                return false;
            }
            
        }
        /// <summary>
        /// 获取阅览室中的座位分布以及使用情况
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public static SeatLayout GetRoomSeatLayOut(string roomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                SeatLayout layout = seatService.GetRoomSeatLayOut(roomNum);
                return layout;
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("获取阅览室座位分布遇到错误：" + ex.Message);
                return null;
            }
           
        }

        public static string DelaySeatUsedTime(ReaderInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            string result = "";
            try
            {
                result = seatService.DelaySeatUsedTime(model);
                return result;
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("延长座位使用时间遇到错误：" + ex.Message);
                return result;
            }
           
        }

        /// <summary>
        /// 添加新的进出记录
        /// </summary>
        /// <param name="model"></param>
        public static HandleResult AddEnterOutLog(EnterOutLogInfo model, ref int newLogId)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                HandleResult result = seatService.AddEnterOutLogInfo(model, ref newLogId);
                return result;
            }
            catch (Exception ex)
            {
                error = true;
                WriteLog.Write("添加进出记录遇到错误：" + ex.Message);
                return HandleResult.Failed;
            }
            
        }

        /// <summary>
        /// 座位计时
        /// </summary>
        /// <param name="enterOutLigNo"></param>
        /// <param name="markTime"></param>
        /// <returns></returns>
        public static bool UpdateMarkTime(string enterOutLigNo, DateTime markTime)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateMarkTime(enterOutLigNo, markTime);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("座位计时操作失败：" + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 获取读者相关信息
        /// </summary>
        /// <returns></returns>
        public static ReaderInfo GetReaderSeatState(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetReaderSeatState(cardNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取读者信息失败：" + ex.Message);
                throw new Exception("获取读者信息失败：" + ex.Message);
                //return null;
            }
           
        }
        /// <summary>
        /// 获取阅览室座位使用状态
        /// </summary>
        /// <param name="roomNums"></param>
        /// <returns></returns>
        public static Dictionary<string, ReadingRoomSeatUsedState> GetReadingRoomSeatUsingStateV2(List<string> roomNums)
        {
            Dictionary<string, ReadingRoomSeatUsedState> seatUsingState = new Dictionary<string, ReadingRoomSeatUsedState>();
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                seatUsingState = seatService.GetRoomSeatUsedStateV4(roomNums);
                return seatUsingState;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取阅览室使用状态失败：" + ex.Message);
                return new Dictionary<string, ReadingRoomSeatUsedState>();
            }
            
        }
    }
}
