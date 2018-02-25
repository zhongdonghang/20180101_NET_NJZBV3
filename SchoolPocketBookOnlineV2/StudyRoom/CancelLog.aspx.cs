using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.StudyRoom
{
    public partial class CancelLog : BasePage
    {
        SeatManage.IPocketBespeak.IBookStudyRoom handler = new SeatManage.PocketBespeak.PecketBespeak_BookStudyRoom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
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
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "cancel":
                        SeatManage.ClassModel.StudyBookingLog log = handler.GetStudyLog(this.UserSchoolInfo, logNo);
                        log.Remark = txtRemark.Value;
                        string result = handler.CancelStudyLog(this.UserSchoolInfo, log);
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