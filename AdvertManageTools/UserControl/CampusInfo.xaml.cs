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
using System.Data;
using AdvertManageTools.EditPage;
using AdvertManageTools.Code;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// SchoolInfo.xaml 的交互逻辑
    /// </summary>
    public partial class CampusInfo : System.Windows.Controls.UserControl
    {
        public CampusInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            CampusdataGrid.ItemsSource = CampusMV.CampusList;
            cbschool.ItemsSource = CampusMV.ComBoxItems;
        }
        CampusInfoListViewModel CampusMV = new CampusInfoListViewModel();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {
            ComboxIntItem si = cbschool.SelectedItem as ComboxIntItem;
            CampusMV.GetData(si.Value);

        }
        public void ComboxDataBinding()
        {
            cbschool.SelectedIndex = 0;
            CampusMV.ComItemGetData();
        }
        /// <summary>
        /// 改变学校查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbschool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBinding();
        }
        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCampus_Click(object sender, RoutedEventArgs e)
        {
            CampusEditWindow cew = new CampusEditWindow();
            cew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 校区编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CampusEdit_Click(object sender, RoutedEventArgs e)
        {
            CampusInfoViewModel editcampus = this.CampusdataGrid.Items[CampusdataGrid.SelectedIndex] as CampusInfoViewModel;
            CampusEditWindow cew = new CampusEditWindow();
            cew.Campus = editcampus;
            cew.SelectSchoolNum = editcampus.Schoolnum;
            cew.IsEdit = true;
            cew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 校区删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CampusDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CampusdataGrid.SelectedIndex > -1)
            {
                MessageBoxResult endResult;
                endResult = MessageBox.Show("确认要删除此校区吗？", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (endResult == MessageBoxResult.Yes)
                {
                    CampusInfoViewModel deletecampus = this.CampusdataGrid.Items[CampusdataGrid.SelectedIndex] as CampusInfoViewModel;
                    if (deletecampus.DeleteCampus())
                    {
                        MessageBox.Show("删除成功！");
                        DataBinding();
                    }
                }
            }
        }
    }
}
