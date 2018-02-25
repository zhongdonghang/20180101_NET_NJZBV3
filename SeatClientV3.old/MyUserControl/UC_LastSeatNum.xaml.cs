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
    /// UC_LastSeatNum.xaml 的交互逻辑
    /// </summary>
    public partial class UC_LastSeatNum : UserControl
    {

        public UC_LastSeatNum()
        {
            InitializeComponent();
        }
        public UC_LastSeatNum(UCViewModel.UC_LastSeatNum ViewModel)
        {
            InitializeComponent();
            viewModel = ViewModel;
            this.DataContext = viewModel;
        }
        public UCViewModel.UC_LastSeatNum viewModel;
    }
}
