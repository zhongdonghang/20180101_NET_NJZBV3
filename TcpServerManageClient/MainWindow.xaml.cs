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

namespace TcpServerManageClient
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ViewModelMainForm vm = new ViewModelMainForm();
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = vm;
            vm.Init();
            vm.Start();
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            //CheckBox cb = sender as CheckBox;
            //if (cb.IsChecked.Value)
            //{
            //    vm.Start();
            //}
            //else
            //{ 

            //}
        }
         
        
    }
}
