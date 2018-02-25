using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_WaitSeatFrequent.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_WaitSeatFrequent : UserControl
    {
        public UC_Tip_WaitSeatFrequent(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
