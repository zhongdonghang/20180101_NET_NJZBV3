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
using System.Threading;

namespace AdvertManageClient.FunPage.NewMediaEdit
{
    /// <summary>
    /// W_TitleAdvertEdit.xaml 的交互逻辑
    /// </summary>
    public partial class W_TitleAdvertEdit : Window
    {
        public W_TitleAdvertEdit()
        {
            InitializeComponent();
            viewModel.CustomerList.GetDataList();
            viewModel.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            cb_Cutomer.SelectedIndex = 0;
            this.DataContext = viewModel;
        }
        public AMS.ViewModel.ViewModel_TitleAdvert viewModel = new AMS.ViewModel.ViewModel_TitleAdvert();
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
                MessageBoxShow();
            }

        }
        private void MessageBoxShow()
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "保存成功！");
            mbw.ShowDialog();
            this.Close();
        }
        /// <summary>
        /// 客户选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cb_Cutomer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (viewModel.IsEdit)
            {
                cb_Cutomer.SelectedIndex = viewModel.CustomerID;
            }
            else
            {
                viewModel.CustomerID = (cb_Cutomer.SelectedItem as AMS.Model.AMS_AdCustomer).ID;
            }
        }

    }
}
