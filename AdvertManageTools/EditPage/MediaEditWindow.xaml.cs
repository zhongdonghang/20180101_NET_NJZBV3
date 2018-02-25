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
using System.Windows.Shapes;
using AdvertManageTools.Code;
using Microsoft.Win32;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// PlaylistEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaEditWindow : Window
    {
        public MediaEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public PlaylistInfoViewModel Playlist
        {
            get { return (PlaylistInfoViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("Playlist", typeof(PlaylistInfoViewModel), typeof(MediaEditWindow));

        private bool _IsEdit = false;
        /// <summary>
        /// 是否为编辑
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; }
        }
        /// <summary>
        /// 添加媒体文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdditem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "媒体文件|*.jpg;*.bmp;*.jpeg;*.png;*.wmv;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                PlaylistItemViewModel itemVM = new PlaylistItemViewModel();
                itemVM.Name = ofd.SafeFileName;
                itemVM.FilePath = ofd.FileName;
                int sum = 0;
                if (itemVM.Name.Substring(itemVM.Name.LastIndexOf(".")) == ".wmv" || itemVM.Name.Substring(itemVM.Name.LastIndexOf(".")) == ".WMV")
                {
                    Shell32.Shell shell = new Shell32.Shell();
                    Shell32.Folder folder = shell.NameSpace(ofd.FileName.Substring(0, ofd.FileName.LastIndexOf("\\")));
                    Shell32.FolderItem folderitem = folder.ParseName(ofd.SafeFileName);
                    string len;
                    if (Environment.OSVersion.Version.Major >= 6)
                    {
                        len = folder.GetDetailsOf(folderitem, 27);
                    }
                    else
                    {
                        len = folder.GetDetailsOf(folderitem, 21);
                    }
                    string[] str = len.Split(new char[] { ':' });

                    sum = int.Parse(str[0]) * 3600 + int.Parse(str[1]) * 60 + int.Parse(str[2]) + 1;

                }
                else
                {
                    if (Playlist.ItemList.Count > 0)
                    {
                        for (int i = Playlist.ItemList.Count - 1; i >= 0; i--)
                        {
                            if (Playlist.ItemList[i].Name.Substring(Playlist.ItemList[i].Name.LastIndexOf(".")) != ".WMV" && Playlist.ItemList[i].Name.Substring(Playlist.ItemList[i].Name.LastIndexOf(".")) != ".wmv")
                            {
                                sum = Playlist.ItemList[i].SunTime;
                                break;
                            }
                        }
                    }
                }
                if (sum == 0)
                {
                    sum = 10;
                }
                itemVM.SunTime = sum;
                Playlist.ItemList.Add(itemVM);
            }
        }
        /// <summary>
        /// 导出离线版
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndownload_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog foldBrowerDialog = new System.Windows.Forms.FolderBrowserDialog();
            foldBrowerDialog.ShowDialog();
            if (!string.IsNullOrEmpty(foldBrowerDialog.SelectedPath))
            {
                if (Playlist.DownloadPlaylist(foldBrowerDialog.SelectedPath))
                {
                    MessageBox.Show("发布离线版本成功！");
                }
            }
        }
        /// <summary>
        /// 发布保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (Playlist.LoopMedia())
            {
                this.Close();
            }
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            itemlistView.ItemsSource = Playlist.ItemList;
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnup_Click(object sender, RoutedEventArgs e)
        {
            if (itemlistView.SelectedIndex > -1)
            {
                PlaylistItemViewModel selectitem = itemlistView.Items[itemlistView.SelectedIndex] as PlaylistItemViewModel;
                int index = Playlist.ItemList.IndexOf(selectitem);
                if (index > 0)
                {
                    Playlist.ItemList.Move(index, index - 1);
                }
            }
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndown_Click(object sender, RoutedEventArgs e)
        {
            if (itemlistView.SelectedIndex > -1)
            {
                PlaylistItemViewModel selectitem = itemlistView.Items[itemlistView.SelectedIndex] as PlaylistItemViewModel;
                int index = Playlist.ItemList.IndexOf(selectitem);
                if (index < Playlist.ItemList.Count - 1)
                {
                    Playlist.ItemList.Move(index, index + 1);
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (itemlistView.SelectedIndex > -1)
            {
                MessageBoxResult endResult;
                endResult = MessageBox.Show("确认要删除此条媒体文件吗？", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (endResult == MessageBoxResult.Yes)
                {
                    PlaylistItemViewModel selectitem = itemlistView.Items[itemlistView.SelectedIndex] as PlaylistItemViewModel;
                    int index = Playlist.ItemList.IndexOf(selectitem);
                    if (index < Playlist.ItemList.Count && index > -1)
                    {
                        Playlist.ItemList.RemoveAt(index);
                    }
                }
            }
        }
    }
}
