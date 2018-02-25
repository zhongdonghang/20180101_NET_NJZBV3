using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AMS.Service
{
    public class AMS_Service:IService.IService
    {
        ServiceHost host = null;
        ServiceHost host2 = null;
        ServiceHost host3 = null;
        public override string ToString()
        {
            return "媒体管理平台业务服务";
        }
        public void Start()
        {
            host = new ServiceHost(typeof(AMS.BllService.AdvertManageBllService));
            host2 = new ServiceHost(typeof(WcfServiceForTransportService.TransportService));
            //host3 = new ServiceHost(typeof(AdvertManage.WCFService.AdvertManageService));
            host.Open();
            host2.Open();
            //host3.Open();
        }

        public void Stop()
        {
            try
            {
                if (host != null)
                {
                    host.Close();
                    host2.Close();
                    host3.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            if (host != null)
            {
                host.Close();
            }
            if (host2 != null)
            {
                host2.Close();
            }
            if (host3 != null)
            {
                host3.Close();
            } 
            GC.SuppressFinalize(this); 
        }
    }
}
