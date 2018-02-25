using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.EnumType;

namespace WeiXinPocketBookOnline.UserInfos
{
    public partial class UserInfo : BasePage
    {
        public string listMessage = "";
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
        //private SeatManage.IPocketBespeak.IMainFunctionPageBll handler = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        //private SeatManage.IPocketBespeak.IQueryLogs handler1 = new SeatManage.PocketBespeak.PocketBespeak_QueryLogs();
        //private SeatManage.IPocketBespeak.IWaitSeat handler2 = new SeatManage.PocketBespeak.PocketBespeak_WaitSeat();
        //private SeatManage.IPocketBespeak.IBespeakSeatListForm handler3 = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();

        public string cmd;
        public string state;
        public string bookNo;
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
                LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
                ShowReaderState();
            }
            //暂时注释ShowBookLogs();
            spanWarmInfo.InnerText = "";
            spanWarmInfo.Visible = false;
            if (!Page.IsPostBack && refreshNum < 2)
            {

            }
            else
            {
                cmd = Request.Form["subCmd"];
                bookNo = Request.Form["subBookNo"];
                //SeatManage.IPocketBookOnlineBll.IMainFunctionPageBll mainFunctionBll = new SeatManage.PocketBookOnLine.Bll.MainFunctionBll();
                ReadingRoomStatus roomState = new ReadingRoomStatus();
                if (LoginUserInfo.AjmReadingRoomState != null)
                {
                    roomState = (ReadingRoomStatus)Enum.Parse(typeof(ReadingRoomStatus), LoginUserInfo.AjmReadingRoomState.OpenCloseState);
                }

                switch (cmd)
                {
                    case "shortLeave":
                        shortLeaveHandle();//设置读者暂离
                        this.LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);//重新绑定读者状态
                        ShowReaderState();
                        break;
                    case "leave":
                        //释放读者座位
                        freeSeat();
                        this.LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo); ;
                        ShowReaderState();
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "ContinuedWhen":
                        LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo); ;
                        EnterOutLogType enterOutLogType = new EnterOutLogType();
                        enterOutLogType =
                            (EnterOutLogType)
                                Enum.Parse(typeof(EnterOutLogType),
                                    LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.EnterOutState);
                        if (LoginUserInfo.AjmReaderStatus.AjmEnterOutLog != null && enterOutLogType != EnterOutLogType.Leave)
                        {
                            switch (enterOutLogType)
                            {
                                case EnterOutLogType.BookingConfirmation:
                                case EnterOutLogType.SelectSeat:
                                case EnterOutLogType.ContinuedTime:
                                case EnterOutLogType.ComeBack:
                                case EnterOutLogType.ReselectSeat:
                                case EnterOutLogType.WaitingSuccess:
                                    LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.Remark = "通过手机预约网站延长座位使用时间";
                                    LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.EnterOutState = EnterOutLogType.ContinuedTime.ToString();
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
                        enterOutLogType =
                            (EnterOutLogType)
                                Enum.Parse(typeof(EnterOutLogType),
                                    LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.EnterOutState);
                        LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
                        if (LoginUserInfo.AjmReaderStatus.AjmEnterOutLog != null && enterOutLogType == EnterOutLogType.ShortLeave)
                        {
                            LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.Remark = "通过手机预约网站恢复在座";
                            LoginUserInfo.AjmReaderStatus.AjmEnterOutLog.EnterOutState = EnterOutLogType.ComeBack.ToString();
                            ComeBack();
                            ShowReaderState();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "暂离回来失败，您还没有暂离";
                        }
                        break;
                    case "cancel":
                        CancelBookLog(bookNo);
                        confrimSeat();
                        LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);//重新绑定读者状态
                        ShowReaderState();
                        //暂时注释ShowBookLogs();
                        break;
                    case "CancelWait":
                        if (LoginUserInfo.AjmReaderStatus.AjmWaitSeatLogs != null)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = weiXinService.CancelWait(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
                            ShowReaderState();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有等待的座位";
                        }
                        break;
                    case "CancelBook":
                        if (LoginUserInfo.AjmReaderStatus.AjmBespeakLogs != null && LoginUserInfo.AjmReaderStatus.AjmBespeakLogs.Count > 0)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = weiXinService.CancelBesapeakById(int.Parse(LoginUserInfo.AjmReaderStatus.AjmBespeakLogs[0].Id), UserSchoolInfo.SchoolNo);

                            LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);//重新绑定读者状态
                            ShowReaderState();
                            //暂时注释ShowBookLogs();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有预约的座位";
                        }
                        break;
                    case "BookConfirm":
                        if (LoginUserInfo.AjmReaderStatus.AjmBespeakLogs != null && LoginUserInfo.AjmReaderStatus.AjmBespeakLogs.Count > 0)
                        {
                            confrimSeat();
                            LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);//重新绑定读者状态
                            ShowReaderState();
                            //暂时注释ShowBookLogs();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有预约的座位";
                        }
                        break;
                }
                subCmd.Value = "";
            }
        }
        /// <summary>
        /// 显示读者状态
        /// </summary>
        /// <param name="reader"></param>
        private void ShowReaderState()
        {
            AJM_WeiXinUserInfo reader = LoginUserInfo;
            if (reader.AjmReaderStatus.AjmEnterOutLog == null)
            {
                state = "Leave";
            }
            else
            {
                state = reader.AjmReaderStatus.AjmEnterOutLog.EnterOutState.ToString();
            }

            if (reader.AjmReaderStatus.AjmBespeakLogs.Count > 0 && state == "Leave")
            {
                state = "Booking";
            }
            if (reader.AjmReaderStatus.AjmWaitSeatLogs != null)
            {
                state = "Waiting";
            }
            string statusMessage = "";
            btnLeave.Visible = false;
            btnShortLeave.Visible = false;
            btn_ComeBack.Visible = false;
            btn_ContinuedWhen.Visible = false;
            btn_CancelBook.Visible = false;
            btn_CancelWait.Visible = false;
            btn_BookConfirm.Visible = false;

            lblReadingRoomName.InnerText = "无";
            lblSeatNo.InnerText = "无";
            lblSeatStatus.InnerText = "无";
            lblenterOutTime.InnerText = "无";
            lblRemark.InnerText = "无";
            switch (state)
            {
                case "SelectSeat":
                case "ComeBack":
                case "ContinuedTime":
                case "WaitingSuccess":
                case "BookingConfirmation":
                case "ReselectSeat":
                    lblSeatStatus.InnerText = "在座";
                    lblReadingRoomName.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.RoomName;
                    lblSeatNo.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.SeatShortNo;
                    lblenterOutTime.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.EnterOutTime;
                    lblRemark.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.Remark;
                    if (reader.AjmPecketBookSetting.UseShortLeave)
                    {
                        btnShortLeave.Visible = true;
                    }
                    if (reader.AjmPecketBookSetting.UseCanLeave)
                    {
                        btnLeave.Visible = true;
                    }
                    //暂时注释
                    //if (reader.AjmPecketBookSetting.UseContinue && reader.Setting.SeatUsedTimeLimit.Used && reader.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
                    //{
                    //    btn_ContinuedWhen.Visible = true;
                    //}

                    break;
                case "Leave":
                    break;
                case "Booking":
                    lblSeatStatus.InnerText = "预约等待签到中";
                    lblReadingRoomName.InnerText = reader.AjmReaderStatus.AjmBespeakLogs[0].RoomName;
                    lblSeatNo.InnerText = reader.AjmReaderStatus.AjmBespeakLogs[0].SeatShortNo;
                    lblenterOutTime.InnerText = reader.AjmReaderStatus.AjmBespeakLogs[0].BookTime;
                    lblRemark.InnerText = reader.AjmReaderStatus.AjmBespeakLogs[0].Remark;
                    if (reader.AjmPecketBookSetting.UseCancelBook)
                    {
                        btn_CancelBook.Visible = true;
                    }
                    if (reader.AjmPecketBookSetting.UseBookComfirm)
                    {
                        if (reader.AjmReaderStatus.AjmBespeakLogs[0].SubmitDateTime == reader.AjmReaderStatus.AjmBespeakLogs[0].BookTime)
                        {
                            btn_BookConfirm.Visible = true;
                        }
                        else
                        {
                            #region 暂时注释
                            //if (Convert.ToDateTime(reader.AjmReaderStatus.AjmBespeakLogs[0].BookTime).AddMinutes(-double.Parse(reader.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= DateTime.Now)
                            //{
                            //    btn_BookConfirm.Visible = true;
                            //}
                            #endregion
                        }
                    }
                    break;
                case "Waiting":
                    lblSeatStatus.InnerText = "等待座位";
                    lblReadingRoomName.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.RoomName;
                    lblSeatNo.InnerText = reader.AjmReaderStatus.AjmWaitSeatLogs.SeatShortNo;
                    lblenterOutTime.InnerText = reader.AjmReaderStatus.AjmWaitSeatLogs.SeatWaitTime;
                    lblRemark.InnerText = "您把读者" + reader.AjmReaderStatus.AjmWaitSeatLogs.StudentNo_A + "设置为暂离，并等待此座位。";
                    if (reader.AjmPecketBookSetting.UseCancelWait)
                    {
                        btn_CancelWait.Visible = true;
                    }
                    break;
                case "ShortLeave":
                    lblSeatStatus.InnerText = "暂离";
                    lblReadingRoomName.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.RoomName;
                    lblSeatNo.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.SeatShortNo;
                    lblenterOutTime.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.EnterOutTime;
                    lblRemark.InnerText = reader.AjmReaderStatus.AjmEnterOutLog.Remark;
                    if (reader.AjmPecketBookSetting.UseComeBack)
                    {
                        btn_ComeBack.Visible = true;
                    }
                    if (reader.AjmPecketBookSetting.UseCanLeave)
                    {
                        btnLeave.Visible = true;
                    }
                    break;
                default: statusMessage = "没有座位";
                    break;
            }
            //if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
            //{
            //    lblReadingRoomName.InnerText = reader.EnterOutLog.ReadingRoomName;
            //    lblSeatNo.InnerText = reader.EnterOutLog.ShortSeatNo;
            //    lblSeatStatus.InnerText = statusMessage;
            //    lblenterOutTime.InnerText = reader.EnterOutLog.EnterOutTime.ToLongTimeString();
            //    lblRemark.InnerText = reader.EnterOutLog.Remark;
            //}
            //else
            //{
            //    lblReadingRoomName.InnerText = "无";
            //    lblSeatNo.InnerText = "无";
            //    lblSeatStatus.InnerText = statusMessage;
            //    lblenterOutTime.InnerText = "无";
            //    lblRemark.InnerText = "无";
            //}
        }
        private void confrimSeat()
        {
            try
            {
                //暂时注释
                string resultValue = null;
                //string resultValue = handler3.ConfrimSeat(this.UserSchoolInfo, int.Parse(LoginUserInfo.AjmReaderStatus.AjmBespeakLogs[0].Id));
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
        }
        /// <summary>
        /// 设置读者暂离的业务逻辑
        /// </summary>
        /// <returns></returns>
        private void shortLeaveHandle()
        {
            try
            {
                string resultValue = weiXinService.ShortLeave(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
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
        }

        /// <summary>
        /// 释放座位
        /// </summary>
        private void freeSeat()
        {
            try
            {
                string resultValue = weiXinService.ReleaseSeat(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
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
        }
        /// <summary>
        /// 续时
        /// </summary>
        private void ContinuedWhen()
        {
            try
            {
                //暂时注释
                string resultValue = "";
                //string resultValue = handler.DelaySeatUsedTime(this.UserSchoolInfo, this.LoginUserInfo);
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
                string resultValue = weiXinService.ComeBack(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = resultValue;
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
            LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);//重新绑定读者状态
            ShowReaderState();
        }

        public override void DataBind()
        {

        }

        /// <summary>
        /// 绑定预约记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="rrId">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        private void ShowBookLogs(int pageIndex, int pageSize)
        {
            try
            {
                List<AJM_BespeakLog> bookLogList = null;
                bookLogList = weiXinService.GetBesapsekLog(LoginUserInfo.StudentNo, pageIndex, pageSize, UserSchoolInfo.SchoolNo);
                StringBuilder sbListInfo = new StringBuilder();
                sbListInfo.Append("<li data-theme='d' data-role='list-divider' role='heading'>预约记录 </li>");
                if (bookLogList.Count < 1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "没有符合查询条件的预约记录信息";
                }
                else
                {
                    for (int i = 0; i < bookLogList.Count; i++)
                    {
                        if (bookLogList[i].IsValid)
                        {
                            sbListInfo.Append(string.Format("<li date-theme='d'>{0}：{1}<ul date-theme='d'><li>预约时间：{2}</li><li>提交时间：{3}</li><li>取消时间：{4}</li>", bookLogList[i].RoomName, bookLogList[i].SeatShortNo, bookLogList[i].BookTime, bookLogList[i].SubmitDateTime, bookLogList[i].CancelTime));
                            sbListInfo.Append("<li>预约状态：等待确认</li>");
                            sbListInfo.Append(string.Format("<li><input data-inline='true' data-mini='false' value='取消' type='button' onclick='subCancel(&apos;{0}&apos;)' /></li>", bookLogList[i].Id));
                        }
                    }
                    sbListInfo.Append("</ul></li>");
                }
                listMessage = sbListInfo.ToString();
            }

            catch (Exception ex)
            {
                listMessage = "查询出错" + ex.Message;
            }
        }

        /// <summary>
        /// 取消预约记录
        /// </summary>
        /// <param name="bookNo"></param>
        /// <param name="bookCancelPerson"></param>
        /// <param name="conn"></param>
        private void CancelBookLog(string bookNo)
        {
            try
            {
                string result = weiXinService.CancelBesapeakById(int.Parse(bookNo), UserSchoolInfo.SchoolNo);
                ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('" + result + "');window.close();", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}