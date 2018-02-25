/*************************************************
 * 作者：王昊天
 * 创建时间：2013-5-20
 * 说明：关于黑名单记录的操作
 * 修改人：
 * 修改时间：
 * ***********************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_Blacklist
    {
        /// <summary>
        /// 添加黑名单记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddBlackList(BlackListInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddBlacklist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加黑名单失败：" + ex.Message);
                return -1;
            }
            
        }
        /// <summary>
        /// 更新黑名单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int UpdateBlackList(BlackListInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateBlacklist(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新黑名单错误：" + ex.Message);
                return -1;
            }
            
        }
        /// <summary>
        /// 获取有效的黑名单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <returns></returns>
        public static List<BlackListInfo> GetBlackListInfo(string cardNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBlacklistInfo(cardNo);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取黑名单失败：" + ex.Message);
                return new List<BlackListInfo>();
            }
            
        }
        /// <summary>
        /// 获取全部的黑名单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static List<BlackListInfo> GetAllBlackListInfo(string cardNo, LogStatus status, string beginDate, string endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBlacklistInfos(cardNo, status, beginDate, endDate);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取黑名单失败：" + ex.Message);
                return new List<BlackListInfo>();
            }
           
        }
        /// <summary>
        /// 通过学号模糊查询获取全部的黑名单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public static List<BlackListInfo> GetAllBlackListInfo_ByFuzzySearch(string cardNo, LogStatus status, string beginDate, string endDate)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBlacklistInfos_ByFuzzySearch(cardNo, status, beginDate, endDate);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取黑名单失败：" + ex.Message);
                return new List<BlackListInfo>();
            }
            
        }
        /// <summary>
        /// 根据黑名单ID获取黑名单记录
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static BlackListInfo GetBlistList(string ID)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetBlistList(ID);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取黑名单失败：" + ex.Message);
                return null;
            }
           
        }
    }
}
