using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatManage.SeatManageComm;
using SeatClientV2.OperateResult;
using System.Threading;
using SeatManage.EnumType;
using System.Windows.Forms;

namespace SeatClientV2.ViewModel
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
            End = 1,
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
        Thread myThread = null;
        bool _isDispose = false;
        /// <summary>
        /// 初始化结果
        /// </summary>
        public HandState State
        {
            get { return _State; }
            set { _State = value; }
        }
        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
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
            myThread = new Thread(new ThreadStart(IntiApp));
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
            while (!CheckMacAddress())
            {
                Thread.Sleep(5000);
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            //while (!CheckAdImage())
            //{
            //    Thread.Sleep(5000);
            //    if (_isDispose)
            //    {
            //        myThread.Abort();
            //    }
            //}
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
                ShowMessage = "正在连接远程服务……";
                DateTime dt = SeatManage.Bll.ServiceDateTime.Now;
                return true;
            }
            catch (Exception ex)
            {
                WriteLog.Write("初始化网络失败：" + ex.Message);
                ShowMessage = "连接远程服务遇到错误，正在等待重试。您也可以选择退出，检查终端配置。";
                return false;
            }
        }

        /// <summary>
        /// 检查终端授权
        /// </summary>
        /// <returns></returns>
        private bool CheckEmpower()
        {
            ShowMessage = "检查授权…";
            SystemObject clientObject = SystemObject.GetInstance();
            if (clientObject.ClientSetting.EmpowerLoseEfficacyTime.CompareTo(SeatManage.Bll.ServiceDateTime.Now) <= 0)
            {
                ShowMessage = "验证授权失败，请检查服务器是否能够访问到Internet…";
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
                else if (!clientObject.IsTestModel)
                {
                    throw new Exception("读卡器初始化失败，请退出程序，检查读卡器接口" );
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
            ShowMessage = "初始化终端设置……";
            try
            {
                SystemObject clientObject = SystemObject.GetInstance();
                if (clientObject.ClientSetting == null)
                {
                    ShowMessage = "终端设置初始化失败, 可能是终端编号设置错误引起的，请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                    return false;
                }
                //else if (clientObject.SchoolLogoImagesResource==null)
                //{
                //    ShowMessage = "学校LOGO图片初始化失败, 请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                //    return false;
                //}
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                ShowMessage = "终端初始化失败, 请检查该终端的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                return false;
            }
        }
        /// <summary>
        /// 检查终端设置是否正确
        /// </summary>
        /// <returns></returns>
        //private bool CheckAdImage()
        //{
        //    ShowMessage = "加载资源图片……";
        //    try
        //    {
        //        SystemObject clientObject = SystemObject.GetInstance();
        //        clientObject();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        ShowMessage = "加载图片资源失败！" + ex.Message;
        //        return false;
        //    }
        //}
        /// <summary>
        /// 验证Mac地址，如果和本地不一致，则说明编号配置错误
        /// </summary>
        /// <returns></returns>
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
            else
            {
                if (localMacAdd.Count > 0)
                {
                    //TODO:更新终端设置
                    clientObject.ClientSetting.TerminalMacAddress = localMacAdd[0];
                }
                try
                {
                    if (!string.IsNullOrEmpty(SeatManage.Bll.TerminalOperatorService.UpdateTeminalSetting(clientObject.ClientSetting)))
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
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室设置失败：{0}", ex.Message));
                ShowMessage = "阅览室设置初始化失败，请检查该阅览室的设置或者查阅错误日志以排除故障。系统将继续尝试，直到获取到正确的配置。";
                return false;
            }
        }
        #endregion
    }
}
