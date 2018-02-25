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
    public partial class UpdateResponse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        public override void DataBind()
        {
            if (Request.QueryString["id"] != null)
            {
                WeiXinResponse type = WeiXinProxy.GetResponseById(Convert.ToInt32(Request.QueryString["id"]));
                txttext.Text = type.Text;
                this.ddlType.SelectedValue = type.Type.ToString();
              
            }
        }

        protected void btnBIn_Click(object sender, EventArgs e)
        {
            if (ddlType.SelectedValue == "-1")
            {
                lbltxt.Text = "请选择一个分类";
                return;
            }
            if (txttext.Text.Trim() == "")
            {
                lbltxt.Text = "输入内容不能为空";
                return;
            }
            if (Request.QueryString["id"] != null)
            {
                AMS_UserInfo userinfo = Session["Login"] as AMS_UserInfo;
                WeiXinResponse wxr = new WeiXinResponse
                {
                    AddDateTime = DateTime.Now,
                    IsUsed = 1,
                    LoginID = new AMS_UserInfo { ID = userinfo.ID },
                    Text = txttext.Text,
                    Type = Convert.ToInt32(ddlType.SelectedValue)
                };
                wxr.ID = Convert.ToInt32(Request.QueryString["id"]);
                if (WeiXinProxy.UpdateResponse(wxr))
                {

                    Response.Write("<script>alert('修改成功!');window.location.href ='TextsConfig.aspx'</script>");
                }
                else
                {

                    lbltxt.Text = "修改失败";
                }
            }
            else
            {
                AMS_UserInfo userinfo = Session["Login"] as AMS_UserInfo;
                WeiXinResponse wxr = new WeiXinResponse
                {
                    AddDateTime = DateTime.Now,
                    IsUsed = 1,
                    LoginID = new AMS_UserInfo { ID = userinfo.ID },
                    Text = txttext.Text,
                    Type = Convert.ToInt32(ddlType.SelectedValue)
                };
                WeiXinProxy.UpResponstype(wxr.Type);
                if (WeiXinProxy.AddResponse(wxr))
                 {
                    Response.Write("<script>window.location.href ='TextsConfig.aspx'</script>");
                }
                else
                {

                    lbltxt.Text = "添加失败";
                }
            }
            DataBind();
        }
    }
}