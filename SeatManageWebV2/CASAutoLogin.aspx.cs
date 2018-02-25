using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using DotNetCasClient;

namespace SeatManageWebV2
{
    public partial class CASAutoLogin : DefaultBasePage
    {
        ////CAS Server登录URL
        //private string loginServer = ConfigurationManager.AppSettings["casServerLoginUrl"];
        ////CAS Server的验证URL
        //private string validateServer = ConfigurationManager.AppSettings["casServerServiceValidateUrl"];
        ////本地登录入口
        //string loginaspx = ConfigurationManager.AppSettings["appUrl"];
        //主页地址
        string defaultTargetUrl = ConfigurationManager.AppSettings["targetUrl"];


        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = User.Identity.Name;
            //SeatManage.SeatManageComm.WriteLog.Write(userName);
            //登录成功重定向url参数
            string redirectUrl = Request.QueryString["redirectUrl"];
            //string tickets = Request.QueryString["CASTGC"];
            if (string.IsNullOrEmpty(userName))
            {
                DotNetCasClient.CasAuthentication.RedirectToLoginPage();
                return;
            }

            string cardNo = userName;
            SeatManage.Bll.Users_ALL userinfocheck = new SeatManage.Bll.Users_ALL();
            string loginID = userinfocheck.CheckUser(cardNo, cardNo);
            if (string.IsNullOrEmpty(loginID))
            {
                // Response.Write("对不起，您所请求的座位管理系统没有授权，请与管理员联系。 ");
                return;
            }
            else
            {
                this.LoginId = loginID;
            }
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = defaultTargetUrl;
            }
            Response.Redirect(redirectUrl);


            ////string userName = User.Identity.Name;

            /////获取用户其他的属性,案例，部分代码,获取方式优化中
            //HttpCookie ticketCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            //FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(ticketCookie.Value);
            //CasAuthenticationTicket casTicket = CasAuthentication.ServiceTicketManager.GetTicket(ticket.UserData);
            //Dictionary<string, string> userAttrs = new Dictionary<string, string>();
            //foreach (KeyValuePair<string, IList<string>> attribute in casTicket.Assertion.Attributes)
            //{
            //    string key = attribute.Key;
            //    Response.Write("key:" + key + "::::" + attribute.Value);
            //    StringBuilder builder = new StringBuilder();
            //    foreach (string valuePart in attribute.Value)
            //    { builder.AppendLine("        " + valuePart); }
            //    userAttrs.Add(key, builder.ToString());
            //}
            //foreach (var VARIABLE in userAttrs)
            //{
            //    SeatManage.SeatManageComm.WriteLog.Write(VARIABLE.Key + "'" + VARIABLE.Value);

            //}




















            ////备注
            //SeatManage.SeatManageComm.WriteLog.Write(redirectUrl ?? "");

            //if (string.IsNullOrEmpty(redirectUrl))
            //{
            //    redirectUrl = defaultTargetUrl;//默认首页
            //}
            ////备注
            //SeatManage.SeatManageComm.WriteLog.Write(Session["uid"] == null ? "" : Session["uid"].ToString());

            ////已经登录直接跳回
            //if (Session["uid"] != null && !string.IsNullOrEmpty(this.LoginId))
            //{
            //    Response.Redirect(redirectUrl);
            //    return;
            //}
            //loginaspx += "?redirectUrl=" + redirectUrl;
            //loginaspx = HttpUtility.UrlEncode(loginaspx);



            //string ticket = Request.QueryString["ticket"];
            //if (string.IsNullOrEmpty(ticket))
            //{
            //    Response.Redirect(loginServer + "?service=" + loginaspx);
            //    return;
            //}



            ////string validateUrl = validateServer + "?ticket=" + ticket + "&service=" + loginaspx
            //string validateUrl = validateServer + "?ticket=" + ticket;

            //SeatManage.SeatManageComm.WriteLog.Write(validateUrl);


            //System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            //StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateUrl));
            //string resp = Reader.ReadToEnd();

            //SeatManage.SeatManageComm.WriteLog.Write(resp);

            //NameTable nt = new NameTable();
            //XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
            //XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
            //XmlTextReader reader = new XmlTextReader(resp, XmlNodeType.Element, context);

            //string uid = null;
            //string userName = null;
            //bool authSuccess = false;

            //while (reader.Read())
            //{
            //    if (reader.IsStartElement())
            //    {
            //        string tag = reader.LocalName;
            //        if (tag == "authenticationSuccess")
            //            authSuccess = true;
            //        if (tag == "user")
            //            uid = reader.ReadString();
            //        if (tag == "cn")
            //            userName = reader.ReadString();
            //    }
            //}
            //reader.Close();


            ////备注
            //SeatManage.SeatManageComm.WriteLog.Write(uid ?? "");
            ////备注
            //SeatManage.SeatManageComm.WriteLog.Write(userName ?? "");
            ////备注
            //SeatManage.SeatManageComm.WriteLog.Write(authSuccess.ToString());


            //if (!authSuccess || uid == null)
            //{
            //    Response.Redirect(loginServer + "?service=" + loginaspx);
            //    return;
            //}
            //else
            //{
            


        }
        public class MyPolicy : ICertificatePolicy
        {
            public bool CheckValidationResult(
                  ServicePoint srvPoint
                , X509Certificate certificate
                , WebRequest request
                , int certificateProblem)
            {

                //Return True to force the certificate to be accepted.
                return true;

            } // end CheckValidationResult
        }
    }
}