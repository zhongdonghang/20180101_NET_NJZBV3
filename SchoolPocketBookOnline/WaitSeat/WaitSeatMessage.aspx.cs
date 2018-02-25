using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;

namespace SchoolPocketBookOnline.WaitSeat
{
    public partial class WaitSeatMessage : BasePage
    {
        SeatManage.IPocketBespeak.IWaitSeat handler = new SeatManage.PocketBespeak.PocketBespeak_WaitSeat();
        SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        string seatNo = "";
        string seatShortNo = "";
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect("../Login.aspx");
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
                    waitInfo.CardNo = this.LoginUserInfo.CardNo;
                    waitInfo.SeatNo = seatNo;
                    waitInfo.NowState = SeatManage.EnumType.LogStatus.Valid;
                    waitInfo.OperateType = SeatManage.EnumType.Operation.Reader;
                    waitInfo.WaitingState = SeatManage.EnumType.EnterOutLogType.Waiting;
                    try
                    {
                        string resultValue = handler.SubmitWaitInfo(this.UserSchoolInfo, waitInfo);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = resultValue;
                            this.LoginUserInfo = handler1.GetReaderInfo(this.UserSchoolInfo, this.LoginUserInfo.CardNo);
                        }
                        else
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = "未知错误";
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page2.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                        return;
                    }
                    break;
            }
        }
        void BindUIElement(string seatNo, string seatShortNo)
        {
            DateTime nowDateTime = DateTime.Now;
            if (this.ReadingRoomList[roomNo].Setting.SeatHoldTime.UsedAdvancedSet)
            {
                for (int i = 0; i < this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                {
                    if (this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                    {
                        DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                        DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                        if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                        {
                            lblWaitTime.InnerText = this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                            lblGetSeatTime.InnerText = nowDateTime.AddMinutes(this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                            break;
                        }
                    }
                }
            }
            else
            {
                lblWaitTime.InnerText = this.ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                lblGetSeatTime.InnerText = nowDateTime.AddMinutes(this.ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
            }
            this.lblSeatNo.InnerText = seatShortNo;
            this.lblReadingRoomName.InnerText = this.ReadingRoomList[roomNo].Name;
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