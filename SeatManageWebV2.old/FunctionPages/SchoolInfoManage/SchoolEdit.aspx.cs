using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.SchoolInfoManage
{
    public partial class SchoolEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "SchoolInfo.aspx" && pageName != "FormSYS.aspx")
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
                if (Request.QueryString["flag"] == "edit" && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    DataBind();
                }
            }
        }
        /// <summary>
        /// 提交设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.School newschool = new SeatManage.ClassModel.School();
            newschool.No = txtSchoolNo.Text.Trim();
            newschool.Name = txtSchoolName.Text.Trim();
            List<SeatManage.ClassModel.School> schoollist = SeatManage.Bll.T_SM_School.GetSchoolInfoList(null, null);
            if (Request.QueryString["flag"] == "add")
            {
                foreach (SeatManage.ClassModel.School school in schoollist)
                {
                    if (school.No == newschool.No || school.Name == newschool.Name)
                    {
                        FineUI.Alert.ShowInTop("校区编号或学校名称已存在，请重新输入！");
                        return;
                    }
                }
                if (SeatManage.Bll.T_SM_School.AddNewSchool(newschool))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("校区添加成功！");
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("校区添加失败！");
                }
            }
            else if (Request.QueryString["flag"] == "edit")
            {
                foreach (SeatManage.ClassModel.School school in schoollist)
                {
                    if (school.No != newschool.No && school.Name == newschool.Name)
                    {
                        FineUI.Alert.ShowInTop("学区名称已存在，请重新输入！");
                        return;
                    }
                }
                if (SeatManage.Bll.T_SM_School.UpdataSchoolInfo(newschool))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("校区修改成功！");
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("校区修改失败！");
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
            txtSchoolNo.Text = "";
            txtSchoolName.Text = "";
            if (Request.QueryString["flag"] == "edit" && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                DataBind();
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
            }
        }
        /// <summary>
        /// 绑定编辑的值
        /// </summary>
        private new void DataBind()
        {
            List<SeatManage.ClassModel.School> school = SeatManage.Bll.T_SM_School.GetSchoolInfoList(Request.QueryString["id"], null);
            if (school.Count > 0)
            {
                txtSchoolNo.Text = school[0].No;
                txtSchoolNo.Readonly = true;
                txtSchoolNo.Enabled = false;
                txtSchoolName.Text = school[0].Name;
            }
            else
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
            }
        }
    }
}