using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class ServerDateTime
    {
        /// <summary>
        /// 服务器当前时间
        /// </summary>
        public static DateTime? Now
        {
            get
            {
                IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
                bool error = false;
                try
                {
                    return advertService.GetServerDateTime();
                }
                catch (Exception ex)
                {
                    error = true;
                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据Id获取服务器时间遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
                    throw ex;
                }
                finally
                {
                    ICommunicationObject ICommObjectService = advertService as ICommunicationObject;
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
}
