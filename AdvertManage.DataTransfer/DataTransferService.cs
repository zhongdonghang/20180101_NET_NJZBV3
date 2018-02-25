using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AdvertManage.DataTransfer
{
    /// <summary>
    /// 数据中转服务
    /// </summary>
    public class DataTransferService : IService.IService
    {
        System.Timers.Timer timer = null;
        double timerInterval = double.Parse(ConfigurationManager.AppSettings["Interval"]);
        public override string ToString()
        {
            return "数据中转服务";
        }
        DataTransfer dataTransfer = new DataTransfer();
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                if (ServiceSet.IsOnline)
                {//如果在线，执行相关操作，否则不执行
                    dataTransfer.CommandHandle();
                    dataTransfer.UpdateDeviceState();
                    dataTransfer.GetDevice();
                    dataTransfer.LogUpload();
                }
                //授权，根据实际设置操作
                dataTransfer.Empower();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("服务执行遇到异常：异常来自：{0}，异常信息：{1}", ex.Source, ex.Message));
            }
            finally
            {
                timer.Start();
            }
        }

        public void Start()
        {
            timer = new System.Timers.Timer(timerInterval);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start(); 
        }


        public void Stop()
        {
            timer.Stop(); 
        }

        public void Dispose()
        {
            Stop();
            timer.Dispose();
            GC.SuppressFinalize(this); 
        }
    }
}
