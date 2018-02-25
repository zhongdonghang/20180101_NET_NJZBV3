using SeatClientV3.OperateResult;
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

namespace SeatClientV3.FunWindow
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
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.Clientobject.ClientSetting.DeviceSetting.WinCountDown.LeaveWindow);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            viewModel.CountDown.Start();
        }

        ViewModel.LeaveWindow_ViewModel viewModel = new ViewModel.LeaveWindow_ViewModel();

        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    SystemObject clientObject = viewModel.Clientobject;
                    clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
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
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = viewModel.Clientobject;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
            this.Close();
        }

        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = viewModel.Clientobject;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ContinuedTime;
            this.Close();
        }

        /// <summary>
        /// 释放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LeaveButton_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = viewModel.Clientobject;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Leave;
            this.Close();
        }

        /// <summary>
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShortLeaveButton_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = viewModel.Clientobject;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ShortLeave;
            this.Close();
        }

        /// <summary>
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReSelectButton_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = viewModel.Clientobject;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.ReSelectSeat;
            this.Close();
        }
    }
}
