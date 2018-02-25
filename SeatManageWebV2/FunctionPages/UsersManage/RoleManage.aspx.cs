using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using FineUI;

namespace SeatManageWebV2.FunctionPages.UsersManage
{
    public partial class RoleManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                BindRoleGrid();
                btnAddRole.OnClientClick = WindowEdit.GetShowReference("RoleEdit.aspx?flag=add", "添加角色");//为btnAddMenu新增点击事件
            }
        }

        /// <summary>
        /// 绑定角色Grid
        /// </summary>
        protected void BindRoleGrid()
        {
            List<SeatManage.ClassModel.SysRolesDicInfo> listSysRolesDicInfo = new List<SeatManage.ClassModel.SysRolesDicInfo>();
            listSysRolesDicInfo = SeatManage.Bll.SysRolesDic.GetRoleList(null, null);
            DataTable dt = new DataTable();
            DataColumn ROLEID = new DataColumn("ROLEID", typeof(int));
            DataColumn ROLENAME = new DataColumn("ROLENAME", typeof(string));
            DataColumn IsLock = new DataColumn("IsLock", typeof(bool));
            dt.Columns.Add(ROLEID);
            dt.Columns.Add(ROLENAME);
            dt.Columns.Add(IsLock);
            foreach (SeatManage.ClassModel.SysRolesDicInfo list in listSysRolesDicInfo)
            {
                DataRow row = dt.NewRow();
                row["ROLEID"] = list.RoleID;
                row["ROLENAME"] = list.RoleName;
                row["IsLock"] = list.IsLock;
                dt.Rows.Add(row);
            }
            GridRole.DataSource = dt;
            GridRole.DataBind();
        }
        //行预绑定事件
        protected void GridRole_PreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;
            string roleId = row[0].ToString();
            string roleName = Server.UrlEncode(row[1].ToString());
            LinkButtonField lnkbtnField = GridRole.FindColumn("lnkbtnEdit") as LinkButtonField;
            LinkButtonField lnkbtnColDel = GridRole.FindColumn("ColDel") as LinkButtonField;
            lnkbtnField.OnClientClick = WindowEdit.GetShowReference("RoleEdit.aspx?flag=edit&roleId=" + roleId + "&roleName=" + roleName, "修改角色");
            FineUI.CheckBoxField cbxField = GridRole.FindColumn("CheckBoxField1") as FineUI.CheckBoxField;
            if (row[2].ToString() == "True")
            {
                lnkbtnField.Enabled = false;
                lnkbtnField.Icon = Icon.Lock;
                lnkbtnField.ToolTip = "不可编辑";
                lnkbtnColDel.Enabled = false;
                lnkbtnColDel.Icon = Icon.Lock;
                lnkbtnColDel.ToolTip = "不可删除";
            }
            else
            {
                lnkbtnField.Enabled = true;
                lnkbtnField.Icon = Icon.Pencil;
                lnkbtnField.ToolTip = "编辑";
                lnkbtnColDel.Enabled = true;
                lnkbtnColDel.Icon = Icon.Delete;
                lnkbtnColDel.ToolTip = "删除";
            }
        }


        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindRoleGrid();
        }

        //删除菜单按钮点击事件
        protected void btnDel_Click(object sender, EventArgs e)
        {
            FineUI.CheckBoxField chkFild = (FineUI.CheckBoxField)GridRole.FindColumn("CheckBoxField1");
            SeatManage.ClassModel.SysRolesDicInfo modelSysRolesDicInfo = new SeatManage.ClassModel.SysRolesDicInfo();
            SeatManage.Bll.SysRolesDic bllSysRolesDic = new SeatManage.Bll.SysRolesDic();
            int selectCount = GridRole.SelectedRowIndexArray.Length;
            if (selectCount > 0)
            {
                for (int i = 0; i < selectCount; i++)
                {
                    int rowIndex = GridRole.SelectedRowIndexArray[i];
                    FineUI.GridRow row = GridRole.Rows[rowIndex] as FineUI.GridRow;
                    modelSysRolesDicInfo.RoleID = row.DataKeys[0].ToString();
                    if (bllSysRolesDic.DeleteRole(modelSysRolesDicInfo))
                    {
                        FineUI.Alert.ShowInTop("删除成功！");
                    }
                    else
                    {
                        FineUI.Alert.ShowInTop("删除失败！");
                    }
                }
                BindRoleGrid();
            }
        }
        //删除列点击事件
        protected void GridRole_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.SysRolesDicInfo modelSysRolesDicInfo = new SeatManage.ClassModel.SysRolesDicInfo();
                SeatManage.Bll.SysRolesDic bllSysRolesDic = new SeatManage.Bll.SysRolesDic();
                modelSysRolesDicInfo.RoleID = GridRole.Rows[e.RowIndex].DataKeys[0].ToString();
                if (!bllSysRolesDic.DeleteRole(modelSysRolesDicInfo))
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除成功！");
                    BindRoleGrid();
                }
            }
        }
    }
}