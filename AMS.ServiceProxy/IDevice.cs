using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class IDevice
    {
        /// <summary>
        /// 根据学校编号获取设备列表
        /// </summary>
        /// <param name="schoolNum"></param>
        /// <param name="flag">表示是否获取更新的</param>
        /// <returns></returns>
        public static List<Model.AMS_Device> GeDeviceModelBySchoolNum(string schoolNum, bool flag)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.GeDeviceModelBySchoolNum(schoolNum, flag);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static Model.Enum.HandleResult UpdateDeviceModel(Model.AMS_Device model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.UpdateDeviceModel(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static Model.Enum.HandleResult UpdateDeviceStatus(string deviceNo, DateTime stateUpdateTime)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.UpdateDeviceStatus(deviceNo, stateUpdateTime);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("更新设备状态遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static Model.Enum.HandleResult AddDeviceModel(Model.AMS_Device model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.AddDeviceModel(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static List<Model.AMS_Device> GeDeviceModelByCampusNum(string campusNum)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.GeDeviceModelByCampusNum(campusNum);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据校区编号查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static Model.AMS_Device GetDevicebyNo(string No)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.GetDevicebyNo(No);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据编号查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
        public static Model.AMS_Device GetDevicebyID(int id)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.GetDevicebyID(id);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据id查看设备信息遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
                throw ex;
            }
            finally
            {
                ICommunicationObject ICommObjectService = bllService as ICommunicationObject;
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
