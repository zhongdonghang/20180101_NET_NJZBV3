using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHost_WeChatWCFHost.Service
{
   public class WeChatWCFService : IService.IService
    {
        private System.ServiceModel.ServiceHost wechatwcfhost;
        public void Start()
        {
            try
            {
                wechatwcfhost = new System.ServiceModel.ServiceHost(typeof(SeatManage.WeChatWcfService.WeChatService));
                wechatwcfhost.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Stop()
        {
            try
            {
                if (wechatwcfhost != null)
                {
                    wechatwcfhost.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            try
            {
                if (wechatwcfhost != null)
                {
                    wechatwcfhost.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            GC.SuppressFinalize(this);
        }
        public override string ToString()
        {
            return "座位系统微信数据传输服务";
        }
    }
}
