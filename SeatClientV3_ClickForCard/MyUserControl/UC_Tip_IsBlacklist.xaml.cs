using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_IsBlacklist.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_IsBlacklist : UserControl
    {
        public UC_Tip_IsBlacklist(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
