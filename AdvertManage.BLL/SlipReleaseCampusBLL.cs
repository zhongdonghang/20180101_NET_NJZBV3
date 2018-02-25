using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class SlipReleaseCampusBLL
    {
        /// <summary>
        /// 下发优惠券
        /// </summary>
        /// <param name="SlipId"></param>
        /// <param name="campusid"></param>
        /// <returns></returns>
        public static int AddSlipRelease(int SlipId,int campusid)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.AddSlipRelease(SlipId, campusid);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("下发优惠券遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据Id获取优惠券下发的学校信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static Model.AMS_SlipCustomerModel GetSlipReleaseListById(int Id)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipReleaseListById(Id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据校区id获取优惠券下发的学校信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据校区id获取优惠券下发学校
        /// </summary>
        /// <returns></returns> 
        public static List<Model.AMS_SlipCustomerModel> GetSlipReleaseListByCampusId(int campusid)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipReleaseListByCampusId(campusid);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据校区id获取优惠券下发信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据校区编号获取优惠券下发学校
        /// </summary>
        /// <returns></returns> 
        public static List<Model.AMS_SlipCustomerModel> GetSlipReleaseListByCampusNum(string campusNum)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipReleaseListByCampusNum(campusNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据校区编号获取优惠券下发信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据学校id获取优惠券下发学校
        /// </summary>
        /// <returns></returns> 
        public static List<Model.AMS_SlipCustomerModel> GetSlipReleaseListBySchoolId(int schoolid)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipReleaseListBySchoolId(schoolid);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校id获取优惠券下发信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据学校编号获取优惠券下发学校
        /// </summary>
        /// <returns></returns> 
        public static List<Model.AMS_SlipCustomerModel> GetSlipReleaseListBySchoolNum(string SchoolNum)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetSlipReleaseListBySchoolNum(SchoolNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取优惠券下发信息遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
