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
    public partial class SchoolInfo : System.Windows.Controls.UserControl
    {
        public SchoolInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            SchooldataGrid.ItemsSource = schoolvm.SchoolList;
        }
        SchoolInfoListViewModel schoolvm = new SchoolInfoListViewModel();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {

            schoolvm.DataGet();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReSelect_Click(object sender, RoutedEventArgs e)
        {
            DataBinding();
        }
        /// <summary>
        /// 添加学校
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddSchool_Click(object sender, RoutedEventArgs e)
        {
            SchoolEditWindow addschool = new SchoolEditWindow();
            addschool.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 删除学校
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            if (SchooldataGrid.SelectedIndex > -1)
            {
                MessageBoxResult endResult;
                endResult = MessageBox.Show("确认要删除此学校吗？", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (endResult == MessageBoxResult.Yes)
                {
                    SchoolInfoViewModel deleteschool = this.SchooldataGrid.Items[SchooldataGrid.SelectedIndex] as SchoolInfoViewModel;
                    if (deleteschool.DeleteSchool())
                    {
                        MessageBox.Show("删除成功！");
                        DataBinding();
                    }

                }
            }
        }
        /// <summary>
        /// 修改学校
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_Edit(object sender, RoutedEventArgs e)
        {
            if (SchooldataGrid.SelectedIndex > -1)
            {
                SchoolInfoViewModel editschool = this.SchooldataGrid.Items[SchooldataGrid.SelectedIndex] as SchoolInfoViewModel;
                SchoolEditWindow sew = new SchoolEditWindow();
                sew.School = editschool;
                sew.IsEdit = true;
                sew.ShowDialog();
                DataBinding();
            }
        }
    }
}
