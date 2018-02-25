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

namespace SchoolNoteEditer
{
    /// <summary>
    /// CheckPasswordWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CheckPasswordWindow : Window
    {
        public CheckPasswordWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public ViewModel.ViewModel_CheckPassword viewModel = new ViewModel.ViewModel_CheckPassword();
        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Check(txt_UserName.Text, txt_Password.Password))
            {
                MainWindow mw = new MainWindow();
                App.IsJuneberry = viewModel.IsJuneberry;
                mw.Show();
                this.Close();
            }
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
