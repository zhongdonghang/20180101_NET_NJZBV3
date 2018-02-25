using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;

namespace SeatClientV3.ViewModel
{
    public class ActivationWindow_ViewModel : INotifyPropertyChanged
    {
        public ActivationWindow_ViewModel()
        {
            clientobject = SystemObject.GetInstance();
            WindowWidth = (double)clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 620;
            WindowHeight = (double)clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 325;
            WindowLeft = (clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = (clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            if (clientobject.ObjCardReader == null)
            {
                TestMode = "Visible";
            }
        }
        #region 属性 成员
        SystemObject clientobject;
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject Clientobject
        {
            get { return clientobject; }
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
        //private UC_Tip_ViewModel _Tip_ViewModel = new UC_Tip_ViewModel();
        ///// <summary>
        ///// 提示消息
        ///// </summary>
        //public UC_Tip_ViewModel Tip_ViewModel
        //{
        //    get { return _Tip_ViewModel; }
        //    set { _Tip_ViewModel = value; OnPropertyChanged("Tip_ViewModel"); }
        //}
        //private SeatManage.EnumType.TipType _MessageType = SeatManage.EnumType.TipType.None;
        /// <summary>
        /// 窗口消息类型
        /// </summary>
        //public SeatManage.EnumType.TipType MessageType
        //{
        //    get { return _MessageType; }
        //    set { _MessageType = value; OnPropertyChanged("MessageType"); }
        //}


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
        private string _CardNo = "";
        /// <summary>
        /// 读卡的卡号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; OnPropertyChanged("CardNo"); }
        }
        private string _TestMode = "Collapsed";
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; OnPropertyChanged("TestMode"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
