using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace ServiceHost_TimerHost
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        private static void Main()
        {
            //SetConfigFile();
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] { new ServiceHost() };
            ServiceBase.Run(ServicesToRun);
        }

        /// <summary> 
        /// 使用指定路径中的指定文件作为配置文件
        /// </summary> 
        /// <param name="configFileName">配置文件名</param> 
        /// <param name="configFileDirectory">配置文件路径</param> 
        static void SetConfigFile()
        {
            string configFileName = "ServiceHostMonitorService.exe.config";
            string configFileDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string configFilePath = System.IO.Path.Combine(configFileDirectory, configFileName);
            AppDomain.CurrentDomain.SetData("APP_CONFIG_FILE", configFilePath);
        }
    }
}
