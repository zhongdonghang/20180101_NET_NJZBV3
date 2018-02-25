using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2
{
    public partial class CASLogout : DefaultBasePage
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