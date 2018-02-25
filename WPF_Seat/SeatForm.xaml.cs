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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Collections;
using WPF_Seat.Code;
using System.IO;
using System.Windows.Ink;
using System.Windows.Media.Effects;
using SeatClient.OperateResult;
using SeatManage.ClassModel;
using SeatManage.Bll;
using WPF_Seat.MyControl;
using SeatManage.SeatManageComm;

namespace WPF_Seat
{
    /// <summary>
    /// SeatForm.xaml 的交互逻辑
    /// </summary>
    public partial class SeatForm : Window
    {
        static SeatForm seatform = null;
        static object _obj = new object();

        public static SeatForm GetInstance()
        {
            if (seatform == null)
            {
                lock (_obj)
                {
                    if (seatform == null)
                    {
                        seatform = new SeatForm();
                    }
                }
            }
            return seatform;
        }

        private SeatLayout _SeatLayout = null;
        SeatClient.OperateResult.SystemObject clientobject = SystemObject.GetInstance();
        SeatFormImageBrush _MyImageBrush = null;
        FormCloseCountdown countDownSeconds = null;
        double scaleX;//宽度的比值
        double scaleY;//高度的比值 
        double moveX = 0;
        double moveY = 0;
        /// <summary>
        /// 小的层
        /// </summary>
        System.Windows.Shapes.Rectangle areaSimle;

        FromResolution frmResolution = null;

        private SeatForm()
        {
            _MyImageBrush = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage);

            InitializeComponent();
            frmResolution = FromResolution.GetFrmResolution(clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X);
            this.DataContext = frmResolution;
            InitialControlBackground();
        }

        void InitialControlBackground()
        {
            this.Background = _MyImageBrush.SeatFormBackImage;
            btnBack.BorderThickness = new Thickness(0);
            btnBack.BorderBrush = null;
            btnBack.Background = _MyImageBrush.BtnExit;
        }

        /// <summary>
        /// 初始化界面，计算比例
        /// </summary>
        public void DrowSeatUsedInfo()
        {
            countDownSeconds = new FormCloseCountdown(60);
            countDownSeconds.EventCountdown += new EventHandler(countDownSeconds_EventCountdown);
            drowSeatLayout();
        }

        private void drowSeatLayout()
        {
            _SeatLayout = EnterOutOperate.GetRoomSeatLayOut(clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
            seatLayout();
        }

        void countDownSeconds_EventCountdown(object sender, EventArgs e)
        {
            FormCloseCountdown countdownseconds = sender as FormCloseCountdown;
            frmResolution.ViewModel.CountDownSeconds = countdownseconds.CountdownSceonds;
            if (countdownseconds.CountdownSceonds <= 0)
            {
                clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Exit;//窗体选座倒计时关闭时间到了，流程结束
                try
                {
                    // this.Hide();
                    this.Dispatcher.Invoke(new Action(() =>
                    {
                        this.Hide();
                    }));
                }
                catch
                { }
            }
        }
        public new void Hide()
        {
            countDownSeconds.Stop();
            countDownSeconds.EventCountdown -= new EventHandler(countDownSeconds_EventCountdown);
            frmResolution.ViewModel.SeatAmcountFree = 0;
            frmResolution.ViewModel.SeatAmcountUsed = 0;
            frmResolution.ViewModel.SeatAmcountAll = 0;
            frmResolution.ViewModel.RoomName = "";
            base.Hide();
        }

        void seatLayout()
        {
            this.Thumbnail.Children.Clear();
            this.CanvaseSeat.Children.Clear();//座位图中移除所有子元素


            int SeatWidth = 24 * _SeatLayout.SeatCol;
            int SeatHeight = 24 * _SeatLayout.SeatRow;

            this.CanvaseSeat.Width = SeatWidth;
            this.CanvaseSeat.Height = SeatHeight;
            double areaScaleX = SeatWidth / SeatWindow.Width;
            double areaScaleY = SeatHeight / SeatWindow.Height;
            if (SeatWidth >= SeatHeight)
            {
                scaleX = SeatWidth / Thumbnail.Width;
                scaleY = SeatWidth / Thumbnail.Height;
                moveY = (SeatWidth - SeatHeight) / 2 / scaleY;
            }
            else
            {
                scaleX = SeatHeight / Thumbnail.Width;
                scaleY = SeatHeight / Thumbnail.Height;
                moveX = (SeatHeight - SeatWidth) / 2 / scaleX;
            }
            //scaleX = SeatWidth / Thumbnail.Width;
            //scaleY = SeatHeight / Thumbnail.Height;

            try
            {
                Canvas.SetLeft(CanvaseSeat, 0);
                Canvas.SetTop(CanvaseSeat, 0);
            }
            catch
            { }

            #region 布局座位
            Code.GetSeatNoteImage getImage = new GetSeatNoteImage();
            ReadingRoomInfo roomInfo = clientobject.EnterOutLogData.Student.AtReadingRoom;
            frmResolution.ViewModel.SeatAmcountAll = _SeatLayout.Seats.Keys.Count;
            frmResolution.ViewModel.SeatAmcountFree = 0;
            frmResolution.ViewModel.RoomName = roomInfo.Name;
            DateTime startTime = DateTime.Now;
            foreach (Seat seat in _SeatLayout.Seats.Values)
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
                seatBtn.Background = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgSeat;
                if (seat.HavePower)
                {
                    seatBtn.PowerImg = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgPower;
                }
                switch (seat.SeatUsedState)
                {
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        if (seat.IsSuspended)
                        {
                            seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgStopUse;
                            frmResolution.ViewModel.SeatAmcountAll -= 1;
                        }
                        else
                        {
                            seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_MouseLeftButtonUp);
                            frmResolution.ViewModel.SeatAmcountFree += 1;
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
                        seatBtn.ReaderBackground= SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgReader;
                        if (roomInfo.Setting.NoManagement.Used)
                        {
                            seatBtn.MouseLeftButtonUp += new MouseButtonEventHandler(seatBtn_WaitSeat);
                        }
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgReader;
                        seatBtn.ShowleaveImg = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgShortLeave;
                        break;
                    case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                        seatBtn.ReaderBackground = SeatFormImageBrush.GetInstance(clientobject.ClientSetting.DeviceSetting.BackImgage).ImgBook;
                        break;
                }
                seatBtn.lblSeatNo.RenderTransform = new RotateTransform(-seat.RotationAngle);
                seatBtn.lblSeatNo.RenderTransformOrigin = new Point(0.5, 0.5);
                seatBtn.RenderTransform = new RotateTransform(seat.RotationAngle);
                seatBtn.RenderTransformOrigin = new Point(0.5, 0.5);
                this.CanvaseSeat.Children.Add(seatBtn);
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
                this.Thumbnail.Children.Add(rec);
                Canvas.SetZIndex(rec, 0);
                Canvas.SetLeft(rec, thumbLeft + moveX);
                Canvas.SetTop(rec, thumbTop + moveY);
                #endregion
            }
            TimeSpan ts = DateTime.Now -startTime ;
            Console.WriteLine("执行时间："+ts.TotalSeconds);
            frmResolution.ViewModel.SeatAmcountUsed = frmResolution.ViewModel.SeatAmcountAll - frmResolution.ViewModel.SeatAmcountFree;
            #endregion

            #region 布局备注
            foreach (Note note in _SeatLayout.Notes)
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
                this.CanvaseSeat.Children.Add(element);
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
                            this.Thumbnail.Children.Add(br);
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
                            this.Thumbnail.Children.Add(rec);
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
                areaSimle.Width = (Thumbnail.Width - moveX * 2) / areaScaleX;//按比例缩小层的宽度
            }
            else
            {
                areaSimle.Width = Thumbnail.Width;
            }
            if (areaScaleY != 0)
            {
                areaSimle.Height = (Thumbnail.Height - moveY * 2) / areaScaleY; //按比例缩小层的高度
            }
            else
            {
                areaSimle.Height = Thumbnail.Height;
            }

            areaSimle.Fill = new SolidColorBrush(Colors.White);
            areaSimle.Opacity = 0.5;

            this.Thumbnail.Children.Add(areaSimle);
            Canvas.SetZIndex(areaSimle, 1000);
            Canvas.SetTop(areaSimle, moveY);
            Canvas.SetLeft(areaSimle, moveX);
            //小的范围层。最后创建，防止被遮盖
        }
        /// <summary>
        /// 读者等待座位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void seatBtn_WaitSeat(object sender, MouseButtonEventArgs e)
        {
            WaitSeatLogInfo lastWaitInfo = T_SM_SeatWaiting.GetListWaitLogByCardNo(clientobject.EnterOutLogData.EnterOutlog.CardNo, clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
            ReadingRoomInfo roomInfo = clientobject.EnterOutLogData.Student.AtReadingRoom;


            if (!string.IsNullOrEmpty(clientobject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                SeatManage.SeatClient.Tip.Tip_Framework tipForm = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.WaitSeatWithSeat, 7);
                countDownSeconds.Pause();
                tipForm.ShowDialog();
                countDownSeconds.Start();
                return;
            }

            if (lastWaitInfo != null && lastWaitInfo.SeatWaitTime.AddMinutes(roomInfo.Setting.NoManagement.OperatingInterval).CompareTo(ServiceDateTime.Now) >= 0)
            {
                SeatManage.SeatClient.Tip.Tip_Framework tipForm = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.WaitSeatFrequent, 7);
                countDownSeconds.Pause();
                tipForm.ShowDialog();
                countDownSeconds.Start();
                return;
            }
            SeatButton seatBtn = sender as SeatButton;
            SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
            {
                SetShortWarning warnForm = new SetShortWarning(seatBtn.ShortSeatNo);
                countDownSeconds.Pause();
                warnForm.ShowDialog();
                countDownSeconds.Start();
                if (warnForm.IsTrue)
                {
                    //初始化等待记录 记录ID需要待执行之后添加。
                    WaitSeatLogInfo waitInfo = new WaitSeatLogInfo();
                    waitInfo.CardNo = clientobject.EnterOutLogData.EnterOutlog.CardNo;
                    waitInfo.NowState = SeatManage.EnumType.LogStatus.Valid;
                    waitInfo.OperateType = SeatManage.EnumType.Operation.Reader;
                    waitInfo.WaitingState = SeatManage.EnumType.EnterOutLogType.Waiting;

                    EnterOutLogInfo seatUsingEnterOutInfo = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatBtn.SeatNo);
                    seatUsingEnterOutInfo.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                    seatUsingEnterOutInfo.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
                    seatUsingEnterOutInfo.Flag = SeatManage.EnumType.Operation.OtherReader;
                    seatUsingEnterOutInfo.TerminalNum = clientobject.ClientSetting.ClientNo;
                    seatUsingEnterOutInfo.Remark = string.Format("在{0} {1}号座位，被读者{2}在终端{3}设置为暂离并等待该座位", seatUsingEnterOutInfo.ReadingRoomName, seatUsingEnterOutInfo.ShortSeatNo, waitInfo.CardNo, clientobject.ClientSetting.ClientNo);

                    clientobject.EnterOutLogData.EnterOutlog = seatUsingEnterOutInfo;//要等待读者的暂离记录
                    clientobject.EnterOutLogData.WaitSeatLogModel = waitInfo;//等待记录 
                    this.Hide();
                }
                else
                {
                    T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }

            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
            {
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.SeatLocking, frmResolution.ViewModel.CountDownSeconds);//显示提示窗体
                tip.ShowDialog();

                drowSeatLayout();
            }
        }
        void seatBtn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SeatButton seatBtn = sender as SeatButton;
            SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
            {
                string roomName = frmResolution.ViewModel.RoomName;
                string seatNo = seatBtn.ShortSeatNo;
                TipForm_SelectSeatConfinmed tip = new TipForm_SelectSeatConfinmed(roomName, seatNo, frmResolution.ViewModel.CountDownSeconds);//显示提示窗体
                countDownSeconds.Pause();
                tip.ShowDialog();
                countDownSeconds.Start();
                if (tip.IsTrue)
                {
                    clientobject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                    clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位  
                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}手动选择，{1}，{2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, seatBtn.ShortSeatNo);
                    this.Hide();
                }
                else
                {
                    T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
            {
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.SeatLocking, frmResolution.ViewModel.CountDownSeconds);//显示提示窗体
                tip.ShowDialog();

                drowSeatLayout();
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.NotExists)
            {

            }
        }
        #region 页面操作逻辑
        private void Thumbnail_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            System.Windows.Point mousePoint = e.GetPosition(this.Thumbnail);
            double x = mousePoint.X - areaSimle.Width / 2;
            double y = mousePoint.Y - areaSimle.Height / 2;
            Canvas.SetLeft(areaSimle, x);
            Canvas.SetTop(areaSimle, y);

            System.Windows.Point imgPanelPonsition = new System.Windows.Point();
            imgPanelPonsition.X = (x - moveX) * scaleX;
            imgPanelPonsition.Y = (y - moveY) * scaleY;

            Canvas.SetLeft(CanvaseSeat, -imgPanelPonsition.X);
            Canvas.SetTop(CanvaseSeat, -imgPanelPonsition.Y);
            e.Handled = true;
        }
        //记录鼠标press时在球的焦点
        private System.Windows.Point mouseFocus = new System.Windows.Point();

        private System.Windows.Point ballPosition = new System.Windows.Point();
        //记录球跟随鼠标的逐帧动画
        private TranslateTransform transform = new TranslateTransform();

        private void MousePress(object sender, MouseButtonEventArgs e)
        {
            CanvaseSeat.RenderTransform = this.transform;
            CanvaseSeat.MouseUp += MouseRelease;

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
            ballPosition = Mouse.GetPosition(this.CanvaseSeat);
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
            seatImgPosition.X = Canvas.GetLeft(CanvaseSeat);
            seatImgPosition.Y = Canvas.GetTop(CanvaseSeat);
            this.CanvaseSeat.MouseLeftButtonUp += imgPanel_MouseLeftButtonUp;
            e.Handled = true;
        }

        private void imgPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isDown = false;
            this.CanvaseSeat.MouseMove += imgPanel_MouseMove;
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

                Canvas.SetLeft(CanvaseSeat, nowPosition.X);
                Canvas.SetTop(CanvaseSeat, nowPosition.Y);
            }
            e.Handled = true;
        }
        #endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (clientobject.ClientSetting.DeviceSetting.UsingEnterNoForSeat)
            {
                Button btnKeyboard = new Button();
                btnKeyboard.Name = "btnKeyboard";
                btnKeyboard.BorderThickness = new Thickness(0);
                btnKeyboard.BorderBrush = null;
                btnKeyboard.Background = _MyImageBrush.KeyBoardImage;
                btnKeyboard.Click += this.btnKeyboard_Click;
                this.mainCanvas.Children.Add(btnKeyboard);
                double top = Canvas.GetTop(btnBack) - 5;
                Canvas.SetTop(btnKeyboard, top + 5);
                btnKeyboard.SetBinding(Canvas.LeftProperty, new Binding("BtnKeyboard.Left"));
                btnKeyboard.SetBinding(Canvas.WidthProperty, new Binding("BtnKeyboard.Width"));
                btnKeyboard.SetBinding(Canvas.HeightProperty, new Binding("BtnKeyboard.Height"));
            }
        }
        private void btnKeyboard_Click(object sender, RoutedEventArgs e)
        {
            NoKeyboard kb = new NoKeyboard(frmResolution.ViewModel.CountDownSeconds);
            countDownSeconds.Pause();
            kb.ShowDialog();
            countDownSeconds.Start();
            if (!string.IsNullOrEmpty(kb.SeatNo))
            {
                string roomName = frmResolution.ViewModel.RoomName;
                string seatNo = SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, kb.SeatNo);
                TipForm_SelectSeatConfinmed tip = new TipForm_SelectSeatConfinmed(roomName, seatNo, frmResolution.ViewModel.CountDownSeconds);//显示提示窗体
                countDownSeconds.Pause();
                tip.ShowDialog();
                countDownSeconds.Start();
                if (tip.IsTrue)
                {
                    clientobject.EnterOutLogData.EnterOutlog.SeatNo = kb.SeatNo;
                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}输入座位号选择，{1}，{2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, seatNo);
                    clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位 
                    this.Hide();
                }
                else
                {
                    T_SM_Seat.UnLockSeat(kb.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            //点击返回，流程为返回阅览室选择界面
            clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.Back;
            this.Hide();
            countDownSeconds.EventCountdown -= new EventHandler(countDownSeconds_EventCountdown);
        }


        public void windowInit()
        {

        }
    }
}
