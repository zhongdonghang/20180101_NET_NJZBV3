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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// MediaItemMixWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MediaItemMixWindow : Window
    {
        public MediaItemMixWindow()
        {
            InitializeComponent();
            this.DataContext = vm_ToopList;
        }
        public AMS.ViewModel.ViewModelPlayListEditWindow vm_ToopList = new AMS.ViewModel.ViewModelPlayListEditWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void addplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBFile.Items.Count == 0)
            {
                vm_ToopList.ErrorMessage = "请先添加一个媒体文件！";
                return;
            }
            if (LBFile.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择右边列表中的一个媒体文件！";
                return;
            }

            vm_ToopList.AddNewPlayFile(LBFile.Items[LBFile.SelectedIndex] as AMS.ViewModel.ViewModelVideoItem);
            vm_ToopList.RefreshLoopTime();
        }

        private void insplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBFile.Items.Count == 0)
            {
                vm_ToopList.ErrorMessage = "请先添加一个媒体文件！";
                return;
            }
            if (LBFile.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择右边列表中的一个媒体文件！";
                return;
            }
            if (LBPlay.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择左边列表要插入文件的位置！";
                return;
            }
            vm_ToopList.InsertPlayFile(LBFile.Items[LBFile.SelectedIndex] as AMS.ViewModel.ViewModelVideoItem, LBPlay.SelectedIndex);
            vm_ToopList.RefreshLoopTime();
        }

        private void deleteplay_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择左边列表要删除的文件！";
                return;
            }
            int index = LBPlay.SelectedIndex;
            vm_ToopList.DeletePlayFile(index);
            LBPlay.SelectedIndex = index - 1;
            vm_ToopList.RefreshLoopTime();
        }

        private void AddNewFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "媒体文件|*.jpg;*.bmp;*.jpeg;*.png;*.wmv;*.JPG;*.BMP;*.JPEG;*.PNG;*.WMV;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_ToopList.AddNewMediaFile(vm_ToopList.GetItem(ofd.FileName));
            }
        }

        private void UPitem_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择左边列表选择需要移动的项！";
                return;
            }
            vm_ToopList.UpMoveItem(LBPlay.SelectedIndex);
        }

        private void DownItem_Click(object sender, RoutedEventArgs e)
        {
            if (LBPlay.SelectedIndex < 0)
            {
                vm_ToopList.ErrorMessage = "请先选择左边列表选择需要移动的项！";
                return;
            }
            vm_ToopList.DownMoveItem(LBPlay.SelectedIndex);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vm_ToopList.PlayFileList = new System.Collections.ObjectModel.ObservableCollection<AMS.ViewModel.ViewModelVideoItem>();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnRef_Click(object sender, RoutedEventArgs e)
        {
            vm_ToopList.RefreshLoopTime();
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            vm_ToopList.RefreshLoopTime();
        }
    }
}
