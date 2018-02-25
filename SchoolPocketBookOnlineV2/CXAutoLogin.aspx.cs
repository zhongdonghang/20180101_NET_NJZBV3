using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatBespeakException;
using SeatManage.ClassModel;
using SeatManage.PocketBespeak;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookOnlineV2
{
    public partial class CXAutoLogin : BasePage
    {
        SeatManage.IPocketBespeak.ILogin handler = new PocketBespeak_Login();
        /// <summary>
        /// 返回的地址
        /// </summary>
        private string cxRedirecturl = ConfigurationManager.AppSettings["CXRedirecturl"];
        /// <summary>
        /// 登录的地址
        /// </summary>
        private string cxLoginurl = ConfigurationManager.AppSettings["CXLoginurl"];

        /// <summary>
        /// key
        /// </summary>
        private string cxKey = ConfigurationManager.AppSettings["CXMD5Key"];
        //主页地址
        string defaultTargetUrl = ConfigurationManager.AppSettings["targetUrl"];

        protected void Page_Load(object sender, EventArgs e)
        {
            string redirectUrl = Request.QueryString["redirectUrl"];
            string check = Request.QueryString["check"];
            string uid = Request.QueryString["uid"];
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = defaultTargetUrl;//默认首页
            }
            if (this.LoginUserInfo != null)
            {
                Response.Redirect(redirectUrl);
                return;
            }
            if (string.IsNullOrEmpty(uid))
            {
                Response.Redirect(LogoutUrl());
                return;
            }
            if (!string.IsNullOrEmpty(check))
            {
                if (bool.Parse(check))
                {
                    string cardNo = uid;
                    UserInfo user = new UserInfo();

                    user.LoginId = cardNo;
                    user.Password = cardNo;
                    if (loginHandle(user))
                    {
                        if (!string.IsNullOrEmpty(redirectUrl))
                        {
                            Response.Redirect(redirectUrl);
                        }
                        else
                        {
                            Response.Redirect("MainFunctionPage.aspx");
                        }
                    }
                }
                else
                {
                    Response.Redirect(LogoutUrl());
                    return;
                }
            }
            string cxRedirecttrue="?uid=" + uid + "&check=true";
            string cxRedirectfalse = "?uid=" + uid + "&check=false";
            string md5string = uid + cxKey + DateTime.Now.ToString("yyyyMMddHHmm");
            md5string = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(md5string);
            cxLoginurl += "?signkey=" + md5string + "&uid=" + uid + "&redirecturl=" + HttpUtility.UrlEncode(cxRedirecturl + cxRedirecttrue) + "&loginurl=" + HttpUtility.UrlEncode(cxRedirecturl + cxRedirectfalse);

            Response.Redirect(cxLoginurl);

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