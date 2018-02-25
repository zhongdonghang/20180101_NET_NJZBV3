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
    /// UC_SchoolInfoDetail.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolInfoDetail : UserControl
    {
        public UC_SchoolInfoDetail()
        {
            InitializeComponent();
            this.DataContext = this;
        }


        /// <summary>
        /// 学校详细
        /// </summary>
        public AMS.ViewModel.ViewModelSchoolInfoDetail SchoolDetail
        {
            get { return (AMS.ViewModel.ViewModelSchoolInfoDetail)GetValue(SchoolDetailProperty); }
            set { SetValue(SchoolDetailProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SchoolDetail.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SchoolDetailProperty =
            DependencyProperty.Register("SchoolDetail", typeof(AMS.ViewModel.ViewModelSchoolInfoDetail), typeof(UC_SchoolInfoDetail), new UIPropertyMetadata(null));


    }
}
