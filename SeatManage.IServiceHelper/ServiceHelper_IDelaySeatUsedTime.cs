using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.JsonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SeatManage.ServiceHelper
{
    public partial class ServiceHelper : IDelaySeatUsedTime
    {
        public string GetDelaySet(string roomNum)
        {
            throw new NotImplementedException();
        }

        public string SubmitDelayResult(string cardNo)
        {
            try
            {
                JM_HandleResult result = new JM_HandleResult();
                result.Result = true;
                result.Msg = "";
                if (string.IsNullOrEmpty(cardNo))
                {
                    result.Result = false;
                    result.Msg = string.Format("输入的学号不能为空。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                DateTime nowDateTime = seatDataService.GetServerDateTime();
                ReaderInfo reader = seatDataService.GetReader(cardNo, true);
                if (reader == null)
                {
                    result.Result = false;
                    result.Msg = string.Format("获取读者信息失败。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                EnterOutLogInfo enterOutlog = reader.EnterOutLog;
                if (enterOutlog == null || enterOutlog.EnterOutState == EnterOutLogType.Leave)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，您尚未选择座位。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (enterOutlog.EnterOutState == EnterOutLogType.ShortLeave)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，您正处于暂离状态。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (!reader.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime || !reader.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，此阅览室尚未开放续时功能。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.CanContinuedTime.AddMinutes(reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) >= DateTime.Parse(reader.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime))
                {
                    result.Result = false;
                    result.Msg = string.Format("您的座位可以使用到闭馆时间{0}，无需再次续时。", reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString());
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.CanContinuedTime > nowDateTime)
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起,您的可续时时间未到,可续时时间为{0}到{1}。",
                        reader.CanContinuedTime.ToShortTimeString(),
                        reader.CanContinuedTime.AddMinutes(reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime).ToShortTimeString());
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
                if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0 && (reader.ContinuedTimeCount >= reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，您的续时次数不足。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }

                enterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;
                enterOutlog.Flag = Operation.Reader;
                enterOutlog.Remark = string.Format("在移动客户端延长{0} {1}号座位使用时间", reader.AtReadingRoom.Name, enterOutlog.ShortSeatNo);
                int newLogId = -1;
                if (seatDataService.AddEnterOutLogInfo(enterOutlog, ref newLogId) == HandleResult.Successed)
                {
                    string LastCount = "";
                    string StartTime = "";
                    string EndTime = "";
                    string SingleTime = "";
                    DateTime dt = new DateTime();
                    if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                    {
                        LastCount = (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - reader.ContinuedTimeCount - 1).ToString();
                    }
                    if (reader.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                    {

                        dt = nowDateTime.AddMinutes(reader.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);


                        if (dt > reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                        {
                            dt = reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                        }
                        else
                        {
                            StartTime = (dt.AddMinutes(-reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                            EndTime = dt.ToShortTimeString();
                        }
                    }
                    else
                    {
                        for (int i = 0; i < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                        {
                            if (nowDateTime < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                            {
                                if (i + 1 < reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    dt = reader.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                    StartTime = (dt.AddMinutes(-reader.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                    EndTime = dt.ToShortTimeString();
                                }
                                else
                                {
                                    dt = reader.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                                break;
                            }
                        }
                    }
                    SingleTime = dt.ToShortTimeString();
                    if (string.IsNullOrEmpty(StartTime))
                    {
                        result.Result = false;
                        result.Msg = string.Format("延长{0} {1}号座位使用时间到{2}，此时间为闭馆时间无需再次续时。", reader.AtReadingRoom.Name, enterOutlog.ShortSeatNo, SingleTime);
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                    else
                    {
                        result.Result = false;
                        result.Msg = string.Format("延长{0} {1}号座位使用时间到{2}，下次续时时间为{3}到{4}{5}。", reader.AtReadingRoom.Name, enterOutlog.ShortSeatNo, SingleTime, StartTime, EndTime, LastCount == "" ? "" : "剩余续时次数" + LastCount + "次");
                        return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                    }
                }
                else
                {
                    result.Result = false;
                    result.Msg = string.Format("对不起，续时失败，请重新尝试。");
                    return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("座位续时遇到异常：" + ex.Message);
                JM_HandleResult result = new JM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return SeatManage.SeatManageComm.JSONSerializer.Serialize(result);
            }
        }
    }
}
