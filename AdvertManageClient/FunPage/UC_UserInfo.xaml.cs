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

namespace AdvertManageClient.FunPage
{
    /// <summary>
    /// UC_UserInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_UserInfo : UserControl
    {
        public UC_UserInfo()
        {
            InitializeComponent();
            this.DataContext = vm_UserList;
        }
        public AMS.ViewModel.ViewModelUserInfoListUC vm_UserList = new AMS.ViewModel.ViewModelUserInfoListUC();
        private void AddUserbtn_Click(object sender, RoutedEventArgs e)
        {
            FunPage.UserManage.AddUserWindow auw = new UserManage.AddUserWindow();
            auw.vm_User.IsEdit = false;
            auw.ShowDialog();
            vm_UserList.GetDataList();
        }

        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            FunPage.UserManage.AddUserWindow auw = new UserManage.AddUserWindow();
            auw.vm_User.UserModel = LBList.Items[LBList.SelectedIndex] as AMS.Model.AMS_UserInfo;
            auw.vm_User.IsEdit = true;
            auw.ShowDialog();
            vm_UserList.GetDataList();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除这个用户？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelUserInfoEditWindow vmuiew = new AMS.ViewModel.ViewModelUserInfoEditWindow();
                vmuiew.UserModel = LBList.Items[LBList.SelectedIndex] as AMS.Model.AMS_UserInfo;
                vmuiew.Delete();
                vm_UserList.GetDataList();
            }
        }

        private void passwordmenu_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MainPageInfo.PassChangeWindow pcw = new MainPageInfo.PassChangeWindow();
            pcw.vm_Password.UserInfo = LBList.Items[LBList.SelectedIndex] as AMS.Model.AMS_UserInfo;
            pcw.vm_Password.IsAdmin = true;
            pcw.ShowDialog();
        }
    }
}
