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

namespace AdvertManageClient.FunPage
{
    /// <summary>
    /// UC_AdvertVideoManageForm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_AdvertVideoManageForm : UserControl
    {
        public UC_AdvertVideoManageForm()
        {
            InitializeComponent();
        }

        private void CustomerManagebtn_Click(object sender, RoutedEventArgs e)
        {
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_Customer.Visibility = System.Windows.Visibility.Visible;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_Customer.DataBinding();
        }

        private void Hardadbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Visible;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.DataBinging();
        }

        private void TiliteADbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Visible;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.DataBinding();
        }

        private void Playlistbtn_Click_4(object sender, RoutedEventArgs e)
        {
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Visible;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.DataBinding();
        }

        private void Slipcustomerbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Visible;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_SlipCustomerList.DataBinding();
        }

        private void PrintSlipbtn_Click(object sender, RoutedEventArgs e)
        {
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Visible;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.DataBinding();
        }

        private void RollTitles_Click(object sender, RoutedEventArgs e)
        {
            UC_Customer.Visibility = System.Windows.Visibility.Collapsed;
            UC_HardAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_TitleAd.Visibility = System.Windows.Visibility.Collapsed;
            UC_PlaylistInfo.Visibility = System.Windows.Visibility.Collapsed;
            UC_SlipCustomerList.Visibility = System.Windows.Visibility.Collapsed;
            UC_PrintTemplateList.Visibility = System.Windows.Visibility.Collapsed;
            UC_RollTitlesList.Visibility = System.Windows.Visibility.Visible;
            UC_RollTitlesList.DataBinding();
        }
    }
}
