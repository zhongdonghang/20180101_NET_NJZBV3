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
using Microsoft.Win32;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// HardAdEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HardAdEditWindow : Window
    {
        public HardAdEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public HardAdViewModel HardVm
        {
            get { return (HardAdViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("HardVm", typeof(HardAdViewModel), typeof(HardAdEditWindow));
        /// <summary>
        /// 选择图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png";
            ofd.ShowDialog();
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                BitmapImage newBitmapImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                HardVm.AdImage = newBitmapImage;
            }
        }
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (HardVm.AddNewHardAd())
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
            HardVm = new HardAdViewModel();
        }

    }
}
