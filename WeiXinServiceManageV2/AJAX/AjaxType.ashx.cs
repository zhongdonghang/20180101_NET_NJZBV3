using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.Model;
using AMS.ServiceProxy;
using System.Web.SessionState;

namespace WeiXinServiceManage.AJAX
{
    /// <summary>
    /// AjaxType 的摘要说明
    /// </summary>
    public class AjaxType : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            string name ="";
            string pwd = "";
            AMS_UserInfo userinfo = context.Session["Login"] as AMS_UserInfo;
            context.Response.Write(userinfo.Remark);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}