using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_ContinueTime.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ContinueTime : UserControl
    {
        public UC_Tip_ContinueTime(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
