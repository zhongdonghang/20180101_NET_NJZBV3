using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatClientLeave.Code;
using SeatManage.Bll;
using SeatManage.ClassModel;
namespace SeatClientLeave
{
    /// <summary>
    /// 处理之后的消息委托
    /// </summary>
    /// <param name="m"></param>
    public delegate void HandleMessage(string m, SeatManage.EnumType.HandleResult r);
    public class RouteResultHandle
    {
        /// <summary>
        /// 处理结果
        /// </summary>
        public event HandleMessage HandleResult;
        /// <summary>
        /// 刷卡操作对象
        /// </summary>
        SeatClientLeave.Code.LeaveClientObject clientobject = SeatClientLeave.Code.LeaveClientObject.GetInstance();
        private RouteResultHandle()
        { }
        static RouteResultHandle handle = null;
        static object _object = new object();
        public static RouteResultHandle GetInstance()
        {
            if (handle == null)
            {
                lock (_object)
                {
                    if (handle == null)
                    {
                        return handle = new RouteResultHandle();
                    }
                }
            }
            return handle;
        }

        /// <summary>
        /// 释放座位
        /// </summary>
        public void Leave()
        {

            int newLogId = -1;
            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在离开终端刷卡释放{0} {1}号座位",
                clientobject.ReaderInfo.EnterOutLog.ReadingRoomName,
                clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
            try
            {
                SeatManage.EnumType.HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    if (HandleResult != null)
                    {
                        HandleResult("    成功释放座位！", SeatManage.EnumType.HandleResult.Successed);
                    }
                }
                else
                {
                    if (HandleResult != null)
                    {
                        HandleResult("    座位释放失败，请重试！", SeatManage.EnumType.HandleResult.Failed);
                    }
                }
            }
            catch
                (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("读者{0}释放座位操作失败：{1}", clientobject.ReaderInfo.CardNo, ex.Message));
                if (HandleResult != null)
                {
                    HandleResult("    座位释放失败，请重试！", SeatManage.EnumType.HandleResult.Failed);
                }
            }

        }
        /// <summary>
        /// 暂时离开
        /// </summary>
        public void ShortLeave()
        {
            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
            int newLogId = -1;
            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在暂离终端刷卡暂离，保留{1} {2}号座位{3}分钟",
                clientobject.ClientSetting.ClientNo,
                clientobject.ReaderInfo.AtReadingRoom.Name,
                clientobject.ReaderInfo.EnterOutLog.ShortSeatNo,
                NowReadingRoomState.GetSeatHoldTime(clientobject.ReaderInfo.AtReadingRoom.Setting.SeatHoldTime, ServiceDateTime.Now));
            try
            {
                SeatManage.EnumType.HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    if (HandleResult != null)
                    {
                        HandleResult(string.Format("   谢谢配合，请在{0}分钟内回来刷卡。", NowReadingRoomState.GetSeatHoldTime(clientobject.ReaderInfo.AtReadingRoom.Setting.SeatHoldTime, ServiceDateTime.Now)), SeatManage.EnumType.HandleResult.Successed);
                    }
                }
                else
                {
                    if (HandleResult != null)
                    {
                        HandleResult("    操作失败，请重试。", SeatManage.EnumType.HandleResult.Failed);
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("读者{0}暂离操作失败：{1}", clientobject.ReaderInfo.CardNo, ex.Message));
                if (HandleResult != null)
                {
                    HandleResult("    操作失败，请重试。", SeatManage.EnumType.HandleResult.Failed);
                }
            }
        }
        /// <summary>
        /// 续时
        /// </summary>
        public void ContinuedTime()
        {
            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ContinuedTime;//设置读者状态为暂回来
            clientobject.ReaderInfo.EnterOutLog.Flag = SeatManage.EnumType.Operation.Reader;
            int newLogId = -1;
            if (clientobject.ReaderInfo.CanContinuedTime > ServiceDateTime.Now)
            {
                HandleResult(string.Format("    在座时间过短，请在{0}后续时。", clientobject.ReaderInfo.CanContinuedTime.ToShortTimeString()), SeatManage.EnumType.HandleResult.Successed);
                return;
            }
            if (clientobject.ReaderInfo.ContinuedTimeCount != 0 && (clientobject.ReaderInfo.ContinuedTimeCount >= clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
            {
                HandleResult("    续时次数不足，请重新选座。", SeatManage.EnumType.HandleResult.Successed);
                return;
            }
            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在续时终端刷卡延长{0}，{1}号座位使用时间", clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
            SeatManage.EnumType.HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
            if (result == SeatManage.EnumType.HandleResult.Successed)
            {
                HandleResult(string.Format("    续时成功，延长{0}，{1}号座位使用时间。", clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                return;
            }
            else
            {
                //TODO:错误提示
            }
        }
        /// <summary>
        ///暂离回来
        /// </summary>
        public void CometoBack()
        {
            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ComeBack;//设置读者状态为暂回来
            clientobject.ReaderInfo.EnterOutLog.Flag = SeatManage.EnumType.Operation.Reader;
            List<WaitSeatLogInfo> waitSeatLogs = T_SM_SeatWaiting.GetWaitSeatList("", clientobject.ReaderInfo.EnterOutLog.EnterOutLogID, null, null, null);
            WaitSeatLogInfo waitSeatLog = null;
            string clientType = "终端";
            switch (LeaveClientSetting.LeaveState)
            {
                case LeaveState.FreeSeat:
                    clientType = "离开终端";
                    break;
                case LeaveState.ShortLeave:
                    clientType = "暂离终端";
                    break;
                case LeaveState.ContinuedTime:
                    clientType = "续时终端";
                    break;
                case LeaveState.Choose:
                    clientType = "自助终端";
                    break;

            }
            int newLogId = -1;
            System.TimeSpan shortleavetimelong = ServiceDateTime.Now - clientobject.ReaderInfo.EnterOutLog.EnterOutTime;
            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离时长{1}分钟，继续使用{2} {3}号座位",
                clientType,
                shortleavetimelong.Minutes,
                clientobject.ReaderInfo.EnterOutLog.ReadingRoomName,
                clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
            try
            {
                SeatManage.EnumType.HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    if (waitSeatLogs.Count > 0)
                    {
                        waitSeatLog = waitSeatLogs[0];
                        waitSeatLog.NowState = SeatManage.EnumType.LogStatus.Fail;
                        waitSeatLog.OperateType = SeatManage.EnumType.Operation.OtherReader;
                        waitSeatLog.WaitingState = SeatManage.EnumType.EnterOutLogType.WaitingCancel;
                        T_SM_SeatWaiting.UpdateWaitLog(waitSeatLog);//取消等待
                    }
                    if (HandleResult != null)
                    {
                        HandleResult(string.Format("    欢迎回来，您的座位在{0} {1}号。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                        if (clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                        {
                            List<SeatManage.EnumType.EnterOutLogType> typeList = new List<SeatManage.EnumType.EnterOutLogType>();
                            typeList.Add(SeatManage.EnumType.EnterOutLogType.BookingConfirmation);
                            typeList.Add(SeatManage.EnumType.EnterOutLogType.SelectSeat);
                            typeList.Add(SeatManage.EnumType.EnterOutLogType.ReselectSeat);
                            typeList.Add(SeatManage.EnumType.EnterOutLogType.WaitingSuccess);
                            typeList.Add(SeatManage.EnumType.EnterOutLogType.ContinuedTime);
                            List<EnterOutLogInfo> seatTimeEnterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogByNo(clientobject.ReaderInfo.EnterOutLog.EnterOutLogNo, typeList, 1);
                            if (seatTimeEnterOutLog.Count > 0)
                            {

                                if (seatTimeEnterOutLog[0].EnterOutState == SeatManage.EnumType.EnterOutLogType.ContinuedTime)
                                {
                                    if (seatTimeEnterOutLog[0].EnterOutTime.AddMinutes(clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength) < ServiceDateTime.Now)
                                    {
                                        if (clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes == 0 || clientobject.ReaderInfo.ContinuedTimeCount < clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes)
                                        {
                                            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ContinuedTime;
                                            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离期间在座超时，系统自动延长{1}，{2}号座位使用时间", clientType, clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
                                            result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                                            if (result == SeatManage.EnumType.HandleResult.Successed)
                                            {
                                                HandleResult(string.Format("    暂离期间在座超时，系统自动延长{0}，{1}号座位使用时间。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                                            }
                                            else
                                            {
                                                //TODO:错误提示
                                            }
                                        }
                                        else
                                        {

                                            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{1}，{2}号座位", clientType, clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
                                            result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                                            if (result == SeatManage.EnumType.HandleResult.Successed)
                                            {
                                                HandleResult(string.Format("    暂离期间在座超时，续时次数已用完，系统自动释放{0}，{1}号座位。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                                            }
                                            else
                                            {
                                                //TODO:错误提示
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (seatTimeEnterOutLog[0].EnterOutTime.AddMinutes(clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength) < ServiceDateTime.Now)
                                    {
                                        if (clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
                                        {
                                            if (clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes == 0 || clientobject.ReaderInfo.ContinuedTimeCount < clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes)
                                            {
                                                clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ContinuedTime;
                                                clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                                clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离期间在座超时，系统自动延长{1}，{2}号座位使用时间", clientType, clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
                                                result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                                                if (result == SeatManage.EnumType.HandleResult.Successed)
                                                {
                                                    HandleResult(string.Format("    暂离期间在座超时，系统自动延长{0}，{1}号座位使用时间。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                                                }
                                                else
                                                {
                                                    //TODO:错误提示
                                                }
                                            }
                                            else
                                            {
                                                clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                                clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                                clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{1}，{2}号座位", clientType, clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
                                                result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                                                if (result == SeatManage.EnumType.HandleResult.Successed)
                                                {
                                                    HandleResult(string.Format("    暂离期间在座超时，续时次数已用完，系统自动释放{0}，{1}号座位。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                                                }
                                                else
                                                {
                                                    //TODO:错误提示
                                                }
                                            }
                                        }
                                        else
                                        {
                                            clientobject.ReaderInfo.EnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                            clientobject.ReaderInfo.EnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                            clientobject.ReaderInfo.EnterOutLog.Remark = string.Format("在{0}刷卡暂离回来，暂离期间在座超时，系统自动释放{1}，{2}号座位", clientType, clientobject.ReaderInfo.AtReadingRoom.Name, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo);
                                            result = EnterOutOperate.AddEnterOutLog(clientobject.ReaderInfo.EnterOutLog, ref newLogId);//插入进出记录
                                            if (result == SeatManage.EnumType.HandleResult.Successed)
                                            {
                                                HandleResult(string.Format("    暂离期间在座超时，系统自动释放{0}，{1}号座位。", clientobject.ReaderInfo.EnterOutLog.ReadingRoomName, clientobject.ReaderInfo.EnterOutLog.ShortSeatNo), SeatManage.EnumType.HandleResult.Successed);
                                            }
                                            else
                                            {
                                                //TODO:错误提示
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (HandleResult != null)
                    {
                        HandleResult("操作失败！", SeatManage.EnumType.HandleResult.Failed);
                    }
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("读者{0}暂离回来操作失败：{1}", clientobject.ReaderInfo.CardNo, ex.Message));
                if (HandleResult != null)
                {
                    HandleResult("操作失败！", SeatManage.EnumType.HandleResult.Failed);
                }
            }
        }
        /// <summary>
        /// 初始化操作
        /// </summary>
        public void Resetting()
        {
            clientobject.ReaderInfo = null;
            if (clientobject.ObjCardReader != null)
            {
                clientobject.ObjCardReader.Start();
            }
            GC.Collect();
        }
    }
}
