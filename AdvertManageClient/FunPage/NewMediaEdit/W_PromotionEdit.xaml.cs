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
using Microsoft.Win32;

namespace AdvertManageClient.FunPage.NewMediaEdit
{
    /// <summary>
    /// W_PromotionEdit.xaml 的交互逻辑
    /// </summary>
    public partial class W_PromotionEdit : Window
    {
        public W_PromotionEdit()
        {
            InitializeComponent();
            viewModel.CustomerList.GetDataList();
            viewModel.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            cb_Cutomer.SelectedIndex = 0;
            this.DataContext = viewModel;
        }
        public AMS.ViewModel.ViewModel_PromotionEdit viewModel = new AMS.ViewModel.ViewModel_PromotionEdit();
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
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Ask,
                string.Format("确定使用当前播放时间？"));
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                pbw = new ProgressBarWindow(viewModel.Vm_ProgressBar);
                pbw.vm_Progress.Refresh();
                pbw.Show();
                Thread myThread = new Thread(new ThreadStart(Save));
                myThread.Start();
            }
        }
        ProgressBarWindow pbw;
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save()
        {
            bool isok = viewModel.Save();
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
        /// <summary>
        /// 图片选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnlogoimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                viewModel.AdImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
        }
    }
}
