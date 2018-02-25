using SeatClientV3.OperateResult;
using System;
using System.Collections.Generic;
using System.Linq;
using SeatClientV3.WindowObject;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatClientV3.Code
{
    internal class ReadCardOperator
    {
        private ReadCardOperator()
        {

        }
        /// <summary>
        /// 打印
        /// </summary>
        PrintSlip printer = PrintSlip.GetInstance();
        static ReadCardOperator handle = null;
        static object _object = new object();
        /// <summary>
        /// 获得单个SystemObject实例
        /// </summary>
        /// <returns></returns>
        public static ReadCardOperator GetInstance()
        {
            if (handle == null)
            {
                lock (_object)
                {
                    if (handle == null)
                    {
                        return handle = new ReadCardOperator();
                    }
                }
            }
            return handle;
        }
        SystemObject Clientobject = SystemObject.GetInstance();
        /// <summary>
        /// 选座操作
        /// </summary>
        public void ChooseSeat()
        {
            //进出记录相关属性赋值。
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutLogNo = SeatComm.RndNum();
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.SelectSeat;
            Clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Reader;
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutType = LogStatus.Valid;
            ReadingRoomWindowObject.GetInstance().Window.ShowMessage();
            //阅览室选座操作为退出，结束选座流程
            if (Clientobject.EnterOutLogData.FlowControl == ClientOperation.Exit)
            {
                return;
            }
            //返回为自动选座
            if (string.IsNullOrEmpty(Clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo) || string.IsNullOrEmpty(Clientobject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                //如果阅览室编号或者座位号为空，则不执行插入操作。
                if (!string.IsNullOrEmpty(Clientobject.EnterOutLogData.EnterOutlog.SeatNo))
                {
                    T_SM_Seat.UnLockSeat(Clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                }
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                return;
            }
            int newLogId = -1;
            if (EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Failed)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                return;
            }
            T_SM_Seat.UnLockSeat(Clientobject.EnterOutLogData.EnterOutlog.SeatNo);//记录插入成功，解锁座位

            if (Clientobject.EnterOutLogData.FlowControl == ClientOperation.WaitSeat)
            {
                //SeatManage.ClassModel.ReaderNoticeInfo rni = new SeatManage.ClassModel.ReaderNoticeInfo();
                //rni.CardNo = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                //rni.Type = NoticeType.OtherSetShortLeaveWarning;
                //rni.Note = Clientobject.EnterOutLogData.EnterOutlog.Remark;
                //SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(rni);

                //PushMsgInfo msg = new PushMsgInfo();
                //msg.Title = "您好，您已被设置为暂离";
                //msg.MsgType = MsgPushType.OtherUser;
                //msg.StudentNum = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                //msg.Message = Clientobject.EnterOutLogData.EnterOutlog.Remark;
                //SeatManage.Bll.T_SM_ReaderNotice.SendPushMsg(msg);

                //TODO:添加等待记录
                Clientobject.EnterOutLogData.WaitSeatLogModel.EnterOutLogID = newLogId;
                if (T_SM_SeatWaiting.AddWaitSeatLog(Clientobject.EnterOutLogData.WaitSeatLogModel) > 0)
                {
                    PrintData data;
                    switch (Clientobject.ClientSetting.DeviceSetting.UsingPrintSlip)
                    {
                        case PrintSlipMode.AutoPrint:
                            data = new PrintData();
                            data.CardNo = Clientobject.EnterOutLogData.WaitSeatLogModel.CardNo;
                            data.EnterTime = ServiceDateTime.Now;
                            data.ReaderName = Clientobject.EnterOutLogData.Student.Name;
                            data.ReadingRoomName = Clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                            data.SeatNo = SeatComm.SeatNoToShortSeatNo(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, Clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                            data.SecCardNo = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                            data.WaitEndDateTime = data.EnterTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.GetSeatHoldTime(data.EnterTime));

                            printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                            break;
                        case PrintSlipMode.UserChoose:
                            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.PrintConfIrm);
                            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
                            {
                                data = new PrintData();
                                data.CardNo = Clientobject.EnterOutLogData.WaitSeatLogModel.CardNo;
                                data.EnterTime = ServiceDateTime.Now;
                                data.ReaderName = Clientobject.EnterOutLogData.Student.Name;
                                data.ReadingRoomName = Clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                                data.SeatNo = SeatComm.SeatNoToShortSeatNo(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, Clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                                data.SecCardNo = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                                data.WaitEndDateTime = data.EnterTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.GetSeatHoldTime(data.EnterTime));

                                printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                            }
                            break;
                        case PrintSlipMode.NotPrint:
                            break;
                    }
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.WaitSeatSuccess);
                }
                else
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                }
            }
            else
            {
                PrintData data;
                switch (Clientobject.ClientSetting.DeviceSetting.UsingPrintSlip)
                {
                    case PrintSlipMode.AutoPrint:
                        data = new PrintData();
                        data.CardNo = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                        data.EnterTime = ServiceDateTime.Now;
                        data.ReaderName = Clientobject.EnterOutLogData.Student.Name;
                        data.ReadingRoomName = Clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        data.SeatNo = SeatComm.SeatNoToShortSeatNo(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, Clientobject.EnterOutLogData.EnterOutlog.SeatNo);

                        printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                        break;
                    case PrintSlipMode.UserChoose:
                        PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.PrintConfIrm);
                        if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
                        {
                            data = new PrintData();
                            data.CardNo = Clientobject.EnterOutLogData.EnterOutlog.CardNo;
                            data.EnterTime = ServiceDateTime.Now;
                            data.ReaderName = Clientobject.EnterOutLogData.Student.Name;
                            data.ReadingRoomName = Clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                            data.SeatNo = SeatComm.SeatNoToShortSeatNo(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, Clientobject.EnterOutLogData.EnterOutlog.SeatNo);

                            printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                        }
                        break;
                    case PrintSlipMode.NotPrint:
                        break;
                }
                //提示选座成功
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatResult);
            }

        }

        /// <summary>
        /// 离开选择操作
        /// </summary>
        public void LeaveOperate()
        {
            LeaveWindowObject.GetInstance().Window.ShowMessage();
            //判断读者选择的离开方式
            Clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Reader;
            switch (Clientobject.EnterOutLogData.FlowControl)
            {
                case ClientOperation.ContinuedTime://读者选择续时操作
                    Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;//设置读者状态为续时
                    ContinuedTime();
                    break;
                case ClientOperation.Leave://读者选择离开操作
                    Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;//设置读者状态为离开
                    ReleaseSeat();
                    break;
                case ClientOperation.ReSelectSeat:
                    ChooseSeat();
                    return;//如果是重新选座，则结束方法
                case ClientOperation.ShortLeave://读者选择暂离操作
                    Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ShortLeave;//设置读者状态为暂离
                    ShortLeave();
                    break;
            }
        }

        /// <summary>
        /// 续时
        /// </summary>
        public void ContinuedTime()
        {
            int newLogId = -1;
            //if (Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
            //{
            //    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinuedTime);

            //}
            //else
            //{
            //    if (Clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) >= DateTime.Parse(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime))
            //    {
            //        PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinuedTimeWithout);
            //        return;
            //    }
            //}
            if (Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanNotContinuedWithBookNetFixed && Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.Used && Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.SpecifiedBespeak && Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.SpecifiedTime)
            {
                List<BespeakLogInfo> bookList = T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(Clientobject.EnterOutLogData.EnterOutlog.SeatNo, ServiceDateTime.Now);
                if (bookList.Any(log => Clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) == log.BsepeakTime))
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinueWithBookLog);
                    return;
                }
            }
            if (Clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) >= DateTime.Parse(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinuedTimeWithout);
                return;
            }
            if (Clientobject.EnterOutLogData.Student.CanContinuedTime > ServiceDateTime.Now)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinuedTimeNotTime);
                return;
            }
            if (Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0 && (Clientobject.EnterOutLogData.Student.ContinuedTimeCount >= Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ContinuedTimeNoCount);
                return;
            }

            Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡延长{1} {2}号座位使用时间", Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.Student.AtReadingRoom.Name, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
            HandleResult result = EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            PopupWindowsObject.GetInstance().Window.ShowMessage(result == HandleResult.Successed ? TipType.ContinuedTime : TipType.Exception);
        }
        /// <summary>
        /// 暂离
        /// </summary>
        public void ShortLeave()
        {
            Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
            int newLogId = -1;
            Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂时离开，保留{1} {2}号座位{3}分钟", Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.Student.AtReadingRoom.Name, Clientobject.EnterOutLogData.Student.EnterOutLog.ShortSeatNo, NowReadingRoomState.GetSeatHoldTime(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime, ServiceDateTime.Now));
            PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Successed ? TipType.ShortLeave : TipType.Exception);
        }
        /// <summary>
        /// 暂离回来操作
        /// </summary>
        public void CometoBack()
        {
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ComeBack;//设置读者状态为暂回来
            Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
            TimeSpan shortleavetimelong = ServiceDateTime.Now - Clientobject.EnterOutLogData.EnterOutlog.EnterOutTime;
            Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离时长{1}分钟，继续使用{2} {3}号座位", Clientobject.ClientSetting.ClientNo, shortleavetimelong.Minutes, Clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            int newLogId = -1;
            PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Failed ? TipType.Exception : TipType.ComeToBack);
            if (Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.NoManagement.Used)
            {
                List<WaitSeatLogInfo> waitSeatLogs = T_SM_SeatWaiting.GetWaitSeatList("", Clientobject.EnterOutLogData.EnterOutlog.EnterOutLogID, null, null, null);
                if (waitSeatLogs.Count > 0)
                {
                    WaitSeatLogInfo waitSeatLog = waitSeatLogs[0];
                    waitSeatLog.NowState = LogStatus.Fail;
                    waitSeatLog.OperateType = Operation.OtherReader;
                    waitSeatLog.WaitingState = EnterOutLogType.WaitingCancel;
                    T_SM_SeatWaiting.UpdateWaitLog(waitSeatLog);
                }
            }
            if (!Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
            {
                return;
            }
            if (Clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) > ServiceDateTime.Now)
            {
                return;
            }

            if (Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes == 0 || Clientobject.EnterOutLogData.Student.ContinuedTimeCount < Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes)
            {
                Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;
                Clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Service;
                Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
                Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，系统自动延长{1} {2}号座位使用时间", Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.Student.AtReadingRoom.Name, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);

                PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Successed ? TipType.AutoContinuedTime : TipType.Exception);
            }
            else
            {

                Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                Clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Service;
                Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
                Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{1} {2}号座位", Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.Student.AtReadingRoom.Name, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);

                PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Successed ? TipType.AutoContinuedTimeNoCount : TipType.Exception);
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        public void ReleaseSeat()
        {
            int newLogId = -1;
            Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
            Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡释放{1} {2}号座位", Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newLogId) == HandleResult.Successed ? TipType.Leave : TipType.Exception);
        }

        /// <summary>
        /// 预约等待
        /// </summary>
        public void BespeakCheck()
        {
            if (Clientobject.EnterOutLogData.Student.BespeakLog.Count <= 0)
            {
                return;
            }
            BespeakLogInfo bespeaklog = Clientobject.EnterOutLogData.Student.BespeakLog[0];
            ReadingRoomSetting set = Clientobject.EnterOutLogData.Student.AtReadingRoom.Setting;
            DateTime dtBegin = bespeaklog.BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
            DateTime dtEnd = bespeaklog.BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
            DateTime nowDate = ServiceDateTime.Now;
            if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowDate) || (set.SeatBespeak.NowDayBespeak && bespeaklog.SubmitTime == bespeaklog.BsepeakTime))
            {
                //TODO:预约时间在开始时间和结束时间之间，执行预约确认操作
                //TODO:预约确认时，判断当前座位上是否有人。
                EnterOutLogInfo seatUsedInfo = T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(bespeaklog.SeatNo);
                if (seatUsedInfo != null && seatUsedInfo.EnterOutState != EnterOutLogType.Leave)
                { //条件满足，说明座位正在使用。
                    seatUsedInfo.EnterOutState = EnterOutLogType.Leave;
                    seatUsedInfo.EnterOutType = LogStatus.Valid;
                    seatUsedInfo.TerminalNum = Clientobject.ClientSetting.ClientNo;
                    seatUsedInfo.Remark = string.Format("预约该座位的读者在终端{0}刷卡确认入座，设置在座读者离开", Clientobject.ClientSetting.ClientNo);
                    seatUsedInfo.Flag = Operation.OtherReader;
                    int newId = -1;
                    if (EnterOutOperate.AddEnterOutLog(seatUsedInfo, ref newId) == HandleResult.Successed)
                    {
                        List<WaitSeatLogInfo> waitInfoList = T_SM_SeatWaiting.GetWaitSeatList(null, seatUsedInfo.EnterOutLogID, null, null, null);
                        if (waitInfoList.Count > 0)
                        {
                            Clientobject.EnterOutLogData.WaitSeatLogModel = waitInfoList[0];
                            Clientobject.EnterOutLogData.WaitSeatLogModel.OperateType = Operation.Reader;
                            Clientobject.EnterOutLogData.WaitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                            Clientobject.EnterOutLogData.WaitSeatLogModel.NowState = LogStatus.Valid;
                            if (!T_SM_SeatWaiting.UpdateWaitLog(Clientobject.EnterOutLogData.WaitSeatLogModel))
                            {
                                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                                return;
                            }
                        }
                    }
                    else
                    {
                        PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                        return;
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
                newEnterOutLog.TerminalNum = Clientobject.ClientSetting.ClientNo;
                newEnterOutLog.Remark = string.Format("在终端{0}刷卡，入座预约的{1} {2}号座位", Clientobject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                Clientobject.EnterOutLogData.EnterOutlog = newEnterOutLog;
                int logid = -1;
                HandleResult result = EnterOutOperate.AddEnterOutLog(newEnterOutLog, ref logid); //添加入座记录
                if (result == HandleResult.Successed)
                {
                    bespeaklog.BsepeakState = BookingStatus.Confinmed;
                    bespeaklog.CancelPerson = Operation.Reader;
                    bespeaklog.CancelTime = nowDate;
                    bespeaklog.Remark = string.Format("在终端{0}刷卡，入座预约的{1} {2}号座位", Clientobject.ClientSetting.ClientNo,
                        bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                    T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);

                    PrintData data = new PrintData();
                    data.CardNo = bespeaklog.CardNo;
                    ;
                    data.EnterTime = nowDate;
                    data.ReaderName = Clientobject.EnterOutLogData.Student.Name;
                    data.ReadingRoomName = Clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    data.SeatNo = SeatComm.SeatNoToShortSeatNo(set.SeatNumAmount, bespeaklog.SeatNo);

                    switch (Clientobject.ClientSetting.DeviceSetting.UsingPrintSlip)
                    {
                        case PrintSlipMode.AutoPrint:
                            printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                            break;
                        case PrintSlipMode.UserChoose:
                            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.PrintConfIrm);
                            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
                            {
                                printer.ThreadPrint(PrintStatus.General, data, Clientobject.ClientSetting.ClientNo);
                            }
                            break;
                        case PrintSlipMode.NotPrint:
                            break;
                    }

                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.BespeatSeatConfirmSuccess);
                }
                else
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                }

            }
            else if (nowDate.CompareTo(dtBegin) < 0)
            {
                //TODO:预约时间过早，请在dtBegin 到dtEnd刷卡确认。
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.BookConfirmWarn);
                if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule != HandleResult.Successed)
                {
                    return;
                }
                bespeaklog.BsepeakState = BookingStatus.Cencaled;
                bespeaklog.CancelPerson = Operation.Reader;
                bespeaklog.CancelTime = ServiceDateTime.Now;
                bespeaklog.Remark = string.Format("在终端{0}刷卡取消{1}，{2}号座位的预约。", Clientobject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                int i = T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.BookCancelSuccess);
            }
        }

        /// <summary>
        /// 等待座位
        /// </summary>
        public void WaitingSeat()
        {
            WaitSeatLogInfo waitLog = Clientobject.EnterOutLogData.Student.WaitSeatLog;
            //ReadingRoomInfo roomInfo = Clientobject.EnterOutLogData.Student.AtReadingRoom;
            //string shortSeatNo = SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, waitLog.SeatNo);
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.WaitSeatCancelWarn);
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule != HandleResult.Successed)
            {
                return;
            }
            //处理等待记录的Id
            Clientobject.EnterOutLogData.WaitSeatLogModel = waitLog;
            Clientobject.EnterOutLogData.WaitSeatLogModel.OperateType = Operation.Reader;
            Clientobject.EnterOutLogData.WaitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
            Clientobject.EnterOutLogData.WaitSeatLogModel.NowState = LogStatus.Valid;
            if (!T_SM_SeatWaiting.UpdateWaitLog(Clientobject.EnterOutLogData.WaitSeatLogModel))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
                return; //恢复读者的在座状态
            }

            Clientobject.EnterOutLogData.EnterOutlog = T_SM_EnterOutLog.GetEnterOutLogInfoById(Clientobject.EnterOutLogData.WaitSeatLogModel.EnterOutLogID);
            TimeSpan shortleavetimelong = ServiceDateTime.Now - Clientobject.EnterOutLogData.EnterOutlog.EnterOutTime;
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ComeBack;
            Clientobject.EnterOutLogData.EnterOutlog.EnterOutType = LogStatus.Valid;
            Clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.OtherReader;
            Clientobject.EnterOutLogData.EnterOutlog.TerminalNum = Clientobject.ClientSetting.ClientNo;
            Clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("读者{0}在{1}终端取消等待{2} {3}号座位，您暂离{4}分钟后恢复为在座状态", Clientobject.EnterOutLogData.WaitSeatLogModel.CardNo, Clientobject.ClientSetting.ClientNo, Clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName, Clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo, shortleavetimelong.Minutes);
            int newId = -1;
            PopupWindowsObject.GetInstance().Window.ShowMessage(EnterOutOperate.AddEnterOutLog(Clientobject.EnterOutLogData.EnterOutlog, ref newId) == HandleResult.Successed ? TipType.WaitSeatCancel : TipType.Exception);
        }

    }
}
