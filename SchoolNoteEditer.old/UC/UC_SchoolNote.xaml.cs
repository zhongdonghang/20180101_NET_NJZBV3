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

namespace SchoolNoteEditer.UC
{
    /// <summary>
    /// UC_SchoolNote.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolNote : UserControl
    {
        public UC_SchoolNote()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        ViewModel.ViewModel_SchoolNoteList viewModel = new ViewModel.ViewModel_SchoolNoteList();
        private void btn_new_Click(object sender, RoutedEventArgs e)
        {
            FunPage.SchoolNoteEdit editWindow = new FunPage.SchoolNoteEdit();
            editWindow.ShowDialog();
            GetDate();
        }

        private void btn_edit_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Note.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                FunPage.SchoolNoteEdit editWindow = new FunPage.SchoolNoteEdit();
                editWindow.viewModel.NoteModel = LB_Note.SelectedItem as SeatManage.ClassModel.SchoolNoteInfo;
                editWindow.viewModel.IsEdit = true;
                editWindow.ShowDialog();
                GetDate();
            }
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Note.SelectedIndex < 0)
            {
                viewModel.ErrorMessage = "请先选择一项";
            }
            else
            {
                viewModel.NoteDelete(LB_Note.SelectedIndex);
                GetDate();
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        public void GetDate()
        {
            viewModel.GetDate();
        }
    }
}
