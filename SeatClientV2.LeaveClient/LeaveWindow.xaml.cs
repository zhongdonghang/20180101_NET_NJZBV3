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

namespace ClientLeaveV2
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
            viewModel.CloseTime = 10;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
            if (viewModel.PopImg == null)
            {
                this.Height = 335;
                image_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down_co.Visibility = System.Windows.Visibility.Visible;
            }
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
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_continueTime_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ContinuedTime;
            this.Close();
        }

        /// <summary>
        /// 暂离
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_shortleave_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ShortLeave;
            this.Close();
           
        }
        /// <summary>
        /// 离开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_leave_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Leave;
            this.Close();
           
        }
    }
}
