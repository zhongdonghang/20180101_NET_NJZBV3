using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.SeatManageComm;
using SeatManage.Bll;
using System.Threading;

namespace SeatClientLeave.Code
{
    /// <summary>
    /// 初始化消息的委托
    /// </summary>
    /// <param name="message">消息</param>
    /// <param name="exceptionType">消息类型</param>
    public delegate void Message(string message);
    /// <summary>
    /// 处理状态
    /// </summary>
    public enum HandState
    {
        None = -1,
        Loading = 0,
        End = 1,
    }
    /// <summary>
    /// 选座终端初始化。
    /// </summary>
    public class ApplicationInitialize
    {
        /// <summary>
        /// 初始化消息事件
        /// </summary>
        public event Message EventInitializeMessage;
        public event EventHandler InitializeEnd;
        private HandState _State = HandState.None;
        /// <summary>
        /// 初始化结果
        /// </summary>
        public HandState State
        {
            get { return _State; }
            set { _State = value; }
        }
        Thread myThread = null;
        public void Run()
        {
            myThread = new Thread(new ThreadStart(initThread));
            myThread.Start();
        }
        bool _isDispose = false;
        public void Dispose(bool isDispose)
        {
            _isDispose = isDispose;
        }

        private void initThread()
        {
            State = HandState.Loading;
            //TODO: 
            while (!CheckNetWork())//如果能成功获取时间，说明网络正常。
            {
                Thread.Sleep(1000);//不能成功获取时间，则线程暂停2s，重新再获取
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            while (!CheckClientConfig())
            {
                Thread.Sleep(10000);
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }

            if (_isDispose)
            {
                myThread.Abort();
            }
             
            if (!CheckCardReader())
            {
                if (_isDispose)
                {
                    myThread.Abort();
                }
                return;
            }
            State = HandState.End;
            if (InitializeEnd != null)
            {
                InitializeEnd(this, new EventArgs());
            }
        }

        /// <summary>
        /// 通过获取服务器时间检查网络是否通畅，获取成功，返回true，否则返回false。
        /// </summary>
        /// <returns></returns>
        private bool CheckNetWork()
        {
            try
            {
                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("正在连接远程服务……");
                }
                DateTime dt = ServiceDateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                WriteLog.Write("初始化网络失败：" + ex.Message);
                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("连接远程服务遇到错误，正在等待重试。您也可以选择退出，检查终端配置。");
                }
                return false;
            }
        }


        /// <summary>
        /// 检查读卡器接口对象
        /// </summary>
        /// <returns></returns>
        private bool CheckCardReader()
        {
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("初始化读卡器……");
            }
            LeaveClientObject clientObject = LeaveClientObject.GetInstance();
            try
            {
                if (clientObject.ObjCardReader != null)
                {
                    clientObject.ObjCardReader.Start();
                    clientObject.ObjCardReader.Stop();
                }
                return true;
            }
            catch(Exception ex)
            {
                if (EventInitializeMessage != null)
                {
                   // SeatManage.SeatManageComm.WriteLog.Write("读卡器初始化失败:"+ex.Message);
                    EventInitializeMessage("读卡器初始化失败:" + ex.Message);
                }
                return false;
            }
        }

        /// <summary>
        /// 检查终端设置是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckClientConfig()
        {
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("初始化终端设置……");
            }
            try
            { 
                LeaveClientObject clientObject = LeaveClientObject.GetInstance();
                if (clientObject.BackgroundImagesResource.Count == 0)
                {
                    if (EventInitializeMessage != null)
                    {
                        EventInitializeMessage("背景图片初始化失败, 请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。");
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                if (EventInitializeMessage != null)
                {
                   // EventInitializeMessage("初始化遇到错误，等待重试……");
                }
                return false;
            }
        }

    }
}
