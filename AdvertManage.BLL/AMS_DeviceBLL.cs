using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class AMS_DeviceBLL
    {
        /// <summary>
        /// 根据学校编号获取设备列表
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag">表示是否获取更新的</param>
        /// <returns></returns>
        public static List<AdvertManage.Model.AMS_DeviceModel> GeDeviceModelBySchoolNum(string schoolNum, bool flag)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GeDeviceModelBySchoolNum(schoolNum, flag);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 更新设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AdvertManage.Model.Enum.HandleResult UpdateDeviceModel(AdvertManage.Model.AMS_DeviceModel model)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.UpdateDeviceModel(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 更新设备状态
        /// </summary>
        /// <param name="deviceNo"></param>
        /// <param name="stateUpdateTime"></param>
        /// <returns></returns>
        public static AdvertManage.Model.Enum.HandleResult UpdateDeviceStatus(string deviceNo, DateTime stateUpdateTime)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.UpdateDeviceStatus(deviceNo, stateUpdateTime);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新设备状态遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 添加设备信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public static AdvertManage.Model.Enum.HandleResult AddDeviceModel(AdvertManage.Model.AMS_DeviceModel model)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.AddDeviceModel(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据校区编号查看设备信息
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag"></param>
        /// <returns></returns> 
        public static List<AdvertManage.Model.AMS_DeviceModel> GeDeviceModelByCampusNum(string campusNum)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GeDeviceModelByCampusNum(campusNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据校区编号查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据设备编号查找设备
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public static AdvertManage.Model.AMS_DeviceModel GetDevicebyNo(string No)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetDevicebyNo(No);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据编号查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
        /// 根据设备id查找设备
        /// </summary>
        /// <param name="No"></param>
        /// <returns></returns>
        public static AdvertManage.Model.AMS_DeviceModel GetDevicebyID(int id)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.GetDevicebyid(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据id查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
