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
using AMS.ViewModel;
using System.Data;

namespace AdvertManageClient.FunPage.SchoolManage
{
    /// <summary>
    /// UC_CampusInfoDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UC_CampusInfoDetail : UserControl
    {
        public UC_CampusInfoDetail()
        {
            InitializeComponent();
            CampusInfoDetail = new ViewModelCampusInfoDetail();
            this.DataContext = this;
        } 
        public ViewModelCampusInfoDetail CampusInfoDetail
        {
            get { return (ViewModelCampusInfoDetail)GetValue(CampusInfoDetailProperty); }
            set { SetValue(CampusInfoDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DeviceInfoDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CampusInfoDetailProperty =
            DependencyProperty.Register("CampusInfoDetail", typeof(ViewModelCampusInfoDetail), typeof(UC_CampusInfoDetail), new UIPropertyMetadata(null));

        private void btnLook_Click(object sender, RoutedEventArgs e)
        {
            DeviceImageWindow device = new DeviceImageWindow();
            listView.SelectedItem = ((Button)sender).DataContext;
            device.Device = (listView.SelectedItem) as AMS.ViewModel.DeviceInfo;
            device.ShowDialog();
        }
    }
}
