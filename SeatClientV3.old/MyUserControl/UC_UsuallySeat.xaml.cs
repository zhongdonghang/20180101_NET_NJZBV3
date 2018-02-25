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
    /// UC_UsuallySeat.xaml 的交互逻辑
    /// </summary>
    public partial class UC_UsuallySeat : UserControl
    {
        public UC_UsuallySeat(UCViewModel.UsuallySeatUC_ViewModel Model)
        {
            InitializeComponent();
            this.DataContext = Model;
            viewModel = Model;
        }
        public UCViewModel.UsuallySeatUC_ViewModel viewModel;
    }
}
