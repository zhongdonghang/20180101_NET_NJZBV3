using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;
using SeatManage.ClassModel;

namespace SeatManageWebV2.FunctionPages.UsersManage
{
    public partial class UserEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "UserInfo.aspx" && pageName != "FormSYS.aspx")
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
                if (Request.QueryString["flag"] == "add")
                {
                    GetRole();
                }
                else if (Request.QueryString["flag"] == "edit" && !string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    GetRole();
                    ShowUserInfo();
                }
                else
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("信息获取失败！请重新打开！");
                }
            }
        }
        /// <summary>
        /// 保存设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["flag"] == "add")
            {
                AddUser();
            }
            else if (Request.QueryString["flag"] == "edit" && !string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                UpdateUser();
            }
        }
        /// <summary>
        /// 添加用户
        /// </summary>
        private void AddUser()
        {
            if (!string.IsNullOrEmpty(txtLoginID.Text) && !string.IsNullOrEmpty(txtPassword.Text) && !string.IsNullOrEmpty(txtPassword_d.Text) && (txtPassword.Text == txtPassword_d.Text))
            {
                if (SeatManage.Bll.Users_ALL.GetUserInfo(txtLoginID.Text.Trim()) != null)
                {
                    FineUI.Alert.Show("输入的用户名重复，请重新输入！");
                }
                else
                {
                    SeatManage.ClassModel.UserInfo user = new SeatManage.ClassModel.UserInfo();
                    user.LoginId = txtLoginID.Text.Trim();
                    user.Password = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(txtPassword.Text.Trim());
                    user.Remark = txtRemark.Text.Trim();
                    user.UserName = txtUserName.Text.Trim();
                    if (clbused.Checked)
                    {
                        user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                    }
                    else
                    {
                        user.IsUsing = SeatManage.EnumType.LogStatus.Fail;
                    }
                    user.UserRoomRight.LoginID = user.LoginId;
                    user.UserType = SeatManage.EnumType.UserType.Admin;
                    user.UserRoomRight.RightRoomList.Clear();
                    foreach (FineUI.CheckItem item in clbroom.Items)
                    {
                        if (item.Selected)
                        {
                            user.UserRoomRight.RightRoomList.Add(new SeatManage.ClassModel.ReadingRoomInfo() { No = item.Value });
                        }
                    }
                    user.ReloID.Clear();
                    foreach (FineUI.CheckItem item in clbRole.Items)
                    {
                        if (item.Selected)
                        {
                            user.ReloID.Add(int.Parse(item.Value));
                        }
                    }
                    if (SeatManage.Bll.Users_ALL.AddNewUser(user))
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("用户添加成功！");
                    }
                    else
                    {
                        FineUI.Alert.Show("数据错误添加失败！");
                    }
                }
            }
            else
            {
                FineUI.Alert.Show("信息输入有误或不完整，请核对输入信息！");
            }
        }
        /// <summary>
        /// 更新读者
        /// </summary>
        private void UpdateUser()
        {
            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(Request.QueryString["id"]);
            user.UserRoomRight = new SeatManage.ClassModel.ManagerPotency();
            user.ReloID = new List<int>();
            if (!string.IsNullOrEmpty(txtPassword.Text.Trim()) || !string.IsNullOrEmpty(txtPassword_d.Text.Trim()))
            {
                if (txtPassword.Text.Trim() == txtPassword_d.Text.Trim())
                {
                    user.Password = SeatManage.SeatManageComm.MD5Algorithm.GetMD5Str32(txtPassword.Text.Trim());
                }
                else
                {
                    FineUI.Alert.Show("新密码输入有误，请核对两次输入的密码！");
                }
            }
            user.Remark = txtRemark.Text.Trim();
            user.UserName = txtUserName.Text.Trim();
            if (clbused.Checked)
            {
                user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
            }
            else
            {
                user.IsUsing = SeatManage.EnumType.LogStatus.Fail;
            }
            user.UserRoomRight.LoginID = user.LoginId;
            user.UserType = SeatManage.EnumType.UserType.Admin;
            user.UserRoomRight.RightRoomList.Clear();
            foreach (FineUI.CheckItem item in clbroom.Items)
            {
                if (item.Selected)
                {
                    user.UserRoomRight.RightRoomList.Add(new SeatManage.ClassModel.ReadingRoomInfo() { No = item.Value });
                }
            }
            user.ReloID.Clear();
            foreach (FineUI.CheckItem item in clbRole.Items)
            {
                if (item.Selected)
                {
                    user.ReloID.Add(int.Parse(item.Value));
                }
            }
            if (SeatManage.Bll.Users_ALL.UpdateUserInfo(user))
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("用户修改成功！");
            }
            else
            {
                FineUI.Alert.Show("数据错误修改失败！");
            }


        }
        /// <summary>
        /// 获取权限列表
        /// </summary>
        private void GetRole()
        {
            List<SeatManage.ClassModel.SysRolesDicInfo> rolelist = SeatManage.Bll.SysRolesDic.GetRoleList(null, null);
            foreach (SeatManage.ClassModel.SysRolesDicInfo roleinfo in rolelist)
            {
                FineUI.CheckItem ci = new FineUI.CheckItem();
                ci.Text = roleinfo.RoleName;
                ci.Value = roleinfo.RoleID;
                clbRole.Items.Add(ci);
            }
            //clbRole.DataTextField = "RoleName";
            //clbRole.DataValueField = "RoleID";
            //clbRole.DataSource = rolelist;
            //clbRole.DataBind();
            List<SeatManage.ClassModel.ReadingRoomInfo> roomlist = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
            foreach (SeatManage.ClassModel.ReadingRoomInfo room in roomlist)
            {
                FineUI.CheckItem ci = new FineUI.CheckItem();
                ci.Text = (room.Name + "(" + room.No + ")");
                ci.Value = room.No;
                clbroom.Items.Add(ci);
            }
            //clbroom.DataTextField = "Name";
            //clbroom.DataValueField = "No";
            //clbroom.DataSource = roomlist;
            //clbroom.DataBind();
        }
        /// <summary>
        /// 编辑模式获取用户信息
        /// </summary>
        private void ShowUserInfo()
        {
            string loginid = Request.QueryString["id"];
            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(loginid);
            if (user != null)
            {
                user.ReloID = SeatManage.Bll.Users_ALL.GetRoleID(loginid);
                user.UserRoomRight = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginid);
            }
            txtLoginID.Text = user.LoginId;
            txtLoginID.Readonly = true;
            txtPassword.Label = "新密码";
            txtPassword.Required = false;
            txtPassword_d.Required = false;
            txtUserName.Required = false;
            txtRemark.Text = user.Remark;
            txtUserName.Text = user.UserName;
            if (user.IsUsing == SeatManage.EnumType.LogStatus.Valid)
            {
                clbused.Checked = true;
            }
            else
            {
                clbused.Checked = false;
            }
            foreach (FineUI.CheckItem ci in clbRole.Items)
            {
                foreach (int role in user.ReloID)
                {
                    if (ci.Value == role.ToString())
                    {
                        ci.Selected = true;
                    }
                }
            }
            foreach (FineUI.CheckItem ci in clbroom.Items)
            {
                foreach (ReadingRoomInfo no in user.UserRoomRight.RightRoomList)
                {
                    if (ci.Value == no.No)
                    {
                        ci.Selected = true;
                    }
                }
            }
        }

    }
}