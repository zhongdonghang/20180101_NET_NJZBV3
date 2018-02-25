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
using System.Windows.Shapes;

namespace SeatClientV2
{
    /// <summary>
    /// ReaderNoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReaderNoteWindow : Window
    {
        ViewModel.ReaderNoteWindow_ViewModel viewModel = new ViewModel.ReaderNoteWindow_ViewModel();
        public ReaderNoteWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            BindingData();
            viewModel.CloseTime = 10;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
        }
        private void BindingData()
        {
            this.Panels.Children.Clear();
            for (int i = 0; i < viewModel.ReaderNoticeInfoList.Count; i++)
            {
                if (i == 0 || viewModel.ReaderNoticeInfoList[i].AddTime.Date != viewModel.ReaderNoticeInfoList[i - 1].AddTime.Date)
                {
                    TextBlock title = new TextBlock();
                    title.Width = 580;
                    title.Margin = new Thickness(0, 10, 0, 0);
                    title.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    title.TextAlignment = TextAlignment.Left;
                    title.Style = (Style)this.FindResource("TextBlock_White_S");
                    title.Text = viewModel.ReaderNoticeInfoList[i].AddTime.ToString("MM月dd日");
                    this.Panels.Children.Add(title);
                }
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock text = new TextBlock();
                text.TextWrapping = TextWrapping.WrapWithOverflow;
                text.Width = 80;
                text.Margin = new Thickness(0, 5, 0, 0);
                text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                text.TextAlignment = TextAlignment.Left;
                text.Style = (Style)this.FindResource("TextBlock_White_XS");
                text.Text = viewModel.ReaderNoticeInfoList[i].AddTime.ToString("HH:mm:ss");
                sp.Children.Add(text);

                TextBlock remark = new TextBlock();
                remark.TextWrapping = TextWrapping.WrapWithOverflow;
                remark.Width = 470;
                remark.Margin = new Thickness(0, 5, 0, 0);
                remark.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                remark.TextAlignment = TextAlignment.Left;
                remark.Style = (Style)this.FindResource("TextBlock_White_XS");
                remark.Text = viewModel.ReaderNoticeInfoList[i].Note;
                sp.Children.Add(remark);

                this.Panels.Children.Add(sp);
            }
        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
        }
    }
}
