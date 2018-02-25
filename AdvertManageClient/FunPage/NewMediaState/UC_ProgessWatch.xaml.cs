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
using AMS.ViewModel;

namespace AdvertManageClient.FunPage.NewMediaState
{
    /// <summary>
    /// UC_ProgessWatch.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ProgessWatch : UserControl
    {
        public UC_ProgessWatch()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            CB_School.SelectedIndex = 0;
            CB_State.SelectedIndex = 0;
            CB_Type.SelectedIndex = 0;
        }
        AMS.ViewModel.ViewModelProgessWatch viewModel = new AMS.ViewModel.ViewModelProgessWatch();
        /// <summary>
        /// 状态选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_State_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_State.SelectedIndex > -1)
            {
                viewModel.SelectCommandState = (AMS.Model.Enum.CommandHandleResult)(CB_State.SelectedItem as IssuedHandleResultItem).Value;
            }
        }
        /// <summary>
        /// 学校选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_School_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_School.SelectedIndex > -1)
            {
                viewModel.SelectedSchoolID = (CB_School.SelectedItem as AMS.Model.AMS_School).Id;
            }
        }
        /// <summary>
        /// 类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Type.SelectedIndex > -1)
            {
                viewModel.SelectCommandType = (AMS.Model.Enum.IsureCommandType)(CB_Type.SelectedItem as IssuedInfoTypeItem).Value;
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_GetData_Click(object sender, RoutedEventArgs e)
        {
            viewModel.GetDataList();
        }
        /// <summary>
        /// 删除进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Issure.SelectedIndex > -1)
            {
                if (viewModel.DelectCommand((LB_Issure.SelectedItem as ViewModelIssureShowItem).IssureItemModel.ID))
                {
                    viewModel.GetDataList();
                }
                else
                {
                    viewModel.ErrorMessage = "删除失败";
                }
            }
            else
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
        }
    }
}
