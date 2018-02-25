using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using AMS.DataTransfer.Code;

namespace AMS.DataTransfer
{
    public class DataTransferService : IService.IService
    {
        bool isUplaodLog = false;
        System.Timers.Timer timer = null;
        double timerInterval = double.Parse(ConfigurationManager.AppSettings["Interval"]);
        Code.DataTransfer dataTransfer = new Code.DataTransfer();
        public override string ToString()
        {
            return "数据中转服务：数据中转服务启动";
        }

        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            try
            {
                if (ServiceSet.IsOnline)
                {//如果在线，执行相关操作，否则不执行
                    dataTransfer.CommandHandle();
                    dataTransfer.UpdateDeviceState();
                    
                    dataTransfer.GetCommand();
                    dataTransfer.LogUpload();
                }
                //授权，根据实际设置操作
                dataTransfer.GetDevice();
                dataTransfer.Empower();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("数据中转服务:服务执行遇到异常：异常来自：{0}，异常信息：{1}", ex.Source, ex.Message));
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
            try
            {
                dataTransfer.GetDevice();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("数据中转服务：获取授权失败：异常来自：{0}，异常信息：{1}", ex.Source, ex.Message));
            }
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
