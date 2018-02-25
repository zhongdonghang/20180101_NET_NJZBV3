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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// SlipCustomerEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SlipCustomerEditWindow : Window
    {
        public SlipCustomerEditWindow()
        {
            InitializeComponent();
            // vm_SlipInfo.User = ((App)Application.Current).LoginUser;
            vm_SlipInfo.CustomerList.GetDataList();
            vm_SlipInfo.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            ccb.SelectedIndex = 0;
            this.DataContext = vm_SlipInfo;
        }
        public AMS.ViewModel.ViewModelSlipCustomerEditWindow vm_SlipInfo = new AMS.ViewModel.ViewModelSlipCustomerEditWindow();
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnlogoimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_SlipInfo.LogoImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void btnwindowsimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_SlipInfo.PrintWindowImageInfo = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void btnclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnfsup_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsImage)
            {
                vm_SlipInfo.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].FontSize++;
        }

        private void btnfsdown_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsImage)
            {
                vm_SlipInfo.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].FontSize--;
        }

        private void btnfb_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsImage)
            {
                vm_SlipInfo.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsBold == "Bold")
            {
                vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsBold = "Normal";
            }
            else
            {
                vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsBold = "Bold";
            }
        }

        private void btnfi_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要修改的文本！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsImage)
            {
                vm_SlipInfo.ErrorMessage = "请先选择文本进行编辑！";
                return;
            }
            if (vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsItalic == "Italic")
            {
                vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsItalic = "Normal";
            }
            else
            {
                vm_SlipInfo.VM_Template.PrintIiemList[LBlist.SelectedIndex].IsItalic = "Italic";
            }
        }

        private void btnaddimage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_SlipInfo.VM_Template.AddItem(vm_SlipInfo.VM_Template.AddImage(ofd.FileName));
            }
        }

        private void btnaddtxt_Click(object sender, RoutedEventArgs e)
        {
            vm_SlipInfo.VM_Template.AddItem(new AMS.ViewModel.ViewModelPrintItem());
        }

        private void btnmoveup_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            vm_SlipInfo.VM_Template.UpMoveItem(LBlist.SelectedIndex);
        }

        private void btnmovedown_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要移动的项！";
                return;
            }
            vm_SlipInfo.VM_Template.DownMoveItem(LBlist.SelectedIndex);
        }

        private void btninserttxt_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择插入的位置！";
                return;
            }
            vm_SlipInfo.VM_Template.InsertItem(new AMS.ViewModel.ViewModelPrintItem(), LBlist.SelectedIndex);
        }

        private void btninserimage_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择插入的位置！";
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                vm_SlipInfo.VM_Template.InsertItem(vm_SlipInfo.VM_Template.AddImage(ofd.FileName), LBlist.SelectedIndex);
            }
        }

        private void btndelete_Click(object sender, RoutedEventArgs e)
        {
            if (LBlist.SelectedIndex < 0)
            {
                vm_SlipInfo.ErrorMessage = "请先选择需要删除的项！";
                return;
            }
            vm_SlipInfo.VM_Template.DeleteItem(LBlist.SelectedIndex);
        }
        ProgressBarWindow pbw;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Ask,
                string.Format("确定使用当前播放时间？\n{0}到{1}", vm_SlipInfo.EffectDate, vm_SlipInfo.EndDate));
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                pbw = new ProgressBarWindow(vm_SlipInfo.Vm_ProgressBar);
                pbw.vm_Progress.Refresh();
                pbw.Show();
                Thread myThread = new Thread(new ThreadStart(Save));
                myThread.Start();
            }

        }
        private void Save()
        {
            bool isok = vm_SlipInfo.Save();
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
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_SlipInfo.IsEdit)
            {
                ccb.SelectedIndex = vm_SlipInfo.CustomerID;
            }
            else
            {
                vm_SlipInfo.CustomerID = (ccb.SelectedItem as AMS.Model.AMS_AdCustomer).ID;
            }

        }
    }
}
