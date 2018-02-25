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
using Microsoft.Win32;

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// UC_UserGuide.xaml 的交互逻辑
    /// </summary>
    public partial class UC_UserGuide : UserControl
    {
        public UC_UserGuide()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        ViewModel.ViewModel_UserGuide viewModel = new ViewModel.ViewModel_UserGuide();
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                viewModel.AddNewItem(new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute)));
            }
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MoveLift_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Image.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                viewModel.MoveItemLeft(LB_Image.SelectedIndex);
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Image.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                viewModel.MoveItemRight(LB_Image.SelectedIndex);
            }
        }
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SelectImage_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Image.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                OpenFileDialog ofd = new OpenFileDialog();
                ofd.Multiselect = false;
                ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
                ofd.ShowDialog();
                if (!string.IsNullOrEmpty(ofd.FileName))
                {
                    viewModel.GuideImages[LB_Image.SelectedIndex].ImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                }
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Image.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                viewModel.DeleteItem(LB_Image.SelectedIndex);
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Save())
            {
                viewModel.GetData();
            }
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        public void GetData()
        {
            viewModel.GetData();
        }
    }
}
