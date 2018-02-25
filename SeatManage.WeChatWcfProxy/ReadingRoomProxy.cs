using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;

namespace SeatManage.WeChatWcfProxy
{
    public class ReadingRoomProxy
    {
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public static string GetAllRoomInfo(string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetAllRoomInfo();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取全部阅览室的基础信息操作失败：" + ex.Message);
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
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public static string GetCanBespeakRoomInfo(string date, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetCanBespeakRoomInfo(date);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取可预约阅览室信息操作失败：" + ex.Message);
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
        /// 获取可预约的阅览室
        /// </summary>
        /// <returns></returns>
        public static string GetCanBespeakRoom(string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetCanBespeakRoom();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取可预约的阅览室操作失败：" + ex.Message);
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
        /// 根据阅览室获取可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public static string GetBespeakDate(string roomNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetBespeakDate(roomNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("根据阅览室获取可预约的日期操作失败：" + ex.Message);
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
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        public static string GetAllRoomNowState(string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetAllRoomNowState();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取全部阅览室的当前的使用状态操作失败：" + ex.Message);
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
        /// 根据阅览室编号获取但个阅览室当前开闭状态
        /// </summary>
        /// <returns></returns>
        public static string GetSingleRoomOpenState(string roomNo, string datetime, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetSingleRoomOpenState(roomNo, datetime);
            }
            catch (Exception ex)
            {
                WriteLog.Write("根据阅览室编号获取但个阅览室当前开闭状态操作失败：" + ex.Message);
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
        /// 获取阅览室布局图
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public static string GetRoomSeatLayout(string roomNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetRoomSeatLayout(roomNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取阅览室布局图操作失败：" + ex.Message);
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
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public static string GetRoomBesapeakState(string roomNo, string bespeakTime, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetRoomBesapeakState(roomNo, bespeakTime);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取阅览室可预约的座位操作失败：" + ex.Message);
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
        /// 获取全部图书馆的使用情况
        /// </summary>
        /// <returns></returns>
        public static string GetLibraryNowState(string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetLibraryNowState();
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取全部图书馆的使用情况操作失败：" + ex.Message);
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
