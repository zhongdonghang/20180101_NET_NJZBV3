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

namespace SchoolNoteEditer.FunPage
{
    /// <summary>
    /// Library.xaml 的交互逻辑
    /// </summary>
    public partial class Library : Window
    {
        public Library()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public SchoolNoteEditer.ViewModel.ViewModel_Library viewModel = new ViewModel.ViewModel_Library();
        public void BindingData()
        {
            viewModel.ScholCBBinding();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Save();
            if (viewModel.ErrorMessage == "")
            {
                this.Close();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.LibraryModel.School = (schoolcb.SelectedItem as ViewModel.ViewModel_School).SchoolModel;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
