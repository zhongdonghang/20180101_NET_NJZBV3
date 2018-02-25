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
using AdvertManageTools.EditPage;
using AdvertManageTools.Code;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// SystemVerInfo.xaml 的交互逻辑
    /// </summary>
    public partial class SystemVerInfo : System.Windows.Controls.UserControl
    {
        SystemVerInfoViewModel vm = new SystemVerInfoViewModel();
        public SystemVerInfo()
        {
            InitializeComponent();
            this.DataContext = vm;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ProgramUpgradeEditWindow editWindow = new ProgramUpgradeEditWindow();
            editWindow.ShowDialog();
        }

        public void DataBind()
        {
            try
            {
                vm.GetProgramList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("执行遇到异常：{0}", ex.Message));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid1.SelectedIndex != -1)
            {
                AdvertManage.Model.ProgramUpgradeModel model = dataGrid1.SelectedItem as AdvertManage.Model.ProgramUpgradeModel;
                IssueCommand issue = new IssueCommand();
                issue.Command = AdvertManage.Model.Enum.CommandType.ProgramUpgrade;
                issue.CommandId = model.Id;
                issue.Show();
            }
        }
    }
}
