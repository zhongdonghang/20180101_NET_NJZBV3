using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    /// <summary>
    /// 硬广操作
    /// </summary>
    public class AMS_HardAd
    {
        /// <summary>
        /// 添加硬广操作
        /// </summary>
        /// <param name="hardAdvert"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddHardAd(HardAdvertInfo hardAdvert)
        { 
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddHardAd(hardAdvert);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加硬广失败：" + ex.Message);
                return  EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        ///  获取有效的硬广信息
        /// </summary>
        /// <returns></returns>
        public static HardAdvertInfo GetHardAd()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetHardAdvert();
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取有效的硬广信息失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 根据编号获取硬广
        /// </summary>
        /// <param name="num">编号</param>
        /// <returns></returns>
        public static HardAdvertInfo GetHardAdvertByNum(string num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetHardAdvertByNum(num);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据编号获取硬广失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        /// 更新硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult UpdateHardAdvert(HardAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateHardAdvert(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新硬广失败：" + ex.Message);
                return  EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult DeleteHardAdvert(HardAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteHardAdvert(model);
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除硬广失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 获取过期硬广
        /// </summary>
        /// <param name="num">编号</param>
        /// <returns></returns>
        public static List<HardAdvertInfo> GetHardAdvertOvertime()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetHardAdvertOverTime();
            }
            catch (FaultException ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据编号获取硬广失败：" + ex.Message);
                return new List<HardAdvertInfo>();
            }
           
        }
             
    }
}
