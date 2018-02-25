using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.StatisticsService.Code
{
    public class RoomUsageStatistics
    {
        public void Run(ref bool isStatistics, DateTime runTime)
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            if (nowDateTime > DateTime.Parse(nowDateTime.ToShortDateString() + " " + runTime.ToShortTimeString()))
            {
                if (!isStatistics)
                {
                    return;
                }
                Statistics();
                isStatistics = false;
            }
            else
            {
                isStatistics = true;
            }

        }
        /// <summary>
        /// 开始计算
        /// </summary>
        private void Statistics()
        {
            try
            {
                List<ReadingRoomInfo> rooms = ClientConfigOperate.GetReadingRooms(null);
                DateTime sdt = SeatUsageDataOperating.GetLastRoomUsageStatisticsDate();
                if (sdt <= DateTime.Parse("2000-1-1"))
                {
                    return;
                }
                sdt = sdt.AddDays(1);
                while (true)
                {
                    //获取进出记录
                    List<EnterOutLogInfo> enterOutLogList = T_SM_EnterOutLog_bak.GetStatisticsLogsByDate(sdt);
                    List<BespeakLogInfo> bespeakLogList = T_SM_SeatBespeak.GetBespeakList(null, null, sdt, 0, null);
                    List<ViolationRecordsLogInfo> violationLogList = T_SM_ViolateDiscipline.GetViolationRecords(null, null, sdt.ToShortDateString(), sdt.Date.AddDays(1).AddSeconds(-1).ToString(), LogStatus.None, LogStatus.None);
                    //List<BlackListInfo> blacklistList = T_SM_Blacklist.GetAllBlackListInfo(null, LogStatus.None, sdt.ToShortDateString(), sdt.Date.AddDays(1).AddSeconds(-1).ToString());
                    if (enterOutLogList.Count <= 0 && bespeakLogList.Count <= 0 && violationLogList.Count <= 0 && sdt >= ServiceDateTime.Now.Date.AddDays(-1))
                    {
                        break;
                    }
                    Dictionary<string, SeatManage.ClassModel.RoomUsageStatistics> roomDir = rooms.ToDictionary(room => room.No, room => new SeatManage.ClassModel.RoomUsageStatistics());

                    //基本数据及排序处理
                    foreach (ReadingRoomInfo room in rooms)
                    {
                        roomDir[room.No].StatisticsDate = sdt;
                        roomDir[room.No].ReadingRoomNo = room.No;
                        roomDir[room.No].SeatAllCount = room.SeatList.Seats.Count;
                        roomDir[room.No].OpenTime = DateTime.Parse(room.Setting.GetRoomOpenTimeByDate(sdt).BeginTime);
                        roomDir[room.No].CloseTime = DateTime.Parse(room.Setting.GetRoomOpenTimeByDate(sdt).EndTime);
                        roomDir[room.No].RoomUsageTime = (int)(roomDir[room.No].CloseTime - roomDir[room.No].OpenTime).TotalMinutes;
                        roomDir[room.No].SeatUsageCount = enterOutLogList.FindAll(u => u.ReadingRoomNo == room.No).GroupBy(u => u.SeatNo).Count();
                        roomDir[room.No].UsedReaderCount = enterOutLogList.FindAll(u => u.ReadingRoomNo == room.No).GroupBy(u => u.CardNo).Count();
                        roomDir[room.No].CanBesapeakSeat = room.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage ? (int)room.Setting.SeatBespeak.BespeakArea.Scale * room.SeatList.Seats.Count : room.SeatList.Seats.Count(u => u.Value.CanBeBespeak);
                        roomDir[room.No].BespeakedSeat = bespeakLogList.FindAll(u => u.ReadingRoomNo == room.No).GroupBy(u => u.SeatNo).Count();
                    }
                    foreach (ViolationRecordsLogInfo vrl in violationLogList)
                    {
                        roomDir[vrl.ReadingRoomID].ViolationRecordCount++;
                        switch (vrl.EnterFlag)
                        {
                            case ViolationRecordsType.BookingTimeOut:
                                roomDir[vrl.ReadingRoomID].VRBookingTimeOut++;
                                break;
                            case ViolationRecordsType.LeaveByAdmin:
                                roomDir[vrl.ReadingRoomID].VRLeaveByAdmin++;
                                break;
                            case ViolationRecordsType.LeaveNotReadCard:
                                roomDir[vrl.ReadingRoomID].VRLeaveNotReadCard++;
                                break;
                            case ViolationRecordsType.SeatOutTime:
                                roomDir[vrl.ReadingRoomID].VRSeatOutTime++;
                                break;
                            case ViolationRecordsType.ShortLeaveByAdminOutTime:
                                roomDir[vrl.ReadingRoomID].VRShortLeaveByAdminOutTime++;
                                break;
                            case ViolationRecordsType.ShortLeaveByReaderOutTime:
                                roomDir[vrl.ReadingRoomID].VRShortLeaveByReaderOutTime++;
                                break;
                            case ViolationRecordsType.ShortLeaveByServiceOutTime:
                                roomDir[vrl.ReadingRoomID].VRShortLeaveByServiceOutTime++;
                                break;
                            case ViolationRecordsType.ShortLeaveOutTime:
                                roomDir[vrl.ReadingRoomID].VRShortLeaveOutTime++;
                                break;
                        }
                    }
                    //预约记录处理
                    foreach (BespeakLogInfo bli in bespeakLogList)
                    {
                        roomDir[bli.ReadingRoomNo].AllBespeakCount++;
                        if (bli.BsepeakTime.Date == bli.SubmitTime.Date)
                        {
                            switch (bli.BsepeakState)
                            {
                                case BookingStatus.Cencaled:
                                    switch (bli.CancelPerson)
                                    {
                                        case Operation.Admin:
                                        case Operation.Reader:
                                            roomDir[bli.ReadingRoomNo].NowDayBespeakCancel++;
                                            break;
                                        case Operation.Service:
                                            roomDir[bli.ReadingRoomNo].NowDayBespeakOverTime++;
                                            break;
                                    }
                                    break;
                                case BookingStatus.Confinmed:
                                    roomDir[bli.ReadingRoomNo].NowDayBespeakCheck++;
                                    break;
                            }
                            roomDir[bli.ReadingRoomNo].NowDayBespeakCount++;
                        }
                        else
                        {
                            switch (bli.BsepeakState)
                            {
                                case BookingStatus.Cencaled:
                                    switch (bli.CancelPerson)
                                    {
                                        case Operation.Admin:
                                        case Operation.Reader:
                                            roomDir[bli.ReadingRoomNo].BespeakCancel++;
                                            break;
                                        case Operation.Service:
                                            roomDir[bli.ReadingRoomNo].BespeakOverTime++;
                                            break;
                                    }
                                    break;
                                case BookingStatus.Confinmed:
                                    roomDir[bli.ReadingRoomNo].BespeakCheck++;
                                    break;
                            }
                            roomDir[bli.ReadingRoomNo].BespeakCount++;
                        }
                    }
                    foreach (EnterOutLogInfo eol in enterOutLogList)
                    {
                        //刷卡次数
                        if (!string.IsNullOrEmpty(eol.TerminalNum) && !(eol.EnterOutState == EnterOutLogType.ContinuedTime && eol.Flag == Operation.Service))
                        {
                            roomDir[eol.ReadingRoomNo].RushCardOperatingCount++;
                        }
                        //记录类型
                        switch (eol.EnterOutState)
                        {
                            case EnterOutLogType.BookingConfirmation:
                                roomDir[eol.ReadingRoomNo].CheckBespeakCount++;
                                if (string.IsNullOrEmpty(eol.TerminalNum))
                                {
                                    roomDir[eol.ReadingRoomNo].CkeckBespeakInOtherClient++;
                                }
                                else
                                {
                                    roomDir[eol.ReadingRoomNo].CheckBespeakInSeatClient++;
                                }
                                break;
                            case EnterOutLogType.ComeBack:
                                roomDir[eol.ReadingRoomNo].ComeBackCount++;
                                switch (eol.Flag)
                                {
                                    case Operation.Admin:
                                        roomDir[eol.ReadingRoomNo].ComeBackByAdmin++;
                                        break;
                                    case Operation.OtherReader:
                                        roomDir[eol.ReadingRoomNo].ComeBackByOtherReader++;
                                        break;
                                    case Operation.Reader:
                                        roomDir[eol.ReadingRoomNo].ComeBackByReader++;
                                        if (string.IsNullOrEmpty(eol.TerminalNum))
                                        {
                                            roomDir[eol.ReadingRoomNo].ComeBackInOtherClient++;
                                        }
                                        else
                                        {
                                            roomDir[eol.ReadingRoomNo].ComeBackInSeatClient++;
                                        }
                                        break;
                                }
                                EnterOutLogInfo slEOL = enterOutLogList.FindLast(u => u.EnterOutState == EnterOutLogType.ShortLeave && u.EnterOutLogNo == eol.EnterOutLogNo && u.EnterOutTime < eol.EnterOutTime);
                                if (slEOL != null)
                                {
                                    roomDir[eol.ReadingRoomNo].ShortLeaveTime += (int)(slEOL.EnterOutTime - eol.EnterOutTime).TotalMinutes;
                                }
                                break;
                            case EnterOutLogType.ContinuedTime:
                                roomDir[eol.ReadingRoomNo].ContinueTimeCount++;
                                switch (eol.Flag)
                                {
                                    case Operation.Service:
                                        roomDir[eol.ReadingRoomNo].ContinueTimeByService++;
                                        break;
                                    case Operation.Reader:
                                        roomDir[eol.ReadingRoomNo].ContinueTimeByReader++;
                                        if (string.IsNullOrEmpty(eol.TerminalNum))
                                        {
                                            roomDir[eol.ReadingRoomNo].ContinueTimeInOtherClient++;
                                        }
                                        else
                                        {
                                            roomDir[eol.ReadingRoomNo].ContinueTimeInSeatClient++;
                                        }
                                        break;
                                }
                                break;
                            case EnterOutLogType.Leave:
                                roomDir[eol.ReadingRoomNo].LeaveCount++;
                                roomDir[eol.ReadingRoomNo].ReaderUsageCount++;
                                switch (eol.Flag)
                                {
                                    case Operation.Service:
                                        roomDir[eol.ReadingRoomNo].LeaveByService++;
                                        break;
                                    case Operation.Admin:
                                        roomDir[eol.ReadingRoomNo].LeaveByAdmin++;
                                        break;
                                    case Operation.Reader:
                                        roomDir[eol.ReadingRoomNo].LeaveByReader++;
                                        if (string.IsNullOrEmpty(eol.TerminalNum))
                                        {
                                            roomDir[eol.ReadingRoomNo].LeaveInOtherClient++;
                                        }
                                        else
                                        {
                                            roomDir[eol.ReadingRoomNo].LeaveInSeatClient++;
                                        }
                                        break;
                                }
                                EnterOutLogInfo enterEOL = enterOutLogList.Find(u => (u.EnterOutState == EnterOutLogType.BookingConfirmation || u.EnterOutState == EnterOutLogType.ReselectSeat || u.EnterOutState == EnterOutLogType.SelectSeat || u.EnterOutState == EnterOutLogType.WaitingSuccess) && u.EnterOutLogNo == eol.EnterOutLogNo);
                                if (enterEOL != null)
                                {
                                    roomDir[eol.ReadingRoomNo].SeatUsageTime += (int)(eol.EnterOutTime - enterEOL.EnterOutTime).TotalMinutes;
                                }
                                break;
                            case EnterOutLogType.ReselectSeat:
                                roomDir[eol.ReadingRoomNo].ReselectSeatCount++;
                                if (string.IsNullOrEmpty(eol.TerminalNum))
                                {
                                    roomDir[eol.ReadingRoomNo].ReselectSeatInOtherClient++;
                                }
                                else
                                {
                                    roomDir[eol.ReadingRoomNo].ReselectSeatInSeatClient++;
                                }
                                break;
                            case EnterOutLogType.SelectSeat:
                                roomDir[eol.ReadingRoomNo].SelectSeatCount++;
                                switch (eol.Flag)
                                {
                                    case Operation.Admin:
                                        roomDir[eol.ReadingRoomNo].SelectSeatByAdmin++;
                                        break;
                                    case Operation.Reader:
                                        roomDir[eol.ReadingRoomNo].SelectSeatByReader++;
                                        if (string.IsNullOrEmpty(eol.TerminalNum))
                                        {
                                            roomDir[eol.ReadingRoomNo].SelectSeatInOtherClient++;
                                        }
                                        else
                                        {
                                            roomDir[eol.ReadingRoomNo].SelectSeatInSeatClient++;
                                        }
                                        break;
                                }
                                break;
                            case EnterOutLogType.ShortLeave:
                                roomDir[eol.ReadingRoomNo].ShortLeaveCount++;
                                switch (eol.Flag)
                                {
                                    case Operation.OtherReader:
                                        roomDir[eol.ReadingRoomNo].ShortLeaveByOtherReader++;
                                        break;
                                    case Operation.Service:
                                        roomDir[eol.ReadingRoomNo].ShortLeaveByService++;
                                        break;
                                    case Operation.Admin:
                                        roomDir[eol.ReadingRoomNo].ShortLeaveByAdmin++;
                                        break;
                                    case Operation.Reader:
                                        roomDir[eol.ReadingRoomNo].ShortLeaveByReader++;
                                        if (string.IsNullOrEmpty(eol.TerminalNum))
                                        {
                                            roomDir[eol.ReadingRoomNo].ShortLeaveInOtherClient++;
                                        }
                                        else
                                        {
                                            roomDir[eol.ReadingRoomNo].ShortLeaveInSeatClient++;
                                        }
                                        break;
                                }
                                break;
                            case EnterOutLogType.WaitingSuccess:
                                roomDir[eol.ReadingRoomNo].WaitSeatCount++;
                                break;
                        }
                    }
                    foreach (SeatManage.ClassModel.RoomUsageStatistics roomUS in roomDir.Values.Where(roomUS => !SeatUsageDataOperating.AddRoomUsageStatistics(roomUS)))
                    {
                        WriteLog.Write(string.Format("数据统计服务：添加阅览室:{0} {1} 数据统计出错", roomUS.ReadingRoomNo, roomUS.StatisticsDate));
                        throw new Exception(string.Format("数据统计服务：添加阅览室:{0} {1} 数据统计出错", roomUS.ReadingRoomNo, roomUS.StatisticsDate));
                    }
                    sdt = sdt.AddDays(1);
                    if (sdt >= ServiceDateTime.Now.Date)
                    {
                        break;
                    }
                    roomDir = null;
                }
                WriteLog.Write("数据统计服务：统计阅览室完成使用情况完成");
            }

            catch (Exception ex)
            {
                WriteLog.Write(string.Format("数据统计服务：统计阅览室使用情况失败：{0}", ex.Message));
            }
        }
    }
}
