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
using AdvertManageTools.Code;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// DeviceEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceEditWindow : Window
    {
        public DeviceEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public DeviceInfoViewModel Device
        {
            get { return (DeviceInfoViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("Device", typeof(DeviceInfoViewModel), typeof(DeviceEditWindow));

        private bool _IsEdit = false;
        /// <summary>
        /// 是否为编辑
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; }
        }
        private string _SelectSchoolNum;

        /// <summary>
        /// 学校编号
        /// </summary>
        public string SelectSchoolNum
        {
            get { return _SelectSchoolNum; }
            set { _SelectSchoolNum = value; }
        }
        private int _SelectCampusID;
        /// <summary>
        /// 校区id
        /// </summary>
        public int SelectCampusID
        {
            get { return _SelectCampusID; }
            set { _SelectCampusID = value; }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

                if (!string.IsNullOrEmpty(txtNo.Text.Trim()))
                {
                    if (IsEdit)
                    {
                        if (Device.UpdateDevice())
                        {
                            MessageBox.Show("设备更新成功！");
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Device.AddDevice())
                        {
                            MessageBox.Show("设备添加成功！");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("设备编号不能为空！");
                }
            
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 页面载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_IsEdit)
                {
                    cbschool.IsEnabled = false;
                }
                else
                {
                    Device = new DeviceInfoViewModel();
                }

                Device.SchoolComItemGetData();
                cbschool.ItemsSource = Device.SchoolComBoxItems;
                cbcampus.ItemsSource = Device.CampusComBoxItems;
                ShowInfo();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 学校选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbschool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbschool.SelectedIndex > -1)
                {
                    ComboxStringItem csi = cbschool.Items[cbschool.SelectedIndex] as ComboxStringItem;
                    Device.Schoolnumber = csi.Value;
                    Device.CampusComItemGetData(csi.Value);
                    cbcampus.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 校区选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbcampus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbcampus.SelectedIndex > -1)
            {
                ComboxIntItem cii = cbcampus.Items[cbcampus.SelectedIndex] as ComboxIntItem;
                Device.Campusid = cii.Value;
            }
        }
        void ShowInfo()
        {
            foreach (ComboxStringItem csi in Device.SchoolComBoxItems)
            {
                if (csi.Value == SelectSchoolNum)
                {
                    cbschool.SelectedItem = csi;
                }
            }
            foreach (ComboxIntItem csi in Device.CampusComBoxItems)
            {
                if (csi.Value == SelectCampusID)
                {
                    cbcampus.SelectedItem = csi;
                }
            }
        }
    }

}
