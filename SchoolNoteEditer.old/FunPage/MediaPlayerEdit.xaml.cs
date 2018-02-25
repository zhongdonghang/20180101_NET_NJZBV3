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

namespace SchoolNoteEditer.FunPage
{
    /// <summary>
    /// MediaPlayerEdit.xaml 的交互逻辑
    /// </summary>
    public partial class MediaPlayerEdit : Window
    {
        public MediaPlayerEdit()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public SchoolNoteEditer.ViewModel.ViewModel_MediaPlayer viewModel = new ViewModel.ViewModel_MediaPlayer();
        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsEdit)
            {
                txt_name.IsReadOnly = true;
                txt_no.IsReadOnly = true;
                viewModel.GetData();
            }
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Save())
            {
                this.Close();
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VideoItemadd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "媒体文件|*.jpg;*.bmp;*.jpeg;*.png;*.wmv";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                SchoolNoteEditer.ViewModel.ViewModel_VideoItem newVideo = viewModel.GetNewItme(ofd.FileName);
                if (newVideo != null)
                {
                    viewModel.VideoItems.Add(newVideo);
                }
            }
            LB_VideoItem.SelectedIndex = LB_VideoItem.Items.Count - 1;
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VideoItemMoveLift_Click(object sender, RoutedEventArgs e)
        {
            if (LB_VideoItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个修改的项";
                return;
            }
            viewModel.MoveLeft(LB_VideoItem.SelectedIndex);
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VideoItemMoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (LB_VideoItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个修改的项";
                return;
            }
            viewModel.MoveRight(LB_VideoItem.SelectedIndex);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VideoItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_VideoItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个删除的项";
                return;
            }
            viewModel.ItemDelete(LB_VideoItem.SelectedIndex);
        }
        /// <summary>
        /// 更换图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_VideoItemSelectImage_Click(object sender, RoutedEventArgs e)
        {
            if (LB_VideoItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个修改的项";
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "媒体文件|*.jpg;*.bmp;*.jpeg;*.png;*.wmv";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                SchoolNoteEditer.ViewModel.ViewModel_VideoItem newVideo = viewModel.GetNewItme(ofd.FileName);
                if (newVideo != null)
                {
                    viewModel.VideoItems[LB_VideoItem.SelectedIndex] = newVideo;
                }
            }
        }
        /// <summary>
        /// 点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Button)
            {
                viewModel.ErrorMessage = "";
            }
        }
    }
}
