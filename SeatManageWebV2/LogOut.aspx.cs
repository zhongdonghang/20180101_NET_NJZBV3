using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2
{
    public partial class LogOut : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region 赋值
            Session["SystemID"] = null;
            Session["UserName"] = null;
            Session["PersonName"] = null;
            this.LoginId = "";
            Session["AgentStatus"] = null;
            Session["RootDeptID"] = null;
            Session["RangeID"] = null;
            Session["UserDeptID"] = null;
            Session["OldDeptID"] = null;//默认当前部门，用于部门树
            Session["UserDeptName"] = null;
            Session["UserOrgID"] = null;    //机构ID
            Session["UserOrgName"] = null;
            Session["CONONAME"] = null;
            Session["RootDeptName"] = null;
            Session["UserAllRights"] = null;
            Session["Themes"] = null;
            Session["MainUrl"] = null;
            #endregion
            Response.Redirect(this.LoginPage);
            //Epower.DevBase.BaseTools.JscriptTool.GotoParentWindow("../Default.aspx?Logout=1");
        }
    }
}