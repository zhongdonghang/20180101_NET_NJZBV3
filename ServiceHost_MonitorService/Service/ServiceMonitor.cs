using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using SeatManage.AppJsonModel;

namespace ServiceHost_MonitorService.Service
{
    public class ServiceMonitor : IService.IService
    {
        private List<string> servicesName;
        private Dictionary<string, int> servicesStatus;
        private System.Timers.Timer t;
        private bool isRestart;
        private bool isRestarted;
        private DateTime restarTime;
        private bool checkWeCharWCF;
        private bool chechSeatWCF;
        private bool serviceStatusLog;
        private string weChatEndpointAddress;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ServiceMonitor()
        {
            try
            {
                servicesName = GetAssemblyInfo();
                servicesStatus=new Dictionary<string, int>();
                foreach (string servce in servicesName)
                {
                    servicesStatus.Add(servce, 0);
                }
                t = new System.Timers.Timer(int.Parse(ConfigurationManager.AppSettings["CheckTime"]));
                t.Elapsed += t_Elapsed;
                t.Enabled = true;
                isRestart = ConfigurationManager.AppSettings["RestartService"] == "1";
                checkWeCharWCF = ConfigurationManager.AppSettings["CheckWeCharWCF"] == "1";
                chechSeatWCF = ConfigurationManager.AppSettings["CheckSeatWCF"] == "1";
                serviceStatusLog = ConfigurationManager.AppSettings["ServiceStatusLog"] == "1";
                isRestarted = true;
                if (ConfigurationManager.ConnectionStrings["WeChatEndpointAddress"] != null)
                {
                    weChatEndpointAddress = SeatManage.SeatManageComm.AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["WeChatEndpointAddress"].ConnectionString);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
            }
        }

        void t_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t.Stop();
            try
            {
                MonitorService();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
            }
            finally
            {
                t.Start();

            }
        }

        public void MonitorService()
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                //监控WCF服务
                if (chechSeatWCF)
                {
                    ServiceController wcfservice = services.FirstOrDefault(u => u.ServiceName == "ServiceHostWCFHost");
                    ServiceController timeservice = services.FirstOrDefault(u => u.ServiceName == "ServiceHostTimerHost");
                    if (wcfservice != null)
                    {
                        try
                        {
                            if (wcfservice.Status == ServiceControllerStatus.Stopped)
                            {
                                wcfservice.Start();
                            }
                            else
                            {
                                DateTime dt = SeatManage.Bll.ServiceDateTime.Now;
                            }
                        }
                        catch
                        {
                            if (timeservice != null)
                            {
                                StopService("ServiceHostTimerHost");
                            }
                            StopService("ServiceHostWCFHost");
                            wcfservice.Start();
                            if (timeservice != null)
                            {
                                timeservice.Start();
                            }
                        }
                    }
                }
                if (checkWeCharWCF)
                {
                    ServiceController wcfservice = services.FirstOrDefault(u => u.ServiceName == "ServiceHostWeChatWCFHost");
                    if (wcfservice != null)
                    {
                        try
                        {
                            if (wcfservice.Status == ServiceControllerStatus.Stopped)
                            {
                                wcfservice.Start();
                            }
                            else
                            {
                                string ajmStr = SeatManage.WeChatWcfProxy.ReaderProxy.GetServerTime(weChatEndpointAddress);
                                AJM_HandleResult result=null;
                                if (string.IsNullOrEmpty(ajmStr))
                                {
                                    StopService("ServiceHostWeChatWCFHost");
                                    wcfservice.Start();
                                }
                                result = SeatManage.SeatManageComm.JSONSerializer.Deserialize<AJM_HandleResult>(ajmStr);
                                if (result == null || !result.Result)
                                {
                                    StopService("ServiceHostWeChatWCFHost");
                                    wcfservice.Start();
                                }
                            }
                        }
                        catch
                        {
                            StopService("ServiceHostWeChatWCFHost");
                            wcfservice.Start();
                        }
                    }
                }
                //重启服务
                if (isRestart)
                {
                    if (!isRestarted && DateTime.Now >= DateTime.Parse(ConfigurationManager.AppSettings["RestartTime"] ?? "5:00"))
                    {
                        foreach (string servce in servicesName)
                        {
                            StopService(servce);
                        }
                        isRestarted = true;
                    }
                    else
                    {
                        if (isRestarted && DateTime.Now <= DateTime.Parse(ConfigurationManager.AppSettings["RestartTime"] ?? "5:00"))
                        {
                            isRestarted = false;
                        }
                    }
                }
                foreach (string s in servicesName)
                {
                    ServiceController service = services.FirstOrDefault(u => u.ServiceName == s);
                    if (service == null || service.Status == ServiceControllerStatus.Running)
                    {
                        continue;
                    }
                    if (serviceStatusLog)
                    {
                        SeatManage.SeatManageComm.WriteLog.Write("“" + service.ServiceName + "”状态：" + service.Status.ToString());
                    }
                    if (service.Status == ServiceControllerStatus.Stopped)
                    {
                        servicesStatus[s] = 0;
                        service.Start();
                    }
                    else
                    {
                        servicesStatus[s]++;
                    }
                    if (servicesStatus[s] >= 10)
                    {
                        servicesStatus[s] = 0;
                        StopService(s);
                        service.Start();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        //private static bool StartService(string serviceName)
        //{
        //    try
        //    {

        //        ServiceController[] services = ServiceController.GetServices();
        //        if (serviceStatusLog)
        //        {
        //            foreach (ServiceController service in services.Where(service => service.ServiceName == serviceName).Where(service => service.Status != ServiceControllerStatus.Running))
        //            {
        //                SeatManage.SeatManageComm.WriteLog.Write("“" + serviceName + "”状态：" + service.Status.ToString());
        //            }
        //        }

        //        foreach (ServiceController service in services.Where(service => service.ServiceName == serviceName).Where(service => service.Status == ServiceControllerStatus.Paused || service.Status == ServiceControllerStatus.Stopped))
        //        {
        //            service.Start();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        /// <summary>
        /// 停止服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private static bool StopService(string serviceName)
        {
            try
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps.Where(item => item.ProcessName == serviceName))
                {
                    item.Kill();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取服务配置信息
        /// </summary>
        /// <returns></returns>
        private static List<string> GetAssemblyInfo()
        {
            string configFileName = ConfigurationManager.AppSettings["configFileName"];
            List<string> assemblysInfo = new List<string>();
            XmlDocument doc = new XmlDocument();
            string path = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, configFileName);
            if (File.Exists(path))
            {
                doc.Load(path);
            }
            else
            {
                return assemblysInfo;
            }
            XmlNodeList nodes = doc.SelectNodes("//configuration/appSettings/add");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["key"] != null && node.Attributes["key"].Value == "ServiceHost")
                {
                    assemblysInfo.Add(node.Attributes["value"].Value);
                }
            }
            return assemblysInfo;
        }

        public void Start()
        {
            try
            {
                if (t != null)
                {
                    MonitorService();
                    t.Start();
                }

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
                if (t != null)
                {
                    t.Stop();
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
                if (t != null)
                {
                    t.Stop();
                    t.Dispose();
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
            return "座位系统程序运行监控服务";
        }
    }
}
