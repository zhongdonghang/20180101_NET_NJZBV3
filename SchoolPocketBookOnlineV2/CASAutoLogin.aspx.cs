using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using SchoolPocketBookOnlineV2;
using SeatBespeakException;
using SeatManage.ClassModel;
using SeatManage.PocketBespeak;
using SeatManage.SeatManageComm;

namespace SeatManageWebV2
{
    public partial class CASAutoLogin : BasePage
    {
        public string loginOkUrl;
        SeatManage.IPocketBespeak.ILogin handler = new PocketBespeak_Login();
        //CAS Server登录URL
        private string loginServer = ConfigurationManager.AppSettings["casServerLoginUrl"];
        //CAS Server的验证URL
        private string validateServer = ConfigurationManager.AppSettings["casServerServiceValidateUrl"];
        //本地登录入口
        string loginaspx = ConfigurationManager.AppSettings["appUrl"];
        //主页地址
        string defaultTargetUrl = ConfigurationManager.AppSettings["targetUrl"];


        protected void Page_Load(object sender, EventArgs e)
        {
            //登录成功重定向url参数
            string redirectUrl = Request.QueryString["redirectUrl"];
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = defaultTargetUrl;//默认首页
            }
            //已经登录直接跳回
            if (Session["uid"] != null && this.LoginUserInfo!=null)
            {
                Response.Redirect(redirectUrl);
                return;
            }
            loginaspx += "?redirectUrl=" + redirectUrl;
            loginaspx = HttpUtility.UrlEncode(loginaspx);

            string ticket = Request.QueryString["ticket"];
            if (string.IsNullOrEmpty(ticket))
            {
                Response.Redirect(loginServer + "?service=" + loginaspx);
                return;
            }

            string validateUrl = validateServer + "?ticket=" + ticket + "&service=" + loginaspx;
            System.Net.ServicePointManager.CertificatePolicy = new MyPolicy();

            StreamReader Reader = new StreamReader(new WebClient().OpenRead(validateUrl));
            string resp = Reader.ReadToEnd();
            NameTable nt = new NameTable();
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
            XmlParserContext context = new XmlParserContext(null, nsmgr, null, XmlSpace.None);
            XmlTextReader reader = new XmlTextReader(resp, XmlNodeType.Element, context);

            string uid = null;
            string userName = null;
            Boolean authSuccess = false;

            while (reader.Read())
            {
                if (reader.IsStartElement())
                {
                    string tag = reader.LocalName;
                    if (tag == "authenticationSuccess")
                        authSuccess = true;
                    if (tag == "user")
                        uid = reader.ReadString();
                    if (tag == "cn")
                        userName = reader.ReadString();
                }
            }
            reader.Close();

            if (!authSuccess || uid == null)
            {
                Response.Redirect(loginServer + "?service=" + loginaspx);
                return;
            }
            else
            {
                string cardNo = uid;
                 UserInfo user = new UserInfo();

                user.LoginId = cardNo;
                user.Password = cardNo;
                if (loginHandle(user))
                {
                    if (!string.IsNullOrEmpty(loginOkUrl))
                    {
                        Response.Redirect(loginOkUrl);
                    }
                    else
                    {
                        Response.Redirect("MainFunctionPage.aspx");
                    }
                }
                else
                {
                    
                }
                Session["uid"] = uid;
                Session["userName"] = userName;
                Response.Redirect(redirectUrl);
            }

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
        /// <summary>
        /// 验证用户信息并把用户信息和学校信息记录到Session
        /// </summary>
        /// <param name="user"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        private bool loginHandle(UserInfo user)
        {
            //this.UserSchoolInfo = handler.GetSingleSchoolInfo(schoolId);
            AMS.Model.AMS_School school = new AMS.Model.AMS_School();
            school.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            this.UserSchoolInfo = school;
            //ConfigurationSettings.AppSettings["ConnStr"];
            if (string.IsNullOrEmpty(this.UserSchoolInfo.ConnectionString))
            {
                return false;
            }
            try
            {
                this.LoginUserInfo = handler.CheckAndGetReaderInfo(user, this.UserSchoolInfo);
                return true;
            }
            catch (RemoteServiceLinkFailed ex)
            {
                Session.Clear();
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
            catch (Exception ex)
            {
                Session.Clear();
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
        }
    }
}