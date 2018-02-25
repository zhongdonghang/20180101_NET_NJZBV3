using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class FunctionPageEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "FunctionPagesManage.aspx" && pageName != "FormSYS.aspx")
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
            }
            if (Request.QueryString["flag"] == "edit")
            {
                txtModSeq.Readonly = true;
                txtModSeq.Enabled = false;

                if (!Page.IsPostBack)
                {
                    EditFuncDicShow();
                }
            }
            else
            {
                txtModSeq.Readonly = false;
            }
        }
        //
        protected void EditFuncDicShow()
        {
            string modSeq = Request.QueryString["ModSeq"];//功能菜单编号
            SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
            List<SeatManage.ClassModel.SysFuncDicInfo> listSysFuncDic = new List<SeatManage.ClassModel.SysFuncDicInfo>();
            listSysFuncDic = bllSysFuncDic.GetFuncPage(null, modSeq);
            if (listSysFuncDic != null)
            {
                txtModSeq.Text = listSysFuncDic[0].No;
                txtModSeq.Readonly = true;
                txtMCaption.Text = listSysFuncDic[0].Name;
                txtMenuLink.Text = listSysFuncDic[0].PageUrl;
                txtOrderSeq.Text = listSysFuncDic[0].Order;
            }
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string flag = Request.QueryString["flag"];
            SeatManage.ClassModel.SysFuncDicInfo modelSysFuncDic = new SeatManage.ClassModel.SysFuncDicInfo();
            SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
            modelSysFuncDic.Name = txtMCaption.Text;
            modelSysFuncDic.PageUrl = txtMenuLink.Text;
            modelSysFuncDic.Order = txtOrderSeq.Text;
            switch (flag)
            {
                //新增功能页
                case "add":
                    modelSysFuncDic.No = txtModSeq.Text;
                    if (string.IsNullOrEmpty(bllSysFuncDic.AddNewFuncPage(modelSysFuncDic)))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("添加成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("添加失败！");
                    }
                    break;
                //修改功能页
                case "edit":
                    modelSysFuncDic.No = txtModSeq.Text;
                    if (bllSysFuncDic.UpdateFuncPage(modelSysFuncDic))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("修改成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("修改失败！");
                    }
                    break;
                default:
                    FineUI.Alert.ShowInTop("未执行任何操作");
                    break;
            }
        }
    }
}