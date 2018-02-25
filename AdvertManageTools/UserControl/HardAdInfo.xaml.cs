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
using AdvertManageTools.Code;
using AdvertManageTools.EditPage;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// HardAdInfo.xaml 的交互逻辑
    /// </summary>
    public partial class HardAdInfo : System.Windows.Controls.UserControl
    {
        public HardAdInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            hardaddataGrid.ItemsSource = Hardlist.HardAdList;
        }
        HardAdListViewModel Hardlist = new HardAdListViewModel();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {

                Hardlist.GetData();

        }
        /// <summary>
        /// 下发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            if (hardaddataGrid.SelectedIndex > -1)
            {
                HardAdViewModel vm = hardaddataGrid.Items[hardaddataGrid.SelectedIndex] as HardAdViewModel;
                IssueCommand ic = new IssueCommand();
                ic.Command = AdvertManage.Model.Enum.CommandType.HardAd;
                ic.CommandId = vm.ID;
                ic.ShowDialog();
            }
        }
        /// <summary>
        /// 新增硬广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddHardAd_Click(object sender, RoutedEventArgs e)
        {
            HardAdEditWindow haew = new HardAdEditWindow();
            haew.ShowDialog();
            DataBinding();
        }
    }
}
