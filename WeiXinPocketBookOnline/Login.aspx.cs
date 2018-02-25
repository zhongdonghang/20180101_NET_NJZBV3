using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI.WebControls;
using AMS.Model;
using SeatBespeakException;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using WeiXinJK;

namespace WeiXinPocketBookOnline
{
    public partial class Login : BasePage
    {
        public string cmd;
        public string loginOkUrl;
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
        protected void Page_Load(object sender, EventArgs e)
        {

            cmd = Request.Form["subCmd"];
            loginOkUrl = Request.QueryString["url"];
            AJM_WeiXinUserInfo user = new AJM_WeiXinUserInfo();
            string loginId = txt_LoginID.Value;
            string password = txt_Password.Value;
            if (!IsPostBack)
            {
                BindSelSchool();
            }

            //通过页面验证，执行登录操作
            if (cmd == "Login")
            {
                CookiesManager.RemoveCookies("userInfo");
                if (loginHandle(loginId, password, selSchool.Value))
                {
                    //若选择记住密码，则记录用户信息cookies
                    CookiesManager.SetCookies(loginId, password, UserSchoolInfo.SchoolNo);
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
            else
            {
                if (Request.Cookies["userInfo"] != null)//存在记录的cookies信息
                {
                    loginId = CookiesManager.GetCookiesValue(CookiesManager.LoginID);
                    password = CookiesManager.GetCookiesValue(CookiesManager.Password);
                    string schoolId = CookiesManager.GetCookiesValue(CookiesManager.SchoolId);

                    if (loginHandle(loginId, password, schoolId))
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
            }
        }
        /// <summary>
        /// 验证用户信息并把用户信息和学校信息记录到Session
        /// </summary>
        /// <param name="user"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        private bool loginHandle(string loginId, string password, string schoolId)
        {
            AJM_School school = new AJM_School();
            school.SchoolNo = schoolId;
            UserSchoolInfo = school;
            if (string.IsNullOrEmpty(school.SchoolNo))
            {
                return false;
            }
            try
            {
                if (weiXinService.CheckReader(loginId, password, UserSchoolInfo.SchoolNo) != null)
                {
                    LoginUserInfo = weiXinService.GetUserInfo_WeiXin(loginId, UserSchoolInfo.SchoolNo);
                    return true;
                }
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
            catch (RemoteServiceLinkFailed ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("连接学校服务器失败，可能是学校已经关闭了服务器的远程访问。");
                Session.Clear();
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("登录失败:{0}", ex.Message);
                Session.Clear();
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
        }
        /// <summary>
        /// 绑定界面学校下拉列表
        /// </summary>
        private void BindSelSchool()
        {
            List<AMS_School> amsSchools = weiXinService.GetWeCharSchoolList();
            ListItem item = new ListItem("-请选择学校-", "-1");
            selSchool.Items.Add(item);
            for (int i = 0; i < amsSchools.Count; i++)
            {
                if (amsSchools[i].IsSeatBespeak)
                {
                    ListItem items = new ListItem(amsSchools[i].Name, amsSchools[i].Number);
                    selSchool.Items.Add(items);
                }
            }
        }

    }
}