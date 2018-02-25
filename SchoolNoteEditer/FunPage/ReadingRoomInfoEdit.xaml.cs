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
    /// ReadingRoomInfoEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ReadingRoomInfoEdit : Window
    {
        public ReadingRoomInfoEdit()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        SchoolNoteEditer.ViewModel.ViewModel_ReadingRoomInfoEdit viewModel = new ViewModel.ViewModel_ReadingRoomInfoEdit();
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void BindingData()
        {
            viewModel.GetData();
        }
        /// <summary>
        /// 添加校区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addschool_Click(object sender, RoutedEventArgs e)
        {
            School s = new School();
            s.ShowDialog();
            BindingData();
        }
        /// <summary>
        /// 编辑校区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editschool_Click(object sender, RoutedEventArgs e)
        {
            if (schoolbox.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个项目！";
            }
            else
            {
                School s = new School();
                s.viewModel.SchoolModel = (schoolbox.SelectedItem as ViewModel.ViewModel_School).SchoolModel;
                s.viewModel.IsEdit = true;
                s.ShowDialog();
                BindingData();
            }
        }
        /// <summary>
        /// 删除校区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteschool_Click(object sender, RoutedEventArgs e)
        {
            if (schoolbox.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一个项目！";
            }
            else
            {
                if (MessageBox.Show("是否确认删除？", "删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ViewModel.ViewModel_School vm = schoolbox.SelectedItem as ViewModel.ViewModel_School;
                    vm.Delete();
                    if (vm.ErrorMessage == "")
                    {
                        BindingData();
                    }
                    else
                    {
                        viewModel.ErrorMessage = vm.ErrorMessage;
                    }
                }
            }
        }
        /// <summary>
        /// 添加图书馆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addlibaray_Click(object sender, RoutedEventArgs e)
        {
            Library l = new Library();
            l.BindingData();
            l.ShowDialog();
            BindingData();
        }
        /// <summary>
        /// 编辑图书馆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editlib_Click(object sender, RoutedEventArgs e)
        {
            if (libbox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个项目！");
            }
            else
            {
                Library l = new Library();
                l.viewModel.LibraryModel = (libbox.SelectedItem as ViewModel.ViewModel_Library).LibraryModel;
                l.viewModel.IsEdit = true;
                l.BindingData();
                l.ShowDialog();
                BindingData();
            }
        }
        /// <summary>
        /// 删除图书馆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletlib_Click(object sender, RoutedEventArgs e)
        {
            if (libbox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个项目！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？", "删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ViewModel.ViewModel_Library vm = libbox.SelectedItem as ViewModel.ViewModel_Library;
                    vm.Delete();
                    if (vm.ErrorMessage == "")
                    {
                        BindingData();
                    }
                    else
                    {
                        viewModel.ErrorMessage = vm.ErrorMessage;
                    }
                }
            }
        }
        /// <summary>
        /// 添加阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void addroom_Click(object sender, RoutedEventArgs e)
        {
            ReadingRoom r = new ReadingRoom();
            r.BindingData();
            r.ShowDialog();
            BindingData();
        }
        /// <summary>
        /// 编辑阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void editroom_Click(object sender, RoutedEventArgs e)
        {
            if (roombox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个项目！");
            }
            else
            {
                ReadingRoom r = new ReadingRoom();
                r.viewModel.ReadingRoomModel = (roombox.SelectedItem as ViewModel.ViewModel_ReadingRoom).ReadingRoomModel;
                r.viewModel.IsEdit = true;
                r.BindingData();
                r.ShowDialog();
                BindingData();
            }
        }
        /// <summary>
        /// 删除阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletroom_Click(object sender, RoutedEventArgs e)
        {
            if (roombox.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择一个项目！");
            }
            else
            {
                if (MessageBox.Show("是否确认删除？", "删除", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    ViewModel.ViewModel_ReadingRoom vm = roombox.SelectedItem as ViewModel.ViewModel_ReadingRoom;
                    vm.Delete();
                    if (vm.ErrorMessage == "")
                    {
                        BindingData();
                    }
                    else
                    {
                        viewModel.ErrorMessage = vm.ErrorMessage;
                    }
                }
            }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void IntButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveLayout();
        }
    }
}
