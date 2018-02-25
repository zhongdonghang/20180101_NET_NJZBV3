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
    /// UC_DateTime.xaml 的交互逻辑
    /// </summary>
    public partial class UC_DateTime : UserControl
    {
        public UC_DateTime()
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }

        private UCViewModel.DateTime_ViewModel viewModel = new UCViewModel.DateTime_ViewModel();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
                viewModel.ShowTimeRun();
        }
    }
}
