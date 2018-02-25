using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using SeatClientV3.Code;
using SeatClientV3.MyUserControl;
using SeatClientV3.OperateResult;
using SeatClientV3.WindowObject;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;

namespace SeatClientV3
{
    /// <summary>
    /// RoomSeatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoomSeatWindow : Window
    {
        public RoomSeatWindow(string roomNo)
        {
            InitializeComponent();
            viewModel = new ViewModel.RoomSeatWindow_ViewModel(roomNo);
            DataContext = viewModel;
            if (viewModel.WindowWidth != 1080)
            {
                btn_MaxWindow.Visibility = Visibility.Collapsed;
                btn_NormnlWindow.Visibility = Visibility.Collapsed;
            }
            viewModel.GetLayout();
            viewModel.RoomWindowClose += viewModel_RoomWindowClose;
            seatLayout();
        }

        void viewModel_RoomWindowClose(object sender, EventArgs e)
        {
            WinClosing();
        }
        public ViewModel.RoomSeatWindow_ViewModel viewModel;

        Rectangle areaSimle;
        double scaleX;//宽度的比值
        double scaleY;//高度的比值 
        double moveX = 0;
        double moveY = 0;
        void seatLayout()
        {
            canvas_Thumbnail.Children.Clear();
            canvas_Seat.Children.Clear();//座位图中移除所有子元素

            viewModel.AllSeatCount = viewModel.Layout.Seats.Count;

            int SeatWidth = 24 * viewModel.Layout.SeatCol;
            int SeatHeight = 24 * viewModel.Layout.SeatRow;
            this.canvas_Seat.Width = SeatWidth;
            this.canvas_Seat.Height = SeatHeight;
            //设置空间最小尺寸
            if (viewModel.WindowWidth <= 1080)
            {
                G_bg.Width = 1080;
            }
            else
            {
                G_bg.Width = viewModel.WindowWidth;
            }
            if (viewModel.WindowHeight <= 1000)
            {
                G_bg.Height = 1000;
            }
            else
            {
                G_bg.Height = viewModel.WindowHeight;
            }
            if (G_bg.Width == 1080 || G_bg.Height == 1000)
            {
                if (G_bg.Height <= G_bg.Width)
                {
                    G_bg.Width = viewModel.WindowWidth * (G_bg.Height / viewModel.WindowHeight);
                }
                else
                {
                    G_bg.Height = viewModel.WindowHeight * (G_bg.Width / viewModel.WindowWidth);
                }
            }

            double areaScaleX = SeatWidth / (G_bg.Width - SeatWindow.Margin.Left - SeatWindow.Margin.Right);
            double areaScaleY = SeatHeight / (G_bg.Height - SeatWindow.Margin.Top - SeatWindow.Margin.Bottom);
            if (SeatWidth >= SeatHeight)
            {
                scaleX = SeatWidth / canvas_Thumbnail.Width;
                scaleY = SeatWidth / canvas_Thumbnail.Height;
                moveY = (SeatWidth - SeatHeight) / 2 / scaleY;
            }
            else
            {
                scaleX = SeatHeight / canvas_Thumbnail.Width;
                scaleY = SeatHeight / canvas_Thumbnail.Height;
                moveX = (SeatHeight - SeatWidth) / 2 / scaleX;
            }

            try
            {
                Canvas.SetLeft(canvas_Seat, 0);
                Canvas.SetTop(canvas_Seat, 0);
            }
            catch
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.Exception);
                WinClosing();
            }

            #region 布局座位
            foreach (KeyValuePair<string, Seat> seat in viewModel.Layout.Seats)
            {

                #region 布局实际图,
                double canLeft = 24 * seat.Value.PositionX;
                double canTop = 24 * seat.Value.PositionY;
                UC_Seat seatUC = new UC_Seat(viewModel.SeatLayoutList[seat.Key]);
                seatUC.Width = 48;
                seatUC.Height = 48;

                seatUC.LblSeatNo.RenderTransform = new RotateTransform(-seat.Value.RotationAngle);
                seatUC.LblSeatNo.RenderTransformOrigin = new Point(0.5, 0.5);
                seatUC.RenderTransform = new RotateTransform(seat.Value.RotationAngle);
                seatUC.RenderTransformOrigin = new Point(0.5, 0.5);
                canvas_Seat.Children.Add(seatUC);
                Canvas.SetLeft(seatUC, canLeft);
                Canvas.SetTop(seatUC, canTop);
                #endregion

                #region 布局缩略图
                Rectangle rec = new Rectangle();
                rec.Width = 36 / scaleX;
                rec.Height = 36 / scaleY;
                double thumbLeft = (seat.Value.PositionX * 24 + 6) / scaleX;
                double thumbTop = (seat.Value.PositionY * 24 + 6) / scaleY;
                if (viewModel.SeatLayoutList[seat.Key].IsStop || viewModel.SeatLayoutList[seat.Key].IsUsing)
                {
                    rec.Fill = new SolidColorBrush(Color.FromRgb(234, 38, 52));
                }
                else
                {
                    rec.Fill = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                }
                rec.RenderTransformOrigin = new Point(0.5, 0.5);
                rec.RenderTransform = new RotateTransform(seat.Value.RotationAngle);
                canvas_Thumbnail.Children.Add(rec);
                Panel.SetZIndex(rec, 0);
                Canvas.SetLeft(rec, thumbLeft + moveX);
                Canvas.SetTop(rec, thumbTop + moveY);
                #endregion
            }
            #endregion

            #region 布局备注
            foreach (Note note in viewModel.Layout.Notes)
            {
                #region 实际图

                ViewModel.NoteUC_ViewModel noteVM = new ViewModel.NoteUC_ViewModel();
                noteVM.Notes = note.Remark;
                noteVM.NoteType = note.Type;
                UC_Note element = new UC_Note(noteVM);
                if (note.Type == SeatManage.EnumType.OrnamentType.Table)
                {
                    element.BorderThickness = new Thickness(1);
                    element.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                element.Width = note.BaseWidth * 24;
                element.Height = note.BaseHeight * 24;
                element.RenderTransformOrigin = new Point(0.5, 0.5);
                element.RenderTransform = new RotateTransform(note.RotationAngle);
                canvas_Seat.Children.Add(element);
                double canLeft = 24 * note.PositionX;
                double canTop = 24 * note.PositionY;
                Canvas.SetLeft(element, canLeft);
                Canvas.SetTop(element, canTop);
                #endregion

                #region 缩略图

                switch (note.Type)
                {
                    case SeatManage.EnumType.OrnamentType.Door:
                    case SeatManage.EnumType.OrnamentType.Steps:
                        break;
                    case SeatManage.EnumType.OrnamentType.Roundtable:
                    case SeatManage.EnumType.OrnamentType.Plant:
                        {
                            Border br = new Border();
                            br.CornerRadius = new CornerRadius(note.BaseWidth * 12 / scaleX);
                            br.BorderThickness = new Thickness(note.BaseWidth * 12 / scaleX);
                            br.BorderBrush = new SolidColorBrush(Color.FromRgb(231, 186, 100));
                            double thumbLeft = 24 / scaleX * note.PositionX;
                            double thumbTop = 24 / scaleY * note.PositionY;
                            canvas_Thumbnail.Children.Add(br);
                            Canvas.SetLeft(br, thumbLeft + moveX);
                            Canvas.SetTop(br, thumbTop + moveY);
                        }
                        break;
                    default:
                        {
                            Rectangle rec = new Rectangle();
                            rec.Width = 24 * note.BaseWidth / scaleX;
                            rec.Height = 24 * note.BaseHeight / scaleY;
                            double thumbLeft = 24 / scaleX * note.PositionX;
                            double thumbTop = 24 / scaleY * note.PositionY;
                            rec.RenderTransformOrigin = new Point(0.5, 0.5);
                            rec.RenderTransform = new RotateTransform(note.RotationAngle);
                            rec.Fill = new SolidColorBrush(Color.FromRgb(231, 186, 100));
                            canvas_Thumbnail.Children.Add(rec);
                            Canvas.SetLeft(rec, thumbLeft + moveX);
                            Canvas.SetTop(rec, thumbTop + moveY);
                        }
                        break;
                }
                #endregion
            }
            #endregion


            areaSimle = new Rectangle();
            areaSimle.Name = "areaSimle";
            if (areaScaleX != 0)
            {
                areaSimle.Width = (canvas_Thumbnail.Width - moveX * 2) / areaScaleX;//按比例缩小层的宽度
            }
            else
            {
                areaSimle.Width = canvas_Thumbnail.Width;
            }
            if (areaScaleY != 0)
            {
                areaSimle.Height = (canvas_Thumbnail.Height - moveY * 2) / areaScaleY; //按比例缩小层的高度
            }
            else
            {
                areaSimle.Height = canvas_Thumbnail.Height;
            }

            areaSimle.Fill = new SolidColorBrush(Colors.White);
            areaSimle.Opacity = 0.5;

            canvas_Thumbnail.Children.Add(areaSimle);
            Panel.SetZIndex(areaSimle, 1000);
            Canvas.SetTop(areaSimle, moveY);
            Canvas.SetLeft(areaSimle, moveX);
            //小的范围层。最后创建，防止被遮盖
        }



        public void ShowMessage()
        {

            viewModel.CloseTime = 60;
            viewModel.CountDown = new FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            viewModel.CountDown.Start();
            viewModel.GetUsingState();
            canvas_Thumbnail.Children.RemoveRange(0, viewModel.Layout.Seats.Count);
            foreach (KeyValuePair<string, Seat> seat in viewModel.Layout.Seats)
            {
                #region 布局缩略图
                Rectangle rec = new Rectangle();
                rec.Name = "recSeat_" + seat.Key;
                rec.Width = 36 / scaleX;
                rec.Height = 36 / scaleY;
                double thumbLeft = (seat.Value.PositionX * 24 + 6) / scaleX;
                double thumbTop = (seat.Value.PositionY * 24 + 6) / scaleY;
                if (viewModel.SeatLayoutList[seat.Key].IsStop || viewModel.SeatLayoutList[seat.Key].IsUsing)
                {
                    rec.Fill = new SolidColorBrush(Color.FromRgb(234, 38, 52));
                }
                else
                {
                    rec.Fill = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                }
                rec.RenderTransformOrigin = new Point(0.5, 0.5);
                rec.RenderTransform = new RotateTransform(seat.Value.RotationAngle);
                canvas_Thumbnail.Children.Insert(0, rec);
                Panel.SetZIndex(rec, 0);
                Canvas.SetLeft(rec, thumbLeft + moveX);
                Canvas.SetTop(rec, thumbTop + moveY);
                #endregion
            }
            if (viewModel.ClientObject.SeatAutoAddSize)
            {
                SystemObject clientObject = SystemObject.GetInstance();
                this.Height = viewModel.WindowHeight += clientObject.AddSize;
                this.Top = viewModel.WindowTop -= clientObject.AddSize;
                G_bg.Height += clientObject.AddSize;
                seatLayout();
                btn_MaxWindow.Visibility = Visibility.Collapsed;
                btn_NormnlWindow.Visibility = Visibility.Visible;
                WPFMessage.MessageHelper.SendMessage(clientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.MoveUp, clientObject.AddSize.ToString());
                WeiCharOperationWindowObject.GetInstance().Window.WinChange((int)(Top));
            }
            //this.Topmost = true;
            this.Owner = ReadingRoomWindowObject.GetInstance().Window;
            //ReadingRoomWindowObject.GetInstance().Window.Hide();
            this.Topmost = true;
            this.Topmost = false;
            ShowDialog();

        }



        #region 页面操作逻辑
        private void Thumbnail_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Point mousePoint = e.GetPosition(canvas_Thumbnail);
            double x = mousePoint.X - areaSimle.Width / 2;
            double y = mousePoint.Y - areaSimle.Height / 2;
            Canvas.SetLeft(areaSimle, x);
            Canvas.SetTop(areaSimle, y);

            Point imgPanelPonsition = new Point();
            imgPanelPonsition.X = (x - moveX) * scaleX;
            imgPanelPonsition.Y = (y - moveY) * scaleY;

            Canvas.SetLeft(canvas_Seat, -imgPanelPonsition.X);
            Canvas.SetTop(canvas_Seat, -imgPanelPonsition.Y);
            e.Handled = true;
        }

        #endregion
        private void WinClosing()
        {
            //Hide();
            viewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            viewModel.CountDown.Stop();
            SystemObject clientObject = SystemObject.GetInstance();
            WPFMessage.MessageHelper.SendMessage(clientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.OnLock);
            GC.Collect();
            Close();
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
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
                Dispatcher.Invoke(new Action(WinClosing));
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
            SystemObject clientObject = SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
            WinClosing();
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Back;
            WinClosing();
        }
        /// <summary>
        /// 随机选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_random_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CountDown.Pause();
            if (viewModel.RandomSeat())
            {
                WinClosing();
            }
            else
            {
                viewModel.CountDown.Start();
            }
        }
        /// <summary>
        /// 键盘选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_keyboard_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CountDown.Pause();
            if (viewModel.KeyboardSelectSeat())
            {
                WinClosing();
            }
            else
            {
                viewModel.CountDown.Start();
            }
        }
        /// <summary>
        /// 全屏显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_MaxWindow_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = SystemObject.GetInstance();
            Height += clientObject.AddSize;
            Top -= clientObject.AddSize;
            G_bg.Height += clientObject.AddSize;
            seatLayout();
            btn_MaxWindow.Visibility = Visibility.Collapsed;
            btn_NormnlWindow.Visibility = Visibility.Visible;
            WPFMessage.MessageHelper.SendMessage(clientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.MoveUp, clientObject.AddSize.ToString());
            WeiCharOperationWindowObject.GetInstance().Window.WinChange((int)(Top));

        }
        /// <summary>
        /// 恢复正常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NormnlWindow_Click(object sender, RoutedEventArgs e)
        {
            SystemObject clientObject = SystemObject.GetInstance();
            Height -= clientObject.AddSize;
            Top += clientObject.AddSize;
            G_bg.Height -= clientObject.AddSize;
            seatLayout();
            btn_MaxWindow.Visibility = Visibility.Visible;
            btn_NormnlWindow.Visibility = Visibility.Collapsed;
            WPFMessage.MessageHelper.SendMessage(clientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.MoveDown, (clientObject.AddSize).ToString());
            WeiCharOperationWindowObject.GetInstance().Window.WinReset();
        }
    }
}
