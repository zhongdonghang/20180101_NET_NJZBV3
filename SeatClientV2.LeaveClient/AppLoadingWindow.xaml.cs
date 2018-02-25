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
using SeatManage.EnumType;
using System.Windows.Forms;

namespace ClientLeaveV2
{
    /// <summary>
    /// AppLoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppLoadingWindow : Window
    {
        public AppLoadingWindow()
        {
            InitializeComponent();

            viewModel.InitializeEnd += new EventHandler(viewModel_InitializeEnd);
            viewModel.Run();
            this.DataContext = viewModel;
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
            Dispatcher.Invoke(new Action(() =>
            {
                this.Close();
            }));

        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.InitializeState = HandleResult.Failed;
            this.Close();
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
    }
}
