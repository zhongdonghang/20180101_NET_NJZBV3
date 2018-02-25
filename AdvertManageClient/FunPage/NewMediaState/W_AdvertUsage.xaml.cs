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

namespace AdvertManageClient.FunPage.NewMediaState
{
    /// <summary>
    /// W_AdvertUsage.xaml 的交互逻辑
    /// </summary>
    public partial class W_AdvertUsage : Window
    {
        public W_AdvertUsage()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public AMS.ViewModel.ViewModel_AdvertUsage viewModel = new AMS.ViewModel.ViewModel_AdvertUsage();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LB_school_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_school.SelectedIndex > -1)
            {
                viewModel.SelectedUsage = LB_school.SelectedItem as AMS.ViewModel.ViewModel_AdvertUsageItem;
            }
        }
    }
}
