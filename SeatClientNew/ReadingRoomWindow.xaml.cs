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
using SeatClientV2.MyUserControl;

namespace SeatClientV2
{
    /// <summary>
    /// ReadingRoomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReadingRoomWindow : Window
    {


        public ReadingRoomWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            BindingReadingRoom();
            viewModel.CloseTime = 60;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
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
                SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                viewModel.RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.None;
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
        public ViewModel.ReadingRoomWindow_ViewModel viewModel = new ViewModel.ReadingRoomWindow_ViewModel();
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            viewModel.RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.None;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
            this.Close();
        }
        /// <summary>
        /// 常坐座位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void usuallySeatBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuallySeatWindow seatWindow = new UsuallySeatWindow();
            viewModel.CountDown.Pause();
            seatWindow.ShowDialog();
            viewModel.CountDown.Start();
            if (seatWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
            {
                viewModel.clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                viewModel.RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.ManualMode;
                this.Close();
            }
        }
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        private void BindingReadingRoom()
        {
            if (viewModel.ReadingRoomUsage.Count > 0)
            {
                foreach (KeyValuePair<string, List<SeatClientV2.ViewModel.ReadingRoomUC_ViewModel>> area in viewModel.ReadingRoomUsage)
                {
                    if (area.Value.Count <= 15)
                    {
                        TabItem areaTI = new TabItem();
                        areaTI.Header = area.Key;
                        areaTI.Style = (Style)this.FindResource("TabItem_ReadingRoom");

                        Canvas roomCanvas = new Canvas();
                        roomCanvas.Width = 730;
                        roomCanvas.Height = 560;
                        roomCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(Canvas_PreviewMouseLeftButtonUp);
                        for (int i = 0; i < area.Value.Count; i++)
                        {
                            UC_ReadingRoom uc_Room = new UC_ReadingRoom(area.Value[i]);
                            if (area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.Close || area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                            {
                                uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Close");
                                uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Close");
                            }
                            else
                            {
                                switch (area.Value[i].Usage)
                                {
                                    case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                                        uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Normal");
                                        uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Normal");
                                        break;
                                    case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                                        uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Crowd");
                                        uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Crowd");
                                        break;
                                    case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                                        uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Full");
                                        uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Full");
                                        break;
                                }
                            }
                            uc_Room.Height = 170;
                            uc_Room.Width = 135;
                            Canvas.SetLeft(uc_Room, 5 + i % 5 * 145);
                            Canvas.SetTop(uc_Room, 20 + i / 5 * 190);
                            roomCanvas.Children.Add(uc_Room);
                        }
                        areaTI.Content = roomCanvas;
                        TabCont.Items.Add(areaTI);
                    }
                    //超过15个的处理
                    else
                    {
                        int tapCount = area.Value.Count / 15;
                        if (area.Value.Count % 15 != 0)
                        {
                            tapCount++;
                        }
                        Char[] chr = "A".ToCharArray();
                        for (int j = 0; j < tapCount; j++)
                        {
                            chr[0] = Convert.ToChar(chr[0] + j);
                            TabItem areaTI = new TabItem();
                            areaTI.Header = area.Key + new string(chr);
                            areaTI.Style = (Style)this.FindResource("TabItem_ReadingRoom");

                            Canvas roomCanvas = new Canvas();
                            roomCanvas.Width = 730;
                            roomCanvas.Height = 560;
                            roomCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(Canvas_PreviewMouseLeftButtonUp);
                            for (int i = 15 * j; i < 15 * (j + 1) && i < area.Value.Count; i++)
                            {
                                UC_ReadingRoom uc_Room = new UC_ReadingRoom(area.Value[i]);
                                if (area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.Close || area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                                {
                                    uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Close");
                                    uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Close");
                                }
                                else
                                {
                                    switch (area.Value[i].Usage)
                                    {
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                                            uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Normal");
                                            uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Normal");
                                            break;
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                                            uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Crowd");
                                            uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Crowd");
                                            break;
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                                            uc_Room.nameRount.Style = (Style)this.FindResource("EllipseStyle_Full");
                                            uc_Room.seatcountTxt.Style = (Style)this.FindResource("TextBlockStyle_Full");
                                            break;
                                    }
                                }
                                uc_Room.Height = 170;
                                uc_Room.Width = 135;
                                Canvas.SetLeft(uc_Room, 5 + (i - j * 15) % 5 * 145);
                                Canvas.SetTop(uc_Room, 20 + (i - j * 15) / 5 * 190);
                                roomCanvas.Children.Add(uc_Room);
                            }
                            areaTI.Content = roomCanvas;
                            TabCont.Items.Add(areaTI);
                        }
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
            if (e.Source is UC_ReadingRoom)
            {
                viewModel.CountDown.Pause();
                viewModel.EnterReadingRoom((e.Source as UC_ReadingRoom).viewModel);
                if (viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.Exit)
                {
                    this.Close();
                }
                if (viewModel.RoomSelectSeatMethod != SeatManage.EnumType.SelectSeatMode.None && viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.SelectSeat || viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.WaitSeat)
                {
                    this.Close();
                }
                viewModel.CountDown.Start();
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
        }
    }
}
