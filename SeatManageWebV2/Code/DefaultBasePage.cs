using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace SeatManageWebV2
{
    public class DefaultBasePage : System.Web.UI.Page
    {
        #region 变量区

        #endregion 变量区结束

        #region 属性区
        #region #BaseRoot:string 应用程序根路径（虚拟路径）
        /// <summary>
        /// 应用程序根路径（虚拟路径）
        /// </summary>
        protected string BaseRoot
        {
            get { return Request.ApplicationPath; }
        }

        protected string LoginPage
        {
            get
            {
                return ConfigurationManager.AppSettings["redirectLoginPage"];
            }
        }
        /// <summary>
        /// 注销页面
        /// </summary>
        protected string LogoutPage 
        {
            get
            {
                return ConfigurationManager.AppSettings["LogOutUrl"];
            }
        }

        protected string LoginId
        {
            get
            {
                if (Session["LoginID"] != null)
                {
                    string loginId= Session["LoginID"].ToString(); 
                    return loginId;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先登录！');parent.location.href='" + LoginPage + "';", true);
                    return null;
                }
            }
            set { Session["LoginID"] = value; }
        }

        #endregion
        #endregion 属性区结束

        #region BasePage
        /// <summary>
        /// constructor
        /// </summary>
        public DefaultBasePage()
        {

        }
        #endregion

        #region Page_PreInit
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreInit(object sender, EventArgs e)
        {

        }

        protected override void OnInit(EventArgs e)
        {

        }

        #endregion

        //private void IsloginRole()
        //{
        //    if (Session["LoginID"] == null)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先登录！');location='../../default.aspx';", true);
        //        return;
        //    }
        //    List<SeatManage.ClassModel.SysMenuInfo> listSysMenu = SeatManage.Bll.SysMenu.GetUserMenus(Session["LoginID"].ToString());
        //    bool isrole = false;
        //    foreach (SeatManage.ClassModel.SysMenuInfo menu in listSysMenu)
        //    {
        //        foreach (SeatManage.ClassModel.SysMenuInfo cmenu in menu.ChildMenu)
        //        {
        //            if ("~/" + cmenu.MenuLink == this.Page.AppRelativeVirtualPath)
        //            {
        //                isrole = true;
        //                break;
        //            }
        //        }
        //    }
        //    if (!isrole)
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('您没有权限访问此页面，请跟管理员联系！');location='../../default.aspx';", true);
        //    }
        //}

    }
}