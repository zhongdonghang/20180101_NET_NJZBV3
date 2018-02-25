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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// RoolTitlesEdit.xaml 的交互逻辑
    /// </summary>
    public partial class RoolTitlesEdit : Window
    {
        public RoolTitlesEdit()
        {
            InitializeComponent();
            vm_SlipList.CustomerList.GetDataList();
            vm_SlipList.CustomerList.CustomerInfoList.Insert(0, new AMS.Model.AMS_AdCustomer { ID = -1, CompanyName = "请选择" });
            ccb.SelectedIndex = 0;
            this.DataContext = vm_SlipList;
        }
        public AMS.ViewModel.ViewModelRollTitlesEditWindow vm_SlipList = new AMS.ViewModel.ViewModelRollTitlesEditWindow();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (vm_SlipList.Save())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "操作成功!");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vm_SlipList.IsEdit)
            {
                ccb.SelectedIndex = vm_SlipList.CustomerId;
            }
            else
            {
                vm_SlipList.CustomerId = (ccb.SelectedItem as AMS.Model.AMS_AdCustomer).ID;
            }

        }
    }
}
