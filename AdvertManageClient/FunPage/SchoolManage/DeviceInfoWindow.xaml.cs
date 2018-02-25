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
    /// DeviceInfoWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfoWindow : Window
    {
        public DeviceInfoWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        /// <summary>
        /// 设备的ViewModel
        /// </summary>
        public AMS.ViewModel.ViewModelDeviceEditWindow ViewModelDevice
        {
            get { return (AMS.ViewModel.ViewModelDeviceEditWindow)GetValue(ViewModelDeviceProperty); }
            set { SetValue(ViewModelDeviceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ViewModelDevice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ViewModelDeviceProperty =
            DependencyProperty.Register("ViewModelDevice", typeof(AMS.ViewModel.ViewModelDeviceEditWindow), typeof(DeviceInfoWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// 执行是否成功
        /// </summary>
        public bool IsSuccess
        {
            get { return (bool)GetValue(IsSuccessProperty); }
            set { SetValue(IsSuccessProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSuccess.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSuccessProperty =
            DependencyProperty.Register("IsSuccess", typeof(bool), typeof(DeviceInfoWindow), new UIPropertyMetadata(null));

        /// <summary>
        /// 删除设备信息
        /// </summary>
        /// <returns></returns>
        public void DeleteDevice()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "确定删除该设备信息吗？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                if (ViewModelDevice.ButtonSubmit())
                {
                    mbw = new MessageBoxWindow();
                    mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "删除成功");
                    mbw.ShowDialog();
                }
            }
            //ViewModelDevice.Cmd = AMS.ViewModel.Enum.HandleType.Delete;
            //MessageBoxResult r = MessageBox.Show("确定删除该设备信息吗？", "删除", MessageBoxButton.OKCancel);
            //if (r == MessageBoxResult.OK)
            //{
            //    IsSuccess =ViewModelDevice.ButtonSubmit();
            //    if (IsSuccess)
            //    {
            //        MessageBox.Show("删除成功。"); 
            //    }
            //}
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            IsSuccess = ViewModelDevice.ButtonSubmit();
            if (IsSuccess)
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
