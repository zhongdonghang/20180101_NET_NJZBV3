using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;

namespace SeatService.SyncService
{

    public class SyncService : IService.IService
    {
        private Code.ReaderSync readerSync;
        private bool readerInfoUpdated = false;//读者库更新标示符
        private TimeLoop timeLoop;//循环时间  
        string loopInterval = ConfigurationManager.AppSettings["ReaderSyncServiceInterval"];
        public override string ToString()
        {
            return "读者信息同步：读者信息同步程序启动";
        }
        public void Start()
        {
            try
            {
                int loopTime = 0;
                if (!int.TryParse(loopInterval, out loopTime))
                {
                    WriteLog.Write("读者信息同步：运行间隔时间获取失败，请检查是否配置了‘ReaderSyncServiceInterval’项");
                }
                //监控服务启动
                readerSync = new Code.ReaderSync();
                //标示读者库更新
                readerInfoUpdated = false;
                //时间循环启动
                timeLoop = new TimeLoop(loopTime);
                timeLoop.TimeTo += new EventHandler(timeLoop_TimeTo);
                timeLoop.TimeStrat();
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("读者信息同步：服务执行遇到错误:{0}", ex.Message));
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
                //同步读者信息
                readerSync.SysnceReaderInfo(ref readerInfoUpdated);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("读者信息同步：服务执行遇到错误,异常来自：{0},错误信息：{1}", ex.Source, ex.Message));
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
