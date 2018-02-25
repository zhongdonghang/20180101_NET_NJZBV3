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
    /// FaultFeedbackWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ErrorCallBackWindow : Window
    {
        public ErrorCallBackWindow()
        {
            InitializeComponent();
          //  vm_CallBackErrorInfoEditWindow.User = ((App)Application.Current).LoginUser;
            vm_CallBackErrorInfoEditWindow.GetSchool();
            vm_CallBackErrorInfoEditWindow.GetStatus();
            this.DataContext = vm_CallBackErrorInfoEditWindow;
            cbs.SelectedIndex = 0;
            cbt.SelectedIndex = 0;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public AMS.ViewModel.ViewModelCallBackErrorInfoEditWindow vm_CallBackErrorInfoEditWindow = new AMS.ViewModel.ViewModelCallBackErrorInfoEditWindow();
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (vm_CallBackErrorInfoEditWindow.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "操作成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void cbt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_CallBackErrorInfoEditWindow.CallBackErrorModel.Solvestatic < 0)
            {
                vm_CallBackErrorInfoEditWindow.CallBackErrorModel.ProblemType = (cbt.SelectedItem as AMS.ViewModel.ViewModelCallBackType).Type;
            }
        }

        private void cbs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_CallBackErrorInfoEditWindow.CallBackErrorModel.Solvestatic < 0)
            {
                vm_CallBackErrorInfoEditWindow.CallBackErrorModel.SchoolId = (cbs.SelectedItem as AMS.Model.AMS_School).Id;
            }
        }
    }
}
