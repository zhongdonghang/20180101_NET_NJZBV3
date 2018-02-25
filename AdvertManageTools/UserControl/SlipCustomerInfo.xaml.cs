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
using AdvertManageTools.EditPage;
using AdvertManageTools.Code;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// SlipCustomerInfo.xaml 的交互逻辑
    /// </summary>
    public partial class SlipCustomerInfo : System.Windows.Controls.UserControl
    {
        public SlipCustomerInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            slipdataGrid.ItemsSource = SlipList.SlipCustomerList;
        }
        SlipCustomerInfoListViewModel SlipList = new SlipCustomerInfoListViewModel();
        /// <summary>
        /// 添加新的优惠券
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            SlipCustomerEditWindow scew = new SlipCustomerEditWindow();
            scew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {
                SlipList.GetData();
           
        }
        /// <summary>
        /// 下发优惠券
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            List<int> idlist = SlipList.SelectIdList();
            if (idlist.Count > 0)
            {
                IssueSlip Is = new IssueSlip();
                Is.IssusClip.ReleaseSlipid = idlist;
                Is.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先选择一个优惠券！");
            }
        }
        /// <summary>
        /// 导出离线版本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            List<int> idlist = SlipList.SelectIdList();
            if (idlist.Count > 0)
            {
                IssueSlip Is = new IssueSlip();
                Is.IssusClip.ReleaseSlipid = idlist;
                Is.IsRelease = false;
                Is.ShowDialog();
            }
            else
            {
                MessageBox.Show("请先选择一个优惠券！");
            }
        }
        /// <summary>
        /// 选中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (slipdataGrid.SelectedIndex > -1)
            {
                SlipCustomerInfoViewModel select = this.slipdataGrid.Items[slipdataGrid.SelectedIndex] as SlipCustomerInfoViewModel;
                CheckBox cb = sender as CheckBox;
                if (cb.IsChecked.Value)
                {
                    select.IsSelect = true;
                }
                else
                {
                    select.IsSelect = false;
                }
            }
        }
    }
}
