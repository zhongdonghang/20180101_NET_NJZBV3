using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.IPocketBespeak;
using SeatManage.PocketBespeak;
using SeatBespeakException;
namespace PocketBookOnline
{
    public partial class Login : BasePage
    {
        public string cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserSchoolInfo = null;
            if (!IsPostBack)
            {
                BindSelSchool();
                if (Request.QueryString["SchoolID"] != null)
                {
                    foreach (ListItem li in selSchool.Items)
                    {
                        if (li.Value == Request.QueryString["SchoolID"].ToString().Trim())
                        {
                            li.Selected = true;
                            break;
                        }
                    }
                }
            }
            cmd = Request.Form["subCmd"];

            UserInfo user = new UserInfo();
            //通过页面验证，执行登录操作
            if (cmd == "Login")
            {
                string schoolId = selSchool.Items[selSchool.SelectedIndex].Value;
                if (schoolId == "-1")
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = string.Format("请选择学校");
                }
                CookiesManager.RemoveCookies("userInfo");
                user.LoginId = txt_LoginID.Value;
                user.Password = txt_Password.Value;
                if (loginHandle(user, schoolId))
                {
                    AMS.Model.AMS_School school = this.UserSchoolInfo;
                    CookiesManager.SetCookies(user.LoginId, user.Password, schoolId);
                    if (Request.QueryString["url"] != null)
                    {
                        Response.Redirect(Request.QueryString["url"]);
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
                    user.LoginId = CookiesManager.GetCookiesValue(CookiesManager.LoginID);
                    user.Password = CookiesManager.GetCookiesValue(CookiesManager.Password);
                    string schoolId = CookiesManager.GetCookiesValue(CookiesManager.SchoolId);

                    if (loginHandle(user, schoolId))
                    {
                        if (Request.QueryString["url"] != null)
                        {
                            Response.Redirect(Request.QueryString["url"]);
                        }
                        else
                        {
                            Response.Redirect("MainFunctionPage.aspx");
                        }
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
        private bool loginHandle(UserInfo user, string schoolId)
        {
            this.UserSchoolInfo = new TcpClient_BespeakSeat.TcpClient_Login().GetSingleSchoolInfo(schoolId);
            this.BespeakHandler = new TcpClient_BespeakSeat.TcpClient_BespeakSeatAllMethod(this.UserSchoolInfo);
            AMS.Model.AMS_School school = this.UserSchoolInfo;
            if (school == null)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("获取学校信息失败。");
                Session.Clear();
                CookiesManager.RemoveCookies("userInfo");
                return false;
            }
            try
            {
                this.LoginUserInfo = BespeakHandler.CheckAndGetReaderInfo(user, school);
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
                spanWarmInfo.InnerText = string.Format("登录失败！" );
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

            List<AMS.Model.AMS_School> schoolList = new TcpClient_BespeakSeat.TcpClient_Login().GetAllSchoolFromLocal();
            ListItem item = new ListItem("-请选择学校-", "-1");
            selSchool.Items.Add(item);
            for (int i = 0; i < schoolList.Count; i++)
            {
                if (schoolList[i].IsSeatBespeak  )
                {
                    ListItem items = new ListItem(schoolList[i].Name, schoolList[i].Id.ToString());
                    selSchool.Items.Add(items);
                }
            }
        }

    }
}