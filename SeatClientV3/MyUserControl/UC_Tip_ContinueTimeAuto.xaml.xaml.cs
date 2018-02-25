using System.Windows.Controls;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_Tip_ContinueTimeAuto.xaml 的交互逻辑
    /// </summary>
    public partial class UC_Tip_ContinueTimeAuto : UserControl
    {
        public UC_Tip_ContinueTimeAuto(ViewModel.UC_Tip_ViewModel model)
        {
            InitializeComponent();
            this.DataContext = model;
        }
    }
}
