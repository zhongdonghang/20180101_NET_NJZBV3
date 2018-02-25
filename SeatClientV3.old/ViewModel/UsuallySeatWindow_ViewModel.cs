using SeatClientV3.FunWindow;
using SeatClientV3.OperateResult;
using SeatClientV3.UCViewModel;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SeatClientV3.ViewModel
{
    public class UsuallySeatWindow_ViewModel
    {
        public UsuallySeatWindow_ViewModel()
        {
            clientObject = SystemObject.GetInstance();
            WindowWidth = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 620;
            WindowHeight = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 320;
            WindowLeft = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;

            
            AddOften();
        }
        private OperateResult.SystemObject clientObject;
        /// <summary>
        /// 基础类
        /// </summary>
        public OperateResult.SystemObject ClientObject
        {
            get { return clientObject; }
            set { clientObject = value; }
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
        private ObservableCollection<UsuallySeatUC_ViewModel> _OftenUsedSeats = new ObservableCollection<UsuallySeatUC_ViewModel>();
        /// <summary>
        /// 常用座位LIST
        /// </summary>
        public ObservableCollection<UsuallySeatUC_ViewModel> OftenUsedSeats
        {
            get { return _OftenUsedSeats; }
            set { _OftenUsedSeats = value; OnPropertyChanged("OftenUsedSeats"); }
        }
        HandleResult operateResule = HandleResult.Failed;
        /// <summary>
        /// 操作结果，成功或者失败
        /// </summary>
        public HandleResult OperateResule
        {
            get { return operateResule; }
            set { operateResule = value; OnPropertyChanged("OperateResule"); }
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

        private int _CloseTime = 30;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        /// <summary>
        /// 添加常坐座位
        /// </summary>
        public void AddOften()
        {
            try
            {
                OftenUsedSeats.Clear();
                List<Seat> seats = SeatManage.Bll.T_SM_Seat.GetReaderOftenUsedSeat(clientObject.EnterOutLogData.EnterOutlog.CardNo, clientObject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.LengthDays, clientObject.ClientSetting.DeviceSetting.Rooms);
                for (int i = 0; i < seats.Count; i++)
                {
                    UsuallySeatUC_ViewModel vm = new UsuallySeatUC_ViewModel();
                    seats[i].ShortSeatNo = SeatComm.SeatNoToShortSeatNo(seats[i].ReadingRoom.Setting.SeatNumAmount, seats[i].SeatNo);
                    vm.SeatNo = seats[i].SeatNo;
                    vm.ShortSeatNo = seats[i].ShortSeatNo;
                    vm.ReadingRoomName = seats[i].ReadingRoom.Name;
                    vm.ReadingRoomNo = seats[i].ReadingRoomNum;
                    OftenUsedSeats.Add(vm);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(MessageType.Exception);
                errorWindow.ShowDialog();
            }
        }
        /// <summary>
        /// 选座座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectSeat(UsuallySeatUC_ViewModel seatBtn)
        {
            ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(seatBtn.ReadingRoomNo);
            clientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;//给读者所在的阅览室赋值。

            //验证读者身份是否允许选择该阅览室。
            if (!Code.SelectSeatProven.ProvenReaderType(clientObject.EnterOutLogData.Student, roomInfo.Setting))
            {
                MessageWindow mesgWindow = new MessageWindow(MessageType.RoomNotReaderType);
                mesgWindow.ShowDialog();
                return false;
            }
            //验证读者黑名单，选座次数。
            if (Code.SelectSeatProven.ProvenReaderState(clientObject.EnterOutLogData.Student, roomInfo, clientObject.RegulationRulesSet.BlacklistSet, clientObject.ClientSetting.DeviceSetting))
            {
                this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                return false;
            }
            SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
            {
                clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
                clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = seatBtn.ReadingRoomName;
                MessageWindow mesgWindow = new MessageWindow(MessageType.SelectSeatConfirm);
                CountDown.Pause();
                mesgWindow.ShowDialog();
                CountDown.Start();
                if (mesgWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = seatBtn.ReadingRoomName;
                    clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = seatBtn.ReadingRoomNo;
                    clientObject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
                    clientObject.EnterOutLogData.EnterOutlog.TerminalNum = clientObject.ClientSetting.ClientNo;
                    clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位  
                    clientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}选择常坐座位，{1}，{2}号座位", clientObject.ClientSetting.ClientNo, clientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatBtn.ShortSeatNo);
                    return true;
                }
                else
                {
                    T_SM_Seat.UnLockSeat(seatBtn.SeatNo);//确认窗口点击取消或者自动关闭，则解锁。
                }
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
            {
                MessageWindow mesgWindow = new MessageWindow(MessageType.SeatIsLocked);
                CountDown.Pause();
                mesgWindow.ShowDialog();
                CountDown.Start();
            }
            else if (lockseat == SeatManage.EnumType.SeatLockState.NotExists)
            {
                MessageWindow mesgWindow = new MessageWindow(MessageType.SeatNotExist);
                CountDown.Pause();
                mesgWindow.ShowDialog();
                CountDown.Start();
            }
            return false;
        }
    }
}
