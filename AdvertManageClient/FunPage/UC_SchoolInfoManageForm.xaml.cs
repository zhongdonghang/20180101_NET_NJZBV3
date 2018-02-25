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
using AdvertManageClient.FunPage.SchoolManage;

namespace AdvertManageClient.FunPage
{
    /// <summary>
    /// UC_SchoolInfoManageForm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolInfoManageForm : UserControl
    {

        public UC_SchoolInfoManageForm()
        {
            InitializeComponent();
            SchoolMainWindow = new AMS.ViewModel.ViewModelSchoolMainWindow();
            this.DataContext = this;
        }



        public AMS.ViewModel.ViewModelSchoolMainWindow SchoolMainWindow
        {
            get { return (AMS.ViewModel.ViewModelSchoolMainWindow)GetValue(SchoolMainWindowProperty); }
            set { SetValue(SchoolMainWindowProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SchoolMainWindow.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolMainWindowProperty =
            DependencyProperty.Register("SchoolMainWindow", typeof(AMS.ViewModel.ViewModelSchoolMainWindow), typeof(UC_SchoolInfoManageForm), new UIPropertyMetadata(null));



        public void DataBind()
        {
            SchoolMainWindow.GetSchoolList();
            uc_schoolInfo.SchoolsInfo = SchoolMainWindow.SchoolInfo;
            uc_schoolDetail.SchoolDetail = SchoolMainWindow.SchoolInfoDetail;
            uc_CampusInfoDetail.CampusInfoDetail = SchoolMainWindow.CampusInfoDetail;
        }

        private void AddProvincebtn_Click(object sender, RoutedEventArgs e)
        {
            SchoolManage.ProvinceListWindow plw = new SchoolManage.ProvinceListWindow();
            plw.ShowDialog();
            DataBind();
        }

        private void AddSchoolbtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void treeView1_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeView sender1 = sender as TreeView;
            if (sender1 != null)
            {
                NodeEntry node = sender1.SelectedItem as NodeEntry;
                SchoolMainWindow.TreeViewSelectedItemHandle(node);
            }
        }

        private void TreeViewItem_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var treeViewItem = VisualUpwardSearch<TreeViewItem>(e.OriginalSource as DependencyObject) as TreeViewItem;
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        static DependencyObject VisualUpwardSearch<T>(DependencyObject source)
        {
            while (source != null && source.GetType() != typeof(T))
                source = VisualTreeHelper.GetParent(source);
            return source;
        }

        private void MenuItem_Click_Edit(object sender, RoutedEventArgs e)
        {
            NodeEntry node = treeView1.SelectedItem as NodeEntry;
            if (node != null)
            {
                switch (node.Type)
                {
                    case NodeType.School:
                        SchoolInfoWindow schoolWindow = new SchoolInfoWindow();
                        schoolWindow.ViewModelSchoolEdit = new ViewModelSchoolEditWindow(AMS.ViewModel.Enum.HandleType.Edit);
                        schoolWindow.ViewModelSchoolEdit.SchoolModelInfo = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node);
                        schoolWindow.ViewModelSchoolEdit.Province = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_ProvinceSchoolInfo>(node.FatherNode);
                        schoolWindow.ShowDialog();
                        if (schoolWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                    case NodeType.Campus:
                        CampusInfoWindow campusWindow = new CampusInfoWindow();
                        campusWindow.ViewModelCampusWindow = new ViewModelCampusEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node.FatherNode));
                        campusWindow.ViewModelCampusWindow.CampusModel = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Campus>(node);
                        campusWindow.ViewModelCampusWindow.Cmd = AMS.ViewModel.Enum.HandleType.Edit;
                        campusWindow.ShowDialog();
                        if (campusWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                    case NodeType.Device:
                        DeviceInfoWindow deviceWindow = new DeviceInfoWindow();
                        deviceWindow.ViewModelDevice = new ViewModelDeviceEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Campus>(node.FatherNode), this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node.FatherNode.FatherNode));
                        deviceWindow.ViewModelDevice.DeviceModel = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Device>(node);
                        deviceWindow.ViewModelDevice.Cmd = AMS.ViewModel.Enum.HandleType.Edit;
                        deviceWindow.ShowDialog();
                        if (deviceWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                }
            }
        }

        private void MenuItem_Click_Delete(object sender, RoutedEventArgs e)
        {
            NodeEntry node = treeView1.SelectedItem as NodeEntry;
            if (node != null)
            {
                switch (node.Type)
                {
                    case NodeType.School:
                        SchoolInfoWindow schoolWindow = new SchoolInfoWindow();
                        schoolWindow.ViewModelSchoolEdit = new ViewModelSchoolEditWindow(AMS.ViewModel.Enum.HandleType.Delete);
                        schoolWindow.ViewModelSchoolEdit.SchoolModelInfo = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node);
                        schoolWindow.DeleteSchoolInfo();
                        if (schoolWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                    case NodeType.Campus:
                        CampusInfoWindow campusWindow = new CampusInfoWindow();
                        campusWindow.ViewModelCampusWindow = new ViewModelCampusEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node.FatherNode));
                        campusWindow.ViewModelCampusWindow.CampusModel = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Campus>(node);
                        campusWindow.ViewModelCampusWindow.Cmd = AMS.ViewModel.Enum.HandleType.Delete;
                        campusWindow.DeleteCampusWindow();
                        if (campusWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;

                    case NodeType.Device:
                         DeviceInfoWindow deviceWindow = new DeviceInfoWindow();
                         deviceWindow.ViewModelDevice = new ViewModelDeviceEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Campus>(node.FatherNode), this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node.FatherNode.FatherNode));
                        deviceWindow.ViewModelDevice.DeviceModel = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Device>(node);
                        deviceWindow.ViewModelDevice.Cmd = AMS.ViewModel.Enum.HandleType.Delete;
                        deviceWindow.DeleteDevice();
                        if (deviceWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                }
            }
        }

        private void MenuItem_Click_Add(object sender, RoutedEventArgs e)
        {
            NodeEntry node = treeView1.SelectedItem as NodeEntry;
            if (node != null)
            {
                switch (node.Type)
                {
                    case NodeType.Province://选中省份，添加学校信息
                        SchoolInfoWindow schoolWindow = new SchoolInfoWindow();
                        schoolWindow.ViewModelSchoolEdit = new ViewModelSchoolEditWindow(AMS.ViewModel.Enum.HandleType.Add);
                        schoolWindow.ViewModelSchoolEdit.Province = this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_ProvinceSchoolInfo>(node);
                        schoolWindow.ShowDialog();
                        if (schoolWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                    case NodeType.School://选中学校，添加 校区信息
                        CampusInfoWindow campusWindow = new CampusInfoWindow();
                        campusWindow.ViewModelCampusWindow = new ViewModelCampusEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node));
                        campusWindow.ViewModelCampusWindow.Cmd = AMS.ViewModel.Enum.HandleType.Add;
                        campusWindow.ShowDialog();
                        if (campusWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;
                    case NodeType.Campus:
                        DeviceInfoWindow deviceWindow = new DeviceInfoWindow();
                        deviceWindow.ViewModelDevice = new ViewModelDeviceEditWindow(this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_Campus>(node), this.SchoolMainWindow.GetSelectNodeInfo<AMS.Model.AMS_School>(node.FatherNode));
                        deviceWindow.ViewModelDevice.Cmd = AMS.ViewModel.Enum.HandleType.Add;
                        deviceWindow.ShowDialog();
                        if (deviceWindow.IsSuccess)
                        {
                            DataBind();
                        }
                        break;

                }
            }
        }

        private void DerviceStatusbtn_Click(object sender, RoutedEventArgs e)
        {
            FunPage.SyatemManage.IssuedSchoolSelectWindow issue = new SyatemManage.IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.Caputre,0);
            issue.ShowDialog();
        }
    }
}
