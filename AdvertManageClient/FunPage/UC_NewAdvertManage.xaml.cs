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
    /// UC_NewAdvertManage.xaml 的交互逻辑
    /// </summary>
    public partial class UC_NewAdvertManage : UserControl
    {
        public UC_NewAdvertManage()
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
        /// 显示优惠券列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_CouponsList.Visibility = System.Windows.Visibility.Visible;
            uc_CouponsList.GetData();
        }
        /// <summary>
        /// 显示视频广告列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PlaylistList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_Playlist.Visibility = System.Windows.Visibility.Visible;
            uc_Playlist.GetData();
        }
        /// <summary>
        /// 冠名广告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_TitleAdvertList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_TitleAdvert.Visibility = System.Windows.Visibility.Visible;
            uc_TitleAdvert.GetData();
        }
        /// <summary>
        /// 弹窗广告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PopAdvertList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_PopAdvert.Visibility = System.Windows.Visibility.Visible;
            uc_PopAdvert.GetData();
        }
        /// <summary>
        /// 打印凭条
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintReceiptList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_PrintReceipt.Visibility = System.Windows.Visibility.Visible;
            uc_PrintReceipt.GetData();
        }
        /// <summary>
        /// 读者推广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReaderAdvertList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_ReaderAdvert.Visibility = System.Windows.Visibility.Visible;
            uc_ReaderAdvert.GetData();
        }
        /// <summary>
        /// 冠名广告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PromotionList_Click(object sender, RoutedEventArgs e)
        {
            SetUCCollapsed();
            uc_Promotion.Visibility = System.Windows.Visibility.Visible;
            uc_Promotion.GetData();
        }
    }
}