using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;

namespace SchoolPocketBookOnlineV2.BookSeat
{
    public partial class BookNowSeatListForm : BasePage
    {
        SeatManage.IPocketBespeak.IBespeakSeatListForm handler = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            spanWarmInfo.InnerText = "";
            if (!IsPostBack)
            {
                BindSelReadingRoom();
            }
            else
            {
                string cmd = Request.Form["subCmd"];
                string readingRoomId = "";
                switch (cmd)
                {
                    case "query":
                        if (selReadingRoom.Items.Count < 1)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有可预约阅览室";
                            return;
                        }
                        readingRoomId = selReadingRoom.Items[selReadingRoom.SelectedIndex].Value;
                        if (string.IsNullOrEmpty(readingRoomId))
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "请选择阅览室";
                        }
                        BindBookSeat(readingRoomId);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "bind":
                        selReadingRoom.Items.Clear();
                        BindSelReadingRoom();
                        if (selReadingRoom.Items.Count > 0)
                        {
                            BindBookSeat(selReadingRoom.Items[selReadingRoom.SelectedIndex].Value);
                        }
                        break;
                }
            }


        }

        /// <summary>
        /// 绑定提供预约阅览室下拉列表
        /// </summary>
        private void BindSelReadingRoom()
        {
            try
            {
                List<SeatManage.ClassModel.ReadingRoomInfo> roomList = handler.GetCanBespeakNowDayRoomInfo(this.UserSchoolInfo);
                if (this.ReadingRoomList != null)
                {
                    this.ReadingRoomList.Clear();
                }
                else
                {
                    this.ReadingRoomList = new Dictionary<string, ReadingRoomInfo>();
                }
                foreach (ReadingRoomInfo room in roomList)
                {
                    this.ReadingRoomList.Add(room.No, room);
                    ListItem item = new ListItem() { Text = room.Name, Value = room.No };
                    selReadingRoom.Items.Add(item);
                }
                if (selReadingRoom.Items.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的日期没有提供预约的阅览室，请重新选择日期！";
                    DataListBookSeat.DataSource = null;
                    DataListBookSeat.DataBind();
                }
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;
            }
        }



        /// <summary>
        /// 绑定预约座位列表
        /// </summary>
        /// <param name="readingRoomNo">阅览室编号</param>
        /// <param name="conn"></param>
        private void BindBookSeat(string readingRoomId)
        {
            try
            {
                if (selReadingRoom.SelectedIndex == -1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "请选择阅览室";
                    return;
                }
                string readingRoomNo = selReadingRoom.Items[selReadingRoom.SelectedIndex].Value;
                List<Seat> seats = handler.GetNowDayBookSeatList(this.UserSchoolInfo, readingRoomNo);
                if (seats.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的日期已经没有可预约的座位。";
                }
                List<SeatManage.ClassModel.ReadingRoomInfo> roomList = handler.GetCanBespeakNowDayRoomInfo(this.UserSchoolInfo);
                if (roomList.Count > 0)
                {
                    foreach (SeatManage.ClassModel.ReadingRoomInfo room in roomList)
                    {
                        if (room.No == readingRoomNo)
                        {
                            if (room.Setting.LimitReaderEnter.Used)
                            {
                                bool isenter = false;
                                if (room.Setting.LimitReaderEnter.CanEnter)
                                {
                                    foreach (string t in room.Setting.LimitReaderEnter.ReaderTypes.Split(';'))
                                    {
                                        if (t == this.LoginUserInfo.ReaderType)
                                        {
                                            isenter = true;
                                        }
                                    }
                                }
                                else
                                {
                                    isenter = true;
                                    foreach (string t in room.Setting.LimitReaderEnter.ReaderTypes.Split(';'))
                                    {
                                        if (t == this.LoginUserInfo.ReaderType)
                                        {
                                            isenter = false;
                                        }
                                    }
                                }
                                if (!isenter)
                                {
                                    seats = new List<Seat>();
                                    spanWarmInfo.Visible = true;
                                    spanWarmInfo.InnerText = "您的读者类型不允许在此阅览室预约。";
                                }
                            }
                            if (room.Setting.SeatBespeak.BespeakArea.BespeakType == BespeakAreaType.Percentage)
                            {
                                int stopSeatCount = 0;
                                foreach (KeyValuePair<string, Seat> item in room.SeatList.Seats)
                                {
                                    if (item.Value.IsSuspended)
                                    {
                                        stopSeatCount++;
                                    }
                                }
                                int bookdCount = room.SeatList.Seats.Count - seats.Count;
                                int canbookCount = (int)((room.SeatList.Seats.Count - stopSeatCount) * room.Setting.SeatBespeak.BespeakArea.Scale);
                                if (bookdCount >= canbookCount)
                                {
                                    seats = new List<Seat>();
                                    spanWarmInfo.Visible = true;
                                    spanWarmInfo.InnerText = "您选择的日期已经没有可预约的座位。";
                                }
                            }
                            break;
                        }
                    }
                }

                DataListBookSeat.DataSource = seats;
                DataListBookSeat.DataBind();
                hidRrId.Value = readingRoomId;
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;
                DataListBookSeat.DataSource = null;
                DataListBookSeat.DataBind();
            }
        }

    }
}