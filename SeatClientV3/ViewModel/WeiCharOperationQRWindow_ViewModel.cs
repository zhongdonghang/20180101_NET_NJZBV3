using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatClientV3.ViewModel
{
    public class WeiCharOperationQRWindow_ViewModel
    {
        public WeiCharOperationQRWindow_ViewModel()
        {
            WindowWidth = (double)ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y / 1080 * 520;
            WindowHeight = (double)ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y / 1080 * 300;
            //WindowLeft = (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X + ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y * 1080 / 1000) / 2 - WindowWidth;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y - WindowHeight;
        }

        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
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
        private int _CloseTime = 0;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
        }

        HandleResult operateResule = HandleResult.Failed;
        /// <summary>
        /// 操作结果，成功或者失败
        /// </summary>
        public HandleResult OperateResule
        {
            get { return operateResule; }
            set { operateResule = value; OnPropertyChanged("OperateResule"); }
        }
        FormCloseCountdown _CountDown = null;
        /// <summary>
        /// 窗体关闭倒计时
        /// </summary>
        public FormCloseCountdown CountDown
        {
            get { return _CountDown; }
            set { _CountDown = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #region 签到二维码
        public TimeLoop MyCheckCodeTime;

        public event EventHandlerCheckCodeChange CodeChange;
        public void CheckCodeRun()
        {
            MyCheckCodeTime = new TimeLoop(10 * 1000);
            MyCheckCodeTime.TimeTo += MyCheckCodeTime_TimeTo;
            MyCheckCodeTime.TimeStrat();
        }


        void MyCheckCodeTime_TimeTo(object sender, EventArgs e)
        {
            MyCheckCodeTime.TimeStop();
            if (CodeChange != null)
            {
                CodeChange();
            }
            MyCheckCodeTime.TimeStrat();
        }

        /// <summary>
        /// 窗体抬升
        /// </summary>
        public void ChangeTop(int size)
        {
            WindowTop = WindowObject.WeiCharOperationWindowObject.GetInstance().Window.Top - size;
        }
        /// <summary>
        /// 窗体下降
        /// </summary>
        public void ResetWin()
        {
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y - WindowHeight;
        }

        #endregion
    }
}
