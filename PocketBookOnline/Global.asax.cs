using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace PocketBookOnline
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            //Exception objError = Server.GetLastError().GetBaseException();
            //string error = "page:" + Request.Url.ToString() + "；";
            //error += "message:" + objError.Message + "；";
            //error += "detail:" + objError.ToString();
            //Server.ClearError();
            //Application["errorMsg"] = error;
            //string errorUrl = "http://" + Request.Url.Authority.ToString() + "/ErrorPage.aspx";
            //Response.Redirect(errorUrl, true);
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}