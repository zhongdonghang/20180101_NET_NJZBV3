using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;

namespace SeatService.MonitorService
{
    public class MonitorService:IService.IService
    {
        private Code.SeatDataOperation monitorService;//监控服务处理类
        private TimeLoop timeLoop;//循环时间  
        string loopInterval = ConfigurationManager.AppSettings["MonitorServiceInterval"];
        public override string ToString()
        {
            return "监控服务：座位管理系统座位监控服务";
        }
        public void Start()
        {
            int loopTime = 0;
            if (!int.TryParse(loopInterval, out loopTime))
            {
                WriteLog.Write("读者信息同步：运行间隔时间获取失败，请检查是否配置了‘MonitorServiceInterval’项");
            }
            monitorService = new Code.SeatDataOperation();
            monitorService.ClearSeat();
            timeLoop = new TimeLoop(loopTime);
            timeLoop.TimeTo += new EventHandler(timeLoop_TimeTo);
            timeLoop.TimeStrat();
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
                    //重新获取阅览室设置
                    monitorService.GetSetting();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：获取阅览室设置遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //开闭馆处理
                    monitorService.OpenCloseReadingRoom();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：开闭馆处理遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //超时处理（在座超时，预约超时，暂离超时，等待成功，违规记录过期）
                    monitorService.EnterOutLogOperate();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：处理超时记录遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //处理预约记录
                    monitorService.BeapeakLogOperating();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：处理预约超时遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //异常锁定处理
                    monitorService.LockOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：解锁锁定座位遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //黑名单到时处理
                    monitorService.BlacklistOperating();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：处理黑名单遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //违规记录超时处理
                    monitorService.ViolationRecordsOperating();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("监控服务：处理违规记录遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：服务执行遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
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
