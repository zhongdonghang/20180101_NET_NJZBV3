using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SeatClientV3.ViewModel
{
    public class WaitingWindow_ViewModel : INotifyPropertyChanged
    {
        public WaitingWindow_ViewModel()
        {
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            //GetRoomUsage();
        }
        public SeatClientV3.OperateResult.SystemObject clientObject
        {
            get { return SeatClientV3.OperateResult.SystemObject.GetInstance(); }
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
    }
}
