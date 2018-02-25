using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SeatClientV3.OperateResult;
using SeatManage.Bll;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatClientV3.ViewModel
{
    public class AppLoadingWindow_ViewModel : INotifyPropertyChanged
    {
        public AppLoadingWindow_ViewModel()
        {
            int i = Screen.PrimaryScreen.Bounds.Width;
            if (i == 1080)
            {
                WindowHeight = 1000;
                WindowWidth = 1080;
                WindowLeft = 0;
                WindowTop = 920;
            }
            else
            {
                WindowHeight = Screen.PrimaryScreen.Bounds.Height;
                WindowWidth = Screen.PrimaryScreen.Bounds.Width;
                WindowLeft = 0;
                WindowTop = 0;
            }
        }

        private bool _isReconnect;
        /// <summary>
        /// 是否是重连
        /// </summary>
        public bool IsReconnect
        {
            get { return _isReconnect; }
            set { _isReconnect = value; }
        }
        private string _ShowMessage = "加载中……";
        /// <summary>
        /// 显示消息
        /// </summary>
        public string ShowMessage
        {
            get { return _ShowMessage; }
            set { _ShowMessage = value; OnPropertyChanged("ShowMessage"); }
        }
        /// <summary>
        /// 处理状态
        /// </summary>
        public enum HandState
        {
            None = -1,
            Loading = 0,
            End = 1
        }
        HandleResult _InitializeState = HandleResult.Failed;
        /// <summary>
        /// 处理结果
        /// </summary>
        public HandleResult InitializeState
        {
            get { return _InitializeState; }
            set { _InitializeState = value; }
        }
        public event EventHandler InitializeEnd;
        private HandState _State = HandState.None;
        Thread myThread;
        bool _isDispose;
        /// <summary>
        /// 初始化结果
        /// </summary>
        public HandState State
        {
            get { return _State; }
            set { _State = value; }
        }
        private double _WindowHeight;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region 初始化方法

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="isDispose"></param>
        public void Dispose(bool isDispose)
        {
            _isDispose = isDispose;
        }
        /// <summary>
        /// 开始运行
        /// </summary>
        public void Run()
        {
            if (myThread != null)
            {
                myThread.Abort();
            }
            myThread = new Thread(IntiApp);
            myThread.SetApartmentState(ApartmentState.STA);
            myThread.IsBackground = true;
            myThread.Start();
        }
        /// <summary>
        /// 初始化主流程
        /// </summary>
        private void IntiApp()
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
            if (IsReconnect)
            {
                State = HandState.End;
                if (InitializeEnd != null)
                {
                    InitializeEnd(this, new EventArgs());
                }
                return;
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
            //if (!InitializationWindow())
            //{
            //    return;
            //}
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
            ShowMessage = "正在连接远程服务……";



            if (!CheckInternet.CheckLocal())
            {
                ShowMessage = "连接网络失败，请检查网卡状态或网线是否接好！";
                return false;
            }
            if (ConfigurationManager.ConnectionStrings["EndpointAddress"] != null)
            {

                string endpointAddress = AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString);
                //net.tcp://192.168.1.100:8201/
                string straddress = endpointAddress.Replace("net.tcp://", "");
                straddress = straddress.Substring(0, straddress.Length - 1);
                if (!CheckInternet.CheckPort(straddress.Split(':')[0], straddress.Split(':')[1]))
                {
                    ShowMessage = "服务器连接失败，请检查服务器设置或网络配置！";
                    return false;
                }
                string pingip = ConfigurationManager.AppSettings["PingIP"];
                IPAddress ip;
                if (ConfigurationManager.AppSettings["PingServer"] != null && ConfigurationManager.AppSettings["PingServer"] == "1" && IPAddress.TryParse(pingip, out ip))
                {
                    CheckInternet.PingIp(pingip);
                }
            }
            else
            {
                ShowMessage = "获取服务器地址失败，请检查终端配置！";
                return false;
            }
            try
            {
                DateTime dt = ServiceDateTime.Now;
            }
            catch (Exception ex)
            {

                WriteLog.Write("连接数据传输服务失败：" + ex.Message);
                ShowMessage = "连接远程服务遇到错误，请检查服务器服务是否启动。";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查终端授权
        /// </summary>
        /// <returns></returns>
        private bool CheckEmpower()
        {
            ShowMessage = "检查授权…";
            SystemObject clientObject = SystemObject.GetInstance();
            if (clientObject.ClientSetting.EmpowerLoseEfficacyTime.CompareTo(ServiceDateTime.Now) <= 0)
            {
                ShowMessage = "验证授权失败，请检查服务器是否能够访问到Internet…";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 检查读卡器接口对象
        /// </summary>
        /// <returns></returns>
        public bool CheckCardReader()
        {
            SystemObject clientObject = SystemObject.GetInstance();
            try
            {
                if (clientObject.ObjCardReader != null)
                {
                    clientObject.ObjCardReader.Start();
                    clientObject.ObjCardReader.Stop();
                }
                else if (clientObject.UseCardReader)
                {
                    ShowMessage = "读卡器初始化失败，请退出程序，检查读卡器设置接口";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage = "读卡器初始化失败，请退出程序，检查读卡器设置：" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 检查终端设置是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckClientConfig()
        {
            ShowMessage = "初始化终端设置……";
            try
            {
                SystemObject clientObject = SystemObject.GetInstance();
                if (clientObject.ClientSetting == null)
                {
                    ShowMessage = "终端设置初始化失败, 可能是终端编号设置错误引起的，请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage = "终端初始化失败, 请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                return false;
            }
        }
        private bool CheckMacAddress()
        {
            ShowMessage = "验证终端设置绑定的Mac地址";
            //TODO:验证Mac地址
            ShowMessage = "获取本地MAC地址";
            List<string> localMacAdd;
            try
            {
                localMacAdd = GetMacAddress.GetLocalAddress();
            }
            catch (Exception ex)
            {
                ShowMessage = "本地MAC获取失败：" + ex.Message;
                return false;
            }
            ShowMessage = "本地MAC获取成功,获取SystemObject单例";
            SystemObject clientObject = SystemObject.GetInstance();
            if (clientObject.ClientSetting == null)
            {
                ShowMessage = "获取终端设置失败, 请检查终端编号是否正确。";
                return false;
            }
            ShowMessage = "获取SystemObject单例获取成功";

            if (!string.IsNullOrEmpty(clientObject.ClientSetting.TerminalMacAddress))//mac地址不为空
            {
                foreach (string macAdd in localMacAdd)
                {
                    if (clientObject.ClientSetting.TerminalMacAddress == macAdd)
                    {
                        return true;
                    }
                }
                ShowMessage = "Mac地址验证失败，重新设置终端编号，您也可以通过本地设置程序强行将Mac地址和终端编号锁定";
                return false;
            }
            if (localMacAdd.Count > 0)
            {
                //TODO:更新终端设置
                clientObject.ClientSetting.TerminalMacAddress = localMacAdd[0];
            }
            try
            {
                if (!string.IsNullOrEmpty(TerminalOperatorService.UpdateTeminalSetting(clientObject.ClientSetting)))
                {
                    ShowMessage = "尝试锁定终端设置的时候出现错误。";
                    return false;
                }
                return true;
            }
            catch
            {
                ShowMessage = "尝试锁定终端设置的时候出现错误。";
                return false;
            }
        }
        /// <summary>
        /// 检查阅览室设置是否正确
        /// </summary>
        /// <returns></returns>
        private bool CheckReadingRoomConfig()
        {

            ShowMessage = "初始化阅览室设置……";
            try
            {
                SystemObject clientObject = SystemObject.GetInstance();
                if (clientObject.ReadingRoomList == null)
                {
                    ShowMessage = "阅览室设置初始化失败，请检查该阅览室的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室设置失败：{0}", ex.Message));
                ShowMessage = "阅览室设置初始化失败，请检查该阅览室的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                return false;
            }
        }

        /// <summary>
        /// 初始化窗体
        /// </summary>
        /// <returns></returns>
        private bool InitializationWindow()
        {
            if (_isReconnect)
            {
                return true;
            }
            ShowMessage = "正在初始化窗体……";
            try
            {

                //ShowMessage = "正在初始化启动窗体……";
                //AppLoadingWindowObject.GetInstance();
                //ShowMessage = "正在初始化键盘窗体……";
                //KeyboardWindowObject.GetInstance();
                //ShowMessage = "正在初始化操作选择窗体……";
                //LeaveWindowObject.GetInstance();
                //ShowMessage = "正在初始化主窗体……";
                //MainWindowObject.GetInstance();
                //ShowMessage = "正在初始化消息提醒窗体……";
                //PopupWindowsObject.GetInstance();
                //ShowMessage = "正在初始化短消息窗体……";
                //ReaderNoteWindowObject.GetInstance();
                //ShowMessage = "正在初始化阅览室窗体……";
                //ReadingRoomWindowObject.GetInstance();
                //ShowMessage = "正在初始化记录查询窗体……";
                //RecordTheQueryWindowObject.GetInstance();
                //ShowMessage = "正在初始化阅览室布局图窗体……";
                //RoomSeatWindowObject.GetInstance();
                //ShowMessage = "正在初始化常坐座位窗体……";
                //UsuallySeatWindowObject.GetInstance();
                return true;
            }
            catch (Exception ex)
            {
                ShowMessage = "初始化窗体失败，具体情况请检查错误日志。";
                return false;
            }
        }
        ///// <summary>  
        ///// 根据IP地址获得主机名称  
        ///// </summary>  
        ///// <param name="ip">主机的IP地址</param>  
        ///// <returns>主机名称</returns>  
        //public string GetHostNameByIp(string ip)
        //{
        //    ip = ip.Trim();
        //    if (ip == string.Empty)
        //        return string.Empty;
        //    try
        //    {
        //        // 是否 Ping 的通  
        //        if (this.Ping(ip))
        //        {
        //            System.Net.IPHostEntry host = System.Net.Dns.GetHostEntry(ip);
        //            return host.HostName;
        //        }
        //        else
        //            return string.Empty;
        //    }
        //    catch (Exception)
        //    {
        //        return string.Empty;
        //    }
        //}
        ///// 是否能 Ping 通指定的主机  
        ///// </summary>  
        ///// <param name="ip">ip 地址或主机名或域名</param>  
        ///// <returns>true 通，false 不通</returns>  
        //public bool Ping(string ip)
        //{
        //    System.Net.NetworkInformation.Ping p = new System.Net.NetworkInformation.Ping();
        //    System.Net.NetworkInformation.PingOptions options = new System.Net.NetworkInformation.PingOptions();
        //    options.DontFragment = true;
        //    string data = "Test Data!";
        //    byte[] buffer = Encoding.ASCII.GetBytes(data);
        //    int timeout = 1000; // Timeout 时间，单位：毫秒  
        //    System.Net.NetworkInformation.PingReply reply = p.Send(ip, timeout, buffer, options);
        //    if (reply.Status == System.Net.NetworkInformation.IPStatus.Success)
        //        return true;
        //    else
        //        return false;
        //}  
        #endregion
    }
}
