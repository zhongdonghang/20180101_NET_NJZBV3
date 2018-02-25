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
    /// UC_RollTitlesList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_RollTitlesList : UserControl
    {
        public UC_RollTitlesList()
        {
            InitializeComponent();
            this.DataContext = vm_SlipList;
        }

        public AMS.ViewModel.ViewModelRollTitlesListWindow vm_SlipList = new AMS.ViewModel.ViewModelRollTitlesListWindow();
        
        public void DataBinding()
        {
            vm_SlipList.GetDataList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.RoolTitlesEdit scew = new RoolTitlesEdit();
            scew.vm_SlipList.IsEdit = false;
            scew.ShowDialog();
            vm_SlipList.GetDataList();
        }

        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.RoolTitlesEdit scew = new RoolTitlesEdit();
            scew.vm_SlipList.IsEdit = true;
            scew.vm_SlipList.RollTitles = TitleAdLbox.SelectedItem as AMS.Model.AMS_RollTitles;
            scew.ShowDialog();
            vm_SlipList.GetDataList();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除这个冠名广告?");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelRollTitlesEditWindow vmtw = new AMS.ViewModel.ViewModelRollTitlesEditWindow();
                vmtw.RollTitles = TitleAdLbox.Items[TitleAdLbox.SelectedIndex] as AMS.Model.AMS_RollTitles;
                vmtw.Del();
                DataBinding();
            }
        }
            
        private void resTitleAd_Click(object sender, RoutedEventArgs e)
        {   
            if (TitleAdLbox.SelectedIndex < 0)
            {
                vm_SlipList.ErrorMessage = "请选择要下发的冠名广告！";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issue = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.RollTitles, (TitleAdLbox.SelectedItem as AMS.Model.AMS_RollTitles).ID);
            issue.ShowDialog();
        }
    }   
}
