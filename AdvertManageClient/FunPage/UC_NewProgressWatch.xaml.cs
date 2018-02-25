using System;
using System.Collections.Generic;
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
	/// UC_NewProgressWatch.xaml 的交互逻辑
	/// </summary>
	public partial class UC_NewProgressWatch : UserControl
	{
		public UC_NewProgressWatch()
		{
			this.InitializeComponent();
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
        /// 下发状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ProgessWatch_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_ProgessWatch.Visibility = System.Windows.Visibility.Visible;
        }
        /// <summary>
        /// 正在播放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SchoolMediaState_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_SchoolMediaState.Visibility = System.Windows.Visibility.Visible;
        }
	}
}