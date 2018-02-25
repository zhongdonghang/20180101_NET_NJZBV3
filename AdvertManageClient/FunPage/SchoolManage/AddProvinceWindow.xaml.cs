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

namespace AdvertManageClient.FunPage.SchoolManage
{
    /// <summary>
    /// AddProvinceWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddProvinceWindow : Window
    {
        public AddProvinceWindow()
        {
            InitializeComponent();
            this.DataContext = vm_ProvinceEditWindow;
        }
        public AMS.ViewModel.ViewModelProvinceEditWindow vm_ProvinceEditWindow = new AMS.ViewModel.ViewModelProvinceEditWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {

            if (vm_ProvinceEditWindow.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
