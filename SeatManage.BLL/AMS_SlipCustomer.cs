using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace SeatManage.Bll
{
    /// <summary>
    /// 优惠券客户信息
    /// </summary>
    public class AMS_SlipCustomer
    {
        /// <summary>
        /// 获取有效的优惠券客户
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerList(string CampusNum)
        {

            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetSlipCustomerList(CampusNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取有效的优惠券客户信息失败：" + ex.Message);
                throw ex;
            }
            
        }
        /// <summary>
        /// 获取过期的优惠券客户
        /// </summary>
        /// <returns></returns>
        public static List<SeatManage.ClassModel.AMS_SlipCustomer> GetSlipCustomerListOverTime(SeatManage.EnumType.LogStatus status)
        {

            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetSlipCustomerListOverTime(status);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("获取过期的优惠券客户信息失败：" + ex.Message);
                return new List<ClassModel.AMS_SlipCustomer>();
            }
           
        }
        /// <summary>
        /// 添加新的优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddSlipCustomer(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("添加优惠券客户信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 删除优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult DeleteSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer model)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.DeleteSlipCustomer(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("删除优惠券客户信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 根据编号获取优惠券信息
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static SeatManage.ClassModel.AMS_SlipCustomer GetSlipCustomerByNum(string num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.GetSlipCustomerByNum(num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("根据编号获取优惠券客户信息失败：" + ex.Message);
                return null;
            }
           
        }
        /// <summary>
        /// 更新凭条客户信息
        /// </summary>
        /// <param name="slipCustomer"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult UpdateSlipCustomer(SeatManage.ClassModel.AMS_SlipCustomer slipCustomer)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.UpdateSlipCustomer(slipCustomer);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新凭条客户信息失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
            
        }
        /// <summary>
        /// 增加打印次数
        /// </summary>
        /// <param name="slipCustomer"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddSlipCustomerPrintCount(string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddSlipCustomerPrintCount(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新凭条客户打印次数失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
        /// <summary>
        /// 增加查看次数
        /// </summary>
        /// <param name="slipCustomer"></param>
        /// <returns></returns>
        public static SeatManage.EnumType.HandleResult AddSlipCustomerLookCount(string Num)
        {
            IWCFService.ISeatManageService seatService = new WcfServiceForSeatManage.SeatManageDateService();
            bool error = false;
            try
            {
                return seatService.AddSlipCustomerLookCount(Num);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManageComm.WriteLog.Write("更新凭条客户查看次数失败：" + ex.Message);
                return EnumType.HandleResult.Failed;
            }
           
        }
    }
}
