using System;
using System.Windows;
using System.Windows.Controls;

namespace SeatClientV3
{
    /// <summary>
    /// KeyboardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KeyboardWindow : Window
    {
        public KeyboardWindow()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
            ViewModel.CloseTime = 30;
            ViewModel.CountDown = new OperateResult.FormCloseCountdown(ViewModel.CloseTime);
            ViewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            ViewModel.CountDown.Start();
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowMessage()
        {
            ViewModel.SeatNo = "";
            ViewModel.CloseTime = 30;
            ViewModel.CountDown = new OperateResult.FormCloseCountdown(ViewModel.CloseTime);
            ViewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            ViewModel.CountDown.Start();
            ShowDialog();
        }
        public ViewModel.KeyboardWindow_ViewModel ViewModel = new ViewModel.KeyboardWindow_ViewModel();
        /// <summary>
        /// 退格按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_backspace_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SeatNo.Length >0)
            {
                ViewModel.SeatNo = ViewModel.SeatNo.Substring(0, ViewModel.SeatNo.Length - 1);
            }
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.CountDown.Pause();
            if (ViewModel.CheckSeatNumber())
            {
                WinClosing();
            }
            else
            {
                ViewModel.CountDown.Start();
                ViewModel.SeatNo = "";
            }
        }
        /// <summary>
        /// 输入按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button != null) ViewModel.SeatNo += button.Content;
        }

        private void WinClosing()
        {
            this.Hide();
            if (ViewModel.ClientObject.TitleAdvert != null)
            {
                ViewModel.ClientObject.TitleAdvert.Usage.WatchCount++;
            }
            ViewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            ViewModel.CountDown.Stop();
        }
        /// <summary>
        /// 倒计时窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (ViewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(WinClosing));
            }
            else
            {
                ViewModel.CloseTime = ViewModel.CountDown.CountdownSceonds;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SeatNo = "";
            WinClosing();
        }
    }
}
