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

namespace AdvertManageClient.FunPage.SchoolManage
{
    /// <summary>
    /// UC_SchoolInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolInfo : UserControl
    {
         
        public UC_SchoolInfo()
        {
            InitializeComponent();
            this.DataContext = this; 
        }



        public AMS.ViewModel.ViewModelSchoolInfo SchoolsInfo
        {
            get { return (AMS.ViewModel.ViewModelSchoolInfo)GetValue(SchoolsInfoProperty); }
            set { SetValue(SchoolsInfoProperty, value);  }
        }

        // Using a DependencyProperty as the backing store for SchoolsInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolsInfoProperty =
            DependencyProperty.Register("SchoolsInfo", typeof(AMS.ViewModel.ViewModelSchoolInfo), typeof(UC_SchoolInfo), new UIPropertyMetadata(null));

        
         
    }
}
