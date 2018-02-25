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
using System.Data;
using AdvertManageTools.Code;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// CampusEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CampusEditWindow : Window
    {
        public CampusEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;

        }
        public CampusInfoViewModel Campus
        {
            get { return (CampusInfoViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("Campus", typeof(CampusInfoViewModel), typeof(CampusEditWindow));
        private string _SelectSchoolNum;
        /// <summary>
        /// 选中的学校编号
        /// </summary>
        public string SelectSchoolNum
        {
            get { return _SelectSchoolNum; }
            set { _SelectSchoolNum = value; }
        }
    
        private bool _IsEdit = false;
        /// <summary>
        /// 是否为编辑
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; }
        }
        /// <summary>
        /// 下拉框绑定
        /// </summary>
        void CBDataBinding()
        {
                Campus.ComItemGetData();
        }
        /// <summary>
        /// 学校选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbschool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.cbschool.SelectedIndex > -1)
            {
                ComboxStringItem csi = this.cbschool.Items[this.cbschool.SelectedIndex] as ComboxStringItem;
                Campus.Schoolnum = csi.Value;
            }
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_IsEdit)
            {
                cbschool.IsEnabled = false;
            }
            else
            {
                Campus = new CampusInfoViewModel();
            }
            CBDataBinding();
            cbschool.ItemsSource = Campus.ComBoxItems;
            ShowInfo();
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                if (!string.IsNullOrEmpty(txtNo.Text.Trim()) && !string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    if (IsEdit)
                    {
                        if (Campus.UpdateCampus())
                        {
                            MessageBox.Show("校区更新成功！");
                            this.Close();
                        }
                    }
                    else
                    {
                        if (Campus.AddCampus())
                        {
                            MessageBox.Show("校区添加成功！");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("校区编号和名称不能为空！");
                }
            
        }
        void ShowInfo()
        {
            foreach (ComboxStringItem csi in Campus.ComBoxItems)
            {
                if (csi.Value == SelectSchoolNum)
                {
                    cbschool.SelectedItem = csi;
                }
            }
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
