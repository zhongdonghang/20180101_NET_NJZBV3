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
    /// UC_SlipCustomerList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SlipCustomerList : UserControl
    {
        public UC_SlipCustomerList()
        {
            InitializeComponent();
            this.DataContext = vm_SlipList;
        }
        public AMS.ViewModel.ViewModelSlipCustomerList vm_SlipList = new AMS.ViewModel.ViewModelSlipCustomerList();
        private void addslip_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.SlipCustomerEditWindow scew = new SlipCustomerEditWindow();
            scew.vm_SlipInfo.IsEdit = false;
            scew.ShowDialog();
            vm_SlipList.GetDataList();
        }

        private void resslip_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0)
            {
                vm_SlipList.ErrorMessage = "请选择需要下发的优惠券！";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issw = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.SlipCustomer, (LBList.SelectedItem as AMS.Model.AMS_SlipCustomer).Id);
            issw.ShowDialog();
        }
        public void DataBinding()
        {
            vm_SlipList.GetDataList();
        }
        private void printmenu_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0)
            {
                vm_SlipList.ErrorMessage = "请先选中一个项目！";
            }
            string templale = (LBList.SelectedItem as AMS.Model.AMS_SlipCustomer).SlipTemplate;
            AMS.ViewModel.ViewModelPrintTest print = new AMS.ViewModel.ViewModelPrintTest(SeatManage.EnumType.SeatManageSubsystem.SeatSlip, templale);
            vm_SlipList.ErrorMessage = print.Print();
        }

        private void update_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.SlipCustomerEditWindow scew = new SlipCustomerEditWindow();
            scew.vm_SlipInfo.IsEdit = true;
            scew.vm_SlipInfo.SlipModel=LBList.SelectedItem as AMS.Model.AMS_SlipCustomer;
            scew.vm_SlipInfo.ToXML();
            scew.ShowDialog();
            vm_SlipList.GetDataList();
        }
    }
}
