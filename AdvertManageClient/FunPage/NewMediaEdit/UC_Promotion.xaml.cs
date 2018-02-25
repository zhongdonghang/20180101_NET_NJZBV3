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

namespace AdvertManageClient.FunPage.NewMediaEdit
{
    /// <summary>
    /// UC_Promotion.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Promotion : UserControl
    {
        public UC_Promotion()
        {
            InitializeComponent(); 
            this.DataContext = viewModel;
        }
        AMS.ViewModel.ViewModel_PromotionList viewModel = new AMS.ViewModel.ViewModel_PromotionList();
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_PromotionEdit promotionAdvert = new W_PromotionEdit();
            promotionAdvert.ShowDialog();
            GetData();
        }
        //获取数据
        public void GetData()
        {
            viewModel.GetDataList();
        }

        private void btn_Release_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Promotion.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.Advertisement;
            w_IisureList.viewModel.CommandID = (LB_Promotion.SelectedItem as AMS.Model.PromotionAdvertInfo).ID;
            w_IisureList.ShowDialog();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            W_PromotionEdit promotionAdvert = new W_PromotionEdit();
            promotionAdvert.viewModel.PromotionModel = LB_Promotion.SelectedItem as AMS.Model.PromotionAdvertInfo;
            promotionAdvert.viewModel.IsEdit = true;
            promotionAdvert.ShowDialog();
            GetData();
        }
    }
}
