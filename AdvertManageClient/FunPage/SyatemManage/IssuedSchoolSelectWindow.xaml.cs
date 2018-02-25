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

namespace AdvertManageClient.FunPage.SyatemManage
{
    /// <summary>
    /// IssuedSchoolSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class IssuedSchoolSelectWindow : Window
    {
        public IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType commandType, int commandID)
        {
            InitializeComponent();
            vm_IssuedSchoolSel.Command = commandType;
            vm_IssuedSchoolSel.CommandId = commandID;
            vm_IssuedSchoolSel.GetSchoolList();
            this.DataContext = vm_IssuedSchoolSel;
        }

        public AMS.ViewModel.ViewModelIssuedSchoolSelectWindow vm_IssuedSchoolSel = new AMS.ViewModel.ViewModelIssuedSchoolSelectWindow();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            NodeIssued node = sender as NodeIssued;
            if (node != null)
            {
                if (node.IsChecked)
                {
                    node.IsChecked = false;
                }
                else
                {
                    node.IsChecked = true;
                }
            }
        }

        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            if (vm_IssuedSchoolSel.Issued())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "下发成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
