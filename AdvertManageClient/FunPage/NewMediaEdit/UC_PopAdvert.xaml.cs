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
    /// UC_PopAdvert.xaml 的交互逻辑
    /// </summary>
    public partial class UC_PopAdvert : UserControl
    {
        public UC_PopAdvert()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        AMS.ViewModel.ViewModel_PopAdvertList viewModel = new AMS.ViewModel.ViewModel_PopAdvertList();
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_PopAdvert popAdvert = new W_PopAdvert();
            popAdvert.ShowDialog();
            GetData();
        }
        //获取数据
        public void GetData()
        {
            viewModel.GetDataList();
        }

        private void btn_Release_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PopAdvert.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.Advertisement;
            w_IisureList.viewModel.CommandID = (LB_PopAdvert.SelectedItem as AMS.Model.PopAdvertInfo).ID;
            w_IisureList.ShowDialog();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            W_PopAdvert popAdvert = new W_PopAdvert();
            popAdvert.viewModel.PopAdvertModel = LB_PopAdvert.SelectedItem as AMS.Model.PopAdvertInfo;
            popAdvert.viewModel.IsEdit = true;
            popAdvert.ShowDialog();
            GetData();
        }
    }
}
