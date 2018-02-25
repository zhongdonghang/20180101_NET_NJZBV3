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

namespace LastSeatClient.MyUserControl
{
    /// <summary>
    /// UC_RoomStatus.xaml 的交互逻辑
    /// </summary>
    public partial class UC_LibStatus : UserControl
    {
        public UC_LibStatus(ViewModel.VM_UC_LibStatus viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        //public ViewModel.VM_UC_LibStatus viewModel = new ViewModel.VM_UC_LibStatus();
    }
}
