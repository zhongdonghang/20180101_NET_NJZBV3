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

namespace SeatClientV3.FunWindow
{
    /// <summary>
    /// LogSearchWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LogSearchWindow : Window
    {
        public LogSearchWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.CloseTime = viewModel.Clientobject.ClientSetting.DeviceSetting.WinCountDown.LogSerachWindow;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            if (viewModel.Clientobject.ObjCardReader != null)
            {
                viewModel.Clientobject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                viewModel.Clientobject.ObjCardReader.Start();
            }
        }
        ViewModel.LogSearchWindow_ViewModel viewModel = new ViewModel.LogSearchWindow_ViewModel();

        /// <summary>
        /// 测试模式获取卡号
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
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
        }
        /// <summary>
        ///获取卡号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            viewModel.Clientobject.ObjCardReader.Stop();
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    GetLogs(e.CardNo);
                }));

            }
            viewModel.Clientobject.ObjCardReader.Start();
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
                    title.Style = (Style)this.FindResource("TextBlock_White_Log_M");
                    DateTime dt = viewModel.EnterOutLogList[i].EnterOutTime;
                    if (dt.Date == SeatManage.Bll.ServiceDateTime.Now.Date)
                    {
                        title.Text = "今天";
                    }
                    else if (dt.Date == SeatManage.Bll.ServiceDateTime.Now.AddDays(-1).Date)
                    {
                        title.Text = "昨天";
                    }
                    else if (dt.Date == SeatManage.Bll.ServiceDateTime.Now.AddDays(-2).Date)
                    {
                        title.Text = "前天";
                    }
                    else
                    {
                        title.Text = dt.ToString("MM月dd日");
                    }
                    this.Panels.Children.Add(title);
                }
                StackPanel sp = new StackPanel();
                sp.Orientation = Orientation.Horizontal;
                TextBlock text = new TextBlock();
                text.TextWrapping = TextWrapping.WrapWithOverflow;
                text.Width = 140;
                text.Margin = new Thickness(0, 5, 0, 0);
                text.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                text.Style = (Style)this.FindResource("TextBlock_White_Log_Content");
                text.Text = viewModel.EnterOutLogList[i].EnterOutTime.ToString("HH:mm:ss");
                sp.Children.Add(text);

                TextBlock remark = new TextBlock();
                remark.TextWrapping = TextWrapping.WrapWithOverflow;
                remark.Width = 700;
                remark.Margin = new Thickness(0, 5, 0, 0);
                remark.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                remark.Style = (Style)this.FindResource("TextBlock_White_Log_Content");
                remark.Text = viewModel.EnterOutLogList[i].Remark;
                sp.Children.Add(remark);

                this.Panels.Children.Add(sp);
            }
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
                Dispatcher.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
            else
            {
                viewModel.CloseTime = viewModel.CountDown.CountdownSceonds;
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
