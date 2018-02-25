using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
namespace SeatManage.Bll
{
    /// <summary>
    /// 阅览室当前状态
    /// </summary>
    [Serializable]
    public class NowReadingRoomState
    {
        public NowReadingRoomState()
        { }
        public NowReadingRoomState(ReadingRoomInfo room)
            : this(room, DateTime.Now)
        { }

        public NowReadingRoomState(ReadingRoomInfo room, DateTime Time)
        {
            //根据设置判断当前阅览室的选座方式
            _SeatChooseMode = RoomSelectSeatMode(room.Setting.SeatChooseMethod, Time);
            //判断阅览室的开放状态
            roomState = ReadingRoomOpenState(room.Setting.RoomOpenSet, Time);
            //计算座位保留时长
            _SeatSaveTime = GetSeatHoldTime(room.Setting.SeatHoldTime, Time);

            _CanBespeakSeat = IsCanBespeakSeat(room.Setting.SeatBespeak, Time);
            _SeatUsedInfo = GetRoomSeatUsedState(room.No);
        }
        private SelectSeatMode _SeatChooseMode = SelectSeatMode.OptionalMode;
        private ReadingRoomStatus roomState = ReadingRoomStatus.Close;
        private double _SeatSaveTime = 0;
        private bool _CanBespeakSeat = false;
        private ReadingRoomSeatUsedState _SeatUsedInfo = new ReadingRoomSeatUsedState();


        /// <summary>
        /// 阅览室座位的使用状态
        /// </summary>
        public ReadingRoomSeatUsedState SeatUsedInfo
        {
            get { return _SeatUsedInfo; }
            set { _SeatUsedInfo = value; }
        }

        /// <summary>
        /// 是否可以预约
        /// </summary>
        public bool CanBespeakSeat
        {
            get { return _CanBespeakSeat; }
            set { _CanBespeakSeat = value; }
        }
        /// <summary>
        /// 座位保留时长
        /// </summary>
        public double SeatSaveTime
        {
            get { return _SeatSaveTime; }
            set { _SeatSaveTime = value; }
        }


        /// <summary>
        /// 当前开放状态
        /// </summary>
        public ReadingRoomStatus RoomOpenState
        {
            get { return roomState; }
            set { roomState = value; }
        }
        /// <summary>
        /// 选座方式
        /// </summary>
        public SelectSeatMode SeatChooseMode
        {
            get { return _SeatChooseMode; }
            set { _SeatChooseMode = value; }
        }


        #region 私有方法
        /// <summary>
        /// 获取阅览室内座位的使用状态
        /// </summary>
        /// <param name="roomNum">房间号</param>
        /// <returns></returns>
        public static ReadingRoomSeatUsedState GetRoomSeatUsedState(string roomNum)
        {

            List<string> roomNums = new List<string>();
            roomNums.Add(roomNum);
            Dictionary<string, ReadingRoomSeatUsedState> list = GetRoomSeatUsedState(roomNums);
            if (list.Count > 0)
            {
                return list[roomNum];
            }
            else
            {
                return null;
            }
        }

        public static Dictionary<string, ReadingRoomSeatUsedState> GetRoomSeatUsedState(List<string> roomNums)
        {
            //TODO:获取阅览室内座位的使用状态; 
            Dictionary<string, ReadingRoomSeatUsedState> seatUsedState = EnterOutOperate.GetReadingRoomSeatUsingState(roomNums);

            return seatUsedState;
        }
        /// <summary>
        /// 获取阅览室中座位预约的情况
        /// </summary>
        /// <param name="roomNum"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static SeatBepeakState GetSeatBespeakState(string roomNum, DateTime date)
        {
            SeatBepeakState seatBespeatState = new SeatBepeakState();
            return seatBespeatState;
        }
        /// <summary>
        /// 判断当前时间是否可以预约座位
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsCanBespeakSeat(SeatBespeakSet bespeakSet, DateTime time)
        {
            if (bespeakSet.Used)
            {
                foreach (TimeSpace ts in bespeakSet.NoBespeakDates)
                {
                    DateTime begintime = DateTime.Parse(time.Year.ToString() + "-" +ts.BeginTime);
                     DateTime endtime = DateTime.Parse(time.Year.ToString() + "-" + ts.EndTime);
                    //判断读者选择的时间是否在不允许预约的时间范围内
                    if (DateTimeOperate.DateAccord(begintime, endtime, time))
                    {
                        return false;
                    }
                }

                //指定的时间减去提前预约的天数， 

                DateTime begindate = time.AddDays(-bespeakSet.BespeakBeforeDays);
                if (DateTimeOperate.DateAccord(begindate, time, DateTime.Now))
                {
                    //当前时间在允许预约的时间之间，返回允许预约

                    DateTime canBespeakBegin = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + bespeakSet.CanBespeatTimeSpace.BeginTime);
                    DateTime canBespeakEnd = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + bespeakSet.CanBespeatTimeSpace.EndTime);
                    if (DateTimeOperate.DateAccord(canBespeakBegin, canBespeakEnd, DateTime.Now))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
        /// <summary>
        /// 计算座位保留时长
        /// </summary>
        /// <param name="set">设置</param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static double GetSeatHoldTime(SeatHoldTimeSet set, DateTime time)
        {
            if (set.UsedAdvancedSet)
            {
                foreach (SeatHoldTimeOption option in set.AdvancedSeatHoldTime)
                {
                    if (option.Used)
                    { //判断指定的时间是否在开始时间和结束时间中间
                        DateTime begintime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endtime = DateTime.Parse(time.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(begintime, endtime, time))
                        {
                            return option.HoldTimeLength;
                        }
                    }
                }
                //遍历结束没有返回，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
            else
            {
                //没有启用阅览室设置，则返回默认保留时长
                return set.DefaultHoldTimeLength;
            }
        }
        /// <summary>
        /// 阅览室开放状态
        /// </summary>
        /// <param name="openSeat"></param>
        /// <returns></returns>
        public static ReadingRoomStatus ReadingRoomOpenState(RoomOpenTimeSet openSeat, DateTime time)
        {
            ReadingRoomStatus openState = ReadingRoomStatus.Close;

            if (openSeat.UsedAdvancedSet)//启用高级设置
            {
                DayOfWeek day = time.DayOfWeek;
                try
                {
                    RoomOpenPlanSet plan = openSeat.RoomOpenPlan[day];

                    if (plan.Used)
                    {
                        foreach (TimeSpace t in plan.OpenTime)
                        {
                            openState = calcRoomState(t.BeginTime, t.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                            switch (openState)
                            { //当前时间阅览室状态为非关闭状态，直接返回结果。否则继续判断
                                case ReadingRoomStatus.BeforeClose:
                                case ReadingRoomStatus.BeforeOpen:
                                case ReadingRoomStatus.Open:
                                    return openState;
                            }
                        }
                        //遍历结束没有返回，则返回最后一次计算的结果
                        return openState;
                    }
                    else
                    {
                        //否则当天没启用高级设置，返回默认开馆状态
                        openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                        return openState;
                    }
                }
                catch
                {
                    //当天没有高级设置，则返回默认开馆状态。
                    openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                    return openState;
                }
            }
            else
            {
                //没有开启高级设置，则返回默认开馆状态。
                openState = calcRoomState(openSeat.DefaultOpenTime.BeginTime, openSeat.DefaultOpenTime.EndTime, time, openSeat.OpenBeforeTimeLength, openSeat.CloseBeforeTimeLength);
                return openState;
            }

        }
        /// <summary>
        /// 根据时间计算阅览室的状态
        /// </summary>
        /// <param name="beginTime">开馆时间</param>
        /// <param name="endTime">闭馆时间</param>
        /// <param name="datetime">要判断开放状态的时间</param>
        /// <param name="openBeforeTimeLength">开馆预处理</param>
        /// <param name="closeBeforeTimeLength">闭馆预处理</param>
        /// <returns></returns>
        private static ReadingRoomStatus calcRoomState(string beginTime, string endTime, DateTime datetime, double openBeforeTimeLength, double closeBeforeTimeLength)
        {
            DateTime begindate = DateTime.Parse(datetime.ToShortDateString() + " " + beginTime);
            DateTime enddate = DateTime.Parse(datetime.ToShortDateString() + " " + endTime);

            if (DateTimeOperate.DateAccord(enddate.AddMinutes(-closeBeforeTimeLength), enddate, datetime))//判断是否符合闭馆预处理
            {
                return ReadingRoomStatus.BeforeClose;
            }
            else if (DateTimeOperate.DateAccord(begindate, enddate, datetime))
            {
                return ReadingRoomStatus.Open;
            }
            else if (DateTimeOperate.DateAccord(begindate.AddMinutes(-openBeforeTimeLength), begindate, datetime))//判断是否符合开馆预处理
            {
                return ReadingRoomStatus.BeforeOpen;
            }
            else
            {
                return ReadingRoomStatus.Close;//条件都不符合，则为闭馆。
            }
        }

        /// <summary>
        /// 判断当前时间阅览室选座状态
        /// </summary>
        /// <param name="set"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static SelectSeatMode RoomSelectSeatMode(SeatChooseMethodSet set, DateTime date)
        {
            SelectSeatMode chooseMethod = set.DefaultChooseMethod;
            //判断是否启用高级设置
            if (set.UsedAdvancedSet)
            {
                DayOfWeek day = date.DayOfWeek;
                try
                {
                    SeatChooseMethodPlan plan = set.AdvancedSelectSeatMode[day];
                    DateTime strDate = date;
                    //遍历当天的时间段，判断是是否有满足当前时间的设置项
                    foreach (SeatChooseMethodOption option in plan.PlanOption)
                    {
                        DateTime beginDatetime = DateTime.Parse(strDate.ToShortDateString() + " " + option.UsedTime.BeginTime);
                        DateTime endDatetime = DateTime.Parse(strDate.ToShortDateString() + " " + option.UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(beginDatetime, endDatetime, date))//判断当前时间是否满足项
                        {
                            chooseMethod = option.ChooseMethod;
                            break;
                        }
                    }

                }
                catch
                {
                    chooseMethod = set.DefaultChooseMethod;
                }
            }

            return chooseMethod;
        }

        #endregion
    }
}
