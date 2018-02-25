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
using SeatManage.SeatManageComm;
using System.Threading;
using SeatClient.OperateResult;
using SeatManage.Bll;

namespace SeatClient.Class
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

            //  CheckCardReader();


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
                Thread.Sleep(5000);//不能成功获取时间，则线程暂停2s，重新再获取
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            while (!CheckMacAddress())
            {
                Thread.Sleep(5000);
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            while (!CheckClientConfig())
            {
                Thread.Sleep(60000);
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            if (!CheckEmpower())
            {
                if (_isDispose)
                {
                    myThread.Abort();
                }
                return;
            }

            if (_isDispose)
            {
                myThread.Abort();
            }
            while (!CheckReadingRoomConfig())
            {
                if (_isDispose)
                {
                    myThread.Abort();
                }
                Thread.Sleep(60000);
            }
            if (!CheckCardReader())
            {
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
        /// 检查终端授权
        /// </summary>
        /// <returns></returns>
        private bool CheckEmpower()
        {
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("检查授权…");
            }
            SystemObject clientObject = SystemObject.GetInstance();
            if (clientObject.ClientSetting.EmpowerLoseEfficacyTime.CompareTo(ServiceDateTime.Now) <= 0)
            {
                EventInitializeMessage("验证授权失败，请检查服务器是否能够访问到Internet…");
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 检查读卡器接口对象
        /// </summary>
        /// <returns></returns>
        public static bool CheckCardReader()
        {

            SystemObject clientObject = SystemObject.GetInstance();
            try
            {
                if (clientObject.ObjCardReader != null)
                {
                    clientObject.ObjCardReader.Start();
                    clientObject.ObjCardReader.Stop();
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("读卡器初始化失败，请退出程序，检查读卡器设置：" + ex.Message);
                // return false;
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
                SystemObject clientObject = SystemObject.GetInstance();
                if (clientObject.ClientSetting == null)
                {
                    if (EventInitializeMessage != null)
                    {
                        EventInitializeMessage("终端设置初始化失败, 可能是终端编号设置错误引起的，请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。");
                    }
                    return false;
                }
                else if (clientObject.BackgroundImagesResource.Count == 0)
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
                    EventInitializeMessage("背景图片初始化失败, 请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。");
                }
                return false;
            }
        }
        /// <summary>
        /// 验证Mac地址，如果和本地不一致，则说明编号配置错误
        /// </summary>
        /// <returns></returns>
        private bool CheckMacAddress()
        {
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("验证终端设置绑定的Mac地址");
            }
            //TODO:验证Mac地址

            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("获取本地MAC地址");
            }
            List<string> localMacAdd;
            try
            {
                localMacAdd = GetMacAddress.GetLocalAddress();
            }
            catch (Exception ex)
            {
                EventInitializeMessage("本地MAC获取失败：" + ex.Message);
                return false;
            }
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("本地MAC获取成功,获取SystemObject单例");
            }
            SystemObject clientObject = SystemObject.GetInstance();
            if (clientObject.ClientSetting == null)
            {
                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("获取终端设置失败, 请检查终端编号是否正确。");
                }
                return false;
            }
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("获取SystemObject单例获取成功");
            }
            if (!string.IsNullOrEmpty(clientObject.ClientSetting.TerminalMacAddress))//mac地址不为空
            {
                foreach (string macAdd in localMacAdd)
                {
                    if (clientObject.ClientSetting.TerminalMacAddress == macAdd)
                    {
                        return true;
                    }
                }

                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("Mac地址验证失败，重新设置终端编号，您也可以通过本地设置程序强行将Mac地址和终端编号锁定");
                }
                return false;
            }
            else
            {
                if (localMacAdd.Count > 0)
                {
                    //TODO:更新终端设置
                    clientObject.ClientSetting.TerminalMacAddress = localMacAdd[0];
                }
                try
                {
                    if (!ClientConfigOperate.UpdateTerminal(clientObject.ClientSetting))
                    {
                        if (EventInitializeMessage != null)
                        {
                            EventInitializeMessage("尝试锁定终端设置的时候出现错误。");
                        }
                        return false;
                    }
                    return true;
                }
                catch
                {
                    if (EventInitializeMessage != null)
                    {
                        EventInitializeMessage("尝试锁定终端设置的时候出现错误。");
                    }
                    return false;
                }
            }

        }
        /// <summary>
        /// 检查阅览室设置是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckReadingRoomConfig()
        {
            if (EventInitializeMessage != null)
            {
                EventInitializeMessage("初始化阅览室设置……");
            }
            try
            {
                SystemObject clientObject = SystemObject.GetInstance();
                if (clientObject.ReadingRoomList == null)
                {
                    if (EventInitializeMessage != null)
                    {
                        EventInitializeMessage("阅览室设置初始化失败，请检查该阅览室的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。");
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
                WriteLog.Write(string.Format("获取阅览室设置失败：{0}", ex.Message));
                if (EventInitializeMessage != null)
                {
                    EventInitializeMessage("阅览室设置初始化失败，请检查该阅览室的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。");
                }
                return false;
            }

        }
    }
}
