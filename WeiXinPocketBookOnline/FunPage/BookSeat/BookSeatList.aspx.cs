using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SeatManage.AppJsonModel;
using SeatManage.SeatManageComm;
using WeiXinService;

namespace WeiXinPocketBookOnline.FunPage.BookSeat
{
    public partial class BookSeatList : BasePage
    {
        IWeiXinService handler = new WeiXinServiceHepler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null || UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            spanWarmInfo.InnerText = "";
            if (!IsPostBack)
            {
                BindSelReadingRooms(DateTime.Now.ToShortDateString());
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
                        if (selReadingRoom.SelectedIndex < 0)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "请选择阅览室";
                        }
                        BindBookSeat(txtBookDate.Value, readingRoomId);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "bind":
                        selReadingRoom.Items.Clear();
                        BindSelReadingRooms(txtBookDate.Value);
                        txtBookDate.Value = txtBookDate.Value;
                        //if (selReadingRoom.Items.Count > 0)
                        //{
                        //    BindBookSeat(txtBookDate.Value, selReadingRoom.Items[selReadingRoom.SelectedIndex].Value);
                        //}
                        break;
                }
            }


        }

        /// <summary>
        /// 绑定提供预约阅览室下拉列表
        /// </summary>
        private void BindSelReadingRooms(string date)
        {
            try
            {
                List<AJM_ReadingRoom> roomList = handler.GetBesapeakRoomList(date, UserSchoolInfo.SchoolNo);
                ReadingRoomList = new Dictionary<string, AJM_ReadingRoom>();
                foreach (AJM_ReadingRoom room in roomList)
                {
                    ReadingRoomList.Add(room.RoomNo, room);
                    ListItem item = new ListItem { Text = room.RoomName, Value = room.RoomNo };
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
        private void BindBookSeat(string date, string readingRoomId)
        {
            try
            {
                if (selReadingRoom.SelectedIndex < 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "请选择阅览室";
                    return;
                }
                string readingRoomNo = selReadingRoom.Items[selReadingRoom.SelectedIndex].Value;
                List<AJM_BespeakSeat> besapeakSeat = handler.GetRoomBesapeakSeat(readingRoomNo, date, UserSchoolInfo.SchoolNo);
                if (besapeakSeat.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的阅览室当前日期已经没有可预约的座位。";
                }
                DataListBookSeat.DataSource = besapeakSeat;
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