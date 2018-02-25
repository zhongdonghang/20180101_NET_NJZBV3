using System;
using SeatManage.EnumType;

namespace WeiXinPocketBookOnline
{
    public partial class MainFunctionPage : BasePage
    {
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();

        public string cmd;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null || UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            DataBind();
            spanWarmInfo.InnerText = "";
            spanWarmInfo.Visible = false;
            cmd = Request.Form["subCmd"];

            switch (cmd)
            {
                case "LoginOut":
                    Session.Clear();
                    Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                    SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                    string LogOutUrl = LogoutUrl();
                    if (string.IsNullOrEmpty(LogOutUrl))
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        Response.Redirect(LogOutUrl);
                    }

                    break;
            }
        }

        public override void DataBind()
        {
            LoginUserInfo = weiXinService.GetUserInfo_WeiXin(LoginUserInfo.StudentNo, UserSchoolInfo.SchoolNo);
            if (!LoginUserInfo.AjmPecketBookSetting.UseBookSeat)
            {
                btn_book.Visible = false;
            }
            if (!LoginUserInfo.AjmPecketBookSetting.UseWaitSeat)
            {
                btn_WaitSeat.Visible = false;
            }
        }
    }
}