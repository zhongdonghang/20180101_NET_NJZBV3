using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.WCFService;
using System.ServiceModel;

namespace AdvertManage.WCFHost
{
    public class AM_WcfHost : IService.IService
    {
        ServiceHost host = null;
        ServiceHost host2 = null;
        public override string ToString()
        {
            return "媒体管理平台远程数据访问服务";
        }
        public void Start()
        {
            try
            {
                host = new ServiceHost(typeof(AdvertManage.WCFService.AdvertManageService));
                host2 = new ServiceHost(typeof(WcfServiceForTransportService.TransportService));
                host.Open();
                host2.Open(); 
            }
            catch(Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("媒体管理平台远程数据访问服务启动失败：{0}",ex.Message));
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
                host2.Close();
            }
            GC.SuppressFinalize(this); 
        }
    }
}
