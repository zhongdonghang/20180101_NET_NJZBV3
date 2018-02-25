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
    /// UC_Playlist.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Playlist : UserControl
    {
        public UC_Playlist()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        AMS.ViewModel.ViewModel_PlaylistList viewModel = new AMS.ViewModel.ViewModel_PlaylistList();
        private void btn_New_Click(object sender, RoutedEventArgs e)
        {
            W_PlaylistEdit newplayer = new W_PlaylistEdit();
            newplayer.ShowDialog();
            GetData();
        }
        //获取数据
        public void GetData()
        {
            viewModel.GetDataList();
        }

        private void btn_Release_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Playlist.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
                return;
            }
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.Advertisement;
            w_IisureList.viewModel.CommandID = (LB_Playlist.SelectedItem as AMS.Model.PlaylistInfo).ID;
            w_IisureList.ShowDialog();
        }

        private void Btn_edit_Click(object sender, RoutedEventArgs e)
        {
            W_PlaylistEdit player = new W_PlaylistEdit();
            player.viewModel.PlaylistModel = LB_Playlist.SelectedItem as AMS.Model.PlaylistInfo;
            player.viewModel.IsEdit = true;
            player.ShowDialog();
            GetData();
        }
    }
}
