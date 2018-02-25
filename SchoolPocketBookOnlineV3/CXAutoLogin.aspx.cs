using System;
using System.Configuration;
using System.Web;
using SeatBespeakException;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb
{
    public partial class CXAutoLogin : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        /// <summary>
        /// key
        /// </summary>
        private string cxKey = ConfigurationManager.AppSettings["CXMD5Key"];

        protected void Page_Load(object sender, EventArgs e)
        {
            string signkey = Request.QueryString["signkey"];
            string uid = Request.QueryString["uid"];
            string loginOkUrl = Request.QueryString["url"];
            if (string.IsNullOrEmpty(loginOkUrl))
            {
                loginOkUrl = "MainFunctionPage.aspx";
            }

            if (LoginUserInfo != null)
            {
                Response.Redirect(loginOkUrl);
                return;
            }
            if (string.IsNullOrEmpty(uid) || string.IsNullOrEmpty(signkey))
            {
                Response.Redirect(LogoutUrl());
                return;
            }
            string md5string = uid + cxKey + DateTime.UtcNow.ToString("yyyyMMddHH");
            if (signkey == MD5Algorithm.GetMD5Str32(md5string))
            {
                if (loginHandle(uid))
                {
                    Response.Redirect(loginOkUrl);
                }
            }
            Response.Redirect(loginOkUrl);
        }
        /// <summary>
        /// 验证用户信息并把用户信息和学校信息记录到Session
        /// </summary>
        /// <param name="user"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        private bool loginHandle(string cardNo)
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
                LoginUserInfo = handler.GetReaderInfoByCardNo(cardNo);
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