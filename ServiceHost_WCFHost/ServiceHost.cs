using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ServiceHost_WCFHost
{
    partial class ServiceHost : ServiceBase
    {
        public ServiceHost()
        {
            InitializeComponent();
        }

        IService.IService serviceHost = null;

        protected override void OnStart(string[] args)
        {
            try
            {
                serviceHost = new Service.WCFService();
                try
                {
                    serviceHost.Start(); SeatManage.SeatManageComm.WriteLog.Write(string.Format("“{0}”已启动", serviceHost.ToString()));
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(string.Format("“{0}”启动遇到异常：{1}", serviceHost.ToString(), ex.Message));

                }

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("ServiceHost启动遇到异常：{0}", ex.Message));
            }
        }

        protected override void OnStop()
        {

            serviceHost.Stop();
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("服务“{0}”已关闭", serviceHost.ToString()));

        }
    }
}
