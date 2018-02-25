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
using System.Threading;

namespace AdvertManageClient.FunPage.NewMediaEdit
{
    /// <summary>
    /// W_CouponsEdit.xaml 的交互逻辑
    /// </summary>
    public partial class W_CouponsEdit : Window
    {
        public W_CouponsEdit()
        {
            InitializeComponent();
            viewModel.CustomerList.GetDataList();
            viewModel.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            cb_Cutomer.SelectedIndex = 0;
            this.DataContext = viewModel;


        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        public AMS.ViewModel.ViewModel_Coupons viewModel = new AMS.ViewModel.ViewModel_Coupons();
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
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
        /// 保存优惠券
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
        /// 广告位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            viewModel.Station = int.Parse(rb.Content.ToString().Substring(0, 1));
            if (viewModel.Station == 8)
            {
                gb_item.IsEnabled = false;
                gb_print.IsEnabled = false;
                dd_end.IsEnabled = true;
                dd_start.IsEnabled = true;
            }
            else
            {
                gb_item.IsEnabled = true;
                gb_print.IsEnabled = true;
                dd_end.IsEnabled = false;
                dd_start.IsEnabled = false;
            }
        }
        /// <summary>
        /// 添加优惠项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsItemadd_Click(object sender, RoutedEventArgs e)
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
        /// 优惠信息图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsItemSelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                if (LB_CouponsItem.SelectedIndex < 0)
                {
                    viewModel.ErrorMessage = "请先选择需要修改的项！";
                    return;
                }
                viewModel.CouponsItemList[LB_CouponsItem.SelectedIndex].PopImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));

            }
        }
        /// <summary>
        /// 添加logo
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
                viewModel.LogoImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
        }
        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LB_CouponsItem_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_CouponsItem.SelectedIndex > -1)
            {
                viewModel.NowEdiretItem = viewModel.CouponsItemList[LB_CouponsItem.SelectedIndex];
            }
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsItemMoveLift_Click(object sender, RoutedEventArgs e)
        {
            if (LB_CouponsItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            viewModel.MoveItemLeft(LB_CouponsItem.SelectedIndex);

        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsItemMoveRight_Click(object sender, RoutedEventArgs e)
        {
            if (LB_CouponsItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            viewModel.MoveItemRight(LB_CouponsItem.SelectedIndex);

        }
        /// <summary>
        /// 删除项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CouponsItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_CouponsItem.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择需要删除的项！";
                return;
            }
            viewModel.DeleteItem(LB_CouponsItem.SelectedIndex);

        }
        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_PrintItemAddText_Click(object sender, RoutedEventArgs e)
        {
            viewModel.NowEdiretItem.TemplateItem.AddItem(new AMS.ViewModel.ViewModelPrintItem());
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
                viewModel.NowEdiretItem.TemplateItem.AddItem(viewModel.NowEdiretItem.TemplateItem.AddImage(ofd.FileName));
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
            viewModel.NowEdiretItem.TemplateItem.UpMoveItem(LB_PrintTemplate.SelectedIndex);

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
            viewModel.NowEdiretItem.TemplateItem.DownMoveItem(LB_PrintTemplate.SelectedIndex);
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
            viewModel.NowEdiretItem.TemplateItem.DeleteItem(LB_PrintTemplate.SelectedIndex);
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
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold == "Bold")
            {
                viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold = "Normal";
            }
            else
            {
                viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsBold = "Bold";
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
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic == "Italic")
            {
                viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic = "Normal";
            }
            else
            {
                viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsItalic = "Italic";
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
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].FontSize++;
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
            if (viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].IsImage)
            {
                viewModel.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            viewModel.NowEdiretItem.TemplateItem.PrintIiemList[LB_PrintTemplate.SelectedIndex].FontSize--;
        }

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (viewModel.IsEdit)
            {
                (this.FindName("rb_s" + viewModel.Station) as RadioButton).IsChecked = true;
                txt_name.IsReadOnly = true;
                txt_no.IsReadOnly = true;
            }
        }
    }
}
