using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeatClientV2.ViewModel;

namespace SeatClientV2.MyUserControl
{
    /// <summary>
    /// UC_ReadingRoom.xaml 的交互逻辑
    /// </summary>
    public partial class UC_ReadingRoom : UserControl
    {
        public UC_ReadingRoom(ReadingRoomUC_ViewModel viewmodel)
        {
            viewModel = viewmodel;
            InitializeComponent();
            this.DataContext = viewModel;
        }
        public ReadingRoomUC_ViewModel viewModel;
    }
}
