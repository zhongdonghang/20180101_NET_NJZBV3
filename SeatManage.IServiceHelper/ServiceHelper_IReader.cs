using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.EnumType;
using JsonModel;
using SeatManage.JsonModel;
using SeatManage.ClassModel;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IReader
    {
        /// <summary>
        /// 根据卡号获取读者信息
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public string GetBaseReaderInfoByCardId(string cardId)
        {
            try
            {
                SeatManage.ClassModel.ReaderInfo reader = seatDataService.GetReaderByCardId(cardId);
                if (reader != null)
                {
                    JM_ReaderInfo jm_Reader = new JM_ReaderInfo();
                    jm_Reader.CardId = reader.CardID;
                    jm_Reader.CardNo = reader.CardNo;
                    jm_Reader.Name = reader.Name;
                    jm_Reader.Sex = reader.Sex;
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_Reader);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起，此读者信息不存在";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("根据卡号获取读者信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 根据学号获取读者信息
        /// </summary>
        /// <param name="cardNum"></param>
        /// <returns></returns>
        public string GetBaseReaderInfo(string cardNum)
        {
            try
            {
                SeatManage.ClassModel.ReaderInfo reader = seatDataService.GetReader(cardNum, false);
                if (reader != null)
                {
                    JM_ReaderInfo jm_Reader = new JM_ReaderInfo();
                    jm_Reader.CardId = reader.CardID;
                    jm_Reader.CardNo = reader.CardNo;
                    jm_Reader.Name = reader.Name;
                    jm_Reader.Sex = reader.Sex;
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_Reader);
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "对不起，此读者信息不存在";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("根据学号获取读者信息遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取实时记录
        /// 预约记录
        ///选座记录
        ///等待记录
        ///黑名单记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="getItemsParameter"></param>
        /// <returns></returns>
        public string GetReaderActualTimeRecord(string cardNum, string getItemsParameter)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum) || string.IsNullOrEmpty(getItemsParameter))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "输入的数据数据不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JsonModel.JM_ActualTimeRecordParam param = SeatManageComm.JSONSerializer.Deserialize<JsonModel.JM_ActualTimeRecordParam>(getItemsParameter);
                JsonModel.JM_ActualTimeRecords records = new JsonModel.JM_ActualTimeRecords();
                if (param.GetEnterOutLog)//获取进出记录
                {
                    SeatManage.ClassModel.EnterOutLogInfo enterOutLog = seatDataService.GetEnterOutLogInfoByCardNo(cardNum);
                    JsonModel.JM_EnterOutLog jm_enterOutLog = null;
                    if (enterOutLog != null)
                    {
                        jm_enterOutLog = new JsonModel.JM_EnterOutLog();
                        jm_enterOutLog.EnterOutTime = enterOutLog.EnterOutTime.ToString("yyyy-MM-dd HH:mm:ss");
                        jm_enterOutLog.EnterOutState = enterOutLog.EnterOutState.ToString();
                        jm_enterOutLog.Id = enterOutLog.EnterOutLogID;
                        jm_enterOutLog.SeatId = enterOutLog.SeatNo;
                        jm_enterOutLog.SeatNum = enterOutLog.ShortSeatNo;
                        jm_enterOutLog.RoomName = enterOutLog.ReadingRoomName;
                        jm_enterOutLog.RoomNum = enterOutLog.ReadingRoomNo;
                        SeatManage.ClassModel.ReadingRoomInfo room = seatDataService.GetReadingRoomInfo(new List<string>() { enterOutLog.ReadingRoomNo })[0];
                        if (enterOutLog.EnterOutState == EnterOutLogType.ShortLeave)
                        {
                            String seatInfo = enterOutLog.Remark;
                            double saveTimeLength = 0;
                            if (enterOutLog.Flag == Operation.Admin)
                            {
                                if (room.Setting.AdminShortLeave.IsUsed)
                                {
                                    saveTimeLength = room.Setting.AdminShortLeave.HoldTimeLength;
                                }
                                else
                                {
                                    saveTimeLength = SeatManage.ClassModel.ReadingRoomSetting.GetSeatHoldTime(room.Setting.SeatHoldTime, enterOutLog.EnterOutTime);
                                }
                            }
                            else
                            {
                                saveTimeLength = SeatManage.ClassModel.ReadingRoomSetting.GetSeatHoldTime(room.Setting.SeatHoldTime, enterOutLog.EnterOutTime);
                            }
                            string saveTime = enterOutLog.EnterOutTime.AddMinutes(saveTimeLength).ToShortTimeString();
                            jm_enterOutLog.Remark = string.Format("座位将为您保留至{2}。", enterOutLog.ReadingRoomName, enterOutLog.ShortSeatNo, saveTime);

                        }
                        else
                        {
                            switch (enterOutLog.EnterOutState)
                            {
                                case EnterOutLogType.BookingConfirmation:
                                case EnterOutLogType.SelectSeat:
                                case EnterOutLogType.ContinuedTime:
                                case EnterOutLogType.ComeBack:
                                case EnterOutLogType.ReselectSeat:
                                case EnterOutLogType.WaitingSuccess:
                                    List<SeatManage.ClassModel.BespeakLogInfo> bespeaklist = seatDataService.GetBespeakLogInfoBySeatNo(enterOutLog.SeatNo, DateTime.Now);
                                    DateTime dt = new DateTime();
                                    if (bespeaklist.Count > 0)
                                    {
                                        dt = bespeaklist[0].BsepeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
                                    }
                                    else if (room.Setting.SeatUsedTimeLimit.Used)
                                    {

                                        if (room.Setting.SeatUsedTimeLimit.Mode == "Free")
                                        {

                                            dt = enterOutLog.EnterOutTime.AddMinutes(room.Setting.SeatUsedTimeLimit.UsedTimeLength);
                                            if (dt > room.Setting.RoomOpenSet.NowCloseTime(enterOutLog.EnterOutTime))
                                            {
                                                dt = room.Setting.RoomOpenSet.NowCloseTime(enterOutLog.EnterOutTime);
                                            }
                                        }
                                        else
                                        {
                                            for (int i = 0; i < room.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                                            {
                                                if (enterOutLog.EnterOutTime < room.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                                {
                                                    if (room.Setting.SeatUsedTimeLimit.IsCanContinuedTime && enterOutLog.EnterOutTime > room.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-room.Setting.SeatUsedTimeLimit.CanDelayTime))
                                                    {
                                                        if (i + 1 < room.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                                        {
                                                            dt = room.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                                        }
                                                        else
                                                        {
                                                            dt = room.Setting.RoomOpenSet.NowCloseTime(enterOutLog.EnterOutTime);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        dt = room.Setting.SeatUsedTimeLimit.FixedTimes[i];
                                                    }
                                                    break;
                                                }
                                            }
                                        }

                                    }
                                    //TODO:如果是续时模式，提示续时时间。
                                    jm_enterOutLog.Remark = string.Format("您的座位可以使用到{0}。", dt.ToShortTimeString());
                                    break;
                            }
                        }
                    }
                    records.EnterOutLog = jm_enterOutLog;
                }
                if (param.GetBespeakLog)
                {
                    JsonModel.JM_BespeakLog jm_bespeakLog = null;
                    SeatManage.ClassModel.BespeakLogInfo bespeakLogs = seatDataService.GetSingleBespeakLogForWait(cardNum);
                    if (bespeakLogs != null)
                    {
                        jm_bespeakLog = new JM_BespeakLog();
                        jm_bespeakLog.DateTime = bespeakLogs.BsepeakTime.ToString("yyyy-MM-dd HH:mm:ss");
                        jm_bespeakLog.Id = bespeakLogs.BsepeaklogID;
                        if (bespeakLogs.BsepeakState == EnumType.BookingStatus.Waiting)
                        {
                            jm_bespeakLog.IsValid = true;
                        }
                        jm_bespeakLog.RoomName = bespeakLogs.ReadingRoomName;
                        jm_bespeakLog.RoomNum = bespeakLogs.ReadingRoomNo;
                        jm_bespeakLog.SeatId = bespeakLogs.SeatNo;
                        jm_bespeakLog.SeatNum = bespeakLogs.ShortSeatNum;
                        jm_bespeakLog.Remark = bespeakLogs.Remark;
                        records.BespeakLog = jm_bespeakLog;
                    }
                }
                if (param.GetWaitLog)
                {
                    List<EnterOutLogType> logType = new List<EnterOutLogType>();
                    logType.Add(EnterOutLogType.Waiting);
                    List<SeatManage.ClassModel.WaitSeatLogInfo> waitSeatlogs = seatDataService.GetWaitLogList(cardNum, null, null, null, logType);
                    if (waitSeatlogs.Count > 0)
                    {
                        EnterOutLogInfo waitEnterOutLog = seatDataService.GetEnterOutLogInfoById(waitSeatlogs[0].EnterOutLogID);
                        if (waitEnterOutLog != null)
                        {
                            JsonModel.JM_WaitSeatLog jm_waitSeatLog = new JM_WaitSeatLog();
                            jm_waitSeatLog.CardNo = waitSeatlogs[0].CardNo;
                            jm_waitSeatLog.CardNoB = waitSeatlogs[0].CardNoB;
                            jm_waitSeatLog.RoomName = waitEnterOutLog.ReadingRoomName;
                            jm_waitSeatLog.RoomNum = waitEnterOutLog.ReadingRoomNo;
                            jm_waitSeatLog.SeatId = waitEnterOutLog.SeatNo;
                            jm_waitSeatLog.SeatNum = waitEnterOutLog.ShortSeatNo;
                            jm_waitSeatLog.SeatWaitTime = waitSeatlogs[0].SeatWaitTime.ToString("yyyy-MM-dd HH:mm:ss");
                            jm_waitSeatLog.SeatWaitingID = waitSeatlogs[0].SeatWaitingID;
                            records.WaitSeatLog = jm_waitSeatLog;
                        }

                    }
                }
                if (param.GetBlackList)
                {
                    List<BlackListInfo> blacklist = seatDataService.GetBlacklistInfo(cardNum);
                    if (blacklist.Count > 0)
                    {

                        JM_Blacklist jm_blacklist = new JM_Blacklist();
                        jm_blacklist.AddTime = blacklist[0].AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                        jm_blacklist.CardNo = blacklist[0].CardNo;
                        jm_blacklist.ID = blacklist[0].ID;
                        switch (blacklist[0].BlacklistState)
                        {
                            case LogStatus.Valid:
                                jm_blacklist.IsValid = true;
                                break;
                            default:
                                jm_blacklist.IsValid = false;
                                break;
                        }
                        jm_blacklist.OutBlacklistMode = blacklist[0].OutBlacklistMode.ToString();
                        jm_blacklist.OutTime = blacklist[0].OutTime.ToString("yyyy-MM-dd HH:mm:ss");
                        jm_blacklist.ReMark = blacklist[0].ReMark;
                        records.BlacklistLog = jm_blacklist;
                    }
                }

                return SeatManageComm.JSONSerializer.Serialize(records);
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
        /// 获取读者的预约记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        public string GetReaderBespeakRecord(string cardNum, int pageIndex, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "输入的学号不能空";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "页面和每行显示数目必须大于等于0";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<SeatManage.ClassModel.BespeakLogInfo> logs = seatDataService.GetBespeakLogsByPage(cardNum, pageIndex, pageSize);
                List<JM_BespeakLog> jm_logs = new List<JM_BespeakLog>();
                for (int i = 0; i < logs.Count; i++)
                {
                    JM_BespeakLog log = new JM_BespeakLog();
                    log.Id = logs[i].BsepeaklogID;
                    log.DateTime = logs[i].BsepeakTime.ToString("yyyy-MM-dd HH:mm:ss");
                    switch (logs[i].BsepeakState)
                    {
                        case BookingStatus.Waiting:
                            log.IsValid = true;
                            break;
                        default:
                            log.IsValid = false;
                            break;
                    }
                    log.Remark = logs[i].Remark;
                    log.RoomName = logs[i].ReadingRoomName;
                    log.RoomNum = logs[i].ReadingRoomNo;
                    log.SeatId = logs[i].SeatNo;
                    log.SeatNum = logs[i].ShortSeatNum;
                    log.SubmitDateTime = logs[i].SubmitTime.ToString("yyyy-MM-dd HH:mm:ss");
                    jm_logs.Add(log);
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_logs);
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
        /// 获取违规记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        public string GetViolateDiscipline(string cardNum, int pageIndex, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "输入的学号不能空";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "页面和每行显示数目必须大于等于0";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<ViolationRecordsLogInfo> logs = seatDataService.GetViolationRecordsLogsByPage(cardNum, pageIndex, pageSize);
                List<JM_ViolationRecordsLog> jm_logs = new List<JM_ViolationRecordsLog>();
                for (int i = 0; i < logs.Count; i++)
                {
                    JM_ViolationRecordsLog log = new JM_ViolationRecordsLog();
                    log.CardNo = logs[i].CardNo;
                    log.EnterOutTime = logs[i].EnterOutTime;
                    log.ReadingRoomName = logs[i].ReadingRoomName;
                    log.Remark = logs[i].Remark;
                    log.SeatID = logs[i].SeatID;
                    jm_logs.Add(log);
                }
                return SeatManageComm.JSONSerializer.Serialize(jm_logs);
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

        public string GetReaderChooseSeatRecord(string cardNum, int pageIndex, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "输入的学号不能空";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "页面和每行显示数目必须大于等于0";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                DateTime endDate = DateTime.Now;
                List<SeatManage.ClassModel.EnterOutLogInfo> logs = seatDataService.GetEnterOutLogsByPage(cardNum, pageIndex, pageSize);
                List<JM_EnterOutLog> jm_logs = new List<JM_EnterOutLog>();
                for (int i = 0; i < logs.Count; i++)
                {
                    JM_EnterOutLog log = new JM_EnterOutLog();

                    log.EnterOutState = logs[i].EnterOutState.ToString();
                    log.EnterOutTime = logs[i].EnterOutTime.ToString("yyyy-MM-dd HH:mm:ss");
                    log.Id = logs[i].EnterOutLogID;
                    log.Remark = logs[i].Remark;
                    log.RoomName = logs[i].ReadingRoomName;
                    log.RoomNum = logs[i].ReadingRoomNo;
                    log.SeatId = logs[i].SeatNo;
                    log.SeatNum = logs[i].ShortSeatNo;

                    jm_logs.Add(log);
                }
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(jm_logs);
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
        /// 获取读者违规记录
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        public string GetReaderBlacklistRecord(string cardNum, int pageIndex, int pageSize)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "输入的学号不能空";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (pageIndex < 0 || pageSize < 0)
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "页面和每行显示数目必须大于等于0";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                List<BlackListInfo> logs = seatDataService.GetBlacklistInfosByPage(cardNum, pageIndex, pageSize);
                List<JM_Blacklist> jm_logs = new List<JM_Blacklist>();
                for (int i = 0; i < logs.Count; i++)
                {
                    JM_Blacklist log = new JM_Blacklist();
                    log.AddTime = logs[i].AddTime.ToString("yyyy-MM-dd HH:mm:ss");
                    log.CardNo = logs[i].CardNo;
                    log.ID = logs[i].ID;
                    switch (logs[i].BlacklistState)
                    {
                        case LogStatus.Valid:
                            log.IsValid = true;
                            break;
                        default:
                            log.IsValid = false;
                            break;
                    }
                    log.OutBlacklistMode = logs[i].OutBlacklistMode.ToString();
                    log.OutTime = logs[i].OutTime.ToString("yyyy-MM-dd HH:mm:ss");
                    log.ReMark = logs[i].ReMark;
                    jm_logs.Add(log);
                }
                return SeatManageComm.JSONSerializer.Serialize(jm_logs);
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
        /// 获取帐号、需要在客户端验证密码。验证成功返回用户基础信息，否则返回空
        /// </summary>
        /// <param name="cardNum"></param>
        /// <param name="beforeDays"></param>
        /// <returns></returns>
        public string GetReaderAccount(string cardNum, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNum.Trim()) || string.IsNullOrEmpty(password.Trim()))
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "用户名或密码不能为空!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                JM_ReaderInfo reader = null;
                SeatManage.ClassModel.UserInfo user = seatDataService.GetUserInfo(cardNum);
                if (user != null)
                {
                    string strPwd = SeatManageComm.MD5Algorithm.GetMD5Str32(password);
                    if (strPwd.Equals(user.Password))
                    {
                        reader = new JM_ReaderInfo();
                        reader.CardNo = user.LoginId;
                        reader.Name = user.UserName;
                        return SeatManageComm.JSONSerializer.Serialize(reader);
                    }
                    else
                    {
                        JM_HandleResult result = new JM_HandleResult();
                        result.Result = false;
                        result.Msg = "用户名或密码错误!";
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }
                else
                {
                    JM_HandleResult result = new JM_HandleResult();
                    result.Result = false;
                    result.Msg = "用户名或密码错误!";
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
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
    }
}
