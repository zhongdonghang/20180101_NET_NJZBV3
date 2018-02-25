using SeatClientV3.OperateResult;
using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace SeatClientV3.UCViewModel
{
    public class LastSeatBtn_ViewModel : INotifyPropertyChanged
    {
        Dictionary<string, ReadingRoomSeatUsedState_Ex> _RoomStatus = new Dictionary<string, ReadingRoomSeatUsedState_Ex>();
        /// <summary>
        /// 阅览室使用情况
        /// </summary>
        public Dictionary<string, ReadingRoomSeatUsedState_Ex> RoomStatus
        {
            get { return _RoomStatus; }
            set { _RoomStatus = value; }
        }
        SystemObject clientObject;
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject Clientobject
        {
            get { return clientObject; }
        }
        /// <summary>
        /// 剩余座位
        /// </summary>
        private double lastSeat = 0;
        /// <summary>
        /// 座位总数
        /// </summary>
        private double allSeat = 0;
        /// <summary>
        /// 遮挡矩形高度
        /// </summary>
        public double RecHeight
        {
            get
            {
                if (allSeat == 0)
                {
                    return 100;
                }
                else
                {
                    return 100 * (lastSeat / allSeat);
                }
            }
        }
        #region 座位数

        Thread MyLastSeatSum = null;
        public SeatManage.SeatManageComm.TimeLoop MyLastSeatSumTime = null;
        public void LastSeatRun()
        {
            clientObject = SystemObject.GetInstance();
            GetLastSeat();
            MyLastSeatSumTime = new SeatManage.SeatManageComm.TimeLoop(30 * 1000);
            MyLastSeatSumTime.TimeTo += new EventHandler(MyLastSeatSumTime_TimeTo);
            MyLastSeatSum = new Thread(new ThreadStart(MyLastSeatSumTime.TimeStrat));
            MyLastSeatSum.Start();
        }

        void MyLastSeatSumTime_TimeTo(object sender, EventArgs e)
        {
            MyLastSeatSumTime.TimeStop();
            GetLastSeat();
            MyLastSeatSumTime.TimeStrat();
        }
        /// <summary>
        /// 计算剩余座位
        /// </summary>
        private void GetLastSeat()
        {
            try
            {
                lastSeat=0;
                allSeat=0;
                RoomStatus = SeatManage.Bll.TerminalOperatorService.GetTeminaRoomStatus(clientObject.ClientSetting.DeviceSetting.Rooms);
                foreach (KeyValuePair<string, ReadingRoomSeatUsedState_Ex> room in RoomStatus)
                {
                    Code.NowReadingRoomState state =new Code.NowReadingRoomState(room.Value.ReadingRoom,false);
                    if (state.RoomOpenState != SeatManage.EnumType.ReadingRoomStatus.Close)
                    {
                        allSeat += room.Value.SeatAmountAll;
                        lastSeat += room.Value.SeatAmountFree;
                    }
                }
                OnPropertyChanged("RecHeight");
            }
            catch(Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室使用状态出错：" + ex.Message);
            }
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
    }
}
