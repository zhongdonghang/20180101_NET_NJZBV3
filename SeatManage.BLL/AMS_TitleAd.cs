using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using SeatManage.ClassModel;

namespace SeatManage.Bll
{
    public class AMS_TitleAd
    {
        /// <summary>
        ///获取当前生效的冠名广告
        /// </summary>
        /// <returns></returns>
        public static SeatManage.ClassModel.TitleAdvertInfo GetTitleAdvertInfo()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetTitleAdvertInfo();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取当前生效的冠名广告失败：" + ex.Message);
                return null;
            }
            
        }
        /// <summary>
        ///获取过期的冠名广告
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.TitleAdvertInfo> GetTitleAdvertOverTime()
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetTitleAdOverTime();
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取过期的冠名广告失败：" + ex.Message);
                return new List<ClassModel.TitleAdvertInfo>();
            }
            
        }
        /// <summary>
        /// 添加冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult AddTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 删除冠名广告
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static EnumType.HandleResult DeleteTitleAdvert(SeatManage.ClassModel.TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }

        public static TitleAdvertInfo GetTitleModel(string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetTitleModel(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除冠名广告失败：" + ex.Message);
                return null;
            }
           
        }

        public static SeatManage.EnumType.HandleResult UpdateTitleAdvert(TitleAdvertInfo model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateTitleAdvert(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加冠名广告失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
    }
}
