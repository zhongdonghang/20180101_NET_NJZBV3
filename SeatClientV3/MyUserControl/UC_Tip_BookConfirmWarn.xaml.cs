using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_BookConfirmWarn.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_BookConfirmWarn : UserControl
    {
        public UC_Tip_BookConfirmWarn(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
