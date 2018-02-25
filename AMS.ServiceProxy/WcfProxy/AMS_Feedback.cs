using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.ServiceProxy
{
    public  class AMS_Feedback
    {
        public static AMS.Model.Enum.HandleResult Add_Feedback(AMS.Model.AMS_Feedback model)
        {
            AMS.IBllService.IAdvertManageBllService bllService = AMS.ServiceConnectChannel.AdvertManageBllServiceChannel.CreateServiceChannel();
            bool error = false;
            try
            {
                return bllService.AddFeedback(model);
            }
            catch (Exception ex)
            {
                error = true;
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("根据学校编号获取设备列表遇到错误，异常来自：{0}；信息：{1}", ex.Source, ex.Message));
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
