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
    /// SchoolEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SchoolEditWindow : Window
    {
        public SchoolEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        public SchoolInfoViewModel School
        {
            get { return (SchoolInfoViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("School", typeof(SchoolInfoViewModel), typeof(SchoolEditWindow));

        private bool _IsEdit = false;
        /// <summary>
        /// 判断是否是编辑模式
        /// </summary>
        public bool IsEdit
        {
            get { return _IsEdit; }
            set { _IsEdit = value; }
        }
        /// <summary>
        /// 编辑的学校
        /// </summary>
        private void bitClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
                if (!string.IsNullOrEmpty(txtNo.Text.Trim()) && !string.IsNullOrEmpty(txtName.Text.Trim()))
                {
                    if (IsEdit)
                    {
                        if (School.UpdateSchool())
                        {
                            MessageBox.Show("学校更新成功！");
                            this.Close();
                        }
                    }
                    else
                    {
                        if (School.AddSchool())
                        {
                            MessageBox.Show("学校添加成功！");
                            this.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("学校编号和名称不能为空！");
                }
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!_IsEdit)
            {
                School = new SchoolInfoViewModel();
            }
        }
    }
}
