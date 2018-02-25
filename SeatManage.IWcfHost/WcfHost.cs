using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.ServiceModel;

namespace SeatManage.WCFService
{
    public class WcfHost : IService.IService
    {
        ServiceHost host = null;
        ServiceHost host2 = null;
        ServiceHost host3 = null;
        ServiceHost host4 = null;
       // string ReaderOperateEndpointAddress = ConfigurationManager.AppSettings["ReaderOperateEndpointAddress"];//服务通信地址 
        public override string ToString()
        {
            return "座位管理系统远程数据访问服务";
        }
        public void Start()
        {
            try
            {
                host = new ServiceHost(typeof(WcfServiceForSeatManage.SeatManageDateService));
                host2 = new ServiceHost(typeof(WcfServiceForTransportService.TransportService));
                //host3 = new ServiceHost(typeof(SeatManage.PocketBespeakBllService.PocketBespeakBllService));
                host4 = new ServiceHost(typeof(SeatManage.WeChatWcfService.WeChatService));

                host.Open();
                host2.Open();
                //host3.Open();
                host4.Open();
            }
            catch
            {
                throw;
            }
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
                    host4.Close();
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
            if (host4 != null)
            {
                host4.Close();
            }
            GC.SuppressFinalize(this); 
        }
    }
}
