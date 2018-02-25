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
    /// TitleAdInfo.xaml 的交互逻辑
    /// </summary>
    public partial class TitleAdInfo : System.Windows.Controls.UserControl
    {
        public TitleAdInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            titleAddataGrid.ItemsSource = TitleAdList.TitleAdList;
        }
        TitleAdListViewModel TitleAdList = new TitleAdListViewModel();
        /// <summary>
        /// 数据获取
        /// </summary>
        public void DataBinding()
        {
            TitleAdList.GetData();
        }
        /// <summary>
        /// 下发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            if (titleAddataGrid.SelectedIndex > -1)
            {
                TitleAdViewModel TitleVM = titleAddataGrid.Items[titleAddataGrid.SelectedIndex] as TitleAdViewModel;
                IssueCommand ic = new IssueCommand();
                ic.Command = AdvertManage.Model.Enum.CommandType.TitleAd;
                ic.CommandId = TitleVM.Id;
                ic.ShowDialog();
            }
        }
        /// <summary>
        /// 新增广告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnadd_Click(object sender, RoutedEventArgs e)
        {
            TitleAdEditWindow taew = new TitleAdEditWindow();
            taew.ShowDialog();
            DataBinding();
        }
    }
}
