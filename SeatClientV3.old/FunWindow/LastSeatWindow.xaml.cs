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
    /// LastSeatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LastSeatWindow : Window
    {
        public LastSeatWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            BindingReadingRoom();
            viewModel.CloseTime = viewModel.clientObject.ClientSetting.DeviceSetting.WinCountDown.RoomWindow;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
            viewModel.LastSeatRun();
        }
        public ViewModel.LastSeatWindow_ViewModel viewModel = new ViewModel.LastSeatWindow_ViewModel();
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        private void BindingReadingRoom()
        {
            if (viewModel.RoomList.Count > 0)
            {
                Canvas roomCanvas = new Canvas();
                roomCanvas.Width = 835;

                Canvas.SetLeft(roomCanvas, 0);
                Canvas.SetTop(roomCanvas, 0);
                int roomCount = viewModel.RoomList.Count;
                for (int i = 0; i < roomCount; i++)
                {
                    SeatClientV3.MyUserControl.UC_LastSeatNum uc_Room = new SeatClientV3.MyUserControl.UC_LastSeatNum(viewModel.RoomList[i]);
                    if (viewModel.RoomList[i].Status == SeatManage.EnumType.ReadingRoomStatus.Close || viewModel.RoomList[i].Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                    {
                        uc_Room.txt_RoomName.Style = (Style)this.FindResource("TextBlockStyle_Close");
                    }
                    else
                    {
                        switch (viewModel.RoomList[i].Usage)
                        {
                            case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                                uc_Room.txt_RoomName.Style = (Style)this.FindResource("TextBlockStyle_Free");
                                break;
                            case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                                uc_Room.txt_RoomName.Style = (Style)this.FindResource("TextBlockStyle_Busy");
                                break;
                            case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                                uc_Room.txt_RoomName.Style = (Style)this.FindResource("TextBlockStyle_Full");
                                break;
                        }
                    }

                    //最后一个格子
                    if ((i - 4) % 5 == 0)
                    {
                        //点（不显示）
                        uc_Room.CircleRightTopBig.Style = (Style)this.FindResource("StyleHidden");
                        uc_Room.CircleRightTopSmall.Style = (Style)this.FindResource("StyleHidden");
                        uc_Room.CircleRightButtomBig.Style = (Style)this.FindResource("StyleHidden");
                        uc_Room.CircleRightButtomSmall.Style = (Style)this.FindResource("StyleHidden");
                        //线（显示下）
                        uc_Room.LineRight.Style = (Style)this.FindResource("LineHidden");
                        uc_Room.LineTop.Style = (Style)this.FindResource("LineHidden");
                        uc_Room.LineButtom.Style = (Style)this.FindResource("LineVisible");
                    }
                    //前四个格子
                    else
                    {
                        //点（显示右上右下）
                        uc_Room.CircleRightTopBig.Style = (Style)this.FindResource("StyleHidden");
                        uc_Room.CircleRightTopSmall.Style = (Style)this.FindResource("StyleHidden");
                        uc_Room.CircleRightButtomBig.Style = (Style)this.FindResource("StyleVisible");
                        uc_Room.CircleRightButtomSmall.Style = (Style)this.FindResource("StyleVisible");
                        //线（显示上下右）
                        uc_Room.LineRight.Style = (Style)this.FindResource("LineVisible");
                        uc_Room.LineTop.Style = (Style)this.FindResource("LineHidden");
                        uc_Room.LineButtom.Style = (Style)this.FindResource("LineVisible");
                    }
                    uc_Room.Height = 160;
                    uc_Room.Width = 160;
                    Canvas.SetLeft(uc_Room, 15 + i % 5 * 160);
                    Canvas.SetTop(uc_Room, i / 5 * 150);
                    roomCanvas.Children.Add(uc_Room);
                }
                //不足5个补齐
                int remainder = roomCount % 5;
                int sum = roomCount + remainder;
                if (remainder > 0)
                {
                    int emptyCount = 5 - remainder;
                    for (int j = 0; j < emptyCount; j++)
                    {
                        SeatClientV3.MyUserControl.UC_LastSeatNum uc_Room = new SeatClientV3.MyUserControl.UC_LastSeatNum();
                        uc_Room.Height = 160;
                        uc_Room.Width = 160;
                        Canvas.SetLeft(uc_Room, 15 + (viewModel.RoomList.Count + j) % 5 * 160);
                        Canvas.SetTop(uc_Room, (viewModel.RoomList.Count + j) / 5 * 150);
                        if (j + 1 == emptyCount)
                        {
                            //点（不显示）
                            uc_Room.CircleRightTopBig.Style = (Style)this.FindResource("StyleHidden");
                            uc_Room.CircleRightTopSmall.Style = (Style)this.FindResource("StyleHidden");
                            uc_Room.CircleRightButtomBig.Style = (Style)this.FindResource("StyleHidden");
                            uc_Room.CircleRightButtomSmall.Style = (Style)this.FindResource("StyleHidden");
                            //线（显示下）
                            uc_Room.LineRight.Style = (Style)this.FindResource("LineHidden");
                            uc_Room.LineTop.Style = (Style)this.FindResource("LineHidden");
                            uc_Room.LineButtom.Style = (Style)this.FindResource("LineVisible");
                        }
                        else
                        {
                            //点（显示右上右下）
                            uc_Room.CircleRightTopBig.Style = (Style)this.FindResource("StyleHidden");
                            uc_Room.CircleRightTopSmall.Style = (Style)this.FindResource("StyleHidden");
                            uc_Room.CircleRightButtomBig.Style = (Style)this.FindResource("StyleVisible");
                            uc_Room.CircleRightButtomSmall.Style = (Style)this.FindResource("StyleVisible");
                            //线（显示上下右）
                            uc_Room.LineRight.Style = (Style)this.FindResource("LineVisible");
                            uc_Room.LineTop.Style = (Style)this.FindResource("LineHidden");
                            uc_Room.LineButtom.Style = (Style)this.FindResource("LineVisible");
                        }
                        roomCanvas.Children.Add(uc_Room);
                    }
                }
                int result = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(viewModel.RoomList.Count) / 5));
                roomCanvas.Height = result * 160;
                Panels.Children.Add(roomCanvas);
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

        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            SeatClientV3.OperateResult.SystemObject clientObject = SeatClientV3.OperateResult.SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
