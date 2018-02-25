using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;

namespace LastSeatClient.ViewModel
{
    public class VM_W_MainWindow : VM_BasicModel
    {

        public VM_W_MainWindow()
        {
            _WindowHeight = double.Parse(ConfigurationManager.AppSettings["Height"]);
            _WindowWidth = double.Parse(ConfigurationManager.AppSettings["Width"]);
            _WindowLeft = double.Parse(ConfigurationManager.AppSettings["Left"]);
            _WindowTop = double.Parse(ConfigurationManager.AppSettings["Top"]);
        }
        /// <summary>
        /// 阅览室列表
        /// </summary>
        private List<ReadingRoomInfo> _roomList = new List<ReadingRoomInfo>();
        /// <summary>
        /// 使用状态
        /// </summary>
        private Dictionary<string, VM_UC_RoomStatus> _stateList = new Dictionary<string, VM_UC_RoomStatus>();

        /// <summary>
        /// 图书馆状态
        /// </summary>
        private VM_UC_LibStatus _LibStatus = new VM_UC_LibStatus();
        /// <summary>
        /// 使用状态
        /// </summary>
        public Dictionary<string, VM_UC_RoomStatus> StateList
        {
            get { return _stateList; }
            set { _stateList = value; }
        }

        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; Changed("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; Changed("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; Changed("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; Changed("WindowTop"); }
        }

        /// <summary>
        /// 图书馆状态
        /// </summary>
        public VM_UC_LibStatus LibStatus
        {
            get { return _LibStatus; }
            set { _LibStatus = value; Changed("LibStatus"); }
        }

        private TimeLoop timeGetStatus;
        private Thread showGetStatus;
        /// <summary>
        /// 获取阅览室列表
        /// </summary>
        public void GetRoomList()
        {
            _roomList = T_SM_ReadingRoom.GetReadingRooms(null, null, null);
            foreach (ReadingRoomInfo item in _roomList)
            {
                VM_UC_RoomStatus status = new VM_UC_RoomStatus();
                status.AllSeatCount = item.SeatList.Seats.Count(u => u.Value.IsSuspended == false);
                status.RoomName = item.Name;
                status.RoomNo = item.No;
                StateList.Add(item.No, status);
            }
            LibStatus.AllSeatCount = StateList.Sum(u => u.Value.AllSeatCount);
            LibStatus.RoomName = _roomList[0].Libaray.Name;

            GetUsage();
            ShowTimeRun();
        }
        /// <summary>
        /// 时间开始
        /// </summary>
        public void ShowTimeRun()
        {
            timeGetStatus = new TimeLoop(30000);
            timeGetStatus.TimeTo += timeDateTimeShow_TimeTo;
            showGetStatus = new Thread(timeGetStatus.TimeStrat);
            showGetStatus.Start();

        }
        //一秒执行
        void timeDateTimeShow_TimeTo(object sender, EventArgs e)
        {
            GetUsage();
        }

        /// <summary>
        /// 获取使用状态
        /// </summary>
        public void GetUsage()
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            Dictionary<string, ReadingRoomSeatUsedState> roomStateList = EnterOutOperate.GetReadingRoomSeatUsingStateV2(_roomList.Select(item => item.No).ToList());
            foreach (ReadingRoomInfo item in _roomList)
            {
                roomStateList[item.No].SeatAmountAll = StateList[item.No].AllSeatCount;
                StateList[item.No].UsingCount = roomStateList[item.No].SeatAmountUsed;
                StateList[item.No].BookingCount = roomStateList[item.No].SeatBookingCount;
                StateList[item.No].UsingStatus = roomStateList[item.No].RoomSeatUsingState;
                StateList[item.No].RoomStatus = NowReadingRoomState.ReadingRoomOpenState(item.Setting.RoomOpenSet, nowDateTime);
            }
            LibStatus.AllSeatCount = StateList.Where(u => u.Value.RoomStatus != SeatManage.EnumType.ReadingRoomStatus.Close).Sum(u => u.Value.AllSeatCount);
            LibStatus.BookingCount = StateList.Sum(u => u.Value.BookingCount);
            LibStatus.UsingCount = StateList.Sum(u => u.Value.UsingCount);
        }
    }
}
