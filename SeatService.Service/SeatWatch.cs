using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;
using SeatManage.WCFService;
using System.Configuration;

namespace SeatService.Service
{
    public class SeatWatch : IService.IService
    {
        private bool ReaderInfoUpdated = false;//读者库更新标示符
        private bool EnterOutLogStatistic = false;//进出记录统计标示符;
        private WatchOperate SeatServerWatch;//监控服务处理类
        private TimeLoop timeLoop;//循环时间  

        public override string ToString()
        {
            return "座位监控服务";
        }
        public void Start()
        {
            try
            {
                //监控服务启动
                SeatServerWatch = new WatchOperate();
                SeatServerWatch.ServiceStartOperate();

                //标示读者库更新
                ReaderInfoUpdated =  false;
                //时间循环启动
                timeLoop = new TimeLoop(15000);
                timeLoop.TimeTo += new EventHandler(timeLoop_TimeTo);
                timeLoop.TimeStrat();
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("服务执行遇到错误:{0}", ex.Message));
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
                    //重新获取阅览室设置
                    SeatServerWatch.GetSetting();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("获取阅览室设置遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //开闭馆处理
                    SeatServerWatch.OpenCloseReadingRoom();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("开闭馆处理遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //超时处理（在座超时，预约超时，暂离超时，等待成功，违规记录过期）
                    SeatServerWatch.OverTimeOperate();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("处理超时记录遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //异常锁定处理
                    SeatServerWatch.LockOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("解锁锁定座位遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //黑名单到时处理
                    SeatServerWatch.BlacklistOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("处理黑名单遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //违规记录超时处理
                    SeatServerWatch.ViolationRecordsOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("处理违规记录遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //删除过期媒体文件
                    //SeatServerWatch.MediaOverDate();
                    SeatServerWatch.AdvertOverTime();
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("处理过期的媒体文件遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //读者库同步
                    SeatServerWatch.SysnceReaderInfo(ref ReaderInfoUpdated);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("同步读者信息遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //进出记录统计
                    SeatServerWatch.EnterOutStatistics(ref EnterOutLogStatistic);
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("统计进出记录遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }
                try
                {
                    //删除一个月前的日志文件
                    WriteLog.DeleteLog(DateTime.Now.AddMonths(-1));
                }
                catch (Exception ex)
                {
                    WriteLog.Write(string.Format("删除过期日志文件遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
                }

            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("服务执行遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
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
