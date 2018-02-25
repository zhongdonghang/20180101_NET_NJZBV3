using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatClientV3.MyUserControl;
using SeatClientV3.FunWindow;
using SeatClientV3.OperateResult;

namespace SeatClientV3.ViewModel
{
    public class RoomSeatWindow_ViewModel : INotifyPropertyChanged
    {
        public RoomSeatWindow_ViewModel()
        {
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
            ReadingRoomNo = clientObject.EnterOutLogData.Student.AtReadingRoom.No;
            if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatChooseMethod.DefaultChooseMethod == SeatManage.EnumType.SelectSeatMode.OptionalMode)
            {
                RandomBtn = "Visible";
            }
            if (clientObject.ClientSetting.DeviceSetting.UsingEnterNoForSeat)
            {
                KeybroadBtn = "Visible";
            }
            //GetSeatLayout();
            if (DrowSeatLayout != null)
            {
                DrowSeatLayout(this, new EventArgs());
            }
        }
        /// <summary>
        /// 获取座位使用情况
        /// </summary>
        public void GetSeatLayout()
        {
            Layout = EnterOutOperate.GetRoomSeatLayOut(clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
        }
        public event EventHandler DrowSeatLayout;


        public SeatClientV3.OperateResult.SystemObject clientObject
        {
            get { return SeatClientV3.OperateResult.SystemObject.GetInstance(); }
        }
        private int _CloseTime = 0;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
        }
        FormCloseCountdown _CountDown = null;
        /// <summary>
        /// 窗体关闭倒计时
        /// </summary>
        public FormCloseCountdown CountDown
        {
            get { return _CountDown; }
            set { _CountDown = value; }
        }
        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }

        private string _ReadingRoomNo = "";
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _ReadingRoomNo; }
            set { _ReadingRoomNo = value; OnPropertyChanged("ReadingRoomNo"); }
        }
        private string _ReadingRoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get { return _ReadingRoomName; }
            set { _ReadingRoomName = value; OnPropertyChanged("ReadingRoomName"); OnPropertyChanged("SeatInfo"); }
        }
        private string _RandomBtn = "Collapsed";
        /// <summary>
        /// 随机按钮
        /// </summary>
        public string RandomBtn
        {
            get { return _RandomBtn; }
            set { _RandomBtn = value; OnPropertyChanged("RandomBtn"); }
        }
        public string Position
        {
            get { return _Layout.Position; }
        }
        private string _KeybroadBtn = "Collapsed";
        /// <summary>
        /// 输入座位号选座
        /// </summary>
        public string KeybroadBtn
        {
            get { return _KeybroadBtn; }
            set { _KeybroadBtn = value; }
        }
        private SeatManage.ClassModel.SeatLayout _Layout = new SeatManage.ClassModel.SeatLayout();
        /// <summary>
        /// 座位布局图
        /// </summary>
        public SeatManage.ClassModel.SeatLayout Layout
        {
            get { return _Layout; }
            set { _Layout = value; }
        }
        private int _AllSeatCount = 0;
        /// <summary>
        /// 座位总数
        /// </summary>
        public int AllSeatCount
        {
            get { return _AllSeatCount; }
            set { _AllSeatCount = value; OnPropertyChanged("LastSeatInfo"); }
        }
        private int _LastSeatCount = 0;
        /// <summary>
        /// 剩余座位数
        /// </summary>
        public int LastSeatCount
        {
            get { return _LastSeatCount; }
            set { _LastSeatCount = value; OnPropertyChanged("LastSeatInfo"); }
        }
        public string LastSeatInfo
        {
            get { return _LastSeatCount + "/" + _AllSeatCount; }
        }
        private SeatManage.EnumType.SelectSeatMode _RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.None;
        /// <summary>
        /// 选座方式
        /// </summary>
        public SeatManage.EnumType.SelectSeatMode RoomSelectSeatMethod
        {
            get { return _RoomSelectSeatMethod; }
            set { _RoomSelectSeatMethod = value; }
        }
        #region INotifyPropertyChanged 成员
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        /// <summary>
        /// 等待座位
        /// </summary>
        /// <returns></returns>
        public bool WaitSeat(SeatButton seatBtn)
        {
            WaitSeatLogInfo lastWaitInfo = T_SM_SeatWaiting.GetListWaitLogByCardNo(clientObject.EnterOutLogData.EnterOutlog.CardNo, clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
            ReadingRoomInfo roomInfo = clientObject.EnterOutLogData.Student.AtReadingRoom;


            if (!string.IsNullOrEmpty(clientObject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.WaitSeatWithSeat);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }

            if (lastWaitInfo != null && lastWaitInfo.SeatWaitTime.AddMinutes(roomInfo.Setting.NoManagement.OperatingInterval).CompareTo(ServiceDateTime.Now) >= 0)
            {
                clientObject.EnterOutLogData.Student.WaitSeatLog = lastWaitInfo;
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.WaitSeatFrequent);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
            SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
            {
                clientObject.EnterOutLogData.WaitSeatLogModel = new WaitSeatLogInfo() { SeatNo = seatBtn.ShortSeatNo };
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.WaitSeatConfirm);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                if (popWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    //初始化等待记录 记录ID需要待执行之后添加。
                    WaitSeatLogInfo waitInfo = new WaitSeatLogInfo();
                    waitInfo.CardNo = clientObject.EnterOutLogData.EnterOutlog.CardNo;
                    waitInfo.NowState = SeatManage.EnumType.LogStatus.Valid;
                    waitInfo.OperateType = SeatManage.EnumType.Operation.Reader;
                    waitInfo.WaitingState = SeatManage.EnumType.EnterOutLogType.Waiting;

                    EnterOutLogInfo seatUsingEnterOutInfo = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatBtn.SeatNo);
                    seatUsingEnterOutInfo.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                    seatUsingEnterOutInfo.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
                    seatUsingEnterOutInfo.Flag = SeatManage.EnumType.Operation.OtherReader;
                    seatUsingEnterOutInfo.TerminalNum = clientObject.ClientSetting.ClientNo;
                    seatUsingEnterOutInfo.Remark = string.Format("在{0} {1}号座位，被读者{2}在终端{3}设置为暂离并等待该座位", seatUsingEnterOutInfo.ReadingRoomName, seatUsingEnterOutInfo.ShortSeatNo, waitInfo.CardNo, clientObject.ClientSetting.ClientNo);

                    clientObject.EnterOutLogData.EnterOutlog = seatUsingEnterOutInfo;//要等待读者的暂离记录
                    clientObject.EnterOutLogData.WaitSeatLogModel = waitInfo;//等待记录 

                    return true;
                }
                else
                {
                    T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }

            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsLocked);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
            }
            return false;
        }
        /// <summary>
        /// 选座座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectSeat(SeatButton seatBtn)
        {
            SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
            {
                clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SelectSeatConfirm);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                if (popWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
                    clientObject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.TerminalNum = clientObject.ClientSetting.ClientNo;
                    clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位  
                    clientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}手动选择，{1}，{2}号座位", clientObject.ClientSetting.ClientNo, clientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatBtn.ShortSeatNo);
                    RoomSelectSeatMethod = SeatManage.EnumType.SelectSeatMode.ManualMode;
                    return true;
                }
                else
                {
                    T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsLocked);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.NotExists)
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
            }
            return false;
        }
        /// <summary>
        /// 键盘选座
        /// </summary>
        /// <returns></returns>
        public bool KeyboardSelectSeat()
        {
            KeyboardWindow keyboardWindow = new KeyboardWindow();
            CountDown.Pause();
            keyboardWindow.ShowDialog();
            CountDown.Start();
            if (!string.IsNullOrEmpty(keyboardWindow.viewModel.SeatNo))
            {
                clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = keyboardWindow.viewModel.SeatNo;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SelectSeatConfirm);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                if (popWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
                    clientObject.EnterOutLogData.EnterOutlog.SeatNo = keyboardWindow.viewModel.LongSeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.TerminalNum = clientObject.ClientSetting.ClientNo;
                    clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位  
                    clientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}输入座位号选择，{1}，{2}号座位", clientObject.ClientSetting.ClientNo, clientObject.EnterOutLogData.Student.AtReadingRoom.Name, clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                    return true;
                }
                else
                {
                    T_SM_Seat.UnLockSeat(keyboardWindow.viewModel.LongSeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }
            }
            return false;
        }
        /// <summary>
        /// 选择预约的座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectBookingSeat(SeatButton seatBtn)
        {
            DateTime datetimeNow = SeatManage.Bll.ServiceDateTime.Now;
            List<SeatManage.ClassModel.BespeakLogInfo> bespeakLogList = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatBtn.SeatNo, datetimeNow.Date);
            if (bespeakLogList.Count > 0)
            {
                if (bespeakLogList[0].BsepeakTime.AddMinutes(-double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= datetimeNow)
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsBespaeked);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    return false;
                }
                else
                {
                    clientObject.EnterOutLogData.BespeakLogInfo = bespeakLogList[0];
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SelectBespeakSeatConfrim);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    if (popWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Failed)
                    {
                        clientObject.EnterOutLogData.BespeakLogInfo = null;
                        return false;
                    }
                }
            }
            else
            {
                SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
                if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
                {
                    clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SelectBespeakSeatConfrim);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();

                    if (popWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Failed)
                    {
                        T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                        return false;
                    }
                }
                else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsLocked);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    return false;
                }
                else if (lockseat == SeatManage.EnumType.SeatLockState.NotExists)
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    return false;
                }
            }
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
            clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
            clientObject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
            clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
            clientObject.EnterOutLogData.EnterOutlog.TerminalNum = clientObject.ClientSetting.ClientNo;
            clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位  
            clientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}手动选择，{1}，{2}号座位", clientObject.ClientSetting.ClientNo, clientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatBtn.ShortSeatNo);
            return true;
        }
    }
}
