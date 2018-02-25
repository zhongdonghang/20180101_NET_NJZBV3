using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHost_FileTransportHost.Service
{
    public class FileService : IService.IService
    {
        private System.ServiceModel.ServiceHost filehost;
        public void Start()
        {
            try
            {
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
            GC.SuppressFinalize(this); 
        }
        public override string ToString()
        {
            return "座位系统远程文件传输服务";
        }
    }
}
