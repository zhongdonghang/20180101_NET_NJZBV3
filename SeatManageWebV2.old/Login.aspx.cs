using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;

namespace SeatManageWebV2
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string cmd = Request.Form["subCmd"];
            if (Request.Cookies["userInfo"] != null)//存在记录的cookies信息
            {
                string loginId = CookiesManager.GetCookiesValue(CookiesManager.LoginID);
                SeatManage.ClassModel.UserInfo LoginUser = GetUserInfo(loginId);
                Session[CookiesManager.LoginID] = LoginUser.LoginId;
                Session[CookiesManager.Name] = LoginUser.UserName;
                Response.Redirect("Pad/SeatPad.aspx");
            }
            //通过页面验证，执行登录操作
            if (cmd == "Login")
            {
                string loginId = txt_LoginID.Value;
                string password = txt_Password.Value;
                SeatManage.Bll.Users_ALL userinfocheck = new SeatManage.Bll.Users_ALL();
                try
                {
                    loginId = userinfocheck.CheckUser(loginId, password);
                    //判断返回信息是否为空
                    if (string.IsNullOrEmpty(loginId))
                    {
                        spanWarmInfo.Visible = true;
                        spanWarmInfo.InnerText = "用户名或密码错误";
                    }
                    else
                    {
                        SeatManage.ClassModel.UserInfo LoginUser = GetUserInfo(loginId);
                        Session[CookiesManager.LoginID] = LoginUser.LoginId;
                        Session[CookiesManager.Name] = LoginUser.UserName;
                        if (LoginUser.UserType == SeatManage.EnumType.UserType.Admin)
                        {
                            //若选择记住密码，则记录用户信息cookies
                            if (chk_RemPasspword.Checked == true)
                            {
                                CookiesManager.SetPadCookies(loginId, password);
                            }
                            else//清除cookies
                            {
                                CookiesManager.RemoveCookies("userInfo");
                            }
                            Response.Redirect("Pad/SeatPad.aspx");
                        }
                        else
                        {
                            spanWarmInfo.Visible = true;
                            spanWarmInfo.InnerText = "您不具备访问权限";
                        }
                    }
                }
                catch (Exception ex)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "数据库连接出错";
                }
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        private SeatManage.ClassModel.UserInfo GetUserInfo(string loginid)
        {
            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(loginid);
            if (user != null)
            {
                user.ReloID = SeatManage.Bll.Users_ALL.GetRoleID(loginid);
                user.UserRoomRight = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginid);
            }
            return user;
        }
    }

}