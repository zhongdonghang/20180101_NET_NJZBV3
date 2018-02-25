using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_SelectSeatConfinmed.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_SelectSeatConfinmed : UserControl
    {
        public UC_Tip_SelectSeatConfinmed(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
