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
    /// PlaylistInfo.xaml 的交互逻辑
    /// </summary>
    public partial class PlaylistInfo : System.Windows.Controls.UserControl
    {
        public PlaylistInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            playlistdataGrid.ItemsSource = playelistList.PlaylistList;
        }
        PlaylistInfoListViewModel playelistList = new PlaylistInfoListViewModel();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {

            playelistList.DataBinding();

        }
        /// <summary>
        /// 添加播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewPlayList_Click(object sender, RoutedEventArgs e)
        {
            PlaylistEditWindow pew = new PlaylistEditWindow();
            pew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 编辑播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlayEdit_Click(object sender, RoutedEventArgs e)
        {
            PlaylistInfoViewModel editplaylist = playlistdataGrid.Items[playlistdataGrid.SelectedIndex] as PlaylistInfoViewModel;
            PlaylistEditWindow pew = new PlaylistEditWindow();
            pew.Playlist = editplaylist;
            pew.IsEdit = true;
            pew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 离线版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDownload_Click(object sender, RoutedEventArgs e)
        {
            PlaylistInfoViewModel editplaylist = playlistdataGrid.Items[playlistdataGrid.SelectedIndex] as PlaylistInfoViewModel;
            System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
            foldBrowerDialog.ShowDialog();
            if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
            {

                if (editplaylist.DownloadPlaylist(foldBrowerDialog.SelectedPath))
                {
                    MessageBox.Show("发布离线版本成功！");
                }
                else
                {
                    MessageBox.Show("发布离线版本失败！");
                }
            }
        }
        /// <summary>
        /// 下发播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRelease_Click(object sender, RoutedEventArgs e)
        {
            PlaylistInfoViewModel Releaseplaylist = playlistdataGrid.Items[playlistdataGrid.SelectedIndex] as PlaylistInfoViewModel;
            IssueCommand ic = new IssueCommand();
            ic.Command = AdvertManage.Model.Enum.CommandType.Playlist;
            ic.CommandId = int.Parse(Releaseplaylist.Id);
            ic.ShowDialog();
        }
    }
}
