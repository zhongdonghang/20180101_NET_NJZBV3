using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_ComeBack.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ComeBack : UserControl
    {
        public UC_Tip_ComeBack(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
