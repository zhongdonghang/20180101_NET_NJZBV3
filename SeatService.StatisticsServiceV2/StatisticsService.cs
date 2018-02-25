using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;

namespace SeatService.StatisticsService
{
    public class StatisticsService : IService.IService
    {
        private bool enterOutLogIsStatictics = false;
        private bool roomFlowIsStatictics = false;
        private bool roomUsageIsStatictics = false;
        private bool terminalFlowIsStatistics = false;
        private bool terminalUsageIsStatictics = false;
        private Code.EnterOutLogStatistics enterOutLogStatictics;
        private Code.RoomFlowStatistics roomFlowStatictics;
        private Code.RoomUsageStatistics roomUsageStatictics;
        private Code.TerminalFlowStatistics terminalFlowStatistics;
        private Code.TerminalUsageStatistics terminalUsageStatictics;
        private Code.AdvertUsageStatistics advertUsageStatistics;
        private TimeLoop timeLoop;//循环时间  
        private DateTime runTime;
        string loopInterval = ConfigurationManager.AppSettings["StatisticsServiceInterval"];
        string staticticsTime = ConfigurationManager.AppSettings["StatisticsServiceRunTime"];
        public override string ToString()
        {
            return "数据统计服务：数据统计服务程序启动";
        }
        public void Start()
        {
            try
            {
                int loopTime = 0;
                if (!int.TryParse(loopInterval, out loopTime))
                {
                    WriteLog.Write("数据统计服务：运行间隔时间获取失败，请检查是否配置了‘StatisticsServiceInterval’项");
                }
                if (!DateTime.TryParse(staticticsTime, out runTime))
                {
                    WriteLog.Write("数据统计服务：运行间隔时间获取失败，请检查是否配置了‘StatisticsServiceRunTime’项");
                }
                enterOutLogStatictics = new Code.EnterOutLogStatistics();
                roomFlowStatictics = new Code.RoomFlowStatistics();
                roomUsageStatictics = new Code.RoomUsageStatistics();
                terminalFlowStatistics = new Code.TerminalFlowStatistics();
                terminalUsageStatictics = new Code.TerminalUsageStatistics();
                advertUsageStatistics = new Code.AdvertUsageStatistics();
                //时间循环启动
                timeLoop = new TimeLoop(loopTime);
                timeLoop.TimeTo += new EventHandler(timeLoop_TimeTo);
                timeLoop.TimeStrat();
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：服务执行遇到错误:{0}", ex.Message));
            }
        }
        /// <summary>
        /// 时间达到监控处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timeLoop_TimeTo(object sender, EventArgs e)
        {
            //停止计时
            timeLoop.TimeStop();
            try
            {
                try
                {
                    //统计进出记录
                    enterOutLogStatictics.Run(ref enterOutLogIsStatictics, runTime);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：删除过期广告遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //删除过期的广告
                    advertUsageStatistics.AdvertOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：删除过期广告遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //阅览室人次统计
                    roomFlowStatictics.Run(ref roomFlowIsStatictics, runTime);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：统计阅览室人流量遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //阅览室使用情况统计
                    roomUsageStatictics.Run(ref roomUsageIsStatictics, runTime);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：统计阅览室使用情况遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //终端人次统计
                    terminalFlowStatistics.Run(ref terminalFlowIsStatistics, runTime);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：统计设备人流量遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //终端使用情况统计
                    terminalUsageStatictics.Run(ref terminalUsageIsStatictics, runTime);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("数据统计服务：统计设备使用情况遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：服务执行遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
            }
            finally
            {
                //开始计时
                timeLoop.TimeStrat();
            }
        }
        public void Stop()
        {
            timeLoop.TimeClose();
            //wcfserver.Stop(); 
        }

        public void Dispose()
        {
            Dispose(true);
            //通知垃圾回收机制不再调用终结器（析构器） 
            GC.SuppressFinalize(this);
        }
        private bool _alreadyDisposed = false;
        // 虚拟的Dispose方法 
        protected virtual void Dispose(bool isDisposing)
        {
            // 不要多次处理 
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                timeLoop.TimeStop();

            }
            // TODO: 此处释放非受控资源。设置被处理过标记 
            _alreadyDisposed = true;
        }
    }
}
