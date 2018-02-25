using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_PrintConfirm.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_PrintConfirm : UserControl
    {
        public UC_Tip_PrintConfirm(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
