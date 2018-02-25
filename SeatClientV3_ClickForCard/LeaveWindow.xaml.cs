using System;
using System.Windows;

namespace SeatClientV3
{
    /// <summary>
    /// LeaveWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LeaveWindow : Window
    {
        public LeaveWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            if (viewModel.PopImg == null)
            {
                this.Height = 335;
                image_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down_co.Visibility = System.Windows.Visibility.Visible;
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowMessage()
        {
            if (viewModel.ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
            {
                viewModel.ContinueBtnVisibility = "Visible";
            }
            viewModel.CloseTime = 10;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
            ShowDialog();
        }
        ViewModel.LeaveWindow_ViewModel viewModel = new ViewModel.LeaveWindow_ViewModel();
        /// <summary>
        /// 触发窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(WinClosing));
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinClosing()
        {
            this.Hide();
            if (viewModel.ClientObject.PopAdvert != null)
            {
                viewModel.ClientObject.PopAdvert.Usage.WatchCount++;
            }
            if (viewModel.ClientObject.TitleAdvert != null)
            {
                viewModel.ClientObject.TitleAdvert.Usage.WatchCount++;
            }
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            WinClosing();
        }
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_continueTime_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ContinuedTime;
            WinClosing();
        }
        /// <summary>
        /// 重新选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_reselect_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ReSelectSeat;
            WinClosing();

        }
        /// <summary>
        /// 暂离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_shortleave_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ShortLeave;
            WinClosing();

        }
        /// <summary>
        /// 离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_leave_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ClientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Leave;
            WinClosing();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!viewModel.ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used || !viewModel.ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
            {
                btn_continueTime.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
