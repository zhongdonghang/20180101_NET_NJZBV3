using System;
using SeatManage.ClassModel;

namespace WeiXinPocketBookOnline.WaitSeat
{
    public partial class WaitSeatMessage : BasePage
    {
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
        string seatNo = "";
        string seatShortNo = "";
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
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
                    waitInfo.CardNo = LoginUserInfo.StudentNo;
                    waitInfo.SeatNo = seatNo;
                    waitInfo.NowState = SeatManage.EnumType.LogStatus.Valid;
                    waitInfo.OperateType = SeatManage.EnumType.Operation.Reader;
                    waitInfo.WaitingState = SeatManage.EnumType.EnterOutLogType.Waiting;
                    try
                    {
                        string resultValue =weiXinService.WaitSeat(waitInfo.CardNo,waitInfo.CardNoB,waitInfo.SeatNo,UserSchoolInfo.SchoolNo);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = resultValue;
                            this.LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
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
            #region 暂时注释
            //DateTime nowDateTime = DateTime.Now;
            //if (this.ReadingRoomList[roomNo].Setting.SeatHoldTime.UsedAdvancedSet)
            //{
            //    for (int i = 0; i < this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
            //    {
            //        if (this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
            //        {
            //            DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
            //            DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
            //            if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
            //            {
            //                lblWaitTime.InnerText = this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
            //                lblGetSeatTime.InnerText = nowDateTime.AddMinutes(this.ReadingRoomList[roomNo].Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
            //                break;
            //            }
            //        }
            //    }
            //}
            //else
            //{
            //    lblWaitTime.InnerText = this.ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
            //    lblGetSeatTime.InnerText = nowDateTime.AddMinutes(this.ReadingRoomList[roomNo].Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
            //}
            //this.lblSeatNo.InnerText = seatShortNo;
            //this.lblReadingRoomName.InnerText = this.ReadingRoomList[roomNo].RoomName;
            //lblSeatNo_Booked.InnerText = seatShortNo;
            #endregion
        }
    }

}