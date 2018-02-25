using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class AMS_RollTitles
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddRollTitles(SeatManage.ClassModel.RollTitlesInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddRollTitles(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取滚动广告失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 查询string
        /// </summary>
        /// <returns></returns>
        public static string GetModelStr()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetModelStr();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取滚动广告失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 根据NUM查
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.RollTitlesInfo GetModelByNum(string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetModelByNum(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取获取滚动广告失败：" + ex.Message);
                throw ex;
            }
            
        }
        /// <summary>
        /// 根据NUM查
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.RollTitlesInfo> GetOverTimeRollTitle()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetOverTimeRollTitleList();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取获取过期滚动广告失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult UpdateRollTitles(SeatManage.ClassModel.RollTitlesInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateRollTitles(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("修改滚动广告失败：" + ex.Message);
                throw ex;
            }
           
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult DeleteRollTitles(string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteRollTitle(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除滚动广告失败：" + ex.Message);
                throw ex;
            }
           
        }
    }
}
