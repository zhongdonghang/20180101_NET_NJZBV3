using System;
using System.Windows;
using System.Windows.Controls;

namespace SeatClientV3
{
    /// <summary>
    /// RecordTheQueryWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RecordTheQueryWindow : Window
    {

        public RecordTheQueryWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        ViewModel.RecordTheQueryWindow_ViewModel viewModel = new ViewModel.RecordTheQueryWindow_ViewModel();
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            StopRead();
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    GetLogs(e.CardNo);
                }));

            }
            StartRead();
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowMessage()
        {
            this.Panels.Children.Clear();
            viewModel.Reader = new ViewModel.ReaderInfoViewModel();
            viewModel.CloseTime = 60;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
            StartRead();
            this.Topmost = true;
            this.Topmost = false;
            ShowDialog();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(WinClosing));
            }
            else
            {
                viewModel.CloseTime = viewModel.CountDown.CountdownSceonds;
            }
        }
        /// <summary>
        /// 测试模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (txt_cardno.Text != "")
            {
                GetLogs(txt_cardno.Text);
            }
        }
        /// <summary>
        /// 显示记录
        /// </summary>
        private void GetLogs(string cardNo)
        {
            this.Panels.Children.Clear();
            viewModel.AddRecoed(cardNo);
            for (int i = 0; i < viewModel.EnterOutLogList.Count; i++)
            {
                if (i == 0 || viewModel.EnterOutLogList[i].EnterOutTime.Date != viewModel.EnterOutLogList[i - 1].EnterOutTime.Date)
                {
                    TextBlock title = new TextBlock();
                    title.Width = 990;
                    title.Margin = new Thickness(0, 10, 0, 0);
                    title.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    title.Style = (Style)this.FindResource("TextBlock_White_M");
                    title.Text = viewModel.EnterOutLogList[i].EnterOutTime.ToString("MM月dd日");
                    this.Panels.Children.Add(title);
                }
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock text = new TextBlock();
                text.TextWrapping = TextWrapping.WrapWithOverflow;
                text.Width = 140;
                text.Margin = new Thickness(0, 5, 0, 0);
                text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                text.Style = (Style)this.FindResource("TextBlock_White_MS_Wrapping");
                text.Text = viewModel.EnterOutLogList[i].EnterOutTime.ToString("HH:mm:ss");
                sp.Children.Add(text);

                TextBlock remark = new TextBlock();
                remark.TextWrapping = TextWrapping.WrapWithOverflow;
                remark.Width = 830;
                remark.Margin = new Thickness(0, 5, 0, 0);
                remark.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                remark.Style = (Style)this.FindResource("TextBlock_White_MS_Wrapping");
                remark.Text = viewModel.EnterOutLogList[i].Remark;
                sp.Children.Add(remark);

                this.Panels.Children.Add(sp);
            }
        }
        /// <summary>
        /// 关闭
        /// </summary>
        private void WinClosing()
        {
            this.Hide();
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            StopRead();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinClosing();
        }

        private void StopRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.Stop();
                viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
        }
        private void StartRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
                viewModel.ClientObject.ObjCardReader.Start();
            }
        }
    }
}
