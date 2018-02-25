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
using AdvertManageTools.Code;

namespace AdvertManageTools.UserControl
{
    /// <summary>
    /// ReleaseLog.xaml 的交互逻辑
    /// </summary>
    public partial class ReleaseLog : System.Windows.Controls.UserControl
    {
         ReleaseLogViewModel ViewModel = new ReleaseLogViewModel();
        public ReleaseLog()
        {
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        private void button8_Click(object sender, RoutedEventArgs e)
        {
           CommandTypeItem cmdType = comboBox_SelectType.SelectedItem as CommandTypeItem;
            CommandHandleResultItem result = comboBox3.SelectedItem as CommandHandleResultItem;
            AdvertManage.Model.AMS_SchoolModel school = cbxSchool.SelectedItem as AdvertManage.Model.AMS_SchoolModel ;
            ViewModel.BindCommandModel(school.Id, cmdType.TypeItem, result.HandleResult);
        }

        public void DataBind()
        {
            ViewModel.BindSchool();
            cbxSchool.SelectedIndex = 0;
            ViewModel.BindCommandTypeItem();
            comboBox_SelectType.SelectedIndex = 0;
            ViewModel.BindCommandHandleResultItem();
            comboBox3.SelectedIndex = 0;
        }
    }
}
