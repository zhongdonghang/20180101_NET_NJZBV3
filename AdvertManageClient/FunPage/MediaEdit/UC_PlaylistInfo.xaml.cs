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
using System.Threading;

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// UC_PlaylistInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_PlaylistInfo : UserControl
    {
        public UC_PlaylistInfo()
        {
            InitializeComponent();
            this.DataContext = vm_Playlist;
        }
        public AMS.ViewModel.ViewModelPlaylistListUC vm_Playlist = new AMS.ViewModel.ViewModelPlaylistListUC();
        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.MediaPlayListEditWindow mplew = new MediaEdit.MediaPlayListEditWindow();
            mplew.vm_PlaylistInfo.PlayListModel = (LBList.Items[LBList.SelectedIndex] as AMS.ViewModel.ViewModelPlayListShow).ModelList;
            mplew.vm_PlaylistInfo.IsEdit = true;
            mplew.vm_PlaylistInfo.RefreshModel();
            mplew.ShowDialog();
            vm_Playlist.GetDataList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.MediaPlayListEditWindow mplew = new MediaEdit.MediaPlayListEditWindow();
            mplew.ShowDialog();
            vm_Playlist.GetDataList();
        }
        public void DataBinding()
        {
            vm_Playlist.GetDataList();
        }

        private void resslip_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0 || !(LBList.SelectedItem is AMS.ViewModel.ViewModelPlayListShow))
            {
                vm_Playlist.ErrorMessage = "请选择需要下发的播放列表！";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issw = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.Playlist, (LBList.SelectedItem as AMS.ViewModel.ViewModelPlayListShow).ModelList.Id);
            issw.ShowDialog();
        }
        ProgressBarWindow pbw;
        MediaPlayerTest mpt;
        private void testmenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Ask, "是否显示优惠券栏？");
            mbw.ShowDialog();

            mpt = new MediaPlayerTest();
            mpt.vm_playlist.PlayModel = AMS.Model.AMS_PlayList.Parse(AMS.Model.AMS_PlayList.ToXml((LBList.SelectedItem as AMS.ViewModel.ViewModelPlayListShow).ModelList));
            mpt.vm_playlist.IsShowSlip = mbw.vm_MessageBoxWindow.Result;

            pbw = new ProgressBarWindow(mpt.vm_playlist.Vm_Progressbar);
            pbw.vm_Progress.Refresh();
            pbw.Show();

            Thread myThread = new Thread(new ThreadStart(Save));
            myThread.Start();


        }
        private void Save()
        {
            bool isok = mpt.vm_playlist.DownLoadFile();
            if (isok)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(MediaPlayerTestShow));
            }
            else
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(MessageBoxShow));
            }
            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(ProgressClose));
        }
        private void MessageBoxShow()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Error, "媒体文件下载失败！");
            mbw.ShowDialog();
        }
        private void ProgressClose()
        {
            pbw.Close();
        }
        private delegate void DoTask();
        private void MediaPlayerTestShow()
        {
            mpt.Start();
            mpt.Show();
        }
    }
}
