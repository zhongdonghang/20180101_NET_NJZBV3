using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SeatManage.JsonModel;
using System.Data.SqlClient;
using SeatManage.ClassModel;
namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IReadingRoom
    {
        /// <summary>
        /// 获取所有阅览室的基础信息
        /// </summary>
        /// <returns></returns>
        public string GetAllReadingRoomBaseInfo()
        {
            try
            {
                List<JM_ReadingRoom> rooms = new List<JM_ReadingRoom>();
                List<SeatManage.ClassModel.ReadingRoomInfo> roomListModel = seatDataService.GetReadingRooms(null, null, null);
                for (int i = 0; i < roomListModel.Count; i++)
                {
                    JM_ReadingRoom roomInfo = new JM_ReadingRoom();
                    roomInfo.RoomNum = roomListModel[i].No;
                    roomInfo.RoomName = roomListModel[i].Name;
                    //TODO:添加阅览室设置属性 

                    //if (!String.IsNullOrEmpty(dr["ReadingSetting"].ToString()))
                    //{
                    //    roomInfo.Setting = new ReadingRoomSetting(dr["ReadingSetting"].ToString());
                    //}
                    //else
                    //{
                    //    roomInfo.Setting = new ReadingRoomSetting();
                    //}

                    roomInfo.LibraryName = roomListModel[i].Libaray.Name;
                    roomInfo.SchoolName = roomListModel[i].Libaray.School.Name;

                    roomInfo.AreaName = roomListModel[i].Area.AreaName;
                    rooms.Add(roomInfo);
                }
                return SeatManageComm.JSONSerializer.Serialize(rooms);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取全部阅览室信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据编号获取阅览室的设置信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public string GetReadingRoomSetInfoByRoomNum(string roomNum)
        {
            try
            {
                if (string.IsNullOrEmpty(roomNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "阅览室编号不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JM_NowRoomSet jm_roomSet = new JM_NowRoomSet();

                SqlParameter[] parameters = { 
                                        new SqlParameter ("@ReadingRoomNo", SqlDbType.NVarChar,50)
                                        };
                List<SeatManage.ClassModel.ReadingRoomInfo> roomListModel = seatDataService.GetReadingRoomInfo(new List<string>() { roomNum });
                if (roomListModel.Count > 0)
                {
                    SeatManage.ClassModel.ReadingRoomSetting roomSet = roomListModel[0].Setting;
                    jm_roomSet.IsCanBespeakNowSeat = roomSet.SeatBespeak.NowDayBespeak && roomSet.SeatBespeak.Used ? true : false;
                    jm_roomSet.IsCanBespeakSeat = roomSet.IsCanBespeakSeat(DateTime.Now);
                    jm_roomSet.IsCanContinueTime = roomSet.SeatUsedTimeLimit.IsCanContinuedTime;
                    jm_roomSet.IsCanSelectBespeakSeat = roomSet.SeatBespeak.SelectBespeakSeat;
                    jm_roomSet.IsCanWaitSeat = roomSet.NoManagement.Used;
                    jm_roomSet.IsLimitBlacklist = roomSet.UsedBlacklistLimit;
                    jm_roomSet.IsLimitReaderType = roomSet.LimitReaderEnter.Used;
                    jm_roomSet.IsEnterOnly = roomSet.LimitReaderEnter.CanEnter;
                    jm_roomSet.LimitType = roomSet.LimitReaderEnter.ReaderTypes;
                    jm_roomSet.NowRoomCloseTime = roomSet.NowCloseTime(DateTime.Now).ToShortTimeString();
                    jm_roomSet.NowRoomOpenTime = roomSet.NowCloseTime(DateTime.Now).ToShortTimeString();
                    jm_roomSet.NowState = roomSet.ReadingRoomOpenState(DateTime.Now).ToString();
                    jm_roomSet.SelectSelectModel = roomSet.RoomSelectSeatMode(DateTime.Now).ToString();
                    jm_roomSet.ShortLeaveHoldTime = int.Parse(roomSet.GetSeatHoldTime(DateTime.Now).ToString().Split('.')[0]);
                    #region 完整的阅览室配置
                    //jm_roomSet.BlackListSet.IsLimitBlacklist = roomSet.UsedBlacklistLimit;
                    //jm_roomSet.BlackListSet.IsViolation = roomSet.IsRecordViolate;
                    //jm_roomSet.BlackListSet.Setting.AutoLeaveDays = roomSet.BlackListSetting.LimitDays;
                    //jm_roomSet.BlackListSet.Setting.IsUsed = roomSet.BlackListSetting.Used;
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationBookingTimeOut = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.BookingTimeOut];
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationLeaveByAdmin = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.LeaveByAdmin];
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationSeatOutTime = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.SeatOutTime];
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveByAdminOutTime = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime];
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveByReaderOutTime = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime];
                    //jm_roomSet.BlackListSet.Setting.IsUseViolationShortLeaveOutTime = roomSet.BlackListSetting.ViolateRoule[EnumType.ViolationRecordsType.ShortLeaveByServiceOutTime];
                    //jm_roomSet.BlackListSet.Setting.LeaveBlacklistModel = roomSet.BlackListSetting.LeaveBlacklist.ToString();
                    //jm_roomSet.BlackListSet.Setting.ViolateCountWithEnterBlacklist = roomSet.BlackListSetting.ViolateTimes;
                    //jm_roomSet.BlackListSet.Setting.ViolateFailDays = roomSet.BlackListSetting.ViolateFailDays;
                    //jm_roomSet.LimitReaderEnterSet.CanEnter = roomSet.LimitReaderEnter.CanEnter;
                    //jm_roomSet.LimitReaderEnterSet.ReaderTypes = roomSet.LimitReaderEnter.ReaderTypes;
                    //jm_roomSet.LimitReaderEnterSet.Used = roomSet.LimitReaderEnter.Used;
                    //jm_roomSet.PosRestrict.IsUsed = roomSet.PosTimes.IsUsed;
                    //jm_roomSet.PosRestrict.Minutes = roomSet.PosTimes.Minutes;
                    //jm_roomSet.PosRestrict.Times = roomSet.PosTimes.Times;
                    //jm_roomSet.RoomOCPlanSet.AdvancedOpenClosePlan = new List<JM_RoomOpenClosePlan>();
                    //foreach (KeyValuePair<DayOfWeek, SeatManage.ClassModel.RoomOpenPlanSet> day in roomSet.RoomOpenSet.RoomOpenPlan)
                    //{
                    //    JM_RoomOpenClosePlan plan = new JM_RoomOpenClosePlan();
                    //    plan.Day = day.Key.ToString();
                    //    plan.IsUsed = day.Value.Used;
                    //    plan.OpenCloseTimeSpan = new List<JM_TimeSpan>();
                    //    foreach (TimeSpace ts in day.Value.OpenTime)
                    //    {
                    //        JM_TimeSpan jm_ts = new JM_TimeSpan();
                    //        jm_ts.StartTime = ts.BeginTime;
                    //        jm_ts.EndTime = ts.EndTime;
                    //        plan.OpenCloseTimeSpan.Add(jm_ts);
                    //    }
                    //    jm_roomSet.RoomOCPlanSet.AdvancedOpenClosePlan.Add(plan);
                    //}
                    //jm_roomSet.RoomOCPlanSet.CloseBeforeTimeLength = int.Parse(roomSet.RoomOpenSet.CloseBeforeTimeLength.ToString().Split('.')[0]);
                    //jm_roomSet.RoomOCPlanSet.DefaultCloseTime = roomSet.RoomOpenSet.DefaultOpenTime.EndTime;
                    //jm_roomSet.RoomOCPlanSet.DefaultOpenTime = roomSet.RoomOpenSet.DefaultOpenTime.BeginTime;
                    //jm_roomSet.RoomOCPlanSet.IsUsed24HourModel = roomSet.RoomOpenSet.UninterruptibleModel;
                    //jm_roomSet.RoomOCPlanSet.IsUsedAdvancedModel = roomSet.RoomOpenSet.UsedAdvancedSet;
                    //jm_roomSet.RoomOCPlanSet.OpenBeforeTimeLength = int.Parse(roomSet.RoomOpenSet.OpenBeforeTimeLength.ToString().Split('.')[0]);
                    //jm_roomSet.SeatBespeakSet.CanBespeakSeatCount = roomSet.SeatBespeak.BespeakSeatCount;
                    //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan = new JM_TimeSpan();
                    //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan.StartTime = roomSet.SeatBespeak.CanBespeatTimeSpace.BeginTime;
                    //jm_roomSet.SeatBespeakSet.CanBespeakTimeSpan.EndTime = roomSet.SeatBespeak.CanBespeatTimeSpace.EndTime;
                    //jm_roomSet.SeatBespeakSet.CanNotBespeakDate = new List<JM_TimeSpan>();
                    //foreach (TimeSpace ts in roomSet.SeatBespeak.NoBespeakDates)
                    //{
                    //    JM_TimeSpan jm_ts = new JM_TimeSpan();
                    //    jm_ts.StartTime = ts.BeginTime;
                    //    jm_ts.EndTime = ts.EndTime;
                    //    jm_roomSet.SeatBespeakSet.CanNotBespeakDate.Add(jm_ts);
                    //}
                    //jm_roomSet.SeatBespeakSet.IsCanBespeakWithOnSeat = roomSet.SeatBespeak.BespeatWithOnSeat;
                    //jm_roomSet.SeatBespeakSet.IsCanSelectBespeakSeat = roomSet.SeatBespeak.SelectBespeakSeat;
                    //jm_roomSet.SeatBespeakSet.IsSpecifiedTime = roomSet.SeatBespeak.SpecifiedTime;
                    //jm_roomSet.SeatBespeakSet.IsUsedMultiSpanBespeak = roomSet.SeatBespeak.SpecifiedBespeak;
                    //jm_roomSet.SeatBespeakSet.IsUsedNowDaySeatBespeak = roomSet.SeatBespeak.NowDayBespeak;
                    //jm_roomSet.SeatBespeakSet.IsUsedSeatBespeak = roomSet.SeatBespeak.Used;
                    //jm_roomSet.SeatBespeakSet.NowDaySeatKeepTimeLength = int.Parse(roomSet.SeatBespeak.SeatKeepTime.ToString().Split('.')[0]);
                    //jm_roomSet.SeatBespeakSet.SigninTimeSpan = new JM_TimeSpan();
                    //jm_roomSet.SeatBespeakSet.SigninTimeSpan.StartTime = roomSet.SeatBespeak.ConfirmTime.BeginTime;
                    //jm_roomSet.SeatBespeakSet.SigninTimeSpan.EndTime = roomSet.SeatBespeak.ConfirmTime.EndTime;
                    //jm_roomSet.SeatBespeakSet.SpecifiedTimeList = new List<string>();
                    //foreach (DateTime item in roomSet.SeatBespeak.SpecifiedTimeList)
                    //{
                    //    jm_roomSet.SeatBespeakSet.SpecifiedTimeList.Add(item.ToShortTimeString());
                    //}
                    //jm_roomSet.SeatChooseMethod.DefaultChooseMethod = roomSet.SeatChooseMethod.DefaultChooseMethod.ToString();
                    //jm_roomSet.SeatChooseMethod.UsedAdvancedSet = roomSet.SeatChooseMethod.UsedAdvancedSet;
                    //jm_roomSet.SeatChooseMethod.AdvancedSet = new List<JM_SeatChooseMethodPlan>();
                    //foreach (KeyValuePair<DayOfWeek, SeatChooseMethodPlan> item in roomSet.SeatChooseMethod.AdvancedSelectSeatMode)
                    //{
                    //    JM_SeatChooseMethodPlan jm_plan = new JM_SeatChooseMethodPlan();
                    //    jm_plan.Day = item.Key.ToString();
                    //    jm_plan.IsUsed = item.Value.Used;
                    //    jm_plan.DayPlan = new List<JM_SeatChooseMethodAdvancedSet>();
                    //    foreach (SeatChooseMethodOption option in item.Value.PlanOption)
                    //    {
                    //        JM_SeatChooseMethodAdvancedSet s = new JM_SeatChooseMethodAdvancedSet();
                    //        s.ChooseMethodTimeSpan = new JM_TimeSpan();
                    //        s.ChooseMethod = option.ChooseMethod.ToString();
                    //        s.ChooseMethodTimeSpan.StartTime = option.UsedTime.BeginTime;
                    //        s.ChooseMethodTimeSpan.EndTime = option.UsedTime.EndTime;
                    //        jm_plan.DayPlan.Add(s);
                    //    }
                    //    jm_roomSet.SeatChooseMethod.AdvancedSet.Add(jm_plan);
                    //}
                    //jm_roomSet.SeatShortLeaveSet.DefaultHoldTimeLength = roomSet.SeatHoldTime.DefaultHoldTimeLength;
                    //jm_roomSet.SeatShortLeaveSet.AdminSet = new JM_AdminShortLeaveSet();
                    //jm_roomSet.SeatShortLeaveSet.AdminSet.HoldTimeLength = roomSet.AdminShortLeave.HoldTimeLength;
                    //jm_roomSet.SeatShortLeaveSet.AdminSet.IsUsed = roomSet.AdminShortLeave.IsUsed;
                    //jm_roomSet.SeatShortLeaveSet.AdvancedSet = new List<JM_ShortLeavePlan>();
                    //foreach (SeatHoldTimeOption item in roomSet.SeatHoldTime.AdvancedSeatHoldTime)
                    //{
                    //    JM_ShortLeavePlan plan = new JM_ShortLeavePlan();
                    //    plan.ChooseMethodTimeSpan = new JM_TimeSpan();
                    //    plan.ChooseMethodTimeSpan.StartTime = item.UsedTime.BeginTime;
                    //    plan.ChooseMethodTimeSpan.EndTime = item.UsedTime.EndTime;
                    //    plan.HoldTimeLength = item.HoldTimeLength;
                    //    plan.IsUsed = item.Used;
                    //    jm_roomSet.SeatShortLeaveSet.AdvancedSet.Add(plan);
                    //}
                    //jm_roomSet.SeatUsedTimeSet.CanContinuedTimes = roomSet.SeatUsedTimeLimit.ContinuedTimes;
                    //jm_roomSet.SeatUsedTimeSet.DelayLastTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.CanDelayTime.ToString().Split('.')[0]);
                    //jm_roomSet.SeatUsedTimeSet.DelayTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.DelayTimeLength.ToString().Split('.')[0]);
                    //jm_roomSet.SeatUsedTimeSet.FixedTimes = new List<string>();
                    //foreach (DateTime dt in roomSet.SeatUsedTimeLimit.FixedTimes)
                    //{
                    //    jm_roomSet.SeatUsedTimeSet.FixedTimes.Add(dt.ToShortTimeString());
                    //}
                    //jm_roomSet.SeatUsedTimeSet.HoldTimeModel = roomSet.SeatUsedTimeLimit.Mode;
                    //jm_roomSet.SeatUsedTimeSet.IsUsed = roomSet.SeatUsedTimeLimit.Used;
                    //jm_roomSet.SeatUsedTimeSet.IsUsedContinueTime = roomSet.SeatUsedTimeLimit.IsCanContinuedTime;
                    //jm_roomSet.SeatUsedTimeSet.OverTimeHandle = roomSet.SeatUsedTimeLimit.OverTimeHandle.ToString();
                    //jm_roomSet.SeatUsedTimeSet.SeatHoldTimeLength = int.Parse(roomSet.SeatUsedTimeLimit.UsedTimeLength.ToString().Split('.')[0]);
                    //jm_roomSet.SeatWaitSet.IsUsed = roomSet.NoManagement.Used;
                    //jm_roomSet.SeatWaitSet.OperatingTimeInterval = int.Parse(roomSet.NoManagement.OperatingInterval.ToString().Split('.')[0]);
                    #endregion

                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起，找不到此阅览室!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                return SeatManageComm.JSONSerializer.Serialize(jm_roomSet);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室设置遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据阅览室编号获取阅览室的座位信息
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public string GetSeatsLayoutByRoomNum(string roomNum)
        {
            try
            {
                if (string.IsNullOrEmpty(roomNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "阅览室编号不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JsonModel.JM_SeatLayout jm_layout = new JM_SeatLayout();
                List<SeatManage.ClassModel.ReadingRoomInfo> roomListModel = seatDataService.GetReadingRooms(new List<string>() { roomNum }, null, null);
                if (roomListModel.Count > 0)
                {
                    SeatLayout seatlayout = roomListModel[0].SeatList;
                    foreach (Seat seat in seatlayout.Seats.Values)
                    {
                        JM_Seat jm_seat = new JM_Seat();
                        jm_seat.HavePower = seat.HavePower;
                        jm_seat.IsSuspended = seat.IsSuspended;
                        jm_seat.ReadingRoomNum = roomListModel[0].No;
                        jm_seat.ReadingRoomName = roomListModel[0].Name;
                        jm_seat.SeatNo = seat.SeatNo;
                        jm_seat.ShortSeatNo = seat.ShortSeatNo;
                        jm_seat.BaseHeight = seat.BaseHeight;
                        jm_seat.BaseWidth = seat.BaseWidth;
                        jm_seat.PositionX = seat.PositionX;
                        jm_seat.PositionY = seat.PositionY;
                        jm_seat.RotationAngle = seat.RotationAngle;
                        jm_layout.Seats.Add(jm_seat);
                    }
                    foreach (Note note in seatlayout.Notes)
                    {
                        JM_Node jm_Note = new JM_Node();
                        jm_Note.BaseHeight = note.BaseHeight;
                        jm_Note.BaseWidth = note.BaseWidth;
                        jm_Note.PositionX = note.PositionX;
                        jm_Note.PositionY = note.PositionY;
                        jm_Note.Remark = note.Remark;
                        jm_Note.RotationAngle = note.RotationAngle;
                        jm_Note.Type = note.Type.ToString();
                        jm_layout.Nodes.Add(jm_Note);
                    }
                    jm_layout.Position = seatlayout.Position;
                    jm_layout.RoomNo = seatlayout.RoomNo;
                    jm_layout.SeatCol = seatlayout.SeatCol;
                    jm_layout.SeatRow = seatlayout.SeatRow;
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_layout);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "此阅览室不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室布局图遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据阅览室编号获取当前座位使用情况的布局图（包括预约）。
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        public string GetSeatsUsedInfoByRoomNum(string roomNum)
        {
            try
            {
                if (string.IsNullOrEmpty(roomNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "阅览室编号不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<JM_Seat> seats = new List<JM_Seat>();
                SeatLayout bespeakSeatLayout = seatDataService.GetRoomSeatLayOut(roomNum);
                foreach (Seat seat in bespeakSeatLayout.Seats.Values)
                {
                    if (!seat.IsSuspended)
                    {
                        JM_Seat jm_seat = new JM_Seat();
                        jm_seat.CanBeBespeak = true;
                        jm_seat.HavePower = seat.HavePower;
                        jm_seat.IsSuspended = seat.IsSuspended;
                        jm_seat.ReadingRoomNum = seat.ReadingRoom.No;
                        jm_seat.ReadingRoomName = seat.ReadingRoom.Name;
                        jm_seat.SeatNo = seat.SeatNo;
                        jm_seat.SeatUsedState = seat.SeatUsedState.ToString();
                        jm_seat.ShortSeatNo = seat.ShortSeatNo;
                        jm_seat.BaseHeight = seat.BaseHeight;
                        jm_seat.BaseWidth = seat.BaseWidth;
                        jm_seat.PositionX = seat.PositionX;
                        jm_seat.PositionY = seat.PositionY;
                        jm_seat.RotationAngle = seat.RotationAngle;
                        seats.Add(jm_seat);
                    }
                }
                return SeatManageComm.JSONSerializer.Serialize(seats);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室座位使用情况布局图遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取可被预约的座位布局设置
        /// </summary>
        /// <param name="roomNum">阅览室编号</param>
        /// <returns></returns>
        //public string GetCanBespeakSeatsLayout(string roomNum)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// 获取可以预约的座位预约信息
        /// </summary>
        /// <param name="roomNum"></param>
        ///  <param name="date">日期</param>
        /// <returns></returns>
        public string GetSeatsBespeakInfoByRoomNum(string roomNum, string date)
        {
            try
            {
                if (string.IsNullOrEmpty(roomNum) || string.IsNullOrEmpty(date))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "阅览室编号或日期不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                //TODO:判断预约的日期是否为当天。
                //如果预约日期是当天，获取当天的可用座位（去掉已经被预约的）
                DateTime bespeakDate;
                if (!DateTime.TryParse(date, out bespeakDate))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "日期格式不正确!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date < DateTime.Now.Date)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起查询日期不得小于当天时间!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<SeatManage.ClassModel.ReadingRoomInfo> roomInfos = seatDataService.GetReadingRoomInfo(new List<string>() { roomNum });
                if (roomInfos.Count < 1)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "当前阅览室不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (!roomInfos[0].Setting.SeatBespeak.Used)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "当前阅览室不提供预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date - DateTime.Now.Date).Days > roomInfos[0].Setting.SeatBespeak.BespeakBeforeDays)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起您选择的日期不开放预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && !roomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起此阅览室不提供当天预约功能!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date != DateTime.Now.Date && !roomInfos[0].Setting.IsCanBespeakSeat(bespeakDate))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起当前日期或时间段暂不开放预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<JM_Seat> seats = new List<JM_Seat>();
                if (bespeakDate.Date == DateTime.Now.Date)
                {
                    SeatLayout seatLayout = seatDataService.GetRoomSeatLayOut(roomNum);//获取当天可预约座位。
                    foreach (Seat seat in seatLayout.Seats.Values)
                    {
                        //if (!seat.IsSuspended && seat.SeatUsedState == EnumType.EnterOutLogType.Leave)
                        //{
                        JM_Seat jm_seat = new JM_Seat();
                        if (!seat.IsSuspended && seat.SeatUsedState == EnumType.EnterOutLogType.Leave)
                        {
                            jm_seat.CanBeBespeak = true;
                        }
                        else
                        {
                            jm_seat.CanBeBespeak = false;
                        }
                        //jm_seat.CanBeBespeak = seat.CanBeBespeak;
                        jm_seat.HavePower = seat.HavePower;
                        jm_seat.IsSuspended = seat.IsSuspended;
                        jm_seat.ReadingRoomNum = seat.ReadingRoom.No;
                        jm_seat.ReadingRoomName = seat.ReadingRoom.Name;
                        jm_seat.SeatNo = seat.SeatNo;
                        jm_seat.SeatUsedState = seat.SeatUsedState.ToString();
                        jm_seat.ShortSeatNo = seat.ShortSeatNo;
                        jm_seat.BaseHeight = seat.BaseHeight;
                        jm_seat.BaseWidth = seat.BaseWidth;
                        jm_seat.PositionX = seat.PositionX;
                        jm_seat.PositionY = seat.PositionY;
                        jm_seat.RotationAngle = seat.RotationAngle;
                        seats.Add(jm_seat);
                        //}
                    }
                }
                else
                {
                    SeatLayout bespeakSeatLayout = seatDataService.GetBeseakSeatLayout(roomNum, bespeakDate);
                    foreach (Seat seat in bespeakSeatLayout.Seats.Values)
                    {
                        //if (seat.CanBeBespeak && seat.SeatUsedState != EnumType.EnterOutLogType.BookingConfirmation && !seat.IsSuspended)
                        //{
                        JM_Seat jm_seat = new JM_Seat();
                        if (seat.CanBeBespeak && seat.SeatUsedState != EnumType.EnterOutLogType.BookingConfirmation && !seat.IsSuspended)
                        {
                            jm_seat.CanBeBespeak = true;
                        }
                        else
                        {
                            jm_seat.CanBeBespeak = false;
                        }
                        //jm_seat.CanBeBespeak = seat.CanBeBespeak;
                        jm_seat.HavePower = seat.HavePower;
                        jm_seat.IsSuspended = seat.IsSuspended;
                        jm_seat.ReadingRoomNum = seat.ReadingRoom.No;
                        jm_seat.ReadingRoomName = seat.ReadingRoom.Name;
                        jm_seat.SeatNo = seat.SeatNo;
                        jm_seat.SeatUsedState = seat.SeatUsedState.ToString();
                        jm_seat.ShortSeatNo = seat.ShortSeatNo;
                        jm_seat.BaseHeight = seat.BaseHeight;
                        jm_seat.BaseWidth = seat.BaseWidth;
                        jm_seat.PositionX = seat.PositionX;
                        jm_seat.PositionY = seat.PositionY;
                        jm_seat.RotationAngle = seat.RotationAngle;
                        seats.Add(jm_seat);
                        //}
                    }
                }
                if (seats.Count > 0)
                {
                    return SeatManageComm.JSONSerializer.Serialize(seats);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "当前日期没有可供预约的座位!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室预约座位布局遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取所有阅览室座位使用信息（使用数/座位总数）
        /// </summary>
        /// <returns></returns>
        public string GetAllRoomSeatUsedInfo()
        {
            try
            {
                // seatDataService.GetLibraryList(null, null, null);
                Dictionary<string, ReadingRoomSeatUsedState> rooms = seatDataService.GetRoomSeatUsedState(null);//阅览室座位使用状态
                List<JM_RoomSeatUsedState> seatUsedStateList = new List<JM_RoomSeatUsedState>();

                Dictionary<string, JM_LibrarySeatsInfo> librarys = new Dictionary<string, JM_LibrarySeatsInfo>();
                foreach (ReadingRoomSeatUsedState seatUsedState in rooms.Values)
                {

                    JM_RoomSeatUsedState jm_seatUsedState = new JM_RoomSeatUsedState();
                    jm_seatUsedState.RoomName = seatUsedState.RoomName;
                    jm_seatUsedState.RoomNum = seatUsedState.RoomNum;
                    jm_seatUsedState.SeatAmountAll = seatUsedState.SeatAmountAll;
                    jm_seatUsedState.SeatAmountUsed = seatUsedState.SeatAmountUsed;
                    jm_seatUsedState.RoomSeatUsingState = seatUsedState.RoomSeatUsingState.ToString();
                    if (!librarys.ContainsKey(seatUsedState.LibraryNum))//如果图书馆列表中存在阅览室所在图书馆的编号,则把当前阅览室添加到该图书馆下
                    {
                        JM_LibrarySeatsInfo library = new JM_LibrarySeatsInfo();
                        library.LibraryName = seatUsedState.LibraryName;
                        library.LibraryNum = seatUsedState.LibraryNum;
                        librarys.Add(seatUsedState.LibraryNum, library);
                    }
                    librarys[seatUsedState.LibraryNum].ReadingRoomSeatUsedState.Add(jm_seatUsedState);
                    librarys[seatUsedState.LibraryNum].SeatAmountAll += jm_seatUsedState.SeatAmountAll;
                    librarys[seatUsedState.LibraryNum].SeatAmountUsed += jm_seatUsedState.SeatAmountUsed;

                }
                List<JM_LibrarySeatsInfo> resultList = librarys.Values.ToList();
                return SeatManageComm.JSONSerializer.Serialize(resultList);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("获取阅览室使用情况遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
    }
}
