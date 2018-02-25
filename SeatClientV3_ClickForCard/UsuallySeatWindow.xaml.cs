using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SeatClientV3
{
    /// <summary>
    /// UsuallySeatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UsuallySeatWindow : Window
    {

        public UsuallySeatWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            SeatBinding();

        }
        public ViewModel.UsuallySeatWindow_ViewModel ViewModel = new ViewModel.UsuallySeatWindow_ViewModel();
        /// <summary>
        /// 绑定座位控件
        /// </summary>
        private void SeatBinding()
        {
            for (int i = 0; i < 12; i++)
            {
                MyUserControl.UC_UsuallySeat UC = new MyUserControl.UC_UsuallySeat(ViewModel.OftenUsedSeats[i]);
                UC.Height = 60;
                UC.Width = 230;
                Canvas.SetLeft(UC, i % 3 * 250 + 40);
                Canvas.SetTop(UC, i / 3 * 90 + 100);
                UsuallySeat_Canvas.Children.Add(UC);
            }
        }
        /// <summary>
        /// 打开窗口
        /// </summary>
        public void ShowMessage()
        {
            ViewModel.AddOften();
            ViewModel.OperateResule = SeatManage.EnumType.HandleResult.Failed;
            ViewModel.CountDown = new OperateResult.FormCloseCountdown(ViewModel.CloseTime = 20);
            ViewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            ViewModel.CountDown.Start();
            ShowDialog();
        }
        /// <summary>
        /// 倒计时
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
        /// <summary>
        /// 座位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsuallySeat_Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.CountDown.Pause();
            if (!(e.Source is MyUserControl.UC_UsuallySeat))
            {
                return;
            }
            MyUserControl.UC_UsuallySeat Seat = e.Source as MyUserControl.UC_UsuallySeat;
            if (Seat.viewModel.UCVisible == "Collapsed")
            {
                return;
            }
            if (ViewModel.SelectSeat(Seat.viewModel))
            {
                ViewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
                WinClosing();
            }
            else
            {
                ViewModel.AddOften();
                SeatBinding();
            }
            ViewModel.CountDown.Start();
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinClosing()
        {
            Hide();
            ViewModel.CountDown.Stop();
            ViewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            WinClosing();
        }
    }
}
