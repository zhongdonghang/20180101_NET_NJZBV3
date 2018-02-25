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
    /// UC_Customer.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Customer : UserControl 
    {
        public UC_Customer()
        {
            InitializeComponent();
            this.DataContext = vm_CustomerList;
        }
        AMS.ViewModel.ViewModelCustomerListWindow vm_CustomerList = new AMS.ViewModel.ViewModelCustomerListWindow();
        public void DataBinding()
        {
            vm_CustomerList.GetDataList();
        }

        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            MediaEdit.AddCustomerWindow acw = new AddCustomerWindow();
            acw.vm_CustomerEditWindow.CustomerModel = CustomerLbox.Items[CustomerLbox.SelectedIndex] as AMS.Model.AMS_AdCustomer;
            acw.vm_CustomerEditWindow.IsEdit = true;
            acw.ShowDialog();
            DataBinding();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning,"是否删除这个客户?");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelCustomerEditWindow vmcw = new AMS.ViewModel.ViewModelCustomerEditWindow(CustomerLbox.Items[CustomerLbox.SelectedIndex] as AMS.Model.AMS_AdCustomer);
                vmcw.Delete();
                DataBinding();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MediaEdit.AddCustomerWindow acw = new AddCustomerWindow();
            acw.vm_CustomerEditWindow.IsEdit = false;
            acw.ShowDialog();
            DataBinding();
        } 
    }
}
