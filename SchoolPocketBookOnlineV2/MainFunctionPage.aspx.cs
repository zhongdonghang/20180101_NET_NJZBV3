using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using SeatManage.Bll;
using System.Configuration;

namespace SchoolPocketBookOnlineV2
{
    public partial class MainFunctionPage : BasePage
    {
        private SeatManage.IPocketBespeak.IMainFunctionPageBll handler = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();

        public string cmd;
        public string state;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            DataBind();
            int refreshNum = SeatManage.SeatManageComm.CookiesManager.RefreshNum;
            refreshNum += 1;
            SeatManage.SeatManageComm.CookiesManager.RefreshNum = refreshNum;
            if (!IsPostBack)
            {
                ShowReaderState();
            }
            spanWarmInfo.InnerText = "";
            spanWarmInfo.Visible = false;
            if (!Page.IsPostBack && refreshNum < 2)
            {

            }
            else
            {
                cmd = Request.Form["subCmd"];
                //SeatManage.IPocketBookOnlineBll.IMainFunctionPageBll mainFunctionBll = new SeatManage.PocketBookOnLine.Bll.MainFunctionBll();
                ReadingRoomStatus roomState = new ReadingRoomStatus();
                if (this.LoginUserInfo.AtReadingRoom != null)
                {
                    roomState = SeatManage.Bll.NowReadingRoomState.ReadingRoomOpenState(this.LoginUserInfo.AtReadingRoom.Setting.RoomOpenSet, DateTime.Now);
                }

                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        string LogOutUrl = LogoutUrl();
                        if (string.IsNullOrEmpty(LogOutUrl))
                        {
                            Response.Redirect("Login.aspx");
                        }
                        else
                        {
                            Response.Redirect(LogOutUrl);
                        }

                        break;
                }
            }
        }
        /// <summary>
        /// 显示读者状态
        /// </summary>
        /// <param name="reader"></param>
        private void ShowReaderState()
        {
            //ReaderInfo reader = this.LoginUserInfo;
            //if (reader.EnterOutLog == null)
            //{
            //    state = "Leave";
            //}
            //else
            //{
            //    state = reader.EnterOutLog.EnterOutState.ToString();
            //}

            //if (reader.BespeakLog.Count > 0)
            //{
            //    state = "Booking";
            //}
            //string message = "";
            //switch (state)
            //{
            //    case "SelectSeat":
            //    case "ComeBack":
            //    case "ContinuedTime":
            //    case "WaitingSuccess":
            //    case "BookingConfirmation":
            //    case "ReselectSeat": message = "当前状态：在座"; break;
            //    case "Leave": message = ""; break;
            //    case "Booking": message = "今天有预约未确认"; break;
            //    case "Waiting": message = "您正在等待座位"; break;
            //    case "ShortLeave": message = "当前状态：暂离"; break;
            //    default: message = ""; break;
            //}
            //if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
            //{
            //    string nowMessage = "";
            //    nowMessage = this.LoginUserInfo.Name + "你好,";
            //    if (message != "")
            //    {
            //        nowMessage += string.Format("你正在{0}{1}号座位 {2}。", reader.AtReadingRoom.Name, reader.EnterOutLog.ShortSeatNo, message);
            //        SpanNowState.InnerText = nowMessage;
            //    }
            //    else
            //    {
            //        SpanNowState.InnerText = "";
            //    }
            //}
            //else
            //{
            //    SpanNowState.InnerText = this.LoginUserInfo.Name + "你好,你当前没有座位。";
            //}
        }


        /// <summary>
        /// 刷新读者状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoginUserInfo = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
            ShowReaderState();
        }

        public override void DataBind()
        {
            SeatManage.ClassModel.ReaderInfo reader = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
            if (!reader.PecketWebSetting.UseBookSeat)
            {
                btn_book.Visible = false;
            }
            if (!reader.PecketWebSetting.UseWaitSeat)
            {
                btn_WaitSeat.Visible = false;
            }
        }
    }
}