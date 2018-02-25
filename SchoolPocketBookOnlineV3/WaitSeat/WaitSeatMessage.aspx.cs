using System;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.WaitSeat
{
    public partial class WaitSeatMessage : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IWaitSeat handler = new SeatManage.PocketBespeak.PocketBespeak_WaitSeat();
        //SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        string seatNo = "";
        string seatShortNo = "";
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            seatNo = Request.QueryString["seatNo"];
            seatShortNo = Request.QueryString["seatShortNo"];
            roomNo = Request.QueryString["roomNo"];

            if (!IsPostBack)
            {
                BindUIElement(seatNo, seatShortNo);
            }
            string cmd = Request.Form["subCmd"];
            switch (cmd)
            {
                case "query":
                    WaitSeatLogInfo waitInfo = new WaitSeatLogInfo();
                    waitInfo.CardNo = LoginUserInfo.CardNo;
                    waitInfo.SeatNo = seatNo;
                    waitInfo.NowState = LogStatus.Valid;
                    waitInfo.OperateType = Operation.Reader;
                    waitInfo.WaitingState = EnterOutLogType.Waiting;
                    try
                    {
                        string resultValue = handler.SubmitWaitInfo( waitInfo);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = resultValue;
                            LoginUserInfo = handler.GetReaderInfo( LoginUserInfo.CardNo);
                        }
                        else
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = "未知错误";
                        }
                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                    }
                    break;
            }
        }
        void BindUIElement(string seatNo, string seatShortNo)
        {
            DateTime nowDateTime = DateTime.Now;
            if (ReadingRoomList[roomNo].Setting.SeatHoldTime.UsedAdvancedSet)
            {
                for (int i = 0; i < ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                {
                    if (ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                    {
                        DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                        DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                        if (DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                        {
                            lblWaitTime.InnerText = ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                            lblGetSeatTime.InnerText = nowDateTime.AddMinutes(ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                            break;
                        }
                    }
                }
            }
            else
            {
                lblWaitTime.InnerText = ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                lblGetSeatTime.InnerText = nowDateTime.AddMinutes(ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
            }
            lblSeatNo.InnerText = seatShortNo;
            lblReadingRoomName.InnerText = ReadingRoomList[roomNo].Name;
            lblSeatNo_Booked.InnerText = seatShortNo;
            //判断自己是否已经预约座位
            //this.LoginUserInfo = handler1.GetReaderInfo(this.UserSchoolInfo,this.LoginUserInfo.CardNo); 
            //List<SeatManage.ClassModel.BespeakLogInfo> readerBespeaklist =this.LoginUserInfo.BespeakLog;
            //if (readerBespeaklist.Count > 0)
            //{
            //    page1.Style.Add("display", "none");
            //    page2.Style.Add("display", "none");
            //    page3.Style.Add("display", "block");
            //    MessageTip.InnerText = "您选择的日期已经预约了座位，请先取消原来的预约。";
            //    return;
            //}
        }
    }

}