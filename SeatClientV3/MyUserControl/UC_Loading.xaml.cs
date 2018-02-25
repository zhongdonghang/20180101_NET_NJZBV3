using System.Windows.Controls;

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
