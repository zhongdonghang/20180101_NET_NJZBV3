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

        public string QRcodeOperation(string codeStr, string studentNo)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                string[] scanResultArray = codeStr.Split('?');
                ClientCheckCodeParamModel scancode = null;
                if (scanResultArray.Length >= 2)
                {
                    string[] strParam = scanResultArray[1].Split('=');
                    if (strParam.Length >= 2)
                    {
                        scancode = ClientCheckCodeParamModel.Prase(strParam[1]);//兼容url的二维码。 
                    }
                }
                else
                {
                    scancode = ClientCheckCodeParamModel.Prase(codeStr);
                }
                DateTime ndt = DateTime.Now;
                if (scancode == null)
                {
                    result.Result = false;
                    result.Msg = "对不起，二维码错误!";
                    return JSONSerializer.Serialize(result);
                }
                if (scancode.CodeTime.AddMinutes(1) <= ndt)
                {
                    result.Result = false;
                    result.Msg = "对不起，二维码超时!";
                    return JSONSerializer.Serialize(result);
                }
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
                if (reader.EnterOutLog == null && reader.BespeakLog.Count < 1)
                {
                    result.Result = false;
                    result.Msg = "对不起，您还没有座位，请先选择或预约一个座位。";
                }

                if (reader.EnterOutLog != null)
                {
                    switch (reader.EnterOutLog.EnterOutState)
                    {
                        case EnterOutLogType.BookingConfirmation: //预约入座
                        case EnterOutLogType.SelectSeat: //选座
                        case EnterOutLogType.ContinuedTime: //续时
                        case EnterOutLogType.ComeBack: //暂离回来
                        case EnterOutLogType.ReselectSeat: //重新选座
                        case EnterOutLogType.WaitingSuccess: //等待入座
                            result.Result = false;
                            result.Msg = "您当前已有座位。";
                            break;
                        case EnterOutLogType.ShortLeave:
                            reader.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                            reader.EnterOutLog.Remark = string.Format("您在选座终端{0}扫描二维码恢复在座，本次暂离时长{1}分钟。", scancode.ClientNo, ((int)(ndt - reader.EnterOutLog.EnterOutTime).TotalMinutes));
                            reader.EnterOutLog.Flag = Operation.Reader;
                            reader.EnterOutLog.EnterOutTime = ndt;
                            result.Result = true;
                            int newId = -1;
                            if (seatManageDateService.AddEnterOutLogInfo(reader.EnterOutLog, ref newId) != HandleResult.Successed)
                            {
                                result.Result = false;
                                result.Msg = "对不起，暂离回来失败!";
                                return JSONSerializer.Serialize(result);
                            }
                            result.Result = true;
                            result.Msg = reader.EnterOutLog.Remark;
                            List<WaitSeatLogInfo> waitSeatLogs = seatManageDateService.GetWaitLogList("", reader.EnterOutLog.EnterOutLogID, null, null, null);
                            if (waitSeatLogs.Count > 0)
                            {
                                WaitSeatLogInfo waitSeatLog = waitSeatLogs[0];
                                waitSeatLog.NowState = LogStatus.Fail;
                                waitSeatLog.OperateType = Operation.OtherReader;
                                waitSeatLog.WaitingState = EnterOutLogType.WaitingCancel;
                                seatManageDateService.UpdateWaitLog(waitSeatLog);
                            }
                            break;
                        case EnterOutLogType.Leave:
                            result.Result = false;
                            result.Msg = "对不起，您还没有座位，请先选择或预约一个座位。";
                            break;
                        default:
                            result.Result = false;
                            result.Msg = "对不起，您还没有座位，请先选择或预约一个座位。";
                            break;
                    }
                    return JSONSerializer.Serialize(result);
                }
                ReadingRoomSetting set = reader.AtReadingRoom.Setting;
                DateTime dtBegin = reader.BespeakLog[0].BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd = reader.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                if (!DateTimeOperate.DateAccord(dtBegin, dtEnd, ndt) && (!set.SeatBespeak.NowDayBespeak || reader.BespeakLog[0].SubmitTime != reader.BespeakLog[0].BsepeakTime))
                {
                    result.Result = false;
                    result.Msg = "对不起，您预约的座位没有到达签到时间";
                    return JSONSerializer.Serialize(result);
                }
                EnterOutLogInfo seatUsedInfo = seatManageDateService.GetEnterOutLogInfoBySeatNum(reader.BespeakLog[0].SeatNo);
                if (seatUsedInfo != null && seatUsedInfo.EnterOutState != EnterOutLogType.Leave)
                {
                    //条件满足，说明座位正在使用。
                    seatUsedInfo.EnterOutState = EnterOutLogType.Leave;
                    seatUsedInfo.EnterOutType = LogStatus.Valid;
                    seatUsedInfo.Remark = "您正在使用的座位已被原预约的读者";
                    seatUsedInfo.Flag = Operation.OtherReader;
                    int newId = -1;
                    if (seatManageDateService.AddEnterOutLogInfo(seatUsedInfo, ref newId) != HandleResult.Successed)
                    {
                        result.Result = false;
                        result.Msg = "对不起，此阅览室尚未开放。";
                        return JSONSerializer.Serialize(result);
                    }

                    List<WaitSeatLogInfo> waitInfoList = seatManageDateService.GetWaitLogList(null, seatUsedInfo.EnterOutLogID, null, null, null);
                    if (waitInfoList.Count > 0)
                    {
                        WaitSeatLogInfo waitSeatLogModel = waitInfoList[0];
                        waitSeatLogModel.OperateType = Operation.Reader;
                        waitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                        waitSeatLogModel.NowState = LogStatus.Valid;
                        seatManageDateService.UpdateWaitLog(waitSeatLogModel);
                    }
                }
                EnterOutLogInfo newEnterOutLog = new EnterOutLogInfo();//构造 
                newEnterOutLog.CardNo = reader.BespeakLog[0].CardNo;
                newEnterOutLog.EnterOutLogNo = SeatComm.RndNum();
                newEnterOutLog.EnterOutState = EnterOutLogType.BookingConfirmation;
                newEnterOutLog.EnterOutType = LogStatus.Valid;
                newEnterOutLog.Flag = Operation.Reader;
                newEnterOutLog.ReadingRoomNo = reader.BespeakLog[0].ReadingRoomNo;
                newEnterOutLog.ReadingRoomName = reader.BespeakLog[0].ReadingRoomName;
                newEnterOutLog.ShortSeatNo = reader.BespeakLog[0].ShortSeatNum;
                newEnterOutLog.SeatNo = reader.BespeakLog[0].SeatNo;
                newEnterOutLog.Remark = string.Format("您在选座终端{0}扫描二维码，签到入座预约的{1} {2}号座位", scancode.ClientNo, reader.BespeakLog[0].ReadingRoomName, reader.BespeakLog[0].ShortSeatNum);

                int logid = -1;
                if (seatManageDateService.AddEnterOutLogInfo(newEnterOutLog, ref logid) == HandleResult.Successed)
                {
                    reader.BespeakLog[0].BsepeakState = BookingStatus.Confinmed;
                    reader.BespeakLog[0].CancelPerson = Operation.Reader;
                    reader.BespeakLog[0].CancelTime = ndt;
                    reader.BespeakLog[0].Remark = newEnterOutLog.Remark;
                    seatManageDateService.UpdateBespeakLogInfo(reader.BespeakLog[0]);
                }
                result.Result = true;
                result.Msg = newEnterOutLog.Remark;
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("读者签到/回来遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常";
                return JSONSerializer.Serialize(result);
            }
        }
        /// <summary>
        /// 获取二维码座位信息
        /// </summary>
        /// <param name="codeStr"></param>
        /// <returns></returns>
        public string QRcodeSeatInfo(string codeStr)
        {
            try
            {
                AJM_HandleResult result = new AJM_HandleResult();
                string[] scanResultArray = codeStr.Split('?');
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
                    scancode = ScanCodeParamModel.Prase(codeStr);
                }
                if (scancode == null)
                {
                    result.Result = false;
                    result.Msg = "二维码错误!";
                    return JSONSerializer.Serialize(result);
                }
                Seat seatInfo = seatManageDateService.GetSeatInfoBySeatNum(scancode.SeatNum);
                if (seatInfo == null)
                {
                    result.Result = false;
                    result.Msg = "此座位不存在!";
                    return JSONSerializer.Serialize(result);
                }
                AJM_SeatStatus jm_Seat = new AJM_SeatStatus();
                jm_Seat.SeatNo = seatInfo.SeatNo;
                jm_Seat.SeatShortNo = seatInfo.ShortSeatNo;
                jm_Seat.RoomName = seatInfo.ReadingRoom.Name;
                jm_Seat.RoomNo = seatInfo.ReadingRoom.No;
                jm_Seat.IsStopUse = seatInfo.IsSuspended;

                if (seatInfo.ReadingRoom.Setting.SeatBespeak.Used && !seatInfo.IsSuspended)
                {
                    DateTime ndt = DateTime.Now;
                    if (seatInfo.ReadingRoom.Setting.SeatBespeak.NowDayBespeak && seatInfo.SeatUsedState == EnterOutLogType.Leave)
                    {
                        jm_Seat.BespeakDate.Add(ndt.ToShortDateString(), seatInfo.ReadingRoom.Setting.GetBespeakTimeList(ndt).Select(t => t.ToShortTimeString()).ToList());
                    }
                    for (int i = 1; i <= seatInfo.ReadingRoom.Setting.SeatBespeak.BespeakBeforeDays; i++)
                    {
                        List<BespeakLogInfo> list = seatManageDateService.GetBespeakLogInfoBySeatNo(seatInfo.SeatNo, ndt.AddDays(i));
                        if (list.Count > 0 && !seatInfo.ReadingRoom.Setting.IsCanBespeakSeat(ndt.AddDays(i)))
                        {
                            continue;
                        }
                        jm_Seat.BespeakDate.Add(ndt.AddDays(i).ToShortDateString(), seatInfo.ReadingRoom.Setting.GetBespeakTimeList(ndt.AddDays(i)).Select(t => t.ToShortTimeString()).ToList());
                    }
                }
                result.Result = true;
                result.Msg = JSONSerializer.Serialize(jm_Seat);
                return JSONSerializer.Serialize(result);
            }
            catch (Exception ex)
            {
                WriteLog.Write("获取二维码信息遇到异常：" + ex.Message);
                AJM_HandleResult result = new AJM_HandleResult();
                result.Result = false;
                result.Msg = "执行遇到异常!";
                return JSONSerializer.Serialize(result);
            }
        }


      
    }
}
