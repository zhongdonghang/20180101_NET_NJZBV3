using System;
using System.Collections.Generic;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatManage.AppServiceHelper
{
    public partial class AppServiceHelper : IAppServiceHelper
    {
        /// <summary>
        /// 获取读者常坐座位
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="seatCount">查询座位数量</param>
        /// <param name="dayCount">统计天数</param>
        /// <returns></returns>
        public string GetOftenSeat(string studentNo, int seatCount, int dayCount)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<Seat> seats = SeatManageDateService.GetOftenUsedSeatByCardNo(studentNo, dayCount, null);
                List<AJM_Seat> ajmSeats = new List<AJM_Seat>();
                for (int i = 0; i < (seats.Count < seatCount ? seats.Count : seatCount); i++)
                {
                    AJM_Seat ajmSeat = new AJM_Seat();
                    ajmSeat.SeatNo = seats[i].SeatNo;
                    ajmSeat.SeatShortNo = seats[i].ShortSeatNo;
                    ajmSeat.RoomName = seats[i].ReadingRoom.Name;
                    ajmSeat.RoomNo = seats[i].ReadingRoomNum;
                    ajmSeats.Add(ajmSeat);
                }
                if (ajmSeats.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "没有查询到常坐座位信息！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmSeats);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取常坐座位遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取常坐座位执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取随机座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <returns></returns>
        public string GetRandomSeat(string roomNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                AJM_Seat ajmSeat = new AJM_Seat();
                ajmSeat.SeatNo = SeatManageDateService.RandomAllotSeat(roomNo);
                if (string.IsNullOrEmpty(ajmSeat.SeatNo))
                {
                    result.Result = false;
                    result.Msg = "阅览室已满，请稍后再试！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmSeat);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取随机座位遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取随机座位执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取座位的可预约信息
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public string GetSeatBespeakInfo(string seatNo, string roomNo, string bespeakTime)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(roomNo) || string.IsNullOrEmpty(bespeakTime))
                {
                    result.Result = false;
                    result.Msg = "阅览室编号或预约时间不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                //如果预约日期是当天，获取当天可用座位（排除已被预约座位）
                DateTime bespeakDate;
                if (!DateTime.TryParse(bespeakTime, out bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "日期格式不正确！";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date < DateTime.Now.Date)
                {
                    result.Result = false;
                    result.Msg = "查询日期不能早于当天日期！";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> readingRoomInfos = SeatManageDateService.GetReadingRoomInfo(new List<string> { roomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "阅览室不存在！";
                    return JSONSerializer.Serialize(result);
                }
                if (!readingRoomInfos[0].SeatList.Seats.ContainsKey(seatNo))
                {
                    result.Result = false;
                    result.Msg = "该座位不存在!";
                    return JSONSerializer.Serialize(result);
                }
                if (readingRoomInfos[0].SeatList.Seats[seatNo].IsSuspended)
                {
                    result.Result = false;
                    result.Msg = "该座位暂停使用!";
                    return JSONSerializer.Serialize(result);
                }
                if (!readingRoomInfos[0].Setting.SeatBespeak.Used)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不提供预约！";
                    return JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date - DateTime.Now.Date).Days > readingRoomInfos[0].Setting.SeatBespeak.BespeakBeforeDays)
                {
                    result.Result = false;
                    result.Msg = "您选择的日期尚未开放预约！";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && !readingRoomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室未开放当天预约！";
                    return JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date == DateTime.Now.Date) && readingRoomInfos[0].Setting.ReadingRoomOpenState(bespeakDate.Date) != EnumType.ReadingRoomStatus.Close)
                {
                    result.Result = false;
                    result.Msg = "对不起当前预约时间段阅览室未开放!";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date != DateTime.Now.Date && !readingRoomInfos[0].Setting.IsCanBespeakSeat(bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "当前时间暂未开放预约！";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date)
                {
                    Seat seat = SeatManageDateService.GetSeatInfoBySeatNum(seatNo);
                    if (seat == null)
                    {
                        result.Result = false;
                        result.Msg = "对不起，获取座位信息失败！";
                        return JSONSerializer.Serialize(result);
                    }
                    if (seat.SeatUsedState != EnterOutLogType.Leave)
                    {
                        result.Result = false;
                        result.Msg = "对不起此座位已被其他人使用！";
                        return JSONSerializer.Serialize(result);
                    }
                }
                if (SeatManageDateService.GetBespeakLogInfoBySeatNo(seatNo, bespeakDate.Date).Count > 0)
                {
                    result.Result = false;
                    result.Msg = "对不起此座位已被其他人预约！";
                    return JSONSerializer.Serialize(result);
                }

                AJM_SeatBespeakInfo ajmseat = new AJM_SeatBespeakInfo();
                ajmseat.SeatNo = seatNo;
                ajmseat.SeatShortNo = readingRoomInfos[0].SeatList.Seats[seatNo].ShortSeatNo;
                ajmseat.RoomNo = readingRoomInfos[0].No;
                ajmseat.RoomName = readingRoomInfos[0].Name;
                ajmseat.BespeakDate = bespeakDate.ToShortDateString();
                ajmseat.IsCanSelectTime = readingRoomInfos[0].Setting.SeatBespeak.SpecifiedBespeak;
                ajmseat.IsUsedSpan = readingRoomInfos[0].Setting.SeatBespeak.SpecifiedTime;
                ajmseat.CheckKeepTime = (int)readingRoomInfos[0].Setting.SeatBespeak.SeatKeepTime;
                ajmseat.CheckBeforeTime = int.Parse(readingRoomInfos[0].Setting.SeatBespeak.ConfirmTime.BeginTime);
                ajmseat.CheckLastTime = int.Parse(readingRoomInfos[0].Setting.SeatBespeak.ConfirmTime.EndTime);

                DateTime bookstartTime = readingRoomInfos[0].Setting.DateOpenTime(bespeakDate);
                if (bookstartTime > DateTime.Now)
                {
                    ajmseat.IsCanNowBook = false;
                    ajmseat.IsCanSelectTime = true;
                }

                if (readingRoomInfos[0].Setting.SeatBespeak.SpecifiedBespeak)
                {
                    List<DateTime> timeSpans = readingRoomInfos[0].Setting.GetSelectTimeList(bespeakDate);
                    foreach (var time in timeSpans)
                    {
                        ajmseat.TimeList.Add(time.ToShortTimeString());
                    }
                }
                else
                {
                    ajmseat.TimeList.Add(readingRoomInfos[0].Setting.DateOpenTime(bespeakDate).ToShortTimeString());
                }


                //if (ajmseat.IsCanSelectTime)
                //{
                //    List<DateTime> timeSpans = readingRoomInfos[0].Setting.GetSelectTimeList(besppeakDate);
                //    foreach (var time in timeSpans)
                //    {
                //        ajmseat.TimeList.Add(time.ToShortTimeString());
                //    }
                //}
                //else
                //{
                //    ajmseat.TimeList.Add(readingRoomInfos[0].Setting.DateOpenTime(besppeakDate).ToShortTimeString());
                //}

                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmseat);
                return JSONSerializer.Serialize(result);

            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取当前可预约座位列表遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取当前阅览室可预约座位执行越到异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取座位信息以及操作
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <param name="isMessager"></param>
        /// <returns></returns>
        public string GetSeatNowStatus(string seatNo, string roomNo, string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            AJM_SeatNowStatus seatStatus = new AJM_SeatNowStatus();
            try
            {
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> readingRoomInfos = SeatManageDateService.GetReadingRoomInfo(new List<string> { roomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "阅览室不存在！";
                    return JSONSerializer.Serialize(result);
                }
                seatStatus.RoomNo = readingRoomInfos[0].No;
                seatStatus.RoomName = readingRoomInfos[0].Name;
                if (!readingRoomInfos[0].SeatList.Seats.ContainsKey(seatNo))
                {
                    result.Result = false;
                    result.Msg = "该座位不存在!";
                    return JSONSerializer.Serialize(result);
                }
                seatStatus.SeatNo = readingRoomInfos[0].SeatList.Seats[seatNo].SeatNo;
                seatStatus.SeatShortNo = readingRoomInfos[0].SeatList.Seats[seatNo].ShortSeatNo;
                Seat seat = SeatManageDateService.GetSeatInfoBySeatNum(seatNo);
                ReaderInfo readerInfo = SeatManageDateService.GetReader(studentNo, true);
                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "未查询到该读者的当前状态";
                    return JSONSerializer.Serialize(result);
                }
                bool isSelfSeat = readerInfo.EnterOutLog != null && readerInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave && seat.SeatNo == readerInfo.EnterOutLog.SeatNo;
                bool isOnSeat = (readerInfo.EnterOutLog != null && readerInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave) ;
                if (readingRoomInfos[0].SeatList.Seats[seatNo].IsSuspended)
                {
                    seatStatus.Status = "StopUsed";
                }
                else
                {
                    switch (seat.SeatUsedState)
                    {
                        case EnterOutLogType.ComeBack:
                        case EnterOutLogType.ContinuedTime:
                        case EnterOutLogType.ReselectSeat:
                        case EnterOutLogType.SelectSeat:
                        case EnterOutLogType.WaitingSuccess:
                        case EnterOutLogType.BookingConfirmation:
                            seatStatus.Status = ReaderStatus.Seating.ToString();
                            seatStatus.CanOperation = isSelfSeat ? "Leave;ShortLeave" : "";
                            break;
                        case EnterOutLogType.ShortLeave:
                            seatStatus.Status = ReaderStatus.Seating.ToString();
                            seatStatus.CanOperation = isSelfSeat ? "Leave" : "";
                            break;
                        case EnterOutLogType.Leave:
                            seatStatus.Status = ReaderStatus.Leave.ToString();
                            seatStatus.CanOperation = !isSelfSeat && isOnSeat ? "ChangeSeat" : "";
                            break;
                    }
                    if (readerInfo.WaitSeatLog != null && readerInfo.WaitSeatLog.SeatNo == seat.SeatNo)
                    {
                        seatStatus.Status = ReaderStatus.Waiting.ToString();
                        seatStatus.CanOperation = "CancelWait";
                    }
                    if (readerInfo.BespeakLog.Count > 0 && readerInfo.BespeakLog[0].BsepeakTime.Date == DateTime.Now.Date && readerInfo.BespeakLog[0].SeatNo == seat.SeatNo)
                    {
                        seatStatus.Status = ReaderStatus.Booking.ToString();
                        seatStatus.CanOperation = "CancelBook";
                    }
                    if (readingRoomInfos[0].Setting.SeatBespeak.Used)
                    {
                        if (readingRoomInfos[0].Setting.SeatBespeak.NowDayBespeak && seat.SeatUsedState == EnterOutLogType.Leave && SeatManageDateService.GetBespeakLogInfoBySeatNo(seatNo, DateTime.Now).Count < 1)
                        {
                            seatStatus.CanBookingDate.Add(DateTime.Now.ToShortDateString());
                        }
                        for (int i = 1; i <= readingRoomInfos[0].Setting.SeatBespeak.BespeakBeforeDays; i++)
                        {
                            if (SeatManageDateService.GetBespeakLogInfoBySeatNo(seatNo, DateTime.Now.AddDays(i)).Count < 1)
                            {
                                seatStatus.CanBookingDate.Add(DateTime.Now.AddDays(i).ToShortDateString());
                            }
                        }

                    }

                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(seatStatus);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取读者当前状态发生异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取用户状态执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取座位信息以及操作
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <param name="isMessager"></param>
        /// <returns></returns>
        public string GetMessageSeatStatus(string seatNo, string roomNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            AJM_SeatMessageStatus seatStatus = new AJM_SeatMessageStatus();
            try
            {
                List<ReadingRoomInfo> readingRoomInfos = SeatManageDateService.GetReadingRoomInfo(new List<string> { roomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "阅览室不存在！";
                    return JSONSerializer.Serialize(result);
                }
                seatStatus.RoomNo = readingRoomInfos[0].No;
                seatStatus.RoomName = readingRoomInfos[0].Name;
                if (!readingRoomInfos[0].SeatList.Seats.ContainsKey(seatNo))
                {
                    result.Result = false;
                    result.Msg = "该座位不存在!";
                    return JSONSerializer.Serialize(result);
                }
                if (readingRoomInfos[0].SeatList.Seats[seatNo].IsSuspended)
                {
                    seatStatus.Status = "StopUsed";
                }
                else
                {
                    Seat seat = SeatManageDateService.GetSeatInfoBySeatNum(seatNo);
                    seatStatus.SeatNo = seat.SeatNo;
                    seatStatus.SeatShortNo = seat.ShortSeatNo;
                    seatStatus.Name = seat.UserName;
                    seatStatus.StuedntNo = seat.UserCardNo;
                    seatStatus.OperationTime = seat.BeginUsedTime.ToString("yyyy-MM-dd HH:mm:ss");
                    switch (seat.SeatUsedState)
                    {
                        case EnterOutLogType.ComeBack:
                        case EnterOutLogType.ContinuedTime:
                        case EnterOutLogType.ReselectSeat:
                        case EnterOutLogType.SelectSeat:
                        case EnterOutLogType.WaitingSuccess:
                            seatStatus.Status = ReaderStatus.Seating.ToString();
                            seatStatus.CanOperation = "Leave;ShortLeave;Blacklist";
                            break;
                        case EnterOutLogType.ShortLeave:
                            seatStatus.Status = ReaderStatus.Seating.ToString();
                            seatStatus.CanOperation = "Leave;Blacklist";
                            break;
                        case EnterOutLogType.Leave:
                            seatStatus.Status = ReaderStatus.Leave.ToString();
                            seatStatus.CanOperation = "GiveSeat";
                            break;
                        case EnterOutLogType.BespeakWaiting:
                            seatStatus.Status = ReaderStatus.Booking.ToString();
                            seatStatus.CanOperation = "CancelBook";
                            break;
                    }
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(seatStatus);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取读者当前状态发生异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取用户状态执行异常！";
                return JSONSerializer.Serialize(result);
            }
        }
    }
}
