using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SeatManageWebV2
{
    public class Global : System.Web.HttpApplication
    {

        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }

        void Session_Start(object sender, EventArgs e)
        {
            // 在新会话启动时运行的代码

        }

        void Session_End(object sender, EventArgs e)
        {
            // 在会话结束时运行的代码。 
            // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
            // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
            // 或 SQLServer，则不会引发该事件。

        }
        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //if (Request.Form.Count > 0)
            //{
            //    string s1 = Request.ServerVariables["SERVER_NAME"].Trim();//服务器名称 
            //    if (Request.ServerVariables["HTTP_REFERER"] != null)
            //    {
            //        string s2 = Request.ServerVariables["HTTP_REFERER"].Trim();//http接收的名称 
            //        string s4 = Request.ServerVariables["HTTP_HOST"].Trim();//类似这样的格式www.ccopus.com 
            //        int count = s4.Length + 1 + 7;
            //        string s5 = "Florms/FormSYS.aspx";
            //        string s3 = s2.Substring(count).ToLower();
            //        if (s3 != s5.ToLower().Trim() && s3 != "default.aspx")
            //        {
            //            Response.Write("警告！你的IP已经被记录!不要使用敏感字符！");// 
            //            Response.End();
            //        }
            //    }
            //}
        }
    }
}
