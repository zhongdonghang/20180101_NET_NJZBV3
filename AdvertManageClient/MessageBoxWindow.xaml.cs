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
using System.Windows.Shapes;
using AMS.ViewModel;

namespace AdvertManageClient
{
    /// <summary>
    /// MessageBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxWindow : Window
    {
        public MessageBoxWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        public ViewModelMessageBoxWindow vm_MessageBoxWindow
        {
            get { return (ViewModelMessageBoxWindow)GetValue(MessageBoxWindowProperty); }
            set { SetValue(MessageBoxWindowProperty, value); }
        }
        public static readonly DependencyProperty MessageBoxWindowProperty =
            DependencyProperty.Register("vm_MessageBoxWindow", typeof(ViewModelMessageBoxWindow), typeof(ViewModelMessageBoxWindow));

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm_MessageBoxWindow.Result = true;
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            vm_MessageBoxWindow.Result = false;
            this.Close();
        }
    }
}
