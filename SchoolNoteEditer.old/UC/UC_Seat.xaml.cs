using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// CtlSeat.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Seat : UserControl, INotifyPropertyChanged
    {
        public UC_Seat()
        {
            InitializeComponent();
            this.DataContext = _seat;

            bgimg.RenderTransformOrigin = new Point(0.5, 0.5);
            RotateTransform rotateTransform = new RotateTransform(0);
            bgimg.RenderTransform = rotateTransform;
        }
        public SeatManage.ClassModel.Seat _seat = new SeatManage.ClassModel.Seat();
        /// <summary>
        /// 座位
        /// </summary>
        public SeatManage.ClassModel.Seat Seat
        {
            get { return _seat; }
            set
            {
                _seat = value;
                if (PropertyChanged != null)
                {
                    this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs("Seat"));
                }
            }
        }
        public bool IsPower
        {
            get { return _seat.HavePower; }
            set
            {
                _seat.HavePower = value;
                if (_seat.HavePower)
                {
                    powerimg.Source = new BitmapImage(new Uri("/Resources/power.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    powerimg.Source = null;
                }
            }
        }
        public bool IsSuspended
        {
            get { return _seat.IsSuspended; }
            set
            {
                _seat.IsSuspended = value;
                if (_seat.IsSuspended)
                {
                    Suspendedimg.Source = new BitmapImage(new Uri("/Resources/Suspended.png", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    Suspendedimg.Source = null;
                }
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox s = sender as TextBox;
            if (s != null)
            {
                s.Visibility = System.Windows.Visibility.Collapsed;
                tbkSeatNo.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Canvas c = VisualTreeHelper.GetParent(this) as Canvas;
            if (c != null)
            {
                c.Children.Remove(this);
            }
            e.Handled = true;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                txtSeatNo.Visibility = System.Windows.Visibility.Visible;
                tbkSeatNo.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void rate_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //旋转控件
            RotateTransform rotateTransform = (RotateTransform)bgimg.RenderTransform;
            rotateTransform.Angle += 10;
            if (rotateTransform.Angle > 360)
            {
                rotateTransform.Angle -= 360;
            }
            bgimg.RenderTransform = rotateTransform;
            e.Handled = true;
        }

        private void ratef_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //旋转控件
            RotateTransform rotateTransform = (RotateTransform)bgimg.RenderTransform;
            rotateTransform.Angle -= 10;
            if (rotateTransform.Angle < 0)
            {
                rotateTransform.Angle += 360;
            }
            bgimg.RenderTransform = rotateTransform;
            e.Handled = true;
        }

    }
}
