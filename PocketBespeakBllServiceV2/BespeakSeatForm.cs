using System;
using System.Collections.Generic;
using System.Linq;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatManage.PocketBespeakBllServiceV2
{
    public partial class PocketBespeakBllService : IPocketBespeakBllServiceV2.IPocketBespeakBllService
    {
        /// <summary>
        /// 获取指定日期开放预约的阅览室。
        /// 1.判断阅览室是否开放预约。
        /// 2.判断当天是否可以预约。
        /// </summary>
        /// <param name="bespeatDate"></param>
        /// <returns></returns>
        public List<ClassModel.ReadingRoomInfo> GetCanBespeakReaderRoomInfo(DateTime bespeatDate)
        {
            List<ClassModel.ReadingRoomInfo> canBespeakReadingRoom = new List<ReadingRoomInfo>();
            List<ClassModel.ReadingRoomInfo> allReadingRooms = seatManage.GetReadingRoomInfo(null);
            List<string> roomNums = new List<string>();
            foreach (ClassModel.ReadingRoomInfo room in allReadingRooms)
            {

                try
                {
                    SeatManage.ClassModel.ReadingRoomSetting set = room.Setting;
                    if (!set.SeatBespeak.Used)
                    {
                        continue;
                    }
                    if (!dateBespeak(set.SeatBespeak, bespeatDate, DateTime.Now))
                    {
                        continue;
                    }
                    //room.SeatList.Seats.Clear();
                    room.SeatList.Notes.Clear();//只获取阅览室信息，清空座位信息。
                    canBespeakReadingRoom.Add(room);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return canBespeakReadingRoom;
        }
        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="bespeakDate"></param>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        public List<ClassModel.Seat> GetBookSeatList(DateTime bespeakDate, string RoomId)
        {
            List<ClassModel.Seat> canBespeakSeat = new List<Seat>();
            List<string> roomNums = new List<string>();
            roomNums.Add(RoomId);
            List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(roomNums);
            {
                if (!timeCanBespeak(rooms[0].Setting.SeatBespeak, DateTime.Now))
                {
                    throw new SeatBespeakException.BespeakSeatFailed(string.Format("预约时间为：{0}到{1}", rooms[0].Setting.SeatBespeak.CanBespeatTimeSpace.BeginTime, rooms[0].Setting.SeatBespeak.CanBespeatTimeSpace.EndTime));
                }
            }
            SeatLayout bespeakSeatLayout = seatManage.GetBeseakSeatLayout(RoomId, bespeakDate);
            foreach (Seat seat in bespeakSeatLayout.Seats.Values)
            {
                if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                {
                    if (seat.CanBeBespeak && seat.CanBespeakSpan.Count > 0 && !seat.IsSuspended)
                    {
                        canBespeakSeat.Add(seat);
                    }
                }
                else
                {
                    if (seat.CanBeBespeak && seat.SeatUsedState != EnumType.EnterOutLogType.BookingConfirmation && !seat.IsSuspended)
                    {
                        canBespeakSeat.Add(seat);
                    }
                }
            }
            return canBespeakSeat;
        }
        /// <summary>
        /// 提交预约信息
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        public string SubmitBespeakInfo(ClassModel.BespeakLogInfo bespeakInfo)
        {
            string returnValue = "";
            List<BespeakLogInfo> bespeak = seatManage.GetBespeakLogInfo(bespeakInfo.CardNo, bespeakInfo.BsepeakTime);
            List<BespeakLogInfo> seatbespeak = seatManage.GetBespeakLogInfoBySeatNo(bespeakInfo.SeatNo, bespeakInfo.BsepeakTime);
            List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(new List<string>() { bespeakInfo.ReadingRoomNo });
            if (bespeakInfo.SubmitTime > bespeakInfo.BsepeakTime)
            {
                throw new SeatBespeakException.BespeakSeatFailed("选择的预约的时间错误。");
            }
            if (rooms.Count == 0)
            {
                throw new SeatBespeakException.BespeakSeatFailed("获取阅览室信息失败。");
            }
            if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
            {
                if (bespeak.Count >= rooms[0].Setting.SeatBespeak.BespeakSeatCount)
                {
                    throw new SeatBespeakException.BespeakSeatFailed("对不起，您一天最多预约" + rooms[0].Setting.SeatBespeak.BespeakSeatCount + "个座位。");
                }

                foreach (BespeakLogInfo b in bespeak)
                {
                    if (b.BsepeakTime == bespeakInfo.BsepeakTime && b.BsepeakState == BookingStatus.Waiting)
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("对不起，同一时间段只能预约一个座位。");
                    }
                }
                foreach (BespeakLogInfo b in seatbespeak)
                {
                    if (b.BsepeakTime == bespeakInfo.BsepeakTime)
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("对不起，此时间段已被预约。");
                    }
                }
            }
            else
            {
                if (bespeak.Count > 0)
                {
                    throw new SeatBespeakException.BespeakSeatFailed("对不起，当前日期您已有等待签到的座位。");
                }
                foreach (BespeakLogInfo b in seatbespeak)
                {
                    if (b.BsepeakState == EnumType.BookingStatus.Waiting)
                    {
                        throw new Exception("所选座位已经被预约。");
                    }
                }
            }
            List<BlackListInfo> blackLog = seatManage.GetBlacklistInfo(bespeakInfo.CardNo);

            if (blackLog.Count > 0)
            {
                throw new SeatBespeakException.BespeakSeatFailed("你已被记录黑名单，无法预约座位。");
            }
            if (rooms[0].Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
            {
                List<BespeakLogInfo> bespeaklogs = seatManage.GetBespeakLogInfoByRoomsNum(new List<string>() { bespeakInfo.ReadingRoomNo }, bespeakInfo.BsepeakTime);
                int canbookCount = (int)((rooms[0].SeatList.Seats.Count - rooms[0].SeatList.Seats.Where(u => u.Value.IsSuspended).ToArray().Count()) * rooms[0].Setting.SeatBespeak.BespeakArea.Scale);
                if (bespeaklogs.Count >= canbookCount)
                {
                    throw new SeatBespeakException.BespeakSeatFailed("对不起当前阅览室已经没有可预约的座位。");
                }
            }
            if (rooms.Count > 0)
            {
                ReadingRoomInfo r = rooms[0];
                if (!r.Setting.IsCanBespeakSeat(bespeakInfo.BsepeakTime))
                {
                    throw new SeatBespeakException.BespeakSeatFailed("对不起当前时间不开放预约。");
                }
                if (dateBespeak(r.Setting.SeatBespeak, bespeakInfo.BsepeakTime, DateTime.Now))//验证日期是否合法。
                {
                    EnumType.HandleResult result = seatManage.AddBespeakLogInfo(bespeakInfo);
                    if (result == EnumType.HandleResult.Successed)
                    {
                        string beginTime = bespeakInfo.BsepeakTime.AddMinutes(-double.Parse(r.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        string endTime = bespeakInfo.BsepeakTime.AddMinutes(double.Parse(r.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                        returnValue = string.Format("座位预约成功，请在{0}至{1}到图书馆刷卡确认", beginTime, endTime);
                    }
                    else
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("所选座位已经被预约。");
                    }
                }
                else
                {
                    throw new SeatBespeakException.BespeakSeatFailed("选择的日期不允许预约。");
                }
            }
            return returnValue;
        }
        /// <summary>
        /// 获取当天可预约的阅览室
        /// </summary>
        /// <returns></returns>
        public List<ReadingRoomInfo> GetCanBespeakNowDayRoomInfo()
        {
            List<ClassModel.ReadingRoomInfo> canBespeakReadingRoom = new List<ReadingRoomInfo>();
            List<ClassModel.ReadingRoomInfo> allReadingRooms = seatManage.GetReadingRoomInfo(null);
            List<string> roomNums = new List<string>();
            foreach (ClassModel.ReadingRoomInfo room in allReadingRooms)
            {

                try
                {
                    SeatManage.ClassModel.ReadingRoomSetting set = room.Setting;
                    if (!set.SeatBespeak.Used || !set.SeatBespeak.NowDayBespeak)
                    {
                        continue;
                    }
                    room.SeatList.Seats.Clear();
                    room.SeatList.Notes.Clear();//只获取阅览室信息，清空座位信息。
                    canBespeakReadingRoom.Add(room);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return canBespeakReadingRoom;
        }
        /// <summary>
        /// 获取当天预约座位列表
        /// </summary>
        /// <param name="RoomId"></param>
        /// <returns></returns>
        public List<Seat> GetNowBookSeatList(string RoomId)
        {
            List<ClassModel.Seat> canBespeakSeat = new List<Seat>();
            List<string> roomNums = new List<string>();
            roomNums.Add(RoomId);
            List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(roomNums);
            SeatLayout bespeakSeatLayout = seatManage.GetBeseakSeatLayout(RoomId, DateTime.Now);
            SeatLayout useSeatLayout = seatManage.GetRoomSeatLayOut(RoomId);
            foreach (KeyValuePair<string, Seat> uSeat in useSeatLayout.Seats)
            {
                if (uSeat.Value.SeatUsedState == EnterOutLogType.BespeakWaiting)
                {
                    if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                    {
                        if (!bespeakSeatLayout.Seats[uSeat.Key].IsSuspended)
                        {
                            canBespeakSeat.Add(bespeakSeatLayout.Seats[uSeat.Key]);
                        }
                    }
                    continue;
                }
                if ((uSeat.Value.SeatUsedState == EnterOutLogType.Leave || uSeat.Value.SeatUsedState == EnterOutLogType.None) && !uSeat.Value.IsSuspended)
                {
                    if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                    {
                        canBespeakSeat.Add(bespeakSeatLayout.Seats[uSeat.Key]);
                    }
                    else
                    {
                        canBespeakSeat.Add(uSeat.Value);
                    }
                }
            }
            return canBespeakSeat;
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="bespeakInfo"></param>
        /// <returns></returns>
        public string SubmitNowDayBespeakInfo(BespeakLogInfo bespeakInfo)
        {
            string returnValue = "";
            if (bespeakInfo.SubmitTime > bespeakInfo.BsepeakTime)
            {
                throw new SeatBespeakException.BespeakSeatFailed("选择的预约的时间错误。");
            }
            List<BespeakLogInfo> seatbespeak = seatManage.GetBespeakLogInfoBySeatNo(bespeakInfo.SeatNo, bespeakInfo.BsepeakTime);
            List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(new List<string>() { bespeakInfo.ReadingRoomNo });
            if (rooms.Count == 0)
            {
                throw new SeatBespeakException.BespeakSeatFailed("获取阅览室信息失败。");
            }
            List<BespeakLogInfo> bespeak = seatManage.GetBespeakLogs(bespeakInfo.CardNo, null, bespeakInfo.BsepeakTime, 0, new List<SeatManage.EnumType.BookingStatus> { SeatManage.EnumType.BookingStatus.Confinmed, SeatManage.EnumType.BookingStatus.Waiting });//.GetBespeakList(this.LoginId, null, date, 0, bespeakStatus);
            if (bespeak.Count >= rooms[0].Setting.SeatBespeak.BespeakSeatCount)
            {
                throw new SeatBespeakException.BespeakSeatFailed("对不起，您一天最多预约" + rooms[0].Setting.SeatBespeak.BespeakSeatCount + "个座位。");
            }
            //判断读者是否有座位
            if (!rooms[0].Setting.SeatBespeak.BespeatWithOnSeat)
            {
                SeatManage.ClassModel.EnterOutLogInfo eol = seatManage.GetEnterOutLogInfoByCardNo(bespeakInfo.CardNo);
                if (eol != null && eol.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    throw new SeatBespeakException.BespeakSeatFailed("您已有座位，不能再预约座位");
                }
            }
            Seat seatInfo = seatManage.GetSeatInfoBySeatNum(bespeakInfo.SeatNo);
            if (seatInfo != null && seatInfo.SeatUsedState != EnterOutLogType.Leave)
            {
                throw new SeatBespeakException.BespeakSeatFailed("对不起，此座位正在被使用。");
            }

            if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
            {
                foreach (BespeakLogInfo b in seatbespeak)
                {
                    if (b.BsepeakTime == bespeakInfo.BsepeakTime)
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("对不起，此时间段已被预约。");
                    }
                }
                foreach (BespeakLogInfo b in bespeak)
                {
                    if (b.BsepeakTime == bespeakInfo.BsepeakTime && b.BsepeakState == BookingStatus.Waiting)
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("对不起，同一时间段只能预约一个座位。");
                    }
                }
                if (bespeakInfo.SubmitTime == bespeakInfo.BsepeakTime)
                {
                    foreach (BespeakLogInfo b in bespeak)
                    {
                        if (b.BsepeakTime == b.SubmitTime && b.BsepeakState == BookingStatus.Waiting)
                        {
                            throw new SeatBespeakException.BespeakSeatFailed("对不起，只能同时进行一次及时预约。");
                        }
                    }
                }
            }
            else
            {
                foreach (BespeakLogInfo b in bespeak)
                {
                    if (b.BsepeakState == BookingStatus.Waiting)
                    {
                        throw new SeatBespeakException.BespeakSeatFailed("对不起，您已有等待签到的座位。");
                    }
                }
                foreach (BespeakLogInfo b in seatbespeak)
                {
                    if (b.BsepeakState == EnumType.BookingStatus.Waiting)
                    {
                        throw new Exception("所选座位已经被预约。");
                    }
                }
            }
            List<BlackListInfo> blackLog = seatManage.GetBlacklistInfo(bespeakInfo.CardNo);

            if (blackLog.Count > 0)
            {
                throw new SeatBespeakException.BespeakSeatFailed("你已被记录黑名单，无法预约座位。");
            }

            if (rooms.Count > 0)
            {
                ReadingRoomInfo r = rooms[0];
                EnumType.HandleResult result = seatManage.AddBespeakLogInfo(bespeakInfo);
                if (result == EnumType.HandleResult.Successed)
                {
                    string beginTime;
                    string endTime;
                    if (bespeakInfo.SubmitTime == bespeakInfo.BsepeakTime)
                    {
                        beginTime = bespeakInfo.BsepeakTime.ToShortTimeString();
                        endTime = bespeakInfo.BsepeakTime.AddMinutes(r.Setting.SeatBespeak.SeatKeepTime).ToShortTimeString();
                    }
                    else
                    {
                        beginTime = bespeakInfo.BsepeakTime.AddMinutes(-double.Parse(r.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        endTime = bespeakInfo.BsepeakTime.AddMinutes(double.Parse(r.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    }
                    returnValue = string.Format("座位预约成功，请在{0}至{1}到图书馆刷卡确认", beginTime, endTime);
                }
                else
                {
                    throw new SeatBespeakException.BespeakSeatFailed("所选座位已经被预约。");
                }
            }
            return returnValue;
        }


        #region 私有方法
        /// <summary>
        /// 判断选择的日期是否可以预约，false为不可预约
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private bool dateBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime bespeakDate, DateTime nowDate)
        {
            DateTime selectedDate = bespeakDate;
            for (int i = 0; i < set.NoBespeakDates.Count; i++)
            {
                try
                {

                    DateTime sDate = DateTime.Parse(nowDate.Month.ToString() + "-" + nowDate.Day.ToString());
                    DateTime beginDate = DateTime.Parse(set.NoBespeakDates[i].BeginTime);
                    DateTime endDate = DateTime.Parse(set.NoBespeakDates[i].EndTime);
                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginDate, endDate, sDate) || sDate.CompareTo(beginDate) == 0 || sDate.CompareTo(endDate) == 0)
                    {//如果当前时间符合某个不可预约的规则，则直接返回false，不可预约
                        return false;
                    }
                }
                catch
                {//日期转换遇到异常，则忽略 
                }
            }
            //判断当天是否大于选择的日期
            TimeSpan span = selectedDate.Date - nowDate.Date;
            if (span.Days > set.BespeakBeforeDays)
            {
                return false;
            }
            if (span.Days <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 判断当前时间是否可以预约
        /// </summary>
        /// <param name="set"></param>
        /// <param name="nowDate"></param>
        /// <returns></returns>
        private bool timeCanBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {
            try
            {
                DateTime beginTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.BeginTime));
                DateTime endTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.EndTime));
                if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginTime, endTime, nowDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        #endregion



        /// <summary>
        /// 获取座位信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        public ClassModel.BespeakSeatModel.ScanCodeViewModel GetScanCodeSeatInfo(string cardNo, string seatNum, string readingRoomNum)
        {
            try
            {
                SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel scanCode = new ClassModel.BespeakSeatModel.ScanCodeViewModel();
                scanCode.ReaderInfo = GetReaderInfo(cardNo);//预约记录为当天的预约记录
                scanCode.SeatInfo = seatManage.GetSeatInfoBySeatNum(seatNum);
                if (scanCode.SeatInfo.ReadingRoom.Setting.SeatBespeak.Used && dateBespeak(scanCode.SeatInfo.ReadingRoom.Setting.SeatBespeak, DateTime.Now.AddDays(1), DateTime.Now))//验证阅览室是否开放预约
                {
                    List<Seat> seats = GetBookSeatList(DateTime.Now.AddDays(1), readingRoomNum);
                    if (scanCode.SeatInfo.ReadingRoom.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                    {
                        int stopSeatCount = 0;
                        foreach (KeyValuePair<string, Seat> item in scanCode.SeatInfo.ReadingRoom.SeatList.Seats)
                        {
                            if (item.Value.IsSuspended)
                            {
                                stopSeatCount++;
                            }
                        }
                        int bookdCount = scanCode.SeatInfo.ReadingRoom.SeatList.Seats.Count - seats.Count;
                        int canbookCount = (int)((scanCode.SeatInfo.ReadingRoom.SeatList.Seats.Count - stopSeatCount) * scanCode.SeatInfo.ReadingRoom.Setting.SeatBespeak.BespeakArea.Scale);
                        if (bookdCount >= canbookCount)
                        {
                            scanCode.SeatInfo.CanBeBespeak = false;
                        }
                        else
                        {
                            List<SeatManage.ClassModel.BespeakLogInfo> bespeaks = seatManage.GetBespeakLogInfoBySeatNo(seatNum, DateTime.Now.AddDays(1));
                            if (bespeaks.Count > 0)
                            {
                                scanCode.SeatInfo.CanBeBespeak = false;
                            }
                            else
                            {
                                scanCode.SeatInfo.CanBeBespeak = true;
                            }
                        }
                    }
                    else
                    {
                        scanCode.SeatInfo.CanBeBespeak = false;//是否可以被预约设置为false，在可预约的座位循环中如果该座位可以被预约，再设置为true。
                        foreach (Seat seat in seats)
                        {
                            if (seat.SeatNo == seatNum)
                            {
                                scanCode.SeatInfo.CanBeBespeak = true;
                                break;
                            }
                        }
                        List<BespeakLogInfo> bespeaks = seatManage.GetBespeakLogInfo(cardNo, DateTime.Now.AddDays(1));
                        if (bespeaks.Count > 0)
                        {
                            scanCode.BespeakLog = bespeaks[0];
                        }
                        else
                        {
                            scanCode.BespeakLog = null;
                        }
                    }
                }
                return scanCode;
            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="seatNum">要更换的座位号</param>
        /// <param name="readingRoomNum">阅览室编号</param>
        /// <returns></returns>
        public string ChangeSeat(string cardNo, string seatNum, string readingRoomNum)
        {
            try
            {
                List<string> roomNums = new List<string>();
                roomNums.Add(readingRoomNum);
                List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(roomNums);
                if (rooms.Count == 0)
                {
                    return "没有找到对应的阅览室";
                }
                ReadingRoomSetting roomSet = rooms[0].Setting;
                ReaderInfo reader = GetReaderInfo(cardNo);
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    nowReaderStatus = reader.EnterOutLog.EnterOutState;
                }
                else if (reader.BespeakLog.Count > 0)
                {
                    nowReaderStatus = EnterOutLogType.BespeakWaiting;
                }
                else if (reader.WaitSeatLog != null)
                {
                    nowReaderStatus = EnterOutLogType.Waiting;
                }

                switch (nowReaderStatus)
                {
                    case EnterOutLogType.Leave:
                        return "你还没有选座";
                    case EnterOutLogType.BespeakWaiting:
                        if (reader.BespeakLog[0].SeatNo == seatNum && reader.BespeakLog[0].BsepeakState == BookingStatus.Waiting)
                        {
                            EnterOutLogInfo newEnterOutLog = new EnterOutLogInfo();//构造 
                            newEnterOutLog.CardNo = reader.BespeakLog[0].CardNo;
                            newEnterOutLog.EnterOutLogNo = SeatComm.RndNum();
                            newEnterOutLog.EnterOutState = EnterOutLogType.BookingConfirmation;
                            newEnterOutLog.EnterOutType = LogStatus.Valid;
                            newEnterOutLog.Flag = Operation.Reader;
                            newEnterOutLog.ReadingRoomNo = reader.BespeakLog[0].ReadingRoomNo;
                            newEnterOutLog.SeatNo = reader.BespeakLog[0].SeatNo;
                            newEnterOutLog.Remark = string.Format("通过扫码入座预约的{0} {1}号座位", reader.BespeakLog[0].ReadingRoomName, reader.BespeakLog[0].ShortSeatNum);
                            int logid = -1;
                            try
                            {
                                HandleResult result = seatManage.AddEnterOutLogInfo(newEnterOutLog, ref logid); //添加入座记录
                                if (result == HandleResult.Successed)
                                {
                                    reader.BespeakLog[0].BsepeakState = BookingStatus.Confinmed;
                                    reader.BespeakLog[0].CancelPerson = Operation.Reader;
                                    reader.BespeakLog[0].CancelTime = seatManage.GetServerDateTime();
                                    reader.BespeakLog[0].Remark = string.Format("通过扫码入座预约的{0} {1}号座位", reader.BespeakLog[0].ReadingRoomName, reader.BespeakLog[0].ShortSeatNum);
                                    seatManage.UpdateBespeakLogInfo(reader.BespeakLog[0]);
                                }
                                else
                                {
                                    return "预约入座失败。";
                                }
                            }
                            catch (Exception ex)
                            {
                                SeatManageComm.WriteLog.Write(string.Format("扫码预约入座确认失败：{0}", ex.Message));
                                return "未知原因，预约入座确认失败";
                            }
                        }
                        else
                        {
                            return string.Format("您已预约{0} {1}号座位，请扫该座位上的条形码", reader.BespeakLog[0].ReadingRoomName, reader.BespeakLog[0].ShortSeatNum);
                        }
                        break;
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                    case EnterOutLogType.ShortLeave:
                        if (seatManage.GetReaderChooseSeatTimes(cardNo, roomSet.PosTimes.Minutes) >= roomSet.PosTimes.Times)
                        {
                            return "选座频繁。";
                        }
                        EnterOutLogInfo enterOutlog = reader.EnterOutLog;
                        enterOutlog.ReadingRoomNo = readingRoomNum;
                        enterOutlog.Remark = "通过扫码更换到该座位";
                        enterOutlog.SeatNo = seatNum;
                        enterOutlog.Flag = EnumType.Operation.Reader;
                        enterOutlog.EnterOutType = EnumType.LogStatus.Valid;
                        enterOutlog.EnterOutState = EnumType.EnterOutLogType.ReselectSeat;
                        enterOutlog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
                        int newLogId = -1;
                        if (seatManage.AddEnterOutLogInfo(enterOutlog, ref newLogId) == HandleResult.Successed)
                        {
                            return "";
                        }
                        else
                        {
                            return "未知原因，更换座位失败";
                        }
                    case EnterOutLogType.Waiting:
                        return "您当前在等待其他座位";
                }
                return "读者状态错误";
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write(string.Format("扫码入座失败：{0}", ex.Message));
                return "系统错误，更换座位失败";
            }
        }




        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="bookNo"></param>
        /// <returns></returns>
        public string CheckSeat(int bookNo)
        {
            try
            {
                DateTime nowDate = DateTime.Now;
                BespeakLogInfo bespeaklog = seatManage.GetBespeaklogById(bookNo);
                if (bespeaklog == null)
                {
                    return "获取预约记录失败";
                }
                if (bespeaklog.BsepeakState != BookingStatus.Waiting)
                {
                    return "此条记录状态无效，请查询刷新页面";
                }

                List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(new List<string>() { bespeaklog.ReadingRoomNo });
                if (rooms.Count < 1)
                {
                    return "阅览室设置获取失败";
                }
                ReadingRoomSetting set = rooms[0].Setting;
                DateTime dtBegin = bespeaklog.BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd = bespeaklog.BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowDate) || (set.SeatBespeak.NowDayBespeak && bespeaklog.SubmitTime == bespeaklog.BsepeakTime))
                {
                    //TODO:预约时间在开始时间和结束时间之间，执行预约确认操作
                    //TODO:预约确认时，判断当前座位上是否有人。
                    EnterOutLogInfo seatUsedInfo = seatManage.GetEnterOutLogInfoBySeatNum(bespeaklog.SeatNo);
                    if (seatUsedInfo != null && seatUsedInfo.EnterOutState != EnterOutLogType.Leave)
                    { //条件满足，说明座位正在使用。
                        seatUsedInfo.EnterOutState = EnterOutLogType.Leave;
                        seatUsedInfo.EnterOutType = LogStatus.Valid;
                        seatUsedInfo.Remark = string.Format("预约该座位的读者在手机网站签到入座，设置在座读者离开");
                        seatUsedInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        if (seatManage.AddEnterOutLogInfo(seatUsedInfo, ref newId) == HandleResult.Successed)
                        {
                            List<WaitSeatLogInfo> waitInfoList = seatManage.GetWaitLogList(null, seatUsedInfo.EnterOutLogID, null, null, null);
                            if (waitInfoList.Count > 0)
                            {
                                WaitSeatLogInfo WaitSeatLogModel = waitInfoList[0];
                                WaitSeatLogModel.OperateType = Operation.Reader;
                                WaitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                                WaitSeatLogModel.NowState = LogStatus.Valid;
                                if (!seatManage.UpdateWaitLog(WaitSeatLogModel))
                                {
                                    return "取消等待此座位的读者状态失败";
                                }
                            }
                        }
                        else
                        {
                            return "设置当前使用此座位的读者离开失败";
                        }
                    }
                    EnterOutLogInfo newEnterOutLog = new EnterOutLogInfo();//构造 
                    newEnterOutLog.CardNo = bespeaklog.CardNo;
                    newEnterOutLog.EnterOutLogNo = SeatComm.RndNum();
                    newEnterOutLog.EnterOutState = EnterOutLogType.BookingConfirmation;
                    newEnterOutLog.EnterOutType = LogStatus.Valid;
                    newEnterOutLog.Flag = Operation.Reader;
                    newEnterOutLog.ReadingRoomNo = bespeaklog.ReadingRoomNo;
                    newEnterOutLog.ReadingRoomName = bespeaklog.ReadingRoomName;
                    newEnterOutLog.ShortSeatNo = bespeaklog.ShortSeatNum;
                    newEnterOutLog.SeatNo = bespeaklog.SeatNo;
                    newEnterOutLog.Remark = string.Format("在手机预约网站预约签到，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                    int logid = -1;
                    HandleResult result = seatManage.AddEnterOutLogInfo(newEnterOutLog, ref logid); //添加入座记录
                    if (result == HandleResult.Successed)
                    {
                        bespeaklog.BsepeakState = BookingStatus.Confinmed;
                        bespeaklog.CancelPerson = Operation.Reader;
                        bespeaklog.CancelTime = nowDate;
                        bespeaklog.Remark = string.Format("在手机预约网站预约签到，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                        if (seatManage.UpdateBespeakLogInfo(bespeaklog) > 0)
                        {
                            return "";
                        }
                        else
                        {
                            return "系统错误，签到失败";
                        }
                    }
                    else
                    {
                        return "系统错误，签到失败";
                    }
                }
                else if (nowDate.CompareTo(dtBegin) < 0)
                {
                    //TODO:预约时间过早，请在dtBegin 到dtEnd刷卡确认。
                    return "对不起，您预约的座位尚未到达签到时间，请在" + dtBegin.ToShortTimeString() + "到" + dtEnd.ToShortTimeString() + "间进行签到";
                }
                else if (nowDate.CompareTo(dtEnd) < 0)
                {
                    return "对不起，您预约已超时";
                }
                else
                {
                    return "系统错误，签到失败";
                }
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write(string.Format("预约签到失败：{0}", ex.Message));
                return "系统错误，签到失败";
            }
        }

        /// <summary>
        /// 获取座位使用状态
        /// </summary>
        /// <param name="seatNum"></param>
        /// <param name="readingRoomNum"></param>
        /// <returns></returns>
        public SeatBookUsingInfo GetSeatBookUsingStatus(string seatNum, string readingRoomNum)
        {
            SeatBookUsingInfo seatInfo = new SeatBookUsingInfo();
            List<ReadingRoomInfo> rooms = seatManage.GetReadingRoomInfo(new List<string>() { readingRoomNum });
            if (rooms.Count > 0)
            {
                seatInfo.InReadingRoom = rooms[0];
            }
            else
            {
                throw new SeatBespeakException.BespeakSeatFailed("此阅览室不存在！");
            }
            seatInfo.SeatInfo = seatManage.GetSeatInfoBySeatNum(seatNum);
            if (seatInfo.SeatInfo == null)
            {
                throw new SeatBespeakException.BespeakSeatFailed("此座位不存在！");
            }
            //获取可预约座位
            if (seatInfo.InReadingRoom.Setting.SeatBespeak.Used)
            {
                if (seatInfo.InReadingRoom.Setting.SeatBespeak.NowDayBespeak)
                {
                    //当天预约信息
                    SeatLayout bespeakSeatLayout = seatManage.GetBeseakSeatLayout(readingRoomNum, DateTime.Now);
                    SeatLayout useSeatLayout = seatManage.GetRoomSeatLayOut(readingRoomNum);
                    foreach (KeyValuePair<string, Seat> uSeat in useSeatLayout.Seats)
                    {
                        if (uSeat.Value.SeatUsedState == EnterOutLogType.BespeakWaiting)
                        {
                            if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                            {
                                if (!bespeakSeatLayout.Seats[uSeat.Key].IsSuspended)
                                {
                                    if (bespeakSeatLayout.Seats[uSeat.Key].SeatNo == seatNum)
                                    {
                                        seatInfo.BookSeatInfo.Add(DateTime.Now, bespeakSeatLayout.Seats[uSeat.Key]);
                                        break;
                                    }
                                }
                            }
                            continue;
                        }
                        if ((uSeat.Value.SeatUsedState == EnterOutLogType.Leave || uSeat.Value.SeatUsedState == EnterOutLogType.None) && !uSeat.Value.IsSuspended)
                        {
                            if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                            {
                                if (bespeakSeatLayout.Seats[uSeat.Key].SeatNo == seatNum)
                                {
                                    seatInfo.BookSeatInfo.Add(DateTime.Now, bespeakSeatLayout.Seats[uSeat.Key]);
                                    break;
                                }
                            }
                            else
                            {
                                if (uSeat.Value.SeatNo == seatNum)
                                {
                                    seatInfo.BookSeatInfo.Add(DateTime.Now, uSeat.Value);
                                    break;
                                }
                            }
                        }
                    }
                }
                //隔天预约信息
                for (int i = 1; i <= seatInfo.InReadingRoom.Setting.SeatBespeak.BespeakBeforeDays; i++)
                {
                    SeatLayout bespeakSeatLayout = seatManage.GetBeseakSeatLayout(readingRoomNum, DateTime.Now.AddDays(i));
                    foreach (Seat seat in bespeakSeatLayout.Seats.Values)
                    {
                        if (rooms[0].Setting.SeatBespeak.CanBookMultiSpan && rooms[0].Setting.SeatBespeak.SpecifiedTime)
                        {
                            if (seat.CanBeBespeak && seat.CanBespeakSpan.Count > 0 && !seat.IsSuspended)
                            {
                                if (seat.SeatNo == seatNum)
                                {
                                    seatInfo.BookSeatInfo.Add(DateTime.Now.AddDays(i), seat);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            if (seat.CanBeBespeak && seat.SeatUsedState != EnumType.EnterOutLogType.BookingConfirmation && !seat.IsSuspended)
                            {
                                if (seat.SeatNo == seatNum)
                                {
                                    seatInfo.BookSeatInfo.Add(DateTime.Now.AddDays(i), seat);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return seatInfo;
        }
    }
}
