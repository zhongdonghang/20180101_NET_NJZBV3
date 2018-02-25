using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace SeatServiceMonitoringTool
{
    /// <summary>
    ///  本工具监控系统的5个服务程序是否正常运行，如果停止了，就启动
    /// </summary>
    class Program
    {
        private static TimeLoop timeLoop;//循环时间  
        static string loopInterval = ConfigurationManager.AppSettings["CheckTimes"];


        static string CurrentTime
        {
            get
            {
                return DateTime.Now.ToString("yyyy年MM月dd日 hh时mm分ss秒  ");
            }
            
        }

        static void Main(string[] args)
        {
            int loopTime = 0;
            if (!int.TryParse(loopInterval, out loopTime))
            {
                WriteLog.Write("运行间隔时间获取失败，请检查是否配置了‘CheckTimes’项");
            }
            timeLoop = new TimeLoop(loopTime);
            timeLoop.TimeTo += new EventHandler(timeLoop_TimeTo);
            timeLoop.TimeStrat();
            Console.WriteLine(CurrentTime+"监控服务程序启动");
            Console.ReadLine();
        }

        private static void timeLoop_TimeTo(object sender, EventArgs e)
        {
            try
            {
                string ServiceNames = ConfigurationManager.AppSettings["ServiceNames"].ToString();
                string[] listNames = ServiceNames.Split(',');
                foreach (var item in listNames)
                {
                    ServiceController sc = new ServiceController(item);
                    if ((sc.Status.Equals(ServiceControllerStatus.Stopped)) || (sc.Status.Equals(ServiceControllerStatus.StopPending)))
                    {
                        sc.Start();
                        Console.WriteLine(CurrentTime+  "   " + item+"服务程序启动");
                        sc.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("异常信息:"+ ex);
                Console.WriteLine(CurrentTime+ "异常信息:" + ex);
            }
            
        }
    }
}
