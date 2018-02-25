using System;
using System.Windows;
using System.Windows.Controls;
using SeatClientV3.OperateResult;
using SeatClientV3.ViewModel;

namespace SeatClientV3
{
    /// <summary>
    /// ReaderNoteWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReaderNoteWindow : Window
    {
        ReaderNoteWindow_ViewModel viewModel = new ReaderNoteWindow_ViewModel();
        public ReaderNoteWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowMessage()
        {
            GetMessage();
            viewModel.CloseTime = 10;
            viewModel.CountDown = new FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            viewModel.CountDown.Start();
            ShowDialog();
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
                Dispatcher.Invoke(new Action(WinClosing));
            }
        }
        public void GetMessage()
        {
            viewModel.AddReaderNoticeInfoList();
            Panels.Children.Clear();
            for (int i = 0; i < viewModel.ReaderNoticeInfoList.Count; i++)
            {
                if (i == 0 || viewModel.ReaderNoticeInfoList[i].AddTime.Date != viewModel.ReaderNoticeInfoList[i - 1].AddTime.Date)
                {
                    TextBlock title = new TextBlock();
                    title.Width = 580;
                    title.Margin = new Thickness(0, 10, 0, 0);
                    title.HorizontalAlignment = HorizontalAlignment.Left;
                    title.TextAlignment = TextAlignment.Left;
                    title.Style = (Style)FindResource("TextBlock_White_S");
                    title.Text = viewModel.ReaderNoticeInfoList[i].AddTime.ToString("MM月dd日");
                    Panels.Children.Add(title);
                }
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock text = new TextBlock();
                text.TextWrapping = TextWrapping.WrapWithOverflow;
                text.Width = 80;
                text.Margin = new Thickness(0, 5, 0, 0);
                text.HorizontalAlignment = HorizontalAlignment.Left;
                text.TextAlignment = TextAlignment.Left;
                text.Style = (Style)FindResource("TextBlock_White_XS");
                text.Text = viewModel.ReaderNoticeInfoList[i].AddTime.ToString("HH:mm:ss");
                sp.Children.Add(text);

                TextBlock remark = new TextBlock();
                remark.TextWrapping = TextWrapping.WrapWithOverflow;
                remark.Width = 470;
                remark.Margin = new Thickness(0, 5, 0, 0);
                remark.HorizontalAlignment = HorizontalAlignment.Left;
                remark.TextAlignment = TextAlignment.Left;
                remark.Style = (Style)FindResource("TextBlock_White_XS");
                remark.Text = viewModel.ReaderNoticeInfoList[i].Note;
                sp.Children.Add(remark);

                Panels.Children.Add(sp);
            }

        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            WinClosing();
        }

        private void WinClosing()
        {
            Hide();
            if (viewModel.ClientObject.TitleAdvert != null)
            {
                viewModel.ClientObject.TitleAdvert.Usage.WatchCount++;
            }
            viewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            viewModel.CountDown.Stop();
        }
    }
}
