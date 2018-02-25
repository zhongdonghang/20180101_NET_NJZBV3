using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHost_PockeService.Service
{
    public class PockeWebService:IService.IService
    {
        private System.ServiceModel.ServiceHost host;
        public void Start()
        {
            try
            {
                host = new System.ServiceModel.ServiceHost(typeof (SeatManage.PocketBespeakBllService.PocketBespeakBllService));
                host.Open();
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
                if (host != null)
                {
                    host.Close();
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
                if (host != null)
                {
                    host.Close();
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
            return "座位系统手机网站数据服务";
        }
    }
}
