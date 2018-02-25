/******************************************
 * 作者：罗晨阳
 * 创建时间：2013-6-6
 * 说明：功能菜单新增、编辑
 * 修改人：
 * 修改时间：
 * ******************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SystemSet
{
    public partial class MenuEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "MenuManage.aspx" && pageName != "FormSYS.aspx")
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
                if (!Page.IsPostBack)
                {
                    EditMenuShow();
                }
            }
        }
        /// <summary>
        /// 绑定主菜单下拉列表
        /// </summary>
        private void BindDdlMainNum()
        {
            List<SeatManage.ClassModel.SysMenuInfo> listMainNum = SeatManage.Bll.SysMenu.GetMenusList();
            if (listMainNum != null)
            {
                ddlMainNum.DataTextField = "MenuName";
                ddlMainNum.DataValueField = "MainNum";
                ddlMainNum.DataSource = listMainNum;
                ddlMainNum.DataBind();
            }

        }
        /// <summary>
        /// 绑定功能页
        /// </summary>
        private void BindDdlFuncDicpage()
        {
            SeatManage.Bll.SysFuncDic bllSysFuncDic = new SeatManage.Bll.SysFuncDic();
            List<SeatManage.ClassModel.SysFuncDicInfo> listSysFuncDicInfo = bllSysFuncDic.GetFuncPage(null, null);
            if (listSysFuncDicInfo != null)
            {
                ddlFunciPage.DataTextField = "Name";
                ddlFunciPage.DataValueField = "No";
                ddlFunciPage.DataSource = listSysFuncDicInfo;
                ddlFunciPage.DataBind();
            }
        }
        /// <summary>
        /// 菜单级别选择事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlMenuLv_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            string lv = ddlMenuLv.SelectedValue;
            if (lv == "first")
            {
                ddlMainNum.Hidden = true;
                ddlFunciPage.Hidden = true;
                txtMenuNum.Hidden = false;
                txtMenuNum.Enabled = true;
                txtMenuNum.Required = true;
            }
            else
            {

                ddlMainNum.Hidden = false;
                ddlFunciPage.Hidden = false;
                txtMenuNum.Required = false;
                txtMenuNum.Enabled = false;
                txtMenuNum.Hidden = true;
                BindDdlMainNum();
                BindDdlFuncDicpage();
            }
        }
        /// <summary>
        /// 加载菜单编辑数据
        /// </summary>
        protected void EditMenuShow()
        {
            string menuId = Request.QueryString["MenuID"];
            List<SeatManage.ClassModel.SysMenuInfo> listSysMenuInfo = SeatManage.Bll.SysMenu.GetMenusList(menuId);
            SeatManage.ClassModel.SysMenuInfo modelSysMenuInfo = new SeatManage.ClassModel.SysMenuInfo();
            if (listSysMenuInfo != null && listSysMenuInfo.Count > 0)
            {
                txtMenuName.Text = listSysMenuInfo[0].MenuName;
                NumberBoxSort.Text = listSysMenuInfo[0].Index.ToString();
                txtMenuNum.Text = listSysMenuInfo[0].MainNum;
                if (listSysMenuInfo[0].FuncPageNum == "0000")
                {
                    ddlMainNum.Hidden = true;
                    ddlFunciPage.Hidden = true;
                    txtMenuNum.Hidden = false;
                    ddlMenuLv.SelectedValue = "first";
                }
                else
                {
                    BindDdlMainNum();
                    BindDdlFuncDicpage();
                    txtMenuNum.Hidden = true;
                    txtMenuNum.Required = false;
                    ddlMainNum.Hidden = false;
                    ddlMainNum.SelectedValue = listSysMenuInfo[0].MainNum;
                    ddlFunciPage.Hidden = false;
                    ddlFunciPage.SelectedValue = listSysMenuInfo[0].FuncPageNum;
                    ddlMenuLv.SelectedValue = "second";
                }
                ddlMenuLv.Enabled = false;
            }
            else
            {
                FineUI.Alert.Show("菜单已删除");
                btnSubmit.Enabled = false;
            }
        }
        //提交按钮点击事件
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string flag = Request.QueryString["flag"];
            string modSeq = "";
            string menuName = txtMenuName.Text;
            int index = Convert.ToInt32(NumberBoxSort.Text);
            string mainNum = "";
            int menuLv = 0;
            if (ddlMenuLv.SelectedValue == "first")
            {
                mainNum = txtMenuNum.Text;
                menuLv = 1;
                modSeq = "0000";
            }
            else
            {
                mainNum = ddlMainNum.SelectedValue;
                menuLv = 2;
                modSeq = ddlFunciPage.SelectedValue;
            }
            SeatManage.ClassModel.SysMenuInfo modelSysMenu = new SeatManage.ClassModel.SysMenuInfo();
            modelSysMenu.MainNum = mainNum;
            modelSysMenu.MenuName = menuName;
            modelSysMenu.MenuLv = menuLv;
            modelSysMenu.Index = index;
            modelSysMenu.FuncPageNum = modSeq;
            switch (flag)
            {
                //新增菜单
                case "add":
                    if (SeatManage.Bll.SysMenu.AddMenus(modelSysMenu))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("添加成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("添加失败！");
                    }
                    break;
                //修改菜单
                case "edit":
                    modelSysMenu.ImageUrl = DBNull.Value.ToString();
                    modelSysMenu.MenuID = Convert.ToInt32(Request.QueryString["MenuID"]);
                    if (SeatManage.Bll.SysMenu.UpdateMenus(modelSysMenu))
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

        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtMenuName.Text = "";
            txtMenuNum.Text = "";
            NumberBoxSort.Text = "";
            ddlMainNum.SelectedIndex = 0;
            ddlFunciPage.SelectedIndex = 0;
            ddlMenuLv.SelectedIndex = 0;
        }
    }
}