using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatService.MonitorService.Code
{
    public partial class SeatDataOperation
    {
        /// <summary>
        /// 处理暂离和在座超时
        /// </summary>
        /// <param name="readingRooms"></param>
        public void EnterOutLogOperate()
        {
            try
            {
                DateTime nowDateTime = ServiceDateTime.Now;
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, null, null, enterOutLogTypeList, LogStatus.Valid, nowDateTime.ToShortDateString(), null);

                //遍历所有阅览室
                foreach (ReadingRoomInfo rri in roomList.Values.Where(rri => rri.Setting != null))
                {
                    DateTime seatOutTime = rri.Setting.RoomOpenSet.NowOpenTime(nowDateTime).AddMinutes(-rri.Setting.RoomOpenSet.OpenBeforeTimeLength);
                    if (rri.Setting.SeatUsedTimeLimit.Used && rri.Setting.SeatUsedTimeLimit.Mode == "Fixed")
                    {
                        foreach (DateTime ft in rri.Setting.SeatUsedTimeLimit.FixedTimes)
                        {
                            if (nowDateTime > ft)
                            {
                                seatOutTime = rri.Setting.SeatUsedTimeLimit.IsCanContinuedTime ? ft.AddMinutes(-rri.Setting.SeatUsedTimeLimit.CanDelayTime) : ft;
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //获取本阅览室的进出记录
                    foreach (EnterOutLogInfo eol in eolList.FindAll(u => u.ReadingRoomNo == rri.No))
                    {
                        switch (eol.EnterOutState)
                        {
                            case EnterOutLogType.SelectSeat:
                            case EnterOutLogType.ReselectSeat:
                            case EnterOutLogType.WaitingSuccess:
                            case EnterOutLogType.BookingConfirmation:
                                try
                                {
                                    if (rri.Setting.SeatUsedTimeLimit.Used)
                                    {
                                        //判断是否在座超时
                                        if (rri.Setting.SeatUsedTimeLimit.Mode == "Free")
                                        {
                                            if (eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.UsedTimeLength) < nowDateTime)
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, nowDateTime);
                                            }
                                            //else if (eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.UsedTimeLength - rri.Setting.SeatUsedTimeLimit.CanDelayTime) < nowDateTime)
                                            //{
                                            //    //推送续时提示
                                            //}
                                        }
                                        else if (rri.Setting.SeatUsedTimeLimit.Mode == "Fixed")
                                        {
                                            if (eol.EnterOutTime < seatOutTime)
                                            {
                                                SeatOverTimeOperator(rri.Setting, eol, nowDateTime);
                                            }
                                            //else if (eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.UsedTimeLength - rri.Setting.SeatUsedTimeLimit.CanDelayTime) < seatOutTime)
                                            //{

                                            //}
                                        }

                                    }
                                }
                                catch (Exception ex)
                                {
                                    WriteLog.Write(string.Format("监控服务：在座超时处理遇到异常{0}", ex.Message));
                                }
                                break;
                            case EnterOutLogType.ContinuedTime:
                                try
                                {
                                    //判断是否续时超时
                                    if (rri.Setting.SeatUsedTimeLimit.Used)
                                    {
                                        if (rri.Setting.SeatUsedTimeLimit.Mode == "Free" && (eol.EnterOutTime.AddMinutes(rri.Setting.SeatUsedTimeLimit.DelayTimeLength) < nowDateTime))
                                        {
                                            SeatOverTimeOperator(rri.Setting, eol, nowDateTime);
                                        }
                                        else if (rri.Setting.SeatUsedTimeLimit.Mode == "Fixed" && eol.EnterOutTime < seatOutTime)
                                        {
                                            SeatOverTimeOperator(rri.Setting, eol, nowDateTime);
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    WriteLog.Write(string.Format("监控服务：续时超时处理遇到异常{0}", ex.Message));
                                }
                                break;
                            case EnterOutLogType.ComeBack:
                                {
                                    if (rri.Setting.SeatUsedTimeLimit.Used)
                                    {
                                        //操作最后一条选座或续时的记录
                                        EnterOutLogInfo neweol = GetLastNoSeatTimeLog(eol);
                                        if (neweol != null)
                                        {
                                            eol.EnterOutTime = neweol.EnterOutTime;
                                            eol.EnterOutState = neweol.EnterOutState;
                                            if (eol.EnterOutState == EnterOutLogType.ContinuedTime)
                                            {
                                                goto case EnterOutLogType.ContinuedTime;
                                            }
                                            else
                                            {
                                                goto case EnterOutLogType.BookingConfirmation;
                                            }
                                        }
                                    }
                                    break;
                                }
                            case EnterOutLogType.ShortLeave:
                                {
                                    if (eol.Flag == Operation.OtherReader && rri.Setting.NoManagement.Used && (eol.EnterOutTime.AddMinutes(rri.Setting.GetSeatHoldTime(eol.EnterOutTime)) < nowDateTime))
                                    {
                                        //判断座位等待处理
                                        List<WaitSeatLogInfo> wslilist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, new List<EnterOutLogType> { EnterOutLogType.Waiting });
                                        if (wslilist.Count > 0)
                                        {
                                            WaitSeatOperate(rri, eol, wslilist, nowDateTime);
                                        }
                                    }
                                    else
                                    {
                                        if (eol.Flag == Operation.Admin)
                                        {
                                            if (rri.Setting.AdminShortLeave.IsUsed && (eol.EnterOutTime.AddMinutes(rri.Setting.AdminShortLeave.HoldTimeLength) < nowDateTime))
                                            {
                                                ShortLeaveOverTimeOperator(eol, rri.Setting, nowDateTime);
                                            }
                                            else if (!rri.Setting.AdminShortLeave.IsUsed && (eol.EnterOutTime.AddMinutes(NowReadingRoomState.GetSeatHoldTime(rri.Setting.SeatHoldTime, eol.EnterOutTime)) < nowDateTime))
                                            {
                                                ShortLeaveOverTimeOperator(eol, rri.Setting, nowDateTime);
                                            }
                                        }
                                        else if (eol.EnterOutTime.AddMinutes(NowReadingRoomState.GetSeatHoldTime(rri.Setting.SeatHoldTime, eol.EnterOutTime)) < nowDateTime)
                                        {
                                            ShortLeaveOverTimeOperator(eol, rri.Setting, nowDateTime);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：处理当前的进出记录失败{0}", ex.Message));
            }

        }

        /// <summary>
        /// 在座超时处理
        /// </summary>
        /// <param name="leavetype">处理类型</param>
        /// <param name="eol">进出记录</param>
        private static void SeatOverTimeOperator(ReadingRoomSetting roomSetting, EnterOutLogInfo eol, DateTime nowDateTime)
        {
            try
            {
                int logdi = 0;
                //在座超时处理
                switch (roomSetting.SeatUsedTimeLimit.OverTimeHandle)
                {
                    case EnterOutLogType.Leave:
                        eol.EnterOutState = EnterOutLogType.Leave;
                        eol.EnterOutTime = nowDateTime;
                        eol.Flag = Operation.Service;
                        eol.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomSetting.SeatNumAmount));
                        EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                        WriteLog.Write(string.Format("监控服务：读者{0}，{1}", eol.CardNo, eol.Remark));
                        //违规处理
                        if (roomSetting.IsRecordViolate)
                        {
                            AddViolationRecordByEnterOutLog(eol, ViolationRecordsType.SeatOutTime, string.Format("读者在{0}，{1}号座位，在座超时", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomSetting.SeatNumAmount)), roomSetting, nowDateTime);
                        }
                        break;
                    case EnterOutLogType.ShortLeave:
                        eol.EnterOutState = EnterOutLogType.ShortLeave;
                        eol.EnterOutTime = nowDateTime;
                        eol.Flag = Operation.Service;
                        eol.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务设置暂离", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomSetting.SeatNumAmount));
                        EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                        WriteLog.Write(string.Format("监控服务：读者{0}，{1}", eol.CardNo, eol.Remark));
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：处理读者在座超时发生错误：" + ex.Message));
            }
        }
        /// <summary>
        /// 暂离超时操作
        /// </summary>
        /// <param name="enterOutlog">进出记录</param>
        /// <param name="roomset">阅览室设置</param>
        private static void ShortLeaveOverTimeOperator(EnterOutLogInfo enterOutlog, ReadingRoomSetting readingRoomSetting, DateTime nowDateTime)
        {
            try
            {
                string vrremark = "";
                ViolationRecordsType vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                enterOutlog.EnterOutTime = nowDateTime;
                enterOutlog.EnterOutState = EnterOutLogType.Leave;
                switch (enterOutlog.Flag)
                {//TODO:记录管理员信息
                    case Operation.Admin:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，被管理员设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，被管理员设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveByAdminOutTime;
                        break;
                    case Operation.OtherReader:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，被其他读者设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，被其他读者设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveByReaderOutTime;
                        break;
                    case Operation.Reader:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                        break;
                    case Operation.Service:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，在座超时，监控服务设置暂离，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，在座超时，监控服务设置暂离，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.SeatOutTime;
                        break;
                    default:
                        enterOutlog.Remark = string.Format("在{0}，{1}号座位，暂离超时，被监控服务释放座位", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrremark = string.Format("读者在{0}，{1}号座位，暂离超时", enterOutlog.ReadingRoomName, enterOutlog.SeatNo.Substring(enterOutlog.SeatNo.Length - readingRoomSetting.SeatNumAmount));
                        vrtypt = ViolationRecordsType.ShortLeaveOutTime;
                        break;
                }
                //ReaderNoticeInfo notice = new ReaderNoticeInfo();
                //notice.CardNo = enterOutlog.CardNo;
                //if (enterOutlog.Flag == Operation.Service)//暂离记录为监控服务操作。
                //{
                //    notice.Type = NoticeType.SeatUsedTimeEnd;
                //    notice.Note = "在座超时，座位已经被释放，如果还需继续使用座位，请重新选座";
                //}
                //else
                //{
                //    notice.Type = NoticeType.SeatUsedTimeEnd;
                //    notice.Note = "暂离超时，座位已经被释放，如果还需继续使用座位，请重新选座";
                //}
                //T_SM_ReaderNotice.AddReaderNotice(notice);


                //PushMsgInfo msg = new PushMsgInfo();
                //msg.Title = "您好，您的座位已被释放";
                //msg.MsgType = MsgPushType.TimeOut;
                //msg.StudentNum = enterOutlog.CardNo;
                //msg.Message = enterOutlog.Remark;
                //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);


                enterOutlog.Flag = Operation.Service;
                int logid = 0;
                EnterOutOperate.AddEnterOutLog(enterOutlog, ref logid);
                WriteLog.Write(string.Format("读者{0}，{1}", enterOutlog.CardNo, enterOutlog.Remark));
                if (readingRoomSetting.IsRecordViolate)
                {
                    AddViolationRecordByEnterOutLog(enterOutlog, vrtypt, vrremark, readingRoomSetting, nowDateTime);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：处理读者暂离超时发生错误：" + ex.Message));
            }
        }

        /// <summary>
        /// 返回最后一条计时记录
        /// </summary>
        /// <param name="eol">进出记录</param>
        private static EnterOutLogInfo GetLastNoSeatTimeLog(EnterOutLogInfo eol)
        {
            try
            {
                List<EnterOutLogType> eoltypeList = new List<EnterOutLogType>();
                eoltypeList.Add(EnterOutLogType.BookingConfirmation);
                eoltypeList.Add(EnterOutLogType.SelectSeat);
                eoltypeList.Add(EnterOutLogType.ReselectSeat);
                eoltypeList.Add(EnterOutLogType.WaitingSuccess);
                eoltypeList.Add(EnterOutLogType.ContinuedTime);
                List<EnterOutLogInfo> lasteol = T_SM_EnterOutLog.GetEnterOutLogByNo(eol.EnterOutLogNo, eoltypeList, 1);
                if (lasteol.Count > 0)
                {
                    return lasteol[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：获取读者最后的续时记录发生错误：" + ex.Message));
                return null;
            }
        }
        /// <summary>
        /// 座位等待处理
        /// </summary>
        /// <param name="readingRoom">阅览室</param>
        /// <param name="enterOutLog">进出记录</param>
        /// <param name="waitSeatLoglist">等待记录列表</param>
        private static void WaitSeatOperate(ReadingRoomInfo readingRoom, EnterOutLogInfo enterOutLog, List<WaitSeatLogInfo> waitSeatLoglist, DateTime nowDateTime)
        {
            try
            {
                waitSeatLoglist[0].WaitingState = EnterOutLogType.WaitingSuccess;
                waitSeatLoglist[0].StatsChangeTime = nowDateTime;
                T_SM_SeatWaiting.UpdateWaitLog(waitSeatLoglist[0]);
                ReaderNoticeInfo notice = new ReaderNoticeInfo();

                //释放原读者座位
                int logid = 0;
                enterOutLog.Flag = Operation.Service;
                enterOutLog.EnterOutState = EnterOutLogType.Leave;
                enterOutLog.EnterOutTime = nowDateTime;
                enterOutLog.Remark = string.Format("在{0}，{1}号座位，被其他读者设置暂离，暂离超时，被监控服务释放座位", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount));
                EnterOutOperate.AddEnterOutLog(enterOutLog, ref logid);


                //notice.CardNo = enterOutLog.CardNo;
                //notice.Type = NoticeType.ShortLeaveTimeEndWarning;
                //notice.Note = "暂离超时，座位已被释放。";
                //T_SM_ReaderNotice.AddReaderNotice(notice);


                //PushMsgInfo msg = new PushMsgInfo();
                //msg.Title = "您好，您的座位已被释放";
                //msg.MsgType = MsgPushType.TimeOut;
                //msg.StudentNum = enterOutLog.CardNo;
                //msg.Message = enterOutLog.Remark;
                //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);


                WriteLog.Write(string.Format("监控服务：读者{0}，{1}", enterOutLog.CardNo, enterOutLog.Remark));
                //等待读者入座
                EnterOutLogInfo new_eol = new EnterOutLogInfo();
                new_eol.CardNo = waitSeatLoglist[0].CardNo;
                new_eol.EnterOutLogNo = SeatComm.RndNum();
                new_eol.EnterOutState = EnterOutLogType.WaitingSuccess;
                new_eol.EnterOutType = LogStatus.Valid;
                new_eol.ReadingRoomNo = waitSeatLoglist[0].ReadingRoomNo;
                new_eol.Flag = Operation.Service;
                new_eol.SeatNo = enterOutLog.SeatNo;
                new_eol.Remark = string.Format("在{0}，{1}号座位，等待成功，自动入座", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount));
                EnterOutOperate.AddEnterOutLog(new_eol, ref logid);


                //notice.CardNo = enterOutLog.CardNo;
                //notice.Type = NoticeType.WaitSeatSuccess;
                //notice.Note = "您等待的座位已经分配给您。";
                //T_SM_ReaderNotice.AddReaderNotice(notice);

                //msg = new PushMsgInfo();
                //msg.Title = "您好，您已等待成功";
                //msg.MsgType = MsgPushType.ToTime;
                //msg.StudentNum = enterOutLog.CardNo;
                //msg.Message = new_eol.Remark;
                //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);



                WriteLog.Write(string.Format("监控服务：读者{0}，{1}", new_eol.CardNo, new_eol.Remark));
                if (readingRoom.Setting.IsRecordViolate)
                {
                    AddViolationRecordByEnterOutLog(enterOutLog, ViolationRecordsType.ShortLeaveByReaderOutTime, string.Format("读者在{0}，{1}号座位，被其他读者设置暂离，暂离超时", enterOutLog.ReadingRoomName, enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - readingRoom.Setting.SeatNumAmount)), readingRoom.Setting, nowDateTime);
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：处理等待读者发生错误：" + ex.Message));
            }
        }
    }
}
