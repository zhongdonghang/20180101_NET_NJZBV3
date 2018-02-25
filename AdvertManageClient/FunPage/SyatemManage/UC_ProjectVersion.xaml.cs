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

namespace AdvertManageClient.FunPage.SyatemManage
{
    /// <summary>
    /// UC_ProjectVersion.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ProjectVersion : UserControl
    {
        public UC_ProjectVersion()
        {
            InitializeComponent();
            this.DataContext = vm_pv;
        }

        public AMS.ViewModel.ViewModelProjectVersionWindow vm_pv = new AMS.ViewModel.ViewModelProjectVersionWindow();

        private void btnReleaseNew_Click(object sender, RoutedEventArgs e)
        {
            SyatemManage.ProjectVersionEditWindow pvEditWindow = new ProjectVersionEditWindow();
            pvEditWindow.ShowDialog();
        }

        private void btnIssued_Click(object sender, RoutedEventArgs e)
        {
            if (lstProjectVersionList.SelectedIndex < 0)
            {
                vm_pv.ErrorMessage = "请选择要下发的版本！";
                return;
            }
            AMS.Model.ProgramUpgrade model = lstProjectVersionList.SelectedItem as AMS.Model.ProgramUpgrade;
            SyatemManage.IssuedSchoolSelectWindow iss = new IssuedSchoolSelectWindow(AMS.Model.Enum.CommandType.ProgramUpgrade,model.Id);
            iss.ShowDialog();
        }

        public void DataBinding()
        {
            vm_pv.GetDataList();
        }
    }
}
