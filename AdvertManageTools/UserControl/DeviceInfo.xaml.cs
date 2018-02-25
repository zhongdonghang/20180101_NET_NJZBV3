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
using AdvertManageTools.Code;
using AdvertManageTools.EditPage;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// SchoolInfo.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceInfo : System.Windows.Controls.UserControl
    {
        public DeviceInfo()
        {
            InitializeComponent();
            this.DataContext = this;
            cbschool.ItemsSource = Deviceinfo.SchoolComBoxItems;
            cbcampus.ItemsSource = Deviceinfo.CampusComBoxItems;
            DevicedataGrid.ItemsSource = Deviceinfo.DeviceList;
        }
        DeviceInfoListViewModel Deviceinfo = new DeviceInfoListViewModel();
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        public void ComboxBingding()
        {
                cbschool.SelectedIndex = 0;
                Deviceinfo.SchoolComItemGetData();
                ComboxStringItem cii = cbschool.Items[cbschool.SelectedIndex] as ComboxStringItem;
                Deviceinfo.CampusComItemGetData(cii.Value);
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void DataBinding()
        {
                ComboxStringItem cii = cbschool.Items[cbschool.SelectedIndex] as ComboxStringItem;
                ComboxStringItem csi = cbcampus.Items[cbcampus.SelectedIndex] as ComboxStringItem;
                Deviceinfo.DateGet(cii.Value, csi.Value);

        }
        /// <summary>
        /// 学校选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbschool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

                ComboxStringItem cii = cbschool.Items[cbschool.SelectedIndex] as ComboxStringItem;
                cbcampus.SelectedIndex = 0;
                Deviceinfo.CampusComItemGetData(cii.Value);
                DataBinding();
        }
        /// <summary>
        /// 校区选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbcampus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataBinding();
        }
        /// <summary>
        /// 添加设备
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddDevice_Click(object sender, RoutedEventArgs e)
        {
            DeviceEditWindow dew = new DeviceEditWindow();
            dew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bitDeviceEdit_Click(object sender, RoutedEventArgs e)
        {
            DeviceInfoViewModel editdevice = this.DevicedataGrid.Items[DevicedataGrid.SelectedIndex] as DeviceInfoViewModel;
            DeviceEditWindow dew = new DeviceEditWindow();
            dew.IsEdit = true;
            dew.Device = editdevice;
            dew.SelectSchoolNum = editdevice.Schoolnumber;
            dew.SelectCampusID = editdevice.Campusid;
            dew.ShowDialog();
            DataBinding();
        }
        /// <summary>
        /// 查看截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            DeviceInfoViewModel editdevice = this.DevicedataGrid.Items[DevicedataGrid.SelectedIndex] as DeviceInfoViewModel;
            DeviceImageWindow diw = new DeviceImageWindow();
            diw.Device = editdevice;
            diw.ShowDialog();
            
        }
        /// <summary>
        /// 获取截图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GetDeviceImage_Click(object sender, RoutedEventArgs e)
        {
            IssueCommand ic = new IssueCommand();
            ic.Command = AdvertManage.Model.Enum.CommandType.Caputre;
            ic.ShowDialog();
        }
    }
}
