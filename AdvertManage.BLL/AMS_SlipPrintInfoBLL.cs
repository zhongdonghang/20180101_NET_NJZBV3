using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class AMS_SlipPrintInfoBLL
    {
        /// <summary>
        /// 添加优惠券打印次数
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static AdvertManage.Model.Enum.HandleResult AddSlipPrintInfo(List<Model.AMS_SlipPrintInfoModel> modelList)
        {
            IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
            bool error = false;
            try
            {
                return advertService.AddSlipPrintInfo(modelList);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加优惠券打印次数遇到异常，异常模块：{0}；信息：{1}", ex.Source, ex.Message));
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
