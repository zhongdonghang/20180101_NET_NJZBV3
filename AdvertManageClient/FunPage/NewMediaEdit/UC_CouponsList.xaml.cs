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

namespace AdvertManageClient.FunPage.NewMediaEdit
{
    /// <summary>
    /// UC_CouponsList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_CouponsList : UserControl
    {
        public UC_CouponsList()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        ViewModel_CouponsList viewModel = new ViewModel_CouponsList();
        /// <summary>
        /// 新建优惠券
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_CouponsEdit w_CouponEdit = new W_CouponsEdit();
            w_CouponEdit.ShowDialog();
            GetData();
        }
        //获取数据
        public void GetData()
        {
            viewModel.GetDataList();
        }

        private void btn_Release_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Coupons.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.Advertisement;
            w_IisureList.viewModel.CommandID = (LB_Coupons.SelectedItem as AMS.Model.CouponsInfo).ID;
            w_IisureList.ShowDialog();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Coupons.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            W_CouponsEdit w_CouponEdit = new W_CouponsEdit();
            w_CouponEdit.viewModel.IsEdit = true;
            w_CouponEdit.viewModel.CouponsModel = LB_Coupons.SelectedItem as AMS.Model.CouponsInfo;
            w_CouponEdit.viewModel.GetUpdateInfo();
            w_CouponEdit.ShowDialog();
            GetData();
        }

        private void Btn_count_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Coupons.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_AdvertUsage usage = new NewMediaState.W_AdvertUsage();
            usage.viewModel.AdvertID = (LB_Coupons.SelectedItem as AMS.Model.CouponsInfo).ID;
            usage.viewModel.GetDataList();
            usage.ShowDialog();
        }
    }
}
