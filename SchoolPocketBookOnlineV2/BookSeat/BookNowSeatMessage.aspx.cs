using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.BookSeat
{
    public partial class BookNowSeatMessage : BasePage
    {
        SeatManage.IPocketBespeak.IBespeakSeatListForm handler = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();
        SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        string seatNo = "";
        string seatShortNo = "";
        string date = DateTime.Now.ToString();
        string roomNo = "";
        string timeSpan = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            seatNo = Request.QueryString["seatNo"];
            seatShortNo = Request.QueryString["seatShortNo"];
            roomNo = Request.QueryString["roomNo"];
            timeSpan = Request.QueryString["timeSpan"];
            if (!IsPostBack)
            {
                BindUIElement(seatNo, seatShortNo, DateTime.Parse(date), timeSpan);
            }
            string cmd = Request.Form["subCmd"];
            switch (cmd)
            {
                case "select":
                    BindUIElement(seatNo, seatShortNo, DateTime.Parse(date), timeSpan);
                    break;
                case "query":
                    SeatManage.ClassModel.BespeakLogInfo bespeakModel = new SeatManage.ClassModel.BespeakLogInfo();
                    bespeakModel.BsepeakState = SeatManage.EnumType.BookingStatus.Waiting;
                    DateTime bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), lblBookTime.InnerText));
                    bespeakModel.BsepeakTime = bespeatDate;
                    bespeakModel.CardNo = this.LoginUserInfo.CardNo;
                    bespeakModel.ReadingRoomNo = roomNo;
                    bespeakModel.Remark = string.Format("读者通过手机预约网站预约座位");
                    bespeakModel.SeatNo = seatNo;
                    bespeakModel.SubmitTime = bookMode.SelectedIndex == 0 ? bespeakModel.BsepeakTime : bespeakModel.SubmitTime = DateTime.Now;
                    try
                    {
                        string resultValue = handler.SubmitNowDayBespeakInfo(this.UserSchoolInfo, bespeakModel);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = resultValue;
                        }
                        else
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = "未知错误";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                        return;
                    }
                    break;
            }
        }

        string bespeakSureTimeSpan(SeatManage.ClassModel.ReadingRoomSetting set)
        {
            DateTime bespeakTime = DateTime.Now;
            if (bookMode.SelectedIndex == 1)
            {
                if (this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime)
                {
                    bespeakTime = Convert.ToDateTime(spanSelect.Items[spanSelect.SelectedIndex].Value);
                }
                else
                {
                    bespeakTime = Convert.ToDateTime(timeSelect.Items[timeSelect.SelectedIndex].Value);
                }
                DateTime bespeakBeginTime = bespeakTime.AddMinutes(-double.Parse(set.SeatBespeak.ConfirmTime.BeginTime));
                DateTime bespeakEndTime = bespeakTime.AddMinutes(double.Parse(set.SeatBespeak.ConfirmTime.EndTime));
                return string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
            }
            else
            {
                DateTime bespeakBeginTime = bespeakTime;
                DateTime bespeakEndTime = bespeakTime.AddMinutes(set.SeatBespeak.SeatKeepTime);
                return string.Format("{0}至{1}", bespeakBeginTime.ToShortTimeString(), bespeakEndTime.ToShortTimeString());
            }


        }

        string bespeakSureTime(SeatManage.ClassModel.ReadingRoomSetting set)
        {
            DateTime bespeakTime = DateTime.Now;
            if (bookMode.SelectedIndex == 1)
            {
                if (this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime)
                {
                    bespeakTime = Convert.ToDateTime(spanSelect.Items[spanSelect.SelectedIndex].Value);
                }
                else
                {
                    bespeakTime = Convert.ToDateTime(timeSelect.Items[timeSelect.SelectedIndex].Value);
                }
            }
            return string.Format(bespeakTime.ToShortTimeString());

        }

        void BindUIElement(string seatNo, string seatShortNo, DateTime date, string timeSP)
        {
            if (!IsPostBack)
            {
                SeatManage.Bll.T_SM_ReadingRoom bllReadingRoom = new SeatManage.Bll.T_SM_ReadingRoom();
                bookMode.Items.Add(new ListItem("立即预约", "0"));
                if (this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime && this.ReadingRoomList[roomNo].Setting.SeatBespeak.CanBookMultiSpan)
                {
                    if (!string.IsNullOrEmpty(timeSP))
                    {
                        string[] sps = timeSP.Split(';');
                        foreach (string s in sps)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                if (DateTime.Parse(s) <= date)
                                {
                                    continue;
                                }
                                spanSelect.Items.Add(new ListItem(s, s));
                            }
                        }
                    }
                }
                else
                {
                    foreach (DateTime dt in this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTimeList)
                    {
                        if (dt <= date)
                        {
                            continue;
                        }
                        spanSelect.Items.Add(new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString()));
                    }
                }
                if (this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedBespeak && spanSelect.Items.Count > 0)
                {
                    bookMode.Items.Add(new ListItem("指定时段", "1"));
                }
                bookMode.SelectedIndex = 0;
                DateTime minTime = DateTime.Parse(date.ToShortDateString() + " " + bllReadingRoom.GetRoomOpenTimeByDate(this.ReadingRoomList[roomNo].Setting, date.ToShortDateString()).BeginTime);
                if (minTime < DateTime.Now)
                {
                    minTime = DateTime.Now.AddMinutes(10 - DateTime.Now.Minute % 10);
                }
                while (true)
                {
                    minTime = minTime.AddMinutes(10);
                    if (minTime.Date > date.Date)
                    {
                        break;
                    }
                    if (Code.NowReadingRoomState.ReadingRoomOpenState(this.ReadingRoomList[roomNo].Setting.RoomOpenSet, minTime) == SeatManage.EnumType.ReadingRoomStatus.Close)
                    {
                        continue;
                    }
                    timeSelect.Items.Add(new ListItem(minTime.ToShortTimeString(), minTime.ToShortTimeString()));
                }
            }

            timeSelect.Visible = false;
            timeSelect_sp.Visible = false;
            spanSelect.Visible = false;
            spanSelect_sp.Visible = false;

            if (bookMode.SelectedIndex == 1)
            {
                if (this.ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime)
                {
                    spanSelect.Visible = true;
                    spanSelect_sp.Visible = true;
                }
                else
                {
                    timeSelect.Visible = true;
                    timeSelect_sp.Visible = true;
                }
            }

            lblBookDate.InnerText = date.ToShortDateString();
            this.lblSeatNo.InnerText = seatShortNo;
            this.lblReadingRoomName.InnerText = this.ReadingRoomList[roomNo].Name;
            lblBookTime.InnerText = bespeakSureTime(this.ReadingRoomList[roomNo].Setting);
            lbbookspan.InnerText = bespeakSureTimeSpan(this.ReadingRoomList[roomNo].Setting);
            lblSeatNo_Booked.InnerText = seatShortNo;
            //判断自己是否已经预约座位
            //this.LoginUserInfo = handler1.GetReaderInfo(this.UserSchoolInfo,this.LoginUserInfo.CardNo); 
            //List<SeatManage.ClassModel.BespeakLogInfo> readerBespeaklist =this.LoginUserInfo.BespeakLog;
            //if (readerBespeaklist.Count > 0)
            //{
            //    page1.Style.Add("display", "none");
            //    page2.Style.Add("display", "none");
            //    page3.Style.Add("display", "block");
            //    MessageTip.InnerText = "您选择的日期已经预约了座位，请先取消原来的预约。";
            //    return;
            //}
        }


    }
}