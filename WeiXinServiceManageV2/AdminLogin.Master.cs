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
    public partial class AdminLogin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Login"] == null)
            {
                Response.Redirect("AdminLoging.aspx");
            }
            if (!IsPostBack)
            {
                if (Session["Login"] == null)
                {
                    Response.Write("<script>window.location.href='AdminLoging.aspx'</script>");
                    return;
                }
                AMS_UserInfo userinfo = Session["Login"] as AMS_UserInfo;
                this.lblName.Text = userinfo.UserName;
                this.lblReaske.Text = userinfo.Remark;
            }
           
        }
    }
}