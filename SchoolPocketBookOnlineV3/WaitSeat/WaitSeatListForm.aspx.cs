using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.WaitSeat
{
    public partial class WaitSeatListForm : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IWaitSeat handler = new SeatManage.PocketBespeak.PocketBespeak_WaitSeat();
        //SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
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
                if (LoginUserInfo.EnterOutLog != null && LoginUserInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "你已有正在使用中的座位！";
                    return;
                }
                if (LoginUserInfo.WaitSeatLog != null)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "你已有正在等待的座位！";
                    return;
                }
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
                            spanWarmInfo.InnerText = "当前没有可等待座位的阅览室";
                            return;
                        }
                        readingRoomId = selReadingRoom.Items[selReadingRoom.SelectedIndex].Value;
                        if (string.IsNullOrEmpty(readingRoomId))
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "请选择阅览室";
                        }
                        BindWaitSeat(readingRoomId);
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
                            BindWaitSeat(selReadingRoom.Items[selReadingRoom.SelectedIndex].Value);
                        }
                        break;
                    case "cancel":
                        if (LoginUserInfo.WaitSeatLog != null)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = handler.CancelWait( LoginUserInfo.WaitSeatLog);
                            LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                            BindSelReadingRoom();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有等待的座位";
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 绑定阅览室下拉列表
        /// </summary>
        private void BindSelReadingRoom()
        {
            try
            {
                List<ReadingRoomInfo> roomList = handler.GetCanWaitSeatRoomInfo();
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
                    spanWarmInfo.InnerText = "当前没有可等待座位的阅览室！";
                    DataListWaitSeat.DataSource = null;
                    DataListWaitSeat.DataBind();
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
        private void BindWaitSeat(string readingRoomId)
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
                if (!handler.IsCanWaitSeat( LoginUserInfo.CardNo, readingRoomNo))
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您等待座位的间隔过短，请稍后重试。";
                    return;
                }
                List<Seat> seats = handler.GetWaitSeatList( readingRoomNo);
                if (seats.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的阅览室当前没有可等待的座位。";
                    return;
                }
                List<ReadingRoomInfo> roomList = handler.GetCanWaitSeatRoomInfo();
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
                                    spanWarmInfo.InnerText = "您的读者类型不允许在此阅览室等待座位。";
                                }
                            }
                            else if (LoginUserInfo.BlacklistLog.Count > 0 && room.Setting.UsedBlacklistLimit)
                            {
                                if (room.Setting.BlackListSetting.Used)
                                {
                                    bool isBlack = false;
                                    foreach (BlackListInfo blinfo in LoginUserInfo.BlacklistLog)
                                    {
                                        if (blinfo.ReadingRoomID == room.No)
                                        {
                                            isBlack = true;
                                            break;
                                        }
                                    }
                                    if (isBlack)
                                    {
                                        seats = new List<Seat>();
                                        spanWarmInfo.Visible = true;
                                        spanWarmInfo.InnerText = "您已进入黑名单不允许在此阅览室等待座位。";
                                    }
                                }
                                else
                                {
                                    seats = new List<Seat>();
                                    spanWarmInfo.Visible = true;
                                    spanWarmInfo.InnerText = "您已进入黑名单不允许在此阅览室等待座位。";
                                }
                            }
                            break;
                        }
                    }
                }

                DataListWaitSeat.DataSource = seats;
                DataListWaitSeat.DataBind();
                hidRrId.Value = readingRoomId;
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;
                DataListWaitSeat.DataSource = null;
                DataListWaitSeat.DataBind();
            }
        }
    }
}