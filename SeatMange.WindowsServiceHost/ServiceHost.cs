using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace SeatManage.WindowsServiceHost
{
    public partial class ServiceHost : ServiceBase
    {
        public ServiceHost()
        {
            InitializeComponent();
        }
        List<IService.IService> services =null;
        protected override void OnStart(string[] args)
        {
            try
            {
                services = IService.ServiceFactory.CreateServiceAssemblys();
                if (services.Count == 0)
                {
                    SeatManage.SeatManageComm.WriteLog.Write("没有需要启动的服务");

                    return;
                }
                foreach (IService.IService service in services)
                {
                    try
                    {
                        service.Start();
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("服务{0}已启动", service.ToString()));
                    }
                    catch (Exception ex)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("服务{0}启动遇到异常：{1}", service.ToString(), ex.Message));

                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("ServiceHost启动遇到异常：{0}",  ex.Message));
            }
        }

        protected override void OnStop()
        {
            foreach (IService.IService service in services)
            {
                service.Stop();
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("服务{0}已关闭", service.ToString()));
            }
        }
    }
}
