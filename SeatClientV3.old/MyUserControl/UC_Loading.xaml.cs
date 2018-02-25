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
using System.Windows.Media.Effects;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Loading.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Loading : UserControl
    {
        public UC_Loading()
        {
            InitializeComponent();
            this.DataContext=viewModel;
            
        }
        ViewModel.LoadingUC_ViewModel viewModel=new ViewModel.LoadingUC_ViewModel();
       
    }
}
