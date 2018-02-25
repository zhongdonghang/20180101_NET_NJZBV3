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
    /// ReceiptEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ReceiptEdit : Window
    {
        public ReceiptEdit()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        public ViewModel.ViewModel_Receipt viewModel = new ViewModel.ViewModel_Receipt();
        /// <summary>
        /// 拖拽移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
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
                viewModel.ToViewModel();
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
        /// 添加文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemAddText_Click(object sender, RoutedEventArgs e)
        {
            viewModel.TemplateItem.AddItem(new ViewModel.ViewModelPrintItem());
        }
        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                viewModel.TemplateItem.AddItem(viewModel.TemplateItem.AddImage(ofd.FileName));
            }
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemMoveUp_Click(object sender, RoutedEventArgs e)
        {

            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            viewModel.TemplateItem.UpMoveItem(LB_PrintTemplate.SelectedIndex);
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemMoveDown_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            viewModel.TemplateItem.DownMoveItem(LB_PrintTemplate.SelectedIndex);
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要删除的项！";
                return;
            }
            viewModel.TemplateItem.DeleteItem(LB_PrintTemplate.SelectedIndex);
        }
        /// <summary>
        /// 加粗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemBlod_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold == "Bold")
            {
                viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold = "Normal";
            }
            else
            {
                viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold = "Bold";
            }
        }
        /// <summary>
        /// 倾斜
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemItalic_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic == "Italic")
            {
                viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic = "Normal";
            }
            else
            {
                viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic = "Italic";
            }
        }
        /// <summary>
        /// 字号加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemSizePlue_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].FontSize++;
        }
        /// <summary>
        /// 字号减
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemSizeMinus_Click(object sender, RoutedEventArgs e)
        {
            if (LB_PrintTemplate.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            viewModel.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].FontSize--;
        }
    }
}
