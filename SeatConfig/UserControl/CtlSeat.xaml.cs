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

namespace SeatConfig.UserCtl
{
    /// <summary>
    /// CtlSeat.xaml 的交互逻辑
    /// </summary>
    public partial class CtlSeat : UserControl, INotifyPropertyChanged
    {
        public CtlSeat()
        {
            InitializeComponent();
            this.DataContext = _seat;
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
            Grid g = VisualTreeHelper.GetParent(this) as Grid;
            if (g != null)
            {
                g.Children.Remove(this);
            }
            e.Handled = true;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txtSeatNo.Visibility = System.Windows.Visibility.Visible;
            tbkSeatNo.Visibility = System.Windows.Visibility.Collapsed;
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
