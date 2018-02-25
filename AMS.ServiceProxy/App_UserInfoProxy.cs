using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using AMS.Model;

namespace AMS.ServiceProxy
{
   public class App_UserInfoProxy 
    {
        /// <summary>
        /// 根据学号和学校编号获取用户信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="schoolNum"></param>
        /// <returns></returns>
       public static AppUserInfo GetAppUserInfoByCardNoAndSchoolNum(string cardNo, string schoolNum)
       {
           AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
           try
           {
               return bllService.GetAppUserInfoByCardNoAndSchoolNum(cardNo,schoolNum);
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
        /// 绑定app相关的用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
       public static bool BindAppUserInfo(AppUserInfo model)
       {
           AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
           try
           {
               return bllService.BindAppUserInfo(model);
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
