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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatClientV3.TipUC
{
    /// <summary>
    /// UC_Tip_SelectSeatWithoutAccess.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_SelectSeatWithoutAccess : UserControl
    {
        public UC_Tip_SelectSeatWithoutAccess(UCViewModel.Tip_ViewModel viewmodel)
        {
            viewModel = viewmodel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
        UCViewModel.Tip_ViewModel viewModel;
        /// <summary>
        /// 点击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CloseButton();
        }
        /// <summary>
        /// 点击确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OKButton();
        }
        /// <summary>
        /// 点击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CancelButton();
        }
    }
}
