using System;

namespace SchoolPocketBookWeb.BookSeat
{
    public partial class BookModelChoose :BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            if (!LoginUserInfo.PecketWebSetting.UseBookNowDaySeat)
            {
                btn_NowDay.Visible = false;
            }
            if (!LoginUserInfo.PecketWebSetting.UseBookNextDaySeat)
            {
                btn_NextDay.Visible = false;
            }
            
        }
    }
}