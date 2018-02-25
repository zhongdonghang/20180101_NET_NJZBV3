using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.UserInfos
{
    public partial class UserInfo : BasePage
    {
        public string listMessage = "";
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //private SeatManage.IPocketBespeak.IMainFunctionPageBll handler = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        //private SeatManage.IPocketBespeak.IQueryLogs handler1 = new SeatManage.PocketBespeak.PocketBespeak_QueryLogs();
        //private SeatManage.IPocketBespeak.IWaitSeat handler2 = new SeatManage.PocketBespeak.PocketBespeak_WaitSeat();
        //private SeatManage.IPocketBespeak.IBespeakSeatListForm handler3 = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();

        public string cmd;
        public string state;
        public string bookNo;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            DataBind();
            int refreshNum = CookiesManager.RefreshNum;
            refreshNum += 1;
            CookiesManager.RefreshNum = refreshNum;
            if (!IsPostBack)
            {
                LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                ShowReaderState();
            }
            ShowBookLogs();
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
                if (LoginUserInfo.AtReadingRoom != null)
                {
                    roomState = NowReadingRoomState.ReadingRoomOpenState(LoginUserInfo.AtReadingRoom.Setting.RoomOpenSet, DateTime.Now);
                }

                switch (cmd)
                {
                    case "shortLeave":
                        shortLeaveHandle();//设置读者暂离
                        LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);//重新绑定读者状态
                        ShowReaderState();
                        break;
                    case "leave":
                        //释放读者座位
                        freeSeat();
                        LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                        ShowReaderState();
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "ContinuedWhen":
                        LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                        if (LoginUserInfo.EnterOutLog != null && LoginUserInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                        {
                            switch (LoginUserInfo.EnterOutLog.EnterOutState)
                            {
                                case EnterOutLogType.BookingConfirmation:
                                case EnterOutLogType.SelectSeat:
                                case EnterOutLogType.ContinuedTime:
                                case EnterOutLogType.ComeBack:
                                case EnterOutLogType.ReselectSeat:
                                case EnterOutLogType.WaitingSuccess:
                                    LoginUserInfo.EnterOutLog.Remark = "通过手机预约网站延长座位使用时间";
                                    LoginUserInfo.EnterOutLog.EnterOutState = EnterOutLogType.ContinuedTime;
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
                        LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                        if (LoginUserInfo.EnterOutLog != null && LoginUserInfo.EnterOutLog.EnterOutState == EnterOutLogType.ShortLeave)
                        {
                            LoginUserInfo.EnterOutLog.Remark = "通过手机预约网站恢复在座";
                            LoginUserInfo.EnterOutLog.EnterOutState = EnterOutLogType.ComeBack;
                            ComeBack();
                            ShowReaderState();
                        }
                        spanWarmInfo.Visible = true;
                        spanWarmInfo.InnerText = "暂离回来失败，您还没有暂离";
                        break;
                    case "cancel":
                        CancelBookLog(bookNo);
                        confrimSeat();
                        LoginUserInfo = handler.GetReaderInfo(LoginUserInfo.CardNo);//重新绑定读者状态
                        ShowReaderState();
                        ShowBookLogs();
                        break;
                    case "CancelWait":
                        if (LoginUserInfo.WaitSeatLog != null)
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = handler.CancelWait( LoginUserInfo.WaitSeatLog);
                            ShowReaderState();
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有等待的座位";
                        }
                        break;
                    case "CancelBook":
                        if (LoginUserInfo.BespeakLog != null && LoginUserInfo.BespeakLog.Count > 0)
                        {
                            spanWarmInfo.Visible = true;
                            if (handler.UpdateBookLogsState( int.Parse(LoginUserInfo.BespeakLog[0].BsepeaklogID)))
                            {
                                spanWarmInfo.InnerText = "取消预约成功";
                                confrimSeat();
                                LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);//重新绑定读者状态
                                ShowReaderState();
                                ShowBookLogs();
                            }
                            else
                            {
                                spanWarmInfo.InnerText = "取消预约取消失败";
                            }
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "当前没有预约的座位";
                        }
                        break;
                    case "BookConfirm":
                        if (LoginUserInfo.BespeakLog != null && LoginUserInfo.BespeakLog.Count > 0)
                        {
                            confrimSeat();
                            LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);//重新绑定读者状态
                            ShowReaderState();
                            ShowBookLogs();
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
            ReaderInfo reader = LoginUserInfo;
            if (reader.EnterOutLog == null)
            {
                state = "Leave";
            }
            else
            {
                state = reader.EnterOutLog.EnterOutState.ToString();
            }

            if (reader.BespeakLog.Count > 0 && state == "Leave")
            {
                state = "Booking";
            }
            if (reader.WaitSeatLog != null)
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
                    lblReadingRoomName.InnerText = reader.EnterOutLog.ReadingRoomName;
                    lblSeatNo.InnerText = reader.EnterOutLog.ShortSeatNo;
                    lblenterOutTime.InnerText = reader.EnterOutLog.EnterOutTime.ToLongTimeString();
                    lblRemark.InnerText = reader.EnterOutLog.Remark;
                    if (reader.PecketWebSetting.UseShortLeave)
                    {
                        btnShortLeave.Visible = true;
                    }
                    if (reader.PecketWebSetting.UseCanLeave)
                    {
                        btnLeave.Visible = true;
                    }
                    if (reader.PecketWebSetting.UseContinue && reader.AtReadingRoom.Setting.SeatUsedTimeLimit.Used && reader.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
                    {
                        btn_ContinuedWhen.Visible = true;
                    }
                    break;
                case "Leave":
                    break;
                case "Booking":
                    lblSeatStatus.InnerText = "预约等待签到中";
                    lblReadingRoomName.InnerText = reader.BespeakLog[0].ReadingRoomName;
                    lblSeatNo.InnerText = reader.BespeakLog[0].ShortSeatNum;
                    lblenterOutTime.InnerText = reader.BespeakLog[0].BsepeakTime.ToLongTimeString();
                    lblRemark.InnerText = reader.BespeakLog[0].Remark;
                    if (reader.PecketWebSetting.UseCancelBook)
                    {
                        btn_CancelBook.Visible = true;
                    }
                    if (reader.PecketWebSetting.UseBookComfirm)
                    {
                        if (reader.BespeakLog[0].SubmitTime == reader.BespeakLog[0].BsepeakTime)
                        {
                            btn_BookConfirm.Visible = true;
                        }
                        else
                        {
                            if (reader.BespeakLog[0].BsepeakTime.AddMinutes(-double.Parse(reader.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)) <= DateTime.Now)
                            {
                                btn_BookConfirm.Visible = true;
                            }
                        }
                    }
                    break;
                case "Waiting":
                    lblSeatStatus.InnerText = "等待座位";
                    lblReadingRoomName.InnerText = reader.WaitSeatLog.EnterOutLog.ReadingRoomName;
                    lblSeatNo.InnerText = reader.WaitSeatLog.EnterOutLog.ShortSeatNo;
                    lblenterOutTime.InnerText = reader.WaitSeatLog.SeatWaitTime.ToLongTimeString();
                    lblRemark.InnerText = "您把读者" + reader.WaitSeatLog.EnterOutLog.CardNo + "设置为暂离，并等待此座位。";
                    if (reader.PecketWebSetting.UseCancelWait)
                    {
                        btn_CancelWait.Visible = true;
                    }
                    break;
                case "ShortLeave":
                    lblSeatStatus.InnerText = "暂离";
                    lblReadingRoomName.InnerText = reader.EnterOutLog.ReadingRoomName;
                    lblSeatNo.InnerText = reader.EnterOutLog.ShortSeatNo;
                    lblenterOutTime.InnerText = reader.EnterOutLog.EnterOutTime.ToLongTimeString();
                    lblRemark.InnerText = reader.EnterOutLog.Remark;
                    if (reader.PecketWebSetting.UseComeBack)
                    {
                        btn_ComeBack.Visible = true;
                    }
                    if (reader.PecketWebSetting.UseCanLeave)
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
                string resultValue = handler.CheckSeat( int.Parse(LoginUserInfo.BespeakLog[0].BsepeaklogID));
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
                string resultValue = handler.SetShortLeave(LoginUserInfo.CardNo);
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
                string resultValue = handler.FreeSeat(LoginUserInfo.CardNo);
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
                string resultValue = handler.DelaySeatUsedTime( LoginUserInfo);
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
                string resultValue = handler.ReaderComeBack( LoginUserInfo);
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
            LoginUserInfo = handler.GetReaderInfo(LoginUserInfo.CardNo);
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
        private void ShowBookLogs()
        {
            try
            {
                List<BespeakLogInfo> bookLogList = null;
                bookLogList = handler.GetBookLogs( LoginUserInfo.CardNo, null, 30);
                StringBuilder sbListInfo = new StringBuilder();
                sbListInfo.Append("<li data-theme='d' data-role='list-divider' role='heading'>预约记录 </li>");
                if (bookLogList==null || bookLogList.Count < 1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "没有符合查询条件的预约记录信息";
                }
                else
                {
                    for (int i = 0; i < bookLogList.Count; i++)
                    {
                        if (bookLogList[i].BsepeakState == BookingStatus.Waiting)
                        {
                            sbListInfo.Append(string.Format("<li date-theme='d'>{0}：{1}<ul date-theme='d'><li>预约时间：{2}</li><li>提交时间：{3}</li><li>取消时间：{4}</li>", bookLogList[i].ReadingRoomName, bookLogList[i].ShortSeatNum, bookLogList[i].BsepeakTime, bookLogList[i].SubmitTime, bookLogList[i].CancelTime));
                            sbListInfo.Append("<li>预约状态：等待确认</li>");
                            sbListInfo.Append(string.Format("<li><input data-inline='true' data-mini='false' value='取消' type='button' onclick='subCancel(&apos;{0}&apos;)' /></li>", bookLogList[i].BsepeaklogID));
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
                bool result = handler.UpdateBookLogsState( int.Parse(bookNo));
                if (result)
                {
                    ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('成功取消预约！');window.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消预约失败！');window.close();", true);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.ToString());
                throw ex;
            }
        }
    }
}