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
using System.IO;

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// UC_HardAd.xaml 的交互逻辑
    /// </summary>
    public partial class UC_HardAd : UserControl
    {
        public UC_HardAd()
        {
            InitializeComponent();
            this.DataContext = vm_AdHardList;
        }
        AMS.ViewModel.ViewModelAdHardListWindow vm_AdHardList = new AMS.ViewModel.ViewModelAdHardListWindow();
        public void DataBinging()
        {
            vm_AdHardList.GetDataList();
        }
        /// <summary>
        /// 编辑硬广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            MediaEdit.AddHardWindow ahw = new AddHardWindow();
            ahw.vm_HardAdEditWindow.HardAdModel = HardAdLbox.Items[HardAdLbox.SelectedIndex] as AMS.Model.AMS_HardAd;
            ahw.vm_HardAdEditWindow.IsEdit = true;
            ahw.vm_HardAdEditWindow.ToListXML();
            ahw.ShowDialog();
            DataBinging();
        }
        /// <summary>
        /// 删除硬广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除此硬广?");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelAdHardEditWindow vaew = new AMS.ViewModel.ViewModelAdHardEditWindow(HardAdLbox.Items[HardAdLbox.SelectedIndex] as AMS.Model.AMS_HardAd);
                vaew.Delete();
                DataBinging();
            }
        }
        /// <summary>
        /// 添加硬广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddHardWindow ah = new AddHardWindow();
                ah.vm_HardAdEditWindow.IsEdit = false;
                ah.ShowDialog();
                DataBinging();
            }
            catch (Exception ex)
            {

                vm_AdHardList.ErrorMessage = ex.Message;
            }
           
        }
        /// <summary>
        /// 下发硬广
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void resHardAd_Click(object sender, RoutedEventArgs e)
        {
            if (HardAdLbox.SelectedIndex < 0)
            {
                vm_AdHardList.ErrorMessage = "请选择需要下发的硬广";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issue = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.HardAd, (HardAdLbox.SelectedItem as AMS.Model.AMS_HardAd).ID);
            issue.ShowDialog();
        }
        /// <summary>
        /// 预览硬广图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void previewmenu_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.PreviewHardAdWindow preview = new PreviewHardAdWindow();
            preview.vm_HardAdEditWindow.HardAdModel = preview.vm_HardAdEditWindow.GetModel((HardAdLbox.Items[HardAdLbox.SelectedIndex] as AMS.Model.AMS_HardAd).Name);
            AMS.Model.AMS_HardAd model = preview.vm_HardAdEditWindow.HardAdModel;
            BitmapImage bmi = new BitmapImage();
            bmi.BeginInit();
            MemoryStream ms = new MemoryStream(model.AdImage);//model.AdImage为从数据库中获取的byte[]数组
            bmi.StreamSource = ms;
            bmi.EndInit();
            preview.image1.Source = bmi;
            preview.ShowDialog();
        }
    }
}
