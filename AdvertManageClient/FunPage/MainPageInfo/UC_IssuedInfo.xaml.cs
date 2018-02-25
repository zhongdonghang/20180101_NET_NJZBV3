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
using System.Data;

namespace AdvertManageClient.FunPage.MainPageInfo
{
    /// <summary>
    /// UC_IssuedInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_IssuedInfo : UserControl
    {
        public UC_IssuedInfo()
        {
            InitializeComponent();
            this.DataContext = vm_IssuedInfoList;
        }

        public AMS.ViewModel.ViewModelIssuedInfoWindow vm_IssuedInfoList = new AMS.ViewModel.ViewModelIssuedInfoWindow();

        public AMS.ViewModel.ViewModelIssudeInfoScreening vms = new AMS.ViewModel.ViewModelIssudeInfoScreening();

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            vm_IssuedInfoList.GetDataList(vms);
        }

        public void DataBinding()
        {
            vm_IssuedInfoList.GetDataList(null);
            vm_IssuedInfoList.BindCommandTypeItem();
            vm_IssuedInfoList.BindSchoolList();
            vm_IssuedInfoList.BindCommandHandleResultItem();
        }

        private void cbxCommandType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vms.SelectedCommandTypeValue = (int)(((AMS.ViewModel.IssuedInfoTypeItem)cbxCommandType.SelectedValue).Value);
        }

        private void cbxCommandHandle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vms.SelectedHandleResultTypeValue = (int)(((AMS.ViewModel.IssuedHandleResultItem)cbxCommandHandle.SelectedValue).Value);
        }

        private void cbxSchool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AMS.Model.AMS_School school = cbxSchool.SelectedItem as AMS.Model.AMS_School;
            vms.SelectedSchoolNumValue = school.Number;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (ComList.SelectedIndex > -1)
            {
                AMS.ViewModel.ViewModelCommandDetail vm = (AMS.ViewModel.ViewModelCommandDetail)this.ComList.SelectedItem;
                if (vm_IssuedInfoList.DelCommandDetailInfo(vm.Id.Value))
                {
                    MessageBoxWindow mbw = new MessageBoxWindow();
                    mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "删除成功");
                    mbw.ShowDialog();
                    DataBinding();
                }
                else
                {
                    MessageBoxWindow mbw = new MessageBoxWindow();
                    mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Error, "删除失败");
                    mbw.ShowDialog();
                }
            }
            else
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "请选中删除");
                mbw.ShowDialog();
            }
        }

        private void ComList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
