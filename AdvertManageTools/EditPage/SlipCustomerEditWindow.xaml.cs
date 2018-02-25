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
using Microsoft.Win32;
using AdvertManageTools.Code;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// SlipCustomerEditWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SlipCustomerEditWindow : Window
    {
        public SlipCustomerEditWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }
        public SlipCustomerInfoViewModel SlipInfo
        {
            get { return (SlipCustomerInfoViewModel)GetValue(SchoolProperty); }
            set { SetValue(SchoolProperty, value); }
        }

        // Using a DependencyProperty as the backing store for School.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolProperty =
            DependencyProperty.Register("SlipInfo", typeof(SlipCustomerInfoViewModel), typeof(SlipCustomerEditWindow));
        /// <summary>
        /// 添加一项优惠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ADSCItem_Click(object sender, RoutedEventArgs e)
        {
            SlipInfo.SlipTemplate.SCitems.Add(new SlipCustomerItemViewModel());
        }

        /// <summary>
        /// 添加logo图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectLogoImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            //this.textBox5.Text = ofd.FileName;
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                BitmapImage newBitmapImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                SlipInfo.CustomerImage = newBitmapImage;
            }
        }
        /// <summary>
        /// 添加店面图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectShopImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            //this.textBox5.Text = ofd.FileName;
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                BitmapImage newBitmapImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                SlipInfo.ImageUrl = newBitmapImage;
            }
        }
        /// <summary>
        /// 添加优惠劵logo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectSTLogoImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;
            ofd.Filter = "图片文件|*.jpg;*.bmp;*.jpeg;*.png;";
            ofd.ShowDialog();
            //this.textBox5.Text = ofd.FileName;
            if (!string.IsNullOrEmpty(ofd.FileName))
            {
                BitmapImage newBitmapImage = new BitmapImage(new Uri(ofd.FileName, UriKind.RelativeOrAbsolute));
                SlipInfo.SlipTemplate.LogoImage = newBitmapImage;
            }
        }
        /// <summary>
        /// 日期选择时判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EndDate.SelectedDate != null && EndDate.SelectedDate < EffectDate.SelectedDate)
            {
                MessageBox.Show("起始日期不能大于结束日期！");
                EffectDate.SelectedDate = null;
            }
        }
        /// <summary>
        /// 日期选择时判断
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EffectDate.SelectedDate != null && EffectDate.SelectedDate > EndDate.SelectedDate)
            {
                MessageBox.Show("结束日期不能小于起始日期！");
                EndDate.SelectedDate = null;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

            if (SlipInfo.AddSC())
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
            SlipInfo = new SlipCustomerInfoViewModel();
            SClist.ItemsSource = SlipInfo.SlipTemplate.SCitems;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (SClist.SelectedIndex > -1)
            {
                MessageBoxResult endResult;
                endResult = MessageBox.Show("确认要删除此条优惠信息吗？", "删除提示", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (endResult == MessageBoxResult.Yes)
                {
                    SlipInfo.SlipTemplate.SCitems.RemoveAt(SClist.SelectedIndex);
                }
            }
        }
        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (SClist.SelectedIndex > 0)
            {
                SlipInfo.SlipTemplate.SCitems.Move(SClist.SelectedIndex, SClist.SelectedIndex - 1);
            }
        }
        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (SClist.SelectedIndex > -1 && SClist.SelectedIndex < SClist.Items.Count - 1)
            {
                SlipInfo.SlipTemplate.SCitems.Move(SClist.SelectedIndex, SClist.SelectedIndex + 1);
            }
        }
    }
}
