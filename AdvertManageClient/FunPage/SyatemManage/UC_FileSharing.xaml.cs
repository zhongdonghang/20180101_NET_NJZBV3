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
using System.Threading;

namespace AdvertManageClient.FunPage.SyatemManage
{
    /// <summary>
    /// UC_FileSharing.xaml 的交互逻辑
    /// </summary>
    public partial class UC_FileSharing : UserControl
    {
        public UC_FileSharing()
        {
            InitializeComponent();
            this.DataContext = vm_fs;
        }
        AMS.ViewModel.ViewModelFileSharingWindow vm_fs = new AMS.ViewModel.ViewModelFileSharingWindow();

        private void btnUpLoadFiles_Click(object sender, RoutedEventArgs e)
        {
            FileSharingUploadWindow sfUp = new FileSharingUploadWindow();
            sfUp.ShowDialog();
            vm_fs.GetFileSharingList();
        }
        ProgressBarWindow pbw;
        private void btnDownLoad_Click(object sender, RoutedEventArgs e)
        {
            if (lstFileSharingInfo.SelectedIndex < 0)
            {
                vm_fs.ErrorMessage = "请选择需要下载的文件！";
                return;
            }
            System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
            foldBrowerDialog.ShowDialog();
            if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
            {
                downloadpath = foldBrowerDialog.SelectedPath;
                selectindex = lstFileSharingInfo.SelectedIndex;
                pbw = new ProgressBarWindow(vm_fs.Vm_ProgressBar);
                pbw.vm_Progress.Refresh();
                pbw.Show();
                Thread myThread = new Thread(new ThreadStart(Down));
                myThread.Start();
            }

        }
        private string downloadpath = "";
        private int selectindex = -1;
        private void Down()
        {

            if (vm_fs.DownLoadFile(downloadpath, selectindex))
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
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "下载成功！");
            mbw.ShowDialog();
        }
        public void DataBinding()
        {
            vm_fs.GetFileSharingList();
        }
    }
}
