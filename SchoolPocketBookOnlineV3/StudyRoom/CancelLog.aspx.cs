using System;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.StudyRoom
{
    public partial class CancelLog : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IBookStudyRoom handler = new SeatManage.PocketBespeak.PecketBespeak_BookStudyRoom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            string cmd = Request.Form["subCmd"];
            int logNo = int.Parse(Request.QueryString["ID"]);
            if (IsPostBack)
            {
                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "cancel":
                        StudyBookingLog log = handler.GetStudyLog( logNo);
                        log.Remark = txtRemark.Value;
                        string result = handler.CancelStudyLog( log);
                        if (string.IsNullOrEmpty(result))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消申请成功！');location.href='StudyLogList.aspx';", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消申请失败！');window.close();", true);
                        }
                        break;
                }
            }
        }
    }
}