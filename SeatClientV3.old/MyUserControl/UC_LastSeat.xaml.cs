using System;
using System.Collections.Generic;
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

namespace SeatClientV3.MyUserControl
{
	/// <summary>
	/// UC_LastSeat.xaml 的交互逻辑
	/// </summary>
	public partial class UC_LastSeat : UserControl
	{
		public UC_LastSeat()
		{
			InitializeComponent();
            this.DataContext = viewModel;
            viewModel.LastSeatRun();
		}
        public UCViewModel.LastSeatBtn_ViewModel viewModel = new UCViewModel.LastSeatBtn_ViewModel();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SeatClientV3.FunWindow.LastSeatWindow lastSeatWindow = new FunWindow.LastSeatWindow();
            lastSeatWindow.ShowDialog();
        }
	}
}