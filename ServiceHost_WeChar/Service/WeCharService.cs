using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceHost_WeChar.Service
{
    public class WeiCharService:IService.IService
    {
        private SeatManage.MobileAppService.MobileAppService host;
        public void Start()
        {
            try
            {
                host = new SeatManage.MobileAppService.MobileAppService();
                host.Start();
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
                    host.Stop();
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
                    host.Stop();
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
            return "座位管理系统微信客户端访问服务";
        }
    }
}
