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

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// UC_Receipt.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Receipt : UserControl
    {
        public UC_Receipt()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        ViewModel.ViewModel_ReceiptList viewModel = new ViewModel.ViewModel_ReceiptList();
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            FunPage.ReceiptEdit window = new FunPage.ReceiptEdit();
            window.ShowDialog();
            GetDate();
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Model.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                FunPage.ReceiptEdit window = new FunPage.ReceiptEdit();
                window.viewModel.PrintReceiptModel = viewModel.ReceiptList[LB_Model.SelectedIndex];
                window.viewModel.IsEdit = true;
                window.ShowDialog();
                GetDate();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Model.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                viewModel.Delete(LB_Model.SelectedIndex);
                GetDate();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void GetDate()
        {
            viewModel.GetDate();
        }
    }
}
