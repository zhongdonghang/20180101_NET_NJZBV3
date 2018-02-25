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

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Seat.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Seat : UserControl
    {
        public UC_Seat(ViewModel.SeatUC_ViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            this.DataContext = ViewModel;
        }

        public ViewModel.SeatUC_ViewModel ViewModel;
        /// <summary>
        /// 点击控件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SeatElement_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ViewModel.SelectSeatOperation();
            e.Handled = true;
        }


    }
}
