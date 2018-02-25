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

namespace SchoolNoteEditer.FunPage
{
    /// <summary>
    /// DimensionalCode.xaml 的交互逻辑
    /// </summary>
    public partial class DimensionalCode_WeChar : Window
    {
        public DimensionalCode_WeChar()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public ViewModel.ViewModel_DimensionalTemplate_WeChar viewModel = new ViewModel.ViewModel_DimensionalTemplate_WeChar();
        /// <summary>
        /// 选择文件夹
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelecePath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                viewModel.SavePath = fbd.SelectedPath;
            }
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
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
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "导出成功！";
                mbw.viewModel.Type = Code.MessageBoxType.Success;
                mbw.ShowDialog();
            }
            else
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.viewModel.Message = "导出失败！";
                mbw.viewModel.Type = Code.MessageBoxType.Error;
                mbw.ShowDialog();
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void rb_1code_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.Codeconut = 1;
        }

        private void rb_2code_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.Codeconut = 2;
        }

        private void rb_4code_Checked(object sender, RoutedEventArgs e)
        {
            viewModel.Codeconut = 4;
        }

        private void SeleceTemp_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
            {
                viewModel.TemplatePath = fbd.SelectedPath;
                viewModel.LoadTemplate();
            }
        }
    }
}
