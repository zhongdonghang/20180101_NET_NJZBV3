using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakSubmitWindow : BasePage
    {
        string seatNo = "";
        string seatShortNo = "";
        string date = "";
        string roomNo = "";
        List<DateTime> timeSpan = new List<DateTime>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "BespeakSeatLayout.aspx" && pageName != "FormSYS.aspx")
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
            date = DateTime.FromBinary(long.Parse(bespeakSubmitModel.BespeakDate)).ToString();
            roomNo = bespeakSubmitModel.RoomNo;
            timeSpan = bespeakSubmitModel.TimeSpan;
            DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
            if (!IsCanBespeak())
            {
                btnBespeak.Enabled = false;
                return;
            }
            if (!IsPostBack)
            {
                BindUIElement(seatNo, seatShortNo, DateTime.Parse(date));
            }
        }
        /// <summary>
        /// 判断座位是否符合预约条件
        /// </summary>
        /// <returns></returns>
        protected bool IsCanBespeak()
        {
            bool result = true;
            DateTime nowDate = SeatManage.Bll.ServiceDateTime.Now;
            SeatManage.ClassModel.ReadingRoomSetting set = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(roomNo).Setting;
            if (!set.SeatBespeak.Used)
            {
                FineUI.Alert.ShowInTop("阅览室没有开放预约");
                result = false;
            }
            if (!dateBespeak(set.SeatBespeak, nowDate))
            {
                FineUI.Alert.ShowInTop("该日期不能预约");
                result = false;
            }
            if (!timeCanBespeak(set.SeatBespeak, nowDate))
            {
                FineUI.Alert.ShowInTop(string.Format("预约时间为：{0}到{1}", set.SeatBespeak.CanBespeatTimeSpace.BeginTime, set.SeatBespeak.CanBespeatTimeSpace.EndTime));
                result = false;
            }
            return result;

        }

        /// <summary>
        /// 判断选择的日期是否可以预约，false为不可预约
        /// </summary>
        /// <param name="set"></param>
        /// <returns></returns>
        private bool dateBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {
            DateTime selectedDate = DateTime.Parse(date);
            for (int i = 0; i < set.NoBespeakDates.Count; i++)
            {
                try
                {
                    DateTime beginDate = DateTime.Parse(set.NoBespeakDates[i].BeginTime);
                    DateTime endDate = DateTime.Parse(set.NoBespeakDates[i].EndTime);
                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginDate, endDate, selectedDate))
                    {//如果当前时间符合某个不可预约的规则，则直接返回false，不可预约
                        return false;
                    }
                }
                catch
                {//日期转换遇到异常，则忽略 
                }
            }
            //判断当天是否大于选择的日期
            TimeSpan span = selectedDate.Date - nowDate.Date;
            if (span.Days > set.BespeakBeforeDays)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// 判断当前时间是否可以预约
        /// </summary>
        /// <param name="set"></param>
        /// <param name="nowDate"></param>
        /// <returns></returns>
        private bool timeCanBespeak(SeatManage.ClassModel.SeatBespeakSet set, DateTime nowDate)
        {
            try
            {
                DateTime beginTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.BeginTime));
                DateTime endTime = DateTime.Parse(string.Format("{0} {1}", nowDate.ToShortDateString(), set.CanBespeatTimeSpace.EndTime));
                if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(beginTime, endTime, nowDate))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return true;
            }
        }

        protected void btnBespeak_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.BespeakLogInfo bespeakModel = new SeatManage.ClassModel.BespeakLogInfo();
            bespeakModel.BsepeakState = SeatManage.EnumType.BookingStatus.Waiting;
            DateTime bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), roomOpenTime.Value));
            if (rblModel.SelectedValue == "1")
            {
                if (!DropDownList_Time.Hidden == true)
                {
                    bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), DropDownList_Time.SelectedText));
                }
                else
                {
                    bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), DropDownList_FreeTime.SelectedText));
                }
            }
            bespeakModel.BsepeakTime = bespeatDate;
            bespeakModel.CardNo = this.LoginId;
            bespeakModel.ReadingRoomNo = roomNo.Trim();
            bespeakModel.Remark = string.Format("读者通过Web页面预约座位");
            bespeakModel.SeatNo = seatNo;
            bespeakModel.SubmitTime = SeatManage.Bll.ServiceDateTime.Now;

            List<SeatManage.ClassModel.BespeakLogInfo> list = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatNo, DateTime.Parse(date).Date);
            foreach (SeatManage.ClassModel.BespeakLogInfo b in list)
            {
                if (b.BsepeakTime == bespeakModel.BsepeakTime)
                {
                    FineUI.Alert.ShowInTop("对不起，此时间段已被预约。");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            List<SeatManage.ClassModel.BespeakLogInfo> readerBespeaklist = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakList(this.LoginId, null, DateTime.Parse(date).Date, 0, new List<SeatManage.EnumType.BookingStatus> { SeatManage.EnumType.BookingStatus.Waiting });
            foreach (SeatManage.ClassModel.BespeakLogInfo b in readerBespeaklist)
            {
                if (b.BsepeakTime == bespeakModel.BsepeakTime)
                {
                    FineUI.Alert.ShowInTop("对不起，同一时间段只能预约一个座位。");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            try
            {
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
            if (room.Setting.SeatBespeak.SpecifiedTime && room.Setting.SeatBespeak.CanBookMultiSpan)
            {
                foreach (DateTime dt in timeSpan)
                {
                    DropDownList_Time.Items.Add(new FineUI.ListItem(dt.ToShortTimeString(), dt.ToShortTimeString()));
                }
            }
            else
            {
                foreach (DateTime dt in room.Setting.SeatBespeak.SpecifiedTimeList)
                {
                    DropDownList_Time.Items.Add(new FineUI.ListItem(dt.ToShortTimeString(), dt.ToShortTimeString()));
                }
            }
            DateTime minTime = DateTime.Parse(date.ToShortDateString() + " " + bllReadingRoom.GetRoomOpenTimeByDate(room.Setting, date.ToShortDateString()).BeginTime);
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
            List<SeatManage.ClassModel.BespeakLogInfo> list = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoBySeatNo(seatNo, date);
            List<SeatManage.EnumType.BookingStatus> bespeakStatus = new List<SeatManage.EnumType.BookingStatus>();
            bespeakStatus.Add(SeatManage.EnumType.BookingStatus.Waiting);
            List<SeatManage.ClassModel.BespeakLogInfo> readerBespeaklist = SeatManage.Bll.T_SM_SeatBespeak.GetBespeakLogInfoByCardNo(this.LoginId, date);//.GetBespeakList(this.LoginId, null, date, 0, bespeakStatus);
            if (room.Setting.SeatBespeak.CanBookMultiSpan && room.Setting.SeatBespeak.SpecifiedTime)
            {
                if (readerBespeaklist.Count >= room.Setting.SeatBespeak.BespeakSeatCount)
                {
                    FineUI.Alert.ShowInTop("对不起，您一天最多预约" + room.Setting.SeatBespeak.BespeakSeatCount + "个座位。");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            else
            {
                if (readerBespeaklist.Count > 0)
                {
                    FineUI.Alert.ShowInTop("您选择的日期已经预约了座位，请先取消原来的预约。");
                    btnBespeak.Enabled = false;
                    PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    return;
                }
            }
            //判断座位是否被别人预约

            roomOpenTime.Value = bllReadingRoom.GetRoomOpenTimeByDate(room.Setting, date.ToShortDateString()).BeginTime;
            this.lblEndDate.Text = bespeakSureTimeSpan(room.Setting);
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

        string bespeakSureTimeSpan(SeatManage.ClassModel.ReadingRoomSetting set)
        {
            SeatManage.Bll.T_SM_ReadingRoom bllReadingRoom = new SeatManage.Bll.T_SM_ReadingRoom();
            DateTime bespeakTime = Convert.ToDateTime(bllReadingRoom.GetRoomOpenTimeByDate(set, date).BeginTime);
            if (rblModel.SelectedValue == "1")
            {
                if (set.SeatBespeak.SpecifiedTime)
                {
                    bespeakTime = set.SeatBespeak.SpecifiedTimeList[0];
                }
            }
            DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
            DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
            return string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
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
            SeatManage.Bll.T_SM_ReadingRoom bllReadingRoom = new SeatManage.Bll.T_SM_ReadingRoom();
            DateTime bespeakTime = Convert.ToDateTime(bllReadingRoom.GetRoomOpenTimeByDate(room.Setting, date).BeginTime);
            if (rblModel.SelectedValue == "0")
            {
                DropDownList_FreeTime.Hidden = true;
                DropDownList_Time.Hidden = true;
                DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(room.Setting.SeatBespeak.ConfirmTime.BeginTime));
                DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(room.Setting.SeatBespeak.ConfirmTime.EndTime));
                lblEndDate.Text = string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
            }
            else
            {
                if (!room.Setting.SeatBespeak.SpecifiedTime)
                {
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