using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;

namespace PocketBookOnline
{
    public partial class MainFunctionPage : BasePage
    { 

        public string cmd;
        public string state;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }
            int refreshNum = SeatManage.SeatManageComm.CookiesManager.RefreshNum;
            refreshNum += 1;
            SeatManage.SeatManageComm.CookiesManager.RefreshNum = refreshNum;
            //string cardNo = Session["CardNo"].ToString();
            //string conn = Session["SchoolConnectionString"].ToString();
            //string name = Session["Name"].ToString();
            //string schoolId = Session["SessionSchoolId"].ToString();
            // ReaderInfo reader = this.LoginUserInfo;
            if (IsPostBack)
            {
                try
                {
                    this.LoginUserInfo = BespeakHandler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                }
                catch (Exception ex)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            ShowReaderState();
            spanWarmInfo.InnerText = "";
            spanWarmInfo.Visible = false;
            if (!Page.IsPostBack && refreshNum < 2)
            {
                 
            }
            else
            {
                cmd = Request.Form["subCmd"];
                //SeatManage.IPocketBookOnlineBll.IMainFunctionPageBll mainFunctionBll = new SeatManage.PocketBookOnLine.Bll.MainFunctionBll();
                //ReadingRoomStatus roomState = new ReadingRoomStatus();
                //if (this.LoginUserInfo.AtReadingRoom != null)
                //{
                //    roomState = SeatManage.Bll.NowReadingRoomState.ReadingRoomOpenState(this.LoginUserInfo.AtReadingRoom.Setting.RoomOpenSet, DateTime.Now);
                //}

                switch (cmd)
                {
                    case "shortLeave":
                        shortLeaveHandle();//设置读者暂离
                        this.LoginUserInfo = BespeakHandler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);//重新绑定读者状态
                        ShowReaderState();
                        break;
                    case "leave":
                        //释放读者座位
                        freeSeat();
                        this.LoginUserInfo = BespeakHandler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                        ShowReaderState();
                        break;
                    case "LoginOut":
                        this.BespeakHandler = null;
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect("Login.aspx");
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
            ReaderInfo reader = this.LoginUserInfo;
            if (reader.EnterOutLog == null)
            {
                state = "Leave";
            }
            else
            {
                state = reader.EnterOutLog.EnterOutState.ToString();
            }

            if (reader.BespeakLog.Count > 0)
            {
                state = "Booking";
            }
            string message = "";
            switch (state)
            {
                case "SelectSeat":
                case "ComeBack":
                case "ContinuedTime":
                case "WaitingSuccess":
                case "BookingConfirmation":
                case "ReselectSeat": message = "当前状态：在座"; break;
                case "Leave": message = ""; break;
                case "Booking": message = "今天有预约未确认"; break;
                case "Waiting": message = "您正在等待座位"; break;
                case "ShortLeave": message = "当前状态：暂离"; break;
                default: message = ""; break;
            }
            if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
            {
                string nowMessage = "";
                nowMessage = this.LoginUserInfo.Name + "你好,";
                if (message != "")
                {
                    nowMessage += string.Format("你正在{0} {1}。", reader.AtReadingRoom.Name, message);
                    SpanNowState.InnerText = nowMessage;
                }
                else
                {
                    SpanNowState.InnerText = "";
                }
            }
            else
            {
                SpanNowState.InnerText = this.LoginUserInfo.Name + "你好,你当前没有座位。";
            }
        }

        /// <summary>
        /// 设置读者暂离的业务逻辑
        /// </summary>
        /// <returns></returns>
        private void shortLeaveHandle()
        {
            try
            {
                string resultValue = BespeakHandler.SetShortLeave(this.UserSchoolInfo, this.LoginUserInfo);
                if (!string.IsNullOrEmpty(resultValue))
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = resultValue;
                }
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;

            }
            //if (state == "SelectSeat" || state == "ComeBack" || state == "ContinuedTime" || state == "ReselectSeat")
            //{
            //    if (reader.AtReadingRoom == null)
            //    {
            //        return;
            //    }
            //    result = mainFunction.AddEnterOutLog(reader.AtReadingRoom.No, cardNo, 2, reader.EnterOutLog.SeatNo, reader.EnterOutLog.EnterOutLogNo, 0, roomState, conn);
            //    if (result == HandleResult.Failed)
            //    {
            //        spanWarmInfo.Visible = true;
            //        spanWarmInfo.InnerText = "暂离失败,您可能已经不是暂离状态！";
            //    }
            //    else
            //    {
            //        spanWarmInfo.Visible = true;
            //        spanWarmInfo.InnerText = "暂离成功，请在规定时间内回来刷卡！";
            //    }
            //}
            //else if (state == "ShortLeave")
            //{
            //    spanWarmInfo.Visible = true;
            //    spanWarmInfo.InnerText = "您当前已经是暂离状态";
            //}
            //else
            //{
            //    spanWarmInfo.Visible = true;
            //    spanWarmInfo.InnerText = "您当前没有座位";
            //}
            //break;
        }

        /// <summary>
        /// 释放座位
        /// </summary>
        private void freeSeat()
        {
            try
            {
                string resultValue = BespeakHandler.FreeSeat(this.UserSchoolInfo, this.LoginUserInfo);
                if (!string.IsNullOrEmpty(resultValue))
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = resultValue;
                }
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;

            }
            //if (state == "SelectSeat" || state == "ComeBack" || state == "ContinuedTime" || state == "ReselectSeat" || state == "ShortLeave")
            //{
            //    if (reader.AtReadingRoom == null)
            //    {
            //        return;
            //    }
            //    result = mainFunction.AddEnterOutLog(reader.AtReadingRoom.No, cardNo, 3, reader.EnterOutLog.SeatNo, reader.EnterOutLog.EnterOutLogNo, 0, roomState, conn);
            //    if (result == HandleResult.Failed)
            //    {
            //        spanWarmInfo.Visible = true;
            //        spanWarmInfo.InnerText = "离开座位失败，座位可能已被释放！";
            //    }
            //    else
            //    {
            //        spanWarmInfo.Visible = true;
            //        spanWarmInfo.InnerText = "您已成功离开座位！";
            //    }
            //}
            //else
            //{
            //    spanWarmInfo.Visible = true;
            //    spanWarmInfo.InnerText = "您当前没有座位！";
            //}
            //break;
        }
        /// <summary>
        /// 刷新读者状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            this.LoginUserInfo = BespeakHandler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
            ShowReaderState();
        }


    }
}