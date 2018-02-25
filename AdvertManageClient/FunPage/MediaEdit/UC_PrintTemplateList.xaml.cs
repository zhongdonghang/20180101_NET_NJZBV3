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
    /// UC_PrintTemplateList.xaml 的交互逻辑
    /// </summary>
    public partial class UC_PrintTemplateList : UserControl
    {
        public UC_PrintTemplateList()
        {
            InitializeComponent();
            this.DataContext = vm_PrintList;
        }
        public AMS.ViewModel.ViewModelPrintTemplateList vm_PrintList = new AMS.ViewModel.ViewModelPrintTemplateList();
        private void resslip_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0)
            {
                vm_PrintList.ErrorMessage = "请选择需要下发的凭条！";
                return;
            }
            FunPage.SyatemManage.IssuedSchoolSelectWindow issw = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.PrintTemplate, (LBList.SelectedItem as AMS.Model.AMS_PrintTemplate).Id);
            issw.ShowDialog();
        }

        private void addslip_Click(object sender, RoutedEventArgs e)
        {
            FunPage.MediaEdit.PrintTemplateEditWindow ptew = new PrintTemplateEditWindow();
            ptew.vm_PrintTemplate.IsEdit = false;
            ptew.ShowDialog();
            vm_PrintList.GetDataList();
        }
        public void DataBinding()
        {
            vm_PrintList.GetDataList();
        }

        private void printmenu_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0)
            {
                vm_PrintList.ErrorMessage = "请先选中一个项目！";
            }
            string templale = (LBList.SelectedItem as AMS.Model.AMS_PrintTemplate).Template;
            AMS.ViewModel.ViewModelPrintTest print = new AMS.ViewModel.ViewModelPrintTest(SeatManage.EnumType.SeatManageSubsystem.SeatSlip, templale);
            vm_PrintList.ErrorMessage = print.Print();
        }

        private void revamp_Click(object sender, RoutedEventArgs e)
        {
            if (LBList.SelectedIndex < 0)
            {
                vm_PrintList.ErrorMessage = "请先选中一个项目！";
            }

            FunPage.MediaEdit.PrintTemplateEditWindow print = print = new PrintTemplateEditWindow();
            print.vm_PrintTemplate.IsEdit = true;
            print.vm_PrintTemplate.PrintModel = LBList.SelectedItem as AMS.Model.AMS_PrintTemplate;
            print.vm_PrintTemplate.ToListXML();
            print.ShowDialog();
            vm_PrintList.GetDataList();
        }
    }
}
