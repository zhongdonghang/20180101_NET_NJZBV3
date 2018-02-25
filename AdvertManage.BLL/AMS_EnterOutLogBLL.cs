using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AdvertManage.Model.Enum;
using System.ServiceModel;

namespace AdvertManage.BLL
{
    public class AMS_EnterOutLogBLL
    {
        /// <summary>
        /// 添加进出记录列表
        /// </summary>
        /// <param name="logList"></param>
        /// <returns></returns>
        public static HandleResult AddEnterOutLogList(List<Model.AMS_EnterOutLog> modelList)
        {  
           IWCFService.IAdvertManageService advertService = WcfAccessProxy.AMS_ServiceProxy.CreateChannelAdvertManageService();
           bool error = false;
           try
           {
               return  advertService.AddEnterOutLogList(modelList);
           }
           catch (Exception ex)
           {
               error = true;
               SeatManage.SeatManageComm.WriteLog.Write(string.Format("添加进出记录列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
