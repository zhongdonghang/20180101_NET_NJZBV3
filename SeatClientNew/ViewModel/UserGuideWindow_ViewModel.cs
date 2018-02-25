using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatClientV2.OperateResult;

namespace SeatClientV2.ViewModel
{
    public class UserGuideWindow_ViewModel : INotifyPropertyChanged
    {
        public UserGuideWindow_ViewModel()
        {
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            UserGuide = clientObject.UserGuide;
            if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
            {
                Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + UserGuide.ImageFilePath[0], UriKind.RelativeOrAbsolute));
            }
        }

        private string Apppath = AppDomain.CurrentDomain.BaseDirectory + "images\\UserGuide\\";
        public SeatClientV2.OperateResult.SystemObject clientObject
        {
            get { return SeatClientV2.OperateResult.SystemObject.GetInstance(); }
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

        private SeatManage.ClassModel.UserGuideInfo _UserGuide;
        /// <summary>
        /// 使用手册
        /// </summary>
        public SeatManage.ClassModel.UserGuideInfo UserGuide
        {
            get { return _UserGuide; }
            set { _UserGuide = value; OnPropertyChanged("UserGuide"); }
        }

        private System.Windows.Media.Imaging.BitmapImage _Image = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// Logo
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage Image
        {
            get { return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }

        #region INotifyPropertyChanged 成员
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        int index = 0;
        public bool MoveLeft()
        {
            if (index > 0)
            {
                index--;
                Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + UserGuide.ImageFilePath[index], UriKind.RelativeOrAbsolute));
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool MoveRight()
        {
            if (index < UserGuide.ImageFilePath.Count - 1)
            {
                index++;
                Image = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + UserGuide.ImageFilePath[index], UriKind.RelativeOrAbsolute));
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
