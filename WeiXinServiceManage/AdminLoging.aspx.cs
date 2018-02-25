using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS.Model;
using AMS.ServiceProxy;

namespace WeiXinServiceManage
{
    public partial class AdminLoging : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string userno = txtName.Text.Trim();
            string userpwd = txtPwd.Text.Trim();
            
            AMS_UserInfo userinfo =new AMS_UserInfo ();
            try
            {
                userinfo = LoginService.Login(userno, userpwd);
            }
            catch
            {
                Response.Redirect("ErrorPage.aspx");
            }
            if (userinfo==null)
            {
                this.Label2.Text ="账号或密码错误";
                return;
            }
            if (userinfo.Remark=="广告管理员")
            {
                Session["Login"] = userinfo;
                Response.Redirect("AdvertiseFrom.aspx");
                return;
            }
            if (userinfo.Remark == "回复管理员")
            {
                Session["Login"] = userinfo;
                Response.Redirect("TextsConfig.aspx");
                return;
            }
            this.Label2.Text = "别闹，你不属于微信管理员";
        }
    }
}