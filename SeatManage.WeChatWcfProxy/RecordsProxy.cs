using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;

namespace SeatManage.WeChatWcfProxy
{
    public class RecordsProxy
    {
        /// <summary>
        /// 获取进出记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public static string GetEnterOutLog(string studentNo, int pageIndex, int pageSize, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetEnterOutLog(studentNo, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取进出记录操作失败：" + ex.Message);
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
        /// 获取预约记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public static string GetBesapsekLog(string studentNo, int pageIndex, int pageSize, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetBesapsekLog(studentNo, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取预约记录操作失败：" + ex.Message);
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
        /// 获取黑名单记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public static string GetBlacklist(string studentNo, int pageIndex, int pageSize, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetBlacklist(studentNo, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取黑名单记录操作失败：" + ex.Message);
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
        /// 获取违规记录
        /// </summary>
        /// <param name="studentNo">用户学号</param>
        /// <param name="pageIndex">页编码</param>
        /// <param name="pageSize">每页数目</param>
        /// <returns></returns>
        public static string GetViolationLog(string studentNo, int pageIndex, int pageSize, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetViolationLog(studentNo, pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取违规记录操作失败：" + ex.Message);
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
