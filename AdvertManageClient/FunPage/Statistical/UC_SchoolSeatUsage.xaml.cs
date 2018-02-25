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

namespace AdvertManageClient.FunPage.Statistical
{
    /// <summary>
    /// UC_SchoolSeatUsage.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolSeatUsage : UserControl
    {
        public UC_SchoolSeatUsage()
        {
            InitializeComponent();
            viewModel.BindSchoolList();
            this.DataContext = viewModel;
        }
        AMS.ViewModel.ViewModel_SchoolSeatUsage viewModel = new AMS.ViewModel.ViewModel_SchoolSeatUsage();
        private void CB_School_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            viewModel.UsageModel.SchoolNum = (CB_School.SelectedItem as AMS.Model.AMS_School).Number;
        }

        private void btn_GetData_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ErrorMessage = "";
            viewModel.GetDate();
        }

        private void btn_SentComand_Click(object sender, RoutedEventArgs e)
        {
            NewMediaState.W_IssuedSchoolList w_IisureList = new NewMediaState.W_IssuedSchoolList();
            w_IisureList.viewModel.Command = AMS.Model.Enum.IsureCommandType.EnterOutLog;
            w_IisureList.ShowDialog();
        }
    }
}
