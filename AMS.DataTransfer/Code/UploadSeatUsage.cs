using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AMS.DataTransfer.Code
{
    public class UploadSeatUsage
    {
        public static bool GetUsage()
        {
            try
            {
                //读者的列表
                Dictionary<string, int> ReaderDic = new Dictionary<string, int>();
                //座位数目
                int SeatCount = 0;
                //统计时间
                DateTime usageDT = AMS.ServiceProxy.SeatUsageOperationService.LastSeatUsageUploadDate(ServiceSet.SchoolNums);
                //阅览室列表
                List<SeatManage.ClassModel.ReadingRoomInfo> roomList = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
                if (roomList.Count < 1)
                {
                    return false;
                }
                foreach (SeatManage.ClassModel.ReadingRoomInfo room in roomList)
                {
                    SeatCount += room.SeatList.Seats.Count;
                }
                if (usageDT.Date <= DateTime.Parse("2000-1-1"))
                {
                    usageDT = SeatManage.Bll.T_SM_EnterOutLog_bak.GetFristLogDate().Date;
                    if (usageDT.Date <= DateTime.Parse("2000-1-1"))
                    {
                        return false;
                    }
                }
                else
                {
                    usageDT = usageDT.AddDays(1);
                }
                List<SeatManage.ClassModel.TerminalInfoV2> terList = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
                while (true)
                {
                    AMS.Model.SMS_SeatUsage usage = new Model.SMS_SeatUsage();
                    usage.SchoolNum = ServiceSet.SchoolNums;
                    usage.SeatCount = SeatCount;
                    usage.UploadDate = usageDT.Date;
                    foreach (SeatManage.ClassModel.TerminalInfoV2 ter in terList)
                    {
                        usage.DeviceUsage.Add(ter.ClientNo, new Model.DeviceUsageInfo() { DeviceNum = ter.ClientNo, DeviceName = ter.Describe });
                    }
                    //获取进出记录
                    List<SeatManage.ClassModel.EnterOutLogInfo> eolList = SeatManage.Bll.T_SM_EnterOutLog_bak.GetEnterOutLogs(null, null, null, usageDT.Date, usageDT.AddDays(1).Date);
                    if (eolList.Count < 1)
                    {
                        if (usageDT >= DateTime.Now)
                        {
                            break;
                        }
                        else
                        {
                            usageDT = usageDT.AddDays(1);
                            continue;
                        }
                    }
                    foreach (SeatManage.ClassModel.EnterOutLogInfo log in eolList)
                    {
                        //增加使用人数
                        if (string.IsNullOrEmpty(log.CardNo))
                        {
                            continue;
                        }
                        if (ReaderDic.ContainsKey(log.CardNo))
                        {
                            ReaderDic[log.CardNo]++;
                        }
                        else
                        {
                            ReaderDic.Add(log.CardNo, 1);
                        }
                        //判断记录类型
                        switch (log.EnterOutState)
                        {
                            case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                                {
                                    usage.SeatUeage.EnterOutVisitors++;
                                    usage.SeatUeage.BookingConfirmCount++;
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                                {
                                    usage.SeatUeage.ContniueTimeCount++;
                                    switch (log.Flag)
                                    {
                                        case SeatManage.EnumType.Operation.Admin:
                                            usage.SeatUeage.ContniueTimeCountByAdmin++;
                                            break;
                                        case SeatManage.EnumType.Operation.Service:
                                            usage.SeatUeage.ContniueTimeCountByService++;
                                            break;
                                        case SeatManage.EnumType.Operation.Reader:
                                            usage.SeatUeage.ContniueTimeCountByUser++;
                                            break;
                                        default: break;
                                    }
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.Leave:
                                {
                                    switch (log.Flag)
                                    {
                                        case SeatManage.EnumType.Operation.Admin:
                                            usage.SeatUeage.LeaveCountByAdmin++;
                                            break;
                                        case SeatManage.EnumType.Operation.Service:
                                            usage.SeatUeage.LeaveCountByService++;
                                            break;
                                        case SeatManage.EnumType.Operation.Reader:
                                            usage.SeatUeage.LeaveCountByUser++;
                                            break;
                                        default: break;
                                    }
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                                {
                                    usage.SeatUeage.EnterOutVisitors++;
                                    usage.SeatUeage.ReselectSeatCount++;
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                                {
                                    usage.SeatUeage.EnterOutVisitors++;
                                    switch (log.Flag)
                                    {
                                        case SeatManage.EnumType.Operation.Admin:
                                            usage.SeatUeage.SelectSeatCountByAdmin++;
                                            break;
                                        case SeatManage.EnumType.Operation.Reader:
                                            usage.SeatUeage.SelectSeatCount++;
                                            break;
                                        default: break;
                                    }
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                                {
                                    usage.SeatUeage.ShortLeaveCount++;
                                    switch (log.Flag)
                                    {
                                        case SeatManage.EnumType.Operation.Admin:
                                            usage.SeatUeage.ShortLeaveCountByAdmin++;
                                            break;
                                        case SeatManage.EnumType.Operation.Service:
                                            usage.SeatUeage.ShortLeaveCountByService++;
                                            break;
                                        case SeatManage.EnumType.Operation.Reader:
                                            usage.SeatUeage.ShortLeaveCountByUser++;
                                            break;
                                        case SeatManage.EnumType.Operation.OtherReader:
                                            usage.SeatUeage.ShortLeaveCountByReader++;
                                            break;
                                        default: break;
                                    }
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                                {
                                    usage.SeatUeage.EnterOutVisitors++;
                                    usage.SeatUeage.WaitSeatCount++;
                                }
                                break;
                            default: break;
                        }
                        if (!string.IsNullOrEmpty(log.TerminalNum) && log.Flag == SeatManage.EnumType.Operation.Reader)
                        {
                            if (usage.DeviceUsage.ContainsKey(log.TerminalNum))
                            {
                                usage.DeviceUsage[log.TerminalNum].RushCardCount++;
                            }
                            else
                            {
                                usage.DeviceUsage.Add(log.TerminalNum, new AMS.Model.DeviceUsageInfo() { DeviceNum = log.TerminalNum, RushCardCount = 1, DeviceName = "未知终端" });
                            }
                            usage.RushCardCount++;
                            switch (log.EnterOutState)
                            {
                                case SeatManage.EnumType.EnterOutLogType.BookingConfirmation: usage.DeviceUsage[log.TerminalNum].BookingConfirmCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.ComeBack: usage.DeviceUsage[log.TerminalNum].ComeBackCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.ContinuedTime: usage.DeviceUsage[log.TerminalNum].ContniueTimeCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.Leave: usage.DeviceUsage[log.TerminalNum].LeaveCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.ReselectSeat: usage.DeviceUsage[log.TerminalNum].SelectSeatCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.SelectSeat: usage.DeviceUsage[log.TerminalNum].SelectSeatCount++; break;
                                case SeatManage.EnumType.EnterOutLogType.ShortLeave: usage.DeviceUsage[log.TerminalNum].ShortLeaveCount++; break;
                                default: break;
                            }
                        }
                    }
                    //预约统计
                    List<SeatManage.ClassModel.BespeakLogInfo> booklogList = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakList(null, null, usageDT.Date, 0, new List<SeatManage.EnumType.BookingStatus>() { SeatManage.EnumType.BookingStatus.Cencaled });
                    foreach (SeatManage.ClassModel.BespeakLogInfo book in booklogList)
                    {
                        switch (book.CancelPerson)
                        {
                            case SeatManage.EnumType.Operation.Admin:
                                usage.SeatUeage.BookingCancelCount++;
                                break;
                            case SeatManage.EnumType.Operation.Service:
                                usage.SeatUeage.BookingOverTimeCount++;
                                break;
                            case SeatManage.EnumType.Operation.Reader:
                                usage.SeatUeage.BookingCancelCount++;
                                break;
                            default: break;
                        }
                    }
                    usage.SeatUeage.BookingCount = usage.SeatUeage.BookingCancelCount + usage.SeatUeage.BookingConfirmCount + usage.SeatUeage.BookingOverTimeCount;
                    //黑名单
                    List<SeatManage.ClassModel.BlackListInfo> blackList = SeatManage.Bll.T_SM_Blacklist.GetAllBlackListInfo(null, SeatManage.EnumType.LogStatus.None, usageDT.ToShortDateString(), usageDT.AddDays(1).ToShortDateString());
                    usage.BlackListRecords.BlackListCount = blackList.Count;
                    List<SeatManage.ClassModel.ViolationRecordsLogInfo> vrList = SeatManage.Bll.T_SM_ViolateDiscipline.GetViolationRecords(null, null, usageDT.ToShortDateString(), usageDT.AddDays(1).ToShortDateString(), SeatManage.EnumType.LogStatus.None, SeatManage.EnumType.LogStatus.None);
                    foreach (SeatManage.ClassModel.ViolationRecordsLogInfo vr in vrList)
                    {
                        usage.BlackListRecords.ViolationRecords[vr.EnterFlag].Count++;
                        usage.BlackListRecords.ViolationRecordsCount++;
                    }
                    usage.UserCount = ReaderDic.Count;
                    if (AMS.ServiceProxy.SeatUsageOperationService.AddNewSeatUsage(usage))
                    {
                        usageDT = usageDT.AddDays(1);
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}
