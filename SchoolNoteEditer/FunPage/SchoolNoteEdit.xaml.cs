﻿using System;
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
    /// SchoolNoteEdit.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolNoteEdit : Window
    {
        public SchoolNoteEdit()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public ViewModel.ViewModel_SchoolNoteEdit viewModel = new ViewModel.ViewModel_SchoolNoteEdit();
        private void btnlogoimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                viewModel.NoteImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
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
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsEdit)
            {
                txt_name.IsReadOnly = true;
                txt_no.IsReadOnly = true;
                viewModel.GetData();
            }
        }
    }
}
