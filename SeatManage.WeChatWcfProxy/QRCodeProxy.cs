using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;

namespace SeatManage.WeChatWcfProxy
{
    public class QRCodeProxy
    {
        /// <summary>
        /// 扫描选座终端二维码操作
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public static string QRcodeOperation(string codeStr, string studentNo, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.QRcodeOperation(codeStr, studentNo);
            }
            catch (Exception ex)
            {
                WriteLog.Write("扫描选座终端二维码操作失败：" + ex.Message);
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
        /// 扫描座位二维码
        /// </summary>
        /// <param name="codeStr">扫描后的二维码</param>
        /// <returns></returns>
        public static string QRcodeSeatInfo(string codeStr, string endPortAddress)
        {
            IWeChatWcfService.IWeChatService weCharService = WeChatWcfChannel.ServiceProxy.CreateChannelSeatManageService(endPortAddress);
            try
            {
                return weCharService.QRcodeSeatInfo(codeStr);
            }
            catch (Exception ex)
            {
                WriteLog.Write("扫描座位二维码操作失败：" + ex.Message);
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
