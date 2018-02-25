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
    /// ReadingRoom.xaml 的交互逻辑
    /// </summary>
    public partial class ReadingRoom : Window
    {
        public ReadingRoom()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public SchoolNoteEditer.ViewModel.ViewModel_ReadingRoom viewModel = new ViewModel.ViewModel_ReadingRoom();
        public void BindingData()
        {
            viewModel.LibraryCBBinding();
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

        private void libcb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.ReadingRoomModel.Libaray = (libcb.SelectedItem as ViewModel.ViewModel_Library).LibraryModel;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
