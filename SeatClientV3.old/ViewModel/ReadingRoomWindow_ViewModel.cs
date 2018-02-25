using SeatClientV3.FunWindow;
using SeatClientV3.OperateResult;
using SeatClientV3.UCViewModel;
using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SeatClientV3.ViewModel
{
    public class ReadingRoomWindow_ViewModel : INotifyPropertyChanged
    {
        public ReadingRoomWindow_ViewModel()
        {
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            if (clientObject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.Used)
            {
                UsuallySeatBtn = "Visible";
            }
            ReaderStatusInfo.ReaderInfo = clientObject.EnterOutLogData.Student;
            //GetRoomUsage();
        }

        #region 属性
        private string _UsuallySeatBtn = "Collapsed";
        /// <summary>
        /// 
        /// </summary>
        public string UsuallySeatBtn
        {
            get { return _UsuallySeatBtn; }
            set { _UsuallySeatBtn = value; OnPropertyChanged("UsuallySeatBtn"); }
        }

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

        private ReaderInfoViewModel _ReaderStatusInfo = new ReaderInfoViewModel();
        /// <summary>
        /// 读者状态
        /// </summary>
        public ReaderInfoViewModel ReaderStatusInfo
        {
            get { return _ReaderStatusInfo; }
            set { _ReaderStatusInfo = value; OnPropertyChanged("ReaderStatusInfo"); }
        }
        private System.Windows.Media.Imaging.BitmapImage _ReaderAdOImage;

        private Dictionary<string, List<ReadingRoomBtn_ViewModel>> _ReadingRoomUsage = new Dictionary<string, List<ReadingRoomBtn_ViewModel>>();
        /// <summary>
        /// 阅览室
        /// </summary>
        public Dictionary<string, List<ReadingRoomBtn_ViewModel>> ReadingRoomUsage
        {
            get { return _ReadingRoomUsage; }
            set { _ReadingRoomUsage = value; OnPropertyChanged("ReadingRoomUsage"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //private ViewModel.LoadingUC_ViewModel _LoadViewModel = new LoadingUC_ViewModel();
        ///// <summary>
        ///// 读取滚动条
        ///// </summary>
        //public ViewModel.LoadingUC_ViewModel LoadViewModel
        //{
        //    get { return _LoadViewModel; }
        //    set { _LoadViewModel = value; }
        //}

        #endregion
        /// <summary>
        /// 分区域排列
        /// </summary>
        public void GetRoomUsage()
        {
            try
            {
                //添加区域
                List<LibraryInfo> linList = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
                foreach (LibraryInfo lib in linList)
                {
                    foreach (AreaInfo area in lib.AreaList)
                    {
                        if (!ReadingRoomUsage.ContainsKey(area.AreaName))
                        {
                            ReadingRoomUsage.Add(area.AreaName, new List<ReadingRoomBtn_ViewModel>());
                        }
                    }
                }
                if (!ReadingRoomUsage.ContainsKey("阅览室"))
                {
                    ReadingRoomUsage.Add("阅览室", new List<ReadingRoomBtn_ViewModel>());
                }

                DateTime nowDateTime = SeatManage.Bll.ServiceDateTime.Now;
                Dictionary<string, ReadingRoomSeatUsedState_Ex> roomStateList = SeatManage.Bll.TerminalOperatorService.GetTeminaRoomStatus(clientObject.ClientSetting.DeviceSetting.Rooms);
                foreach (KeyValuePair<string, ReadingRoomSeatUsedState_Ex> item in roomStateList)
                {
                    if (item.Value.ReadingRoom.Area.AreaName == "")
                    {
                        item.Value.ReadingRoom.Area.AreaName = "阅览室";
                    }
                    if (roomStateList.ContainsKey(item.Key))
                    {
                        SeatManage.EnumType.ReadingRoomStatus roomStatus = SeatClientV3.Code.NowReadingRoomState.ReadingRoomOpenState(item.Value.ReadingRoom.Setting.RoomOpenSet, nowDateTime);
                        if (roomStatus == SeatManage.EnumType.ReadingRoomStatus.Close && !clientObject.ClientSetting.DeviceSetting.IsShowClosedRoom)
                        {
                            continue;
                        }
                        ReadingRoomBtn_ViewModel viewModel = new ReadingRoomBtn_ViewModel();
                        viewModel.ReadingRoomName = item.Value.ReadingRoom.Name;
                        viewModel.ReadingRoomNo = item.Value.ReadingRoom.No;
                        viewModel.IsBook = item.Value.ReadingRoom.Setting.SeatBespeak.Used;
                        viewModel.AllSeatCount = roomStateList[item.Key].SeatAmountAll;
                        viewModel.UsedSeatCount = roomStateList[item.Key].SeatAmountUsed;
                        viewModel.BookingSeatCount = roomStateList[item.Key].SeatBookingCount;
                        viewModel.Usage = roomStateList[item.Key].RoomSeatUsingState;
                        viewModel.Status = roomStatus;
                        ReadingRoomUsage[item.Value.ReadingRoom.Area.AreaName].Add(viewModel);
                    }
                }
                //删除空区域
                List<string> deleteArea = new List<string>();
                foreach (KeyValuePair<string, List<ReadingRoomBtn_ViewModel>> item in ReadingRoomUsage)
                {
                    if (item.Value.Count < 1)
                    {
                        deleteArea.Add(item.Key);
                    }
                }
                foreach (string a in deleteArea)
                {
                    ReadingRoomUsage.Remove(a);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(SeatManage.EnumType.MessageType.Exception);
                errorWindow.ShowDialog();
            }

        }
        /// <summary>
        /// 进入阅览室前判断
        /// </summary>
        /// <param name="roomNo"></param>
        public void EnterReadingRoom(ReadingRoomBtn_ViewModel vm_Room)
        {
            try
            {
                this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = vm_Room.ReadingRoomNo;
                ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(vm_Room.ReadingRoomNo);
                if (vm_Room.Status == SeatManage.EnumType.ReadingRoomStatus.Close || vm_Room.Status == SeatManage.EnumType.ReadingRoomStatus.BeforeClose)
                {
                    clientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;
                    MessageWindow fullWindow = new MessageWindow(SeatManage.EnumType.MessageType.RoomNotOpen);
                    fullWindow.ShowDialog();
                    clientObject.EnterOutLogData.Student.AtReadingRoom = null;
                    return;
                }
                if (vm_Room.Usage == SeatManage.EnumType.ReadingRoomUsingStatus.Full && (!roomInfo.Setting.NoManagement.Used))
                {
                    MessageWindow fullWindow = new MessageWindow(SeatManage.EnumType.MessageType.RoomFull);
                    fullWindow.ShowDialog();
                    return;
                }
                clientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;//给读者所在的阅览室赋值。

                //验证读者身份是否允许选择该阅览室。
                if (!Code.SelectSeatProven.ProvenReaderType(clientObject.EnterOutLogData.Student, roomInfo.Setting))
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.RoomNotReaderType);
                    popWindow.ShowDialog();
                    return;
                }
                //验证读者黑名单，选座次数。
                if (Code.SelectSeatProven.ProvenReaderState(clientObject.EnterOutLogData.Student, roomInfo, clientObject.RegulationRulesSet.BlacklistSet, clientObject.ClientSetting.DeviceSetting))
                {
                    this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                    return;
                }
                //TODO:验证终端选座方式
                if (vm_Room.Usage == SeatManage.EnumType.ReadingRoomUsingStatus.Full && roomInfo.Setting.NoManagement.Used)
                {
                    this.clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                    this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                    this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                    RoomSeatWindow roomSeatWindow = new RoomSeatWindow();
                    roomSeatWindow.ShowDialog();
                    return;
                }
                else
                {
                    SeatManage.EnumType.SelectSeatMode selectSeatMethod = SeatClientV3.Code.SelectSeatProven.ProvenSelectSeatMethod(clientObject.ClientSetting.DeviceSetting, roomInfo.Setting.SeatChooseMethod);

                    if (selectSeatMethod == SeatManage.EnumType.SelectSeatMode.OptionalMode || selectSeatMethod == SeatManage.EnumType.SelectSeatMode.ManualMode)
                    {
                        this.clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                        RoomSeatWindow roomSeatWindow = new RoomSeatWindow();
                        roomSeatWindow.ShowDialog();
                        return;
                    }
                    else
                    {
                        this.clientObject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.RandonSelect;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(SeatManage.EnumType.MessageType.Exception);
                errorWindow.ShowDialog();
            }
        }
    }
    /// <summary>
    /// 读者状态
    /// </summary>
    public class ReaderInfoViewModel : INotifyPropertyChanged
    {
        private ReaderInfo _ReaderInfo = new ReaderInfo();
        /// <summary>
        /// 读者类
        /// </summary>
        public ReaderInfo ReaderInfo
        {
            get { return _ReaderInfo; }
            set { _ReaderInfo = value; Changed("ReaderInfo"); }
        }

        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get
            {
                if (_ReaderInfo.AtReadingRoom != null)
                {
                    return "阅览室：" + _ReaderInfo.AtReadingRoom.Name;
                }
                else
                {
                    return "阅览室：暂无";
                }
            }
        }
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return "学号：" + _ReaderInfo.CardNo; }
        }
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get
            {
                if (_ReaderInfo.EnterOutLog != null && _ReaderInfo.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    return "座位号：" + _ReaderInfo.EnterOutLog.ShortSeatNo;
                }
                else
                {
                    return "座位号：暂无";
                }
            }
        }
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return "姓名：" + _ReaderInfo.Name; }
        }
        /// <summary>
        /// 进出记录状态
        /// </summary>
        public string EnterOutState
        {
            get
            {
                if (_ReaderInfo.EnterOutLog == null)
                {
                    return "状态：未选座";
                }
                else
                {
                    switch (_ReaderInfo.EnterOutLog.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                            return "状态：在座";
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            return "状态：暂离";
                    }
                    return "状态：未选座";
                }
            }
        }
        /// <summary>
        /// 座位信息
        /// </summary>
        public string RoomSeat
        {
            get
            {
                if (_ReaderInfo.AtReadingRoom != null && _ReaderInfo.EnterOutLog != null)
                {
                    return "座位：" + _ReaderInfo.AtReadingRoom.Name + " " + _ReaderInfo.EnterOutLog.ShortSeatNo + "号";
                }
                else
                {
                    return "座位：暂无";
                }
            }
        }
        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
