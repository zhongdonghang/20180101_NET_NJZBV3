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

namespace AdvertManageClient.FunPage.MediaEdit
{
    /// <summary>
    /// PreviewHardAdWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PreviewHardAdWindow : Window
    {
        public PreviewHardAdWindow()
        {
            InitializeComponent();
        }
        public AMS.ViewModel.ViewModelAdHardEditWindow vm_HardAdEditWindow = new AMS.ViewModel.ViewModelAdHardEditWindow();

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
