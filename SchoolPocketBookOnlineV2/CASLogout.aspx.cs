using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SchoolPocketBookOnlineV2;
using SeatManage.SeatManageComm;

namespace SeatManageWebV2
{
    public partial class CASLogout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 赋值
            CookiesManager.RemoveCookies("userInfo");
            Session["uid"] = null;
            Session["userName"] = null;
            String logoutUrl = ConfigurationManager.AppSettings["casServerLogoutUrl"];
            string defaultTargetUrl = ConfigurationManager.AppSettings["targetUrl"];
            string loginaspx = ConfigurationManager.AppSettings["appUrl"];
            string redirectUrl = defaultTargetUrl;
            loginaspx += "?redirectUrl=" + redirectUrl;
            loginaspx = HttpUtility.UrlEncode(loginaspx);
            String logoutURL = logoutUrl + "?service=" + loginaspx;
            Response.Redirect(logoutURL);
            #endregion
        }
    }
}