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
using SeatManage.ClassModel;
using SeatClientV2.OperateResult;
using SeatClientV2.Code;
using SeatManage.SeatManageComm;
using SeatClientV2.MyUserControl;

namespace SeatClientV2
{
    /// <summary>
    /// RoomSeatWindow.xaml 的交互逻辑
    /// </summary>
    public partial class RoomSeatWindow : Window
    {
        public RoomSeatWindow()
        {
            InitializeComponent();
            seatLayout();
            viewModel.DrowSeatLayout += new EventHandler(viewModel_DrowSeatLayout);
            this.DataContext = viewModel;
            viewModel.CloseTime = 60;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
        }
        void viewModel_DrowSeatLayout(object sender, EventArgs e)
        {
            seatLayout();
        }
        public ViewModel.RoomSeatWindow_ViewModel viewModel = new ViewModel.RoomSeatWindow_ViewModel();


        System.Windows.Shapes.Rectangle areaSimle;
        double scaleX;//宽度的比值
        double scaleY;//高度的比值 
        double moveX = 0;
        double moveY = 0;
        void seatLayout()
        {
            this.canvas_Thumbnail.Children.Clear();
            this.canvas_Seat.Children.Clear();//座位图中移除所有子元素

            viewModel.AllSeatCount = viewModel.Layout.Seats.Count;

            int SeatWidth = 24 * viewModel.Layout.SeatCol;
            int SeatHeight = 24 * viewModel.Layout.SeatRow;

            this.canvas_Seat.Width = SeatWidth;
            this.canvas_Seat.Height = SeatHeight;
            double areaScaleX = SeatWidth / SeatWindow.Width;
            double areaScaleY = SeatHeight / SeatWindow.Height;
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
            //scaleX = SeatWidth / canvas_Thumbnail.Width;
            //scaleY = SeatHeight / canvas_Thumbnail.Height;

            try
            {
                Canvas.SetLeft(canvas_Seat, 0);
                Canvas.SetTop(canvas_Seat, 0);
            }
            catch
            { }

            #region 布局座位
            Code.GetSeatNoteImage getImage = new GetSeatNoteImage();
            ReadingRoomInfo roomInfo = viewModel.clientObject.EnterOutLogData.Student.AtReadingRoom;
            DateTime startTime = DateTime.Now;
            foreach (Seat seat in viewModel.Layout.Seats.Values)
            {

                seat.ShortSeatNo = SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, seat.SeatNo);
                #region 布局实际图,
                double canLeft = 24 * (double)seat.PositionX;
                double canTop = 24 * (double)seat.PositionY;
                SeatButton seatBtn = new SeatButton();
                seatBtn.Width = 48;
                seatBtn.Height = 48;
                seatBtn.ShortSeatNo = seat.ShortSeatNo;
                seatBtn.SeatNo = seat.SeatNo;
                seatBtn.Background = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgSeat;
                if (seat.HavePower)
                {
                    seatBtn.PowerImg = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgPower;
                }
                switch (seat.SeatUsedState)
                {
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        if (seat.IsSuspended)
                        {
                            seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgStopUse;
                            viewModel.AllSeatCount -= 1;
                        }
                        else
                        {
                            seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_MouseLeftButtonUp);
                            viewModel.LastSeatCount += 1;
                            seatBtn.lblSeatNo.Visibility = System.Windows.Visibility.Visible;
                        }
                        //frmResolution.ViewModel.SeatAmcountFree += 1;//遇到空闲座位，ViewModel空闲座位数+1
                        //seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_MouseLeftButtonUp);
                        break;
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                    case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgReader;
                        if (roomInfo.Setting.NoManagement.Used)
                        {
                            seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_WaitSeat);
                        }
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgReader;
                        seatBtn.ShowleaveImg = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgShortLeave;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                        seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(viewModel.clientObject.ClientSetting.DeviceSetting.BackImgage).ImgBook;
                        if (roomInfo.Setting.SeatBespeak.SelectBespeakSeat)
                        {
                            seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_SelectBookingSeat);
                        }
                        break;
                }
                seatBtn.lblSeatNo.RenderTransform = new RotateTransform(-seat.RotationAngle);
                seatBtn.lblSeatNo.RenderTransformOrigin = new Point(0.5, 0.5);
                seatBtn.RenderTransform = new RotateTransform(seat.RotationAngle);
                seatBtn.RenderTransformOrigin = new Point(0.5, 0.5);
                this.canvas_Seat.Children.Add(seatBtn);
                Canvas.SetLeft(seatBtn, canLeft);
                Canvas.SetTop(seatBtn, canTop);
                #endregion

                #region 布局缩略图
                Rectangle rec = new Rectangle();
                rec.Width = 36 / scaleX;
                rec.Height = 36 / scaleY;
                double thumbLeft = (double)(seat.PositionX * (double)24 + 6) / scaleX;
                double thumbTop = (double)(seat.PositionY * (double)24 + 6) / scaleY;
                switch (seat.SeatUsedState)
                {
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        rec.Fill = new SolidColorBrush(Color.FromRgb(220, 220, 220));
                        if (seat.IsSuspended)
                        {
                            rec.Fill = new SolidColorBrush(Color.FromRgb(234, 38, 52));
                        }
                        break;
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                    case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                    case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        rec.Fill = new SolidColorBrush(Color.FromRgb(234, 38, 52));
                        break;
                }
                rec.RenderTransformOrigin = new Point(0.5, 0.5);
                rec.RenderTransform = new RotateTransform(seat.RotationAngle);
                this.canvas_Thumbnail.Children.Add(rec);
                Canvas.SetZIndex(rec, 0);
                Canvas.SetLeft(rec, thumbLeft + moveX);
                Canvas.SetTop(rec, thumbTop + moveY);
                #endregion
            }
            TimeSpan ts = DateTime.Now - startTime;
            Console.WriteLine("执行时间：" + ts.TotalSeconds);
            //frmResolution.ViewModel.SeatAmcountUsed = frmResolution.ViewModel.SeatAmcountAll - frmResolution.ViewModel.SeatAmcountFree;
            #endregion

            #region 布局备注
            foreach (Note note in viewModel.Layout.Notes)
            {
                #region 实际图

                NoteElement element = new NoteElement();
                if (note.Type == SeatManage.EnumType.OrnamentType.Table)
                {
                    element.BorderThickness = new Thickness(1);
                    element.BorderBrush = new SolidColorBrush(Colors.Black);
                }
                //element.BorderThickness = new Thickness(1);
                //element.BorderBrush = new SolidColorBrush(Colors.Yellow);
                element.Width = note.BaseWidth * 24;
                element.Height = note.BaseHeight * 24;
                element.Notes = note.Remark;
                element.RenderTransformOrigin = new Point(0.5, 0.5);
                element.RenderTransform = new RotateTransform(note.RotationAngle);
                element.NoteType = note.Type;
                Canvas.SetLeft(element.lbRemark, element.Width / 2 - element.lbRemark.Width / 2);
                this.canvas_Seat.Children.Add(element);
                double canLeft = 24 * (double)note.PositionX;
                double canTop = 24 * (double)note.PositionY;
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
                            br.CornerRadius = new CornerRadius(note.BaseWidth * (double)12 / scaleX);
                            br.BorderThickness = new Thickness(note.BaseWidth * (double)12 / scaleX);
                            br.BorderBrush = new SolidColorBrush(Color.FromRgb(231, 186, 100));
                            double thumbLeft = (double)(24 / scaleX) * (double)note.PositionX;
                            double thumbTop = (double)(24 / scaleY) * (double)note.PositionY;
                            this.canvas_Thumbnail.Children.Add(br);
                            Canvas.SetLeft(br, thumbLeft + moveX);
                            Canvas.SetTop(br, thumbTop + moveY);
                        }
                        break;
                    default:
                        {
                            Rectangle rec = new Rectangle();
                            rec.Width = (double)24 * note.BaseWidth / scaleX;
                            rec.Height = (double)24 * note.BaseHeight / scaleY;
                            double thumbLeft = (double)(24 / scaleX) * (double)note.PositionX;
                            double thumbTop = (double)(24 / scaleY) * (double)note.PositionY;
                            rec.RenderTransformOrigin = new Point(0.5, 0.5);
                            rec.RenderTransform = new RotateTransform(note.RotationAngle);
                            rec.Fill = new SolidColorBrush(Color.FromRgb(231, 186, 100));
                            this.canvas_Thumbnail.Children.Add(rec);
                            Canvas.SetLeft(rec, thumbLeft + moveX);
                            Canvas.SetTop(rec, thumbTop + moveY);
                        }
                        break;
                }

                #endregion
            }
            #endregion


            areaSimle = new System.Windows.Shapes.Rectangle();
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

            this.canvas_Thumbnail.Children.Add(areaSimle);
            Canvas.SetZIndex(areaSimle, 1000);
            Canvas.SetTop(areaSimle, moveY);
            Canvas.SetLeft(areaSimle, moveX);
            //小的范围层。最后创建，防止被遮盖
        }
        /// <summary>
        /// 选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void seatBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.SelectSeat(sender as SeatButton))
            {
                SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                this.Close();
            }
        }
        /// <summary>
        /// 等待
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void seatBtn_WaitSeat(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.WaitSeat(sender as SeatButton))
            {
                SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.WaitSeat;
                this.Close();
            }
        }

        /// <summary>
        /// 选择预约座位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void seatBtn_SelectBookingSeat(object sender, MouseButtonEventArgs e)
        {
            if (viewModel.SelectBookingSeat(sender as SeatButton))
            {
                SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                this.Close();
            }
        }

        #region 页面操作逻辑
        private void Thumbnail_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point mousePoint = e.GetPosition(this.canvas_Thumbnail);
            double x = mousePoint.X - areaSimle.Width / 2;
            double y = mousePoint.Y - areaSimle.Height / 2;
            Canvas.SetLeft(areaSimle, x);
            Canvas.SetTop(areaSimle, y);

            System.Windows.Point imgPanelPonsition = new System.Windows.Point();
            imgPanelPonsition.X = (x - moveX) * scaleX;
            imgPanelPonsition.Y = (y - moveY) * scaleY;

            Canvas.SetLeft(canvas_Seat, -imgPanelPonsition.X);
            Canvas.SetTop(canvas_Seat, -imgPanelPonsition.Y);
            e.Handled = true;
        }
        //记录鼠标press时在球的焦点
        private System.Windows.Point mouseFocus = new System.Windows.Point();

        private System.Windows.Point ballPosition = new System.Windows.Point();
        //记录球跟随鼠标的逐帧动画
        private TranslateTransform transform = new TranslateTransform();

        private void MousePress(object sender, MouseButtonEventArgs e)
        {
            canvas_Seat.RenderTransform = this.transform;
            canvas_Seat.MouseUp += MouseRelease;

            mouseFocus = Mouse.GetPosition(this.SeatWindow);
            CompositionTarget.Rendering += MouseDrag;
            e.Handled = true;
        }
        /// <summary>
        /// 鼠标拖动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseDrag(object sender, EventArgs e)
        {
            ballPosition = Mouse.GetPosition(this.canvas_Seat);
            transform.X = ballPosition.X - mouseFocus.X;
            transform.Y = ballPosition.Y - mouseFocus.Y;
        }


        /// <summary>
        /// 鼠标放开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void MouseRelease(object sender, EventArgs e)
        {
            CompositionTarget.Rendering -= MouseDrag;
        }

        private void mouseOut(object sender, MouseEventArgs e)
        {
            CompositionTarget.Rendering -= MouseDrag;
            e.Handled = true;
        }

        bool isDown = false;//鼠标状态
        System.Windows.Point beforePoint = new System.Windows.Point();//记录鼠标点击下的时候相对于控件的位置
        System.Windows.Point seatImgPosition = new System.Windows.Point();//鼠标点下的时候IMG的位置
        private void imgPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            isDown = true;
            beforePoint = e.GetPosition(this.SeatWindow);//当前鼠标相对于包着IMG的位置
            seatImgPosition.X = Canvas.GetLeft(canvas_Seat);
            seatImgPosition.Y = Canvas.GetTop(canvas_Seat);
            this.canvas_Seat.MouseLeftButtonUp += imgPanel_MouseLeftButtonUp;
            e.Handled = true;
        }

        private void imgPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            this.canvas_Seat.MouseMove += imgPanel_MouseMove;
            e.Handled = true;
        }

        private void imgPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isDown)
            {
                System.Windows.Point position = e.GetPosition(this.SeatWindow);//鼠标移动的当前位置
                System.Windows.Point nowPosition = new System.Windows.Point();   //模块的新位置
                System.Windows.Point movePosition = new System.Windows.Point(); //鼠标移动的距离
                movePosition.X = position.X - beforePoint.X;
                movePosition.Y = position.Y - beforePoint.Y;

                nowPosition.X = seatImgPosition.X + movePosition.X;
                nowPosition.Y = seatImgPosition.Y + movePosition.Y;

                Canvas.SetLeft(canvas_Seat, nowPosition.X);
                Canvas.SetTop(canvas_Seat, nowPosition.Y);
            }
            e.Handled = true;
        }
        #endregion
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
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
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
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
            SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;
            this.Close();
        }
        /// <summary>
        /// 返回按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = "";
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Back;
            this.Close();
        }
        /// <summary>
        /// 随机选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_random_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.LastSeatCount == 0)
            {
                PopupWindow popWindow = new PopupWindow(SeatManage.EnumType.TipType.ReadingRoomFull);
                viewModel.CountDown.Pause();
                popWindow.ShowDialog();
                viewModel.CountDown.Start();
            }
            else
            {
                viewModel.RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.AutomaticMode;
                this.Close();
            }
        }
        /// <summary>
        /// 键盘选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_keyboard_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.KeyboardSelectSeat())
            {
                SeatClientV2.OperateResult.SystemObject clientObject = SeatClientV2.OperateResult.SystemObject.GetInstance();
                clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                viewModel.RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.ManualMode;
                this.Close();
            }
        }
    }
}
