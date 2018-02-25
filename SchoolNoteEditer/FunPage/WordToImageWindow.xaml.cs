using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace SchoolNoteEditer.FunPage
{
    /// <summary>
    /// WordToImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WordToImageWindow : Window
    {
        public WordToImageWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private ViewModel.ViewModel_WordToImage viewModel = new ViewModel.ViewModel_WordToImage();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private Bitmap image;
        private void btn_Watch_Click(object sender, RoutedEventArgs e)
        {
            image = viewModel.GetBitmap();
            if (image != null)
            {
                image_Path.Fill = new ImageBrush(Imaging.CreateBitmapSourceFromHBitmap(image.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions()));
            }
            else
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "生成失败！";
                mbw.viewModel.Type = Code.MessageBoxType.Error;
                mbw.ShowDialog();
            }
        }

        private void btn_TextFontSizePluse_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TextFontSize++;
        }

        private void btn_TextFontSizeLower_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TextFontSize--;
        }

        private void btn_TitleFontSizePluse1_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TitleFontSize++;
        }

        private void btn_TitleFontSizeLower1_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TitleFontSize--;
        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            if (image == null)
            {
                mbw.viewModel.Message = "请先预览生成图片！";
                mbw.viewModel.Type = Code.MessageBoxType.Error;
                mbw.ShowDialog();
            }
            SaveFileDialog openFileDialog = new SaveFileDialog();
            openFileDialog.Title = "保存为";
            openFileDialog.Filter = "jpg文件|*.jpg|bmp文件|*.bmp|所有文件|*.*";
            openFileDialog.FileName = string.Empty;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;
            openFileDialog.DefaultExt = "jpg";
            bool result = (bool)openFileDialog.ShowDialog();
            if (!result)
            {
                return;
            }
            string fileName = openFileDialog.FileName;
            image.Save(fileName);
            mbw = new MessageBoxWindow();
            mbw.viewModel.Message = "保存完成！";
            mbw.viewModel.Type = Code.MessageBoxType.Success;
            mbw.ShowDialog();
        }
    }
}
