using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;

namespace SeatManage.WeChatWcfProxy
{
    public class SeatProxy
    {
        /// <summary>
        /// 获取常用座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">获取的座位数目</param>
        /// <param name="dayCount">统计的天数</param>
        /// <returns></returns>
        public static string GetOftenSeat(string studentNo, int seatCount, int dayCount, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetOftenSeat(studentNo, seatCount, dayCount);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取常用座位操作失败：" + ex.Message);
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
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public static string GetRandomSeat(string roomNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetRandomSeat(roomNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取随机座位操作失败：" + ex.Message);
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
        /// 获取座位预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public static string GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetSeatBespeakInfo(seatNo, roomNo, bespeakTime);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取座位预约信息操作失败：" + ex.Message);
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
        /// 获取座位当前状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <param name="isMessager"></param>
        /// <returns></returns>
        public static string GetSeatNowStatus(string seatNo, string roomNo, string studentNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetSeatNowStatus(seatNo, roomNo, studentNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取座位当前状态操作失败：" + ex.Message);
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
        /// 管理员获取座位的状态
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public static string GetMessageSeatStatus(string seatNo, string roomNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.GetMessageSeatStatus(seatNo, roomNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("管理员获取座位的状态操作失败：" + ex.Message);
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
