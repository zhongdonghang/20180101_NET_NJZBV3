using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class TerminalOperatorService
    {
        /// <summary>
        /// 获取终端配置
        /// </summary>
        /// <returns></returns>
        public static SeatManage.ClassModel.TerminalInfoV2 GetTeminalSetting(string teminalNo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetTeminalInfo(teminalNo);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取配置失败：" + ex.Message);
                throw new Exception("获取终端设置失败：" + ex.Message);
            }
            
        }
        /// <summary>
        /// 获取所有终端
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.TerminalInfoV2> GetAllTeminalInfo()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetAllTeminalInfo();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取配置失败：" + ex.Message);
                throw new Exception("获取终端设置失败：" + ex.Message);
            }
         
        }
        /// <summary>
        /// 更新终端配置
        /// </summary>
        /// <returns></returns>
        public static string UpdateTeminalSetting(ClassModel.TerminalInfoV2 teminal)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.UpdateTeminalInfo(teminal);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("更新配置失败：" + ex.Message);
                throw new Exception("更新终端设置失败：" + ex.Message);
            }
          
        }
        /// <summary>
        /// 廷加终端配置
        /// </summary>
        /// <returns></returns>
        public static string AddTeminalInfo(ClassModel.TerminalInfoV2 teminal)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.AddTeminalInfo(teminal);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("添加配置失败：" + ex.Message);
                throw new Exception("添加终端设置失败：" + ex.Message);
            }
           
        }
        /// <summary>
        /// 阅览室座位剩余
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static int LastSeatCount(List<string> list)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.LastSeatCount(list);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取剩余座位失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 阅览室座位剩余
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Dictionary<string, SeatManage.ClassModel.ReadingRoomSeatUsedState_Ex> GetTeminaRoomStatus(List<string> list)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetTeminaRoomStatus(list);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室使用情况失败：" + ex.Message);
                throw ex;
            }
           
        }
    }
}
