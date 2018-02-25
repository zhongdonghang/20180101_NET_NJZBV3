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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// AddTitleAdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddTitleAdWindow : Window
    {
        public AddTitleAdWindow()
        {
            InitializeComponent();
            // vm_TitleAdEditWindow.User = ((App)Application.Current).LoginUser;
            vm_TitleAdEditWindow.CustomerList.GetDataList();
            vm_TitleAdEditWindow.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            ccb.SelectedIndex = 0;
            this.DataContext = vm_TitleAdEditWindow;
        }
        public AMS.ViewModel.ViewModelTitleAdEditWindow vm_TitleAdEditWindow = new AMS.ViewModel.ViewModelTitleAdEditWindow();

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (vm_TitleAdEditWindow.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "操作成功!");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_TitleAdEditWindow.IsEdit)
            {
                ccb.SelectedIndex = vm_TitleAdEditWindow.CustomerId;
            }
            else
            {
                vm_TitleAdEditWindow.CustomerId = (ccb.SelectedItem as AMS.Model.AMS_AdCustomer).ID;
            }

        }
    }
}
