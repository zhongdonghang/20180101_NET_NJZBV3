using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;

namespace WeiXinPocketBookOnline.WaitSeat
{
    public partial class WaitSeatListForm : BasePage
    {
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
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
                if (this.LoginUserInfo.AjmReaderStatus.AjmEnterOutLog != null && LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave.ToString())
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "你已有正在使用中的座位！";
                    return;
                }
                if (this.LoginUserInfo.AjmReaderStatus.AjmWaitSeatLogs != null)
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
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
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
                        if (LoginUserInfo.AjmReaderStatus.AjmWaitSeatLogs != null)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = weiXinService.CancelWait(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
                            LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
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
                //暂时注释
                List<AJM_ReadingRoom> roomList = new List<AJM_ReadingRoom>();
                //List<AJM_ReadingRoom> roomList = handler.GetCanWaitSeatRoomInfo(this.UserSchoolInfo);
                if (this.ReadingRoomList != null)
                {
                    this.ReadingRoomList.Clear();
                }
                else
                {
                    this.ReadingRoomList = new Dictionary<string, AJM_ReadingRoom>();
                }
                foreach (AJM_ReadingRoom room in roomList)
                {
                    this.ReadingRoomList.Add(room.RoomNo, room);
                    ListItem item = new ListItem() { Text = room.RoomName, Value = room.RoomNo };
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
                #region 暂时注释
                //if (!handler.IsCanWaitSeat(UserSchoolInfo,LoginUserInfo.StudentNo, readingRoomNo))
                //{
                //    spanWarmInfo.Visible = true;
                //    spanWarmInfo.InnerText = "您等待座位的间隔过短，请稍后重试。";
                //    return;
                //}
                #endregion
                //暂时注释
                List<Seat> seats = null;
                //List<Seat> seats = handler.GetWaitSeatList(this.UserSchoolInfo, readingRoomNo);
                if (seats.Count <= 0)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "您选择的阅览室当前没有可等待的座位。";
                    return;
                }
                //暂时注释
                List<SeatManage.ClassModel.ReadingRoomInfo> roomList = null;
                //List<SeatManage.ClassModel.ReadingRoomInfo> roomList = handler.GetCanWaitSeatRoomInfo(this.UserSchoolInfo);
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
                                    spanWarmInfo.InnerText = "您的读者类型不允许在此阅览室等待座位。";
                                }
                            }
                            #region 暂时注释
                            //else if (this.LoginUserInfo.BlacklistLog.Count > 0 && room.Setting.UsedBlacklistLimit)
                            //{
                            //    if (room.Setting.BlackListSetting.Used)
                            //    {
                            //        bool isBlack = false;
                            //        foreach (BlackListInfo blinfo in this.LoginUserInfo.BlacklistLog)
                            //        {
                            //            if (blinfo.ReadingRoomID == room.No)
                            //            {
                            //                isBlack = true;
                            //                break;
                            //            }
                            //        }
                            //        if (isBlack)
                            //        {
                            //            seats = new List<Seat>();
                            //            spanWarmInfo.Visible = true;
                            //            spanWarmInfo.InnerText = "您已进入黑名单不允许在此阅览室等待座位。";
                            //        }
                            //    }
                            //    else
                            //    {
                            //        seats = new List<Seat>();
                            //        spanWarmInfo.Visible = true;
                            //        spanWarmInfo.InnerText = "您已进入黑名单不允许在此阅览室等待座位。";
                            //    }
                            //}
                            #endregion
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