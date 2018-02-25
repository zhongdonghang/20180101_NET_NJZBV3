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
    public partial class PlaylistEditWindow : Window
    {
        public PlaylistEditWindow()
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
            DependencyProperty.Register("Playlist", typeof(PlaylistInfoViewModel), typeof(PlaylistEditWindow));

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
                itemVM.Md5Value = SeatManage.SeatManageComm.SeatComm.GetMD5HashFromFile(itemVM.FilePath);
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
            if (!_IsEdit)
            {
                if (Playlist.AddPlaylist())
                {
                    MessageBox.Show("发布成功！");
                    this.Close();
                }
            }
            else
            {

                if (Playlist.UpdatePlaylist())
                {
                    MessageBox.Show("更新成功！");
                    this.Close();
                }
            }
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_IsEdit)
            {
                txtNum.IsEnabled = false;
                btnSave.Content = "更新";
            }
            else
            {
                Playlist = new PlaylistInfoViewModel();
            }
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
        /// <summary>
        /// 批量添加文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_AddloopMedia_Click(object sender, RoutedEventArgs e)
        {
            MediaEditWindow med = new MediaEditWindow();
            med.Playlist = new PlaylistInfoViewModel();
            med.ShowDialog();
            if (med.Playlist.LoopList.Count > 0)
            {
                foreach (PlaylistItemViewModel loopitem in med.Playlist.LoopList)
                {
                    PlaylistItemViewModel itemVM = new PlaylistItemViewModel();
                    itemVM.Name = loopitem.Name;
                    itemVM.FilePath = loopitem.FilePath;
                    itemVM.SunTime = loopitem.SunTime;
                    itemVM.Md5Value = loopitem.Md5Value;
                    Playlist.ItemList.Add(itemVM);
                }
            }
        }
        /// <summary>
        /// 媒体统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountMedia_Click(object sender, RoutedEventArgs e)
        {
            int AllMediaCount = 0;
            int AllPlayTime = 0;
            Dictionary<string, int> MediaList = new Dictionary<string, int>();
            Dictionary<string, int> TimeList = new Dictionary<string, int>();
            foreach (PlaylistItemViewModel mediaitem in Playlist.ItemList)
            {
                AllMediaCount++;
                AllPlayTime = AllPlayTime + mediaitem.SunTime;
                if (MediaList.ContainsKey(mediaitem.Name))
                {
                    MediaList[mediaitem.Name]++;
                    TimeList[mediaitem.Name] = TimeList[mediaitem.Name] + mediaitem.SunTime;
                }
                else
                {
                    MediaList.Add(mediaitem.Name, 1);
                    TimeList.Add(mediaitem.Name, mediaitem.SunTime);
                }
            }
            StringBuilder message = new StringBuilder();
            message.Append("媒体文件统计：");
            message.Append("\n\n全部媒体文件\n播放总次数" + AllMediaCount + " 总时长" + AllPlayTime / 60 + "分钟");
            foreach (KeyValuePair<string, int> item in MediaList)
            {
                message.Append("\n\n" + item.Key + "\n播放总次数" + item.Value + " 总时长" + TimeList[item.Key] / 60 + "分钟");
            }
            MessageBox.Show(message.ToString());

        }
    }
}
