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
    public partial class UserInfo : BasePage
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
                btnAddUser.OnClientClick = WindowEdit.GetShowReference("UserEdit.aspx?flag=add", "添加用户");
                BindGrid();
            }
        }
        /// <summary>
        /// 验证页面权限
        /// </summary>
        private void IsloginRole()
        {
            //if (string.IsNullOrEmpty(this.LoginId))
            //{

            //}
            List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = SeatManage.Bll.SysMenu.GetUserMenus(this.LoginId);
            bool isrole = false;
            foreach (SeatManage.ClassModel.SysMenuInfo menu in listSysMenu)
            {
                foreach (SeatManage.ClassModel.SysMenuInfo cmenu in menu.ChildMenu)
                {
                    if ("~/" + cmenu.MenuLink == this.Page.AppRelativeVirtualPath)
                    {
                        isrole = true;
                        break;
                    }
                }
            }
            if (!isrole)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('您没有权限访问此页面，请跟管理员联系！');location='../../default.aspx';", true);
            }
        }
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void BindGrid()
        {
            string sortField = UsersGrid.Columns[UsersGrid.SortColumnIndex].SortField;
            string sortDirection = UsersGrid.SortDirection;
            DataTable table = GetUserInfoDateTable();
            DataView TableView = table.DefaultView;
            TableView.Sort = String.Format("{0} {1}", sortField, sortDirection);
            UsersGrid.DataSource = TableView;
            UsersGrid.DataBind();
        }
        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        private DataTable GetUserInfoDateTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("用户姓名", typeof(string));
            dt.Columns.Add("用户登录ID", typeof(string));
            dt.Columns.Add("用户类型", typeof(string));
            dt.Columns.Add("用户状态", typeof(bool));
            dt.Columns.Add("备注", typeof(string));
            List<SeatManage.ClassModel.UserInfo> userlist = SeatManage.Bll.Users_ALL.GetUsers();
            foreach (SeatManage.ClassModel.UserInfo user in userlist)
            {
                if ((SeatManage.EnumType.UserType)int.Parse(ddlselectType.SelectedValue) != SeatManage.EnumType.UserType.None && user.UserType != (SeatManage.EnumType.UserType)int.Parse(ddlselectType.SelectedValue))
                {
                    continue;
                }
                if ((SeatManage.EnumType.LogStatus)int.Parse(ddlselectIsUsed.SelectedValue) != SeatManage.EnumType.LogStatus.None && user.IsUsing != (SeatManage.EnumType.LogStatus)int.Parse(ddlselectIsUsed.SelectedValue))
                {
                    continue;
                }
                DataRow dr = dt.NewRow();
                dr["用户姓名"] = user.UserName;
                dr["用户登录ID"] = user.LoginId;
                switch (user.UserType)
                {
                    case SeatManage.EnumType.UserType.Admin: dr["用户类型"] = "管理员"; break;
                    case SeatManage.EnumType.UserType.Reader: dr["用户类型"] = "读者"; break;
                    case SeatManage.EnumType.UserType.None: dr["用户类型"] = "未知"; break;
                }
                dr["用户状态"] = user.IsUsing;
                dr["备注"] = user.Remark;
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 页面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UsersGrid_PageIndexChange(object sender, FineUI.GridPageEventArgs e)
        {
            UsersGrid.PageIndex = e.NewPageIndex;
        }
        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UsersGrid_Sort(object sender, FineUI.GridSortEventArgs e)
        {
            UsersGrid.SortDirection = e.SortDirection;
            UsersGrid.SortColumnIndex = e.ColumnIndex;
            BindGrid();
        }
        /// <summary>
        /// 删除事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UsersGrid_RowCommand(object sender, FineUI.GridCommandEventArgs e)
        {
            if (e.CommandName == "ActionDelete")
            {
                SeatManage.ClassModel.UserInfo user = new SeatManage.ClassModel.UserInfo();
                user.LoginId = UsersGrid.Rows[e.RowIndex].DataKeys[0].ToString();
                if (!SeatManage.Bll.Users_ALL.DeleteUser(user))
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("删除完成！");
                    BindGrid();
                }
            }
        }
        /// <summary>
        /// 编辑窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void WindowEdit_Close(object sender, FineUI.WindowCloseEventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 分类下拉框更变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void selectType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 分类下拉框更变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void selectIsUsed_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelectDelete_Click(object sender, EventArgs e)
        {
            int[] selectindex = UsersGrid.SelectedRowIndexArray;
            foreach (int index in selectindex)
            {
                SeatManage.ClassModel.UserInfo user = new SeatManage.ClassModel.UserInfo();
                user.LoginId = UsersGrid.Rows[index].DataKeys[0].ToString();
                if (user.LoginId == "admin" || user.LoginId == "user" || user.LoginId == "reader")
                {
                    FineUI.Alert.ShowInTop("默认用户\"" + user.LoginId + "\"无法删除！");
                    return;
                }
                if (!SeatManage.Bll.Users_ALL.DeleteUser(user))
                {
                    FineUI.Alert.ShowInTop("删除失败！");
                    return;
                }
            }
            FineUI.Alert.ShowInTop("删除完成！");
            BindGrid();
        }
        protected void UsersGrid_OnPreRowDataBound(object sender, FineUI.GridPreRowEventArgs e)
        {
            LinkButtonField lbf = UsersGrid.FindColumn("userdelete") as LinkButtonField;
            LinkButtonField wf = UsersGrid.FindColumn("useredit") as LinkButtonField;
            DataRowView row = e.DataItem as DataRowView;
            string loginid = row[1].ToString();
            string usertype = row[2].ToString();
            if (loginid == "admin" || loginid == "user" || loginid == "reader")
            {
                lbf.Enabled = false;
                lbf.Icon = FineUI.Icon.Lock;
                lbf.ToolTip = "默认用户锁定操作";
                wf.Enabled = false;
                wf.Icon = FineUI.Icon.Lock;
                wf.ToolTip = "默认用户锁定操作";
            }
            else
            {
                lbf.Enabled = true;
                lbf.Icon = FineUI.Icon.Delete;
                lbf.ToolTip = "删除用户";
                //if (usertype == "读者")
                //{
                //    wf.Enabled = false;
                //    wf.Icon = FineUI.Icon.Lock;
                //    wf.ToolTip = "读者锁定操作";
                //}
                //else
                //{
                wf.Enabled = true;
                wf.Icon = FineUI.Icon.Pencil;
                wf.ToolTip = "编辑用户";
                wf.OnClientClick = WindowEdit.GetShowReference("UserEdit.aspx?flag=edit&id=" + loginid + "", "用户编辑");
                //}
            }
        }
    }
}