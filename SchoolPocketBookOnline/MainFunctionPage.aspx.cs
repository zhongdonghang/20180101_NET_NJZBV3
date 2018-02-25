using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using SeatManage.Bll;

namespace SchoolPocketBookOnline
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
                Response.Redirect("Login.aspx");
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
                    case "shortLeave":
                        shortLeaveHandle();//设置读者暂离
                        this.LoginUserInfo = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);//重新绑定读者状态
                        ShowReaderState();
                        break;
                    case "leave":
                        //释放读者座位
                        freeSeat();
                        this.LoginUserInfo = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                        ShowReaderState();
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect("Login.aspx");
                        break;
                    case "ContinuedWhen":
                        this.LoginUserInfo = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                        if (this.LoginUserInfo.EnterOutLog != null && this.LoginUserInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                        {
                            switch (this.LoginUserInfo.EnterOutLog.EnterOutState)
                            {
                                case EnterOutLogType.BookingConfirmation:
                                case EnterOutLogType.SelectSeat:
                                case EnterOutLogType.ContinuedTime:
                                case EnterOutLogType.ComeBack:
                                case EnterOutLogType.ReselectSeat:
                                case EnterOutLogType.WaitingSuccess:
                                    this.LoginUserInfo.EnterOutLog.Remark = "通过手机预约网站延长座位使用时间";
                                    this.LoginUserInfo.EnterOutLog.EnterOutState = EnterOutLogType.ContinuedTime;
                                    ContinuedWhen();
                                    ShowReaderState();
                                    break;
                                case EnterOutLogType.ShortLeave:
                                    spanWarmInfo.Visible = true;
                                    spanWarmInfo.InnerText = "续时失败，你处于暂离状态";
                                    break;
                            }
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "续时失败，您还没有选座";
                        }
                        break;
                    case "ComeBack":
                        this.LoginUserInfo = handler.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                        if (this.LoginUserInfo.EnterOutLog != null && this.LoginUserInfo.EnterOutLog.EnterOutState == EnterOutLogType.ShortLeave)
                        {
                            this.LoginUserInfo.EnterOutLog.Remark = "通过手机预约网站恢复在座";
                            this.LoginUserInfo.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                            ComeBack();
                            ShowReaderState();
                            break;
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "暂离回来失败，您还没有暂离";
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
                    nowMessage += string.Format("你正在{0}{1}号座位 {2}。", reader.AtReadingRoom.Name,reader.EnterOutLog.ShortSeatNo, message);
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
                string resultValue = handler.SetShortLeave(this.UserSchoolInfo, this.LoginUserInfo);
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
                string resultValue = handler.FreeSeat(this.UserSchoolInfo, this.LoginUserInfo);
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
        /// 续时
        /// </summary>
        private void ContinuedWhen()
        {
            try
            {
                string resultValue = handler.DelaySeatUsedTime(this.UserSchoolInfo, this.LoginUserInfo);
                if (!string.IsNullOrEmpty(resultValue))
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = resultValue;
                }
                else
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "操作成功";
                }
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;

            }
        }
        /// <summary>
        /// 暂离回来
        /// </summary>
        private void ComeBack()
        {
            try
            {
                string resultValue = ((SeatManage.IPocketBespeak.IMainFunctionPage_Ex)handler).ReaderComeBack(this.UserSchoolInfo, this.LoginUserInfo);
                if (!string.IsNullOrEmpty(resultValue))
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = resultValue;
                }
                else
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "操作成功";
                }
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = ex.Message;

            }
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
            if (reader.AtReadingRoom == null)
            {
                return;
            }

            SeatManage.ClassModel.ReadingRoomSetting roomSet = reader.AtReadingRoom.Setting;
            if (roomSet.SeatUsedTimeLimit.Used && roomSet.SeatUsedTimeLimit.IsCanContinuedTime)
            {
                this.btn_ContinuedWhen.Visible = true;
            }
            else
            {
                this.btn_ContinuedWhen.Visible = false;
            }
        }
    }
}