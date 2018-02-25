/***************************************
 * 作者：王昊天
 * 时间：2013-5-21
 * 说明：座位预约的bll
 * 修改人：
 * 修改时间：
 * *************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_SeatBespeak
    {
        /// <summary>
        /// 获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// 
        /// <param name="status">预约状态</param>
        /// <returns></returns>
        public static List<BespeakLogInfo> GetBespeakList(string cardNo, string roomNum, DateTime endDate, int spanDays, List<BookingStatus> status)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBespeakLogs(cardNo, roomNum, endDate, spanDays, status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取预约记录：" + ex.Message);
                return new List<BespeakLogInfo>();
            }
          
        }
        /// <summary>
        /// 通过学号模糊查询获取全部的记录，为空就是不设条件查询
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">阅览室编号</param>
        /// <param name="date">查询当前日期的记录</param>
        /// 
        /// <param name="status">预约状态</param>
        /// <returns></returns>
        public static List<BespeakLogInfo> GetBespeakList_ByFuzzySearch(string cardNo, string roomNum, DateTime endDate, int spanDays, List<BookingStatus> status)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBespeakLogs_ByFuzzySearch(cardNo, roomNum, endDate, spanDays, status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取预约记录：" + ex.Message);
                return new List<BespeakLogInfo>();
            }
           
        }
        /// <summary>
        /// 更改预约记录
        /// </summary>
        /// <param name="model">预约记录</param>
        /// <returns></returns>
        public static int UpdateBespeakList(BespeakLogInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateBespeakLogInfo(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新预约记录失败：" + ex.Message);
                return -1;
            }
           
        }

        /// <summary>
        /// 获取阅览室中座位的预约状态
        /// </summary>
        /// <param name="roomNums"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static Dictionary<string, ReadingRoomSeatBespeakState> GetRoomBespeakSeatState(List<string> roomNums, DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetRoomBespeakSeatState(roomNums, date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取座位预约状态失败：" + ex.Message);
                return new Dictionary<string, ReadingRoomSeatBespeakState>();
            }
           
        }
        /// <summary>
        /// 根据阅览室编号获取指定预约座位
        /// </summary>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public static SeatLayout GetBeseakSeatSettingLayout(string roomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBeseakSeatSettingLayout(roomNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取指定预约座位布局图失败：" + ex.Message);
                return null;
            }
           
        }

        public static SeatLayout GetBeseakSeatLayout(string roomNum, DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBeseakSeatLayout(roomNum, date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取预约座位布局图失败：" + ex.Message);
                return null;
            }
           
        }
        public static List<BespeakLogInfo> GetNotCheckedBespeakLogInfo(List<string> roomNum, DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetNotCheckedBespeakLogInfo(roomNum, date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("通过日期获取未签到的预约记录失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 通过学号获取读者的预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<BespeakLogInfo> GetBespeakLogInfoByCardNo(string cardNo, DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBespeakLogInfo(cardNo, date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("通过学号获取读者的预约记录失败：" + ex.Message);
                throw ex;
            }
            
        }

        /// <summary>
        /// 根据座位号获取预约记录
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static List<BespeakLogInfo> GetBespeakLogInfoBySeatNo(string seatNo, DateTime date)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBespeakLogInfoBySeatNo(seatNo, date);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据座位号获取预约记录失败：" + ex.Message);
                return new List<BespeakLogInfo>();
            }
           
        }

        public static HandleResult AddBespeakLogInfo(BespeakLogInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddBespeakLogInfo(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加预约记录失败：" + ex.Message);
                throw ex;
            }
           
        }

        public static BespeakLogInfo GetBespeaklogById(int id)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBespeaklogById(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加预约记录失败：" + ex.Message);
                throw ex;
            }
          

        }
    }
}
