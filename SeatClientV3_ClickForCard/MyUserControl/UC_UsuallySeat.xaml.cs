using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_UsuallySeat.xaml 的交互逻辑
    /// </summary>
    public partial class UC_UsuallySeat : UserControl
    {
        public UC_UsuallySeat(ViewModel.UsuallySeatUC_ViewModel Model)
        {
            InitializeComponent();
            this.DataContext = Model;
            viewModel = Model;
        }
        public ViewModel.UsuallySeatUC_ViewModel viewModel;
    }
}
