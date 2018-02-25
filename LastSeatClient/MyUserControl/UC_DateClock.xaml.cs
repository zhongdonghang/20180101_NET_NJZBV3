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
    /// UC_DateClock.xaml 的交互逻辑
    /// </summary>
    public partial class UC_DateClock : UserControl
    {
        public UC_DateClock()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        ViewModel.VM_UC_DateClock viewModel = new ViewModel.VM_UC_DateClock();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.ShowTimeRun();
        }
    }
}
