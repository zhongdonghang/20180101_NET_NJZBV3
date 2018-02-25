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
using AdvertManageClient.Code;

namespace AdvertManageClient
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        ResourceDictionary rd;
        ViewModelLoginWindow vm = new ViewModelLoginWindow();
        public LoginWindow()
        {
            InitializeComponent();
            rd = new ResourceDictionary();
            rd.Source = new Uri("MyDictionary/ImageDictionary.xaml", UriKind.Relative);
            this.DataContext = vm; 
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm.Login())
            { 
                //((App)Application.Current).LoginUser = vm.User;
                MainWindow mw = new MainWindow();
                Application.Current.MainWindow = mw;
                this.Close();
                mw.Show();
            }
            
        }

        private void txt_password_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txt_password.Password))
            {
                txt_password.Background = (ImageBrush)rd["PwdMouseOver"];
            }
            else
            {
                txt_password.Background = (ImageBrush)rd["PwdGotFocus"];
            }
        }

        private void txt_password_GotFocus(object sender, RoutedEventArgs e)
        {
            txt_password.Background = (ImageBrush)rd["PwdGotFocus"];
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 
    }

    
}
