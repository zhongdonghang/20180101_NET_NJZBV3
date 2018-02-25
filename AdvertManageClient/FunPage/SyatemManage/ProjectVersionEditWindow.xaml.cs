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
using System.Threading;

namespace AdvertManageClient.FunPage.SyatemManage
{
    /// <summary>
    /// ProjectVersionEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProjectVersionEditWindow : Window
    {
        public ProjectVersionEditWindow()
        {
            InitializeComponent();
            vm_pvEdit.ProgramUpgradeViewModel();
            this.DataContext = vm_pvEdit;

        }

        public AMS.ViewModel.ViewModelProjectVersionEditWindow vm_pvEdit = new AMS.ViewModel.ViewModelProjectVersionEditWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            if (vm_pvEdit.ProjectFileRelease())
            {
                fileUpload();
                //myThread = new Thread(new ThreadStart(fileUpload));
                //myThread.Start();
            }
        }

        Thread myThread = null;
        /// <summary>
        /// 文件上传方法
        /// </summary>
        public void fileUpload()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "文件上传成功！");
            mbw.ShowDialog();
            this.Close();
        }

        private void btnBrowser_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (!string.IsNullOrEmpty(fbd.SelectedPath))
            {
                vm_pvEdit.FilePath = fbd.SelectedPath;
                vm_pvEdit.BuildUpdateConfigFile(vm_pvEdit.FilePath);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ProgramUpgradeItem Item = cbBoxProjectType.SelectedItem as ProgramUpgradeItem;
            vm_pvEdit.ComboxSelectedItemHandle(Item);
        }
    }
}
