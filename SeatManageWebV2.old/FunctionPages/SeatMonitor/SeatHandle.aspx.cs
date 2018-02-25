using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SeatMonitor
{
    public partial class SeatHandle : BasePage
    {
        string seatNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            seatNo = Request.QueryString["seatNo"];
            string seatShortNo = Request.QueryString["seatShortNo"];
            string seatUsed = Request.QueryString["used"];
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "SeatGraph.aspx" && pageName != "FormSYS.aspx" && pageName != "MonitorListMode.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                        //PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                    //PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
                BindUIElement(seatNo, seatShortNo, seatUsed);
            }
            SeatManage.ClassModel.Seat seat = SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
            SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
            SeatManage.ClassModel.ReadingRoomInfo readingroom = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(seat.ReadingRoomNum);
            if ((!rulesSet.BlacklistSet.Used) && (!readingroom.Setting.BlackListSetting.Used))
            {
                btnAddBlackList.Enabled = false;
            }
        }
        /// <summary>
        /// 绑定UI元素
        /// </summary>
        private void BindUIElement(string seatNo, string seatShortNo, string seatUsed)
        {
            if (!string.IsNullOrEmpty(seatNo))
            {
                lblSeatNo.Text = seatShortNo;
                if (seatUsed == "0")
                {
                    lblCardNo.Text = "无";
                    lblName.Text = "无";
                    lblSeatStatus.Text = "空闲";
                    lblTimeLength.Text = "";
                    txtSeat.Text = seatShortNo;
                    btnAddBlackList.Enabled = false;
                    btnShortLeave.Enabled = false;
                    btnShortLeave.Text = "暂离";
                    btnShortLeave.ConfirmText = "是否确定把该读者设置为暂离？";
                    btnLeave.Enabled = false;
                    btnAllotSeat.Enabled = true;
                }
                else if (seatUsed == "2")
                {
                    List<SeatManage.ClassModel.BespeakLogInfo> list = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatNo, SeatManage.Bll.ServiceDateTime.Now);
                    if (list == null)
                    {
                        FineUI.Alert.ShowInTop("没有获取到相关的座位信息", "错误");
                    }
                    else
                    {
                        txtSeat.Text = seatShortNo;
                        lblCardNo.Text = list[0].CardNo;
                        lblName.Text = list[0].ReaderName;
                        lblSeatStatus.Text = "已被预约";
                        lblTimeLength.Text = string.Format("{0:MM月dd日 HH:mm:ss}", list[0].BsepeakTime);
                        btnAddBlackList.Enabled = false;
                        btnShortLeave.Enabled = false;
                        btnShortLeave.Text = "暂离";
                        btnShortLeave.ConfirmText = "是否确定把该读者设置为暂离？";
                        btnLeave.Enabled = false;
                        btnAllotSeat.Enabled = false;
                    }
                }
                else if (seatUsed == "3")
                {
                    FineUI.Alert.ShowInTop("此座位已暂停使用", "提示");
                    btnAddBlackList.Enabled = false;
                    btnShortLeave.Enabled = false;
                    btnShortLeave.ConfirmText = "此座位已暂停使用，请重新选择";
                    btnLeave.Enabled = false;
                    btnAllotSeat.Enabled = false;
                }
                else
                {
                    SeatManage.ClassModel.Seat seat = SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
                    if (seat == null)
                    {
                        FineUI.Alert.ShowInTop("没有获取到相关的座位信息", "错误");
                    }
                    else
                    {
                        if (seat.SeatUsedState == SeatManage.EnumType.EnterOutLogType.Leave)
                        {
                            lblCardNo.Text = "无";
                            lblName.Text = "无";
                            lblSeatStatus.Text = "空闲";
                            lblTimeLength.Text = "";
                            txtSeat.Text = seat.ShortSeatNo;
                            btnAddBlackList.Enabled = false;
                            btnShortLeave.Enabled = false;
                            btnShortLeave.Text = "暂离";
                            btnShortLeave.ConfirmText = "是否确定把该读者设置为暂离？";
                            btnLeave.Enabled = false;
                            btnAllotSeat.Enabled = true;
                        }
                        else if (seat.SeatUsedState == SeatManage.EnumType.EnterOutLogType.ShortLeave)
                        {
                            txtCardNo.Text = seat.UserCardNo;
                            txtCardNo1.Text = seat.UserCardNo;
                            txtReaderName.Text = seat.UserName;
                            txtSeat.Text = seat.ShortSeatNo;
                            lblCardNo.Text = seat.UserCardNo;
                            lblName.Text = seat.UserName;
                            lblSeatStatus.Text = SeatManage.SeatManageComm.SeatComm.ConvertReaderState(seat.SeatUsedState);
                            lblTimeLength.Text = string.Format("{0:MM月dd日 HH:mm:ss}", seat.BeginUsedTime);
                            btnAddBlackList.Enabled = true;
                            btnShortLeave.Enabled = true;
                            btnShortLeave.Text = "取消暂离";
                            btnShortLeave.ConfirmText = "是否取消此读者的暂离状态，并还原为在座状态？";
                            btnLeave.Enabled = true;
                            btnAllotSeat.Enabled = false;
                        }
                        else
                        {
                            txtCardNo.Text = seat.UserCardNo;
                            txtCardNo1.Text = seat.UserCardNo;
                            txtReaderName.Text = seat.UserName;
                            txtSeat.Text = seat.ShortSeatNo;
                            lblCardNo.Text = seat.UserCardNo;
                            lblName.Text = seat.UserName;
                            lblSeatStatus.Text = SeatManage.SeatManageComm.SeatComm.ConvertReaderState(seat.SeatUsedState);
                            lblTimeLength.Text = string.Format("{0:MM月dd日 HH:mm:ss}", seat.BeginUsedTime);
                            btnAddBlackList.Enabled = true;
                            btnShortLeave.Enabled = true;
                            btnShortLeave.Text = "暂离";
                            btnShortLeave.ConfirmText = "是否确定把该读者设置为暂离？";
                            btnLeave.Enabled = true;
                            btnAllotSeat.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                FineUI.Alert.ShowInTop("座位号不能为空", "错误");
            }
        }

        protected void btn_shortLeave(object sender, EventArgs e)
        {
            SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
            SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);
            if (btnShortLeave.Text == "暂离")
            {
                enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
                enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站设置为暂离", roomInfo.Name, enterOutLog.ShortSeatNo, this.LoginId);
                int newId = -1;
                SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLog, ref newId);
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    FineUI.Alert.ShowInTop("设置读者暂离成功", "成功");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    FineUI.Alert.ShowInTop("设置读者暂离失败", "失败");
                }
            }
            else
            {
                enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.ComeBack;
                enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
                enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站设置为在座", roomInfo.Name, enterOutLog.ShortSeatNo, this.LoginId);
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
                            FineUI.Alert.ShowInTop("取消读者暂离成功", "成功");
                            PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                        }
                        else
                        {
                            FineUI.Alert.ShowInTop("取消读者暂离成功，取消等待失败", "失败");
                        }
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("取消读者暂离成功", "成功");
                        PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    }
                }
                else
                {
                    FineUI.Alert.ShowInTop("取消读者暂离失败", "失败");
                }
            }

        }

        protected void btn_btnLeave(object sender, EventArgs e)
        {
            SeatManage.ClassModel.EnterOutLogInfo enterOutLog = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
            SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(enterOutLog.ReadingRoomNo);
            enterOutLog.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
            enterOutLog.Flag = SeatManage.EnumType.Operation.Admin;
            enterOutLog.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, this.LoginId);
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
                            violationRecords.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, this.LoginId);
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
                        violationRecords.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站设置离开", roomInfo.Name, enterOutLog.ShortSeatNo, this.LoginId);
                        violationRecords.BlacklistID = "-1";
                        SeatManage.Bll.T_SM_ViolateDiscipline.AddViolationRecords(violationRecords);
                    }
                }

                FineUI.Alert.ShowInTop("设置读者离开成功", "成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                FineUI.Alert.ShowInTop("设置读者离开失败", "失败");
            }
        }

        protected void btn_SureAddBlacklist(object sender, EventArgs e)
        {
            SeatManage.ClassModel.Seat seat = SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
            SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
            SeatManage.ClassModel.BlacklistSetting blacklistSet = rulesSet.BlacklistSet;
            SeatManage.ClassModel.ReadingRoomInfo readingroom = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(seat.ReadingRoomNum);
            int i = -1;
            if (readingroom != null && readingroom.Setting.BlackListSetting.Used)
            {
                SeatManage.ClassModel.BlackListInfo blacklistModel = new SeatManage.ClassModel.BlackListInfo();
                blacklistModel.AddTime = SeatManage.Bll.ServiceDateTime.Now;
                blacklistModel.ReadingRoomID = readingroom.No;
                blacklistModel.BlacklistState = SeatManage.EnumType.LogStatus.Valid;
                blacklistModel.CardNo = txtCardNo.Text;
                blacklistModel.OutBlacklistMode = readingroom.Setting.BlackListSetting.LeaveBlacklist;
                if (blacklistModel.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.AutomaticMode)
                {
                    blacklistModel.ReMark = string.Format("管理员{0}把读者加入黑名单，记录黑名单{1}天，备注：{2}", this.LoginId, readingroom.Setting.BlackListSetting.LimitDays, txtRemark.Text);
                    blacklistModel.OutTime = blacklistModel.AddTime.AddDays(readingroom.Setting.BlackListSetting.LimitDays);
                }
                else
                {
                    blacklistModel.ReMark = string.Format("管理员{0}把读者加入黑名单，手动离开黑名单，备注：{1}", this.LoginId, txtRemark.Text);
                }
                blacklistModel.ReadingRoomID = seat.ReadingRoomNum;
                i = SeatManage.Bll.T_SM_Blacklist.AddBlackList(blacklistModel);
                SeatManage.ClassModel.ReaderNoticeInfo blackRni = new SeatManage.ClassModel.ReaderNoticeInfo();
                blackRni.IsRead = SeatManage.EnumType.LogStatus.Valid;
                blackRni.CardNo = txtCardNo.Text;
                blackRni.Note = string.Format("{0}", blacklistModel.ReMark);
                SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(blackRni);
            }
            else if (blacklistSet.Used)
            {
                SeatManage.ClassModel.BlackListInfo blacklistModel = new SeatManage.ClassModel.BlackListInfo();
                blacklistModel.AddTime = SeatManage.Bll.ServiceDateTime.Now;
                blacklistModel.OutTime = blacklistModel.AddTime.AddDays(blacklistSet.LimitDays);
                blacklistModel.BlacklistState = SeatManage.EnumType.LogStatus.Valid;
                blacklistModel.CardNo = txtCardNo.Text;
                blacklistModel.OutBlacklistMode = blacklistSet.LeaveBlacklist;
                if (blacklistModel.OutBlacklistMode == SeatManage.EnumType.LeaveBlacklistMode.AutomaticMode)
                {
                    blacklistModel.ReMark = string.Format("管理员{0}把读者加入黑名单，记录黑名单{1}天，备注：{2}", this.LoginId, blacklistSet.LimitDays, txtRemark.Text);
                    blacklistModel.OutTime = blacklistModel.AddTime.AddDays(blacklistSet.LimitDays);
                }
                else
                {
                    blacklistModel.ReMark = string.Format("管理员{0}把读者加入黑名单，手动离开黑名单，备注：{1}", this.LoginId, txtRemark.Text);
                }
                blacklistModel.ReadingRoomID = seat.ReadingRoomNum;
                i = SeatManage.Bll.T_SM_Blacklist.AddBlackList(blacklistModel);
                SeatManage.ClassModel.ReaderNoticeInfo blackRni = new SeatManage.ClassModel.ReaderNoticeInfo();
                blackRni.IsRead = SeatManage.EnumType.LogStatus.Valid;
                blackRni.CardNo = txtCardNo.Text;
                blackRni.Note = string.Format("{0}", blacklistModel.ReMark);
                SeatManage.Bll.T_SM_ReaderNotice.AddReaderNotice(blackRni);
            }
            else
            {
                FineUI.Alert.ShowInTop("对不起，此阅览室以及图书馆没有启用黑名单功能", "失败");
                return;
            }
            if (i > 0)
            {
                SeatManage.ClassModel.EnterOutLogInfo enterOutLogModel = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogInfoByCardNo(txtCardNo.Text);
                enterOutLogModel.EnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
                enterOutLogModel.Flag = SeatManage.EnumType.Operation.Admin;
                enterOutLogModel.Remark = string.Format("在{0}，{1}号座位，被管理员{2}，在后台管理网站加入黑名单并释放座位", enterOutLogModel.ReadingRoomName, enterOutLogModel.ShortSeatNo, this.LoginId);
                SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLogModel, ref i);
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    FineUI.Alert.ShowInTop("黑名单添加成功", "成功");
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    FineUI.Alert.ShowInTop("黑名单添加失败", "失败");
                }
            }
            else
            {
                FineUI.Alert.ShowInTop("黑名单添加失败", "失败");
            }
        }

        protected void btnSureAllotSeat_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.Seat seat = SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
            SeatManage.ClassModel.ReadingRoomInfo roomInfo = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(seat.ReadingRoomNum);
            if (seat == null)
            {
                FineUI.Alert.ShowInTop("座位号错误，没有找到座位的相关信息");
                return;
            }
            //判断当前座位上是否有读者在座。
            SeatManage.ClassModel.EnterOutLogInfo enterOutLogByseatNo = SeatManage.Bll.T_SM_EnterOutLog.GetUsingEnterOutLogBySeatNo(seatNo);
            if (enterOutLogByseatNo != null && enterOutLogByseatNo.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
            {
                FineUI.Alert.ShowInTop("座位已经被其他读者选择");
                return;
            }
            //判断读者是否有座位
            string strCardNo = txtCardNo1.Text;
            List<SeatManage.ClassModel.BlackListInfo> blacklistInfoByCardNo = SeatManage.Bll.T_SM_Blacklist.GetBlackListInfo(strCardNo);
            SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
            if (roomInfo.Setting.UsedBlacklistLimit && blacklistInfoByCardNo.Count > 0)
            {
                if (roomInfo.Setting.BlackListSetting.Used)
                {
                    bool isblack = false;
                    foreach (SeatManage.ClassModel.BlackListInfo blinfo in blacklistInfoByCardNo)
                    {
                        if (blinfo.ReadingRoomID == roomInfo.No)
                        {
                            isblack = true;
                            break;
                        }
                    }
                    if (isblack)
                    {
                        FineUI.Alert.ShowInTop("该读者已进入黑名单，不能在该阅览室为其分配座位");
                        return;
                    }
                }
                else
                {
                    FineUI.Alert.ShowInTop("该读者已进入黑名单，不能在该阅览室为其分配座位");
                    return;
                }
            }
            SeatManage.ClassModel.EnterOutLogInfo enterOutLogByCardNo = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogInfoByCardNo(strCardNo);
            if (enterOutLogByCardNo != null && enterOutLogByCardNo.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
            {
                FineUI.Alert.ShowInTop(string.Format("读者已经在{0}，{1}号座位就做", enterOutLogByCardNo.ReadingRoomName, enterOutLogByCardNo.ShortSeatNo));
                return;
            }

            SeatManage.ClassModel.EnterOutLogInfo enterOutLogModel = new SeatManage.ClassModel.EnterOutLogInfo();
            enterOutLogModel.CardNo = strCardNo;
            enterOutLogModel.EnterOutLogNo = SeatManage.SeatManageComm.SeatComm.RndNum();
            enterOutLogModel.EnterOutState = SeatManage.EnumType.EnterOutLogType.SelectSeat;
            enterOutLogModel.EnterOutTime = SeatManage.Bll.ServiceDateTime.Now;
            enterOutLogModel.EnterOutType = SeatManage.EnumType.LogStatus.Valid;
            enterOutLogModel.Flag = SeatManage.EnumType.Operation.Admin;
            enterOutLogModel.ReadingRoomNo = seat.ReadingRoomNum;
            enterOutLogModel.Remark = string.Format("在后台管理网站被管理员{0}，分配{1}，{2}座位", this.LoginId, roomInfo.Name, seat.SeatNo.Substring(seat.SeatNo.Length - roomInfo.Setting.SeatNumAmount));
            enterOutLogModel.SeatNo = seatNo;
            int newId = -1;
            SeatManage.EnumType.HandleResult result = SeatManage.Bll.EnterOutOperate.AddEnterOutLog(enterOutLogModel, ref newId);
            if (result == SeatManage.EnumType.HandleResult.Successed)
            {
                FineUI.Alert.ShowInTop("分配座位成功", "成功");
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
            }
            else
            {
                FineUI.Alert.ShowInTop("分配座位失败", "失败");
            }
        }
    }
}