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
                if (Session["LoginID"] == null)
                {
                    if(ConfigurationManager.AppSettings["appUrl"]!=null)
                    {
                        Response.Redirect(ConfigurationManager.AppSettings["appUrl"]);
                    }
                    else
                    {
                        Response.Redirect("../Default.aspx");
                     }
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
                //string userIP = GetLoginIp();
                //if (!string.IsNullOrEmpty(userIP) && !string.IsNullOrEmpty(user.LockIPAdress) && user.LockIPAdress != "0.0.0.0" && user.LockIPAdress != userIP)
                //{

                //    Response.Redirect("对不起您登录的IP地址没有经过授权");
                //}
                user.ReloID = SeatManage.Bll.Users_ALL.GetRoleID(loginid);
                user.UserRoomRight = SeatManage.Bll.T_SM_ManagerPotency.GetManangePotencyByLoginID(loginid);
                user.UserMenus = SeatManage.Bll.SysMenu.GetUserMenus(loginid);
                //获取全部对的阅览室权限
                if (loginid == "admin" || loginid == "user")
                {
                    List<SeatManage.ClassModel.ReadingRoomInfo> rightrooms = SeatManage.Bll.T_SM_ReadingRoom.GetReadingRooms(null, null, null);
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
        /// 获取远程访问用户的Ip地址  
        /// </summary>  
        /// <returns>返回Ip地址</returns>  
        private string GetLoginIp()
        {
            string loginip = "";
            //Request.ServerVariables[""]--获取服务变量集合   
            if (Request.ServerVariables["REMOTE_ADDR"] != null) //判断发出请求的远程主机的ip地址是否为空  
            {
                //获取发出请求的远程主机的Ip地址  
                loginip = Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
            //判断登记用户是否使用设置代理  
            else if (Request.ServerVariables["HTTP_VIA"] != null)
            {
                if (Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                {
                    //获取代理的服务器Ip地址  
                    loginip = Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                }
                else
                {
                    //获取客户端IP  
                    loginip = Request.UserHostAddress;
                }
            }
            else
            {
                //获取客户端IP  
                loginip = Request.UserHostAddress;
            }
            return loginip;
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