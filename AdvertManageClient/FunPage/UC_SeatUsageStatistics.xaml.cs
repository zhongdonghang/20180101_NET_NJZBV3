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

namespace AdvertManageClient.FunPage
{
    /// <summary>
    /// UC_SeatUsageStatistics.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SeatUsageStatistics : UserControl
    {
        public UC_SeatUsageStatistics()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 设置控件隐藏
        /// </summary>
        private void SetUCCollapsed()
        {
            foreach (UserControl uc in ucGrid.Children)
            {
                uc.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        private void btn_SchoolSeatUsage_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_SchoolSeatUsage.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
