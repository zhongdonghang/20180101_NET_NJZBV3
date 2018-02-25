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

namespace AdvertManageClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void btnhome_Click(object sender, RoutedEventArgs e)
        {
            uc_SchoolManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_MainForm.Visibility = System.Windows.Visibility.Visible;
            uc_AdVideoManage.Visibility = System.Windows.Visibility.Collapsed;
            uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_SysManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_UserManageForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Ask, "是否要退出程序？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                Application.Current.Shutdown();
            }
        }

        private void StackPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }


        private void btnSchool_Click(object sender, RoutedEventArgs e)
        {
            uc_SchoolManageForm.Visibility = System.Windows.Visibility.Visible;
            uc_SchoolManageForm.DataBind();
            uc_MainForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_AdVideoManage.Visibility = System.Windows.Visibility.Collapsed;
            uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_SysManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_UserManageForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnmedia_Click(object sender, RoutedEventArgs e)
        {
            uc_SchoolManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_MainForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_AdVideoManage.Visibility = System.Windows.Visibility.Visible;
            uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_SysManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_UserManageForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnStic_Click(object sender, RoutedEventArgs e)
        {
            uc_SchoolManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_MainForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_AdVideoManage.Visibility = System.Windows.Visibility.Collapsed;
            uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Visible;
            uc_SysManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_UserManageForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            if (AMS.ViewModel.ViewModelObject.User.LoginId.Equals("juneberry"))
            {
                uc_SchoolManageForm.Visibility = System.Windows.Visibility.Collapsed;
                uc_MainForm.Visibility = System.Windows.Visibility.Collapsed;
                uc_AdVideoManage.Visibility = System.Windows.Visibility.Collapsed;
                uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Collapsed;
                uc_SysManageForm.Visibility = System.Windows.Visibility.Collapsed;
                uc_UserManageForm.Visibility = System.Windows.Visibility.Visible;
                uc_UserManageForm.vm_UserList.GetDataList();
            }
            else
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Informatsion,
                    "此功能只能管理员操作，请用管理员身份登录！");
                mbw.ShowDialog();
            }
        }

        private void btnSystem_Click(object sender, RoutedEventArgs e)
        {
            uc_SchoolManageForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_MainForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_AdVideoManage.Visibility = System.Windows.Visibility.Collapsed;
            uc_dataStatisticsForm.Visibility = System.Windows.Visibility.Collapsed;
            uc_SysManageForm.Visibility = System.Windows.Visibility.Visible;
            uc_UserManageForm.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NewMainWindow nmw = new NewMainWindow();
            nmw.ShowDialog();
        }


    }
}
