using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.ClassModel;
using System.ServiceModel;

namespace SeatManage.Bll
{
    public class AdvertisementOperation
    {
        /// <summary>
        /// 获取单个广告
        /// </summary>
        /// <param name="adNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.AMS_Advertisement GetAdModel(string adNum, SeatManage.EnumType.AdType adType)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetAdModel(adNum, adType);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取广告失败：" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 获取有效的广告列表
        /// </summary>
        /// <param name="isOverTime"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_Advertisement> GetAdList(bool? isOverTime, SeatManage.EnumType.AdType adType)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetAdList(isOverTime, adType);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取广告列表失败：" + ex.Message);
                return new List<AMS_Advertisement>();
            }

        }
        /// <summary>
        /// 添加广告
        /// </summary>
        /// <param name="adInfo"></param>
        /// <returns></returns>
        public static string AddAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.AddAdModel(adInfo);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("添加广告失败：" + ex.Message);
                return ex.Message;
            }

        }
        /// <summary>
        /// 更新广告
        /// </summary>
        /// <param name="adInfo"></param>
        /// <returns></returns>
        public static string UpdateAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.UpdateAdModel(adInfo);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("更新广告失败：" + ex.Message);
                return ex.Message;
            }

        }
        /// <summary>
        /// 删除广告
        /// </summary>
        /// <param name="adInfo"></param>
        /// <returns></returns>
        public static string DeleteAdModel(SeatManage.ClassModel.AMS_Advertisement adInfo)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.DeleteAdModel(adInfo);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("删除广告失败：" + ex.Message);
                return ex.Message;
            }

        }

        /// <summary>
        /// 更新使用情况
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateAdvertUsage(SeatManage.ClassModel.AMS_AdvertUsage model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.UpdateAdvertUsage(model);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("更新广告状态失败：" + ex.Message);
                return ex.Message;
            }

        }
        /// <summary>
        /// 获取单个记录
        /// </summary>
        /// <param name="AdNum"></param>
        /// <param name="adType"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.AMS_AdvertUsage GetAdvertUsage(string AdNum, SeatManage.EnumType.AdType adType)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetAdvertUsage(AdNum, adType);
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取广告状态失败：" + ex.Message);
                return null;
            }

        }
        /// <summary>
        /// 获取全部状态
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_AdvertUsage> GetAdvertUsageList()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            try
            {
                return seatService.GetAdvertUsageList();
            }
            catch (FaultException ex)
            {
                SeatManageComm.WriteLog.Write("获取广告状态失败：" + ex.Message);
                return new List<AMS_AdvertUsage>();
            }
        }
    }
}
