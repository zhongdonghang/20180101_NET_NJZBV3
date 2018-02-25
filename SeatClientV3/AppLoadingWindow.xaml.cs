using SeatManage.EnumType;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using SeatClientV3.Code;
using SeatClientV3.WindowObject;

namespace SeatClientV3
{
    /// <summary>
    /// AppLoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppLoadingWindow : Window
    {
        public AppLoadingWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        public ViewModel.AppLoadingWindow_ViewModel viewModel = new ViewModel.AppLoadingWindow_ViewModel();
        /// <summary>
        /// 初始化成功
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_InitializeEnd(object sender, EventArgs e)
        {
            viewModel.InitializeState = HandleResult.Successed;
            viewModel.InitializeEnd -= viewModel_InitializeEnd;
            Dispatcher.Invoke(new Action(Hide));
        }
        /// <summary>
        /// 开始启动
        /// </summary>
        /// <param name="isReconnect"></param>
        public void CheckConfigConnection(bool isReconnect)
        {
            viewModel.IsReconnect = isReconnect;
            viewModel.InitializeEnd += new EventHandler(viewModel_InitializeEnd);
            viewModel.Run();
            ShowDialog();
        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                PasswordCloseWindow pcw = new PasswordCloseWindow();
                pcw.ShowDialog();
                if (pcw.viewModel.OperateResule == HandleResult.Successed)
                {
                    viewModel.InitializeState = HandleResult.Failed;
                    WPFMessage.MessageHelper.SendMessage(ConfigurationManager.AppSettings["SendMessageAPP"], SendClentMessageType.Close);
                    Close();
                }
            }
        }
        /// <summary>
        /// 窗口关闭操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                if (viewModel.State == ViewModel.AppLoadingWindow_ViewModel.HandState.Loading)
                {
                    viewModel.Dispose(true);
                }
            }
            catch (Exception ex)
            { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
