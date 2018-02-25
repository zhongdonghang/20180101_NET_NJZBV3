using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakNowDayHandle : BasePage
    {
        string seatNo = "";
        string seatShortNo = "";
        DateTime date;
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "BespeakNowDaySeatLayout.aspx" && pageName != "FormSYS.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
            }
            string parameters = Request.QueryString["parameters"];
            SeatManageWebV2.Code.BespeakSubmitWindowParamModel bespeakSubmitModel = new Code.BespeakSubmitWindowParamModel(parameters);
            seatNo = bespeakSubmitModel.SeatNo;
            seatShortNo = bespeakSubmitModel.ShortSeatNo;
            date = SeatManage.Bll.ServiceDateTime.Now;
            roomNo = bespeakSubmitModel.RoomNo;
            if (!IsCanBespeak())
            {
                btnBespeak.Enabled = false;
                return;
            }
            if (!IsPostBack)
            {
                BindUIElement(seatNo, seatShortNo, date);
            }
        }
        /// <summary>
        /// 判断座位是否符合预约条件
        /// </summary>
        /// <returns></returns>
        protected bool IsCanBespeak()
        {
            bool result = true;
            SeatManage.ClassModel.ReadingRoomSetting set = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo).Setting;
            if (!set.SeatBespeak.Used || !set.SeatBespeak.NowDayBespeak)
            {
                FineUI.Alert.ShowInTop("阅览室没有开放预约");
                result = false;
            }
            return result;

        }


        protected void btnBespeak_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.BespeakLogInfo bespeakModel = new SeatManage.ClassModel.BespeakLogInfo();
            bespeakModel.BsepeakState = SeatManage.EnumType.BookingStatus.Waiting;
            bespeakModel.BsepeakTime = date;
            bespeakModel.CardNo = this.LoginId;
            bespeakModel.ReadingRoomNo = roomNo.Trim();
            if (rblModel.SelectedValue == "1")
            {
                if (!DropDownList_Time.Hidden == true)
                {
                    bespeakModel.BsepeakTime = DateTime.Parse(string.Format("{0} {1}", date.ToShortDateString(), DropDownList_Time.SelectedText));
                }
                else
                {
                    bespeakModel.BsepeakTime = DateTime.Parse(string.Format("{0} {1}", date.ToShortDateString(), DropDownList_FreeTime.SelectedText));
                }
            }
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(bespeakModel.ReadingRoomNo);
            if (room.Setting.ReadingRoomOpenState(bespeakModel.BsepeakTime) == ReadingRoomStatus.Close)
            {
                FineUI.Alert.ShowInTop("对不起，当前时间阅览室未开放。");
                PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
            }
            bespeakModel.Remark = string.Format("读者通过Web页面预约当天座位");
            bespeakModel.SeatNo = seatNo;
            bespeakModel.SubmitTime = date;
            if (bespeakModel.BsepeakTime < bespeakModel.SubmitTime)
            {
                FineUI.Alert.ShowInTop("对不起，预约的时间不正确，请刷新页面重新选择。");
                PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
            }
            try
            {
                Seat seatInfo =  SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
                if (bespeakModel.BsepeakTime == DateTime.Now.Date && seatInfo.SeatUsedState != EnterOutLogType.Leave) //如果启用预约，判断选择的日期是否为当天的日期
                {
                    FineUI.Alert.ShowInTop("对不起，你所预约的座位正在使用。");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
                List<SeatManage.ClassModel.BespeakLogInfo> seatbespeakLog = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatNo, date);
                if (seatbespeakLog.Count > 0)
                {
                    FineUI.Alert.ShowInTop("对不起，该座位已经被别人预约。");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
                SeatManage.EnumType.HandleResult result = SeatManage.Bll.T_SM_SeatBespeak.AddBespeakLogInfo(bespeakModel);
                if (result == SeatManage.EnumType.HandleResult.Successed)
                {
                    FineUI.Alert.ShowInTop("座位预约成功，请在规定的时间内刷卡确认。");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
                else
                {
                    FineUI.Alert.ShowInTop("预约失败，该座位已经被别人预约。");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                }
            }
            catch (Exception ex)
            {
                FineUI.Alert.ShowInTop(string.Format("执行预约操作遇到错误：{0}", ex.Message));
                PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
            }
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
        }

        /// <summary>
        /// 绑定UI元素
        /// </summary>
        /// <param name="seatNo"></param>
        /// <param name="seatShortNo"></param>GetBespeakLogInfoBySeatNo
        void BindUIElement(string seatNo, string seatShortNo, DateTime date)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            SeatManage.Bll.T_SM_ReadingRoom bllReadingRoom = new SeatManage.Bll.T_SM_ReadingRoom();
            foreach (DateTime dt in room.Setting.SeatBespeak.SpecifiedTimeList)
            {
                if (dt <= date)
                {
                    continue;
                }
                DropDownList_Time.Items.Add(new FineUI.ListItem(dt.ToShortTimeString(), dt.ToShortTimeString()));
            }
            DateTime minTime = date.AddMinutes(10 - date.Minute % 10);
            while (true)
            {
                minTime = minTime.AddMinutes(10);
                if (minTime.Date > date.Date)
                {
                    break;
                }
                if (Code.NowReadingRoomState.ReadingRoomOpenState(room.Setting.RoomOpenSet, minTime) == SeatManage.EnumType.ReadingRoomStatus.Close)
                {
                    continue;
                }
                DropDownList_FreeTime.Items.Add(new FineUI.ListItem(minTime.ToShortTimeString(), minTime.ToShortTimeString()));
            }
            if (!room.Setting.SeatBespeak.SpecifiedBespeak)
            {
                rblModel.Hidden = true;
            }
            if (rblModel.SelectedValue == "0")
            {
                DropDownList_FreeTime.Hidden = true;
                DropDownList_Time.Hidden = true;
            }
            else
            {
                if (!room.Setting.SeatBespeak.SpecifiedTime)
                {
                    DropDownList_Time.Hidden = true;
                    DropDownList_FreeTime.Hidden = false;
                }
                else
                {
                    DropDownList_FreeTime.Hidden = true;
                    DropDownList_Time.Hidden = false;
                }
            }
            lblbeginDate.Text = date.ToShortDateString();
            this.lblSeatNo.Text = seatShortNo;
            this.lblRoomName.Text = room.Name;
            //判断自己是否已经预约座位
            List<SeatManage.EnumType.BookingStatus> bespeakStatus = new List<SeatManage.EnumType.BookingStatus>();
            bespeakStatus.Add(SeatManage.EnumType.BookingStatus.Waiting);
            List<SeatManage.ClassModel.BespeakLogInfo> readerBespeaklist = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoByCardNo(this.LoginId, date);//.GetBespeakList(this.LoginId, null, date, 0, bespeakStatus);
            if (readerBespeaklist.Count > 0)
            {
                FineUI.Alert.ShowInTop("您今天已有预约的座位，请先取消原来的预约。");
                btnBespeak.Enabled = false;
                PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                return;
            }
            //判断可预约次数是否超过
            readerBespeaklist = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakList(this.LoginId, null, date, 0, new List<SeatManage.EnumType.BookingStatus> { SeatManage.EnumType.BookingStatus.Confinmed });//.GetBespeakList(this.LoginId, null, date, 0, bespeakStatus);
            if (readerBespeaklist.Count >= room.Setting.SeatBespeak.BespeakSeatCount)
            {
                FineUI.Alert.ShowInTop("您一天只能预约" + room.Setting.SeatBespeak.BespeakSeatCount + "次座位。");
                btnBespeak.Enabled = false;
                PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                return;
            }
            //判断读者是否有座位
            if (!room.Setting.SeatBespeak.BespeatWithOnSeat)
            {
                SeatManage.ClassModel.EnterOutLogInfo eol = SeatManage.Bll.T_SM_EnterOutLog.GetEnterOutLogInfoByCardNo(this.LoginId);
                if (eol != null && eol.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    FineUI.Alert.ShowInTop("您已有座位，不能再预约座位");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            //判断操作次数
            if (SeatManage.Bll.EnterOutOperate.CheckReaderChooseSeatTimesByReadingRoom(this.LoginId, room.Setting.PosTimes, room.No))
            {
                FineUI.Alert.ShowInTop("您的选座/预约操作过于频繁，请稍后重试");
                btnBespeak.Enabled = false;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                return;
            }
            //判断座位是否被别人使用
            SeatManage.ClassModel.Seat seat = SeatManage.Bll.T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
            if (seat.SeatUsedState != SeatManage.EnumType.EnterOutLogType.Leave)
            {
                FineUI.Alert.ShowInTop("座位已经被别人选择，请预约其他座位");
                btnBespeak.Enabled = false;
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                return;
            }
            //判断座位是否被预约
            List<SeatManage.ClassModel.BespeakLogInfo> list = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatNo, date);
            seatKeepTime.Value = room.Setting.SeatBespeak.SeatKeepTime.ToString();
            this.lblEndDate.Text = string.Format("{0}至{1}", date.ToShortTimeString(), date.AddMinutes(room.Setting.SeatBespeak.SeatKeepTime).ToShortTimeString());
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].BsepeakState == SeatManage.EnumType.BookingStatus.Waiting)
                {
                    FineUI.Alert.ShowInTop("座位已经被别人预约，请预约其他座位");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            //判断是否已加入黑名单
            List<SeatManage.ClassModel.BlackListInfo> blacklistInfoByCardNo = SeatManage.Bll.T_SM_Blacklist.GetBlackListInfo(this.LoginId);
            SeatManage.ClassModel.RegulationRulesSetting rulesSet = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting();
            if (room.Setting.UsedBlacklistLimit && blacklistInfoByCardNo.Count > 0)
            {
                if (room.Setting.BlackListSetting.Used)
                {
                    bool isblack = false;
                    foreach (SeatManage.ClassModel.BlackListInfo blinfo in blacklistInfoByCardNo)
                    {
                        if (blinfo.ReadingRoomID == room.No)
                        {
                            isblack = true;
                            break;
                        }
                    }
                    if (isblack)
                    {
                        FineUI.Alert.ShowInTop("你已进入黑名单，不能在该阅览室预约座位");
                        PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                        return;
                    }
                }
                else
                {
                    FineUI.Alert.ShowInTop("你已进入黑名单，不能在该阅览室预约座位");
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            if (room.Setting.LimitReaderEnter.Used)
            {
                SeatManage.ClassModel.ReaderInfo readerInfo = SeatManage.Bll.EnterOutOperate.GetReaderInfo(this.LoginId);
                string[] litype = room.Setting.LimitReaderEnter.ReaderTypes.Split(';');
                if (!room.Setting.LimitReaderEnter.CanEnter)
                {
                    foreach (string type in litype)
                    {
                        if (type == readerInfo.ReaderType)
                        {
                            FineUI.Alert.ShowInTop("你的读者类型不能在该阅览室预约座位");
                            PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                            return;
                        }
                    }
                }
                else
                {
                    bool isintype = false;
                    foreach (string type in litype)
                    {
                        if (type == readerInfo.ReaderType)
                        {
                            isintype = true;
                            break;
                        }
                    }
                    if (!isintype)
                    {
                        FineUI.Alert.ShowInTop("你的读者类型不能在该阅览室预约座位");
                        PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                        return;
                    }
                }
            }

            btnBespeak.Enabled = true;

        }

        protected void DropDownList_FreeTime_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            DateTime bespeakTime = DateTime.Parse(DropDownList_FreeTime.SelectedText);
            DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
            DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
            lblEndDate.Text = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
        }

        protected void DropDownList_Time_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            DateTime bespeakTime = DateTime.Parse(DropDownList_Time.SelectedText);
            DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
            DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
            lblEndDate.Text = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
        }

        protected void rblModel_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo);
            DateTime bespeakTime = date;
            if (rblModel.SelectedValue == "0")
            {
                DropDownList_FreeTime.Hidden = true;
                DropDownList_Time.Hidden = true;
                this.lblEndDate.Text = string.Format("{0}至{1}", bespeakTime.ToShortTimeString(), bespeakTime.AddMinutes(room.Setting.SeatBespeak.SeatKeepTime).ToShortTimeString());
            }
            else
            {
                if (!room.Setting.SeatBespeak.SpecifiedTime)
                {
                    if (DropDownList_FreeTime.Items.Count < 1)
                    {
                        FineUI.Alert.ShowInTop("对不起当前时间已超过最后可预约的时间段");
                        rblModel.SelectedIndex = 0;
                        return;
                    }
                    DropDownList_Time.Hidden = true;
                    DropDownList_FreeTime.Hidden = false;
                    DropDownList_FreeTime.SelectedIndex = 0;
                    bespeakTime = DateTime.Parse(DropDownList_FreeTime.SelectedText);
                    DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
                    DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
                    lblEndDate.Text = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
                }
                else
                {
                    if (DropDownList_Time.Items.Count < 1)
                    {
                        FineUI.Alert.ShowInTop("对不起当前时间已超过最后可预约的时间段");
                        rblModel.SelectedIndex = 0;
                        return;
                    }
                    DropDownList_FreeTime.Hidden = true;
                    DropDownList_Time.Hidden = false;
                    DropDownList_Time.SelectedIndex = 0;
                    bespeakTime = DateTime.Parse(DropDownList_Time.SelectedText);
                    DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
                    DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
                    lblEndDate.Text = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
                }
            }
        }
    }
}