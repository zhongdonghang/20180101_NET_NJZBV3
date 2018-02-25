using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;

namespace SeatManageWebV2.Florms
{
    public partial class FormSYS : BasePage
    {
        /// <summary>
        /// 标题
        /// </summary>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(this.LoginId))
                {
                    Response.Redirect(this.LogoutPage);
                    return;
                }
                InitTreeMenu();
                LoadData();
                btnPassword.OnClientClick = Window_Edit_Password.GetShowReference("../FunctionPages/UsersManage/ChangePassword.aspx?id=" + this.LoginId + "", "密码修改");
            }

        }

        private void InitTreeMenu()
        {

        }

        //退出
        protected void btnExit_Click(object sender, EventArgs e)
        {
            //#region 赋值
            //Session["SystemID"] = null;
            //Session["UserName"] = null;
            //Session["PersonName"] = null;
            //this.LoginId = "";
            //Session["AgentStatus"] = null;
            //Session["RootDeptID"] = null;
            //Session["RangeID"] = null;
            //Session["UserDeptID"] = null;
            //Session["OldDeptID"] = null;//默认当前部门，用于部门树
            //Session["UserDeptName"] = null;
            //Session["UserOrgID"] = null;    //机构ID
            //Session["UserOrgName"] = null;
            //Session["CONONAME"] = null;
            //Session["RootDeptName"] = null;
            //Session["UserAllRights"] = null;
            //Session["Themes"] = null;
            //Session["MainUrl"] = null;
            //#endregion
            //Response.Redirect("~/default.aspx");
            //Epower.DevBase.BaseTools.JscriptTool.GotoParentWindow("../Default.aspx?Logout=1");
            Response.Redirect(this.LogoutPage);
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        private void LoadData()
        {
            //获取用户信息
            SeatManage.ClassModel.UserInfo LoginUser = GetUserInfo(this.LoginId);
            if (LoginUser==null)
            {
                Response.Write(@"<script language='javascript'>alert('用户信息获取失败请重新登录！'); </script> ");
                Response.Redirect(this.LogoutPage);
            }
            ShowUserInfo(LoginUser);
            List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = LoginUser.UserMenus;
            if (listSysMenu != null)
            {
                foreach (SeatManage.ClassModel.SysMenuInfo list in listSysMenu)
                {
                    FineUI.TreeNode node = new FineUI.TreeNode();
                    node.Text = list.MenuName;
                    node.Expanded = false;
                    node.SingleClickExpand = true;
                    TreeMenu.Nodes.Add(node);
                    foreach (SeatManage.ClassModel.SysMenuInfo listChild in list.ChildMenu)
                    {
                        FineUI.TreeNode nodeChild = new FineUI.TreeNode();
                        nodeChild.Text = listChild.MenuName;
                        nodeChild.Expanded = false;
                        nodeChild.NavigateUrl = "../" + listChild.MenuLink;
                        node.Nodes.Add(nodeChild);
                    }
                }
            }
            if (LoginUser.UserType == SeatManage.EnumType.UserType.Admin)
            {
                houseTab.IFrameUrl = "../FunctionPages/Statistical/LibraryUsedStatistical.aspx";
            }
            else
            {
                if (ConfigurationManager.AppSettings["ChangePassWord"] == "close")
                {
                    btnPassword.Visible = false;
                }
                else
                {
                    btnPassword.Visible = true;
                }
                houseTab.IFrameUrl = "../FunctionPages/Statistical/LibraryUsedStat.aspx";
            }
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="loginid"></param>
        /// <returns></returns>
        private SeatManage.ClassModel.UserInfo GetUserInfo(string loginid)
        {
            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(loginid);
            if (user != null)
            {
                user.ReloID = SeatManage.Bll.Users_ALL.GetRoleID(loginid);
                user.UserRoomRight = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginid);
                user.UserMenus = SeatManage.Bll.SysMenu.GetUserMenus(loginid);
                //获取全部对的阅览室权限
                if (loginid == "admin" || loginid == "user")
                {
                    List<SeatManage.ClassModel.ReadingRoomInfo> rightrooms = SeatManage.Bll.ClientConfigOperate.GetReadingRooms(null);
                    if (user.UserRoomRight == null || rightrooms.Count != user.UserRoomRight.RightRoomList.Count)
                    {
                        user.UserRoomRight.RightRoomList.Clear();
                        foreach (SeatManage.ClassModel.ReadingRoomInfo room in rightrooms)
                        {
                            user.UserRoomRight.RightRoomList.Add(room);
                        }
                        SeatManage.Bll.Users_ALL.UpdateUserInfo(user);
                    }
                }
            }
            return user;
        }
        /// <summary>
        /// 显示用户信息
        /// </summary>
        /// <param name="user"></param>
        private void ShowUserInfo(SeatManage.ClassModel.UserInfo user)
        {
            //lbloginTime.Text = SeatManage.Bll.ServiceDateTime.Now.ToString();
            lblUserName.Text = user.UserName + "，欢迎您！";
        }

    }
}