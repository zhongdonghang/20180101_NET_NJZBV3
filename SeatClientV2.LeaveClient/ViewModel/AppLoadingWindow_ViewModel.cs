using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatManage.SeatManageComm;
using ClientLeaveV2.OperateResult;
using System.Threading;
using SeatManage.EnumType;
using System.Windows.Forms;

namespace ClientLeaveV2.ViewModel
{
    public class AppLoadingWindow_ViewModel : INotifyPropertyChanged
    {
        public AppLoadingWindow_ViewModel()
        {
            int i = Screen.PrimaryScreen.Bounds.Width;
            WindowHeight = Screen.PrimaryScreen.Bounds.Height;
            WindowWidth = Screen.PrimaryScreen.Bounds.Width;
            WindowLeft = 0;
            WindowTop = 0;
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
            if (_isDispose)
            {
                myThread.Abort();
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

        #endregion
    }
}
