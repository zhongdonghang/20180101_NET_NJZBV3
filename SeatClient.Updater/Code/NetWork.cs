/************************************************************
 * 作者：王随
 * 创建日期：2013-5-30
 * 修改时间：
 * 说明：选座终端配置初始化功能类，调用Run执行初始化。初始化过程信息通过事件传递给调用者。
 * ------如果初始化错误，则提示相应的信息。成功初始化结束，触发初始化结束事件，以通知调用者初始化成功。
 * **********************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading;
using SeatClient.OperateResult;
using SeatManage.Bll;

namespace SeatClient.Updater.Code
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
    public class NetWork
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
                Thread.Sleep(3000);//不能成功获取时间，则线程暂停3s，重新再获取
                if (_isDispose)
                {
                    myThread.Abort();
                }
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
                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("连接远程服务遇到错误，正在等待重试。您也可以选择退出，检查终端配置。");
                }
                return false;
            }
        } 
    }
}
