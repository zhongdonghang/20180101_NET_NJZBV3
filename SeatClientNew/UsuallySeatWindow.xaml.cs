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
    /// UsuallySeatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UsuallySeatWindow : Window
    {

        public UsuallySeatWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            SeatBinding();
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
        }
        public ViewModel.UsuallySeatWindow_ViewModel viewModel = new ViewModel.UsuallySeatWindow_ViewModel();
        /// <summary>
        /// 绑定座位控件
        /// </summary>
        private void SeatBinding()
        {
            for (int i = 0; i < UsuallySeat_Canvas.Children.Count; i++)
            {
                if (UsuallySeat_Canvas.Children[i] is MyUserControl.UC_UsuallySeat)
                {
                    UsuallySeat_Canvas.Children.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < viewModel.OftenUsedSeats.Count; i++)
            {
                if (i > 12)
                {
                    break;
                }
                MyUserControl.UC_UsuallySeat UC = new MyUserControl.UC_UsuallySeat(viewModel.OftenUsedSeats[i]);
                UC.Height = 60;
                UC.Width = 230;
                Canvas.SetLeft(UC, i % 3 * 250 + 40);
                Canvas.SetTop(UC, i / 3 * 90 + 100);
                UsuallySeat_Canvas.Children.Add(UC);
            }
        }
        /// <summary>
        /// 倒计时
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
        /// <summary>
        /// 座位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsuallySeat_Canvas_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is MyUserControl.UC_UsuallySeat)
            {
                MyUserControl.UC_UsuallySeat Seat = e.Source as MyUserControl.UC_UsuallySeat;
                if (viewModel.SelectSeat(Seat.viewModel))
                {
                    viewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
                    this.Close();
                }
                else
                {
                    viewModel.AddOften();
                    SeatBinding();
                }
            }
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
