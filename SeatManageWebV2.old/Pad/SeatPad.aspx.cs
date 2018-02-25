using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using System.Text;
using SeatManage.SeatManageComm;

namespace SeatManageWebV2.Pad
{
    public partial class SeatPad : System.Web.UI.Page
    {
        public string cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LoginId"] == null)
            {
                Response.Redirect("../Login.aspx");
            }
            if (!IsPostBack)
            {
                BindReadingRoomList(Session[CookiesManager.LoginID].ToString());
                GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
            }

            cmd = Request.Form["subCmd"];
            if (cmd != null)
            {
                #region 座位相关操作
                switch (cmd)
                {
                    case "search"://查询座位
                        try
                        {
                            GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                        }
                        catch
                        {

                        }
                        break;
                    case "ShortLeave"://设置暂离
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && enterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.ShortLeave)
                                {
                                    SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);
                                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                                    enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                                    enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置为暂离", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());
                                    int newId = -1;
                                    SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId);
                                    if (result == SeatManage.EnumType.HandleResult.Successed)
                                    {
                                        Page.RegisterStartupScript("", "<script>alert('设置读者暂离成功');</script>");
                                        GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("", "<script>alert('设置读者暂离失败');</script>");
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case "ReleaseShortLeave"://取消暂离
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && enterOutLog.EnterOutState == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                                {
                                    SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);
                                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ComeBack;
                                    enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                                    enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备取消暂离，恢复为在座", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());
                                    int newId = -1;
                                    SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId);
                                    if (result == SeatManage.EnumType.HandleResult.Successed)
                                    {
                                        List<SeatManage.ClassModel.WaitSeatLogInfo> waitSeatLogs = SeatManage.Bll.T_SM_SeatWaiting.GetWaitSeatList("", enterOutLog.EnterOutLogID, null, null, null);
                                        SeatManage.ClassModel.WaitSeatLogInfo waitSeatLog = null;
                                        if (waitSeatLogs.Count > 0)
                                        {
                                            waitSeatLog = waitSeatLogs[0];
                                            waitSeatLog.NowState = SeatManage.EnumType.LogStatus.Fail;
                                            waitSeatLog.OperateType = SeatManage.EnumType.Operation.OtherReader;
                                            waitSeatLog.WaitingState = SeatManage.EnumType.EnterOutLogType.WaitingCancel;
                                            if (SeatManage.Bll.T_SM_SeatWaiting.UpdateWaitLog(waitSeatLog))
                                            {
                                                Page.RegisterStartupScript("", "<script>alert('取消读者暂离成功');</script>");
                                                GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                            }
                                            else
                                            {
                                                Page.RegisterStartupScript("", "<script>alert('取消读者暂离成功，取消读者等待失败');</script>");
                                                GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                            }
                                        }
                                        else
                                        {
                                            Page.RegisterStartupScript("", "<script>alert('取消读者暂离成功');</script>");
                                            GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                        }
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("", "<script>alert('取消读者暂离失败');</script>");
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                        break;
                    case "Release"://释放座位
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && enterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                                {
                                    SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);

                                    enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                    enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                                    enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());
                                    int newId = -1;
                                    SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId);
                                    if (result == SeatManage.EnumType.HandleResult.Successed)
                                    {
                                        SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
                                        if (roomInfo.Setting.IsRecordViolate)
                                        {
                                            if (roomInfo.Setting.BlackListSetting.Used)
                                            {
                                                if (roomInfo.Setting.BlackListSetting.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin])
                                                {
                                                    SeatManage.ClassModel.ViolationRecordsLogInfo violationRecords = new SeatManage.ClassModel.ViolationRecordsLogInfo();
                                                    violationRecords.CardNo = enterOutLog.CardNo;
                                                    violationRecords.SeatID = enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - roomInfo.Setting.SeatNumAmount, roomInfo.Setting.SeatNumAmount);
                                                    violationRecords.ReadingRoomID = enterOutLog.ReadingRoomNo;
                                                    violationRecords.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now.ToString();
                                                    violationRecords.EnterFlag = SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin;
                                                    violationRecords.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());
                                                    violationRecords.BlacklistID = "-1";
                                                    SeatManage.Bll.T_SM_ViolateDiscipline.AddViolationRecords(violationRecords);
                                                }
                                            }
                                            else if (rulesSet.BlacklistSet.Used && rulesSet.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin])
                                            {
                                                SeatManage.ClassModel.ViolationRecordsLogInfo violationRecords = new SeatManage.ClassModel.ViolationRecordsLogInfo();
                                                violationRecords.CardNo = enterOutLog.CardNo;
                                                violationRecords.SeatID = enterOutLog.SeatNo.Substring(enterOutLog.SeatNo.Length - roomInfo.Setting.SeatNumAmount, roomInfo.Setting.SeatNumAmount);
                                                violationRecords.ReadingRoomID = enterOutLog.ReadingRoomNo;
                                                violationRecords.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now.ToString();
                                                violationRecords.EnterFlag = SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin;
                                                violationRecords.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());
                                                violationRecords.BlacklistID = "-1";
                                                SeatManage.Bll.T_SM_ViolateDiscipline.AddViolationRecords(violationRecords);
                                            }
                                        }
                                        Page.RegisterStartupScript("", "<script>alert('设置读者离开成功');</script>");
                                        GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);

                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("", "<script>alert('设置读者离开失败');</script>");
                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case "onTime"://计时
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && enterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.ShortLeave)
                                {
                                    DateTime markTime = SeatManage.Bll.ServiceDateTime.Now;
                                    SeatManage.Bll.EnterOutOperate.UpdateMarkTime(enterOutLog.EnterOutLogID, markTime);
                                    GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                }
                            }
                        }
                        catch
                        {

                        }

                        break;
                    case "offTime"://取消计时
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && !string.IsNullOrEmpty(enterOutLog.MarkTime.ToString()) && enterOutLog.MarkTime.CompareTo(DateTime.Parse("1900/1/1")) != 0)
                                {
                                    DateTime markTime = DateTime.Parse("1900-1-1");
                                    SeatManage.Bll.EnterOutOperate.UpdateMarkTime(enterOutLog.EnterOutLogID, markTime);
                                    GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                }
                            }
                        }
                        catch
                        {

                        }

                        break;
                    case "AddBlacklist":
                        try
                        {
                            string seatStr = hidSeatNo.Value;
                            string[] noArr = seatStr.Split(',');
                            string seatNo = "";
                            int newId = -1;
                            SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
                            for (int i = 0; i < noArr.Length; i++)
                            {
                                newId = -1;
                                seatNo = noArr[i].Trim();
                                SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
                                if (enterOutLog != null && enterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                                {
                                    SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);
                                    if (roomInfo != null && roomInfo.Setting.BlackListSetting.Used)
                                    {
                                        SeatManage.ClassModel.BlackListInfo blacklistModel = new SeatManage.ClassModel.BlackListInfo();
                                        blacklistModel.AddTime = SeatManage.Bll.ServiceDateTime.Now;
                                        blacklistModel.ReadingRoomID = roomInfo.No;
                                        blacklistModel.BlacklistState = SeatManage.EnumType.LogStatus.Valid;
                                        blacklistModel.CardNo = enterOutLog.CardNo;
                                        blacklistModel.OutBlacklistMode = roomInfo.Setting.BlackListSetting.LeaveBlacklist;
                                        if (blacklistModel.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.AutomaticMode)
                                        {
                                            blacklistModel.ReMark = string.Format("管理员{0}通过手持设备{0}把读者加入黑名单，记录黑名单{1}天", Session["LoginID"].ToString(), roomInfo.Setting.BlackListSetting.LimitDays);
                                            blacklistModel.OutTime = blacklistModel.AddTime.AddDays(roomInfo.Setting.BlackListSetting.LimitDays);
                                        }
                                        else
                                        {
                                            blacklistModel.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，手动离开黑名单", Session["LoginID"].ToString());
                                        }
                                        blacklistModel.ReadingRoomID = roomInfo.No;
                                        newId = SeatManage.Bll.T_SM_Blacklist.AddBlackList(blacklistModel);
                                        SeatManage.ClassModel.ReaderNoticeInfo blackRni = new SeatManage.ClassModel.ReaderNoticeInfo();
                                        blackRni.IsRead = SeatManage.EnumType.LogStatus.Valid;
                                        blackRni.CardNo = enterOutLog.CardNo;
                                        blackRni.Note = string.Format("{0}", blacklistModel.ReMark);
                                        SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(blackRni);
                                    }
                                    else if (rulesSet.BlacklistSet.Used)
                                    {
                                        SeatManage.ClassModel.BlackListInfo blacklistModel = new SeatManage.ClassModel.BlackListInfo();
                                        blacklistModel.AddTime = SeatManage.Bll.ServiceDateTime.Now;
                                        blacklistModel.OutTime = blacklistModel.AddTime.AddDays(rulesSet.BlacklistSet.LimitDays);
                                        blacklistModel.BlacklistState = SeatManage.EnumType.LogStatus.Valid;
                                        blacklistModel.CardNo = enterOutLog.CardNo;
                                        blacklistModel.OutBlacklistMode = rulesSet.BlacklistSet.LeaveBlacklist;
                                        if (blacklistModel.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.AutomaticMode)
                                        {
                                            blacklistModel.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，记录黑名单{1}天", Session["LoginID"].ToString(), rulesSet.BlacklistSet.LimitDays);
                                            blacklistModel.OutTime = blacklistModel.AddTime.AddDays(rulesSet.BlacklistSet.LimitDays);
                                        }
                                        else
                                        {
                                            blacklistModel.ReMark = string.Format("管理员{0}通过手持设备把读者加入黑名单，手动离开黑名单", Session["LoginID"].ToString());
                                        }
                                        blacklistModel.ReadingRoomID = roomInfo.No;
                                        newId = SeatManage.Bll.T_SM_Blacklist.AddBlackList(blacklistModel);
                                        SeatManage.ClassModel.ReaderNoticeInfo blackRni = new SeatManage.ClassModel.ReaderNoticeInfo();
                                        blackRni.IsRead = SeatManage.EnumType.LogStatus.Valid;
                                        blackRni.CardNo = enterOutLog.CardNo;
                                        blackRni.Note = string.Format("{0}", blacklistModel.ReMark);
                                        SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(blackRni);
                                    }
                                    else
                                    {
                                        Page.RegisterStartupScript("", "<script>alert('对不起，此阅览室以及图书馆没有启用黑名单功能');</script>");
                                        return;
                                    }
                                    if (newId > 0)
                                    {
                                        enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                                        enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                                        enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，通过手持设备设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, Session["LoginID"].ToString());

                                        SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId);
                                        if (result == SeatManage.EnumType.HandleResult.Successed)
                                        {
                                            Page.RegisterStartupScript("", "<script>alert('设置读者黑名单成功');</script>");
                                            GetSeatList(selectReadingRomm.Items[selectReadingRomm.SelectedIndex].Value, selectSeatState.Items[selectSeatState.SelectedIndex].Value);
                                        }
                                        else
                                        {
                                            Page.RegisterStartupScript("", "<script>alert('设置读者黑名单失败');</script>");
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        catch
                        {

                        }
                        break;
                    case "LoginOut":
                        HttpCookie aCookie;
                        string cookieName;
                        int limit = Request.Cookies.Count;
                        for (int i = 0; i < limit; i++)
                        {
                            cookieName = Request.Cookies[i].Name;
                            aCookie = new HttpCookie(cookieName);
                            aCookie.Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies.Add(aCookie);
                        }
                        Response.Redirect("../Login.aspx");
                        break;
                }
                #endregion
            }
        }
        /// <summary>
        /// 根据用户权限绑定阅览室
        /// </summary>
        /// <param name="loginId"></param>
        protected void BindReadingRoomList(string loginId)
        {
            ManagerPotency modelManagerPotency = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginId);
            List<ReadingRoomInfo> list = modelManagerPotency.RightRoomList;
            for (int i = 0; i < list.Count; i++)
            {
                ListItem items = new ListItem(list[i].Name, list[i].No);
                selectReadingRomm.Items.Add(items);
            }
        }

        /// <summary>
        /// 布局座位列表
        /// </summary>
        /// <param name="roomNum"></param>
        protected void GetSeatList(string roomNum, string state)
        {
            SeatLayout _SeatLayout = SeatManage.Bll.EnterOutOperate.GetRoomSeatLayOut(roomNum);
            StringBuilder seatHtml = new StringBuilder();
            foreach (Seat seat in _SeatLayout.Seats.Values)
            {
                if (seat.IsSuspended)
                {
                    continue;
                }
                string seatStyle = "";//座位样式
                string tipContent = "";
                string used = "";
                string onTime = "";
                switch (seat.SeatUsedState)
                {
                    case SeatManage.EnumType.EnterOutLogType.Leave:
                        seatStyle = "RealSeatFree";
                        tipContent = string.Format("座位空闲");
                        used = "空闲";
                        onTime = "0";
                        break;
                    case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                    case SeatManage.EnumType.EnterOutLogType.ComeBack:
                    case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                    case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        string time = String.Format("{0:HH:mm:ss}", seat.BeginUsedTime);
                        used = "在座";
                        if (!string.IsNullOrEmpty(seat.MarkTime.ToString()) && seat.MarkTime.CompareTo(DateTime.Parse("1900/1/1")) != 0)
                        {
                            onTime = "1";
                            System.TimeSpan ts = SeatManage.Bll.ServiceDateTime.Now.Subtract(seat.MarkTime);
                            string ontime = ts.Hours + "小时" + ts.Minutes + "分钟" + ts.Seconds + "秒";
                            seatStyle = "RealSeatOnTime";
                            tipContent = string.Format("学号：{0}<br />姓名：{1}<br />入座时间：{2}<br />已计时：{3}", seat.UserCardNo, seat.UserName, time, ontime);
                        }
                        else
                        {
                            onTime = "0";
                            seatStyle = "RealSeatBusy";
                            tipContent = string.Format("学号：{0}<br />姓名：{1}<br />入座时间：{2}", seat.UserCardNo, seat.UserName, time);
                        }
                        break;
                    case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                        onTime = "0";
                        seatStyle = "RealSeatLeave";
                        time = String.Format("{0:HH:mm:ss}", seat.BeginUsedTime);
                        tipContent = string.Format("学号：{0}<br />姓名：{1}<br />暂离时间：{2}", seat.UserCardNo, seat.UserName, time);
                        used = "暂离";
                        break;
                    case SeatManage.EnumType.EnterOutLogType.BespeakWaiting:
                        onTime = "0";
                        seatStyle = "RealSeatBusy";
                        tipContent = string.Format("等待预约读者确认入座");
                        used = "在座";
                        break;

                }
                switch (state)
                {
                    case "seated":
                        if (used == "在座")
                        {
                            //seatHtml.AppendFormat("<div data-inline='true' class='{0}' data-transition='none' data-role='button' onclick='tipShow(this,\"{1}\");checkSeat({2})'><input id='{3}' type='checkbox' name='ckbSeat' value='{4}' onclick='checkSeat({5})' Style=\"position:static\" /><span Style=\"font-size: 90%;\">{6}</span><br /><span Style=\"font-size: 90%;\">{7}</span></div>", seatStyle, tipContent, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.ShortSeatNo, used);
                            seatHtml.AppendFormat("<div class='{0}' onclick='tipShowPad(this,\"{1}\");checkSeat({2})' style='float: left; width: 75px;height: 70px;background-image: url('../Images/Pad/pink.png');background-repeat: no-repeat;vertical-align: middle;'><br/><input id='{3}' type='checkbox' name='ckbSeat' value='{4}' onclick='checkSeat({5})' Style=\"position:static\" /><span Style=\"font-size: 90%;\">{6}</span><br /><span Style=\"font-size: 90%;\">{7}</span></div>", seatStyle, tipContent, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.ShortSeatNo, used);
                        }
                        break;
                    case "shortLeave":
                        if (used == "暂离")
                        {
                            seatHtml.AppendFormat("<div  class='{0}' onclick='tipShowPad(this,\"{1}\");checkSeat({2})' style='float: left; width: 75px;height: 70px;background-image: url('../Images/Pad/blue.png');background-repeat: no-repeat;vertical-align: middle;'><br/><input id='{3}' type='checkbox' name='ckbSeat' value='{4}' onclick='checkSeat({5})'  Style=\"position:static\"/><span Style=\"font-size: 90%;\">{6}</span><br /><span Style=\"font-size: 90%;\">{7}</span></div>", seatStyle, tipContent, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.ShortSeatNo, used);
                        }
                        break;
                    case "onTime":
                        if (onTime == "1")
                        {
                            seatHtml.AppendFormat("<div class='{0}' onclick='tipShowPad(this,\"{1}\");checkSeat({2})' style='float: left; width: 75px;height: 70px;vertical-align: middle;background-image: url('../Images/Pad/onTime.png');background-repeat: no-repeat;'><br/><input id='{3}' type='checkbox' name='ckbSeat' value='{4}' onclick='checkSeat({5})' Style=\"position:static\" /><span Style=\"font-size: 90%;\">{6}</span><br /><span Style=\"font-size: 90%;\">{7}</span></div>", seatStyle, tipContent, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.ShortSeatNo, used);
                        }
                        break;
                    case "allSeat":
                        seatHtml.AppendFormat("<div class='{0}' onclick='tipShowPad(this,\"{1}\");checkSeat({2})' style='float: left; width: 75px;height: 70px;background-image: url('../Images/Pad/gray.png');background-repeat: no-repeat;vertical-align: middle;'><br/><input id='{3}' type='checkbox' name='ckbSeat' value='{4}' onclick='checkSeat({5})' Style=\"position:static\"/><span Style=\"font-size: 90%;\">{6}</span><br /><span Style=\"font-size: 90%;\">{7}</span></div>", seatStyle, tipContent, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.SeatNo, seat.ShortSeatNo, used);
                        break;
                }
            }
            seatContent.InnerHtml = seatHtml.ToString();
        }
    }
}