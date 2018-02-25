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
using Microsoft.Win32;
using System.Threading;

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// MediaPlayListEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayListEditWindow : Window
    {
        public MediaPlayListEditWindow()
        {
            InitializeComponent();
            //AMS.ViewModel.ViewModelObject.User = ((App)Application.Current).LoginUser;
            this.DataContext = vm_PlaylistInfo;
        }
        public AMS.ViewModel.ViewModelPlayListEditWindow vm_PlaylistInfo = new AMS.ViewModel.ViewModelPlayListEditWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void AddNewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "媒体文件|*.jpg;*.bmp;*.jpeg;*.png;*.wmv;*.JPG;*.BMP;*.JPEG;*.PNG;*.WMV;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_PlaylistInfo.AddNewMediaFile(vm_PlaylistInfo.GetItem(ofd.FileName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBFile.Items.Count == 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先添加一个媒体文件！";
                return;
            }
            if (LBFile.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择右边列表中的一个媒体文件！";
                return;
            }

            vm_PlaylistInfo.AddNewPlayFile(LBFile.Items[LBFile.SelectedIndex] as AMS.ViewModel.ViewModelVideoItem);

        }

        private void insplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBFile.Items.Count == 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先添加一个媒体文件！";
                return;
            }
            if (LBFile.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择右边列表中的一个媒体文件！";
                return;
            }
            if (LBPlay.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择左边列表要插入文件的位置！";
                return;
            }
            vm_PlaylistInfo.InsertPlayFile(LBFile.Items[LBFile.SelectedIndex] as AMS.ViewModel.ViewModelVideoItem, LBPlay.SelectedIndex);

        }

        private void deleteplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择左边列表要删除的文件！";
                return;
            }
            int index = LBPlay.SelectedIndex;
            vm_PlaylistInfo.DeletePlayFile(index);
            LBPlay.SelectedIndex = index - 1;
        }

        private void mixadd_Click(object sender, RoutedEventArgs e)
        {
            MediaItemMixWindow mimw = new MediaItemMixWindow();
            mimw.vm_ToopList.MediaFileList = vm_PlaylistInfo.MediaFileList;
            mimw.ShowDialog();
            if (mimw.vm_ToopList.LoopPlayFileList.Count > 0)
            {
                for (int i = 0; i < mimw.vm_ToopList.LoopPlayFileList.Count; i++)
                {
                    vm_PlaylistInfo.LoopAddNewPlayFile(mimw.vm_ToopList.LoopPlayFileList[i]);
                }
                vm_PlaylistInfo.RefreshPlayList();
                vm_PlaylistInfo.RefreshFileList();
            }
        }

        private void mininst_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择左边列表要插入文件的位置！";
                return;
            }
            MediaItemMixWindow mimw = new MediaItemMixWindow();
            mimw.vm_ToopList.MediaFileList = vm_PlaylistInfo.MediaFileList;
            mimw.ShowDialog();
            if (mimw.vm_ToopList.LoopPlayFileList.Count > 0)
            {
                for (int i = 0; i < mimw.vm_ToopList.LoopPlayFileList.Count; i++)
                {
                    vm_PlaylistInfo.LoopInsertPlayFile(mimw.vm_ToopList.LoopPlayFileList[i], LBPlay.SelectedIndex + i);
                }
                vm_PlaylistInfo.RefreshPlayList();
                vm_PlaylistInfo.RefreshFileList();
            }
        }
        ProgressBarWindow pbw;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Ask,
                string.Format("确定使用当前播放时间？\n{0}到{1}", vm_PlaylistInfo.EffectDate, vm_PlaylistInfo.EndDate));
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                pbw = new ProgressBarWindow(vm_PlaylistInfo.Vm_ProgressBar);
                pbw.vm_Progress.Refresh();
                pbw.Show();
                Thread myThread = new Thread(new ThreadStart(Save));
                myThread.Start();
            }

        }
        private void Save()
        {
            bool isok = vm_PlaylistInfo.Save();
            if (isok)
            {
                System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(MessageBoxShow));
            }
            System.Windows.Application.Current.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.SystemIdle, new DoTask(ProgressClose));
        }

        private void ProgressClose()
        {
            pbw.Close();
        }
        private delegate void DoTask();
        private void MessageBoxShow()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "保存成功！");
            mbw.ShowDialog();
            this.Close();
        }
        private void upbtn_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择左边列表选择需要移动的项！";
                return;
            }
            vm_PlaylistInfo.UpMoveItem(LBPlay.SelectedIndex);
        }

        private void downbtn_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_PlaylistInfo.ErrorMessage = "请先选择左边列表选择需要移动的项！";
                return;
            }
            vm_PlaylistInfo.DownMoveItem(LBPlay.SelectedIndex);

        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            vm_PlaylistInfo.RefreshPlayList();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vm_PlaylistInfo.RefreshPlayList();
        }
    }
}
