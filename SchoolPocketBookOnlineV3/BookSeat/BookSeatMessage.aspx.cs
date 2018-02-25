using System;
using System.Web.UI.WebControls;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using NowReadingRoomState = SchoolPocketBookWeb.Code.NowReadingRoomState;

namespace SchoolPocketBookWeb.BookSeat
{
    public partial class BookSeatMessage : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IBespeakSeatListForm handler = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();
        //SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        string seatNo = "";
        string seatShortNo = "";
        string date = "";
        string roomNo = "";
        string timeSpan = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            seatNo = Request.QueryString["seatNo"];
            seatShortNo = Request.QueryString["seatShortNo"];
            date = Request.QueryString["date"];
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
                    BespeakLogInfo bespeakModel = new BespeakLogInfo();
                    bespeakModel.BsepeakState = BookingStatus.Waiting;
                    DateTime bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), lblBookTime.InnerText));
                    bespeakModel.BsepeakTime = bespeatDate;
                    bespeakModel.CardNo = LoginUserInfo.CardNo;
                    bespeakModel.ReadingRoomNo = roomNo;
                    bespeakModel.Remark = "读者通过手机预约网站预约座位";
                    bespeakModel.SeatNo = seatNo;
                    bespeakModel.SubmitTime = DateTime.Now;
                    try
                    {
                        string resultValue = handler.SubmitBespeakInfo( bespeakModel);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
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
                        }
                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                    }
                    break;
            }
        }

        string bespeakSureTimeSpan(ReadingRoomSetting set)
        {
            T_SM_ReadingRoom bllReadingRoom = new T_SM_ReadingRoom();
            DateTime bespeakTime = Convert.ToDateTime(bllReadingRoom.GetRoomOpenTimeByDate(set, date).BeginTime);
            if (spanSelect.Visible)
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

        string bespeakSureTime(ReadingRoomSetting set)
        {
            T_SM_ReadingRoom bllReadingRoom = new T_SM_ReadingRoom();
            DateTime bespeakTime = Convert.ToDateTime(bllReadingRoom.GetRoomOpenTimeByDate(set, date).BeginTime);
            if (spanSelect.Visible)
            {
                bespeakTime = Convert.ToDateTime(spanSelect.Items[spanSelect.SelectedIndex].Value);
            }
            else
            {
                bespeakTime = Convert.ToDateTime(timeSelect.Items[timeSelect.SelectedIndex].Value);
            }
            return string.Format(bespeakTime.ToShortTimeString());

        }

        void BindUIElement(string seatNo, string seatShortNo, DateTime date, string timeSP)
        {
            if (!IsPostBack)
            {
                T_SM_ReadingRoom bllReadingRoom = new T_SM_ReadingRoom();
                if (ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime && ReadingRoomList[roomNo].Setting.SeatBespeak.CanBookMultiSpan)
                {
                    if (!string.IsNullOrEmpty(timeSP))
                    {
                        string[] sps = timeSP.Split(';');
                        foreach (string s in sps)
                        {
                            if (!string.IsNullOrEmpty(s))
                            {
                                spanSelect.Items.Add(new ListItem(s, s));
                            }
                        }
                    }
                }
                else
                {
                    if (ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedBespeak && ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime)
                    {
                        foreach (DateTime dt in ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTimeList)
                        {
                            spanSelect.Items.Add(new ListItem(dt.ToShortTimeString(), dt.ToShortTimeString()));
                        }
                        DateTime bespeakTime = Convert.ToDateTime(bllReadingRoom.GetRoomOpenTimeByDate(ReadingRoomList[roomNo].Setting, date.ToShortDateString()).BeginTime);
                        spanSelect.Items.Add(new ListItem(bespeakTime.ToShortTimeString(), bespeakTime.ToShortTimeString()));
                    }
                    else
                    {
                        DateTime openTime = DateTime.Parse(date.ToShortDateString() + " " + bllReadingRoom.GetRoomOpenTimeByDate(ReadingRoomList[roomNo].Setting, date.ToShortDateString()).BeginTime);
                        spanSelect.Items.Add(new ListItem(openTime.ToShortTimeString(), openTime.ToShortTimeString()));
                    }
                }
                DateTime minTime = DateTime.Parse(date.ToShortDateString() + " " + bllReadingRoom.GetRoomOpenTimeByDate(ReadingRoomList[roomNo].Setting, date.ToShortDateString()).BeginTime);
                while (true)
                {
                    minTime = minTime.AddMinutes(10);
                    if (minTime.Date > date.Date)
                    {
                        break;
                    }
                    if (NowReadingRoomState.ReadingRoomOpenState(ReadingRoomList[roomNo].Setting.RoomOpenSet, minTime) == ReadingRoomStatus.Close)
                    {
                        continue;
                    }
                    timeSelect.Items.Add(new ListItem(minTime.ToShortTimeString(), minTime.ToShortTimeString()));
                }

                timeSelect.Visible = false;
                timeSelect_sp.Visible = false;
                spanSelect.Visible = false;
                spanSelect_sp.Visible = false;
            }


            if (!ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedTime && ReadingRoomList[roomNo].Setting.SeatBespeak.SpecifiedBespeak)
            {
                timeSelect.Visible = true; ;
                timeSelect_sp.Visible = true;

            }
            else
            {
                spanSelect.Visible = true;
                spanSelect_sp.Visible = true;
            }

            lblBookDate.InnerText = date.ToShortDateString();
            lblSeatNo.InnerText = seatShortNo;
            lblReadingRoomName.InnerText = ReadingRoomList[roomNo].Name;
            lblBookTime.InnerText = bespeakSureTime(ReadingRoomList[roomNo].Setting);
            lbbookspan.InnerText = bespeakSureTimeSpan(ReadingRoomList[roomNo].Setting);
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