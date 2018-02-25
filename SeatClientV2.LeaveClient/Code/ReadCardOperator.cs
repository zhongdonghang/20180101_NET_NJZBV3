using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClientLeaveV2.OperateResult;
using SeatManage.EnumType;
using SeatManage.Bll;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;
using System.Configuration;

namespace ClientLeaveV2.Code
{
    public class PopMessage
    {
        public PopMessage(SeatManage.EnumType.TipType type, string message)
        {
            _PopType = type;
            _Message = message;
        }
        private string _Message = "";
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
        private SeatManage.EnumType.TipType _PopType = TipType.None;
        /// <summary>
        /// 弹窗类型
        /// </summary>
        public SeatManage.EnumType.TipType PopType
        {
            get { return _PopType; }
            set { _PopType = value; }
        }
    }
    internal class ReadCardOperator
    {
        private ReadCardOperator()
        {

        }
        /// <summary>
        /// 打印
        /// </summary>
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
        public delegate void PopMessageEventHandler(Object sender, PopMessage e);
        public event PopMessageEventHandler popMessage;
        SystemObject clientobject = SystemObject.GetInstance();
        /// <summary>
        /// 无座操作
        /// </summary>
        public void NoSeat()
        {
            //提示选座成功
            if (popMessage != null)
            {
                popMessage(this, new PopMessage(TipType.SelectSeatResult, "暂无座位"));
            }
        }


        /// <summary>
        /// 离开选择操作
        /// </summary>
        public void LeaveOperate()
        {
            string leaveModel = ConfigurationManager.AppSettings["LeaveState"];
            if (string.IsNullOrEmpty(leaveModel) || leaveModel == "0")
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
                    case ClientOperation.ShortLeave://读者选择暂离操作
                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ShortLeave;//设置读者状态为暂离
                        ShortLeave();
                        break;
                }
            }
            else
            {
                switch (leaveModel)
                {
                    case "1"://读者选暂离时操作
                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ShortLeave;//设置读者状态为暂离
                        ShortLeave();
                        break;
                    case "2"://读者选择离开操作
                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;//设置读者状态为离开
                        ReleaseSeat();
                        break;
                    case "3"://读者选择续时操作
                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.ContinuedTime;//设置读者状态为续时
                        ContinuedTime();
                        break;
                }
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
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.ContinuedTimeWithout, "无需续时"));
                }
                return;
            }
            if (clientobject.EnterOutLogData.Student.CanContinuedTime > ServiceDateTime.Now)
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.ContinuedTimeNotTime, "还没到达可续时时间"));
                }
                return;
            }
            if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0 && (clientobject.EnterOutLogData.Student.ContinuedTimeCount >= clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes))
            {
                PopupWindow popWindow = new PopupWindow(TipType.ContinuedTimeNoCount);
                popWindow.Show();
                return;
            }
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡延长{0} {1}号座位使用时间", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.ContinuedTime, "续时成功"));
                }
            }
            else
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                }
            }
        }
        /// <summary>
        /// 暂离
        /// </summary>
        public void ShortLeave()
        {
            int newLogId = -1;
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂时离开，保留{0} {1}号座位{2}分钟",
                clientobject.EnterOutLogData.Student.AtReadingRoom.Name,
                clientobject.EnterOutLogData.Student.EnterOutLog.ShortSeatNo,
                NowReadingRoomState.GetSeatHoldTime(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime, ServiceDateTime.Now));
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.ShortLeave, "暂离成功"));
                }
            }
            else
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                }
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
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离时长{0}分钟，继续使用{1} {2}号座位",
                shortleavetimelong.Minutes,
                clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName,
                clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
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
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.ComeToBack, "欢迎回来"));
                }
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
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离期间在座超时，系统自动延长{0} {1}号座位使用时间", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.AutoContinuedTime, "自动续时成功"));
                                        }
                                    }
                                    else
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                                        }
                                    }
                                }
                                else
                                {

                                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{0} {1}号座位", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.AutoContinuedTimeNoCount, "续时次数不足"));
                                        }
                                    }
                                    else
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                                        }
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
                                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离期间在座超时，系统自动延长{0} {1}号座位使用时间", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                        result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                        if (result == HandleResult.Successed)
                                        {
                                            if (popMessage != null)
                                            {
                                                popMessage(this, new PopMessage(TipType.AutoContinuedTime, "续时成功"));
                                            }
                                        }
                                        else
                                        {
                                            if (popMessage != null)
                                            {
                                                popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                                            }
                                        }
                                    }
                                    else
                                    {
                                        clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离期间在座超时，续时次数已用完，系统自动释放{0} {1}号座位", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                        result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                        if (result == HandleResult.Successed)
                                        {
                                            if (popMessage != null)
                                            {
                                                popMessage(this, new PopMessage(TipType.AutoContinuedTimeNoCount, "续时次数不足"));
                                            }
                                        }
                                        else
                                        {
                                            if (popMessage != null)
                                            {
                                                popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    clientobject.EnterOutLogData.EnterOutlog.EnterOutState = EnterOutLogType.Leave;
                                    clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在离开终端刷卡暂离回来，暂离期间在座超时，系统自动释放{0} {0}号座位", clientobject.EnterOutLogData.Student.AtReadingRoom.Name, clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
                                    result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
                                    if (result == HandleResult.Successed)
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.AutoContinuedTimeNoCount, "续时次数不足"));
                                        }
                                    }
                                    else
                                    {
                                        if (popMessage != null)
                                        {
                                            popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
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
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                }
            }
        }
        /// <summary>
        /// 释放座位
        /// </summary>
        public void ReleaseSeat()
        {
            int newLogId = -1;
            clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}刷卡释放{0} {1}号座位",
                clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName,
                clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo);
            HandleResult result = EnterOutOperate.AddEnterOutLog(clientobject.EnterOutLogData.EnterOutlog, ref newLogId);//插入进出记录
            if (result == HandleResult.Successed)
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.Leave, "座位释放"));
                }
            }
            else
            {
                if (popMessage != null)
                {
                    popMessage(this, new PopMessage(TipType.Exception, "操作失败"));
                }
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
                        seatUsedInfo.Remark = string.Format("预约该座位的读者在离开终端刷卡确认入座，设置在座读者离开");
                        seatUsedInfo.Flag = Operation.OtherReader;
                        int newId = -1;
                        EnterOutOperate.AddEnterOutLog(seatUsedInfo, ref newId);
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
                    newEnterOutLog.Remark = string.Format("在离开终端刷卡，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                    clientobject.EnterOutLogData.EnterOutlog = newEnterOutLog;
                    int logid = -1;
                    HandleResult result = EnterOutOperate.AddEnterOutLog(newEnterOutLog, ref logid); //添加入座记录
                    if (result == HandleResult.Successed)
                    {
                        bespeaklog.BsepeakState = BookingStatus.Confinmed;
                        bespeaklog.CancelPerson = Operation.Reader;
                        bespeaklog.CancelTime = nowDate;
                        bespeaklog.Remark = string.Format("在离开终端刷卡，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                        T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                    }
                    if (popMessage != null)
                    {
                        popMessage(this, new PopMessage(TipType.BespeatSeatConfirmSuccess, "预约签到成功"));
                    }
                }
                else if (nowDate.CompareTo(dtBegin) < 0)
                {
                    if (popMessage != null)
                    {
                        popMessage(this, new PopMessage(TipType.BookConfirmWarn, "没到签到时间"));
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
            if (popMessage != null)
            {
                popMessage(this, new PopMessage(TipType.WaitSeatCancelWarn, "已有等待座位"));
            }

        }

    }
}
