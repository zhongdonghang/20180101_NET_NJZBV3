using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SeatClientV3.MyUserControl;
using SeatClientV3.OperateResult;
using SeatClientV3.ViewModel;
using SeatClientV3.WindowObject;
using SeatManage.EnumType;

namespace SeatClientV3
{
    /// <summary>
    /// ReadingRoomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReadingRoomWindow : Window
    {
        public ReadingRoomWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            BindingReadingRoom();
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        public void ShowMessage()
        {
            viewModel.GetUsage();
            viewModel.CloseTime = 60;
            viewModel.CountDown = new FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            viewModel.CountDown.Start();
            //this.Owner = MainWindowObject.GetInstance().Window;
            if (viewModel.ClientObject.RoomAutoAddSize)
            {
                WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.MoveUp, viewModel.ClientObject.AddSize.ToString());
                WeiCharOperationWindowObject.GetInstance().Window.WinChange((int) (viewModel.WindowTop));
            }
            g_loading.Visibility = System.Windows.Visibility.Hidden;
            this.Topmost = true;
            this.Topmost = false;
            ShowDialog();
        }
        /// <summary>
        /// 倒计时窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                SystemObject clientObject = SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = ClientOperation.Exit;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                Dispatcher.Invoke(new Action(WinClosing));
            }
            else
            {
                viewModel.CloseTime = viewModel.CountDown.CountdownSceonds;
            }
        }
        public ReadingRoomWindow_ViewModel viewModel = new ReadingRoomWindow_ViewModel();
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.FlowControl = ClientOperation.Exit;
            WinClosing();
        }
        /// <summary>
        /// 常坐座位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usuallySeatBtn_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CountDown.Pause();
            UsuallySeatWindowObject.GetInstance().Window.ShowMessage();
            if (UsuallySeatWindowObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
            {
                WinClosing();
            }
            else
            {
                viewModel.CountDown.Start();
            }
        }
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        private void BindingReadingRoom()
        {
            //设置空间最小尺寸
            G_bg.Width = viewModel.WindowWidth <= 1080 ? 1080 : viewModel.WindowWidth;
            G_bg.Height = viewModel.WindowHeight <= 1000 ? 1000 : viewModel.WindowHeight;
            if (viewModel.WindowWidth <= 1080 || viewModel.WindowHeight <= 1000)
            {
                if (viewModel.WindowHeight <= viewModel.WindowWidth)
                {
                    G_bg.Width = viewModel.WindowWidth * (1000 / viewModel.WindowHeight);
                }
                else
                {
                    G_bg.Height = viewModel.WindowHeight * (1080 / viewModel.WindowWidth);
                }
            }
            if (viewModel.ReadingRoomUsage.Count <= 0)
            {
                return;
            }
            //处理UC个数
            double rcHeight = G_bg.Height - 370;
            double rcWidth = G_bg.Width - 340;
            int rowCount = (int)rcHeight / 150;
            int colCount = (int)rcWidth / 135;
            double freeHeight = 0;
            double freeWidth = 0;
            while (true)
            {
                if ((colCount + 1) * 10 > rcWidth - colCount * 135)
                {
                    colCount--;
                }
                else
                {
                    freeWidth = (rcWidth - colCount * 135) / (colCount + 1);
                    break;
                }
            }
            while (true)
            {
                if ((rowCount + 1) * 10 > rcHeight - rowCount * 150)
                {
                    rowCount--;
                }
                else
                {
                    freeHeight = (rcHeight - rowCount * 150) / (rowCount + 1);
                    break;
                }
            }
            int allCount = rowCount * colCount;
            int tiCount = (int)rcWidth / 140;
            foreach (KeyValuePair<string, List<ReadingRoomUC_ViewModel>> area in viewModel.ReadingRoomUsage.TakeWhile(area => TabCont.Items.Count < tiCount))
            {
                if (area.Value.Count <= allCount)
                {
                    TabItem areaTI = new TabItem();
                    areaTI.Header = area.Key;
                    areaTI.Style = (Style)FindResource("TabItem_ReadingRoom");

                    Canvas roomCanvas = new Canvas();
                    roomCanvas.Width = rcWidth;
                    roomCanvas.Height = rcHeight;

                    roomCanvas.PreviewMouseLeftButtonUp += Canvas_PreviewMouseLeftButtonUp;
                    for (int i = 0; i < area.Value.Count; i++)
                    {
                        UC_ReadingRoom uc_Room = new UC_ReadingRoom(area.Value[i]);
                        uc_Room.Height = 170;
                        uc_Room.Width = 135;
                        Canvas.SetLeft(uc_Room, freeWidth + i % colCount * (135 + freeWidth));
                        Canvas.SetTop(uc_Room, freeHeight + i / colCount * (150 + freeHeight));
                        roomCanvas.Children.Add(uc_Room);
                    }
                    areaTI.Content = roomCanvas;
                    TabCont.Items.Add(areaTI);
                }
                //超过15个的处理
                else
                {
                    int tapCount = area.Value.Count / allCount;
                    if (area.Value.Count % allCount != 0)
                    {
                        tapCount++;
                    }
                    Char[] chr = "A".ToCharArray();
                    for (int j = 0; j < tapCount; j++)
                    {
                        chr[0] = Convert.ToChar(chr[0] + j);
                        TabItem areaTI = new TabItem();
                        areaTI.Header = area.Key + new string(chr);
                        areaTI.Style = (Style)FindResource("TabItem_ReadingRoom");

                        Canvas roomCanvas = new Canvas();
                        roomCanvas.Width = rcWidth;
                        roomCanvas.Height = rcHeight;
                        roomCanvas.PreviewMouseLeftButtonUp += Canvas_PreviewMouseLeftButtonUp;
                        for (int i = allCount * j; i < allCount * (j + 1) && i < area.Value.Count; i++)
                        {
                            UC_ReadingRoom uc_Room = new UC_ReadingRoom(area.Value[i]);
                            uc_Room.Height = 150;
                            uc_Room.Width = 135;
                            Canvas.SetLeft(uc_Room, freeWidth + (i - j * allCount) % colCount * (135 + freeWidth));
                            Canvas.SetTop(uc_Room, freeHeight + (i - j * allCount) / colCount * (150 + freeHeight));
                            roomCanvas.Children.Add(uc_Room);
                        }
                        areaTI.Content = roomCanvas;
                        TabCont.Items.Add(areaTI);
                    }
                }
            }
        }
        /// <summary>
        /// 点击阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            UC_ReadingRoom room = e.Source as UC_ReadingRoom;
            if (room != null)
            {
                viewModel.CountDown.Pause();
                g_loading.Visibility = System.Windows.Visibility.Visible;
                viewModel.EnterReadingRoom(room.ViewModel);
                if (viewModel.ClientObject.EnterOutLogData.FlowControl == ClientOperation.Exit)
                {
                    WinClosing();
                }
                else if (viewModel.ClientObject.EnterOutLogData.FlowControl == ClientOperation.SelectSeat || viewModel.ClientObject.EnterOutLogData.FlowControl == ClientOperation.WaitSeat)
                {
                    WinClosing();
                }
                else
                {
                    WeiCharOperationWindowObject.GetInstance().Window.WinChange((int)(Top));
                    viewModel.CountDown.Start();
                    //if (!this.ShowActivated)
                    //{
                    //    this.ShowDialog();
                    //}
                }
                g_loading.Visibility = System.Windows.Visibility.Hidden;
            }
        }
        /// <summary>
        /// 窗口关闭
        /// </summary>
        private void WinClosing()
        {
            g_loading.Visibility = System.Windows.Visibility.Hidden;
            Hide();
            if (viewModel.ClientObject.ReaderAdvert != null)
            {
                viewModel.ClientObject.ReaderAdvert.Usage.WatchCount++;
            }
            viewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            viewModel.CountDown.Stop();
            //GC.Collect();
        }
    }
}
