using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class T_SM_SeatWaiting
    {
        /// <summary>
        /// 获取等待记录列表
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="enterOutLogNo">进出记录编号</param>
        /// <param name="logType">记录类型</param>
        /// <param name="logStatus">记录状态</param>
        /// <returns></returns>
        public static List<WaitSeatLogInfo> GetWaitSeatList(string cardNo, string enterOutLogNo, string begindate, string enddate, List<EnterOutLogType> logType)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                List<WaitSeatLogInfo> list = seatService.GetWaitLogList(cardNo,enterOutLogNo, begindate, enddate,logType);
                return list;
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取等待记录失败：" + ex.Message);
                return new List<WaitSeatLogInfo>();
            }
          
        }
        /// <summary>
        /// 添加等待记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static int AddWaitSeatLog(WaitSeatLogInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddWaitLog(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加等待记录失败：" + ex.Message);
                return -1;
            }
            
        }
        /// <summary>
        /// 修改等待记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateWaitLog(WaitSeatLogInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateWaitLog(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("修改等待记录失败：" + ex.Message);
                return false;
            }
           
        }
        /// <summary>
        /// 根据学号获取读者最后一条等待记录。
        /// 如果房间号为空，则不根据房间号查询。
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="roomNum">要查询的房间号</param>
        /// <returns></returns>
        public static WaitSeatLogInfo GetListWaitLogByCardNo(string cardNo,string roomNum)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetListWaitLogByCardNo(cardNo, roomNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取等待记录失败：" + ex.Message);
                return null;
            }
           
        }
    }
}
