using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHost_WCFHost.Service
{
    public class WCFService : IService.IService
    {
        private System.ServiceModel.ServiceHost wcfhost;
        private System.ServiceModel.ServiceHost filehost;
        public void Start()
        {
            try
            {
                wcfhost = new System.ServiceModel.ServiceHost(typeof(WcfServiceForSeatManage.SeatManageDateService));
                wcfhost.Open();
                filehost = new System.ServiceModel.ServiceHost(typeof(WcfServiceForTransportService.TransportService));
                filehost.Open();
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
                if (filehost != null)
                {
                    filehost.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                if (wcfhost != null)
                {
                    wcfhost.Close();
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
                if (filehost != null)
                {
                    filehost.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            try
            {
                if (wcfhost != null)
                {
                    wcfhost.Close();
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
            return "座位系统远程数据传输服务";
        }
    }
}
