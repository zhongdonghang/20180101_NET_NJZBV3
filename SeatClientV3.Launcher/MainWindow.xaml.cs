using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatClientV3.Launcher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.StratUpFinish += viewModel_StratUpFinish;
            MessageHelper = new WPFMessage.MessageHelper();
            MessageHelper.GetMessage += MessageHelper_GetMessage;
        }

        void MessageHelper_GetMessage(object sender, string message)
        {
            string[] msg = message.Split(';');
            int type = 0;
            if (!int.TryParse(msg[0], out type))
            {
                return;
            }
            if ((SeatManage.EnumType.SendClentMessageType)type == SeatManage.EnumType.SendClentMessageType.Close)
            {
                WPFMessage.MessageHelper.SendMessage(viewModel.SeatClient, SeatManage.EnumType.SendClentMessageType.Close);
                WPFMessage.MessageHelper.SendMessage(viewModel.MediaCLient, SeatManage.EnumType.SendClentMessageType.Close);
                SeatManage.SeatManageComm.TaskBarHider.HideTask(false);
                Application.Current.Shutdown();
            }
            if ((SeatManage.EnumType.SendClentMessageType)type == SeatManage.EnumType.SendClentMessageType.ReStartUpSeatClient)
            {
                Process[] ps = Process.GetProcesses();
                foreach (Process item in ps.Where(item => item.ProcessName == "SeatClient"))
                {
                    item.Kill();
                }
                viewModel.StartProcess();
            }
        }
        protected WPFMessage.MessageHelper MessageHelper;
        void viewModel_StratUpFinish(string message)
        {
            Dispatcher.Invoke(new Action(Hide));
        }
        /// <summary>
        /// viewmodel
        /// </summary>
        private ViewModel.MainWindow_ViewModel viewModel = new ViewModel.MainWindow_ViewModel();
        /// <summary>
        /// 窗口启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            (PresentationSource.FromVisual(this) as HwndSource).AddHook(MessageHelper.WndProc);
            SeatManage.SeatManageComm.TaskBarHider.HideTask(true);
            viewModel.Run();
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SeatManage.SeatManageComm.TaskBarHider.HideTask(false);
        }
    }
}
