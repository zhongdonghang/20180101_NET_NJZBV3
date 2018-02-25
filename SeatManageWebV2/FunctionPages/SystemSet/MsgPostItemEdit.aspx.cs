using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.UsersManage
{
    public partial class MsgPostItemEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["flag"] == "edit")
            {
                if (!Page.IsPostBack)
                {
                    string userName = Request.QueryString["userName"];
                    EditContextShow(userName);
                }
            }
            else
            {
                txtUserName.Enabled = true;
            }
        }

        public void EditContextShow(string userName)
        {
            SeatManage.ClassModel.MsgPostSet mps = SeatManage.Bll.T_SM_SystemSet.GetMsgPostSet();
            foreach (SeatManage.ClassModel.MsgPostItem item in mps.PostItems)
            {
                if (item.UserName == userName)
                {
                    txtUserName.Text = item.UserName;
                    txtAppId.Text = item.AppID;
                    txtUrl.Text = item.PostUrl;
                    break;
                }
            }
        }
        //提交按钮点击事件
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            SeatManage.ClassModel.MsgPostSet mps = SeatManage.Bll.T_SM_SystemSet.GetMsgPostSet();

            if (Request.QueryString["flag"] == "edit")
            {
                foreach (SeatManage.ClassModel.MsgPostItem item in mps.PostItems)
                {
                    if (item.UserName == txtUserName.Text)
                    {
                        item.AppID = txtAppId.Text;
                        item.PostUrl = txtUrl.Text;
                        break;
                    }
                }
            }
            else if (Request.QueryString["flag"] == "add")
            {
                if (mps == null)
                {
                    mps = new SeatManage.ClassModel.MsgPostSet();
                }
                IAuthorizeVerify verify = new WebAuthorizeVerify();
                if (verify.Verify(txtUserName.Text, txtPwd.Text, "Other", "PostMsg"))
                {
                    SeatManage.ClassModel.MsgPostItem item = new SeatManage.ClassModel.MsgPostItem();
                    item.UserName = txtUserName.Text;
                    item.PostUrl = txtUrl.Text;
                    item.AppID = txtAppId.Text;
                    mps.PostItems.Add(item);

                }
                else
                {
                    FineUI.Alert.ShowInTop("授权验证失败！");
                    return;
                }
            }

            if (SeatManage.Bll.T_SM_SystemSet.SaveMsgPostSet(mps))
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("添加成功！");
            }
            else
            {
                FineUI.Alert.ShowInTop("操作失败！");
            }
        }
    }
}