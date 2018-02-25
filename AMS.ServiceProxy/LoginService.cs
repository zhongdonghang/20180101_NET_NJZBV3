using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class LoginService
    {
        /// <summary>
        /// 用户登录操作，登录成功，返回用户Model
        /// 验证错误返回null。
        /// 有异常直接throw到上层。
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static AMS.Model.AMS_UserInfo Login(string loginName, string password)
        {

           //return new Model.AMS_UserInfo() { ID = 1, UserName = "张三三", UserPwd = "skdfjalkdsjalkjf", LoginId = "user" };

            //登录方法代理
            AMS.IBllService.IAdvertManageBllService bllService =  AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            try
            {
                return bllService.Login(loginName,password);
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
