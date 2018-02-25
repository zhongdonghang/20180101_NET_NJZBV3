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
using System.IO;
using AMS.ViewModel;
using System.Threading;

namespace AdvertManageClient.FunPage.SyatemManage
{
    /// <summary>
    /// FileSharingUploadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class FileSharingUploadWindow : Window
    {
        public FileSharingUploadWindow()
        {
            InitializeComponent();
            //vm_fsup.ProgressStatus += new ViewModelFileSharingUploadWindow.ProgressBarStatus(vm_fsup_ProgressStatus);
            this.DataContext = vm_fsup;
            vm_fsup.GetFileType();
        }

        //void vm_fsup_ProgressStatus(string progress)
        //{
        //    this.Dispatcher.BeginInvoke(new Action(() =>
        //    {
        //        pbw.vm_Progress.NowProgress = progress;
        //    }));
        //}
        //Thread myThread = null;
        public AMS.ViewModel.ViewModelFileSharingUploadWindow vm_fsup = new AMS.ViewModel.ViewModelFileSharingUploadWindow();

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }


        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = false;
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_fsup.AddFile(ofd.FileName);
            }
        }
        ProgressBarWindow pbw;
        private void btnUpLoad_Click(object sender, RoutedEventArgs e)
        {
            pbw = new ProgressBarWindow(vm_fsup.Vm_ProgressBar);
            pbw.vm_Progress.Refresh();
            pbw.Show();
            Thread myThread = new Thread(new ThreadStart(Save));
            myThread.Start();

        }
        private void Save()
        {
            bool isok = vm_fsup.FileUpLoad();
            if (isok)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(MessageBoxShow));
            }
            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(ProgressClose));
        }

        private void ProgressClose()
        {
            pbw.Close();
        }
        private delegate void DoTask();
        private void MessageBoxShow()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "保存成功！");
            mbw.ShowDialog();
            this.Close();
        }
        private void cbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm_fsup.FileType = (cbox.SelectedItem as FileTypeItem).Value;
        }
    }
}
