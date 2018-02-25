using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.BookSeat
{
    public partial class BookSeatListForm : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IBespeakSeatListForm handler = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            spanWarmInfo.InnerText = "";
            //if (this.LoginUserInfo.BespeakLog.Count > 0)
            //{
            //    spanWarmInfo.Visible = true;
            //    spanWarmInfo.InnerText = "你已经存在有效的预约记录";
            //    return;
            //}

            //string cardNo = Session["CardNo"].ToString();
            //string name = Session["Name"].ToString();
            //spanWarmInfo.Visible = false;
            //if (string.IsNullOrEmpty(cardNo) || string.IsNullOrEmpty(conn))
            //{
            //    Response.Redirect("Login.aspx");
            //}
            string bookDate = "";
            if (!IsPostBack)
            {
                bookDate = DateTime.Now.AddDays(1).ToShortDateString();
                txtBookDate.Value = bookDate;
                BindSelReadingRoom(bookDate);
            }
            else
            {
                string cmd = Request.Form["subCmd"];
                string readingRoomId = "";
                switch (cmd)
                {
                    case "query":
                        bookDate = txtBookDate.Value;
                        if (selReadingRoom.Items.Count < 1)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有可预约阅览室";
                            return;
                        }
                        readingRoomId = selReadingRoom.Items[selReadingRoom.SelectedIndex].Value;
                        if (string.IsNullOrEmpty(bookDate))
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "请选择预约日期";
                        }
                        if (string.IsNullOrEmpty(readingRoomId))
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "请选择阅览室";
                        }
                        BindBookSeat(readingRoomId, bookDate);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "bind":
                        bookDate = txtBookDate.Value;
                        selReadingRoom.Items.Clear();
                        BindSelReadingRoom(bookDate);
                        txtBookDate.Value = bookDate;
                        if (selReadingRoom.Items.Count > 0)
                        {
                            BindBookSeat(selReadingRoom.Items[selReadingRoom.SelectedIndex].Value, bookDate);
                        }
                        break;
                }
            }


        }

        /// <summary>
        /// 绑定提供预约阅览室下拉列表
        /// </summary>
        private void BindSelReadingRoom(string bookDate)
        {
            DateTime bespeakDate = DateTime.Parse(bookDate);
            try
            {
                List<ReadingRoomInfo> roomList = handler.GetCanBespeakReaderRoomInfo( bespeakDate);
                if (ReadingRoomList != null)
                {
                    ReadingRoomList.Clear();
                }
                else
                {
                    ReadingRoomList = new Dictionary<string, ReadingRoomInfo>();
                }
                foreach (ReadingRoomInfo room in roomList)
                {
                    ReadingRoomList.Add(room.No, room);
                    ListItem item = new ListItem { Text = room.Name, Value = room.No };
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
        private void BindBookSeat(string readingRoomId, string bookDate)
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
                List<Seat> seats = handler.GetBookSeatList(DateTime.Parse(bookDate), readingRoomNo);
                if (seats.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的日期已经没有可预约的座位。";
                }
                List<ReadingRoomInfo> roomList = handler.GetCanBespeakReaderRoomInfo(DateTime.Parse(bookDate));
                if (roomList.Count > 0)
                {
                    foreach (ReadingRoomInfo room in roomList)
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
                                        if (t == LoginUserInfo.ReaderType)
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
                                        if (t == LoginUserInfo.ReaderType)
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
                hidBookDate.Value = bookDate;
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