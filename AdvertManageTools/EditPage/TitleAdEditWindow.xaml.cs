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
    /// TitleAdEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TitleAdEditWindow : Window
    {
        public TitleAdEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public TitleAdViewModel TitleInfo
        {
            get { return (TitleAdViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("TitleInfo", typeof(TitleAdViewModel), typeof(TitleAdEditWindow));
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (TitleInfo.AddNewTitle())
            {
                MessageBox.Show("发布成功！");
                this.Close();
            }
        }
        /// <summary>
        /// 窗体载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TitleInfo = new TitleAdViewModel();
        }
    }
}
