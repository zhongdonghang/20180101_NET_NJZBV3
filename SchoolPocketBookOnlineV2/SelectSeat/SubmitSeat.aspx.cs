using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.SelectSeat
{
    public partial class SubmitSeat : BasePage
    {
        SeatManage.IPocketBespeak.ISelectSeat handler = new SeatManage.PocketBespeak.PocketBespeak_SelectSeat();
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
                case "select":
                    BindUIElement(seatNo, seatShortNo);
                    break;
                case "submit":

                    try
                    {
                        string resultValue = handler.SubmitSeat(this.UserSchoolInfo, this.LoginUserInfo.CardNo, seatNo, roomNo);//bookSeatMessageBll.AddBespeakLogInfo(bespeakModel, Session["SchoolConnectionString"].ToString());
                        page1.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = resultValue;

                    }
                    catch (Exception ex)
                    {
                        page1.Style.Add("display", "none");
                        page3.Style.Add("display", "block");
                        MessageTip.InnerText = ex.Message;
                        return;
                    }
                    break;
            }
        }

        void BindUIElement(string seatNo, string seatShortNo)
        {
            if (!IsPostBack)
            {
                this.lblSeatNo.InnerText = seatShortNo;
                this.lblReadingRoomName.InnerText = this.ReadingRoomList[roomNo].Name;
            }
        }
    }
}