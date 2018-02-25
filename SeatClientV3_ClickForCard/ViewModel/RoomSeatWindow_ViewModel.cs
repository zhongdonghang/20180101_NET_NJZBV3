using System;
using System.Collections.Generic;
using System.ComponentModel;
using SeatClientV3.MyUserControl;
using SeatClientV3.OperateResult;
using SeatManage.Bll;
using System.Windows.Forms;
using SeatClientV3.Code;
using SeatClientV3.WindowObject;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;

namespace SeatClientV3.ViewModel
{
    public class RoomSeatWindow_ViewModel : INotifyPropertyChanged
    {
        public event EventHandler RoomWindowClose;
        public RoomSeatWindow_ViewModel(string roomNo)
        {
            WindowHeight = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            //WindowHeight = ClientObject.AutoAddSize ? ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y + ClientObject.AddSize : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            //WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            //WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            //WindowTop = ClientObject.AutoAddSize ? ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y - ClientObject.AddSize : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            ReadingRoomName = ClientObject.ReadingRoomList[roomNo].Name;
            ReadingRoomNo = ClientObject.ReadingRoomList[roomNo].No;
            if (ClientObject.ReadingRoomList[roomNo].Setting.SeatChooseMethod.DefaultChooseMethod == SelectSeatMode.OptionalMode)
            {
                RandomBtn = "Visible";
            }
            if (ClientObject.ClientSetting.DeviceSetting.UsingEnterNoForSeat)
            {
                KeybroadBtn = "Visible";
            }
        }

        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
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
        private SeatLayout _Layout = new SeatLayout();
        /// <summary>
        /// 座位布局图
        /// </summary>
        public SeatLayout Layout
        {
            get { return _Layout; }
            set { _Layout = value; OnPropertyChanged("Position"); }
        }
        private int _AllSeatCount = 0;
        /// <summary>
        /// 座位总数
        /// </summary>
        public int AllSeatCount
        {
            get { return _AllSeatCount; }
            set { _AllSeatCount = value; }
        }
        private int _LastSeatCount = 0;
        /// <summary>
        /// 剩余座位数
        /// </summary>
        public int LastSeatCount
        {
            get { return _LastSeatCount; }
            set { _LastSeatCount = value; }
        }
        /// <summary>
        /// 座位布局
        /// </summary>
        public Dictionary<string, SeatUC_ViewModel> SeatLayoutList
        {
            get { return _seatLayoutList; }
            set { _seatLayoutList = value; }
        }
        /// <summary>
        /// 装饰物布局
        /// </summary>
        public List<NoteUC_ViewModel> NoteLayoutList
        {
            get { return _noteLayoutList; }
            set { _noteLayoutList = value; }
        }

        /// <summary>
        /// 座位布局
        /// </summary>
        private Dictionary<string, SeatUC_ViewModel> _seatLayoutList = new Dictionary<string, SeatUC_ViewModel>();

        private List<NoteUC_ViewModel> _noteLayoutList = new List<NoteUC_ViewModel>();

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
        /// 获取布局图
        /// </summary>
        public void GetLayout()
        {
            Layout = ClientObject.ReadingRoomList[ReadingRoomNo].SeatList;
            bool isCanSelectBespeak = ClientObject.ReadingRoomList[ReadingRoomNo].Setting.SeatBespeak.Used && ClientObject.ReadingRoomList[ReadingRoomNo].Setting.SeatBespeak.SelectBespeakSeat;
            bool isCanWait = ClientObject.ReadingRoomList[ReadingRoomNo].Setting.NoManagement.Used;
            foreach (KeyValuePair<string, Seat> seat in Layout.Seats)
            {
                SeatUC_ViewModel seatVM = new SeatUC_ViewModel();
                seat.Value.ShortSeatNo = SeatComm.SeatNoToShortSeatNo(ClientObject.ReadingRoomList[ReadingRoomNo].Setting.SeatNumAmount, seat.Value.SeatNo);
                seatVM.IsCanSelectBespeakSeat = isCanSelectBespeak;
                seatVM.IsCanWaitSeat = isCanWait;
                seatVM.IsBespeak = false;
                seatVM.IsPower = seat.Value.HavePower;
                seatVM.IsShortLeave = false;
                seatVM.IsStop = seat.Value.IsSuspended;
                seatVM.IsUsing = false;
                seatVM.IsWaiting = false;
                seatVM.LongSeatNo = seat.Value.SeatNo;
                seatVM.ReadingRoomNo = seat.Value.ReadingRoomNum;
                seatVM.ShortSeatNo = seat.Value.ShortSeatNo;
                seatVM.SelectSeat += RoomSeatWindow_SelectSeat;
                seatVM.SelectBespeakSeat += RoomSeatWindow_SelectBespeakSeat;
                seatVM.WaitSeat += RoomSeatWindow_WaitSeat;
                _seatLayoutList.Add(seat.Key, seatVM);
            }
        }
        /// <summary>
        /// 获取座位使用情况
        /// </summary>
        public void GetUsingState()
        {
            LastSeatCount = AllSeatCount;
            Layout = EnterOutOperate.GetRoomSeatLayOut(ReadingRoomNo);
            foreach (KeyValuePair<string, Seat> seat in Layout.Seats)
            {
                _seatLayoutList[seat.Key].IsBespeak = false;
                _seatLayoutList[seat.Key].IsShortLeave = false;
                _seatLayoutList[seat.Key].IsUsing = false;
                //_seatLayoutList[seat.Key].IsWaiting = false;
                switch (seat.Value.SeatUsedState)
                {
                    case EnterOutLogType.Leave:
                        _seatLayoutList[seat.Key].IsUsing = false;
                        _seatLayoutList[seat.Key].IsBespeak = false;
                        _seatLayoutList[seat.Key].IsShortLeave = false;
                        _seatLayoutList[seat.Key].IsWaiting = false;
                        break;
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.WaitingSuccess:
                        _seatLayoutList[seat.Key].IsUsing = true;
                        _seatLayoutList[seat.Key].IsBespeak = false;
                        _seatLayoutList[seat.Key].IsShortLeave = false;
                        _seatLayoutList[seat.Key].IsWaiting = false;
                        LastSeatCount--;
                        break;
                    case EnterOutLogType.ShortLeave:
                        _seatLayoutList[seat.Key].IsUsing = true;
                        _seatLayoutList[seat.Key].IsShortLeave = true;
                        _seatLayoutList[seat.Key].IsBespeak = false;
                        _seatLayoutList[seat.Key].IsWaiting = false;
                        LastSeatCount--;
                        break;
                    case EnterOutLogType.BespeakWaiting:
                        _seatLayoutList[seat.Key].IsUsing = false;
                        _seatLayoutList[seat.Key].IsBespeak = true;
                        _seatLayoutList[seat.Key].IsShortLeave = false;
                        _seatLayoutList[seat.Key].IsWaiting = false;
                        if (!_seatLayoutList[seat.Key].IsCanSelectBespeakSeat)
                        {
                            LastSeatCount--;
                        }
                        break;
                }
            }
        }
        void RoomSeatWindow_WaitSeat(object sender, EventArgs e)
        {
            CountDown.Pause();
            if (WaitSeat(sender as SeatUC_ViewModel))
            {
                if (RoomWindowClose != null)
                {
                    RoomWindowClose(this, new EventArgs());
                }
            }
            else
            {
                CountDown.Start();
            }
        }

        private void RoomSeatWindow_SelectBespeakSeat(object sender, EventArgs e)
        {
            CountDown.Pause();
            if (SelectBookingSeat(sender as SeatUC_ViewModel))
            {
                if (RoomWindowClose != null)
                {
                    RoomWindowClose(this, new EventArgs());
                }
            }
            else
            {
                CountDown.Start();
            }
        }

        private void RoomSeatWindow_SelectSeat(object sender, EventArgs e)
        {
            CountDown.Pause();
            if (SelectSeat(sender as SeatUC_ViewModel))
            {
                if (RoomWindowClose != null)
                {
                    RoomWindowClose(this, new EventArgs());
                }
            }
            else
            {
                CountDown.Start();
            }
        }
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <returns></returns>
        public bool WaitSeat(SeatUC_ViewModel seatVM)
        {
            WaitSeatLogInfo lastWaitInfo = T_SM_SeatWaiting.GetListWaitLogByCardNo(ClientObject.EnterOutLogData.EnterOutlog.CardNo, ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
            ReadingRoomInfo roomInfo = ClientObject.EnterOutLogData.Student.AtReadingRoom;
            if (!string.IsNullOrEmpty(ClientObject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.WaitSeatWithSeat);
                return false;
            }

            if (lastWaitInfo != null && lastWaitInfo.SeatWaitTime.AddMinutes(roomInfo.Setting.NoManagement.OperatingInterval).CompareTo(ServiceDateTime.Now) >= 0)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.WaitSeatFrequent);
                return false;
            }
            SeatLockState lockseat = T_SM_Seat.LockSeat(seatVM.LongSeatNo);
            if (lockseat != SeatLockState.Locked) //座位成功加锁
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                return false;
            }
            ClientObject.EnterOutLogData.WaitSeatLogModel = new WaitSeatLogInfo() { SeatNo = seatVM.ShortSeatNo };
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SetShortWarning);
            ClientObject.EnterOutLogData.WaitSeatLogModel = null;
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
            {
                //初始化等待记录 记录ID需要待执行之后添加。
                WaitSeatLogInfo waitInfo = new WaitSeatLogInfo();
                waitInfo.CardNo = ClientObject.EnterOutLogData.EnterOutlog.CardNo;
                waitInfo.NowState = LogStatus.Valid;
                waitInfo.OperateType = Operation.Reader;
                waitInfo.WaitingState = EnterOutLogType.Waiting;

                EnterOutLogInfo seatUsingEnterOutInfo = T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatVM.LongSeatNo);
                seatUsingEnterOutInfo.EnterOutState = EnterOutLogType.ShortLeave;
                seatUsingEnterOutInfo.EnterOutType = LogStatus.Valid;
                seatUsingEnterOutInfo.Flag = Operation.OtherReader;
                seatUsingEnterOutInfo.TerminalNum = ClientObject.ClientSetting.ClientNo;
                seatUsingEnterOutInfo.Remark = string.Format("在{0} {1}号座位，被读者{2}在终端{3}设置为暂离并等待该座位", seatUsingEnterOutInfo.ReadingRoomName, seatUsingEnterOutInfo.ShortSeatNo, waitInfo.CardNo, ClientObject.ClientSetting.ClientNo);

                ClientObject.EnterOutLogData.EnterOutlog = seatUsingEnterOutInfo;//要等待读者的暂离记录
                ClientObject.EnterOutLogData.WaitSeatLogModel = waitInfo;//等待记录 
                ClientObject.EnterOutLogData.FlowControl = ClientOperation.WaitSeat;
                return true;
            }
            else
            {
                T_SM_Seat.UnLockSeat(seatVM.LongSeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                return false;
            }
        }
        /// <summary>
        /// 选座座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectSeat(SeatUC_ViewModel seatVM)
        {
            SeatLockState lockseat = T_SM_Seat.LockSeat(seatVM.LongSeatNo);
            if (lockseat != SeatLockState.Locked)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatVM.ShortSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatConfinmed);
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
            {
                ClientObject.EnterOutLogData.EnterOutlog.SeatNo = seatVM.LongSeatNo;
                ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatVM.ShortSeatNo;
                ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
                ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
                ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}手动选择，{1}，{2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatVM.ShortSeatNo);
                ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                return true;
            }
            else
            {
                T_SM_Seat.UnLockSeat(seatVM.LongSeatNo); //确认窗口点击取消或者自动关闭，则解锁。
                return false;
            }
        }

        /// <summary>
        /// 键盘选座
        /// </summary>
        /// <returns></returns>
        public bool KeyboardSelectSeat()
        {
            KeyboardWindowObject.GetInstance().Window.ShowMessage();
            if (string.IsNullOrEmpty(KeyboardWindowObject.GetInstance().Window.ViewModel.SeatNo))
            {
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = KeyboardWindowObject.GetInstance().Window.ViewModel.SeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatConfinmed);
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
            {
                ClientObject.EnterOutLogData.EnterOutlog.SeatNo = KeyboardWindowObject.GetInstance().Window.ViewModel.LongSeatNo;
                ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
                ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}输入座位号选择，{1}，{2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
                return true;
            }
            else
            {
                T_SM_Seat.UnLockSeat(KeyboardWindowObject.GetInstance().Window.ViewModel.LongSeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                return false;
            }
        }
        /// <summary>
        /// 选择预约的座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectBookingSeat(SeatUC_ViewModel seatVM)
        {
            DateTime datetimeNow = ServiceDateTime.Now;
            List<BespeakLogInfo> bespeakLogList = T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatVM.LongSeatNo, datetimeNow.Date);
            if (bespeakLogList.Count > 0)
            {
                if (bespeakLogList[0].BsepeakTime.AddMinutes(-double.Parse(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= datetimeNow)
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.IsBookingSeat);
                    return false;
                }
                ClientObject.EnterOutLogData.BespeakLogInfo = bespeakLogList[0];
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectBookingSeatWarn);
                if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Failed)
                {
                    ClientObject.EnterOutLogData.BespeakLogInfo = null;
                    return false;
                }
            }
            SeatLockState lockseat = T_SM_Seat.LockSeat(seatVM.LongSeatNo);
            if (lockseat != SeatLockState.Locked) //座位成功加锁
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatVM.ShortSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = _ReadingRoomName;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = _ReadingRoomNo;
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatConfinmed);
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Failed)
            {
                T_SM_Seat.UnLockSeat(seatVM.LongSeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.SeatNo = seatVM.LongSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatVM.ShortSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
            ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}手动选择，{1}，{2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatVM.ShortSeatNo);
            ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
            return true;
        }
        /// <summary>
        /// 随机选择
        /// </summary>
        /// <returns></returns>
        public bool RandomSeat()
        {
            if (LastSeatCount == 0)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ReadingRoomFull);
                return false;
            }
            string tempSeatNo = T_SM_Seat.RandomAllotSeat(ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
            if (T_SM_Seat.LockSeat(tempSeatNo) != SeatLockState.Locked)//座位锁定失败，则提示
            {
                ClientObject.EnterOutLogData.EnterOutlog.SeatNo = "";
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡，自动选择{1} {2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, tempSeatNo.Substring(tempSeatNo.Length - ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount));
            ClientObject.EnterOutLogData.EnterOutlog.SeatNo = tempSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = SeatComm.SeatNoToShortSeatNo(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, ClientObject.EnterOutLogData.EnterOutlog.SeatNo);
            ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
            return true;
        }
    }
}
