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
    /// UC_SystemManageForm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SystemManageForm : UserControl
    {
        public UC_SystemManageForm()
        {
            InitializeComponent();
        }

        private void VerManagebtn_Click(object sender, RoutedEventArgs e)
        {
            UC_FileSharing.Visibility = System.Windows.Visibility.Collapsed;
            UC_ProjectVersion.Visibility = System.Windows.Visibility.Visible;
            UC_ProjectVersion.DataBinding();
        }

        private void FilesSharebtn_Click(object sender, RoutedEventArgs e)
        {
            UC_ProjectVersion.Visibility = System.Windows.Visibility.Collapsed;
            UC_FileSharing.Visibility = System.Windows.Visibility.Visible;
            UC_FileSharing.DataBinding();
        }
    }
}
