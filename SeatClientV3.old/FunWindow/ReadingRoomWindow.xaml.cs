using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SeatClientV3.FunWindow
{
    /// <summary>
    /// ReadingRoomWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ReadingRoomWindow : Window
    {
        public ReadingRoomWindow()
        {
            this.DataContext = viewModel;
            ThreadPool.QueueUserWorkItem((o) =>
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    viewModel.GetRoomUsage();
                    BindingReadingRoom();
                    viewModel.CloseTime = viewModel.clientObject.ClientSetting.DeviceSetting.WinCountDown.RoomWindow;
                    viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
                    viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
                    viewModel.CountDown.Start();
                    loading_G.Visibility = System.Windows.Visibility.Collapsed;
                }));

            });
            InitializeComponent();
        }
        public ViewModel.ReadingRoomWindow_ViewModel viewModel = new ViewModel.ReadingRoomWindow_ViewModel();

        /// <summary>
        /// 倒计时窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                SeatClientV3.OperateResult.SystemObject clientObject = SeatClientV3.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
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
            if (viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.SelectSeat)
            {
                this.Close();
            }
        }
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        private void BindingReadingRoom()
        {
            try
            {
                if (viewModel.ReadingRoomUsage.Count > 0)
                {
                    foreach (KeyValuePair<string, List<SeatClientV3.UCViewModel.ReadingRoomBtn_ViewModel>> area in viewModel.ReadingRoomUsage)
                    {
                        if (area.Value.Count <= 20)
                        {
                            Canvas roomCanvas = new Canvas();
                            roomCanvas.Width = 835;
                            roomCanvas.Height = 710;
                            Canvas.SetLeft(roomCanvas, 0);
                            Canvas.SetTop(roomCanvas, 0);
                            roomCanvas.Name = area.Key + "_Canvas";
                            roomCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(Canvas_PreviewMouseLeftButtonUp);
                            for (int i = 0; i < area.Value.Count; i++)
                            {
                                SeatClientV3.MyUserControl.UC_ReadingRoom uc_Room = new SeatClientV3.MyUserControl.UC_ReadingRoom(area.Value[i]);
                                if (area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.Close || area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                                {
                                    uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_C");
                                    uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_C");
                                    uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_C");
                                }
                                else
                                {
                                    switch (area.Value[i].Usage)
                                    {
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                                            uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_N");
                                            uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_N");
                                            uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_N");
                                            break;
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                                            uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_B");
                                            uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_B");
                                            uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_B");
                                            break;
                                        case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                                            uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_F");
                                            uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_F");
                                            uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_F");
                                            break;
                                    }
                                }
                                uc_Room.Height = 150;
                                uc_Room.Width = 120;
                                Canvas.SetLeft(uc_Room, 45 + i % 5 * 160);
                                Canvas.SetTop(uc_Room, 30 + i / 5 * 170);
                                roomCanvas.Children.Add(uc_Room);
                            }
                            RadioButton rb_Room = new RadioButton();
                            rb_Room.Name = area.Key + "_RadioButton";
                            rb_Room.Width = 150;
                            rb_Room.Height = 75;
                            rb_Room.Margin = new Thickness(0, 2, 0, 0);
                            rb_Room.Style = (Style)this.FindResource("RadioButtonStyle_RoomArea");
                            rb_Room.Click += AreaRadioButton_Click;
                            rb_Room.Content = area.Key;
                            sp_rb.Children.Add(rb_Room);

                            roomCanvas.Visibility = System.Windows.Visibility.Collapsed;
                            Can_Area.Children.Add(roomCanvas);
                        }
                        //超过15个的处理
                        else
                        {
                            int tapCount = area.Value.Count / 20;
                            if (area.Value.Count % 20 != 0)
                            {
                                tapCount++;
                            }
                            Char[] chr = "A".ToCharArray();
                            for (int j = 0; j < tapCount; j++)
                            {
                                chr[0] = Convert.ToChar(chr[0] + j);

                                Canvas roomCanvas = new Canvas();
                                roomCanvas.Width = 835;
                                roomCanvas.Height = 710;
                                Canvas.SetLeft(roomCanvas, 0);
                                Canvas.SetTop(roomCanvas, 0);
                                roomCanvas.Name = area.Key + new string(chr) + "_Canvas";

                                roomCanvas.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(Canvas_PreviewMouseLeftButtonUp);
                                for (int i = 20 * j; i < 20 * (j + 1) && i < area.Value.Count; i++)
                                {
                                    SeatClientV3.MyUserControl.UC_ReadingRoom uc_Room = new SeatClientV3.MyUserControl.UC_ReadingRoom(area.Value[i]);
                                    if (area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.Close || area.Value[i].Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                                    {
                                        uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_C");
                                        uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_C");
                                        uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_C");
                                    }
                                    else
                                    {
                                        switch (area.Value[i].Usage)
                                        {
                                            case SeatManage.EnumType.ReadingRoomUsingStatus.Normal:
                                                uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_N");
                                                uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_N");
                                                uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_N");
                                                break;
                                            case SeatManage.EnumType.ReadingRoomUsingStatus.Crowd:
                                                uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_B");
                                                uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_B");
                                                uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_B");
                                                break;
                                            case SeatManage.EnumType.ReadingRoomUsingStatus.Full:
                                                uc_Room.rec.Style = (Style)this.FindResource("RectangleStyle_F");
                                                uc_Room.recBG.Style = (Style)this.FindResource("RectangleStyle_F");
                                                uc_Room.eill.Style = (Style)this.FindResource("EllipseStyle_F");
                                                break;
                                        }
                                    }
                                    uc_Room.Height = 150;
                                    uc_Room.Width = 120;
                                    Canvas.SetLeft(uc_Room, 45 + (i - 20 * j) % 5 * 160);
                                    Canvas.SetTop(uc_Room, 30 + (i - 20 * j) / 5 * 170);
                                    roomCanvas.Children.Add(uc_Room);
                                }
                                RadioButton rb_Room = new RadioButton();
                                rb_Room.Name = area.Key + new string(chr) + "_RadioButton";
                                rb_Room.Width = 150;
                                rb_Room.Height = 75;
                                rb_Room.Margin = new Thickness(0, 2, 0, 0);
                                rb_Room.Style = (Style)this.FindResource("RadioButtonStyle_RoomArea");
                                rb_Room.Click += AreaRadioButton_Click;
                                rb_Room.Content = area.Key + new string(chr);
                                sp_rb.Children.Add(rb_Room);

                                roomCanvas.Visibility = System.Windows.Visibility.Collapsed;
                                Can_Area.Children.Add(roomCanvas);
                            }
                        }
                    }
                    if (sp_rb.Children.Count > 0)
                    {
                        RadioButton rb = sp_rb.Children[0] as RadioButton;
                        rb.IsChecked = true;
                        Canvas ca = Can_Area.Children[0] as Canvas;
                        ca.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("初始化阅览室列表界面失败：" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(SeatManage.EnumType.MessageType.Exception);
                errorWindow.ShowDialog();
                SeatClientV3.OperateResult.SystemObject clientObject = SeatClientV3.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
                this.Close();
            }
        }
        /// <summary>
        /// 点击阅览室
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is SeatClientV3.MyUserControl.UC_ReadingRoom)
            {
                viewModel.CountDown.Pause();
                viewModel.EnterReadingRoom((e.Source as SeatClientV3.MyUserControl.UC_ReadingRoom).viewModel);
                if (viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.Exit)
                {
                    this.Close();
                }
                if (viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.SelectSeat
                    || viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.WaitSeat
                    || viewModel.clientObject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.RandonSelect)
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
        /// <summary>
        /// 点击打开页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AreaRadioButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Canvas uc in Can_Area.Children)
            {
                if (uc.Name.Split('_')[0] == (sender as RadioButton).Name.Split('_')[0])
                {
                    uc.Visibility = System.Windows.Visibility.Visible;
                }
                else
                {
                    uc.Visibility = System.Windows.Visibility.Collapsed;
                }

            }
        }


    }
}
