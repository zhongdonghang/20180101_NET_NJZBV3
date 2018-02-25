using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.UsersManage
{
    public partial class ChangePassword : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "FormSYS.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
                if (string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
                }
            }
        }
        /// <summary>
        /// 保存密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(Request.QueryString["id"]);
            if (user.Password != SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(txtPassword_old.Text))
            {
                FineUI.Alert.ShowInTop("原密码错误，请重新输入！");
                return;
            }
            if (txtPassword.Text != txtPassword_d.Text)
            {
                FineUI.Alert.ShowInTop("新密码两次输入不一致，请重新输入！");
                return;
            }
            user.Password = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(txtPassword.Text);
            if (SeatManage.Bll.Users_ALL.UpdateUserOnlyInfo(user))
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("密码更新成功！");
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("密码更新失败！");
            }
        }
    }
}