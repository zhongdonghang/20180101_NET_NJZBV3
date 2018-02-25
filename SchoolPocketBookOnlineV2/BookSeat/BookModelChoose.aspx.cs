using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.BookSeat
{
    public partial class BookModelChoose :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            if (!this.LoginUserInfo.PecketWebSetting.UseBookNowDaySeat)
            {
                btn_NowDay.Visible = false;
            }
            if (!this.LoginUserInfo.PecketWebSetting.UseBookNextDaySeat)
            {
                btn_NextDay.Visible = false;
            }
            
        }
    }
}