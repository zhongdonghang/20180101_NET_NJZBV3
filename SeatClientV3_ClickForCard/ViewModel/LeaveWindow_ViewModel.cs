using SeatClientV3.OperateResult;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using SeatClientV3.Code;

namespace SeatClientV3.ViewModel
{
    public class LeaveWindow_ViewModel : INotifyPropertyChanged
    {
        public LeaveWindow_ViewModel()
        {
            WindowWidth = 590;
            WindowHeight = 470;
            if (ClientObject.PopAdvert != null)
            {
                WindowHeight = 335;
                PopImg = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\PopImage\\" + ClientObject.PopAdvert.PopImagePath, UriKind.RelativeOrAbsolute));
            }
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            TiteleAd = ClientObject.TitleAdvert != null ? ClientObject.TitleAdvert.TextContent : "Juneberry提醒您";
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


        FormCloseCountdown _CountDown = null;
        /// <summary>
        /// 窗体关闭倒计时
        /// </summary>
        public FormCloseCountdown CountDown
        {
            get { return _CountDown; }
            set { _CountDown = value; }
        }
        private string _ContinueBtnVisibility = "Collapsed";
        /// <summary>
        /// 按钮隐藏
        /// </summary>
        public string ContinueBtnVisibility
        {
            get { return _ContinueBtnVisibility; }
            set { _ContinueBtnVisibility = value; OnPropertyChanged("OKCaneclBtnVisibility"); }
        }


        private string _TiteleAd;
        /// <summary>
        /// 标题
        /// </summary>
        public string TiteleAd
        {
            get { return _TiteleAd; }
            set { _TiteleAd = value; OnPropertyChanged("Titele"); }
        }

        private System.Windows.Media.Imaging.BitmapImage _PopImg;
        /// <summary>
        /// 尾部广告
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PopImg
        {
            get { return _PopImg; }
            set { _PopImg = value; OnPropertyChanged("PopImg"); }
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
