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

namespace AdvertManageClient.FunPage.NewMediaState
{
    /// <summary>
    /// W_IssuedSchoolList.xaml 的交互逻辑
    /// </summary>
    public partial class W_IssuedSchoolList : Window
    {
        public W_IssuedSchoolList()
        {
            InitializeComponent();
            viewModel.GetSchoolList();
            this.DataContext = viewModel;
            
        }
        public AMS.ViewModel.ViewModel_IssureCommand viewModel = new AMS.ViewModel.ViewModel_IssureCommand();

        private void btn_Submit_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.Issued())
            {
                MessageBoxWindow mbw = new MessageBoxWindow();
                mbw.vm_MessageBoxWindow = new AMS.ViewModel.ViewModelMessageBoxWindow(AMS.Model.Enum.MessageBoxType.Success, "下发成功！");
                mbw.ShowDialog();
                this.Close();
            }
        }

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
