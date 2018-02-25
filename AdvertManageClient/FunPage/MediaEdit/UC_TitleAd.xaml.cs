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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// UC_TitleAd.xaml 的交互逻辑
    /// </summary>
    public partial class UC_TitleAd : UserControl
    {
        public UC_TitleAd()
        {
            InitializeComponent();
            this.DataContext = vm_TitleAdList;
        }
        public AMS.ViewModel.ViewModelTitleAdListWindow vm_TitleAdList = new AMS.ViewModel.ViewModelTitleAdListWindow();

        public void DataBinding()
        {
            vm_TitleAdList.GetDataList();
        }

        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            MediaEdit.AddTitleAdWindow atw = new AddTitleAdWindow();
            atw.vm_TitleAdEditWindow.TitleAdModel = TitleAdLbox.Items[TitleAdLbox.SelectedIndex] as AMS.Model.AMS_TitleAd;
            atw.vm_TitleAdEditWindow.IsEdit = true;
            atw.ShowDialog();
            DataBinding();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除这个冠名广告?");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelTitleAdEditWindow vmtw = new AMS.ViewModel.ViewModelTitleAdEditWindow();
                vmtw.TitleAdModel = TitleAdLbox.Items[TitleAdLbox.SelectedIndex] as AMS.Model.AMS_TitleAd;
                vmtw.Delete();
                DataBinding();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaEdit.AddTitleAdWindow atw = new AddTitleAdWindow();
            atw.vm_TitleAdEditWindow.IsEdit = false;
            atw.ShowDialog();
            DataBinding();     
        }

        private void resTitleAd_Click(object sender, RoutedEventArgs e)
        {
            if (TitleAdLbox.SelectedIndex<0)
            {
                vm_TitleAdList.ErrorMessage = "请选择要下发的冠名广告！";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issue = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.TitleAd, (TitleAdLbox.SelectedItem as AMS.Model.AMS_TitleAd).Id);
            issue.ShowDialog();
        }
    }
}
