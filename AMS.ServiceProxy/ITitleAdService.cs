using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
   public class ITitleAdService
    {
        /// <summary>
        /// 获取冠名列表
        /// </summary>
        /// <returns></returns>
       public static List<AMS.Model.AMS_TitleAd> GetTitleAdList()
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.GetTitleAdList();
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
        /// 添加新的冠名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static string AddNewtTitleAd(AMS.Model.AMS_TitleAd model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.AddNewTitleAd(model);
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
        /// 更新冠名
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static string UpdateTitleAd(AMS.Model.AMS_TitleAd model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.UpdateTitleAd(model);
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
       /// 删除冠名
       /// </summary>
       /// <param name="model"></param>
       /// <returns></returns>
       public static string DeleteTitleAd(AMS.Model.AMS_TitleAd model)
       {
           AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
           try
           {
               return bllService.DeleteTitleAd(model);
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
       /// 获取单个冠名
       /// </summary>
       /// <param name="Num"></param>
       /// <returns></returns>
       public static Model.AMS_TitleAd GetTitleAdByNum(int Num)
       {
           AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
           try
           {
               return bllService.GetTitleAdByNum(Num);
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
