using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using WeiXinJK;

namespace WeiXinServiceManage
{
    /// <summary>
    /// WeiXinSendMsg 的摘要说明
    /// </summary>
    public class WeiXinSendTemplateMsg : IHttpHandler
    {
        HttpContext context = null;
        public void ProcessRequest(HttpContext context)
        {
            this.context = context;
            context.Response.ContentType = "text/plain";
            if (this.context.Request.HttpMethod == "POST")
            {
                string jsonTemplateMsg = getPostData();
                IWeiXinAdvertService service = new WeiXinAdvertService();
                service.SendTxtMessage(jsonTemplateMsg);
                return;
            }
            else
            {
                context.Response.Write("Hello World");
            }
        }

        private static string getPostData()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        /// <summary>
        ///  获取post请求数据
        /// </summary>
        /// <returns></returns>
        private string PostInput()
        {
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            byte[] b = new byte[s.Length];
            s.Read(b, 0, (int)s.Length);
            return Encoding.UTF8.GetString(b);
        }
    }
}