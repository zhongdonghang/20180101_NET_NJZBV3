using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.EnumType;
using SeatManage.ClassModel;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class ReadingRoomSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    DataBind();
                    btnSetBespeakSeat.OnClientClick = WindowEdit.GetShowReference(string.Format("BespeakSeatSetting.aspx?roomId={0}", Request.QueryString["id"]), "指定预约座位");
                }
                else
                {
                    FineUI.PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("阅览室设置获取错误！");

                }
            }
        }

        void SelectAllRR_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            AuthorizeVerify.FunctionAuthorizeInfo authorize = SeatManage.Bll.AuthorizationOperation.GetFunctionAuthorize();
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(Request.QueryString["id"]);
            if (room == null)
            {
                room = new SeatManage.ClassModel.ReadingRoomInfo();
            }
            SeatManage.ClassModel.ReadingRoomSetting roomSet = room.Setting;
            if (roomSet == null)
            {
                roomSet = new SeatManage.ClassModel.ReadingRoomSetting();
            }
            //选座模式设置
            SeatSelectDefaultMode.SelectedValue = ((int)roomSet.SeatChooseMethod.DefaultChooseMethod).ToString();
            SeatSelectAdMode.Checked = roomSet.SeatChooseMethod.UsedAdvancedSet;
            //SelectSeatPosTimes.Text = roomSet.PosTimes.Times.ToString();
            SelectSeatPosCount.Text = roomSet.PosTimes.Times.ToString();
            SelectSeatPosTimes.Text = roomSet.PosTimes.Minutes.ToString();
            SeatSelectPos.Checked = roomSet.PosTimes.IsUsed;
            //if (SeatSelectAdMode.Checked)
            //{
            //    SeatSelectAdModeTable.Style["display"] = "block";
            //}
            //else
            //{
            //    SeatSelectAdModeTable.Style["display"] = "none";
            //}
            //高级设置
            foreach (KeyValuePair<DayOfWeek, SeatChooseMethodPlan> day in roomSet.SeatChooseMethod.AdvancedSelectSeatMode)
            {
                string dayNum = ((int)day.Value.Day).ToString();
                CheckBox DayCheck = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum) as CheckBox;
                DayCheck.Checked = day.Value.Used;
                for (int i = 0; i < day.Value.PlanOption.Count; i++)
                {
                    string[] begintime = day.Value.PlanOption[i].UsedTime.BeginTime.Split(':');
                    string[] endtime = day.Value.PlanOption[i].UsedTime.EndTime.Split(':');
                    TextBox begintimeH = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_StartH") as TextBox;
                    TextBox begintimeM = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_StartM") as TextBox;
                    TextBox endtimeH = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_EndH") as TextBox;
                    TextBox endtimeM = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_EndM") as TextBox;
                    RadioButtonList selectmode = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_SelectMode") as RadioButtonList;
                    begintimeH.Text = begintime[0];
                    begintimeM.Text = begintime[1];
                    endtimeH.Text = endtime[0];
                    endtimeM.Text = endtime[1];
                    selectmode.SelectedValue = ((int)day.Value.PlanOption[i].ChooseMethod).ToString();
                }
            }
            //暂离设置
            ShortLeaveDufaultTime.Text = roomSet.SeatHoldTime.DefaultHoldTimeLength.ToString();
            ShortLeaveAdMode.Checked = roomSet.SeatHoldTime.UsedAdvancedSet;
            //if (ShortLeaveAdMode.Checked)
            //{
            //    ShortLeaveAdModeTable.Style["display"] = "block";
            //}
            //else
            //{
            //    ShortLeaveAdModeTable.Style["display"] = "none";
            //}
            //高级设置
            for (int i = 0; i < roomSet.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
            {
                string[] begintime = roomSet.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime.Split(':');
                string[] endtime = roomSet.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime.Split(':');
                TextBox begintimeH = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_StartH") as TextBox;
                TextBox begintimeM = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_StartM") as TextBox;
                TextBox endtimeH = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_EndH") as TextBox;
                TextBox endtimeM = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_EndM") as TextBox;
                TextBox leavetime = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_LeaveTime") as TextBox;
                CheckBox used = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1)) as CheckBox;
                begintimeH.Text = begintime[0];
                begintimeM.Text = begintime[1];
                endtimeH.Text = endtime[0];
                endtimeM.Text = endtime[1];
                leavetime.Text = roomSet.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                used.Checked = roomSet.SeatHoldTime.AdvancedSeatHoldTime[i].Used;
            }
            //开闭馆计划设置
            string[] opentime = roomSet.RoomOpenSet.DefaultOpenTime.BeginTime.Split(':');
            string[] closetime = roomSet.RoomOpenSet.DefaultOpenTime.EndTime.Split(':');
            ReadingRoomDufaultOpenTimeH.Text = opentime[0];
            ReadingRoomDufaultOpenTimeM.Text = opentime[1];
            ReadingRoomBeforeOpenTime.Text = roomSet.RoomOpenSet.OpenBeforeTimeLength.ToString();
            ReadingRoomDufaultCloseTimeH.Text = closetime[0];
            ReadingRoomDufaultCloseTimeM.Text = closetime[1];
            ReadingRoomBeforeCloseTime.Text = roomSet.RoomOpenSet.CloseBeforeTimeLength.ToString();
            ReadingRoomOpen24H.Checked = roomSet.RoomOpenSet.UninterruptibleModel;
            //验证授权
            //if (authorize != null && !authorize.SystemFunction.Contains("RoomOC_24HModel"))
            //{
            //    open24htr.Style["display"] = "none";
            //}
            //高级设置
            ReadingRoomOpenCloseAdMode.Checked = roomSet.RoomOpenSet.UsedAdvancedSet;
            //if (ReadingRoomOpenCloseAdMode.Checked)
            //{
            //    ReadingRoomOpenCloseAdModeTable.Style["display"] = "block";
            //}
            //else
            //{
            //    ReadingRoomOpenCloseAdModeTable.Style["display"] = "none";
            //}
            foreach (KeyValuePair<DayOfWeek, RoomOpenPlanSet> day in roomSet.RoomOpenSet.RoomOpenPlan)
            {
                string dayNum = ((int)day.Value.Day).ToString();
                CheckBox DayCheck = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum) as CheckBox;
                DayCheck.Checked = day.Value.Used;
                for (int i = 0; i < day.Value.OpenTime.Count; i++)
                {
                    string[] begintime = day.Value.OpenTime[i].BeginTime.Split(':');
                    string[] endtime = day.Value.OpenTime[i].EndTime.Split(':');
                    TextBox begintimeH = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_OpenH") as TextBox;
                    TextBox begintimeM = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_OpenM") as TextBox;
                    TextBox endtimeH = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_CloseH") as TextBox;
                    TextBox endtimeM = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_CloseM") as TextBox;
                    begintimeH.Text = begintime[0];
                    begintimeM.Text = begintime[1];
                    endtimeH.Text = endtime[0];
                    endtimeM.Text = endtime[1];
                }
            }
            ShortLeaveByAdmin.Checked = roomSet.AdminShortLeave.IsUsed;
            ShortLeaveByAdmin_LeaveTime.Text = roomSet.AdminShortLeave.HoldTimeLength.ToString();
            //在座时长设置
            SeatTime.Checked = roomSet.SeatUsedTimeLimit.Used;
            SeatTime_Mode.SelectedValue = roomSet.SeatUsedTimeLimit.Mode;
            SeatTime_SeatTime.Text = roomSet.SeatUsedTimeLimit.UsedTimeLength.ToString();
            SeatTime_OverTime_Mode.SelectedValue = ((int)roomSet.SeatUsedTimeLimit.OverTimeHandle).ToString();
            SeatTime_ContinueTime.Checked = roomSet.SeatUsedTimeLimit.IsCanContinuedTime;
            SeatTime_ContinueTime_Time.Text = roomSet.SeatUsedTimeLimit.DelayTimeLength.ToString();
            SeatTime_ContinueTime_ContinueCount.Text = roomSet.SeatUsedTimeLimit.ContinuedTimes.ToString();
            SeatTime_ContinueTime_BeforeTime.Text = roomSet.SeatUsedTimeLimit.CanDelayTime.ToString();
            for (int i = 0; i < roomSet.SeatUsedTimeLimit.FixedTimes.Count; i++)
            {
                SeatTime_TimeSpanList.Text += roomSet.SeatUsedTimeLimit.FixedTimes[i].ToShortTimeString() + ";";
                //TextBox timeH = FindControl("PanelSetting").FindControl("SeatTimeSetting").FindControl("SeatTime_TimeH_" + i) as TextBox;
                //timeH.Text = roomSet.SeatUsedTimeLimit.FixedTimes[i].ToShortTimeString().Split(':')[0];
                //TextBox timeM = FindControl("PanelSetting").FindControl("SeatTimeSetting").FindControl("SeatTime_TimeM_" + i) as TextBox;
                //timeM.Text = roomSet.SeatUsedTimeLimit.FixedTimes[i].ToShortTimeString().Split(':')[1];
            }
            //验证授权
            //if (authorize != null && !authorize.SystemFunction.Contains("LimitTime_SpanMode"))
            //{
            //    seatTimeModeltr.Style["display"] = "none";
            //    timespanlisttr.Style["display"] = "none";
            //}
            //预约功能设置
            SeatBook.Checked = roomSet.SeatBespeak.Used;
            ckbDelayTime.Checked = roomSet.SeatBespeak.AllowDelayTime;
            ckbLeave.Checked = roomSet.SeatBespeak.AllowLeave;
            ckbShortLeave.Checked = roomSet.SeatBespeak.AllowShortLeave;
            SeatBook_BeforeBookDay.Text = roomSet.SeatBespeak.BespeakBeforeDays.ToString();
            string[] beginbooktime = roomSet.SeatBespeak.CanBespeatTimeSpace.BeginTime.Split(':');
            string[] endbooktime = roomSet.SeatBespeak.CanBespeatTimeSpace.EndTime.Split(':');
            SeatBook_BookTime_StartH.Text = beginbooktime[0];
            SeatBook_BookTime_StartM.Text = beginbooktime[1];
            SeatBook_BookTime_EndH.Text = endbooktime[0];
            SeatBook_BookTime_EndM.Text = endbooktime[1];
            SeatBook_SubmitBeforeTime.Text = roomSet.SeatBespeak.ConfirmTime.BeginTime;
            SeatBook_SubmitLateTime.Text = roomSet.SeatBespeak.ConfirmTime.EndTime;
            if (roomSet.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
            {
                SeatBook_SeatBookRadioPercent.Checked = true;
            }
            else if (roomSet.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.AppointSeat)
            {
                SeatBook_SeatBookRadioSetted.Checked = true;
            }
            SeatBook_SeatBookRadioPercent_Percent.Text = ((roomSet.SeatBespeak.BespeakArea.Scale) * 100).ToString();
            foreach (SeatManage.ClassModel.TimeSpace cannotbookdate in roomSet.SeatBespeak.NoBespeakDates)
            {
                if (!string.IsNullOrEmpty(SeatBook_CanNotSeatBookDate.Text))
                {
                    SeatBook_CanNotSeatBookDate.Text += ";";
                }
                SeatBook_CanNotSeatBookDate.Text += cannotbookdate.BeginTime + "~" + cannotbookdate.EndTime;
            }
            cbNowDayBook.Checked = roomSet.SeatBespeak.NowDayBespeak;
            NowDayBookTime.Text = roomSet.SeatBespeak.SeatKeepTime.ToString();
            cbSpecifiedBook.Checked = roomSet.SeatBespeak.SpecifiedBespeak;
            SeatBook_SelectBespeakSeat.Checked = roomSet.SeatBespeak.SelectBespeakSeat;
            SeatBook_SpecifiedTime.Checked = roomSet.SeatBespeak.SpecifiedTime;
            SeatBook_BespeakSeatOnSeat.Checked = roomSet.SeatBespeak.BespeatWithOnSeat;
            SeatBook_BespeakSeatCount.Text = roomSet.SeatBespeak.BespeakSeatCount.ToString();
            foreach (DateTime dt in roomSet.SeatBespeak.SpecifiedTimeList)
            {
                if (SeatBook_SpecifiedTimeList.Text != "")
                {
                    SeatBook_SpecifiedTimeList.Text += ";";
                }
                SeatBook_SpecifiedTimeList.Text += dt.ToShortTimeString();
            }
            //if (authorize != null && !authorize.SystemFunction.Contains("Bespeak_AppointTime"))
            //{
            //    cbSpecifiedBook.Visible = false;
            //    appointTimetr.Style["display"] = "none";
            //}
            //if (authorize != null && !authorize.SystemFunction.Contains("Bespeak_NowDay"))
            //{
            //    cbNowDayBook.Visible = false;
            //    nowDaytr1.Style["display"] = "none";
            //    nowDaytr2.Style["display"] = "none";
            //    nowDaytr3.Style["display"] = "none";
            //}

            //黑名单设置
            UseBlacklist.Checked = roomSet.UsedBlacklistLimit;
            IsRecordViolate.Checked = roomSet.IsRecordViolate;
            UseBlacklistSetting.Checked = roomSet.BlackListSetting.Used;
            //if (UseBlacklistSetting.Checked)
            //{
            //    UseBlacklistSettingTable.Style["display"] = "block";
            //}
            //else
            //{
            //    UseBlacklistSettingTable.Style["display"] = "none";
            //}
            RecordViolateCount.Text = roomSet.BlackListSetting.ViolateTimes.ToString();
            LeaveBlackDays.Text = roomSet.BlackListSetting.LimitDays.ToString();
            LeaveRecordViolateDays.Text = roomSet.BlackListSetting.ViolateFailDays.ToString();
            if (roomSet.BlackListSetting.LeaveBlacklist == LeaveBlacklistMode.AutomaticMode)
            {
                AutoLeave.Checked = true;
            }
            else
            {
                HardLeave.Checked = true;
            }
            RecordViolate_BookOverTime.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.BookingTimeOut];
            RecordViolate_LeaveByAdmin.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.LeaveByAdmin];
            RecordViolate_SeatOverTime.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.SeatOutTime];
            RecordViolate_ShortLeaveByAdmin.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByAdminOutTime];
            RecordViolate_ShortLeaveByReader.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByReaderOutTime];
            RecordViolate_ShortLeaveOverTime.Checked = roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveOutTime];
            //其他设置
            ShowSeatNumberCount.Text = roomSet.SeatNumAmount.ToString();
            NoManMode.Checked = roomSet.NoManagement.Used;
            NoManMode_WaitTime.Text = roomSet.NoManagement.OperatingInterval.ToString();

            ReaderLimit.Checked = roomSet.LimitReaderEnter.Used;
            if (roomSet.LimitReaderEnter.CanEnter)
            {
                ReaderLimit_LimitMode_Writelist.Checked = true;
            }
            else
            {
                ReaderLimit_LimitMode_Blacklist.Checked = true;
            }
            SeatManage.Bll.T_SM_Reader readerbll = new SeatManage.Bll.T_SM_Reader();
            List<string> readertype = readerbll.GetReaderType();
            ReaderLimit_ReaderMode.Items.Clear();
            foreach (string reader in readertype)
            {
                if (string.IsNullOrEmpty(reader))
                {
                    ReaderLimit_ReaderMode.Items.Add("未指定");
                }
                else
                {
                    ReaderLimit_ReaderMode.Items.Add(reader);
                }
            }
            string[] readerType = roomSet.LimitReaderEnter.ReaderTypes.Split(';');
            foreach (ListItem ci in ReaderLimit_ReaderMode.Items)
            {
                foreach (string reader in readerType)
                {
                    if (string.IsNullOrEmpty(reader) && ci.Value == "未指定")
                    {
                        ci.Selected = true;
                        break;
                    }
                    else if (ci.Value == reader)
                    {
                        ci.Selected = true;
                        break;
                    }
                }
            }
            SameRoomSet.Items.Clear();
            List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            foreach (SeatManage.ClassModel.ReadingRoomInfo roominfo in rooms)
            {
                if (roominfo.No != room.No)
                {
                    ListItem li = new ListItem(roominfo.Name + "&nbsp;&nbsp;", roominfo.No);
                    SameRoomSet.Items.Add(li);
                }
            }
            for (int i = 0; i < SameRoomSet.Items.Count; i++)
            {
                SameRoomSet.Items[i].Attributes.Add("onmouseover", "showToolTip(event,'" + SameRoomSet.Items[i].Value + "')");
            }
        }

        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {

        }

        //void UseBlacklistSetting_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (UseBlacklistSetting.Checked)
        //    {
        //        UseBlacklist.Enabled = false;
        //        UseBlacklist.Checked = true;
        //        IsRecordViolate.Enabled = false;
        //        IsRecordViolate.Checked = true;
        //    }
        //    else
        //    {
        //        IsRecordViolate.Enabled = true;
        //        UseBlacklist.Enabled = true;
        //    }
        //}
        /// <summary>&
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_OnClick(object sender, EventArgs e)
        {
            SeatManage.ClassModel.ReadingRoomInfo room = SeatManage.Bll.T_SM_ReadingRoom.GetSingleRoomInfo(Request.QueryString["id"]);
            if (room == null)
            {
                FineUI.Alert.Show("阅览室设置保存失败！");
                return;
            }
            SeatManage.ClassModel.ReadingRoomSetting roomSet = room.Setting;
            if (roomSet == null)
            {
                roomSet = new SeatManage.ClassModel.ReadingRoomSetting();
            }
            //选座设置
            roomSet.SeatChooseMethod.DefaultChooseMethod = (SeatManage.EnumType.SelectSeatMode)int.Parse(SeatSelectDefaultMode.SelectedValue);
            roomSet.SeatChooseMethod.UsedAdvancedSet = SeatSelectAdMode.Checked;
            roomSet.PosTimes.Minutes = int.Parse(SelectSeatPosTimes.Text);
            roomSet.PosTimes.Times = int.Parse(SelectSeatPosCount.Text);
            roomSet.PosTimes.IsUsed = SeatSelectPos.Checked;
            //高级设置
            roomSet.SeatChooseMethod.AdvancedSelectSeatMode.Clear();
            for (int dayNum = 0; dayNum < 7; dayNum++)
            {
                SeatChooseMethodPlan scmp = new SeatChooseMethodPlan();
                CheckBox DayCheck = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum) as CheckBox;
                scmp.Used = DayCheck.Checked;
                scmp.PlanOption.Clear();
                for (int i = 0; i < 3; i++)
                {
                    TextBox begintimeH = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_StartH") as TextBox;
                    TextBox begintimeM = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_StartM") as TextBox;
                    TextBox endtimeH = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_EndH") as TextBox;
                    TextBox endtimeM = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_EndM") as TextBox;
                    RadioButtonList selectmode = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("SeatSelectAdModeDay" + dayNum + "_Time" + (i + 1) + "_SelectMode") as RadioButtonList;
                    if (!string.IsNullOrEmpty(begintimeH.Text) || !string.IsNullOrEmpty(begintimeM.Text) || !string.IsNullOrEmpty(endtimeH.Text) || !string.IsNullOrEmpty(endtimeM.Text))
                    {
                        DateTime begintime = new DateTime();
                        DateTime endtime = new DateTime();
                        if (!DateTime.TryParse(begintimeH.Text + ":" + begintimeM.Text, out begintime))
                        {
                            FineUI.Alert.Show("选座设置高级设置，时间设置错误！");
                            return;
                        }
                        if (!DateTime.TryParse(endtimeH.Text + ":" + endtimeM.Text, out endtime))
                        {
                            FineUI.Alert.Show("选座设置高级设置，时间设置错误！");
                            return;
                        }
                        SeatChooseMethodOption scmo = new SeatChooseMethodOption();
                        scmo.ChooseMethod = (SelectSeatMode)int.Parse(selectmode.SelectedValue);
                        scmo.UsedTime.BeginTime = begintime.ToShortTimeString();
                        scmo.UsedTime.EndTime = endtime.ToShortTimeString();
                        scmp.PlanOption.Add(scmo);
                    }
                }
                roomSet.SeatChooseMethod.AdvancedSelectSeatMode.Add((DayOfWeek)dayNum, scmp);
            }
            //暂离设置
            roomSet.SeatHoldTime.DefaultHoldTimeLength = int.Parse(ShortLeaveDufaultTime.Text);
            roomSet.SeatHoldTime.UsedAdvancedSet = ShortLeaveAdMode.Checked;
            //高级设置
            roomSet.SeatHoldTime.AdvancedSeatHoldTime.Clear();
            for (int i = 0; i < 2; i++)
            {
                TextBox begintimeH = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_StartH") as TextBox;
                TextBox begintimeM = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_StartM") as TextBox;
                TextBox endtimeH = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_EndH") as TextBox;
                TextBox endtimeM = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_EndM") as TextBox;
                TextBox leavetime = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1) + "_LeaveTime") as TextBox;
                CheckBox used = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("ShortLeaveAdMode_Time" + (i + 1)) as CheckBox;
                DateTime begintime = new DateTime();
                DateTime endtime = new DateTime();
                if (!string.IsNullOrEmpty(begintimeH.Text) || !string.IsNullOrEmpty(begintimeM.Text) || !string.IsNullOrEmpty(endtimeH.Text) || !string.IsNullOrEmpty(endtimeM.Text) || !string.IsNullOrEmpty(leavetime.Text))
                {
                    if (!DateTime.TryParse(begintimeH.Text + ":" + begintimeM.Text, out begintime))
                    {
                        FineUI.Alert.Show("暂离设置高级设置，时间设置错误！");
                        return;
                    }
                    if (!DateTime.TryParse(endtimeH.Text + ":" + endtimeM.Text, out endtime))
                    {
                        FineUI.Alert.Show("暂离设置高级设置，时间设置错误！");
                        return;
                    }
                    SeatHoldTimeOption shto = new SeatHoldTimeOption();
                    shto.HoldTimeLength = int.Parse(leavetime.Text);
                    shto.Used = used.Checked;
                    shto.UsedTime.BeginTime = begintime.ToShortTimeString();
                    shto.UsedTime.EndTime = endtime.ToShortTimeString();
                    roomSet.SeatHoldTime.AdvancedSeatHoldTime.Add(shto);
                }
            }
            roomSet.AdminShortLeave.IsUsed = ShortLeaveByAdmin.Checked;
            roomSet.AdminShortLeave.HoldTimeLength = int.Parse(ShortLeaveByAdmin_LeaveTime.Text);
            //开闭馆计划设置
            DateTime opentime = new DateTime();
            DateTime closetime = new DateTime();
            if (!DateTime.TryParse(ReadingRoomDufaultOpenTimeH.Text + ":" + ReadingRoomDufaultOpenTimeM.Text, out opentime))
            {
                FineUI.Alert.Show("开闭馆计划设置，时间设置错误！");
                return;
            }
            if (!DateTime.TryParse(ReadingRoomDufaultCloseTimeH.Text + ":" + ReadingRoomDufaultCloseTimeM.Text, out closetime))
            {
                FineUI.Alert.Show("开闭馆计划设置，时间设置错误！");
                return;
            }
            roomSet.RoomOpenSet.DefaultOpenTime.BeginTime = opentime.ToShortTimeString();
            roomSet.RoomOpenSet.DefaultOpenTime.EndTime = closetime.ToShortTimeString();
            roomSet.RoomOpenSet.OpenBeforeTimeLength = int.Parse(ReadingRoomBeforeOpenTime.Text);
            roomSet.RoomOpenSet.CloseBeforeTimeLength = int.Parse(ReadingRoomBeforeCloseTime.Text);
            roomSet.RoomOpenSet.UninterruptibleModel = ReadingRoomOpen24H.Checked;
         
            //高级设置
            roomSet.RoomOpenSet.UsedAdvancedSet = ReadingRoomOpenCloseAdMode.Checked;
            //foreach (KeyValuePair<DayOfWeek, RoomOpenPlanSet> day in roomSet.RoomOpenSet.RoomOpenPlan)
            //{
            roomSet.RoomOpenSet.RoomOpenPlan.Clear();
            for (int dayNum = 0; dayNum < 7; dayNum++)
            {
                RoomOpenPlanSet rops = new RoomOpenPlanSet();
                CheckBox DayCheck = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum) as CheckBox;
                rops.Used = DayCheck.Checked;
                rops.OpenTime.Clear();
                for (int i = 0; i < 3; i++)
                {
                    TextBox begintimeH = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_OpenH") as TextBox;
                    TextBox begintimeM = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_OpenM") as TextBox;
                    TextBox endtimeH = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_CloseH") as TextBox;
                    TextBox endtimeM = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1) + "_CloseM") as TextBox;
                    CheckBox used = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("ReadingRoomAdOpenTime_Day" + dayNum + "_Time" + (i + 1)) as CheckBox;
                    if (!string.IsNullOrEmpty(begintimeH.Text) || !string.IsNullOrEmpty(begintimeM.Text) || !string.IsNullOrEmpty(endtimeH.Text) || !string.IsNullOrEmpty(endtimeM.Text))
                    {
                        DateTime begintime = new DateTime();
                        DateTime endtime = new DateTime();
                        if (!DateTime.TryParse(begintimeH.Text + ":" + begintimeM.Text, out begintime))
                        {
                            FineUI.Alert.Show("开闭馆计划高级设置，时间设置错误！");
                            return;
                        }
                        if (!DateTime.TryParse(endtimeH.Text + ":" + endtimeM.Text, out endtime))
                        {
                            FineUI.Alert.Show("开闭馆计划高级设置，时间设置错误！");
                            return;
                        }
                        TimeSpace ts = new TimeSpace();
                        ts.BeginTime = begintime.ToShortTimeString();
                        ts.EndTime = endtime.ToShortTimeString();
                        rops.OpenTime.Add(ts);
                    }
                }
                roomSet.RoomOpenSet.RoomOpenPlan.Add((DayOfWeek)dayNum, rops);
            }
            //在座时长设置
            roomSet.SeatUsedTimeLimit.Used = SeatTime.Checked;
            roomSet.SeatUsedTimeLimit.Mode = SeatTime_Mode.SelectedValue;
            roomSet.SeatUsedTimeLimit.UsedTimeLength = int.Parse(SeatTime_SeatTime.Text);
            roomSet.SeatUsedTimeLimit.OverTimeHandle = (EnterOutLogType)int.Parse(SeatTime_OverTime_Mode.SelectedValue);
            roomSet.SeatUsedTimeLimit.IsCanContinuedTime = SeatTime_ContinueTime.Checked;
            roomSet.SeatUsedTimeLimit.DelayTimeLength = int.Parse(SeatTime_ContinueTime_Time.Text);
            roomSet.SeatUsedTimeLimit.ContinuedTimes = int.Parse(SeatTime_ContinueTime_ContinueCount.Text);
            roomSet.SeatUsedTimeLimit.CanDelayTime = int.Parse(SeatTime_ContinueTime_BeforeTime.Text);
            roomSet.SeatUsedTimeLimit.FixedTimes.Clear();
            string[] timeSpanList = SeatTime_TimeSpanList.Text.Split(';');
            for (int i = 0; i < timeSpanList.Length; i++)
            {
                //TextBox timeH = FindControl("PanelSetting").FindControl("SeatTimeSetting").FindControl("SeatTime_TimeH_" + i) as TextBox;
                //TextBox timeM = FindControl("PanelSetting").FindControl("SeatTimeSetting").FindControl("SeatTime_TimeM_" + i) as TextBox;
                if (timeSpanList[i] != "")
                {
                    DateTime dt = new DateTime();
                    if (!DateTime.TryParse(timeSpanList[i], out dt))
                    {
                        FineUI.Alert.Show("在座时长设置，时间设置错误！");
                        return;
                    }
                    roomSet.SeatUsedTimeLimit.FixedTimes.Add(dt);
                }
            }
            if (roomSet.SeatUsedTimeLimit.Mode != "Free" && roomSet.SeatUsedTimeLimit.Used && roomSet.SeatUsedTimeLimit.FixedTimes.Count == 0)
            {
                FineUI.Alert.Show("在座时长设置，请设置限制时间！");
                return;
            }

            if (roomSet.RoomOpenSet.UninterruptibleModel)
            {
                if (roomSet.SeatUsedTimeLimit.Mode == "Fixed")
                {
                    FineUI.Alert.Show("24小时不间断模式无法兼容在座限时[指定固定时间]模式！请选择[计算在座时长]模式");
                    return;
                }
            }

            //预约功能设置
            roomSet.SeatBespeak.Used = SeatBook.Checked;
            roomSet.SeatBespeak.AllowDelayTime = ckbDelayTime.Checked;
            roomSet.SeatBespeak.AllowLeave = ckbLeave.Checked;
            roomSet.SeatBespeak.AllowShortLeave = ckbShortLeave.Checked;
            roomSet.SeatBespeak.NowDayBespeak = cbNowDayBook.Checked;
            roomSet.SeatBespeak.SeatKeepTime = int.Parse(NowDayBookTime.Text);
            roomSet.SeatBespeak.BespeakBeforeDays = int.Parse(SeatBook_BeforeBookDay.Text);
            DateTime beginbooktime = new DateTime();
            DateTime endbooktime = new DateTime();
            if (!DateTime.TryParse(SeatBook_BookTime_StartH.Text + ":" + SeatBook_BookTime_StartM.Text, out beginbooktime))
            {
                FineUI.Alert.Show("预约设置，时间设置错误！");
                return;
            }
            if (!DateTime.TryParse(SeatBook_BookTime_EndH.Text + ":" + SeatBook_BookTime_EndM.Text, out endbooktime))
            {
                FineUI.Alert.Show("预约设置，时间设置错误！");
                return;
            }
            roomSet.SeatBespeak.CanBespeatTimeSpace.BeginTime = beginbooktime.ToShortTimeString();
            roomSet.SeatBespeak.CanBespeatTimeSpace.EndTime = endbooktime.ToShortTimeString();
            roomSet.SeatBespeak.ConfirmTime.BeginTime = SeatBook_SubmitBeforeTime.Text;
            roomSet.SeatBespeak.ConfirmTime.EndTime = SeatBook_SubmitLateTime.Text;

            roomSet.SeatBespeak.SpecifiedBespeak = cbSpecifiedBook.Checked;
            roomSet.SeatBespeak.SelectBespeakSeat = SeatBook_SelectBespeakSeat.Checked;
            roomSet.SeatBespeak.SpecifiedTime = SeatBook_SpecifiedTime.Checked;
            roomSet.SeatBespeak.BespeatWithOnSeat = SeatBook_BespeakSeatOnSeat.Checked;
            roomSet.SeatBespeak.BespeakSeatCount = int.Parse(SeatBook_BespeakSeatCount.Text);
            roomSet.SeatBespeak.SpecifiedTimeList.Clear();
            if (SeatBook_SpecifiedTimeList.Text != "")
            {
                string[] booktimes = SeatBook_SpecifiedTimeList.Text.Split(';');
                foreach (string dt in booktimes)
                {
                    DateTime t = new DateTime();
                    if (DateTime.TryParse(dt, out t))
                    {
                        if (roomSet.SeatBespeak.SpecifiedTimeList.Count > 0 && t <= roomSet.SeatBespeak.SpecifiedTimeList[roomSet.SeatBespeak.SpecifiedTimeList.Count - 1])
                        {
                            FineUI.Alert.Show("预约设置，指定时段设置错误！");
                            return;
                        }
                        roomSet.SeatBespeak.SpecifiedTimeList.Add(t);
                    }
                }
            }
            if (roomSet.SeatBespeak.SpecifiedTimeList.Count < 1 && roomSet.SeatBespeak.SpecifiedTime)
            {
                FineUI.Alert.Show("预约设置，请设置指定的预约时间！");
                return;
            }


            if (SeatBook_SeatBookRadioPercent.Checked)
            {
                roomSet.SeatBespeak.BespeakArea.BespeakType = BespeakAreaType.Percentage;
            }
            else if (SeatBook_SeatBookRadioSetted.Checked)
            {
                roomSet.SeatBespeak.BespeakArea.BespeakType = BespeakAreaType.AppointSeat;
            }
            roomSet.SeatBespeak.BespeakArea.Scale = double.Parse(SeatBook_SeatBookRadioPercent_Percent.Text) / 100;
            roomSet.SeatBespeak.NoBespeakDates.Clear();
            if (!string.IsNullOrEmpty(SeatBook_CanNotSeatBookDate.Text))
            {
                string[] cannotbookdate = SeatBook_CanNotSeatBookDate.Text.Split(';');
                foreach (string date in cannotbookdate)
                {
                    string[] datespan = date.Split('~');
                    DateTime begindate = new DateTime();
                    DateTime enddate = new DateTime();
                    if (datespan.Length > 1)
                    {
                        if (!DateTime.TryParse(datespan[0], out begindate))
                        {
                            FineUI.Alert.Show("预约设置，不可预约时间设置错误！");
                            return;
                        }
                        if (!DateTime.TryParse(datespan[1], out enddate))
                        {
                            FineUI.Alert.Show("预约设置，不可预约时间设置错误！");
                            return;
                        }
                        TimeSpace ts = new TimeSpace();
                        ts.BeginTime = begindate.Month.ToString() + "-" + begindate.Day.ToString();
                        ts.EndTime = enddate.Month.ToString() + "-" + enddate.Day.ToString();
                        roomSet.SeatBespeak.NoBespeakDates.Add(ts);
                    }
                    else
                    {
                        FineUI.Alert.Show("预约设置，不可预约时间设置错误！");
                        return;
                    }
                }
            }
            //黑名单设置
            roomSet.UsedBlacklistLimit = UseBlacklist.Checked;
            roomSet.IsRecordViolate = IsRecordViolate.Checked;
            roomSet.BlackListSetting.Used = UseBlacklistSetting.Checked;
            if (roomSet.BlackListSetting.Used)
            {
                roomSet.UsedBlacklistLimit = true;
                roomSet.IsRecordViolate = true;
            }
            roomSet.BlackListSetting.ViolateTimes = int.Parse(RecordViolateCount.Text);
            roomSet.BlackListSetting.LimitDays = int.Parse(LeaveBlackDays.Text);
            roomSet.BlackListSetting.ViolateFailDays = int.Parse(LeaveRecordViolateDays.Text);
            if (AutoLeave.Checked)
            {
                roomSet.BlackListSetting.LeaveBlacklist = LeaveBlacklistMode.AutomaticMode;
            }
            else
            {
                roomSet.BlackListSetting.LeaveBlacklist = LeaveBlacklistMode.ManuallyMode;
            }
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.BookingTimeOut] = RecordViolate_BookOverTime.Checked;
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.LeaveByAdmin] = RecordViolate_LeaveByAdmin.Checked;
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.SeatOutTime] = RecordViolate_SeatOverTime.Checked;
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByAdminOutTime] = RecordViolate_ShortLeaveByAdmin.Checked;
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveByReaderOutTime] = RecordViolate_ShortLeaveByReader.Checked;
            roomSet.BlackListSetting.ViolateRoule[ViolationRecordsType.ShortLeaveOutTime] = RecordViolate_ShortLeaveOverTime.Checked;
            //其他设置
            roomSet.SeatNumAmount = int.Parse(ShowSeatNumberCount.Text);
            roomSet.NoManagement.Used = NoManMode.Checked;
            roomSet.NoManagement.OperatingInterval = double.Parse(NoManMode_WaitTime.Text);
            roomSet.LimitReaderEnter.Used = ReaderLimit.Checked;
            if (ReaderLimit_LimitMode_Writelist.Checked)
            {
                roomSet.LimitReaderEnter.CanEnter = true;
            }
            else
            {
                roomSet.LimitReaderEnter.CanEnter = false;
            }
            roomSet.LimitReaderEnter.ReaderTypes = "";
            foreach (ListItem type in ReaderLimit_ReaderMode.Items)
            {
                if (type.Selected)
                {
                    if (!string.IsNullOrEmpty(roomSet.LimitReaderEnter.ReaderTypes))
                    {
                        roomSet.LimitReaderEnter.ReaderTypes += ";";
                    }
                    roomSet.LimitReaderEnter.ReaderTypes += type.Value;
                }
            }
            roomSet.LimitReaderEnter.ReaderTypes = roomSet.LimitReaderEnter.ReaderTypes.Replace("未指定", "");
            room.Setting = roomSet;
            if (SeatManage.Bll.T_SM_ReadingRoom.UpdateReadingRoom(room))
            {
                List<SeatManage.ClassModel.ReadingRoomInfo> rooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
                foreach (ListItem li in SameRoomSet.Items)
                {
                    if (li.Selected)
                    {
                        foreach (SeatManage.ClassModel.ReadingRoomInfo roominfo in rooms)
                        {
                            if (roominfo.No == li.Value)
                            {
                                roominfo.Setting = roomSet;
                                if (!SeatManage.Bll.T_SM_ReadingRoom.UpdateReadingRoom(roominfo))
                                {
                                    FineUI.Alert.Show("保存失败！");
                                    return;
                                }
                            }
                        }
                    }
                }
                FineUI.PageContext.RegisterStartupScript(FineUI.ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("保存成功！");
            }
            else
            {
                FineUI.Alert.Show("保存失败！");
            }
        }
        /// <summary>
        /// 隐藏高级选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SeatSelectAdMode_OnCheckedChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 4; j++)
            //    {
            //        FineUI.FormRow frp = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("Day" + i + "_panel") as FineUI.FormRow;
            //        FineUI.FormRow fr = FindControl("PanelSetting").FindControl("FormReadingRoomSet").FindControl("Day" + i + "_Time" + (j + 1)) as FineUI.FormRow;
            //        if (SeatSelectAdMode.Checked)
            //        {
            //            foreach (FineUI.ControlBase c in frp.Items)
            //            {
            //                c.Hidden = false;
            //            }
            //            foreach (FineUI.ControlBase c in fr.Items)
            //            {
            //                c.Hidden = false;
            //            }
            //        }
            //        else
            //        {
            //            foreach (FineUI.ControlBase c in fr.Items)
            //            {
            //                c.Hidden = true;
            //            }
            //            foreach (FineUI.ControlBase c in frp.Items)
            //            {
            //                c.Hidden = true;
            //            }
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 暂离高级设置隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShortLeaveAdMode_OnCheckedChanged(object sender, EventArgs e)
        {
            //for (int j = 1; j < 3; j++)
            //{
            //    FineUI.FormRow fr = FindControl("PanelSetting").FindControl("FormShortLeave").FindControl("SLTime" + j) as FineUI.FormRow;
            //    if (ShortLeaveAdMode.Checked)
            //    {
            //        foreach (FineUI.ControlBase c in SL_panel.Items)
            //        {
            //            c.Hidden = false;
            //        }
            //        foreach (FineUI.ControlBase c in fr.Items)
            //        {
            //            c.Hidden = false;
            //        }
            //    }
            //    else
            //    {
            //        foreach (FineUI.ControlBase c in SL_panel.Items)
            //        {
            //            c.Hidden = true;
            //        }
            //        foreach (FineUI.ControlBase c in fr.Items)
            //        {
            //            c.Hidden = true;
            //        }
            //    }
            //}
        }
        /// <summary>
        /// 开闭馆高级设置隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ReadingRoomOpenCloseAdMode_OnCheckedChanged(object sender, EventArgs e)
        {
            //for (int i = 0; i < 7; i++)
            //{
            //    for (int j = 0; j < 3; j++)
            //    {
            //        FineUI.FormRow frp = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("OCDay" + i + "_panel") as FineUI.FormRow;
            //        FineUI.FormRow fr = FindControl("PanelSetting").FindControl("FormReadingRoomOC").FindControl("OCDay" + i + "_Time" + (j + 1)) as FineUI.FormRow;
            //        if (ReadingRoomOpenCloseAdMode.Checked)
            //        {
            //            foreach (FineUI.ControlBase c in frp.Items)
            //            {
            //                c.Hidden = false;
            //            }
            //            foreach (FineUI.ControlBase c in fr.Items)
            //            {
            //                c.Hidden = false;
            //            }
            //        }
            //        else
            //        {
            //            foreach (FineUI.ControlBase c in frp.Items)
            //            {
            //                c.Hidden = true;
            //            }
            //            foreach (FineUI.ControlBase c in fr.Items)
            //            {
            //                c.Hidden = true;
            //            }
            //        }
            //    }
            //}
        }
        ///// <summary>
        ///// 编辑窗口关闭
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        //{

        //}
    }
}