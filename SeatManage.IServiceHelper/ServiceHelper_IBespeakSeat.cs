using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;
using SeatManage.JsonModel;

/**
 * 该接口可实现两种预约方式：
 *    1.预约当天座位
 *    2.预约其他时间座位
 *    3.自定义预约时间
 * **/

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IBespeakSeat
    {
        private static string schoolNum = System.Configuration.ConfigurationManager.AppSettings["SchoolNo"];
        private SeatManage.DAL.T_SM_SeatBespeak bespeakDal = new DAL.T_SM_SeatBespeak();
        private WcfServiceForSeatManage.SeatManageDateService seatDataService = new WcfServiceForSeatManage.SeatManageDateService();
        SeatManage.IPocketBespeakBllService.IPocketBespeakBllService bespeakBllService = new SeatManage.PocketBespeakBllService.PocketBespeakBllService();
        /// <summary>
        /// 获取开放预约的阅览室
        /// </summary>
        /// <param name="strDate"></param>
        /// <returns></returns>
        public string GetOpenBespeakRooms(string strDate)
        {
            try
            {
               
                List<SeatManage.ClassModel.ReadingRoomInfo> readingRoomModels = seatDataService.GetReadingRooms(null, null, null);
                DateTime bespeakDate = DateTime.Parse(strDate);
                List<JsonModel.JM_OpenBespeakReadingRoom> rooms = new List<JsonModel.JM_OpenBespeakReadingRoom>();
                Dictionary<string, ReadingRoomSeatBespeakState> roomBesapeakState = seatDataService.GetRoomBespeakSeatState(null, bespeakDate);
                for (int i = 0; i < readingRoomModels.Count; i++)
                {

                    ClassModel.ReadingRoomInfo roomInfo = readingRoomModels[i];
                    SeatLayout bespeakSeatLayout = seatDataService.GetBeseakSeatLayout(roomInfo.No, bespeakDate);
                    if (!roomInfo.Setting.SeatBespeak.Used)//判断阅览室是否启用预约
                    {
                        continue;
                    }
                    //启用预约，初始化json要传送的room信息
                    JsonModel.JM_OpenBespeakReadingRoom room = new JsonModel.JM_OpenBespeakReadingRoom();
                    room.AreaName = roomInfo.Area.AreaName;
                    room.LibraryName = roomInfo.Libaray.Name;
                    room.RoomName = roomInfo.Name;
                    room.RoomNum = roomInfo.No;
                    room.SchoolName = roomInfo.Libaray.School.Name;
                    room.DefaultBespeakTime = roomInfo.Setting.RoomOpenSet.DateOpenTime(bespeakDate).ToString("yyyy-MM-dd HH:mm:ss");
                    room.CanBespeakSeatCount = roomBesapeakState[roomInfo.No].CanBespeakAmcount;
                    room.BespeakedCount = roomBesapeakState[roomInfo.No].BespeakedAmcount;
                    if (bespeakDate.Date == DateTime.Now.Date)//预约当天座位
                    {
                        if (!roomInfo.Setting.SeatBespeak.NowDayBespeak)//不启用当天预约，也忽略
                        {
                            continue;
                        }
                        else
                        {
                            room.CanBespeak = true;
                            rooms.Add(room);
                            continue;
                        }
                    }
                    else
                    {   //验证预约日期是否符合规定
                        if (!dateBespeak(roomInfo.Setting, bespeakDate, DateTime.Now))
                        {
                            continue;
                        }
                        else if (timeCanBespeak(roomInfo.Setting.SeatBespeak, DateTime.Now))
                        {
                            room.CanBespeak = true;
                            rooms.Add(room);
                            continue;
                        }
                        else
                        {
                            room.CanBespeak = false;
                            room.Remark = string.Format("{0}至{1}开始预约。", roomInfo.Setting.SeatBespeak.CanBespeatTimeSpace.BeginTime, roomInfo.Setting.SeatBespeak.CanBespeatTimeSpace.EndTime);
                            rooms.Add(room);
                            continue;
                        }
                    }
                }
                return SeatManageComm.JSONSerializer.Serialize(rooms);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 提交自定义时间的预约记录
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="bespeakDatetime"></param>
        /// <returns></returns>
        public string SubmitBespeakInfoCustomTime(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            try
            {
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                //UserInfo user = seatDataService.GetUserInfo(cardNo);
                //if (user == null)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，您的账号不存在，请先去触摸屏终端进行预约激活。";
                //    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                //}
                //if (user.UserType != EnumType.UserType.Reader)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，您没有预约座位的权限。";
                //    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                //}
                //验证读者当前是否有座位
                DateTime bespeakDate;
                if (!DateTime.TryParse(bespeakDatetime, out bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "日期格式不正确!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date < DateTime.Now.Date)
                {
                    result.Result = false;
                    result.Msg = "对不起查询日期不得小于当天时间!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date)
                {
                    EnterOutLogInfo enterOutLog = seatDataService.GetEnterOutLogInfoByCardNo(cardNo);
                    if (enterOutLog != null && enterOutLog.EnterOutState != EnterOutLogType.Leave)
                    {
                        result.Result = false;
                        result.Msg = "对不起，您当前已经有座位。";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    //验证读者是否在等待座位
                    List<EnterOutLogType> logType = new List<EnterOutLogType>();
                    logType.Add(EnterOutLogType.Waiting);
                    List<WaitSeatLogInfo> waitSeatlogs = seatDataService.GetWaitLogList(cardNo, null, null, null, logType);
                    if (waitSeatlogs.Count > 0)
                    {
                        result.Result = false;
                        result.Msg = "对不起，您当前在等待座位。";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }
                if (string.IsNullOrEmpty(roomNum) || string.IsNullOrEmpty(bespeakDatetime))
                {
                    result.Result = false;
                    result.Msg = "阅览室编号或日期不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                Seat seatInfo = seatDataService.GetSeatInfoBySeatNum(seatNum);
                if (seatInfo == null)
                {
                    result.Result = false;
                    result.Msg = "对不起此座位不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatInfo.IsSuspended)
                {
                    result.Result = false;
                    result.Msg = "对不起此座位已停用!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }

               
                List<SeatManage.ClassModel.ReadingRoomInfo> roomInfos = seatDataService.GetReadingRoomInfo(new List<string>() { roomNum });
                if (roomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不存在!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (!roomInfos[0].Setting.SeatBespeak.Used)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不提供预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date - DateTime.Now.Date).Days > roomInfos[0].Setting.SeatBespeak.BespeakBeforeDays)
                {
                    result.Result = false;
                    result.Msg = "对不起您选择的日期不开放预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && !roomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    result.Result = false;
                    result.Msg = "对不起此阅览室不提供当天预约功能!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date != DateTime.Now.Date && !roomInfos[0].Setting.IsCanBespeakSeat(bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "对不起当前日期或时间段暂不开放预约!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (!string.IsNullOrEmpty(checkBlacklist(cardNo, roomInfos[0])))
                {
                    result.Result = false;
                    result.Msg = "对不起，您在黑名单中存在记录。";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> bespeakLogs = seatDataService.GetBespeakLogInfo(cardNo, bespeakDate);//获取指定时间的预约信息
                if (bespeakLogs.Count > 0)//如果存在预约信息，则不能再预约
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，您已预约了[{0}] [{1}]座位。请在规定的时间内刷卡确认,或者取消该预约。", bespeakLogs[0].ReadingRoomName, bespeakLogs[0].ShortSeatNum);
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                ClassModel.ReadingRoomInfo roomInfo = roomInfos[0];
                if (roomInfo.Setting.ReadingRoomOpenState(bespeakDate) == ReadingRoomStatus.Close)
                {
                    result.Result = false;
                    result.Msg = "对不起，您预约的时间阅览室处于关闭状态。";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> seatbespeakLog = seatDataService.GetBespeakLogInfoBySeatNo(seatNum, bespeakDate);
                if (seatbespeakLog.Count > 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，此座位在您选择的时间段已被预约。";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && seatInfo.SeatUsedState != EnterOutLogType.Leave)//如果启用预约，判断选择的日期是否为当天的日期
                {
                    result.Result = false;
                    result.Msg = "对不起，此座位正在被使用。";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (roomInfo.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                {
                    List<BespeakLogInfo> bespeaklogs = seatDataService.GetBespeakLogInfoByRoomsNum(new List<string>() { roomInfo.No }, bespeakDate);
                    int canbookCount = (int)((roomInfo.SeatList.Seats.Count - roomInfo.SeatList.Seats.Where(u => u.Value.IsSuspended).ToArray().Count()) * roomInfo.Setting.SeatBespeak.BespeakArea.Scale);
                    if (bespeaklogs.Count >= canbookCount)
                    {
                        result.Result = false;
                        result.Msg = "对不起，当前阅览室已经没有可预约的座位。";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }

                SeatManage.ClassModel.BespeakLogInfo bespeakInfo = new BespeakLogInfo();
                bespeakInfo.BsepeakState = EnumType.BookingStatus.Waiting;
                bespeakInfo.BsepeakTime = bespeakDate;
                bespeakInfo.CardNo = cardNo;
                bespeakInfo.ReadingRoomNo = roomNum;
                bespeakInfo.SubmitTime = DateTime.Now;
                bespeakInfo.SeatNo = seatNum;
                bespeakInfo.Remark = remark;

                if (seatDataService.AddBespeakLogInfo(bespeakInfo) != EnumType.HandleResult.Successed)
                {
                    result.Result = false;
                    result.Msg = "添加预约信息失败！";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                result.Result = true;
                result.Msg = string.Format("预约成功，请在{0}至{1}之间到图书馆刷卡确认。", bespeakDate.AddMinutes(-int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), bespeakDate.AddMinutes(int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("提交预约记录遇到异常：" + ex.Message);
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 提交时验证是否允许提交当提交的预约时间是否为自定义
        /// 如果提交预约当天的座位，需要验证预约时间是否闭馆。
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="roomNum"></param>
        /// <param name="seatNum"></param>
        /// <param name="bespeakDatetime">提交预约的时间，如果时间为开馆时间，该参数为null</param>
        /// <returns></returns>
        public string SubmitBespeakInfo(string cardNo, string roomNum, string seatNum, string bespeakDatetime, string remark)
        {
            try
            {
                JsonModel.JM_HandleResult result = new JsonModel.JM_HandleResult();
                UserInfo user = seatDataService.GetUserInfo(cardNo);
                if (user.UserType != EnumType.UserType.Reader)
                {
                    result.Result = false;
                    result.Msg = "对不起，您没有预约座位的权限。";
                }

                else
                {
                    //验证在选择的日期里是否有预约的座位
                    //TODO:验证时间日期是否合法。
                    //TODO:验证座位号是否正确
                    //验证阅览室是否开放预约
                    DateTime bespeakDate = DateTime.Parse(bespeakDatetime);

                    List<SeatManage.ClassModel.ReadingRoomInfo> roomInfos = seatDataService.GetReadingRoomInfo(new List<string>() { roomNum });
                    if (roomInfos.Count > 0 && !string.IsNullOrEmpty(checkBlacklist(cardNo, roomInfos[0])))
                    {
                        result.Result = false;
                        result.Msg = "对不起，您在黑名单中存在记录。";
                    }
                    else
                    {
                        List<BespeakLogInfo> bespeakLogs = seatDataService.GetBespeakLogInfo(cardNo, bespeakDate);//获取指定时间的预约信息
                        if (bespeakLogs.Count > 0)//如果存在预约信息，则不能再预约
                        {
                            result.Result = false;
                            result.Msg = string.Format("对不起，您已预约了[{0}] [{1}]座位。请在规定的时间内刷卡确认,或者取消该预约。", bespeakLogs[0].ReadingRoomName, bespeakLogs[0].ShortSeatNum);
                        }
                        else
                        {
                            if (roomInfos.Count > 0)
                            {
                                ClassModel.ReadingRoomInfo roomInfo = roomInfos[0];
                                if (roomInfo.Setting.SeatBespeak.Used)
                                {
                                    SeatManage.ClassModel.BespeakLogInfo bespeakInfo = new BespeakLogInfo();
                                    bespeakInfo.BsepeakState = EnumType.BookingStatus.Waiting;
                                    bespeakInfo.BsepeakTime = bespeakDate;
                                    bespeakInfo.CardNo = cardNo;
                                    bespeakInfo.ReadingRoomNo = roomNum;
                                    bespeakInfo.SubmitTime = DateTime.Now;
                                    bespeakInfo.SeatNo = seatNum;
                                    bespeakInfo.Remark = remark;
                                    if (bespeakDate.Date == DateTime.Now.Date)//如果启用预约，判断选择的日期是否为当天的日期
                                    {
                                        if (roomInfo.Setting.SeatBespeak.NowDayBespeak)
                                        {
                                            DateTime beginTime = DateTime.Parse(string.Format("{0} {1}", bespeakDate.ToShortDateString(), roomInfo.Setting.RoomOpenSet.DefaultOpenTime.BeginTime));
                                            DateTime endTime = DateTime.Parse(string.Format("{0} {1}", bespeakDate.ToShortDateString(), roomInfo.Setting.RoomOpenSet.DefaultOpenTime.EndTime));
                                            if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginTime, endTime, bespeakDate))//验证当天预约时间是否在开馆时间内
                                            {
                                                try
                                                {
                                                    if (seatDataService.AddBespeakLogInfo(bespeakInfo) == EnumType.HandleResult.Successed)
                                                    {
                                                        result.Result = true;
                                                        result.Msg = string.Format("预约成功，请在{0}点之前到图书馆刷卡确认。", bespeakDate.AddMinutes(roomInfo.Setting.SeatBespeak.SeatKeepTime));

                                                    }
                                                    else
                                                    {
                                                        result.Result = false;
                                                        result.Msg = "添加预约信息失败！";
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    result.Result = false;
                                                    result.Msg = "数据库错误！";
                                                }
                                            }
                                            else
                                            {
                                                result.Result = false;
                                                result.Msg = "您预约的时间没有开馆！";
                                            }
                                        }
                                        else
                                        {
                                            result.Result = false;
                                            result.Msg = "该阅览室不能预约当天座位";

                                        }
                                    }
                                    else
                                    {
                                        DateTime openTime = roomInfo.Setting.RoomOpenSet.DateOpenTime(bespeakDate); //获取指定日期的开馆时间

                                        if (bespeakDate.Date == DateTime.Now.Date)//如果预约时间为自定义时间
                                        {
                                            result.Result = false;
                                            result.Msg = "请使用SubmitBespeakInfoCustomTime方法提交定义时间的预约记录。";
                                        }
                                        else
                                        {
                                            if (dateBespeak(roomInfo.Setting, bespeakDate, DateTime.Now))//验证当前时间是否可与预约
                                            {
                                                if (timeCanBespeak(roomInfo.Setting.SeatBespeak, DateTime.Now))
                                                {   //不是自定义时间，则提交预约信息
                                                    if (seatDataService.AddBespeakLogInfo(bespeakInfo) == EnumType.HandleResult.Successed)
                                                    {
                                                        result.Result = true;
                                                        result.Msg = string.Format("预约成功，请在{0}至{1}之间到图书馆刷卡确认。", bespeakDate.AddMinutes(-int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), bespeakDate.AddMinutes(int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());

                                                    }
                                                    else
                                                    {
                                                        result.Result = false;
                                                        result.Msg = "添加预约信息失败！";
                                                    }
                                                }
                                                else
                                                {
                                                    result.Result = false;
                                                    result.Msg = string.Format("预约时间为：{0}至{1}", roomInfo.Setting.SeatBespeak.CanBespeatTimeSpace.BeginTime, roomInfo.Setting.SeatBespeak.CanBespeatTimeSpace.EndTime);

                                                }

                                            }
                                            else
                                            {
                                                result.Result = false;
                                                result.Msg = "选择的日期不能预约座位";
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    result.Result = false;
                                    result.Msg = "提交失败，阅览室未开启预约功能";
                                }
                            }
                            else
                            {
                                result.Result = false;
                                result.Msg = "阅览室编号错误";

                            }
                        }
                    }
                }
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }




        /// <summary>
        /// 判断选择的日期是否可以预约，false为不可预约
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private bool dateBespeak(SeatManage.ClassModel.ReadingRoomSetting set, DateTime bespeakDate, DateTime nowDate)
        {
            DateTime selectedDate = bespeakDate;
            if (selectedDate.Date == DateTime.Now.Date)
            {
                ReadingRoomStatus roomStatus = SeatManage.ClassModel.ReadingRoomSetting.ReadingRoomOpenState(set.RoomOpenSet, selectedDate);
                if (roomStatus == ReadingRoomStatus.Close || roomStatus == ReadingRoomStatus.BeforeClose)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            for (int i = 0; i < set.SeatBespeak.NoBespeakDates.Count; i++)
            {
                try
                {
                    DateTime beginDate = DateTime.Parse(set.SeatBespeak.NoBespeakDates[i].BeginTime);
                    DateTime endDate = DateTime.Parse(set.SeatBespeak.NoBespeakDates[i].EndTime);

                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginDate, endDate, selectedDate) || selectedDate.CompareTo(beginDate) == 0 || selectedDate.CompareTo(endDate) == 0)
                    {
                        //如果当前时间符合某个不可预约的规则，则直接返回false，不可预约
                        return false;
                    }

                }
                catch
                {//日期转换遇到异常，则忽略 
                }
            }
            //判断当天是否大于选择的日期
            TimeSpan span = selectedDate.Date - nowDate.Date;
            if (span.Days > set.SeatBespeak.BespeakBeforeDays)
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
        /// <summary>
        /// 检查是否被加入黑名单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns>如果为空，则验证通过，否则验证不通过，返回结果为黑名单信息</returns>
        private string checkBlacklist(string cardNo, string roomNum)
        {
            List<ReadingRoomInfo> rooms = seatDataService.GetReadingRooms(new List<string>() { roomNum }, null, null);
            List<BlackListInfo> blacklist = seatDataService.GetBlacklistInfo(cardNo);
            string result = "";
            if (rooms.Count > 0 && rooms[0].Setting.UsedBlacklistLimit && blacklist.Count > 0)
            {
                if (rooms[0].Setting.BlackListSetting.Used)
                {
                    foreach (BlackListInfo blinfo in blacklist)
                    {
                        if (blinfo.ReadingRoomID == roomNum)
                        {
                            result = blacklist[0].ReMark;
                            break;
                        }
                    }
                }
                else
                {
                    result = blacklist[0].ReMark;
                }
            }
            return result;

        }
        private string checkBlacklist(string cardNo, ReadingRoomInfo room)
        {
            List<BlackListInfo> blacklist = seatDataService.GetBlacklistInfo(cardNo);
            string result = "";
            if (room.Setting.UsedBlacklistLimit && blacklist.Count > 0)
            {
                if (room.Setting.BlackListSetting.Used)
                {
                    foreach (BlackListInfo blinfo in blacklist)
                    {
                        if (blinfo.ReadingRoomID == room.No)
                        {
                            result = blacklist[0].ReMark;
                            break;
                        }
                    }
                }
                else
                {
                    result = blacklist[0].ReMark;
                }
            }
            return result;

        }

        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId">预约记录的ID</param>
        /// <returns></returns>
        public string CancelBespeakLog(int bespeakId, string remark)
        {
            try
            {
                SeatManage.ClassModel.BespeakLogInfo model = seatDataService.GetBespeaklogById(bespeakId);
                JsonModel.JM_HandleResult returnValue = new JsonModel.JM_HandleResult();
                if (model != null)
                {
                    model.BsepeakState = SeatManage.EnumType.BookingStatus.Cencaled;
                    model.CancelPerson = SeatManage.EnumType.Operation.Reader;
                    model.CancelTime = DateTime.Now;
                    model.Remark = remark;
                    int result = seatDataService.UpdateBespeakLogInfo(model);
                    if (result > 0)
                    {
                        returnValue.Result = true;
                        returnValue.Msg = "取消预约成功";
                    }
                    else
                    {
                        returnValue.Result = false;
                        returnValue.Msg = "未知原因取消失败";
                    }
                }
                else
                {
                    returnValue.Result = false;
                    returnValue.Msg = "记录不存在。";
                }

                return SeatManageComm.JSONSerializer.Serialize(returnValue);
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 根据学号和日期取消预约
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="bespeakDate">预约日期</param>
        /// <returns></returns>
        public string CancelBespeakLogByCardNo(string cardNum, string bespeakDate)
        {
            List<BespeakLogInfo> bespeaks = seatDataService.GetBespeakLogInfo(cardNum, DateTime.Parse(bespeakDate));
            JM_HandleResult result = new JM_HandleResult();
            if (bespeaks.Count < 1)
            {
                result.Result = false;
                result.Msg = "没有预约记录。";
                return SeatManageComm.JSONSerializer.Serialize(result);
            }
            else
            {
                BespeakLogInfo model = bespeaks[0];
                model.BsepeakState = SeatManage.EnumType.BookingStatus.Cencaled;
                model.CancelPerson = SeatManage.EnumType.Operation.Reader;
                model.CancelTime = DateTime.Now;
                model.Remark = "取消预约";
                int i = seatDataService.UpdateBespeakLogInfo(model);
                if (i > 0)
                {
                    result.Result = true;
                    result.Msg = "取消预约成功";
                }
                else
                {
                    result.Result = false;
                    result.Msg = "未知原因取消失败";
                }
            }
            return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
        }

        public string GetReaderActualTimeBespeakRecord(string cardNum)
        {
            throw new NotSupportedException();
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="readingRoom"></param>
        /// <returns>执行结果</returns>
        public string ChangeSeat(string cardNo, string seatNo, string readingRoom)
        {
            try
            {
                //验证读者是否可以选择座位
                JM_HandleResult result = new JM_HandleResult();
                result.Result = true;
                result.Msg = "";
                if (string.IsNullOrEmpty(cardNo))
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "读者的学号不能为空";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatNo.Length != 9)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位编号不正确";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                string roomNo = seatNo.Substring(0, 6);
                result = SeatManageComm.JSONSerializer.Deserialize<JM_HandleResult>(VerifyCanDoIt(cardNo, roomNo));
                if (!result.Result)
                {
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }

                //TODO:验证座位是否被使用
                //TODO:验证用户当前状态是否为有座位
                //TODO:验证用户是否有未确认的预约
                SeatManage.ClassModel.Seat seatInfo = seatDataService.GetSeatInfoBySeatNum(seatNo);
                if (seatInfo == null)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "此座位不存在";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatInfo.IsSuspended)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位已停用";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (seatInfo.SeatUsedState != EnterOutLogType.Leave)
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位正在被使用";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> bespeaklogList = seatDataService.GetBespeakLogInfoBySeatNo(seatNo, DateTime.Now);
                if (seatInfo.SeatUsedState == EnterOutLogType.Leave && bespeaklogList.Count > 0)
                {
                    if (!seatInfo.ReadingRoom.Setting.SeatBespeak.SelectBespeakSeat)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    if (bespeaklogList[0].BsepeakTime == bespeaklogList[0].SubmitTime)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    if (bespeaklogList[0].BsepeakTime.AddMinutes(-double.Parse(seatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= DateTime.Now)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "此座位座位已被预约";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }

                EnterOutLogInfo enterOutlog = seatDataService.GetEnterOutLogInfoByCardNo(cardNo);
                if (enterOutlog == null || enterOutlog.EnterOutState == EnterOutLogType.Leave)
                {
                    if (seatDataService.GetSingleBespeakLogForWait(cardNo) != null)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "您有等待签到的预约记录。";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    if (seatDataService.GetWaitLogList(cardNo, null, null, null, new List<EnterOutLogType>() { EnterOutLogType.Waiting }).Count > 0)
                    {
                        result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "您有正在等待的座位。";
                        return SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "请先选择一个座位。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                EnterOutLogInfo newlog = enterOutlog;
                newlog.ReadingRoomNo = seatInfo.ReadingRoomNum;
                newlog.Remark = "通过移动客户端更换到该座位";
                newlog.SeatNo = seatInfo.SeatNo;
                newlog.Flag = EnumType.Operation.Reader;
                newlog.EnterOutType = EnumType.LogStatus.Valid;
                newlog.EnterOutState = EnumType.EnterOutLogType.ReselectSeat;
                newlog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
                int newLogId = -1;
                if (seatDataService.AddEnterOutLogInfo(newlog, ref newLogId) == HandleResult.Successed)
                {
                    result = new JM_HandleResult();
                    result.Result = true;
                    result.Msg = "座位更换成功。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "座位更换失败。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("更换座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }

        /// <summary>
        /// 获取扫码后的信息
        /// </summary>
        /// <param name="scanResult"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string GetScanCodeSeatInfo(string scanResult, string cardNo)
        {
            try
            {
                JM_ScanResult handleResult = null;
                string[] scanResultArray = scanResult.Split('?');
                ScanCodeParamModel scancode = null;
                if (scanResultArray.Length >= 2)
                {
                    string[] strParam = scanResultArray[1].Split('=');
                    if (strParam.Length >= 2)
                    {
                        scancode = ScanCodeParamModel.Prase(strParam[1]);//兼容url的二维码。 
                    }
                }
                else
                {
                    scancode = ScanCodeParamModel.Prase(scanResult);
                }
                if (scancode != null)
                {
                    handleResult = new JM_ScanResult();
                    SeatManage.ClassModel.BespeakSeatModel.ScanCodeViewModel scanResultModel = new ClassModel.BespeakSeatModel.ScanCodeViewModel();
                    List<BespeakLogInfo> bespeaks = seatDataService.GetBespeakLogInfo(cardNo, DateTime.Now);
                    if (bespeaks.Count > 0)
                    { //说明有需要处理的预约记录

                        handleResult.IsHandleLog = true;
                        if (bespeaks[0].SeatNo.Trim() == scancode.SeatNum.Trim() && bespeaks[0].ReadingRoomNo.Trim() == scancode.ReadingRoomNum.Trim())
                        {
                            handleResult.LogHandleResult = HandleBespeakLog(bespeaks[0]);
                        }
                        else
                        {
                            handleResult.LogHandleResult = new JM_HandleResult();
                            handleResult.LogHandleResult.Result = false;
                            handleResult.LogHandleResult.Msg = string.Format("您预约了{0} {1}座位，请直接扫描该座位上的二维码确认入座。", bespeaks[0].ReadingRoomName, bespeaks[0].ShortSeatNum);
                        }
                    }
                    else
                    {
                        //如果没有要处理的预约记录，则判断是否存在有要处理的暂离记录。
                        EnterOutLogInfo log = seatDataService.GetEnterOutLogInfoByCardNo(cardNo);
                        if (log.EnterOutState == EnterOutLogType.ShortLeave && log.SeatNo == scancode.SeatNum && log.ReadingRoomNo == scancode.ReadingRoomNum)
                        {//暂离入座
                            handleResult.IsHandleLog = true;
                            handleResult.LogHandleResult = HandleEnterOutLog(log);
                        }
                    }
                    handleResult.ActualTimeRecords = getJM_BespeakLog(cardNo);
                    handleResult.SeatInfo = getSeatInfoBySeatNum(scancode.SeatNum, ref handleResult);
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(handleResult);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }



        #region 私有方法
        /// <summary>
        /// 通过获取座位相关信息，以及是否可以被预约、是否在被使用等
        /// </summary>
        /// <param name="seatNum"></param>
        /// <returns></returns>
        private JM_Seat getSeatInfoBySeatNum(string seatNum, ref JM_ScanResult handleResult)
        {
            SeatManage.ClassModel.Seat seatInfo = seatDataService.GetSeatInfoBySeatNum(seatNum);
            JM_Seat jm_seat = null;
            jm_seat = new JM_Seat();
            jm_seat.CanBeBespeak = seatInfo.CanBeBespeak;
            jm_seat.HavePower = seatInfo.HavePower;
            jm_seat.IsSuspended = seatInfo.IsSuspended;
            jm_seat.ReadingRoomNum = seatInfo.ReadingRoomNum;
            jm_seat.SeatNo = seatInfo.SeatNo;
            jm_seat.SeatUsedState = seatInfo.SeatUsedState.ToString();
            jm_seat.ShortSeatNo = seatInfo.ShortSeatNo;
            jm_seat.ReadingRoomName = seatInfo.ReadingRoom.Name;

            if (seatInfo.IsSuspended)
            {//如果座位暂停使用，则直接返回座位基础信息 
                return jm_seat;
            }
            //判断该座位是否可以被预约
            //1.判断阅览室是否开放预约、当前是否可以被预约
            //2.判断座位是否可以被预约
            if (seatInfo.ReadingRoom.Setting.SeatBespeak.Used && dateBespeak(seatInfo.ReadingRoom.Setting, DateTime.Now.AddDays(1), DateTime.Now))//验证阅览室是否开放预约
            {
                // jm_seat.Remark = string.Format("需要在{0}至{1}之间到图书馆刷卡确认。", DateTime.Now.AddMinutes(-int.Parse(seatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), DateTime.Now.AddMinutes(int.Parse(seatInfo.ReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());
                JM_OpenBespeakReadingRoom room = new JM_OpenBespeakReadingRoom();

                room.AreaName = seatInfo.ReadingRoom.Area.AreaName;
                room.LibraryName = seatInfo.ReadingRoom.Libaray.Name;
                room.RoomName = seatInfo.ReadingRoom.Name;
                room.RoomNum = seatInfo.ReadingRoom.No;
                room.SchoolName = seatInfo.ReadingRoom.Libaray.School.Name;
                room.DefaultBespeakTime = seatInfo.ReadingRoom.Setting.RoomOpenSet.DateOpenTime(DateTime.Now).ToString("HH:mm:ss");
                handleResult.Room = room;
                List<ClassModel.Seat> canBespeakSeat = new List<Seat>();
                List<string> roomNums = new List<string>();
                roomNums.Add(seatInfo.ReadingRoomNum);
                List<ReadingRoomInfo> rooms = seatDataService.GetReadingRooms(roomNums, null, null);
                if (rooms.Count > 0)
                {
                    if (!timeCanBespeak(rooms[0].Setting.SeatBespeak, DateTime.Now))
                    {
                        jm_seat.CanBeBespeak = false;
                    }
                    else if (seatInfo.ReadingRoom.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                    {
                        jm_seat.CanBeBespeak = true;
                    }
                }
            }
            else
            {
                jm_seat.CanBeBespeak = false;
            }
            return jm_seat;
        }

        private JM_ActualTimeRecords getJM_BespeakLog(string cardNo)
        {
            JM_ActualTimeRecordParam param = new JM_ActualTimeRecordParam();
            param.GetBespeakLog = true;
            param.GetEnterOutLog = true;
            param.GetBlackList = true;
            param.GetWaitLog = true;
            JM_ActualTimeRecords records = SeatManageComm.JSONSerializer.Deserialize<JM_ActualTimeRecords>(GetReaderActualTimeRecord(cardNo, SeatManageComm.JSONSerializer.Serialize(param)));
            return records;
        }
        /// <summary>
        /// 扫码时处理选座记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private JM_HandleResult HandleEnterOutLog(SeatManage.ClassModel.EnterOutLogInfo log)
        {
            JM_HandleResult result = new JM_HandleResult();
            log.EnterOutState = EnterOutLogType.ComeBack;
            log.Remark = "通过扫码暂离入座";
            int resultId = -1;
            try
            {
                seatDataService.AddEnterOutLogInfo(log, ref resultId);
                if (resultId != -1)
                {
                    result.Result = true;
                    result.Msg = "暂离入座成功";
                }
                else
                {
                    result.Result = false;
                    result.Msg = "暂离入座失败";
                }
            }
            catch
            {
                result.Result = false;
                result.Msg = "暂离入座失败";
            }
            return result;
        }
        /// <summary>
        /// 扫码时处理预约记录
        /// </summary>
        /// <param name="log"></param>
        /// <returns></returns>
        private JM_HandleResult HandleBespeakLog(BespeakLogInfo log)
        {
            JM_HandleResult logHandleResult = new JM_HandleResult();
            EnterOutLogInfo newEnterOutLog = new EnterOutLogInfo();//构造 
            newEnterOutLog.CardNo = log.CardNo;
            newEnterOutLog.EnterOutLogNo = SeatComm.RndNum();
            newEnterOutLog.EnterOutState = EnterOutLogType.BookingConfirmation;
            newEnterOutLog.EnterOutType = LogStatus.Valid;
            newEnterOutLog.Flag = Operation.Reader;
            newEnterOutLog.ReadingRoomNo = log.ReadingRoomNo;
            newEnterOutLog.SeatNo = log.SeatNo;
            newEnterOutLog.Remark = string.Format("通过扫码预约入座", log.ReadingRoomName, log.ShortSeatNum);
            int logid = -1;
            try
            {
                HandleResult result = seatDataService.AddEnterOutLogInfo(newEnterOutLog, ref logid); //添加入座记录
                if (result == HandleResult.Successed)
                {
                    log.BsepeakState = BookingStatus.Confinmed;
                    log.CancelPerson = Operation.Reader;
                    log.CancelTime = seatDataService.GetServerDateTime();
                    log.Remark = string.Format("通过扫码预约入座", log.ReadingRoomName, log.ShortSeatNum);
                    seatDataService.UpdateBespeakLogInfo(log);
                    logHandleResult.Result = true;
                    logHandleResult.Msg = "预约入座成功！";
                }
                else
                {
                    logHandleResult.Result = false;
                    logHandleResult.Msg = "预约入座失败.";

                }
            }
            catch (Exception ex)
            {
                logHandleResult.Result = false;
                logHandleResult.Msg = "未知原因，预约入座确认失败";
            }
            return logHandleResult;
        }

        #endregion




        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public string BespeakCheck(string cardNum)
        {
            try
            {
                JM_HandleResult result = new JM_HandleResult();
                DateTime nowDate = DateTime.Now;
                List<BespeakLogInfo> bespeaks = seatDataService.GetBespeakLogInfo(cardNum, nowDate);
                if (bespeaks.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "对不起，您没有等待签到的座位";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                BespeakLogInfo bespeaklog = bespeaks[0];
                List<ReadingRoomInfo> rooms = seatDataService.GetReadingRooms(new List<string>() { bespeaklog.ReadingRoomNo }, null, null);
                if (rooms.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室不存在。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                //判断阅览室是否开放
                if (ReadingRoomOpenState(rooms[0].Setting.RoomOpenSet, seatDataService.GetServerDateTime()) == ReadingRoomStatus.Close)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室尚未开放。";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }

                ReadingRoomSetting set = rooms[0].Setting;
                DateTime dtBegin = bespeaklog.BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd = bespeaklog.BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowDate) || (set.SeatBespeak.NowDayBespeak && bespeaklog.SubmitTime == bespeaklog.BsepeakTime))
                {
                    //TODO:预约时间在开始时间和结束时间之间，执行预约确认操作
                    //TODO:预约确认时，判断当前座位上是否有人。
                    EnterOutLogInfo seatUsedInfo = seatDataService.GetEnterOutLogInfoBySeatNum(bespeaklog.SeatNo);
                    if (seatUsedInfo != null && seatUsedInfo.EnterOutState != EnterOutLogType.Leave)
                    {
                        //条件满足，说明座位正在使用。
                        seatUsedInfo.EnterOutState = EnterOutLogType.Leave;
                        seatUsedInfo.EnterOutType = LogStatus.Valid;
                        seatUsedInfo.Remark = string.Format("预约该座位的读者签到入座，设置在座读者离开");
                        seatUsedInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        if (seatDataService.AddEnterOutLogInfo(seatUsedInfo, ref newId) == HandleResult.Successed)
                        {
                            List<WaitSeatLogInfo> waitInfoList = seatDataService.GetWaitLogList(null, seatUsedInfo.EnterOutLogID, null, null, null);
                            if (waitInfoList.Count > 0)
                            {
                                WaitSeatLogInfo waitSeatLogModel = waitInfoList[0];
                                waitSeatLogModel.OperateType = Operation.Reader;
                                waitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                                waitSeatLogModel.NowState = LogStatus.Valid;
                                if (!seatDataService.UpdateWaitLog(waitSeatLogModel))
                                {
                                    result.Result = false;
                                    result.Msg = "对不起，此阅览室尚未开放。";
                                    return SeatManageComm.JSONSerializer.Serialize(result);
                                }
                            }
                        }
                        else
                        {
                            result.Result = false;
                            result.Msg = "对不起，此阅览室尚未开放。";
                            return SeatManageComm.JSONSerializer.Serialize(result);
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
                    newEnterOutLog.Remark = string.Format("签到入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);

                    int logid = -1;
                    if (seatDataService.AddEnterOutLogInfo(newEnterOutLog, ref logid) == HandleResult.Successed)
                    {
                        bespeaklog.BsepeakState = BookingStatus.Confinmed;
                        bespeaklog.CancelPerson = Operation.Reader;
                        bespeaklog.CancelTime = nowDate;
                        bespeaklog.Remark = string.Format("签到入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                        seatDataService.UpdateBespeakLogInfo(bespeaklog);
                    }
                    result.Result = true;
                    result.Msg = newEnterOutLog.Remark;
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
                else
                {
                    result.Result = false;
                    result.Msg = "对不起，您预约的座位没有到达签到时间";
                    return SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("选择座位遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }


        /// <summary>
        /// 返回读者消息列表
        /// </summary>
        /// <param name="cardNum">学号</param>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">每页大小</param>
        /// <returns></returns>
        public string GetReaderNoticeList(string cardNum, int pageIndex, int pageSize)
        {
            List<ReaderNoticeInfo> list = seatDataService.GetReaderNoticeList(cardNum, pageIndex, pageSize);
            List<JM_ReaderNotice> jm_noticeList = new List<JM_ReaderNotice>();
            for (int i = 0; i < list.Count; i++)
            {
                JM_ReaderNotice jm_notice = new JM_ReaderNotice();
                jm_notice.AddTime = list[i].AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                jm_notice.CardNo = list[i].CardNo;
                jm_notice.SchoolNum = schoolNum;
                switch (list[i].IsRead)
                {
                    case LogStatus.Fail:
                        jm_notice.IsRead = false;
                        break;
                    case LogStatus.Valid:
                        jm_notice.IsRead = true;
                        break;
                }
                jm_notice.Note = list[i].Note;
                jm_notice.NoticeID = list[i].NoticeID;
                jm_notice.NoticeTitle = NoticeTypeValue.valueOf(list[i].Type);
                jm_noticeList.Add(jm_notice);
            }
            return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_noticeList);
        }

        /// <summary>
        /// 获取读者座位状态
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public string GetReaderSeatState(string cardNum)
        {
            SeatManage.ClassModel.EnterOutLogInfo enterOutLog = seatDataService.GetEnterOutLogInfoByCardNo(cardNum);
            JM_ReaderUsedSeatStateInfo readerstate = new JM_ReaderUsedSeatStateInfo();
            if (enterOutLog != null)
            {
                readerstate.SeatNo = enterOutLog.ShortSeatNo;
                readerstate.ReadingRoomName = enterOutLog.ReadingRoomName;

                List<ReadingRoomInfo> rooms = seatDataService.GetReadingRoomInfo(new List<string> { enterOutLog.ReadingRoomNo });

                ReadingRoomSetting set = rooms[0].Setting;

                TimeSpan ts = DateTime.Now - enterOutLog.EnterOutTime; //当前时间和进出记录时间的间隔。。
                switch (enterOutLog.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                        readerstate.State = JM_ReaderUsedSeatStateInfo.AtSeat;

                        readerstate.UsedTimeLength = ts.TotalSeconds;
                        break;
                    case EnterOutLogType.ShortLeave:
                        readerstate.State = JM_ReaderUsedSeatStateInfo.ShortLeave;
                        readerstate.ShortLeaveTimeLength = ReadingRoomSetting.GetSeatHoldTime(set.SeatHoldTime, enterOutLog.EnterOutTime) * 60 - ts.TotalSeconds;

                        break;
                    case EnterOutLogType.Leave:
                        readerstate.State = JM_ReaderUsedSeatStateInfo.Leave;
                        return SeatManageComm.JSONSerializer.Serialize(readerstate);//赋值完成，直接返回。
                }

                /******给CanUsedTimeLength（座位可用时长）属性赋值******/
                readerstate.BeginTime = enterOutLog.EnterOutTime.ToString("yyyy-MM-dd HH:mm:ss");
                readerstate.IsSeatUsedTimeLimit = set.SeatUsedTimeLimit.Used;
                if (readerstate.IsSeatUsedTimeLimit)//启用座位使用时长限制
                {
                    if (set.SeatUsedTimeLimit.Mode == "Fixed")
                    {
                        List<DateTime> times = set.SeatUsedTimeLimit.FixedTimes.OrderBy(time => time).ToList();//对时间进行升序排列
                        DateTime now = DateTime.Now;
                        for (int i = 0; i < times.Count; i++)
                        {
                            if (now < times[i])
                            {
                                ts = times[i] - now;
                                readerstate.CanUsedTimeLength = ts.TotalSeconds;
                                return SeatManageComm.JSONSerializer.Serialize(readerstate);//赋值完成，直接返回。
                            }
                        }
                        //如果执行到这里，说明当前时间不在以上区间，在闭馆时间之前，则可用到闭馆时间。
                        DateTime closeTime = set.NowCloseTime(now);
                        readerstate.CanUsedTimeLength = (closeTime - now).TotalSeconds;
                        return SeatManageComm.JSONSerializer.Serialize(readerstate);//返回。

                    }
                    else
                    {
                        switch (enterOutLog.EnterOutState)
                        {//当前记录为续时或者暂离回来，则座位使用剩余时间使用 阅览室设置里的续时后使用时长来处理。
                            case EnterOutLogType.ContinuedTime:
                            case EnterOutLogType.ComeBack:
                                readerstate.CanUsedTimeLength = set.SeatUsedTimeLimit.DelayTimeLength * 60 - ts.TotalSeconds;
                                break;
                            case EnterOutLogType.SelectSeat:
                            case EnterOutLogType.ReselectSeat:
                            case EnterOutLogType.WaitingSuccess:
                                //当前记录为选座、重新选座、等待入座，则座位剩余使用时间为选座后使用时长来处理
                                ts = DateTime.Now - enterOutLog.EnterOutTime;
                                readerstate.CanUsedTimeLength = set.SeatUsedTimeLimit.UsedTimeLength * 60 - ts.TotalSeconds;
                                break;
                        }
                    }
                }
                else
                {
                    readerstate.CanUsedTimeLength = null;
                }

            }
            else
            {
                readerstate.State = JM_ReaderUsedSeatStateInfo.Leave;
            }
            return SeatManageComm.JSONSerializer.Serialize(readerstate);
        }


    }
}
