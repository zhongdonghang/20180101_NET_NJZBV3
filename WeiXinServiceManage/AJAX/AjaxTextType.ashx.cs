using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AMS.ServiceProxy;
using WeiXinJK.Model;

namespace WeiXinServiceManage.AJAX
{
    /// <summary>
    /// AjaxTextType 的摘要说明
    /// </summary>
    public class AjaxTextType : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int type=Convert.ToInt32(context.Request.Form["types"]);
            EnumReplyMsgType MsgType = new EnumReplyMsgType();
            MsgType = (EnumReplyMsgType)type;
            string types = WeiXinProxy.GetResponse(MsgType);
            string sss = "";
            context.Response.Write(types);
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