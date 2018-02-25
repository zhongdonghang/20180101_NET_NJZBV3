using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_ActivationSuccess.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ActivationSuccess : UserControl
    {
        public UC_Tip_ActivationSuccess(ViewModel.UC_Tip_ViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
