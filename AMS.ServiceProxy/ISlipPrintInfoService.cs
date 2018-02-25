using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public class ISlipPrintInfoService
    {
        /// <summary>
        /// 添加优惠券打印次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AMS.Model.Enum.HandleResult AddSlipPrintInfo(List<Model.View_SlipPrintInfo> modelList)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.AddSlipPrintInfo(modelList);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加优惠券打印次数遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
