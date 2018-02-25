using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
   public class IAdCustomerService
    {
        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <returns></returns>
       public static List<AMS.Model.AMS_AdCustomer> GetCustomerList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetAdCustomerList();
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
        /// 添加新的客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static string AddNewCustomer(AMS.Model.AMS_AdCustomer model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddNewCustomer(model);
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
        /// 更新客户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static string UpdateCustomer(AMS.Model.AMS_AdCustomer model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateCustomer(model);
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
       /// 删除客户
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static string DeleteCustomer(AMS.Model.AMS_AdCustomer model)
       {
           AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
           try
           {
               return bllService.DeleteCustomer(model);
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
