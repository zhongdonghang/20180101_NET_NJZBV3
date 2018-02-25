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

namespace AdvertManageClient
{
    /// <summary>
    /// NewMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NewMainWindow : Window
    {
        public NewMainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 窗口拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_WindowClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
        /// <summary>
        /// 广告状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_AdvertState_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_AdvertStatus.Visibility = System.Windows.Visibility.Visible;
            
        }
        /// <summary>
        /// 广告发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MediaManage_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_AdvertManage.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// 统计查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_StatisticalQueries_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_SeatUsageStatistics.Visibility = System.Windows.Visibility.Visible;
        }
    }
}
