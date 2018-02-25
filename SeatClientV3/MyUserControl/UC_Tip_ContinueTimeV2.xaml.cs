using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_ContinueTime.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ContinueTimeV2 : UserControl
    {
        public UC_Tip_ContinueTimeV2(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
