using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SeatClientV3.Code;
using SeatClientV3.OperateResult;
using System.Windows.Forms;
using SeatClientV3.WindowObject;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;


namespace SeatClientV3.ViewModel
{
    public class ReadingRoomWindow_ViewModel : INotifyPropertyChanged
    {
        public ReadingRoomWindow_ViewModel()
        {
            //WindowHeight = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            //WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            //WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            //WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;

            WindowHeight = ClientObject.RoomAutoAddSize ? ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y + ClientObject.AddSize : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = ClientObject.RoomAutoAddSize ? ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y - ClientObject.AddSize : ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            if (ClientObject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.Used)
            {
                UsuallySeatBtn = "Visible";
            }
            if (ClientObject.ReaderAdvert != null)
            {
                ReaderAdOImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\ReaderImage\\" + ClientObject.ReaderAdvert.ReaderAdImagePath, UriKind.RelativeOrAbsolute));
            }
            GetRoomArea();
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
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }

        private Dictionary<string, ReadingRoomUC_ViewModel> _stateList = new Dictionary<string, ReadingRoomUC_ViewModel>();
        /// <summary>
        /// 使用状态
        /// </summary>
        public Dictionary<string, ReadingRoomUC_ViewModel> StateList
        {
            get { return _stateList; }
            set { _stateList = value; }
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
        /// <summary>
        /// 读者广告图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage ReaderAdOImage
        {
            get { return _ReaderAdOImage; }
            set { _ReaderAdOImage = value; OnPropertyChanged("ReaderAdOImage"); }
        }

        private Dictionary<string, List<ReadingRoomUC_ViewModel>> _ReadingRoomUsage = new Dictionary<string, List<ReadingRoomUC_ViewModel>>();
        /// <summary>
        /// 阅览室
        /// </summary>
        public Dictionary<string, List<ReadingRoomUC_ViewModel>> ReadingRoomUsage
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
        #endregion
        /// <summary>
        /// 分区域排列
        /// </summary>
        public void GetRoomArea()
        {
            try
            {
                //添加区域
                List<LibraryInfo> linList = T_SM_Library.GetLibraryInfoList(null, null, null);
                foreach (AreaInfo area in from lib in linList from area in lib.AreaList where !ReadingRoomUsage.ContainsKey(area.AreaName) select area)
                {
                    ReadingRoomUsage.Add(area.AreaName, new List<ReadingRoomUC_ViewModel>());
                }
                if (!ReadingRoomUsage.ContainsKey("阅览室"))
                {
                    ReadingRoomUsage.Add("阅览室", new List<ReadingRoomUC_ViewModel>());
                }
                foreach (KeyValuePair<string, ReadingRoomInfo> item in ClientObject.ReadingRoomList)
                {
                    if (item.Value.Area.AreaName == "")
                    {
                        item.Value.Area.AreaName = "阅览室";
                    }
                    ReadingRoomUC_ViewModel viewModel = new ReadingRoomUC_ViewModel();
                    viewModel.ReadingRoomName = item.Value.Name;
                    viewModel.ReadingRoomNo = item.Value.No;
                    viewModel.IsBook = item.Value.Setting.SeatBespeak.Used;
                    viewModel.AllSeatCount = item.Value.SeatList.Seats.Count(u => u.Value.IsSuspended == false);
                    StateList.Add(viewModel.ReadingRoomNo, viewModel);
                    ReadingRoomUsage[item.Value.Area.AreaName].Add(StateList[viewModel.ReadingRoomNo]);
                }
                List<string> deleteArea = (from item in ReadingRoomUsage where item.Value.Count < 1 select item.Key).ToList();
                foreach (string a in deleteArea)
                {
                    ReadingRoomUsage.Remove(a);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
            }

        }
        /// <summary>
        /// 获取使用状态
        /// </summary>
        public void GetUsage()
        {
            ReaderStatusInfo.ReaderInfo = ClientObject.EnterOutLogData.Student;
            DateTime nowDateTime = ServiceDateTime.Now;
            Dictionary<string, ReadingRoomSeatUsedState> roomStateList = EnterOutOperate.GetReadingRoomSeatUsingStateV2(ClientObject.ClientSetting.DeviceSetting.Rooms);
            foreach (KeyValuePair<string, ReadingRoomInfo> item in ClientObject.ReadingRoomList)
            {
                roomStateList[item.Key].SeatAmountAll = StateList[item.Key].AllSeatCount;
                StateList[item.Key].UsedSeatCount = roomStateList[item.Key].SeatAmountUsed;
                StateList[item.Key].BookingSeatCount = roomStateList[item.Key].SeatBookingCount;
                StateList[item.Key].Usage = roomStateList[item.Key].RoomSeatUsingState;
                StateList[item.Key].Status = NowReadingRoomState.ReadingRoomOpenState(item.Value.Setting.RoomOpenSet, nowDateTime);
            }
        }
        /// <summary>
        /// 进入阅览室前判断
        /// </summary>
        /// <param name="roomNo"></param>
        public void EnterReadingRoom(ReadingRoomUC_ViewModel vm_Room)
        {
            try
            {
                //ClientObject.EnterOutLogData.FlowControl = ClientOperation.Back;
                ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = vm_Room.ReadingRoomNo;
                ReadingRoomInfo roomInfo = T_SM_ReadingRoom.GetSingleRoomInfo(vm_Room.ReadingRoomNo);
                if (vm_Room.Status == ReadingRoomStatus.Close || vm_Room.Status == ReadingRoomStatus.BeforeClose)
                {
                    ClientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ReadingRoomClosing);
                    ClientObject.EnterOutLogData.Student.AtReadingRoom = null;
                    return;
                }
                if (vm_Room.Usage == ReadingRoomUsingStatus.Full && (!roomInfo.Setting.NoManagement.Used))
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ReadingRoomFull);
                    return;
                }
                ClientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;//给读者所在的阅览室赋值。

                //验证读者身份是否允许选择该阅览室。
                if (!SelectSeatProven.ProvenReaderType(ClientObject.EnterOutLogData.Student, roomInfo.Setting))
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ReaderTypeInconformity);
                    return;
                }
                //验证读者黑名单，选座次数。
                if (SelectSeatProven.ProvenReaderState(ClientObject.EnterOutLogData.Student, roomInfo, ClientObject.RegulationRulesSet.BlacklistSet, ClientObject.ClientSetting.DeviceSetting))
                {
                    ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                    return;
                }
                //TODO:验证终端选座方式
                if (vm_Room.Usage == ReadingRoomUsingStatus.Full && roomInfo.Setting.NoManagement.Used)
                {
                    ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                    ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                    ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                    //ReadingRoomWindowObject.GetInstance().Window.Hide();
                    RoomSeatWindowObject.GetInstance(roomInfo.No).Window[roomInfo.No].ShowMessage();
                }
                else
                {
                    SelectSeatMode selectSeatMethod = SelectSeatProven.ProvenSelectSeatMethod(ClientObject.ClientSetting.DeviceSetting, roomInfo.Setting.SeatChooseMethod);

                    if (selectSeatMethod == SelectSeatMode.OptionalMode || selectSeatMethod == SelectSeatMode.ManualMode)
                    {
                        ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                        ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                        ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                        //ReadingRoomWindowObject.GetInstance().Window.Hide();
                        RoomSeatWindowObject.GetInstance(roomInfo.No).Window[roomInfo.No].ShowMessage();
                        
                    }
                    else
                    {
                        ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = roomInfo.No;
                        ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = roomInfo.Name;
                        string tempSeatNo = T_SM_Seat.RandomAllotSeat(ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
                        if (T_SM_Seat.LockSeat(tempSeatNo) != SeatLockState.Locked)//座位锁定失败，则提示
                        {
                            ClientObject.EnterOutLogData.EnterOutlog.SeatNo = "";
                            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                            return;
                        }
                        ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡，自动选择{1} {2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, tempSeatNo.Substring(tempSeatNo.Length - ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount));
                        ClientObject.EnterOutLogData.EnterOutlog.SeatNo = tempSeatNo;
                        ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
                        ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = SeatComm.SeatNoToShortSeatNo(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, ClientObject.EnterOutLogData.EnterOutlog.SeatNo);
                        ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
                    }
                }
                //RoomSeatWindowObject.GetInstance(roomInfo.No).Window[roomInfo.No] = null;
            }
            catch (Exception ex)
            {
                WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
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
            set
            {
                _ReaderInfo = value;
                Changed("ReaderInfo");
                Changed("ReadingRoomName");
                Changed("CardNo");
                Changed("SeatNo");
                Changed("ReaderName");
                Changed("EnterOutState");
                Changed("RoomSeat");
            }
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
                if (_ReaderInfo.EnterOutLog != null && _ReaderInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
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
                        case EnterOutLogType.BookingConfirmation:
                        case EnterOutLogType.ReselectSeat:
                        case EnterOutLogType.SelectSeat:
                        case EnterOutLogType.WaitingSuccess:
                        case EnterOutLogType.ComeBack:
                        case EnterOutLogType.ContinuedTime:
                            return "状态：在座";
                        case EnterOutLogType.ShortLeave:
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
