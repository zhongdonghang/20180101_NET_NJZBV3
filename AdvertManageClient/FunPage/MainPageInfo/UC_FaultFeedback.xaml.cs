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

namespace AdvertManageClient.FunPage.MainPageInfo
{
    /// <summary>
    /// UC_FaultFeedback.xaml 的交互逻辑
    /// </summary>
    public partial class UC_FaultFeedback : UserControl
    {
        public UC_FaultFeedback()
        {
            InitializeComponent();
            this.DataContext = vm_CallBackErrorInfoListWindow;
        }
        public AMS.ViewModel.ViewModelCallBackErrorInfoListWindow vm_CallBackErrorInfoListWindow = new AMS.ViewModel.ViewModelCallBackErrorInfoListWindow();

        public void DataBinding()
        {
            vm_CallBackErrorInfoListWindow.GetDataList();
        }

        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel = FeedbackLbox.Items[FeedbackLbox.SelectedIndex] as AMS.Model.AMS_CallBackErrorInfo;
            fbw.vm_CallBackErrorInfoEditWindow.IsEdit = true;
            fbw.ShowDialog();
            DataBinding();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除这个信息?");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
                fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel = (FeedbackLbox.Items[FeedbackLbox.SelectedIndex] as AMS.ViewModel.ViewModelCallBackErrorInfoShow).CallBackErrorModel;
                fbw.vm_CallBackErrorInfoEditWindow.Delete();
                DataBinding();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel.FbTime = DateTime.Now.Date;
            fbw.ShowDialog();
            DataBinding();
        }

        private void solvemenu_Click(object sender, RoutedEventArgs e)
        {
            MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel = (FeedbackLbox.Items[FeedbackLbox.SelectedIndex] as AMS.ViewModel.ViewModelCallBackErrorInfoShow).CallBackErrorModel;
            fbw.vm_CallBackErrorInfoEditWindow.IsEdit = true;
            fbw.vm_CallBackErrorInfoEditWindow.GetStatus();
            fbw.ShowDialog();
            DataBinding();
        }

        private void finishmenu_Click(object sender, RoutedEventArgs e)
        {
            MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel = (FeedbackLbox.Items[FeedbackLbox.SelectedIndex] as AMS.ViewModel.ViewModelCallBackErrorInfoShow).CallBackErrorModel;
            fbw.vm_CallBackErrorInfoEditWindow.IsEdit = true;
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel.FbTime = DateTime.Now.Date;
            fbw.vm_CallBackErrorInfoEditWindow.GetStatus();
            fbw.ShowDialog();
            DataBinding();
        }

        private void watchmenu_Click(object sender, RoutedEventArgs e)
        {
            MainPageInfo.ErrorCallBackWindow fbw = new ErrorCallBackWindow();
            fbw.vm_CallBackErrorInfoEditWindow.CallBackErrorModel = (FeedbackLbox.Items[FeedbackLbox.SelectedIndex] as AMS.ViewModel.ViewModelCallBackErrorInfoShow).CallBackErrorModel;
            fbw.vm_CallBackErrorInfoEditWindow.IsEdit = false;
            fbw.vm_CallBackErrorInfoEditWindow.GetStatus();
            fbw.ShowDialog();
            DataBinding();
        }
    }
}