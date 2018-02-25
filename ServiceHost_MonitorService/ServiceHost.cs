using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ServiceHost_MonitorService
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
                serviceHost = new Service.ServiceMonitor();
                try
                {
                    if (serviceHost != null)
                    {
                        serviceHost.Start();
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("“{0}”已启动", serviceHost.ToString()));
                    }
                    else
                    {
                        SeatManage.SeatManageComm.WriteLog.Write(string.Format("“{0}”启动失败，服务初始化失败", serviceHost.ToString()));

                    }
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
            if (serviceHost!=null)
            {
                serviceHost.Stop();
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("“{0}”已关闭", serviceHost.ToString()));

            }
        }
    }
}
