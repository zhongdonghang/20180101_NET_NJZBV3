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
    /// UC_MainForm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_MainForm : UserControl
    {
        public UC_MainForm()
        {
            InitializeComponent();
        }

        private void ErrorCallbackbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_FaultFeedback.Visibility = System.Windows.Visibility.Visible;
            UC_IssuedInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_FaultFeedback.DataBinding();
        }

        private void PasswordChangebtn_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MainPageInfo.PassChangeWindow pcw = new MainPageInfo.PassChangeWindow();
            pcw.vm_Password.UserInfo = AMS.ViewModel.ViewModelObject.User;
            pcw.ShowDialog();
        }

        private void Issuedprogressbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_FaultFeedback.Visibility = System.Windows.Visibility.Collapsed;
            UC_IssuedInfo.Visibility = System.Windows.Visibility.Visible;
            UC_IssuedInfo.DataBinding();
        }
    }
}
