using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;

namespace SeatManage.WeChatWcfProxy
{
    public class ReaderProxy
    {
        /// <summary>
        /// 获取用户的基本信息
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>

        public static string GetUserInfo(string studentNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetUserInfo(studentNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取用户的基本信息操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <returns></returns>
        public static string GetUserNowState(string studentNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetUserNowState(studentNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取用户当前的在座状态操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 验证用户信息
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string CheckUser(string loginId, string password, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.CheckUser(loginId, password);
            }
            catch (Exception ex)
            {
                WriteLog.Write("验证用户信息操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取登录读者详细信息
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public static string GetUserInfo_WeiXin(string studentNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetUserInfo_WeiXin(studentNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write(" 获取登录读者详细信息操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取用户当前的在座状态
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public static string GetUserNowStateV2(string studentNo, bool isCheckCode, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetUserNowStateV2(studentNo, isCheckCode);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取用户当前的在座状态操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <param name="endPortAddress"></param>
        /// <returns></returns>
        public static string GetServerTime(string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetServerTime();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取服务器时间操作失败：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "对不起，数据连接失败，请稍后重试。";
                return JSONSerializer.Serialize(result);
            }
            finally
            {
                ICommunicationObject ICommObjectService = weCharService as ICommunicationObject;
                try
                {
                    if (ICommObjectService != null && ICommObjectService.State == CommunicationState.Faulted)
                    {
                        ICommObjectService.Abort();
                    }
                    else
                    {
                        if (ICommObjectService != null) ICommObjectService.Close();
                    }
                }
                catch
                {
                    if (ICommObjectService != null) ICommObjectService.Abort();
                }
            }
        }
    }
}
