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
    /// UC_ReaderAdvert.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ReaderAdvert : UserControl
    {
        public UC_ReaderAdvert()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        AMS.ViewModel.ViewModel_ReaderAdverList viewModel = new AMS.ViewModel.ViewModel_ReaderAdverList();
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_ReaderAdvertEdit readerAdvert = new W_ReaderAdvertEdit();
            readerAdvert.ShowDialog();
            GetData();
        }
        //获取数据
        public void GetData()
        {
            viewModel.GetDataList();
        }

        private void btn_Release_Click(object sender, RoutedEventArgs e)
        {
            if (LB_ReaderAdvert.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.Advertisement;
            w_IisureList.viewModel.CommandID = (LB_ReaderAdvert.SelectedItem as AMS.Model.ReaderAdvertInfo).ID;
            w_IisureList.ShowDialog();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            W_ReaderAdvertEdit readerAdvert = new W_ReaderAdvertEdit();
            readerAdvert.viewModel.ReaderAdvertModel = LB_ReaderAdvert.SelectedItem as AMS.Model.ReaderAdvertInfo;
            readerAdvert.viewModel.IsEdit = true;
            readerAdvert.ShowDialog();
            GetData();
        }
    }
}
