using System;
using System.Collections.Generic;
using System.Linq;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IWeChatWcfService;
using SeatManage.SeatManageComm;
using WcfServiceForSeatManage;

namespace SeatManage.WeChatWcfService
{
    public partial class WeChatService : IWeChatService
    {
        /// <summary>
        /// 根据阅览室编号获取阅览室布局
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public string GetRoomSeatLayout(string roomNum)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(roomNum))
                {
                    result.Result = false;
                    result.Msg = "阅览室编号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                AJM_SeatLayout ajmSeatLayout = new AJM_SeatLayout();
                List<ReadingRoomInfo> roomInfos = seatManageDateService.GetReadingRoomInfo(new List<string> { roomNum });
                if (roomInfos.Count < 0)
                {
                    result.Result = false;
                    result.Msg = string.Format("编号为{0}的阅览室不存在！", roomNum);
                    return JSONSerializer.Serialize(result);
                }
                SeatLayout seatLayout = roomInfos[0].SeatList;

                ajmSeatLayout.ColumsCount = seatLayout.SeatCol;
                ajmSeatLayout.RowsCount = seatLayout.SeatRow;
                ajmSeatLayout.IsUpdate = false;
                foreach (var seat in seatLayout.Seats.Values)
                {
                    AJM_Element ajmElement = new AJM_Element();
                    ajmElement.Angle = seat.RotationAngle;
                    ajmElement.BaseHeight = seat.BaseHeight;
                    ajmElement.BaseWidth = seat.BaseWidth;
                    ajmElement.SeatNo = seat.SeatNo;
                    ajmElement.ElementType = "Seat";
                    ajmElement.HasPower = seat.HavePower;
                    ajmElement.X = seat.PositionX;
                    ajmElement.Y = seat.PositionY;
                    ajmElement.Remark = seat.ShortSeatNo;
                    ajmSeatLayout.ElementList.Add(ajmElement);
                }
                foreach (AJM_Element ajmElement in seatLayout.Notes.Select(note => new AJM_Element
                {
                    Angle = note.RotationAngle,
                    BaseHeight = note.BaseHeight,
                    BaseWidth = note.BaseWidth,
                    ElementType = note.Type.ToString(),
                    X = note.PositionX,
                    Y = note.PositionY,
                    Remark = note.Remark
                }))
                {
                    ajmSeatLayout.ElementList.Add(ajmElement);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmSeatLayout);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室布局图遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取阅览室布局执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取全部阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomInfo()
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<AJM_ReadingRoom> ajmReadingRooms = new List<AJM_ReadingRoom>();
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(null);
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "没有查询到阅览室信息，请查看阅览室是否存在！";
                    return JSONSerializer.Serialize(result);
                }
                for (int i = 0; i < readingRoomInfos.Count; i++)
                {
                    AJM_ReadingRoom ajmReadingRoom = new AJM_ReadingRoom();
                    ajmReadingRoom.RoomNo = readingRoomInfos[i].No;
                    ajmReadingRoom.RoomName = readingRoomInfos[i].Name;
                    ajmReadingRoom.LibraryName = readingRoomInfos[i].Libaray.Name;
                    ajmReadingRooms.Add(ajmReadingRoom);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReadingRooms);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取所有阅览室的基础信息遇到异常{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取阅览室基础信息";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取全部阅览室的当前的使用状态（在座人数，是否开馆等）
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomNowState()
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                //获取阅览室座位使用状态
                Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsedStates = seatManageDateService.GetRoomSeatUsedStateV5(null);
                List<AJM_ReadingRoomState> ajmReadingRoomStates = new List<AJM_ReadingRoomState>();
                foreach (ReadingRoomSeatUsedState seatUsedState in roomSeatUsedStates.Values)
                {
                    AJM_ReadingRoomState ajmReadingRoomState = new AJM_ReadingRoomState();
                    ajmReadingRoomState.RoomName = seatUsedState.RoomName;
                    ajmReadingRoomState.RoomNo = seatUsedState.RoomNum;
                    List<string> listNum = new List<string>();
                    listNum.Add(seatUsedState.RoomNum);
                    ReadingRoomInfo readingRoomInfo = seatManageDateService.GetReadingRoomInfo(listNum)[0];
                    ajmReadingRoomState.OpenCloseState = readingRoomInfo.Setting.ReadingRoomOpenState(DateTime.Now).ToString();
                    ajmReadingRoomState.SeatAmount_All = seatUsedState.SeatAmountAll;
                    ajmReadingRoomState.SeatAmount_Used = seatUsedState.SeatAmountUsed;
                    ajmReadingRoomState.SeatAmount_Bespeak = seatUsedState.SeatBookingCount;
                    ajmReadingRoomState.SeatAmount_Last = ajmReadingRoomState.SeatAmount_All - ajmReadingRoomState.SeatAmount_Used - ajmReadingRoomState.SeatAmount_Bespeak + seatUsedState.SeatTemUseCount;
                    ajmReadingRoomStates.Add(ajmReadingRoomState);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReadingRoomStates);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室使用状态遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取阅览室使用状态执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 根据阅览室编号获取但个阅览室开闭状态
        /// </summary>
        /// <returns></returns>
        public string GetSingleRoomOpenState(string roomNo, string datetime)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(roomNo))
                {
                    result.Result = false;
                    result.Msg = "阅览室编号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                AJM_ReadingRoomState ajmReadingRoomState = new AJM_ReadingRoomState();
                List<string> listNum = new List<string>();
                listNum.Add(roomNo);
                ReadingRoomInfo readingRoomInfo = seatManageDateService.GetReadingRoomInfo(listNum)[0];
                ajmReadingRoomState.OpenCloseState = readingRoomInfo.Setting.ReadingRoomOpenState(Convert.ToDateTime(datetime)).ToString();
                ajmReadingRoomState.RoomName = readingRoomInfo.Name;
                ajmReadingRoomState.RoomNo = readingRoomInfo.No;
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReadingRoomState);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室开闭状态异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取阅览室开闭状态执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取阅览室可预约的座位
        /// </summary>
        /// <param name="roomNo">阅览室编号</param>
        /// <param name="bespeakTime"></param>
        /// <returns></returns>
        public string GetRoomBesapeakState(string roomNo, string bespeakTime)
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
                DateTime besppeakDate;
                if (!DateTime.TryParse(bespeakTime, out besppeakDate))
                {
                    result.Result = false;
                    result.Msg = "日期格式不正确！";
                    return JSONSerializer.Serialize(result);
                }
                if (besppeakDate.Date < DateTime.Now.Date)
                {
                    result.Result = false;
                    result.Msg = "查询日期不能早于当天日期！";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(new List<string> { roomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "阅览室不存在！";
                    return JSONSerializer.Serialize(result);
                }
                if (!readingRoomInfos[0].Setting.SeatBespeak.Used)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不提供预约！";
                    return JSONSerializer.Serialize(result);
                }
                if ((besppeakDate.Date - DateTime.Now.Date).Days > readingRoomInfos[0].Setting.SeatBespeak.BespeakBeforeDays)
                {
                    result.Result = false;
                    result.Msg = "您选择的日期尚未开放预约！";
                    return JSONSerializer.Serialize(result);
                }
                if (besppeakDate.Date == DateTime.Now.Date && !readingRoomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室未开放当天预约！";
                    return JSONSerializer.Serialize(result);
                }
                if (besppeakDate.Date != DateTime.Now.Date && !readingRoomInfos[0].Setting.IsCanBespeakSeat(besppeakDate))
                {
                    result.Result = false;
                    result.Msg = "当前时间暂未开放预约！";
                    return JSONSerializer.Serialize(result);
                }
                List<AJM_BespeakSeat> ajmSeats = new List<AJM_BespeakSeat>();
                //获取当天可预约座位
                if (besppeakDate.Date == DateTime.Now.Date)
                {
                    SeatLayout seatLayout = seatManageDateService.GetRoomSeatLayOut(roomNo);
                    foreach (Seat seat in seatLayout.Seats.Values)
                    {
                        if (!seat.IsSuspended && seat.SeatUsedState == EnterOutLogType.Leave)
                        {
                            AJM_BespeakSeat ajmSeat = new AJM_BespeakSeat();
                            ajmSeat.SeatNo = seat.SeatNo;
                            ajmSeat.SeatShortNo = seat.ShortSeatNo;
                            ajmSeats.Add(ajmSeat);
                        }
                    }
                }
                else
                {
                    //获取选择日期提供预约的座位
                    SeatLayout seats = seatManageDateService.GetBeseakSeatLayout(roomNo, besppeakDate);
                    foreach (Seat seat in seats.Seats.Values)
                    {
                        if (seat.CanBeBespeak && seat.SeatUsedState != EnterOutLogType.BookingConfirmation && !seat.IsSuspended)
                        {
                            AJM_BespeakSeat ajmSeat = new AJM_BespeakSeat();
                            ajmSeat.SeatNo = seat.SeatNo;
                            ajmSeat.SeatShortNo = seat.ShortSeatNo;
                            ajmSeats.Add(ajmSeat);
                        }
                    }
                }
                if (ajmSeats.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "当前时间没有可预约的座位！";
                    return JSONSerializer.Serialize(result);
                }

                AJM_BesapeakRoom ajmroom = new AJM_BesapeakRoom();
                ajmroom.RoomNo = readingRoomInfos[0].No;
                ajmroom.RoomName = readingRoomInfos[0].Name;
                ajmroom.BespeakDate = besppeakDate.ToShortDateString();
                ajmroom.SeatList = ajmSeats;
                ajmroom.IsCanSelectTime = readingRoomInfos[0].Setting.SeatBespeak.SpecifiedBespeak || besppeakDate.Date != DateTime.Now.Date;
                ajmroom.CheckBeforeTime = int.Parse(readingRoomInfos[0].Setting.SeatBespeak.ConfirmTime.BeginTime);
                ajmroom.CheckLastTime = int.Parse(readingRoomInfos[0].Setting.SeatBespeak.ConfirmTime.EndTime);
                ajmroom.CheckKeepTime = (int)readingRoomInfos[0].Setting.SeatBespeak.SeatKeepTime;
                //if (!ajmroom.IsCanSelectTime)
                //{
                //    ajmroom.CheckBeforeTime = 0;
                //    ajmroom.CheckLastTime = (int)readingRoomInfos[0].Setting.SeatBespeak.SeatKeepTime;
                //}
                if (readingRoomInfos[0].Setting.SeatBespeak.SpecifiedBespeak)
                {
                    List<DateTime> timeSpans = readingRoomInfos[0].Setting.GetSelectTimeList(besppeakDate);
                    foreach (var time in timeSpans)
                    {
                        ajmroom.TimeList.Add(time.ToShortTimeString());
                    }
                }
                else
                {
                    ajmroom.TimeList.Add(readingRoomInfos[0].Setting.DateOpenTime(besppeakDate).ToShortTimeString());
                }

                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmroom);
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
        /// 获取可预约的阅览室
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public string GetCanBespeakRoomInfo(string date)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                List<ReadingRoomInfo> readingRoomModels = seatManageDateService.GetReadingRoomInfo(null);
                DateTime bespeakDate = new DateTime();
                if (!DateTime.TryParse(date, out bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "日期格式错误!";
                    return JSONSerializer.Serialize(result);
                }
                List<AJM_ReadingRoom> rooms = new List<AJM_ReadingRoom>();
                foreach (ReadingRoomInfo roomInfo in readingRoomModels)
                {
                    if (!roomInfo.Setting.SeatBespeak.Used)//判断阅览室是否启用预约
                    {
                        continue;
                    }
                    if (bespeakDate.Date == DateTime.Now.Date && !roomInfo.Setting.SeatBespeak.NowDayBespeak)//预约当天座位
                    {
                        continue;
                    }
                    if (bespeakDate.Date != DateTime.Now.Date && !roomInfo.Setting.IsCanBespeakSeat(bespeakDate))
                    {
                        continue;
                    }

                    //启用预约，初始化json要传送的room信息
                    AJM_ReadingRoom room = new AJM_ReadingRoom();
                    room.LibraryName = roomInfo.Libaray.Name;
                    room.RoomName = roomInfo.Name;
                    room.RoomNo = roomInfo.No;
                    rooms.Add(room);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(rooms);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取可预约的阅览室遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取读者所在阅览室开闭状态
        /// </summary>
        /// <returns></returns>
        public AJM_ReadingRoomState GetSingleRoomOpenState(string roomNo)
        {
            try
            {
                if (string.IsNullOrEmpty(roomNo))
                {
                    return null;
                }
                AJM_ReadingRoomState ajmReadingRoomState = new AJM_ReadingRoomState();
                List<string> listNum = new List<string>();
                listNum.Add(roomNo);
                ReadingRoomInfo readingRoomInfo = seatManageDateService.GetReadingRoomInfo(listNum)[0];
                ajmReadingRoomState.OpenCloseState = readingRoomInfo.Setting.ReadingRoomOpenState(DateTime.Now).ToString();
                ajmReadingRoomState.RoomName = readingRoomInfo.Name;
                ajmReadingRoomState.RoomNo = readingRoomInfo.No;
                return ajmReadingRoomState;
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取读者所在阅览室开闭状态遇到异常：{0}", ex.Message));
                return null;
            }
        }

        /// <summary>
        /// 获取全部图书馆的使用情况
        /// </summary>
        /// <returns></returns>
        public string GetLibraryNowState()
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                //获取阅览室座位使用状态
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(null);
                Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsedStates = seatManageDateService.GetRoomSeatUsedStateV5(readingRoomInfos.Select(u => u.No).ToList());
                List<AJM_LibraryStatus> ajmLibraryStates = new List<AJM_LibraryStatus>();

                foreach (ReadingRoomInfo room in readingRoomInfos)
                {
                    if (ajmLibraryStates.Count < 1 || !ajmLibraryStates.Exists(u => u.LibraryNo == room.Libaray.No))
                    {
                        AJM_LibraryStatus status = new AJM_LibraryStatus();
                        status.LibraryNo = room.Libaray.No;
                        status.LibraryName = room.Libaray.Name;
                        ajmLibraryStates.Add(status);
                    }
                    AJM_ReadingRoomState ajmReadingRoomState = new AJM_ReadingRoomState();
                    ajmReadingRoomState.RoomName = room.Name;
                    ajmReadingRoomState.RoomNo = room.No;
                    ajmReadingRoomState.OpenCloseState = room.Setting.ReadingRoomOpenState(DateTime.Now).ToString();
                    ajmReadingRoomState.IsCanBookNowSeat = room.Setting.SeatBespeak.Used && room.Setting.SeatBespeak.NowDayBespeak;
                    ajmReadingRoomState.SeatAmount_All = room.SeatList.Seats.Count(u => u.Value.IsSuspended != true);
                    ajmReadingRoomState.SeatAmount_Used = roomSeatUsedStates[room.No].SeatAmountUsed;
                    ajmReadingRoomState.SeatAmount_Bespeak = roomSeatUsedStates[room.No].SeatBookingCount;
                    ajmReadingRoomState.SeatAmount_Last = ajmReadingRoomState.SeatAmount_All - ajmReadingRoomState.SeatAmount_Used - ajmReadingRoomState.SeatAmount_Bespeak + roomSeatUsedStates[room.No].SeatTemUseCount;
                    ajmLibraryStates.Find(u => u.LibraryNo == room.Libaray.No).RoomStatus.Add(ajmReadingRoomState);
                }
                foreach (AJM_LibraryStatus status in ajmLibraryStates)
                {
                    status.AllSeats = status.RoomStatus.Sum(u => u.SeatAmount_All);
                    status.AllBooked = status.RoomStatus.Sum(u => u.SeatAmount_Bespeak);
                    status.AllUsed = status.RoomStatus.Sum(u => u.SeatAmount_Used);
                    status.UsedPercentage = (int)((double)status.AllUsed / (double)status.AllSeats * 100);
                }


                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmLibraryStates);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取阅览室使用状态遇到异常：{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取阅览室使用状态执行遇到异常！";
                return JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取开放预约的阅览室
        /// </summary>
        /// <returns></returns>
        public string GetCanBespeakRoom()
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<AJM_ReadingRoom> ajmReadingRooms = new List<AJM_ReadingRoom>();
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(null);
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "没有查询到阅览室信息，请查看阅览室是否存在！";
                    return JSONSerializer.Serialize(result);
                }
                foreach (ReadingRoomInfo room in readingRoomInfos.FindAll(u => u.Setting.SeatBespeak.Used))
                {
                    AJM_ReadingRoom ajmReadingRoom = new AJM_ReadingRoom();
                    ajmReadingRoom.RoomNo = room.No;
                    ajmReadingRoom.RoomName = room.Name;
                    ajmReadingRoom.LibraryName = room.Libaray.Name;
                    ajmReadingRooms.Add(ajmReadingRoom);
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReadingRooms);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取开放预约的阅览室遇到异常{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取开放预约的阅览室遇到异常";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取阅览室可预约的日期
        /// </summary>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string GetBespeakDate(string roomNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<string> listNum = new List<string>();
                listNum.Add(roomNo);
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(listNum);
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "没有查询到阅览室信息，请查看阅览室是否存在！";
                    return JSONSerializer.Serialize(result);
                }

                AJM_ReadingRoomBespeak ajmReadingRoom = new AJM_ReadingRoomBespeak();
                ajmReadingRoom.RoomNo = readingRoomInfos[0].No;
                ajmReadingRoom.RoomName = readingRoomInfos[0].Name;
                ajmReadingRoom.LibraryName = readingRoomInfos[0].Libaray.Name;
                if (!readingRoomInfos[0].Setting.SeatBespeak.Used)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室没有开放预约！";
                    return JSONSerializer.Serialize(result);
                }
                if (readingRoomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    ajmReadingRoom.BespeakDate.Add(DateTime.Now.ToString("yyyy-MM-dd"));
                }
                for (int i = 1; i <= readingRoomInfos[0].Setting.SeatBespeak.BespeakBeforeDays;i++)
                {
                    if (readingRoomInfos[0].Setting.IsCanBespeakSeat(DateTime.Now.AddDays(i)))
                    {
                        ajmReadingRoom.BespeakDate.Add(DateTime.Now.AddDays(i).ToString("yyyy-MM-dd"));
                    }
                }

                result.Result = true;
                result.Msg = JSONSerializer.Serialize(ajmReadingRoom);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("获取开放预约的阅览室遇到异常{0}", ex.Message));
                result.Result = false;
                result.Msg = "获取开放预约的阅览室遇到异常";
                return JSONSerializer.Serialize(result);
            }
        }
    }
}
