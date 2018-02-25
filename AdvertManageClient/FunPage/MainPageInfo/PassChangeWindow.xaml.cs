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

namespace AdvertManageClient.FunPage.MainPageInfo
{
    /// <summary>
    /// PassChangeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PassChangeWindow : Window
    {
        public PassChangeWindow()
        {
            InitializeComponent();
            this.DataContext = vm_Password;
        }
        public AMS.ViewModel.ViewModelPasswordChangeWindow vm_Password = new AMS.ViewModel.ViewModelPasswordChangeWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (vm_Password.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

    }
}
