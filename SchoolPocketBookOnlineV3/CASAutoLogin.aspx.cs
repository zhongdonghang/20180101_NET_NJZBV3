using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;
using SeatBespeakException;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb
{
    public partial class CASAutoLogin : BasePage
    {
        public string loginOkUrl;
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.ILogin handler = new PocketBespeak_Login();
        //CAS Server登录URL
        //private string loginServer = ConfigurationManager.AppSettings["casServerLoginUrl"];
        ////CAS Server的验证URL
        //private string validateServer = ConfigurationManager.AppSettings["casServerServiceValidateUrl"];
        ////本地登录入口
        //string loginaspx = ConfigurationManager.AppSettings["appUrl"];
        ////主页地址
        //string defaultTargetUrl = ConfigurationManager.AppSettings["targetUrl"];


        protected void Page_Load(object sender, EventArgs e)
        {
            string userName = User.Identity.Name;
            if (string.IsNullOrEmpty(userName))
            {
                DotNetCasClient.CasAuthentication.RedirectToLoginPage();
                return;
            }
            string cardNo = userName;
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
            //AMS_School school = new AMS_School();
            //school.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            //UserSchoolInfo = school;
            ////ConfigurationSettings.AppSettings["ConnStr"];
            //if (string.IsNullOrEmpty(UserSchoolInfo.ConnectionString))
            //{
            //    return false;
            //}
            try
            {
                LoginUserInfo = handler.CheckAndGetReaderInfo(user);
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