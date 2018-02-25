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

namespace AMS.MediaPlayer
{
    /// <summary>
    /// CouponsShowWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CouponsShowWindow : Window
    {
        public CouponsShowWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public ViewModel.ViewModel_CouponsShow viewModel = new ViewModel.ViewModel_CouponsShow();
        /// <summary>
        /// 右翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_right_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            viewModel.MoveRight();

        }
        /// <summary>
        /// 左翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_left_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            viewModel.MoveLeft();
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_print_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer1.Start();
            viewModel.Print();
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.UpdateUsage();
        }
        System.Timers.Timer timer1;
        public void DataBinding()
        {
            viewModel.NewUsage();
            viewModel.bindingItem();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                timer1 = new System.Timers.Timer(30000);
                timer1.Elapsed += (s, ea) => timer1_Elapsed(s, ea);
                timer1.Start();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        void timer1_Elapsed(object s, System.Timers.ElapsedEventArgs ea)
        {
            //timer1.Dispose();//每次都要释放之前新建的time对象
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    this.Close();
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
            }));

        }
    }
}
