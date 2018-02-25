using SeatClientV3.FunWindow;
using SeatClientV3.OperateResult;
using SeatManage.Bll;
using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeatClientV3.ViewModel
{
    public class LastSeatWindow_ViewModel : INotifyPropertyChanged
    {
        public LastSeatWindow_ViewModel()
        {
            WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
            WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
            WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
            WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            GetRoomUsage();
        }
        #region 属性

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

        private int _LastSeatCountSum = 0;
        /// <summary>
        /// 剩余座位数目
        /// </summary>
        public string LastSeatCountSum
        {
            get { return "剩余座位总数：" + _LastSeatCountSum; }
        }

        private List<UCViewModel.UC_LastSeatNum> _RoomList = new List<UCViewModel.UC_LastSeatNum>();

        public List<UCViewModel.UC_LastSeatNum> RoomList
        {
            get { return _RoomList; }
            set { _RoomList = value; OnPropertyChanged("RoomList"); }
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

        public void GetRoomUsage()
        {
            try
            {
                //添加区域
                List<LibraryInfo> linList = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);

                DateTime nowDateTime = SeatManage.Bll.ServiceDateTime.Now;
                Dictionary<string, ReadingRoomSeatUsedState_Ex> roomStateList = SeatManage.Bll.TerminalOperatorService.GetTeminaRoomStatus(clientObject.ClientSetting.DeviceSetting.Rooms);
                foreach (KeyValuePair<string, ReadingRoomSeatUsedState_Ex> item in roomStateList)
                {
                    SeatManage.EnumType.ReadingRoomStatus roomStatus = SeatClientV3.Code.NowReadingRoomState.ReadingRoomOpenState(item.Value.ReadingRoom.Setting.RoomOpenSet, nowDateTime);
                    if (roomStatus == SeatManage.EnumType.ReadingRoomStatus.Close && !clientObject.ClientSetting.DeviceSetting.IsShowClosedRoom)
                    {
                        continue;
                    }
                    UCViewModel.UC_LastSeatNum viewModel = new UCViewModel.UC_LastSeatNum();
                    viewModel.ReadingRoomName = item.Value.ReadingRoom.Name;
                    viewModel.ReadingRoomNo = item.Value.ReadingRoom.No;
                    viewModel.IsBook = item.Value.ReadingRoom.Setting.SeatBespeak.Used;
                    viewModel.AllSeatCount = roomStateList[item.Key].SeatAmountAll;
                    viewModel.UsedSeatCount = roomStateList[item.Key].SeatAmountUsed;
                    viewModel.BookingSeatCount = roomStateList[item.Key].SeatBookingCount;
                    viewModel.Usage = roomStateList[item.Key].RoomSeatUsingState;
                    viewModel.Status = roomStatus;
                    RoomList.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(SeatManage.EnumType.MessageType.Exception);
                errorWindow.ShowDialog();
            }
        }
        #region 时间更新
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeShow = null;
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeSync = null;
        Thread showTimeThread = null;
        Thread syncTimeThread = null;

        #endregion

        #region 座位数
        Thread MyLastSeatSum = null;
        public SeatManage.SeatManageComm.TimeLoop MyLastSeatSumTime = null;
        public void LastSeatRun()
        {
            _LastSeatCountSum = SeatManage.Bll.TerminalOperatorService.LastSeatCount(clientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeatCountSum");
            MyLastSeatSumTime = new SeatManage.SeatManageComm.TimeLoop(30 * 1000);
            MyLastSeatSumTime.TimeTo += new EventHandler(MyLastSeatSumTime_TimeTo);
            timeDateTimeShow = new SeatManage.SeatManageComm.TimeLoop(1000);
            MyLastSeatSum = new Thread(new ThreadStart(timeDateTimeShow.TimeStrat));
            MyLastSeatSum.Start();
        }

        void MyLastSeatSumTime_TimeTo(object sender, EventArgs e)
        {
            MyLastSeatSumTime.TimeStop();
            _LastSeatCountSum = SeatManage.Bll.TerminalOperatorService.LastSeatCount(clientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeatCountSum");
            MyLastSeatSumTime.TimeStrat();
        }

        #endregion
    }
}
