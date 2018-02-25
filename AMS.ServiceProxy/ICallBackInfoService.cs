using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class ICallBackInfoService
    {
        /// <summary>
        /// 获取反馈信息列表
        /// </summary>
        /// <returns></returns>
        public static List<AMS.Model.AMS_CallBackErrorInfo> GetCallBackInfoList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetCallBackInfo();
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
        /// 添加新的反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string AddNewCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddNewCallBackInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
        /// 更新反馈信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string UpdateCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateCallBackInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
        public static string DeleteCallBackInfo(AMS.Model.AMS_CallBackErrorInfo model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.DeleteCallBackInfo(model);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
        public static List<AMS.Model.AMS_CallBackErrorInfo> GetCallBackInfoBySolveStatic(string SolveStatic)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetCallBackInfoBySolveStatic(SolveStatic);
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
        /// 获取学校列表
        /// </summary>
        /// <param name="SolveStatic"></param>
        /// <returns></returns>
        public static List<AMS.Model.AMS_School> GetSchoolList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetSchoolInfoList();
            }
            catch (EndpointNotFoundException ex)
            {
                throw new AMS.Model.CustomerException("连接服务器失败");
            }
            catch (CommunicationException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
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
