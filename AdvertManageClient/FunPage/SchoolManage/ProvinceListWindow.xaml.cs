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

namespace AdvertManageClient.FunPage.SchoolManage
{
    /// <summary>
    /// ProvinceListWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProvinceListWindow : Window
    {
        public ProvinceListWindow()
        {
            InitializeComponent();
            this.DataContext = vm_ProvinceList;

        }
        AMS.ViewModel.ViewModelProvinceListWindow vm_ProvinceList = new AMS.ViewModel.ViewModelProvinceListWindow();
        private void DataBinding()
        {
            vm_ProvinceList.GetDataList();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SchoolManage.AddProvinceWindow apw = new SchoolManage.AddProvinceWindow();
            apw.vm_ProvinceEditWindow.IsEdit = false;
            apw.ShowDialog();
            DataBinding();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void editmenu_Click(object sender, RoutedEventArgs e)
        {
            SchoolManage.AddProvinceWindow apw = new SchoolManage.AddProvinceWindow();
            apw.vm_ProvinceEditWindow.ProvinceModel = LBlsit.Items[LBlsit.SelectedIndex] as AMS.Model.AMS_Province;
            apw.vm_ProvinceEditWindow.IsEdit = true;
            apw.ShowDialog();
            DataBinding();
        }

        private void deletemenu_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxWindow mbw = new MessageBoxWindow();
            mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Warning, "是否删除这个地区？");
            mbw.ShowDialog();
            if (mbw.vm_MessageBoxWindow.Result)
            {
                AMS.ViewModel.ViewModelProvinceEditWindow vmpew = new AMS.ViewModel.ViewModelProvinceEditWindow();
                vmpew.ProvinceModel = LBlsit.Items[LBlsit.SelectedIndex] as AMS.Model.AMS_Province;
                vmpew.Delete();
                DataBinding();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataBinding();
        }
    }
}
