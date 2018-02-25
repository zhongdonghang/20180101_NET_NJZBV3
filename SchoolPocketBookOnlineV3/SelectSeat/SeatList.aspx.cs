using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.SelectSeat
{
    public partial class SeatList : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.ISelectSeat handler = new SeatManage.PocketBespeak.PocketBespeak_SelectSeat();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            spanWarmInfo.InnerText = "";
            if (!IsPostBack)
            {
                BindSelReadingRoom();
                //if (selReadingRoom.Items.Count > 0)
                //{
                //    BindBookSeat(selReadingRoom.Items[selReadingRoom.SelectedIndex].Value);
                //}
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
                            spanWarmInfo.InnerText = "当前没有可用阅览室";
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
                        CookiesManager.RefreshNum = 0;
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
                List<ReadingRoomInfo> roomList = handler.GetReadingRoomUsingUsingState();
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
                    spanWarmInfo.InnerText = "读不起当前没有可选座的阅览室";
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
                List<Seat> seats = handler.GetReadingRoomSeatList( readingRoomNo);
                if (seats.Count <= 0)
                {
                    DataListBookSeat.DataSource = null;
                    DataListBookSeat.DataBind();
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "当前阅览室没有可选择的座位。";
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