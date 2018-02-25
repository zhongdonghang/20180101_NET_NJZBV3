using System.Windows;
using System.Windows.Controls;
using SeatClientV3.ViewModel;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_ReadingRoom.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ReadingRoom : UserControl
    {
        public UC_ReadingRoom(ReadingRoomUC_ViewModel viewmodel)
        {
            ViewModel = viewmodel;
            viewmodel.UsageStateChange += viewmodel_UsageStateChange;
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        void viewmodel_UsageStateChange(object sender, System.EventArgs e)
        {
            if (ViewModel.Status == SeatManage.EnumType.ReadingRoomStatus.Close || ViewModel.Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
            {
                nameRount.Style = (Style)this.FindResource("EllipseStyleClose");
                seatcountTxt.Style = (Style)this.FindResource("TextBlockStyleClose");
            }
            else
            {
                switch (ViewModel.Usage)
                {
                    case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                        nameRount.Style = (Style)this.FindResource("EllipseStyleNormal");
                        seatcountTxt.Style = (Style)this.FindResource("TextBlockStyleNormal");
                        break;
                    case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                        nameRount.Style = (Style)this.FindResource("EllipseStyleCrowd");
                        seatcountTxt.Style = (Style)this.FindResource("TextBlockStyleCrowd");
                        break;
                    case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                        nameRount.Style = (Style)this.FindResource("EllipseStyleFull");
                        seatcountTxt.Style = (Style)this.FindResource("TextBlockStyleFull");
                        break;
                }
            }
        }
        public ReadingRoomUC_ViewModel ViewModel;
    }
}
