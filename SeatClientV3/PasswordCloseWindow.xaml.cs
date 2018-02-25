using System;
using System.Windows;

namespace SeatClientV3
{
    /// <summary>
    /// PasswordCloseWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PasswordCloseWindow : Window
    {
        public PasswordCloseWindow()
        {
            InitializeComponent();
            viewModel = new ViewModel.PasswordWindow_ViewModel();
            this.DataContext = viewModel;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(20);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
        }
        public ViewModel.PasswordWindow_ViewModel viewModel;
        /// <summary>
        /// 触发窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OperateResule = SeatManage.EnumType.HandleResult.Failed;
            this.Close();
        }

        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CheckPassword(txt_Password.Password);
            this.Close();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OperateResule = SeatManage.EnumType.HandleResult.Failed;
            this.Close();
        }
    }
}
