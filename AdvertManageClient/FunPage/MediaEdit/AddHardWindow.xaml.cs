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
using System.Drawing;
using Microsoft.Win32;
using System.IO;

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// AddHardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AddHardWindow : Window
    {
        public AddHardWindow()
        {
            InitializeComponent();
            //vm_HardAdEditWindow.User = ((App)Application.Current).LoginUser;
            vm_HardAdEditWindow.CustomerList.GetDataList();
            vm_HardAdEditWindow.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            ccb.SelectedIndex = 0;
            this.DataContext = vm_HardAdEditWindow;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (vm_HardAdEditWindow.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }   
        }
        public AMS.ViewModel.ViewModelAdHardEditWindow vm_HardAdEditWindow = new AMS.ViewModel.ViewModelAdHardEditWindow();

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_HardAdEditWindow.AdImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
        }
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_HardAdEditWindow.IsEdit)
            {
                ccb.SelectedIndex = vm_HardAdEditWindow.CustomerId;
            }
            else
            {
                vm_HardAdEditWindow.CustomerId = (ccb.SelectedItem as AMS.Model.AMS_AdCustomer).ID;
            }

        }
    }
}
