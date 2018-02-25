using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class AMS_SlipCustomerBLL
    {
        /// <summary>
        /// 根据ID获取优惠券信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerById(int id)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipCustomerById(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据Id获取优惠券信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

        /// <summary>
        /// 根据编号获取优惠券客户信息
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns> 
        public static AdvertManage.Model.AMS_SlipCustomerModel GetSlipCustomerByNum(string number)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipCustomerByNum(number);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据编号获取优惠券客户信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取所有优惠券客户信息
        /// </summary>
        /// <returns></returns> 
        public static List<Model.AMS_SlipCustomerModel> GetSlipCustomerList()
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipCustomerList( );
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("获取所有优惠券客户信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
            }
        /// <summary>
        /// 更新优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public static Model.Enum.HandleResult UpdateSlipCustomer(Model.AMS_SlipCustomerModel model)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.UpdateSlipCustomer(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新优惠券客户信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 添加优惠券客户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public static Model.Enum.HandleResult AddSlipCustomer(Model.AMS_SlipCustomerModel model)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.AddSlipCustomer(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加优惠券客户信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }

        /// <summary>
        /// 删除优惠券客户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public static Model.Enum.HandleResult DeleteSlipCustomer(int id)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.DeleteSlipCustomer(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("删除优惠券客户遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
                try
                {
                    if (ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        ICommObjectService.Close();
                    }
                }
                catch
                {
                    ICommObjectService.Abort();
                }
            }
        }
    }
}
