using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnline.BookSeat
{
    public partial class BookSeatMessage : BasePage
    {
        SeatManage.IPocketBespeak.IBespeakSeatListForm handler = new SeatManage.PocketBespeak.PocketBespeak_BespeakSeat();
        SeatManage.IPocketBespeak.IMainFunctionPageBll handler1 = new SeatManage.PocketBespeak.PocketBespeak_MainFunctionPageBll();
        string seatNo = "";
        string seatShortNo = "";
        string date = "";
        string roomNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect("../Login.aspx");
            }
            seatNo = Request.QueryString["seatNo"];
            seatShortNo = Request.QueryString["seatShortNo"];
            date = Request.QueryString["date"];
            roomNo = Request.QueryString["roomNo"];

            if (!IsPostBack)
            {
                BindUIElement(seatNo, seatShortNo, DateTime.Parse(date));
            }
            string cmd = Request.Form["subCmd"];
            switch (cmd)
            {
                case "query":
                    SeatManage.ClassModel.BespeakLogInfo bespeakModel = new SeatManage.ClassModel.BespeakLogInfo();
                    bespeakModel.BsepeakState = SeatManage.EnumType.BookingStatus.Waiting;
                    DateTime bespeatDate = DateTime.Parse(string.Format("{0} {1}", DateTime.Parse(date).ToShortDateString(), lblBookTime.InnerText));
                    bespeakModel.BsepeakTime = bespeatDate;
                    bespeakModel.CardNo = this.LoginUserInfo.CardNo;
                    bespeakModel.ReadingRoomNo = roomNo;
                    bespeakModel.Remark = string.Format("读者通过手机预约网站预约座位");
                    bespeakModel.SeatNo = seatNo;
                    bespeakModel.SubmitTime = DateTime.Now;
                    try
                    {
                        string resultValue = handler.SubmitBespeakInfo(this.UserSchoolInfo, bespeakModel);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        if (!string.IsNullOrEmpty(resultValue))
                        {
                            page1.Style.Add("display", "none");
                            page2.Style.Add("display", "none");
                            page3.Style.Add("display", "block");
                            MessageTip.InnerText = resultValue;
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

        void BindUIElement(string seatNo, string seatShortNo, DateTime date)
        {
            lblBookDate.InnerText = date.ToShortDateString();
            this.lblSeatNo.InnerText = seatShortNo;
            this.lblReadingRoomName.InnerText = this.ReadingRoomList[roomNo].Name;
            lblBookTime.InnerText = this.ReadingRoomList[roomNo].Setting.RoomOpenSet.DefaultOpenTime.BeginTime;
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