using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.PocketBespeak;
using SeatManage.SeatManageComm;
using SeatBespeakException;
using System.Configuration;

namespace SchoolPocketBookOnline
{
    public partial class Login : BasePage
    {
        public string cmd;
        SeatManage.IPocketBespeak.ILogin handler = new PocketBespeak_Login();
        protected void Page_Load(object sender, EventArgs e)
        {
          
            //if (!IsPostBack)
            //{
            //    BindSelSchool();
            //    if (Request.QueryString["SchoolID"] != null)
            //    {
            //        foreach (ListItem li in selSchool.Items)
            //        {
            //            if (li.Value == Request.QueryString["SchoolID"].ToString().Trim())
            //            {
            //                li.Selected = true;
            //                break;
            //            }
            //        }
            //    }
            //}
            cmd = Request.Form["subCmd"];

            UserInfo user = new UserInfo();
            //通过页面验证，执行登录操作
            if (cmd == "Login")
            {
                //string schoolId = selSchool.Items[selSchool.SelectedIndex].Value;
                //if (schoolId == "-1")
                //{
                //    spanWarmInfo.Visible = true;
                //    spanWarmInfo.InnerText = string.Format("请选择学校");
               // }
                CookiesManager.RemoveCookies("userInfo");
                user.LoginId = txt_LoginID.Value;
                user.Password = txt_Password.Value;
                if (loginHandle(user))
                {
                    //若选择记住密码，则记录用户信息cookies
                    if (chk_RemPasspword.Checked == true)
                    {
                        AMS.Model.AMS_School school = this.UserSchoolInfo;
                        CookiesManager.SetCookies(user.LoginId, user.Password,"1");
                    }
                    else//清除cookies
                    {
                        CookiesManager.RemoveCookies("userInfo");
                    }
                    Response.Redirect("MainFunctionPage.aspx");
                }
            }
            else
            {
                if (Request.Cookies["userInfo"] != null)//存在记录的cookies信息
                {
                    user.LoginId = CookiesManager.GetCookiesValue(CookiesManager.LoginID);
                    user.Password = CookiesManager.GetCookiesValue(CookiesManager.Password);
                    string schoolId = CookiesManager.GetCookiesValue(CookiesManager.SchoolId);

                    if (loginHandle(user))
                    {
                        Response.Redirect("MainFunctionPage.aspx");
                    }
                    else
                    {
                        return;
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
        private bool loginHandle(UserInfo user)
        {
            //this.UserSchoolInfo = handler.GetSingleSchoolInfo(schoolId);
            AMS.Model.AMS_School school = new AMS.Model.AMS_School();
            school.ConnectionString = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            this.UserSchoolInfo = school;
                //ConfigurationSettings.AppSettings["ConnStr"];
            if (string.IsNullOrEmpty(this.UserSchoolInfo.ConnectionString ))
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
        //private void BindSelSchool()
        //{

        //    List<AdvertManage.Model.AMS_SchoolModel> schoolList = handler.GetAllSchoolFromLocal();
        //    ListItem item = new ListItem("-请选择学校-", "-1");
        //    selSchool.Items.Add(item);
        //    for (int i = 0; i < schoolList.Count; i++)
        //    {
        //        if (schoolList[i].Flag == 1)
        //        {
        //            ListItem items = new ListItem(schoolList[i].Name, schoolList[i].Id.ToString());
        //            selSchool.Items.Add(items);
        //        }
        //    }
        //}

    }
}