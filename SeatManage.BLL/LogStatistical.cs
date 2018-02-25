using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SeatManage.Bll
{
    public class LogStatistical
    {
        /// <summary>
        /// 获取在座时长排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static DataTable TopSeatTimeList(int top, string startDate, string endDate, int type)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.TopSeatTimeList(top, startDate, endDate, type);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户权限失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 获取选座次数排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static DataTable TopSeatCountList(int top, string startDate, string endDate, int type)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.TopSeatCountList(top, startDate, endDate, type);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户权限失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 获取黑名单排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static DataTable TopBlacklistList(int top, string startDate, string endDate, int type)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.TopBlacklistList(top, startDate, endDate, type);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户权限失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 获取违规记录排行榜
        /// </summary>
        /// <param name="top"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        public static DataTable TopViolateDisciplineList(int top, string startDate, string endDate, int type)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.TopViolateDisciplineList(top, startDate, endDate, type);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取用户权限失败：" + ex.Message);
                return null;
            }
            
        }
    }
}
