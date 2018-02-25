using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class LibraryEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "LibraryInfo.aspx" && pageName != "FormSYS.aspx")
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
                DataBind();
            }
        }
        /// <summary>
        /// 提交设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.LibraryInfo newlibrary = new SeatManage.ClassModel.LibraryInfo();
            newlibrary.No = txtLibraryNo.Text.Trim();
            newlibrary.Name = txtLibraryName.Text.Trim();
            newlibrary.School.No = ddlschool.SelectedValue;
            if (txtArea.Text.Trim() != "")
            {
                string[] areaList = txtArea.Text.Split(';');
                for (int i = 0; i < areaList.Length; i++)
                {
                    if (areaList[i].Trim() != "")
                    {
                        SeatManage.ClassModel.AreaInfo Area = new SeatManage.ClassModel.AreaInfo();
                        Area.AreaName = areaList[i];
                        Area.AreaNo = i;
                        newlibrary.AreaList.Add(Area);
                    }
                }
            }
            List<SeatManage.ClassModel.LibraryInfo> librarylist = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, null, null);
            if (Request.QueryString["flag"] == "add")
            {
                foreach (SeatManage.ClassModel.LibraryInfo library in librarylist)
                {
                    if (library.No == newlibrary.No || library.Name == newlibrary.Name)
                    {
                        FineUI.Alert.ShowInTop("图书馆编号或图书馆名称已存在，请重新输入！");
                        return;
                    }
                }
                if (SeatManage.Bll.T_SM_Library.AddNewLibrary(newlibrary))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("图书馆添加成功！");
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("图书馆添加失败！");
                }
            }
            else if (Request.QueryString["flag"] == "edit")
            {
                foreach (SeatManage.ClassModel.LibraryInfo library in librarylist)
                {
                    if (library.No != newlibrary.No && library.Name == newlibrary.Name)
                    {
                        FineUI.Alert.ShowInTop("图书馆名称已存在，请重新输入！");
                        return;
                    }
                }
                if (SeatManage.Bll.T_SM_Library.UpdataLibraryInfo(newlibrary))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("图书馆修改成功！");
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("图书馆修改失败！");
                }
            }
        }
        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtLibraryNo.Text = "";
            txtLibraryName.Text = "";
            ddlschool.Items.Clear();
            DataBind();

        }
        /// <summary>
        /// 绑定编辑的值
        /// </summary>
        private void DataBind()
        {
            List<SeatManage.ClassModel.School> schoollist = SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, null);
            foreach (SeatManage.ClassModel.School school in schoollist)
            {
                ddlschool.Items.Add(new FineUI.ListItem(school.Name, school.No));
            }
            if (Request.QueryString["flag"] == "edit" && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                List<SeatManage.ClassModel.LibraryInfo> library = SeatManage.Bll.T_SM_Library.GetLibraryInfoList(null, Request.QueryString["id"], null);
                if (library.Count > 0)
                {
                    txtLibraryNo.Text = library[0].No;
                    txtLibraryNo.Readonly = true;
                    txtLibraryNo.Enabled = false;
                    txtLibraryName.Text = library[0].Name;
                    ddlschool.SelectedValue = library[0].School.No;
                    foreach (SeatManage.ClassModel.AreaInfo area in library[0].AreaList)
                    {
                        txtArea.Text += area.AreaName + ";";
                    }
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
                }
            }
        }
    }
}