using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;

namespace SeatClientV3.ViewModel
{
    public class BindWeiCharWindow_ViewModel
    {
        public BindWeiCharWindow_ViewModel()
        {
            WindowWidth = 800;
            WindowHeight = 550;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            if (ClientObject.ObjCardReader == null)
            {
                TestMode = "Visible";
            }
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
        private UC_Tip_ViewModel _Tip_ViewModel = new UC_Tip_ViewModel();
        /// <summary>
        /// 提示消息
        /// </summary>
        public UC_Tip_ViewModel Tip_ViewModel
        {
            get { return _Tip_ViewModel; }
            set { _Tip_ViewModel = value; OnPropertyChanged("Tip_ViewModel"); }
        }
        private TipType _MessageType = TipType.None;
        /// <summary>
        /// 窗口消息类型
        /// </summary>
        public TipType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; OnPropertyChanged("MessageType"); }
        }
        private ImageSource _PopAd;
        /// <summary>
        /// 弹窗广告
        /// </summary>
        public ImageSource PopAd
        {
            get { return _PopAd; }
            set { _PopAd = value; OnPropertyChanged("PopAd"); }
        }
        private string _TitleAd;
        /// <summary>
        /// 标题广告
        /// </summary>
        public string TitleAd
        {
            get { return _TitleAd; }
            set { _TitleAd = value; OnPropertyChanged("TitleAd"); }
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
        private string _OKCaneclBtnVisibility = "Collapsed";
        /// <summary>
        /// 按钮隐藏
        /// </summary>
        public string OKCaneclBtnVisibility
        {
            get { return _OKCaneclBtnVisibility; }
            set { _OKCaneclBtnVisibility = value; OnPropertyChanged("OKCaneclBtnVisibility"); }
        }
        private string _CloseBtnVisibility = "Visible";
        /// <summary>
        /// 
        /// </summary>
        public string CloseBtnVisibility
        {
            get { return _CloseBtnVisibility; }
            set { _CloseBtnVisibility = value; OnPropertyChanged("CloseBtnVisibility"); }
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
    }
}
