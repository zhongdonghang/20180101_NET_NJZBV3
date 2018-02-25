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
        SeatManageDateService seatManageDateService = new SeatManageDateService();
        /// <summary>
        /// 提交预约座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="studentNo"></param>
        /// <param name="besapeakTime"></param>
        /// <param name="isNowBesapeak"></param>
        /// <returns></returns>
        public string SubmitBesapeskSeat(string seatNo, string roomNo, string studentNo, string besapeakTime, bool isNowBesapeak)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                //UserInfo user = SeatManageDateService.GetUserInfo(studentNo);
                //if (user == null)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，您的账号不存在，请先去触摸屏终端进行预约激活。";
                //    return JSONSerializer.Serialize(result);
                //}
                //if (user.UserType != UserType.Reader)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，您没有预约座位的权限。";
                //    return JSONSerializer.Serialize(result);
                //}
                //验证读者当前是否有座位
                

                if (string.IsNullOrEmpty(roomNo) || (string.IsNullOrEmpty(besapeakTime) && isNowBesapeak))
                {
                    result.Result = false;
                    result.Msg = "阅览室编号或日期不能为空!";
                    return JSONSerializer.Serialize(result);
                }
                Seat seatInfo = seatManageDateService.GetSeatInfoBySeatNum(seatNo);
                if (seatInfo == null)
                {
                    result.Result = false;
                    result.Msg = "对不起此座位不存在!";
                    return JSONSerializer.Serialize(result);
                }
                if (seatInfo.IsSuspended)
                {
                    result.Result = false;
                    result.Msg = "对不起此座位已停用!";
                    return JSONSerializer.Serialize(result);
                }

                DateTime bespeakDate;
                if (!isNowBesapeak)
                {
                    if (!DateTime.TryParse(besapeakTime, out bespeakDate))
                    {
                        result.Result = false;
                        result.Msg = "日期格式不正确!";
                        return JSONSerializer.Serialize(result);
                    }
                }
                else
                {
                    bespeakDate = DateTime.Now;
                }
                if (bespeakDate.Date < DateTime.Now.Date)
                {
                    result.Result = false;
                    result.Msg = "对不起查询日期不得小于当天时间!";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date)
                {
                    EnterOutLogInfo enterOutLog = seatManageDateService.GetEnterOutLogInfoByCardNo(studentNo);
                    if (enterOutLog != null && enterOutLog.EnterOutState != EnterOutLogType.Leave)
                    {
                        result.Result = false;
                        result.Msg = "对不起，您当前已经有座位。";
                        return JSONSerializer.Serialize(result);
                    }
                    //验证读者是否在等待座位
                    List<EnterOutLogType> logType = new List<EnterOutLogType>();
                    logType.Add(EnterOutLogType.Waiting);
                    List<WaitSeatLogInfo> waitSeatlogs = seatManageDateService.GetWaitLogList(studentNo, null, null, null, logType);
                    if (waitSeatlogs.Count > 0)
                    {
                        result.Result = false;
                        result.Msg = "对不起，您当前在等待座位。";
                        return JSONSerializer.Serialize(result);
                    }
                }

                List<ReadingRoomInfo> roomInfos = seatManageDateService.GetReadingRoomInfo(new List<string> { roomNo });
                if (roomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不存在!";
                    return JSONSerializer.Serialize(result);
                }
                if (!roomInfos[0].Setting.SeatBespeak.Used)
                {
                    result.Result = false;
                    result.Msg = "当前阅览室不提供预约!";
                    return JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date - DateTime.Now.Date).Days > roomInfos[0].Setting.SeatBespeak.BespeakBeforeDays)
                {
                    result.Result = false;
                    result.Msg = "对不起您选择的日期不开放预约!";
                    return JSONSerializer.Serialize(result);
                }
                if ((bespeakDate.Date == DateTime.Now.Date) && !roomInfos[0].Setting.SeatBespeak.NowDayBespeak)
                {
                    result.Result = false;
                    result.Msg = "对不起此阅览室不提供当天预约功能!";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date != DateTime.Now.Date && !roomInfos[0].Setting.IsCanBespeakSeat(bespeakDate))
                {
                    result.Result = false;
                    result.Msg = "对不起当前日期或时间段暂不开放预约!";
                    return JSONSerializer.Serialize(result);
                }
                if (!string.IsNullOrEmpty(checkBlacklist(studentNo, roomInfos[0])))
                {
                    result.Result = false;
                    result.Msg = "对不起，您在黑名单中存在记录。";
                    return JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> bespeakLogs = seatManageDateService.GetBespeakLogInfo(studentNo, bespeakDate);//获取指定时间的预约信息
                if (bespeakLogs.Count > 0)//如果存在预约信息，则不能再预约
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，您已预约了[{0}] [{1}]座位。请在规定的时间内刷卡确认,或者取消该预约。", bespeakLogs[0].ReadingRoomName, bespeakLogs[0].ShortSeatNum);
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomInfo roomInfo = roomInfos[0];
                //if (roomInfo.Setting.ReadingRoomOpenState(bespeakDate) == ReadingRoomStatus.Close)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，您预约的时间阅览室处于关闭状态。";
                //    return JSONSerializer.Serialize(result);
                //}
                List<BespeakLogInfo> seatbespeakLog = seatManageDateService.GetBespeakLogInfoBySeatNo(seatNo, bespeakDate);
                if (seatbespeakLog.Count > 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，此座位在您选择的时间段已被预约。";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && seatInfo.SeatUsedState != EnterOutLogType.Leave)//如果启用预约，判断选择的日期是否为当天的日期
                {
                    result.Result = false;
                    result.Msg = "对不起，此座位正在被使用。";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && seatManageDateService.SeatLocked(seatNo) != SeatLockState.Locked)
                {
                    result.Result = false;
                    result.Msg = "对不起，此座位正在被其他人操作。";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakDate.Date == DateTime.Now.Date && roomInfo.Setting.PosTimes.IsUsed && seatManageDateService.GetReaderChooseSeatTimes(studentNo, roomInfo.Setting.PosTimes.Minutes) >= roomInfo.Setting.PosTimes.Times)
                {
                    result.Result = false;
                    result.Msg = "对不起您的选座/预约次数过于频繁，请稍后重试。";
                    return JSONSerializer.Serialize(result);
                }
                //判断读者类型
                ReaderInfo reader = seatManageDateService.GetReader(studentNo, false);
                if (reader == null)
                {
                    reader = new ReaderInfo();
                    reader.CardNo = studentNo;
                    reader.ReaderType = "未指定";
                }
                if (!ProvenReaderType(reader, roomInfo.Setting))
                {
                    result.Result = false;
                    result.Msg = "对不起，您的用户类型'" + reader.ReaderType + "'不允许在此阅览室预约。";
                    return JSONSerializer.Serialize(result);
                }
                if (roomInfo.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                {
                    List<BespeakLogInfo> bespeaklogs = seatManageDateService.GetBespeakLogInfoByRoomsNum(new List<string>() { roomInfo.No }, bespeakDate);
                    int canbookCount = (int)((roomInfo.SeatList.Seats.Count - roomInfo.SeatList.Seats.Where(u => u.Value.IsSuspended).ToArray().Count()) * roomInfo.Setting.SeatBespeak.BespeakArea.Scale);
                    if (bespeaklogs.Count >= canbookCount)
                    {
                        result.Result = false;
                        result.Msg = "对不起，已经没有可预约的座位了。";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }
                if (bespeakDate < DateTime.Now && !isNowBesapeak)
                {
                    result.Result = false;
                    result.Msg = "对不起，当前阅览室已经没有可预约的座位。";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                BespeakLogInfo bespeakInfo = new BespeakLogInfo();
                bespeakInfo.BsepeakState = BookingStatus.Waiting;
                bespeakInfo.SubmitTime = DateTime.Now;
                bespeakInfo.BsepeakTime = bespeakDate;
                if (isNowBesapeak)
                {
                    bespeakInfo.BsepeakTime = bespeakInfo.SubmitTime;
                }
                bespeakInfo.CardNo = studentNo;
                bespeakInfo.ReadingRoomNo = roomNo;
                bespeakInfo.SeatNo = seatNo;
                if (isNowBesapeak)
                {
                    bespeakInfo.Remark = string.Format("您在移动客户端预约{0} 在{1} {2}号座位，请在{3}至{4}之间到图书馆刷卡确认。", bespeakDate, roomInfo.Name, seatInfo.ShortSeatNo, bespeakDate.ToShortTimeString(), bespeakDate.AddMinutes(roomInfo.Setting.SeatBespeak.SeatKeepTime).ToShortTimeString());
                }
                else
                {
                    bespeakInfo.Remark = string.Format("您在移动客户端预约{0} 在{1} {2}号座位，请在{3}至{4}之间到图书馆刷卡确认。", bespeakDate, roomInfo.Name, seatInfo.ShortSeatNo, bespeakDate.AddMinutes(-int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString(), bespeakDate.AddMinutes(int.Parse(roomInfo.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString());
                }
                if (seatManageDateService.AddBespeakLogInfo(bespeakInfo) != HandleResult.Successed)
                {
                    result.Result = false;
                    result.Msg = "添加预约信息失败！";
                    return JSONSerializer.Serialize(result);
                }
                seatManageDateService.SeatUnLocked(seatNo);
                result.Result = true;
                result.Msg = bespeakInfo.Remark;
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("预约座位遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 取消预约
        /// </summary>
        /// <param name="bespeakId"></param>
        /// <returns></returns>
        public string CancelBesapeak(int bespeakId)
        {
            try
            {
                BespeakLogInfo model = seatManageDateService.GetBespeaklogById(bespeakId);
                AJM_HandleResult returnValue = new AJM_HandleResult();
                if (model != null)
                {
                    model.BsepeakState = BookingStatus.Cencaled;
                    model.CancelPerson = Operation.Reader;
                    model.CancelTime = DateTime.Now;
                    model.Remark = string.Format("您在移动终端上，取消{0} {1}号座位，在{2}的预约。", model.ReadingRoomName, model.ShortSeatNum, model.BsepeakTime);
                    if (seatManageDateService.UpdateBespeakLogInfo(model) > 0)
                    {
                        returnValue.Result = true;
                        returnValue.Msg = model.Remark;
                        return JSONSerializer.Serialize(returnValue);
                    }
                    returnValue.Result = false;
                    returnValue.Msg = "对不起，取消预约失败";
                    return JSONSerializer.Serialize(returnValue);
                }
                returnValue.Result = false;
                returnValue.Msg = "对不起，记录不存在。";
                return JSONSerializer.Serialize(returnValue);
            }
            catch (Exception ex)
            {
                WriteLog.Write("取消预约遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据学号和日期取消预约
        /// </summary>
        /// <param name="studentNo">学号</param>
        /// <param name="bespeakDate">预约日期</param>
        /// <returns></returns>
        public string CancelBespeakLogByCardNo(string studentNo, string bespeakDate)
        {
            List<BespeakLogInfo> bespeaks = seatManageDateService.GetBespeakLogInfo(studentNo, DateTime.Parse(bespeakDate));
            AJM_HandleResult result = new AJM_HandleResult();
            if (bespeaks.Count < 1)
            {
                result.Result = false;
                result.Msg = "没有预约记录。";
                return JSONSerializer.Serialize(result);
            }

            BespeakLogInfo model = bespeaks[0];
            model.BsepeakState = BookingStatus.Cencaled;
            model.CancelPerson = Operation.Reader;
            model.CancelTime = DateTime.Now;
            model.Remark = "取消预约";
            int i = seatManageDateService.UpdateBespeakLogInfo(model);
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
            return JSONSerializer.Serialize(result);
        }
        /// <summary>
        /// 暂离
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ShortLeave(string studentNo)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "读者学号为空。";
                    return JSONSerializer.Serialize(result);
                }
                ReaderInfo reader = seatManageDateService.GetReader(studentNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "获取读者信息失败。";
                    return JSONSerializer.Serialize(result);
                }
                if (reader.EnterOutLog == null)
                {
                    result.Result = false;
                    result.Msg = "您还没有选座。";
                    return JSONSerializer.Serialize(result);
                }
                switch (reader.EnterOutLog.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation://预约入座
                    case EnterOutLogType.SelectSeat:    //选座
                    case EnterOutLogType.ContinuedTime: //续时
                    case EnterOutLogType.ComeBack:      //暂离回来
                    case EnterOutLogType.ReselectSeat:  //重新选座
                    case EnterOutLogType.WaitingSuccess: //等待入座
                        reader.EnterOutLog.EnterOutState = EnterOutLogType.ShortLeave;
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;
                        reader.EnterOutLog.Remark = string.Format("您在移动终端设置{0} {1}号座位暂离，暂离暂离时间{2}，请在{3}之前返回。", reader.AtReadingRoom.Name, reader.EnterOutLog.ShortSeatNo, reader.AtReadingRoom.Setting.GetSeatHoldTime(reader.EnterOutLog.EnterOutTime), reader.EnterOutLog.EnterOutTime.AddMinutes(reader.AtReadingRoom.Setting.GetSeatHoldTime(reader.EnterOutLog.EnterOutTime)).ToShortTimeString());

                        int newId = -1;
                        if (seatManageDateService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId) == HandleResult.Successed)
                        {
                            result.Result = true;
                            result.Msg = reader.EnterOutLog.Remark;
                        }
                        else
                        {
                            result.Result = false;
                            result.Msg = "对不起，暂离失败。";
                        }
                        break;
                    case EnterOutLogType.ShortLeave:
                        result.Result = false;
                        result.Msg = "对不起，您已经是暂离状态。";
                        break;
                    case EnterOutLogType.Leave:
                        result.Result = false;
                        result.Msg = "对不起，您还没有座位，请先选择一个座位。";
                        break;
                    default:
                        result.Result = false;
                        result.Msg = "对不起，您当前没有座位，请先选择一个座位。";
                        break;
                }
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("读者暂离遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ReleaseSeat(string studentNo)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                ReaderInfo reader = seatManageDateService.GetReader(studentNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "获取读者信息失败。";
                    return JSONSerializer.Serialize(result);
                }
                if (reader.EnterOutLog == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，您还没有座位，请先选择一个座位。";
                    return JSONSerializer.Serialize(result);
                }
                switch (reader.EnterOutLog.EnterOutState)
                {
                    case EnterOutLogType.BookingConfirmation://预约入座
                    case EnterOutLogType.SelectSeat:    //选座
                    case EnterOutLogType.ContinuedTime: //续时
                    case EnterOutLogType.ComeBack:      //暂离回来
                    case EnterOutLogType.ReselectSeat:  //重新选座
                    case EnterOutLogType.WaitingSuccess: //读者通过等待座位入座
                    case EnterOutLogType.ShortLeave:    //读者暂离 
                        reader.EnterOutLog.EnterOutState = EnterOutLogType.Leave;
                        reader.EnterOutLog.Remark = string.Format("您在移动终端释放{0} {1}号座位，感谢您的使用。", reader.AtReadingRoom.Name, reader.EnterOutLog.ShortSeatNo);
                        reader.EnterOutLog.Flag = Operation.Reader;
                        reader.EnterOutLog.EnterOutTime = DateTime.Now;

                        int newId = -1;
                        HandleResult returnResult = seatManageDateService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId);
                        if (returnResult == HandleResult.Successed)
                        {
                            result.Result = true;
                            result.Msg = reader.EnterOutLog.Remark;
                            return JSONSerializer.Serialize(result);
                        }
                        else
                        {
                            result.Result = false;
                            result.Msg = "对不起，释放座位失败";
                            return JSONSerializer.Serialize(result);
                        }
                    default:
                        result.Result = false;
                        result.Msg = "对不起，您还没有座位，请先选择一个座位。";
                        return JSONSerializer.Serialize(result);
                }


            }
            catch (Exception ex)
            {
                WriteLog.Write("释放座位遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 取消等待
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string CancelWait(string studentNo)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                ReaderInfo reader = seatManageDateService.GetReader(studentNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = "获取读者信息失败。";
                    return JSONSerializer.Serialize(result);
                }
                if (reader.WaitSeatLog == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，您没有等待的座位。";
                    return JSONSerializer.Serialize(result);
                }
                //处理等待记录的Id
                reader.WaitSeatLog.OperateType = Operation.Reader;
                reader.WaitSeatLog.WaitingState = EnterOutLogType.WaitingCancel;
                reader.WaitSeatLog.NowState = LogStatus.Valid;
                if (seatManageDateService.UpdateWaitLog(reader.WaitSeatLog))
                {  //恢复读者的在座状态
                    EnterOutLogInfo enterOutlog = seatManageDateService.GetEnterOutLogInfoById(reader.WaitSeatLog.EnterOutLogID);
                    TimeSpan shortleavetimelong = DateTime.Now - enterOutlog.EnterOutTime;
                    enterOutlog.EnterOutState = EnterOutLogType.ComeBack;
                    enterOutlog.EnterOutType = LogStatus.Valid;
                    enterOutlog.Flag = Operation.OtherReader;
                    enterOutlog.Remark = string.Format("读者{0}在移动终端取消等待您的{1} {2}号座位，您暂离{3}分钟后恢复为在座状态。", reader.WaitSeatLog.CardNo, enterOutlog.ReadingRoomName, enterOutlog.ShortSeatNo, shortleavetimelong.Minutes);
                    int newId = -1;
                    if (seatManageDateService.AddEnterOutLogInfo(enterOutlog, ref newId) == HandleResult.Successed)
                    {
                        result.Result = true;
                        result.Msg = string.Format("您{0}在移动终端取消等待{1} {2}号座位，本次等待时间为{3}分钟。", reader.WaitSeatLog.CardNo, enterOutlog.ReadingRoomName, enterOutlog.ShortSeatNo, shortleavetimelong.Minutes);
                        return JSONSerializer.Serialize(result);
                    }
                    else
                    {
                        result.Result = false;
                        result.Msg = "对不起，恢复原读者在座失败！";
                        return JSONSerializer.Serialize(result);
                    }
                }
                else
                {
                    result.Result = false;
                    result.Msg = "对不起，取消等待失败！";
                    return JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("取消等待遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 更换座位
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <param name="cardNo"></param>
        /// <param name="schoolNo"></param>
        /// <returns></returns>
        public string ChangeSeat(string seatNo, string roomNo, string cardNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                if (string.IsNullOrEmpty(cardNo))
                {
                    result.Result = false;
                    result.Msg = "读者学号不能为空！";
                    return JSONSerializer.Serialize(result);
                }
                if (seatNo.Length < 9)
                {
                    result.Result = false;
                    result.Msg = "座位编号不正确";
                    return JSONSerializer.Serialize(result);
                }
                result = JSONSerializer.Deserialize<AJM_HandleResult>(VerifyCanDoIt(cardNo, roomNo));
                if (!result.Result)
                {
                    return JSONSerializer.Serialize(result);
                }
                //TODO:验证座位是否被使用
                //TODO:验证用户当前状态是够为在座
                //TODO:验证用户是否有未确认的预约
                Seat seat = seatManageDateService.GetSeatInfoBySeatNum(seatNo);
                if (seat == null)
                {
                    result.Result = false;
                    result.Msg = "座位不存在！";
                    return JSONSerializer.Serialize(result);
                }
                if (seat.IsSuspended)
                {
                    result.Result = false;
                    result.Msg = "座位已停用！";
                    return JSONSerializer.Serialize(result);
                }
                if (seat.SeatUsedState != EnterOutLogType.Leave)
                {
                    result.Result = false;
                    result.Msg = "座位正在被使用！";
                    return JSONSerializer.Serialize(result);
                }
                List<BespeakLogInfo> bespeakLogInfos = seatManageDateService.GetBespeakLogInfoBySeatNo(seatNo,
                    DateTime.Now);
                if (seat.SeatUsedState == EnterOutLogType.Leave && bespeakLogInfos.Count > 0)
                {
                    if (!seat.ReadingRoom.Setting.SeatBespeak.SelectBespeakSeat)
                    {
                        result.Result = false;
                        result.Msg = "此座位已被预约！";
                        return JSONSerializer.Serialize(result);
                    }
                    if (bespeakLogInfos[0].BsepeakTime == bespeakLogInfos[0].SubmitTime)
                    {
                        result.Result = false;
                        result.Msg = "此座位已被预约！";
                        return JSONSerializer.Serialize(result);

                    }
                    if (bespeakLogInfos[0].BsepeakTime.AddMinutes(-double.Parse(seat.ReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= DateTime.Now)
                    {
                        result.Result = false;
                        result.Msg = "此座位已被预约！";
                        return JSONSerializer.Serialize(result);
                    }
                }
                EnterOutLogInfo enterOutLogInfo = seatManageDateService.GetEnterOutLogInfoByCardNo(cardNo);
                if (enterOutLogInfo == null || enterOutLogInfo.EnterOutState == EnterOutLogType.Leave)
                {
                    if (seatManageDateService.GetSingleBespeakLogForWait(cardNo) != null)
                    {
                        result.Result = false;
                        result.Msg = "您有等待签到的预约记录！";
                        return JSONSerializer.Serialize(result);
                    }
                    if (seatManageDateService.GetWaitLogList(cardNo, null, null, null, new List<EnterOutLogType>() { EnterOutLogType.Waiting }).Count > 0)
                    {
                        result.Result = false;
                        result.Msg = "您有正在等待的座位！";
                        return JSONSerializer.Serialize(result);
                    }
                    result.Result = false;
                    result.Msg = "请先选择一个座位！";
                    return JSONSerializer.Serialize(result);
                }
                EnterOutLogInfo newLogInfo = enterOutLogInfo;
                newLogInfo.ReadingRoomNo = seat.ReadingRoomNum;
                newLogInfo.Remark = "通过移动客户端更换到该座位";
                newLogInfo.SeatNo = seat.SeatNo;
                newLogInfo.Flag = Operation.Reader;
                newLogInfo.EnterOutType = LogStatus.Valid;
                newLogInfo.EnterOutState = EnterOutLogType.ReselectSeat;
                newLogInfo.EnterOutLogNo = SeatComm.RndNum();
                int newLogId = -1;
                if (seatManageDateService.AddEnterOutLogInfo(newLogInfo, ref newLogId) == HandleResult.Successed)
                {
                    result.Result = true;
                    result.Msg = "座位更换成功！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "座位更换失败！";
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("更换座位遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 暂离回来
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string ComeBack(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                int newId = -1;
                ReaderInfo readerInfo = seatManageDateService.GetReader(studentNo, true);
                if (readerInfo == null || readerInfo.EnterOutLog == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，暂离回来失败！";
                    return JSONSerializer.Serialize(result);
                }
                readerInfo.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                readerInfo.EnterOutLog.Remark = string.Format("您在移动终端设置{0} {1}号座位暂离回来", readerInfo.EnterOutLog.ReadingRoomName, readerInfo.EnterOutLog.SeatNo);
                readerInfo.EnterOutLog.Flag = Operation.Reader;
                readerInfo.EnterOutLog.EnterOutTime = DateTime.Now;
                HandleResult handleResult = seatManageDateService.AddEnterOutLogInfo(readerInfo.EnterOutLog, ref newId);
                if (handleResult == HandleResult.Successed)
                {
                    result.Result = true;
                    result.Msg = readerInfo.EnterOutLog.Remark;
                    return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "对不起，暂离回来失败！";
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("暂离回来遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 选择座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string SelectSeat(string studentNo, string seatNo, string roomNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<string> roomNos = new List<string>();
                roomNos.Add(roomNo);
                List<ReadingRoomInfo> roomInfos = seatManageDateService.GetReadingRoomInfo(roomNos);
                if (roomInfos.Count == 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，没有找到对应的阅览室！";
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting roomSetting = roomInfos[0].Setting;
                ReaderInfo readerInfo = seatManageDateService.GetReader(studentNo, true);
                EnterOutLogType readerStatus = EnterOutLogType.Leave;
                if (readerInfo.EnterOutLog != null && readerInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    readerStatus = readerInfo.EnterOutLog.EnterOutState;
                }
                if (readerInfo.WaitSeatLog != null)
                {
                    readerStatus = EnterOutLogType.Waiting;
                }
                if (readerInfo.BespeakLog.Count > 0)
                {
                    readerStatus = EnterOutLogType.BespeakWaiting;
                }
                switch (readerStatus)
                {
                    case EnterOutLogType.Leave:
                        if (seatManageDateService.GetReaderChooseSeatTimes(studentNo, roomSetting.PosTimes.Minutes) >=
                            roomSetting.PosTimes.Times)
                        {
                            result.Result = false;
                            result.Msg = "对不起，您选座过于频繁！";
                            return JSONSerializer.Serialize(result);
                        }
                        EnterOutLogInfo enterOutLogInfo = new EnterOutLogInfo();
                        enterOutLogInfo.CardNo = studentNo;
                        enterOutLogInfo.ReadingRoomNo = roomNo;
                        enterOutLogInfo.Remark = string.Format("您通过移动终端选择{0} {1}号座位", roomInfos[0].Name, seatNo);
                        enterOutLogInfo.SeatNo = seatNo;
                        enterOutLogInfo.Flag = Operation.Reader;
                        enterOutLogInfo.EnterOutType = LogStatus.Valid;
                        enterOutLogInfo.EnterOutState = EnterOutLogType.SelectSeat;
                        enterOutLogInfo.EnterOutLogNo = SeatComm.RndNum();
                        int newId = -1;
                        if (seatManageDateService.AddEnterOutLogInfo(enterOutLogInfo, ref newId) ==
                            HandleResult.Successed)
                        {
                            result.Result = true;
                            result.Msg = enterOutLogInfo.Remark;
                            return JSONSerializer.Serialize(result);
                        }
                        result.Result = false;
                        result.Msg = "对不起，选座失败！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.BespeakWaiting:
                        result.Result = false;
                        result.Msg = "对不起，您有等待签到的座位！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                    case EnterOutLogType.ShortLeave:
                        result.Result = false;
                        result.Msg = "对不起，您已经有座位了！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.Waiting:
                        result.Result = false;
                        result.Msg = "对不起，您当前正在等待其他座位！";
                        return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "对不起，读者当前状态异常！";
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("选择座位遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 座位续时
        /// </summary>
        /// <param name="studentNo"></param>
        /// <returns></returns>
        public string DelayTime(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                ReaderInfo readerInfo = new ReaderInfo();
                readerInfo = seatManageDateService.GetReader(studentNo, true);
                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，没有查询到读者信息！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = true;
                result.Msg = seatManageDateService.DelaySeatUsedTime(readerInfo);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("座位续时遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 等待座位
        /// </summary>
        /// <param name="studentNo_A"></param>
        /// <param name="studentNo_B"></param>
        /// <param name="seatNo"></param>
        /// <returns></returns>
        public string WaitSeat(string studentNo_A, string studentNo_B, string seatNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                EnterOutLogInfo enterOutLogInfo = seatManageDateService.GetEnterOutLogInfoBySeatNum(seatNo);
                if (enterOutLogInfo == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，等待座位失败！";
                    return JSONSerializer.Serialize(result);
                }
                List<string> roomNos = new List<string>();
                roomNos.Add(enterOutLogInfo.ReadingRoomNo);
                List<ReadingRoomInfo> roomInfos = seatManageDateService.GetReadingRoomInfo(roomNos);
                if (roomInfos.Count == 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，没有找到对应的阅览室！";
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting roomSetting = roomInfos[0].Setting;
                if (enterOutLogInfo.EnterOutState == EnterOutLogType.BookingConfirmation ||
                    enterOutLogInfo.EnterOutState == EnterOutLogType.ComeBack ||
                    enterOutLogInfo.EnterOutState == EnterOutLogType.ContinuedTime ||
                    enterOutLogInfo.EnterOutState == EnterOutLogType.ReselectSeat ||
                    enterOutLogInfo.EnterOutState == EnterOutLogType.SelectSeat ||
                    enterOutLogInfo.EnterOutState == EnterOutLogType.WaitingSuccess)
                {
                    enterOutLogInfo.EnterOutState = EnterOutLogType.ShortLeave;
                    enterOutLogInfo.EnterOutType = LogStatus.Valid;
                    enterOutLogInfo.Flag = Operation.OtherReader;
                    enterOutLogInfo.Remark = string.Format("读者{0}通过移动终端设置您{1} {2}号座位暂离并等待该座位", studentNo_B,
                        enterOutLogInfo.ReadingRoomName, enterOutLogInfo.ShortSeatNo);
                    int newId = -1;
                    HandleResult handleResult = seatManageDateService.AddEnterOutLogInfo(enterOutLogInfo, ref newId);
                    if (handleResult == HandleResult.Successed)
                    {
                        WaitSeatLogInfo waitSeatLogInfo = new WaitSeatLogInfo();
                        waitSeatLogInfo.EnterOutLogID = newId;
                        waitSeatLogInfo.CardNo = studentNo_A;
                        waitSeatLogInfo.CardNoB = studentNo_B;
                        waitSeatLogInfo.NowState = LogStatus.Valid;
                        waitSeatLogInfo.OperateType = Operation.OtherReader;
                        waitSeatLogInfo.ReadingRoomNo = enterOutLogInfo.ReadingRoomNo;
                        waitSeatLogInfo.SeatNo = enterOutLogInfo.SeatNo;
                        waitSeatLogInfo.SeatWaitTime = DateTime.Now;
                        waitSeatLogInfo.WaitingState = EnterOutLogType.Waiting;
                        if (seatManageDateService.AddWaitLog(waitSeatLogInfo) < 1)
                        {
                            result.Result = true;
                            result.Msg = string.Format("您通过移动终端等待读者{0}在{1} {2}号座位，等待时间为{3}分钟", studentNo_A,
                                enterOutLogInfo.ReadingRoomName, enterOutLogInfo.ShortSeatNo, roomSetting.SeatHoldTime);
                            return JSONSerializer.Serialize(result);
                        }
                        result.Result = false;
                        result.Msg = "对不起，等待座位失败！";
                        return JSONSerializer.Serialize(result);
                    }
                }
                result.Result = false;
                result.Msg = "对不起，该座位不能被等待，请重新选择！";
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("座位等待遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        public string ConfirmSeat(string besapeakNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                BespeakLogInfo bespeakLogInfo = seatManageDateService.GetBespeaklogById(int.Parse(besapeakNo));
                DateTime nowTime = DateTime.Now;
                if (bespeakLogInfo == null)
                {
                    result.Result = false;
                    result.Msg = "获取预约记录失败！";
                    return JSONSerializer.Serialize(result);
                }
                if (bespeakLogInfo.BsepeakState != BookingStatus.Waiting)
                {
                    result.Result = false;
                    result.Msg = "此记录无效，请刷新页面重试！";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> readingRoomInfos =
                    seatManageDateService.GetReadingRoomInfo(new List<string>() { bespeakLogInfo.ReadingRoomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "未查询到与记录相关阅览室信息！";
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting roomSetting = readingRoomInfos[0].Setting;
                DateTime dtBegin =
                    bespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(roomSetting.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd =
                    bespeakLogInfo.BsepeakTime.AddMinutes(double.Parse(roomSetting.SeatBespeak.ConfirmTime.EndTime));
                if (nowTime.CompareTo(dtBegin) > 0)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，未到签到时间，请在{0}到{1}间签到入座", dtBegin.ToShortTimeString(), dtEnd.ToShortTimeString());
                    return JSONSerializer.Serialize(result);
                }
                if (nowTime.CompareTo(dtEnd) < 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，您的预约已超时！";
                    return JSONSerializer.Serialize(result);
                }
                if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowTime) ||
                    (roomSetting.SeatBespeak.NowDayBespeak && bespeakLogInfo.SubmitTime == bespeakLogInfo.BsepeakTime))
                {
                    EnterOutLogInfo enterOutLogInfo =
                        seatManageDateService.GetEnterOutLogInfoBySeatNum(bespeakLogInfo.SeatNo);
                    if (enterOutLogInfo != null && enterOutLogInfo.EnterOutState != EnterOutLogType.Leave)
                    {
                        enterOutLogInfo.EnterOutState = EnterOutLogType.Leave;
                        enterOutLogInfo.EnterOutType = LogStatus.Valid;
                        enterOutLogInfo.Remark = string.Format("读者{0}在移动终端签到入座{1} {2}号座位，并设置在座读者{3}离开",
                            bespeakLogInfo.CardNo, bespeakLogInfo.ReadingRoomName, bespeakLogInfo.SeatNo,
                            enterOutLogInfo.CardNo);
                        enterOutLogInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        if (seatManageDateService.AddEnterOutLogInfo(enterOutLogInfo, ref newId) ==
                            HandleResult.Successed)
                        {
                            List<WaitSeatLogInfo> waitSeatLogInfos = seatManageDateService.GetWaitLogList(null,
                                enterOutLogInfo.EnterOutLogID, null, null, null);
                            if (waitSeatLogInfos.Count > 0)
                            {
                                WaitSeatLogInfo waitSeatLogInfo = waitSeatLogInfos[0];
                                waitSeatLogInfo.OperateType = Operation.Reader;
                                waitSeatLogInfo.WaitingState = EnterOutLogType.WaitingCancel;
                                waitSeatLogInfo.NowState = LogStatus.Valid;
                                if (!seatManageDateService.UpdateWaitLog(waitSeatLogInfo))
                                {
                                    result.Result = false;
                                    result.Msg = "设置等待该座位读者离开失败！";
                                    return JSONSerializer.Serialize(result);
                                }
                            }
                        }
                        result.Result = false;
                        result.Msg = "设置当前在座读者离开失败！";
                        return JSONSerializer.Serialize(result);
                    }
                    EnterOutLogInfo newEnterOutLogInfo = new EnterOutLogInfo();
                    newEnterOutLogInfo.CardNo = bespeakLogInfo.CardNo;
                    newEnterOutLogInfo.EnterOutLogNo = SeatComm.RndNum();
                    newEnterOutLogInfo.EnterOutState = EnterOutLogType.BookingConfirmation;
                    newEnterOutLogInfo.EnterOutType = LogStatus.Valid;
                    newEnterOutLogInfo.Flag = Operation.Reader;
                    newEnterOutLogInfo.ReadingRoomNo = bespeakLogInfo.ReadingRoomNo;
                    newEnterOutLogInfo.ReadingRoomName = bespeakLogInfo.ReadingRoomName;
                    newEnterOutLogInfo.ShortSeatNo = bespeakLogInfo.ShortSeatNum;
                    newEnterOutLogInfo.SeatNo = bespeakLogInfo.SeatNo;
                    newEnterOutLogInfo.Remark = string.Format("您在移动终端预约签到入座{0} {1}号座位", bespeakLogInfo.ReadingRoomName,
                        bespeakLogInfo.ShortSeatNum);
                    int logId = -1;
                    if (seatManageDateService.AddEnterOutLogInfo(newEnterOutLogInfo, ref logId) == HandleResult.Successed)
                    {
                        bespeakLogInfo.BsepeakState = BookingStatus.Confinmed;
                        bespeakLogInfo.CancelPerson = Operation.Reader;
                        bespeakLogInfo.CancelTime = nowTime;
                        bespeakLogInfo.Remark = newEnterOutLogInfo.Remark;
                        if (seatManageDateService.UpdateBespeakLogInfo(bespeakLogInfo) > 0)
                        {
                            result.Result = true;
                            result.Msg = newEnterOutLogInfo.Remark;
                            return JSONSerializer.Serialize(result);
                        }
                        result.Result = false;
                        result.Msg = "系统错误，签到失败！";
                        return JSONSerializer.Serialize(result);
                    }
                    result.Result = false;
                    result.Msg = "系统错误，签到失败！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "系统错误，签到失败！";
                return JSONSerializer.Serialize(result);

            }
            catch (Exception ex)
            {
                WriteLog.Write("座位续时遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 预约签到
        /// </summary>
        /// <param name="besapeakNo"></param>
        /// <returns></returns>
        public string CheckSeat(string studentNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                DateTime nowTime = DateTime.Now;
               if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "学号不能为空";
                    return JSONSerializer.Serialize(result);
                }
                ReaderInfo readerInfo = seatManageDateService.GetReader(studentNo, true);

                if (readerInfo == null)
                {
                    result.Result = false;
                    result.Msg = "未查询到该读者的当前状态";
                    return JSONSerializer.Serialize(result);
                }
                if (readerInfo.BespeakLog.Count < 1 || readerInfo.BespeakLog[0].BsepeakTime.Date != nowTime.Date)
                {
                    result.Result = false;
                    result.Msg = "对不起，您当前没有预约记录！";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> readingRoomInfos = seatManageDateService.GetReadingRoomInfo(new List<string>() { readerInfo.BespeakLog[0].ReadingRoomNo });
                if (readingRoomInfos.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "未查询到与记录相关阅览室信息！";
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting roomSetting = readingRoomInfos[0].Setting;
                DateTime dtBegin = readerInfo.BespeakLog[0].BsepeakTime.AddMinutes(-double.Parse(roomSetting.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd = readerInfo.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(roomSetting.SeatBespeak.ConfirmTime.EndTime));
                if (readerInfo.BespeakLog[0].BsepeakTime<dtBegin)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，未到签到时间，请在{0}到{1}间签到入座", dtBegin.ToShortTimeString(), dtEnd.ToShortTimeString());
                    return JSONSerializer.Serialize(result);
                }
                if (dtEnd < readerInfo.BespeakLog[0].BsepeakTime)
                {
                    result.Result = false;
                    result.Msg = "对不起，您的预约已超时！";
                    return JSONSerializer.Serialize(result);
                }
                if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowTime) ||(roomSetting.SeatBespeak.NowDayBespeak && readerInfo.BespeakLog[0].SubmitTime == readerInfo.BespeakLog[0].BsepeakTime))
                {
                    EnterOutLogInfo enterOutLogInfo =seatManageDateService.GetEnterOutLogInfoBySeatNum(readerInfo.BespeakLog[0].SeatNo);
                    if (enterOutLogInfo != null && enterOutLogInfo.EnterOutState != EnterOutLogType.Leave)
                    {
                        enterOutLogInfo.EnterOutState = EnterOutLogType.Leave;
                        enterOutLogInfo.EnterOutType = LogStatus.Valid;
                        enterOutLogInfo.Remark = string.Format("读者{0}在移动终端签到入座{1} {2}号座位，并设置在座读者{3}离开",readerInfo.BespeakLog[0].CardNo, readerInfo.BespeakLog[0].ReadingRoomName, readerInfo.BespeakLog[0].SeatNo,enterOutLogInfo.CardNo);
                        enterOutLogInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        if (seatManageDateService.AddEnterOutLogInfo(enterOutLogInfo, ref newId) == HandleResult.Successed)
                        {
                            List<WaitSeatLogInfo> waitSeatLogInfos = seatManageDateService.GetWaitLogList(null, enterOutLogInfo.EnterOutLogID, null, null, null);
                            if (waitSeatLogInfos.Count > 0)
                            {
                                WaitSeatLogInfo waitSeatLogInfo = waitSeatLogInfos[0];
                                waitSeatLogInfo.OperateType = Operation.Reader;
                                waitSeatLogInfo.WaitingState = EnterOutLogType.WaitingCancel;
                                waitSeatLogInfo.NowState = LogStatus.Valid;
                                if (!seatManageDateService.UpdateWaitLog(waitSeatLogInfo))
                                {
                                    result.Result = false;
                                    result.Msg = "设置等待该座位读者离开失败！";
                                    return JSONSerializer.Serialize(result);
                                }
                            }
                        }
                        else
                        {
                            result.Result = false;
                            result.Msg = "设置当前在座读者离开失败！";
                            return JSONSerializer.Serialize(result);
                        }
                    }
                    EnterOutLogInfo newEnterOutLogInfo = new EnterOutLogInfo();
                    newEnterOutLogInfo.CardNo = readerInfo.BespeakLog[0].CardNo;
                    newEnterOutLogInfo.EnterOutLogNo = SeatComm.RndNum();
                    newEnterOutLogInfo.EnterOutState = EnterOutLogType.BookingConfirmation;
                    newEnterOutLogInfo.EnterOutType = LogStatus.Valid;
                    newEnterOutLogInfo.Flag = Operation.Reader;
                    newEnterOutLogInfo.ReadingRoomNo = readerInfo.BespeakLog[0].ReadingRoomNo;
                    newEnterOutLogInfo.ReadingRoomName = readerInfo.BespeakLog[0].ReadingRoomName;
                    newEnterOutLogInfo.ShortSeatNo = readerInfo.BespeakLog[0].ShortSeatNum;
                    newEnterOutLogInfo.SeatNo = readerInfo.BespeakLog[0].SeatNo;
                    newEnterOutLogInfo.Remark = string.Format("您在移动终端预约签到入座{0} {1}号座位", readerInfo.BespeakLog[0].ReadingRoomName,readerInfo.BespeakLog[0].ShortSeatNum);
                    int logId = -1;
                    if (seatManageDateService.AddEnterOutLogInfo(newEnterOutLogInfo, ref logId) == HandleResult.Successed)
                    {
                        readerInfo.BespeakLog[0].BsepeakState = BookingStatus.Confinmed;
                        readerInfo.BespeakLog[0].CancelPerson = Operation.Reader;
                        readerInfo.BespeakLog[0].CancelTime = nowTime;
                        readerInfo.BespeakLog[0].Remark = newEnterOutLogInfo.Remark;
                        if (seatManageDateService.UpdateBespeakLogInfo(readerInfo.BespeakLog[0]) > 0)
                        {
                            result.Result = true;
                            result.Msg = newEnterOutLogInfo.Remark;
                            return JSONSerializer.Serialize(result);
                        }
                        result.Result = false;
                        result.Msg = "系统错误，签到失败！";
                        return JSONSerializer.Serialize(result);
                    }
                    result.Result = false;
                    result.Msg = "系统错误，签到失败！";
                    return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "系统错误，签到失败！";
                return JSONSerializer.Serialize(result);

            }
            catch (Exception ex)
            {
                WriteLog.Write("座位续时遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 判断读者是否可以进入阅览室
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        private string VerifyCanDoIt(string studentNo, string roomNo)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = true;
                result.Msg = "";
                if (string.IsNullOrEmpty(studentNo))
                {
                    result.Result = false;
                    result.Msg = "对不起，输入的学号不能为空。";
                    return JSONSerializer.Serialize(result);
                }
                List<ReadingRoomInfo> rooms = seatManageDateService.GetReadingRoomInfo(new List<string>() { roomNo });
                if (rooms.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室不存在。";
                    return JSONSerializer.Serialize(result);
                }
                //判断阅览室是否开放
                List<string> noList = new List<string> { roomNo };
                ReadingRoomInfo readingRoomInfo = seatManageDateService.GetReadingRoomInfo(noList)[0];
                if (readingRoomInfo.Setting.ReadingRoomOpenState(seatManageDateService.GetServerDateTime()) == ReadingRoomStatus.Close)
                {
                    result.Result = false;
                    result.Msg = "对不起，此阅览室尚未开放。";
                    return JSONSerializer.Serialize(result);
                }
                //判断阅览室是否已满
                //Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsedStates = SeatManageDateService.GetRoomSeatUsedStateV5(noList);
                //ReadingRoomSeatUsedState roomStatus = new ReadingRoomSeatUsedState();

                //roomStatus = roomSeatUsedStates[roomNo];

                //if (roomStatus != null && roomStatus.SeatAmountFree <= 0 && !rooms[0].Setting.NoManagement.Used)
                //{
                //    result.Result = false;
                //    result.Msg = "对不起，此阅览室座位已满。";
                //    return JSONSerializer.Serialize(result);
                //}
                //判断黑名单
                if (!string.IsNullOrEmpty(checkBlacklist(studentNo, rooms[0])))
                {
                    result.Result = false;
                    result.Msg = "对不起，您在黑名单中存在记录。";
                    return JSONSerializer.Serialize(result);
                }
                //判断选座次数
                if (rooms[0].Setting.PosTimes.IsUsed && seatManageDateService.GetReaderChooseSeatTimes(studentNo, rooms[0].Setting.PosTimes.Minutes) >= rooms[0].Setting.PosTimes.Times)
                {
                    result.Result = false;
                    result.Msg = "操作失败，选座频繁。";
                    return JSONSerializer.Serialize(result);
                }
                //判断读者类型
                ReaderInfo reader = seatManageDateService.GetReader(studentNo, false);
                if (reader == null)
                {
                    reader = new ReaderInfo();
                    reader.CardNo = studentNo;
                    reader.ReaderType = "未指定";
                }
                if (!ProvenReaderType(reader, rooms[0].Setting))
                {
                    result.Result = false;
                    result.Msg = "对不起，您的用户类型'" + reader.ReaderType + "'不允许在此阅览室选座。";
                    return JSONSerializer.Serialize(result);
                }
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("判断读者是否允许进入阅览室遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 验证黑名单
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        private string checkBlacklist(string cardNo, ReadingRoomInfo room)
        {
            List<BlackListInfo> blacklist = seatManageDateService.GetBlacklistInfo(cardNo);
            string result = "";
            if (!room.Setting.UsedBlacklistLimit || blacklist.Count <= 0)
            {
                return result;
            }
            if (room.Setting.BlackListSetting.Used)
            {
                if (blacklist.Any(blinfo => blinfo.ReadingRoomID == room.No))
                {
                    result = blacklist[0].ReMark;
                }
            }
            else
            {
                result = blacklist[0].ReMark;
            }
            return result;

        }
        /// <summary>
        /// 验证读者类型
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="roomSet"></param>
        /// <returns></returns>
        private bool ProvenReaderType(ReaderInfo reader, ReadingRoomSetting roomSet)
        {
            if (roomSet.LimitReaderEnter.Used)
            {
                string[] readerTypes = roomSet.LimitReaderEnter.ReaderTypes.Split(';');
                if (readerTypes.Any(t => reader.ReaderType == t))
                {
                    return roomSet.LimitReaderEnter.CanEnter;
                }
                return !roomSet.LimitReaderEnter.CanEnter;
            }
            return true;
        }

        /// <summary>
        /// 管理员分配座位
        /// </summary>
        /// <param name="studentNo"></param>
        /// <param name="seatNo"></param>
        /// <param name="roomNo"></param>
        /// <returns></returns>
        public string SelectSeatByMessager(string studentNo, string seatNo, string roomNo)
        {
            AJM_HandleResult result = new AJM_HandleResult();
            try
            {
                List<string> roomNos = new List<string>();
                roomNos.Add(roomNo);
                List<ReadingRoomInfo> roomInfos = seatManageDateService.GetReadingRoomInfo(roomNos);
                if (roomInfos.Count == 0)
                {
                    result.Result = false;
                    result.Msg = "对不起，没有找到对应的阅览室！";
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting roomSetting = roomInfos[0].Setting;
                ReaderInfo readerInfo = seatManageDateService.GetReader(studentNo, true);
                EnterOutLogType readerStatus = EnterOutLogType.Leave;
                if (readerInfo.EnterOutLog != null && readerInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    readerStatus = readerInfo.EnterOutLog.EnterOutState;
                }
                if (readerInfo.WaitSeatLog != null)
                {
                    readerStatus = EnterOutLogType.Waiting;
                }
                if (readerInfo.BespeakLog.Count > 0)
                {
                    readerStatus = EnterOutLogType.BespeakWaiting;
                }
                switch (readerStatus)
                {
                    case EnterOutLogType.Leave:
                        EnterOutLogInfo enterOutLogInfo = new EnterOutLogInfo();
                        enterOutLogInfo.CardNo = studentNo;
                        enterOutLogInfo.ReadingRoomNo = roomNo;
                        enterOutLogInfo.Remark = string.Format("管理员通过移动终端分配{0} {1}号座位", roomInfos[0].Name, seatNo);
                        enterOutLogInfo.SeatNo = seatNo;
                        enterOutLogInfo.Flag = Operation.Admin;
                        enterOutLogInfo.EnterOutType = LogStatus.Valid;
                        enterOutLogInfo.EnterOutState = EnterOutLogType.SelectSeat;
                        enterOutLogInfo.EnterOutLogNo = SeatComm.RndNum();
                        int newId = -1;
                        if (seatManageDateService.AddEnterOutLogInfo(enterOutLogInfo, ref newId) ==
                            HandleResult.Successed)
                        {
                            result.Result = true;
                            result.Msg = enterOutLogInfo.Remark;
                            return JSONSerializer.Serialize(result);
                        }
                        result.Result = false;
                        result.Msg = "对不起，选座失败！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.BespeakWaiting:
                        result.Result = false;
                        result.Msg = "对不起，用户有等待签到的座位！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                    case EnterOutLogType.ShortLeave:
                        result.Result = false;
                        result.Msg = "对不起，用户已经有座位了！";
                        return JSONSerializer.Serialize(result);
                    case EnterOutLogType.Waiting:
                        result.Result = false;
                        result.Msg = "对不起，用户当前正在等待其他座位！";
                        return JSONSerializer.Serialize(result);
                }
                result.Result = false;
                result.Msg = "对不起，读者当前状态异常！";
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("选择座位遇到异常：" + ex.Message);
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }
    }
}
