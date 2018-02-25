using System;
using System.Configuration;
using System.Web;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb
{
    public partial class CASLogout : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DotNetCasClient.CasAuthentication.SingleSignOut();
            DotNetCasClient.CasAuthentication.ClearAuthCookie();
            string loginServer = ConfigurationManager.AppSettings["casServerLoginUrl"];
            Response.Redirect(loginServer);
        }
    }
}