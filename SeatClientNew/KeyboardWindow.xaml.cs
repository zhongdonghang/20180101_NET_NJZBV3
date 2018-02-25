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

namespace SeatClientV2
{
    /// <summary>
    /// KeyboardWindow.xaml 的交互逻辑
    /// </summary>
    public partial class KeyboardWindow : Window
    {
        public KeyboardWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.CloseTime = 30;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
        }
        public ViewModel.KeyboardWindow_ViewModel viewModel = new ViewModel.KeyboardWindow_ViewModel();
        private void btn_backspace_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.SeatNo.Length >0)
            {
                viewModel.SeatNo = viewModel.SeatNo.Substring(0, viewModel.SeatNo.Length - 1);
            }
        }

        private void btn_submit_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.CheckSeatNumber())
            {
                this.Close();
            }
            else
            {
                viewModel.SeatNo = "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            viewModel.SeatNo += button.Content;
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
        }
        /// <summary>
        /// 倒计时窗口关闭
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
            else
            {
                viewModel.CloseTime = viewModel.CountDown.CountdownSceonds;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
