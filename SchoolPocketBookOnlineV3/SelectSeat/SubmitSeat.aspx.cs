using System;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;

namespace SchoolPocketBookWeb.SelectSeat
{
    public partial class SubmitSeat : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.ISelectSeat handler = new SeatManage.PocketBespeak.PocketBespeak_SelectSeat();
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
                case "select":
                    BindUIElement(seatNo, seatShortNo);
                    break;
                case "submit":

                    try
                    {
                        string resultValue = handler.SubmitSeat(LoginUserInfo.CardNo, seatNo, roomNo);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        page1.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = resultValue;

                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                    }
                    break;
            }
        }

        void BindUIElement(string seatNo, string seatShortNo)
        {
            if (!IsPostBack)
            {
                lblSeatNo.InnerText = seatShortNo;
                lblReadingRoomName.InnerText = ReadingRoomList[roomNo].Name;
            }
        }
    }
}