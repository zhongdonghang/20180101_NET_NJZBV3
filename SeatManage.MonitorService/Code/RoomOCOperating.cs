using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;

namespace SeatService.MonitorService.Code
{
    public partial class SeatDataOperation
    {
        public void ClearSeat()
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            try
            {
                //获取昨天的进出记录
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, null, null, enterOutLogTypeList, LogStatus.Valid, "1900-1-1", ServiceDateTime.Now.ToShortDateString());
                foreach (EnterOutLogInfo eol in eolList)
                {
                    if ((eol.EnterOutState == EnterOutLogType.ShortLeave) && (eol.Flag == Operation.Admin || eol.Flag == Operation.OtherReader))
                    {
                        //获取昨天的等待记录
                        List<WaitSeatLogInfo> wsllist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, new List<EnterOutLogType> { EnterOutLogType.Waiting });
                        if (wsllist.Count > 0)
                        {
                            wsllist[0].WaitingState = EnterOutLogType.WaitingCancel;
                            wsllist[0].StatsChangeTime = nowDateTime;
                            T_SM_SeatWaiting.UpdateWaitLog(wsllist[0]);
                        }
                    }
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = nowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，闭馆释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - roomList[eol.ReadingRoomNo].Setting.SeatNumAmount));
                    int logdi = 0;
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("监控服务：读者{0}，{1}", eol.CardNo, eol.Remark));
                }
                //预约记录处理
                TimeSpan span = nowDateTime - DateTime.Parse("2010-1-1");
                List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetBespeakList(null, null, nowDateTime.AddDays(-1), span.Days, new List<BookingStatus> { BookingStatus.Waiting });
                if (blilist.Count <= 0)
                {
                    return;
                }
                foreach (BespeakLogInfo bli in blilist.Where(bli => bli.BsepeakTime.Date < nowDateTime.Date))
                {
                    bli.CancelTime = bli.BsepeakTime.AddMinutes(int.Parse(roomList[bli.ReadingRoomNo].Setting.SeatBespeak.ConfirmTime.EndTime));
                    bli.CancelPerson = Operation.Service;
                    bli.BsepeakState = BookingStatus.Cencaled;
                    bli.Remark = string.Format("读者，在{0}，{1}号座位，预约，闭馆取消预约", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - roomList[bli.ReadingRoomNo].Setting.SeatNumAmount));
                    T_SM_SeatBespeak.UpdateBespeakList(bli);
                    WriteLog.Write(string.Format("监控服务：读者{0}，{1}", bli.CardNo, bli.Remark));
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：执行开馆处理遇到错误：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 执行阅览室开闭馆处理
        /// </summary>
        /// <param name="RoomsStatus">阅览室状态</param>
        public void OpenCloseReadingRoom()
        {
            try
            {
                DateTime nowDateTime = ServiceDateTime.Now;
                List<ReadingRoomOpenCloseLogInfo> rrocList = T_SM_RROpenCloseLog.GetReadingRoomOClog(null, LogStatus.Valid, null, null);
                //遍历所有阅览室
                foreach (ReadingRoomInfo rri in roomList.Values.Where(rri => rri.Setting != null))
                {

                    //获取阅览室状态
                    ReadingRoomOpenCloseLogInfo rroc = rrocList.OrderByDescending(u => u.OperateTime).ToList().Find(u => u.ReadingRoomNo == rri.No);
                    if (rroc == null)
                    {
                        rroc = new ReadingRoomOpenCloseLogInfo();
                        rroc.OpenCloseState = ReadingRoomStatus.Close;
                        rroc.ReadingRoomNo = rri.No;
                        rroc.OperateNo = SeatComm.RndNum();
                        rroc.OperateTime = nowDateTime;
                        rroc.Logstatus = LogStatus.Valid;
                    }

                    int new_id = 0;
                    //如果启用24小时模式
                    if (rri.Setting.RoomOpenSet.UninterruptibleModel)
                    {
                        if (rroc.OpenCloseState == ReadingRoomStatus.Close)
                        {
                            rroc.OpenCloseState = ReadingRoomStatus.Open;
                            rroc.OperateTime = nowDateTime;
                            T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                            WriteLog.Write(string.Format("监控服务：{0},开启", rri.Name));
                        }
                        continue;
                    }
                    ReadingRoomStatus nowState = rri.Setting.ReadingRoomOpenState(nowDateTime);
                    //判断状态
                    if (rroc.OpenCloseState == nowState)
                    {
                        continue;
                    }
                    switch (nowState)
                    {
                        case ReadingRoomStatus.Open:
                            rroc.OpenCloseState = ReadingRoomStatus.Open;
                            rroc.OperateTime = ServiceDateTime.Now;
                            rroc.OperateNo = SeatComm.RndNum();
                            T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                            WriteLog.Write(string.Format("监控服务：{0},开启", rri.Name));
                            break;
                        case ReadingRoomStatus.Close:
                            CloseReadingRoom(rri);
                            rroc.OpenCloseState = ReadingRoomStatus.Close;
                            rroc.OperateTime = ServiceDateTime.Now;
                            T_SM_RROpenCloseLog.AddNewReadingRoomOClog(rroc, ref new_id);
                            WriteLog.Write(string.Format("监控服务：{0},关闭", rri.Name));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：执行阅览室开闭馆处理遇到错误：{0}", ex.Message));
            }
        }

        /// <summary>
        /// 开馆处理
        /// </summary>
        /// <param name="readingRooms"></param>
        private void OpenReadingRoom(ReadingRoomInfo room)
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            try
            {
                //获取昨天的进出记录
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, room.No, null, enterOutLogTypeList, LogStatus.Valid, "1900-1-1", ServiceDateTime.Now.ToShortDateString());
                foreach (EnterOutLogInfo eol in eolList)
                {
                    if ((eol.EnterOutState == EnterOutLogType.ShortLeave) && (eol.Flag == Operation.Admin || eol.Flag == Operation.OtherReader))
                    {
                        //获取昨天的等待记录
                        List<WaitSeatLogInfo> wsllist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, new List<EnterOutLogType> { EnterOutLogType.Waiting });
                        if (wsllist.Count > 0)
                        {
                            wsllist[0].WaitingState = EnterOutLogType.WaitingCancel;
                            wsllist[0].StatsChangeTime = nowDateTime;
                            T_SM_SeatWaiting.UpdateWaitLog(wsllist[0]);
                        }
                    }
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = nowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，闭馆释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - room.Setting.SeatNumAmount));
                    int logdi = 0;
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);
                    WriteLog.Write(string.Format("监控服务：读者{0}，{1}", eol.CardNo, eol.Remark));
                }
                //预约记录处理
                TimeSpan span = nowDateTime - DateTime.Parse("2010-1-1");
                List<BespeakLogInfo> blilist = T_SM_SeatBespeak.GetBespeakList(null, room.No, nowDateTime.AddDays(-1), span.Days, new List<BookingStatus> { BookingStatus.Waiting });
                if (blilist.Count <= 0)
                {
                    return;
                }
                foreach (BespeakLogInfo bli in blilist.Where(bli => bli.BsepeakTime.Date < nowDateTime.Date))
                {
                    bli.CancelTime = bli.BsepeakTime.AddMinutes(int.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
                    bli.CancelPerson = Operation.Service;
                    bli.BsepeakState = BookingStatus.Cencaled;
                    bli.Remark = string.Format("读者，在{0}，{1}号座位，预约，闭馆取消预约", bli.ReadingRoomName, bli.SeatNo.Substring(bli.SeatNo.Length - room.Setting.SeatNumAmount));
                    T_SM_SeatBespeak.UpdateBespeakList(bli);
                    WriteLog.Write(string.Format("监控服务：读者{0}，{1}", bli.CardNo, bli.Remark));
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：执行开馆处理遇到错误：{0}", ex.Message));
            }
        }
        /// <summary>
        /// 闭馆处理
        /// </summary>
        /// <param name="readingRooms"></param>
        private void CloseReadingRoom(ReadingRoomInfo room)
        {
            DateTime nowDateTime = ServiceDateTime.Now;
            //添加记录状态
            try
            {
                //获取今天的进出记录
                List<EnterOutLogInfo> eolList = T_SM_EnterOutLog.GetEnterOutLogByStatus(null, room.No, null, enterOutLogTypeList, LogStatus.Valid, nowDateTime.ToShortDateString(), null);
                foreach (EnterOutLogInfo eol in eolList)
                {
                    if ((eol.EnterOutState == EnterOutLogType.ShortLeave) && (eol.Flag == Operation.Admin || eol.Flag == Operation.OtherReader))
                    {
                        //获取今天的的等待记录
                        List<WaitSeatLogInfo> wsllist = T_SM_SeatWaiting.GetWaitSeatList(null, eol.EnterOutLogID, null, null, new List<EnterOutLogType> {EnterOutLogType.Waiting});
                        if (wsllist.Count > 0)
                        {
                            wsllist[0].WaitingState = EnterOutLogType.WaitingCancel;
                            wsllist[0].StatsChangeTime = nowDateTime;
                            T_SM_SeatWaiting.UpdateWaitLog(wsllist[0]);
                        }
                    }
                    eol.EnterOutState = EnterOutLogType.Leave;
                    eol.EnterOutTime = nowDateTime;
                    eol.Flag = Operation.Service;
                    eol.Remark = string.Format("在{0}，{1}号座位，闭馆释放座位", eol.ReadingRoomName, eol.SeatNo.Substring(eol.SeatNo.Length - room.Setting.SeatNumAmount));
                    int logdi = 0;
                    EnterOutOperate.AddEnterOutLog(eol, ref logdi);


                    //PushMsgInfo msg = new PushMsgInfo();
                    //msg.Title = "您好，您的座位已被释放";
                    //msg.MsgType = MsgPushType.ToTime;
                    //msg.StudentNum = eol.CardNo;
                    //msg.Message = eol.Remark;
                    //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);

                    WriteLog.Write(string.Format("监控服务：读者{0}，{1}", eol.CardNo, eol.Remark));
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("监控服务：执行闭馆处理遇到错误：{0}", ex.Message));
            }
        }
    }
}
