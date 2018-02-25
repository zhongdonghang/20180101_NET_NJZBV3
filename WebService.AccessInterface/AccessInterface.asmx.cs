using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Text;
using System.Configuration;

namespace WebService.AccessInterface
{
    /// <summary>
    /// SM_AccessInterface 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class SM_AccessInterface : System.Web.Services.WebService
    {

        [WebMethod]
        public string EnterLib(string CardNo)
        {
            string[] strs = CardNo.Split(',');
            CardNo = strs[0];
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            //SeatManage.SeatManageComm.WriteLog.Write("传入卡号：" + strs[0]);
            //SeatManage.SeatManageComm.WriteLog.Write("传入图书馆号：" + strs[1]);
            StringBuilder message = new StringBuilder();
            string ReaderNo = CardNo;
            string ReaderName = "";
            string NowStatus = "";
            string BeforeStatus = "";
            string Error = "";
            message.Append("<ReaderInfo>");
            message.Append("<ReaderNo>{0}</ReaderNo>");
            message.Append("<ReaderName>{1}</ReaderName>");
            message.Append("<NowStatus>{2}</NowStatus>");
            message.Append("<BeforeStatus>{3}</BeforeStatus>");
            message.Append("<Error>{4}</Error>");
            message.Append("</ReaderInfo>");
            try
            {
                if (string.IsNullOrEmpty(CardNo))
                {
                    throw new Exception("输入的学号为空！");
                }
                SeatManage.ClassModel.AccessSetting accset = SeatManage.Bll.T_SM_SystemSet.GetAccessSetting();
                if (accset == null)
                {
                    throw new Exception("获取通道机设置失败！");
                }
                SeatManage.ClassModel.ReaderInfo reader = SeatManage.Bll.EnterOutOperate.GetReaderInfo(CardNo);
                if (reader == null)
                {
                    throw new Exception("获取不到此学生的信息！");
                }
                if (strs.Length > 1 && reader.AtReadingRoom != null && strs[1] != "00" && reader.AtReadingRoom.Libaray.No != strs[1])
                {
                    throw new Exception("学生在此图书馆未选座位！");
                }
                ReaderNo = reader.CardNo;
                ReaderName = reader.Name;
                if (reader.EnterOutLog == null)
                {
                    NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                    BeforeStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                }
                else
                {
                    NowStatus = ((int)reader.EnterOutLog.EnterOutState).ToString();
                    BeforeStatus = ((int)reader.EnterOutLog.EnterOutState).ToString();
                }
                if (accset.IsUsed)
                {
                    if (accset.EnterLib && reader.EnterOutLog != null)
                    {
                        switch (reader.EnterOutLog.EnterOutState)
                        {
                            case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                            case SeatManage.EnumType.EnterOutLogType.ComeBack:
                            case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                            case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                            case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                            case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                                if (accset.IsReleaseOnSeat && reader.EnterOutLog.EnterOutTime.AddMinutes(accset.LeaveTimeSpan) < SeatManage.Bll.ServiceDateTime.Now)
                                {
                                    SeatManage.ClassModel.EnterOutLogInfo enterOutLog = reader.EnterOutLog;
                                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                    enterOutLog.TerminalNum = "";
                                    enterOutLog.Remark = string.Format("读者离开图书馆未刷卡，再次通过通道机进入，系统自动释放{0} {1}号座位", reader.AtReadingRoom.Name, enterOutLog.ShortSeatNo);
                                    enterOutLog.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now;
                                    enterOutLog.Flag = SeatManage.EnumType.Operation.Service;
                                    int newId = 0;
                                    if (SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId) == SeatManage.EnumType.HandleResult.Failed)
                                    {
                                        throw new Exception("更新进出记录失败！");
                                    }
                                    NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                                    if (accset.AddViolationRecords)
                                    {
                                        SeatManage.ClassModel.ViolationRecordsLogInfo vrInfo = new SeatManage.ClassModel.ViolationRecordsLogInfo();
                                        vrInfo.CardNo = enterOutLog.CardNo;
                                        vrInfo.EnterFlag = SeatManage.EnumType.ViolationRecordsType.LeaveNotReadCard;
                                        vrInfo.EnterOutTime = enterOutLog.EnterOutTime.ToString();
                                        vrInfo.Flag = SeatManage.EnumType.LogStatus.Valid;
                                        vrInfo.ReadingRoomID = enterOutLog.ReadingRoomNo;
                                        vrInfo.SeatID = enterOutLog.SeatNo;
                                        vrInfo.Remark = string.Format("读者{0}离开图书馆未刷卡，再次通过通道机，记录违规", ReaderNo);
                                        if (!SeatManage.Bll.T_SM_ViolateDiscipline.AddViolationRecords(vrInfo))
                                        {
                                            throw new Exception("添加违规记录失败！");
                                        }
                                    }
                                }
                                break;
                            case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                                if (accset.IsComeBack)
                                {
                                    DateTime NowTime = SeatManage.Bll.ServiceDateTime.Now;
                                    SeatManage.ClassModel.EnterOutLogInfo enterOutLog = reader.EnterOutLog;
                                    System.TimeSpan shortleavetimelong = NowTime - enterOutLog.EnterOutTime;
                                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ComeBack;
                                    enterOutLog.TerminalNum = "";
                                    enterOutLog.Remark = string.Format("在通道机刷卡暂离回来，暂离时长{0}分钟，继续使用{1} {2}号座位", shortleavetimelong.TotalMinutes.ToString().Split('.')[0], enterOutLog.ReadingRoomName, enterOutLog.ShortSeatNo);
                                    enterOutLog.EnterOutTime = NowTime;
                                    enterOutLog.Flag = SeatManage.EnumType.Operation.Service;
                                    int newId = 0;
                                    if (SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId) == SeatManage.EnumType.HandleResult.Failed)
                                    {
                                        throw new Exception("更新进出记录失败！");
                                    }
                                    List<SeatManage.ClassModel.WaitSeatLogInfo> waitSeatLogs = SeatManage.Bll.T_SM_SeatWaiting.GetWaitSeatList("", enterOutLog.EnterOutLogID, null, null, null);
                                    if (waitSeatLogs.Count > 0)
                                    {
                                        SeatManage.ClassModel.WaitSeatLogInfo waitSeatLog = waitSeatLogs[0];
                                        waitSeatLog.NowState = SeatManage.EnumType.LogStatus.Fail;
                                        waitSeatLog.OperateType = SeatManage.EnumType.Operation.OtherReader;
                                        waitSeatLog.WaitingState = SeatManage.EnumType.EnterOutLogType.WaitingCancel;
                                        if (!SeatManage.Bll.T_SM_SeatWaiting.UpdateWaitLog(waitSeatLog))
                                        {
                                            throw new Exception("修改等待记录失败！");
                                        }
                                    }
                                    NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.ComeBack).ToString();
                                }
                                break;
                        }
                    }
                    else
                    {
                        DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
                        if (accset.IsBookingConfinmed && reader.BespeakLog.Count > 0)
                        {
                            SeatManage.ClassModel.BespeakLogInfo bespeaklog = reader.BespeakLog[0];
                            SeatManage.ClassModel.ReadingRoomSetting set = reader.AtReadingRoom.Setting;
                            DateTime dtBegin = bespeaklog.BsepeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                            DateTime dtEnd = bespeaklog.BsepeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                            if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(dtBegin, dtEnd, nowDate) || (set.SeatBespeak.NowDayBespeak && bespeaklog.SubmitTime == bespeaklog.BsepeakTime))
                            {
                                SeatManage.ClassModel.EnterOutLogInfo seatUsedInfo = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(bespeaklog.SeatNo);
                                if (seatUsedInfo != null && seatUsedInfo.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                                { //条件满足，说明座位正在使用。
                                    seatUsedInfo.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                    seatUsedInfo.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
                                    seatUsedInfo.Remark = string.Format("预约该座位的读者在通道机刷卡确认入座，设置在座读者离开");
                                    seatUsedInfo.Flag = SeatManage.EnumType.Operation.OtherReader;
                                    int newId = -1;
                                    SeatManage.Bll.EnterOutOperate.AddEnterOutLog(seatUsedInfo, ref newId);
                                }
                                SeatManage.ClassModel.EnterOutLogInfo newEnterOutLog = new SeatManage.ClassModel.EnterOutLogInfo();//构造 
                                newEnterOutLog.CardNo = bespeaklog.CardNo;
                                newEnterOutLog.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
                                newEnterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.BookingConfirmation;
                                newEnterOutLog.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
                                newEnterOutLog.Flag = SeatManage.EnumType.Operation.Reader;
                                newEnterOutLog.ReadingRoomNo = bespeaklog.ReadingRoomNo;
                                newEnterOutLog.SeatNo = bespeaklog.SeatNo;
                                newEnterOutLog.Remark = string.Format("在通道机刷卡，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                                NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.BookingConfirmation).ToString();
                                int logid = -1;
                                SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(newEnterOutLog, ref logid); //添加入座记录
                                if (result == SeatManage.EnumType.HandleResult.Successed)
                                {
                                    bespeaklog.BsepeakState = SeatManage.EnumType.BookingStatus.Confinmed;
                                    bespeaklog.CancelPerson = SeatManage.EnumType.Operation.Reader;
                                    bespeaklog.CancelTime = nowDate;
                                    bespeaklog.Remark = string.Format("在通道机刷卡，入座预约的{0} {1}号座位", bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                                    SeatManage.Bll.T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                                }
                            }
                        }
                    }
                }


            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return string.Format(message.ToString(), ReaderNo, ReaderName, NowStatus, BeforeStatus, Error);
        }
        [WebMethod]
        public string OutLib(string CardNo)
        {
            string[] strs = CardNo.Split(',');
            CardNo = strs[0];
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            //SeatManage.SeatManageComm.WriteLog.Write("传入卡号：" + CardNo);
            StringBuilder message = new StringBuilder();
            string ReaderNo = CardNo;
            string ReaderName = "";
            string NowStatus = "";
            string BeforeStatus = "";
            string Error = "";
            message.Append("<ReaderInfo>");
            message.Append("<ReaderNo>{0}</ReaderNo>");
            message.Append("<ReaderName>{1}</ReaderName>");
            message.Append("<NowStatus>{2}</NowStatus>");
            message.Append("<BeforeStatus>{3}</BeforeStatus>");
            message.Append("<Error>{4}</Error>");
            message.Append("</ReaderInfo>");
            try
            {
                if (string.IsNullOrEmpty(CardNo))
                {
                    throw new Exception("输入的学号为空！");
                }
                SeatManage.ClassModel.AccessSetting accset = SeatManage.Bll.T_SM_SystemSet.GetAccessSetting();
                if (accset == null)
                {
                    throw new Exception("获取通道机设置失败！");
                }
                SeatManage.ClassModel.ReaderInfo reader = SeatManage.Bll.EnterOutOperate.GetReaderInfo(CardNo);
                if (reader == null)
                {
                    throw new Exception("获取不到此学生的信息！");
                }
                if (strs.Length > 1 && reader.AtReadingRoom != null && strs[1] != "00" && reader.AtReadingRoom.Libaray.No != strs[1])
                {
                    throw new Exception("学生在此图书馆未选座位！");
                }
                ReaderNo = reader.CardNo;
                ReaderName = reader.Name;
                if (reader.EnterOutLog == null)
                {
                    NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                    BeforeStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                }
                else
                {
                    NowStatus = ((int)reader.EnterOutLog.EnterOutState).ToString();
                    BeforeStatus = ((int)reader.EnterOutLog.EnterOutState).ToString();
                }
                if (accset.IsUsed && accset.OutLib && reader.EnterOutLog != null)
                {
                    switch (reader.EnterOutLog.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                            if (accset.LeaveMode == SeatManage.EnumType.EnterOutLogType.Leave)
                            {
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = reader.EnterOutLog;
                                enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                enterOutLog.TerminalNum = "";
                                enterOutLog.Remark = string.Format("读者离开图书馆，通过通道机，系统自动释放{0} {1}号座位", reader.AtReadingRoom.Name, enterOutLog.ShortSeatNo);
                                enterOutLog.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now;
                                enterOutLog.Flag = SeatManage.EnumType.Operation.Service;
                                int newId = 0;
                                if (SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId) == SeatManage.EnumType.HandleResult.Failed)
                                {
                                    throw new Exception("更新进出记录失败！");
                                }
                                NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.Leave).ToString();
                            }
                            else if (accset.LeaveMode == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                            {
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = reader.EnterOutLog;
                                enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                                enterOutLog.TerminalNum = "";
                                enterOutLog.Remark = string.Format("读者离开图书馆，通过通道机，设置读者为暂离，保留{0} {1}号座位{2}分钟", reader.AtReadingRoom.Name, enterOutLog.ShortSeatNo, SeatManage.Bll.NowReadingRoomState.GetSeatHoldTime(reader.AtReadingRoom.Setting.SeatHoldTime, SeatManage.Bll.ServiceDateTime.Now));
                                enterOutLog.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now;
                                enterOutLog.Flag = SeatManage.EnumType.Operation.Service;
                                int newId = 0;
                                if (SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId) == SeatManage.EnumType.HandleResult.Failed)
                                {
                                    throw new Exception("更新进出记录失败！");
                                }
                                NowStatus = ((int)SeatManage.EnumType.EnterOutLogType.ShortLeave).ToString();
                            }
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return string.Format(message.ToString(), ReaderNo, ReaderName, NowStatus, BeforeStatus, Error);
        }
        [WebMethod]
        public string GetBlacklist(string CardNo)
        {
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            //SeatManage.SeatManageComm.WriteLog.Write("传入卡号：" + CardNo);
            StringBuilder message = new StringBuilder();
            string ReaderNo = CardNo;
            string ReaderName = "";
            string Error = "";
            message.Append("<ReaderInfo>");
            message.Append("<ReaderNo>{0}</ReaderNo>");
            message.Append("<ReaderName>{1}</ReaderName>");
            message.Append("<Error>{2}</Error>");
            message.Append("</ReaderInfo>");
            try
            {
                if (string.IsNullOrEmpty(CardNo))
                {
                    throw new Exception("输入的学号为空！");
                }
                SeatManage.ClassModel.AccessSetting accset = SeatManage.Bll.T_SM_SystemSet.GetAccessSetting();
                if (accset == null)
                {
                    throw new Exception("获取通道机设置失败！");
                }
                SeatManage.ClassModel.ReaderInfo reader = SeatManage.Bll.EnterOutOperate.GetReaderInfo(CardNo);
                if (reader == null)
                {
                    throw new Exception("获取不到此学生的信息！");
                }
                ReaderNo = reader.CardNo;
                ReaderName = reader.Name;
                if (reader.BlacklistLog.Count > 0 && accset.IsLimitBlackList)
                {
                    foreach (SeatManage.ClassModel.BlackListInfo blinfo in reader.BlacklistLog)
                    {
                        message.Append("<BlistlistInfo>");
                        message.AppendFormat("<ReadingRoomNo>{0}</ReadingRoomNo>", blinfo.ReadingRoomID);
                        message.AppendFormat("<ReadingRoomName>{0}</ReadingRoomName>", blinfo.ReadingRoomName);
                        message.AppendFormat("<AddTime>{0}</AddTime>", blinfo.AddTime.ToString());
                        message.AppendFormat("<OutTime>{0}</OutTime>", blinfo.OutTime.ToString());
                        message.AppendFormat("<Remark>{0}</Remark>", blinfo.ReMark);
                        message.Append("</BlistlistInfo>");
                    }
                }
            }
            catch (Exception e)
            {
                Error = e.Message;
            }
            return string.Format(message.ToString(), ReaderNo, ReaderName, Error);
        }
        /// <summary>
        /// 读者当前状态
        /// </summary>
        /// <param name="StuNo"></param>
        /// <returns></returns>
        [WebMethod]
        public string StuState(string StuNo)
        {
            if (!Verifylicensing())
            {
                return "非法操作，此接口未进行授权！";
            }
            string result = "";
            SeatManage.Bll.T_SM_Reader reader = new SeatManage.Bll.T_SM_Reader();
            SeatManage.ClassModel.ReaderInfo readerModel = new SeatManage.ClassModel.ReaderInfo();
            readerModel = reader.GetReader(StuNo);
            string state = "";
            string seatNo = "";
            string readingRoomName = "";
            if (!string.IsNullOrEmpty(readerModel.CardNo))
            {
                if (readerModel.EnterOutLog != null)
                {
                    switch (readerModel.EnterOutLog.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingCancel:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                            state = "在座";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            string rrId = readerModel.EnterOutLog.ReadingRoomNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.Leave:
                        case SeatManage.EnumType.EnterOutLogType.None:
                        case SeatManage.EnumType.EnterOutLogType.BookingCancel:
                            state = "无座";
                            break;
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            state = "暂离";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.Waiting:
                            state = "等待座位";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                        case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                            state = "存在未确认预约座位";
                            seatNo = readerModel.EnterOutLog.SeatNo;
                            readingRoomName = readerModel.EnterOutLog.ReadingRoomName;
                            break;
                    }
                }
                result = string.Format("<ReaderInfo><Reader Name='{0}' CardNo='{1}' RoomName='{2}'  SeatNo='{3}' Status='{4}'></Reader></ReaderInfo>", readerModel.Name, readerModel.CardNo, readingRoomName, seatNo, state);

            }
            else
            {
                result = "<ReaderInfo><Reader Name='' CardNo='' RoomName=''  SeatNo='' Status=''></Reader></ReaderInfo>";
            }
            return result;
        }
        /// <summary>
        /// 认证许可
        /// </summary>
        /// <returns></returns>
        private bool Verifylicensing()
        {
            return true;
            //try
            //{
            //    if (SeatManage.Bll.Registry.AccessInterfaceIsAuthorize())
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch
            //{
            //    return false;
            //}
            //string interfacekey = ConfigurationManager.AppSettings["AccessInterfaceKey"];
            //interfacekey = interfacekey.Replace("-", "");
            //List<SeatManage.ClassModel.TerminalInfoV2> treList = SeatManage.Bll.TerminalOperatorService.GetAllTeminalInfo();
            //if (treList.Count < 1)
            //{
            //    return false;
            //}
            //string ClientNo = treList[0].ClientNo;
            //if (SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32WithListKey(new List<string>() { ClientNo.Substring(0, ClientNo.Length - 2), "JuneberryReadingRoomInterfaceKey" }) == interfacekey)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
