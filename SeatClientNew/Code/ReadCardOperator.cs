using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SeatClientV2.OperateResult;
using SeatManage.EnumType;
using SeatManage.Bll;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;

namespace SeatClientV2.Code
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
        SystemObject clientobject = SystemObject.GetInstance();
        /// <summary>
        /// 选座操作
        /// </summary>
        public void ChooseSeat()
        {
            //进出记录相关属性赋值。
            clientobject.EnterOutLogData.EnterOutlog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
            clientobject.EnterOutLogData.EnterOutlog.EnterOutState = SeatManage.EnumType.EnterOutLogType.SelectSeat;
            clientobject.EnterOutLogData.EnterOutlog.Flag = SeatManage.EnumType.Operation.Reader;
            clientobject.EnterOutLogData.EnterOutlog.EnterOutType = SeatManage.EnumType.LogStatus.Valid;

            ReadingRoomWindow readingRoomWindow = new ReadingRoomWindow();
            readingRoomWindow.ShowDialog();
            if (clientobject.EnterOutLogData.FlowControl == SeatManage.EnumType.ClientOperation.Exit)//阅览室选座操作为退出，结束选座流程
            {
                return;
            }
            switch (readingRoomWindow.viewModel.RoomSelectSeatMethod)
            {
                case SelectSeatMode.AutomaticMode:
                    string tempSeatNo = T_SM_Seat.RandomAllotSeat(clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo);
                    SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(tempSeatNo);
                    if (lockseat == SeatLockState.Locked)//座位锁定失败，则提示
                    {
                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡，自动选择{1} {2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, tempSeatNo.Substring(tempSeatNo.Length - clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount));
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = tempSeatNo;
                        clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                        clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo = SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                    }
                    else
                    {
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
                        PopupWindow popWindow = new PopupWindow(TipType.SeatLocking);
                        popWindow.ShowDialog();
                    }
                    break;
                case SelectSeatMode.ManualMode:
                    //座位选择的操作为退出，结束选座流程；操作为选座，跳出循环；否则操作为back，继续执行循环操作
                    break;
            }
            if (string.IsNullOrEmpty(clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo) || string.IsNullOrEmpty(clientobject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                //如果阅览室编号或者座位号为空，则不执行插入操作。
                return;
            }
            int newLogId = -1;
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                T_SM_Seat.UnLockSeat(clientobject.EnterOutLogData.EnterOutlog.SeatNo);//记录插入成功，解锁座位
                if (clientobject.EnterOutLogData.WaitSeatLogModel != null)
                {
                    //等待记录的实体不为空，说明当前读者操作为等待座位。
                    //TODO:添加等待记录
                    clientobject.EnterOutLogData.WaitSeatLogModel.EnterOutLogID = newLogId;
                    T_SM_SeatWaiting.AddWaitSeatLog(clientobject.EnterOutLogData.WaitSeatLogModel);
                    PrintData data = new PrintData();
                    data.CardNo = clientobject.EnterOutLogData.WaitSeatLogModel.CardNo;
                    data.EnterTime = ServiceDateTime.Now;
                    data.ReaderName = clientobject.EnterOutLogData.Student.Name;
                    data.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    data.SeatNo = SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                    data.SecCardNo = clientobject.EnterOutLogData.EnterOutlog.CardNo;
                    double timelength = NowReadingRoomState.GetSeatHoldTime(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime, data.EnterTime);
                    data.WaitEndDateTime = data.EnterTime.AddMinutes(timelength);
                    printer.ThreadPrint(PrintStatus.Wait, data, clientobject.ClientSetting.ClientNo);

                    PopupWindow popWindow = new PopupWindow(TipType.WaitSeatSuccess);
                    popWindow.ShowDialog();
                }
                else
                {
                    PrintData data = new PrintData();
                    data.CardNo = clientobject.EnterOutLogData.EnterOutlog.CardNo;
                    data.EnterTime = ServiceDateTime.Now;
                    data.ReaderName = clientobject.EnterOutLogData.Student.Name;
                    data.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    data.SeatNo = SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientobject.EnterOutLogData.EnterOutlog.SeatNo);
                    if (clientobject.ClientSetting.DeviceSetting.UsingPrintSlip == PrintSlipMode.AutoPrint)
                    {
                        printer.ThreadPrint(PrintStatus.General, data, clientobject.ClientSetting.ClientNo);
                    }
                    if (clientobject.ClientSetting.DeviceSetting.UsingPrintSlip == PrintSlipMode.UserChoose)
                    {
                        PopupWindow printWindow = new PopupWindow(TipType.PrintConfIrm);
                        printWindow.ShowDialog();
                        if (printWindow.viewModel.OperateResule == HandleResult.Successed)
                        {
                            printer.ThreadPrint(PrintStatus.General, data, clientobject.ClientSetting.ClientNo);
                        }
                    }
                    //提示选座成功
                    PopupWindow popWindow = new PopupWindow(TipType.SelectSeatResult);
                    popWindow.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 离开选择操作
        /// </summary>
        public void LeaveOperate()
        {
            LeaveWindow leaveSelect = new LeaveWindow();
            leaveSelect.ShowDialog();
            //判断读者选择的离开方式
            clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Reader;
            switch (clientobject.EnterOutLogData.FlowControl)
            {
                case ClientOperation.ContinuedTime://读者选择续时操作
                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;//设置读者状态为续时
                    ContinuedTime();
                    break;
                case ClientOperation.Leave://读者选择离开操作
                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;//设置读者状态为离开
                    ReleaseSeat();
                    break;
                case ClientOperation.ReSelectSeat:
                    ChooseSeat();
                    return;//如果是重新选座，则结束方法
                case ClientOperation.ShortLeave://读者选择暂离操作
                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ShortLeave;//设置读者状态为暂离
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
            if (clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime) >= DateTime.Parse(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime))
            {
                PopupWindow popWindow = new PopupWindow(TipType.ContinuedTimeWithout);
                popWindow.ShowDialog();
                return;
            }
            if (clientobject.EnterOutLogData.Student.CanContinuedTime > ServiceDateTime.Now)
            {
                PopupWindow popWindow = new PopupWindow(TipType.ContinuedTimeNotTime);
                popWindow.ShowDialog();
                return;
            }
            if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0 && (clientobject.EnterOutLogData.Student.ContinuedTimeCount >= clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
            {
                PopupWindow popWindow = new PopupWindow(TipType.ContinuedTimeNoCount);
                popWindow.ShowDialog();
                return;
            }
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡延长{1} {2}号座位使用时间", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                PopupWindow popWindow = new PopupWindow(TipType.ContinuedTime);
                popWindow.ShowDialog();
            }
            else
            {
                PopupWindow popWindow = new PopupWindow(TipType.Exception);
                popWindow.ShowDialog();
            }
        }
        /// <summary>
        /// 暂离
        /// </summary>
        public void ShortLeave()
        {
            clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
            int newLogId = -1;
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂时离开，保留{1} {2}号座位{3}分钟",
                clientobject.ClientSetting.ClientNo,
                clientobject.EnterOutLogData.Student.AtReadingRoom.Name,
                clientobject.EnterOutLogData.Student.EnterOutLog.ShortSeatNo,
                NowReadingRoomState.GetSeatHoldTime(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime, ServiceDateTime.Now));
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                PopupWindow popWindow = new PopupWindow(TipType.ShortLeave);
                popWindow.ShowDialog();
            }
            else
            {
                PopupWindow popWindow = new PopupWindow(TipType.Exception);
                popWindow.ShowDialog();
            }
        }
        /// <summary>
        /// 暂离回来操作
        /// </summary>
        public void CometoBack()
        {
            clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ComeBack;//设置读者状态为暂回来
            clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.Reader;
            List<WaitSeatLogInfo> waitSeatLogs = T_SM_SeatWaiting.GetWaitSeatList("", clientobject.EnterOutLogData.EnterOutlog.EnterOutLogID, null, null, null);
            WaitSeatLogInfo waitSeatLog = null;

            int newLogId = -1;
            System.TimeSpan shortleavetimelong = ServiceDateTime.Now - clientobject.EnterOutLogData.EnterOutlog.EnterOutTime;
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离时长{1}分钟，继续使用{2} {3}号座位",
                clientobject.ClientSetting.ClientNo,
                shortleavetimelong.Minutes,
                clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName,
                clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                if (waitSeatLogs.Count > 0)
                {
                    waitSeatLog = waitSeatLogs[0];
                    waitSeatLog.NowState = LogStatus.Fail;
                    waitSeatLog.OperateType = Operation.OtherReader;
                    waitSeatLog.WaitingState = EnterOutLogType.WaitingCancel;
                    T_SM_SeatWaiting.UpdateWaitLog(waitSeatLog);
                }
                PopupWindow popWindow = new PopupWindow(TipType.ComeToBack);
                popWindow.ShowDialog();
                if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used && clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                {
                    List<EnterOutLogType> typeList = new List<EnterOutLogType>();
                    typeList.Add(EnterOutLogType.BookingConfirmation);
                    typeList.Add(EnterOutLogType.SelectSeat);
                    typeList.Add(EnterOutLogType.ReselectSeat);
                    typeList.Add(EnterOutLogType.WaitingSuccess);
                    typeList.Add(EnterOutLogType.ContinuedTime);
                    List<EnterOutLogInfo> seatTimeEnterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogByNo(clientobject.EnterOutLogData.EnterOutlog.EnterOutLogNo, typeList, 1);
                    if (seatTimeEnterOutLog.Count > 0)
                    {

                        if (seatTimeEnterOutLog[0].EnterOutState == EnterOutLogType.ContinuedTime)
                        {
                            if (seatTimeEnterOutLog[0].EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength) < ServiceDateTime.Now)
                            {
                                if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes == 0 || clientobject.EnterOutLogData.Student.ContinuedTimeCount < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes)
                                {
                                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;
                                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，系统自动延长{1} {2}号座位使用时间", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.AutoContinuedTime);
                                        popWindow_Continue.ShowDialog();
                                    }
                                    else
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                                        popWindow_Continue.ShowDialog();
                                    }
                                }
                                else
                                {

                                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{1} {2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.AutoContinuedTimeNoCount);
                                        popWindow_Continue.ShowDialog();
                                    }
                                    else
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                                        popWindow_Continue.ShowDialog();
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (seatTimeEnterOutLog[0].EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength) < ServiceDateTime.Now)
                            {
                                if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
                                {
                                    if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes == 0 || clientobject.EnterOutLogData.Student.ContinuedTimeCount < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes)
                                    {
                                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;
                                        clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，系统自动延长{1} {2}号座位使用时间", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                        result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                        if (result == HandleResult.Successed)
                                        {
                                            PopupWindow popWindow_Continue = new PopupWindow(TipType.AutoContinuedTime);
                                            popWindow_Continue.ShowDialog();
                                        }
                                        else
                                        {
                                            PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                                            popWindow_Continue.ShowDialog();
                                        }
                                    }
                                    else
                                    {
                                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                        clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{1} {2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                        result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                        if (result == HandleResult.Successed)
                                        {
                                            PopupWindow popWindow_Continue = new PopupWindow(TipType.AutoContinuedTimeNoCount);
                                            popWindow_Continue.ShowDialog();
                                        }
                                        else
                                        {
                                            PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                                            popWindow_Continue.ShowDialog();
                                        }
                                    }
                                }
                                else
                                {
                                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡暂离回来，暂离期间在座超时，系统自动释放{1} {2}号座位", clientobject.ClientSetting.ClientNo, clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.AutoContinuedTimeNoCount);
                                        popWindow_Continue.ShowDialog();
                                    }
                                    else
                                    {
                                        PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                                        popWindow_Continue.ShowDialog();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                PopupWindow popWindow_Continue = new PopupWindow(TipType.Exception);
                popWindow_Continue.ShowDialog();
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        public void ReleaseSeat()
        {
            int newLogId = -1;
            clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡释放{1} {2}号座位",
                clientobject.ClientSetting.ClientNo,
                clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName,
                clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                PopupWindow popWindow = new PopupWindow(TipType.Leave);
                popWindow.ShowDialog();
            }
            else
            {
                PopupWindow popWindow = new PopupWindow(TipType.Exception);
                popWindow.ShowDialog();
            }
        }

        /// <summary>
        /// 预约等待
        /// </summary>
        public void BespeakSeatWait()
        {
            DateTime nowDate = ServiceDateTime.Now;

            if (clientobject.EnterOutLogData.Student.BespeakLog.Count > 0)
            {
                BespeakLogInfo bespeaklog = clientobject.EnterOutLogData.Student.BespeakLog[0];
                //if (!SelectSeatProven.CheckReadingRoomInThisClient(bespeaklog.ReadingRoomNo, clientobject.ClientSetting.DeviceSetting))
                //{  //验证房间号是否属于本触摸屏所管理的阅览室
                //    Tip_Framework uc = new Tip_Framework(TipType.BeapeatRoomNotExists, 9);
                //}
                ReadingRoomSetting set = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting;
                DateTime dtBegin = bespeaklog.BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                DateTime dtEnd = bespeaklog.BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                if (DateTimeOperate.DateAccord(dtBegin, dtEnd, nowDate) || (set.SeatBespeak.NowDayBespeak && bespeaklog.SubmitTime == bespeaklog.BsepeakTime))
                {
                    //TODO:预约时间在开始时间和结束时间之间，执行预约确认操作
                    //TODO:预约确认时，判断当前座位上是否有人。
                    EnterOutLogInfo seatUsedInfo = T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(bespeaklog.SeatNo);

                    if (seatUsedInfo != null && seatUsedInfo.EnterOutState != EnterOutLogType.Leave)
                    { //条件满足，说明座位正在使用。
                        seatUsedInfo.EnterOutState = EnterOutLogType.Leave;
                        seatUsedInfo.EnterOutType = LogStatus.Valid;
                        seatUsedInfo.TerminalNum = clientobject.ClientSetting.ClientNo;
                        seatUsedInfo.Remark = string.Format("预约该座位的读者在终端{0}刷卡确认入座，设置在座读者离开", clientobject.ClientSetting.ClientNo);
                        seatUsedInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        if (EnterOutOperate.AddEnterOutLog(seatUsedInfo, ref newId) == HandleResult.Successed)
                        {
                            List<WaitSeatLogInfo> waitInfoList = SeatManage.Bll.T_SM_SeatWaiting.GetWaitSeatList(null, seatUsedInfo.EnterOutLogID, null, null, null);
                            if (waitInfoList.Count > 0)
                            {
                                this.clientobject.EnterOutLogData.WaitSeatLogModel = waitInfoList[0];
                                this.clientobject.EnterOutLogData.WaitSeatLogModel.OperateType = Operation.Reader;
                                this.clientobject.EnterOutLogData.WaitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                                this.clientobject.EnterOutLogData.WaitSeatLogModel.NowState = LogStatus.Valid;
                                if (!T_SM_SeatWaiting.UpdateWaitLog(this.clientobject.EnterOutLogData.WaitSeatLogModel))
                                {
                                    PopupWindow errorWindow = new PopupWindow(TipType.Exception);
                                    errorWindow.ShowDialog();
                                    return;
                                }
                            }
                        }
                        else
                        {
                            PopupWindow errorWindow = new PopupWindow(TipType.Exception);
                            errorWindow.ShowDialog();
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
                    newEnterOutLog.TerminalNum = clientobject.ClientSetting.ClientNo;
                    newEnterOutLog.Remark = string.Format("在终端{0}刷卡，入座预约的{1} {2}号座位", clientobject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                    clientobject.EnterOutLogData.EnterOutlog = newEnterOutLog;
                    int logid = -1;
                    HandleResult result = EnterOutOperate.AddEnterOutLog(newEnterOutLog, ref logid); //添加入座记录
                    if (result == HandleResult.Successed)
                    {
                        bespeaklog.BsepeakState = BookingStatus.Confinmed;
                        bespeaklog.CancelPerson = Operation.Reader;
                        bespeaklog.CancelTime = nowDate;
                        bespeaklog.Remark = string.Format("在终端{0}刷卡，入座预约的{1} {2}号座位", clientobject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                        T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                        PrintData data = new PrintData();
                        data.CardNo = bespeaklog.CardNo; ;
                        data.EnterTime = nowDate;
                        data.ReaderName = clientobject.EnterOutLogData.Student.Name;
                        data.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        data.SeatNo = SeatComm.SeatNoToShortSeatNo(set.SeatNumAmount, bespeaklog.SeatNo);
                        if (clientobject.ClientSetting.DeviceSetting.UsingPrintSlip == PrintSlipMode.AutoPrint)
                        {
                            printer.ThreadPrint(PrintStatus.General, data, clientobject.ClientSetting.ClientNo);
                        }
                        if (clientobject.ClientSetting.DeviceSetting.UsingPrintSlip == PrintSlipMode.UserChoose)
                        {
                            PopupWindow printWindow = new PopupWindow(TipType.PrintConfIrm);
                            printWindow.ShowDialog();
                            if (printWindow.viewModel.OperateResule == HandleResult.Successed)
                            {
                                printer.ThreadPrint(PrintStatus.General, data, clientobject.ClientSetting.ClientNo);
                            }
                        }
                    }
                    PopupWindow popWindow = new PopupWindow(TipType.BespeatSeatConfirmSuccess);
                    popWindow.ShowDialog();
                }
                else if (nowDate.CompareTo(dtBegin) < 0)
                {
                    //TODO:预约时间过早，请在dtBegin 到dtEnd刷卡确认。
                    PopupWindow popWindow = new PopupWindow(TipType.BookConfirmWarn);
                    popWindow.ShowDialog();
                    if (popWindow.viewModel.OperateResule == HandleResult.Successed)
                    {
                        bespeaklog.BsepeakState = BookingStatus.Cencaled;
                        bespeaklog.CancelPerson = Operation.Reader;
                        bespeaklog.CancelTime = ServiceDateTime.Now;
                        bespeaklog.Remark = string.Format("在终端{0}刷卡取消{1}，{2}号座位的预约。", clientobject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                        int i = T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                        PopupWindow popWindowCancel = new PopupWindow(TipType.BookCancelSuccess);
                        popWindowCancel.ShowDialog();
                    }
                }
                else if (nowDate.CompareTo(dtEnd) < 0)
                {
                    //TODO:最迟确认时间为：dtEnd。
                }
                else
                {
                    //TODO:未知原因，预约确认失败。
                }
            }
        }

        /// <summary>
        /// 等待座位
        /// </summary>
        public void WaitingSeat()
        {
            WaitSeatLogInfo waitLog = clientobject.EnterOutLogData.Student.WaitSeatLog;
            ReadingRoomInfo roomInfo = clientobject.EnterOutLogData.Student.AtReadingRoom;
            string shortSeatNo = SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, waitLog.SeatNo);
            PopupWindow popWindowCancel = new PopupWindow(TipType.WaitSeatCancelWarn);
            popWindowCancel.ShowDialog();
            if (popWindowCancel.viewModel.OperateResule == HandleResult.Successed)
            {
                //处理等待记录的Id
                this.clientobject.EnterOutLogData.WaitSeatLogModel = waitLog;
                this.clientobject.EnterOutLogData.WaitSeatLogModel.OperateType = Operation.Reader;
                this.clientobject.EnterOutLogData.WaitSeatLogModel.WaitingState = EnterOutLogType.WaitingCancel;
                this.clientobject.EnterOutLogData.WaitSeatLogModel.NowState = LogStatus.Valid;
                if (T_SM_SeatWaiting.UpdateWaitLog(this.clientobject.EnterOutLogData.WaitSeatLogModel))
                {  //恢复读者的在座状态

                    this.clientobject.EnterOutLogData.EnterOutlog = T_SM_EnterOutLog.GetEnterOutLogInfoById(this.clientobject.EnterOutLogData.WaitSeatLogModel.EnterOutLogID);
                    System.TimeSpan shortleavetimelong = ServiceDateTime.Now - clientobject.EnterOutLogData.EnterOutlog.EnterOutTime;
                    this.clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ComeBack;
                    this.clientobject.EnterOutLogData.EnterOutlog.EnterOutType = LogStatus.Valid;
                    this.clientobject.EnterOutLogData.EnterOutlog.Flag = Operation.OtherReader;
                    clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                    this.clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("读者{0}在{1}终端取消等待{2} {3}号座位，您暂离{4}分钟后恢复为在座状态",
                        this.clientobject.EnterOutLogData.WaitSeatLogModel.CardNo,
                        this.clientobject.ClientSetting.ClientNo,
                        this.clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName,
                        this.clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo,
                        shortleavetimelong.Minutes);
                    int newId = -1;
                    if (EnterOutOperate.AddEnterOutLog(this.clientobject.EnterOutLogData.EnterOutlog, ref newId) == HandleResult.Successed)
                    {
                        PopupWindow popWindow = new PopupWindow(TipType.WaitSeatCancel);
                        popWindow.ShowDialog();
                    }
                    else
                    {
                        PopupWindow popWindow = new PopupWindow(TipType.Exception);
                        popWindow.ShowDialog();
                    }
                }

            }
        }

    }
}
