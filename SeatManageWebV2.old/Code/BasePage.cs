/*******************************************************************
 * 版权所有：
 * 描述：页面基类，所有的页面继承此页面
 * 创建者：罗晨阳
 * 创建日期：2013-06-04
 * 修改日期：
 * *****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using FineUI;
using System.Web;

namespace SeatManageWebV2
{
    public class BasePage : DefaultBasePage
    {
        #region 变量区

        static string serverName;
        static string httpReferer;
        static string httpHost;
        static string loginId;
        static string url;

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

        /// <summary>
        /// 当前登录的Id
        /// </summary>
        //protected string LoginId
        //{
        //    get
        //    {
        //        if (Session["LoginID"] != null)
        //        {
        //            return Session["LoginID"].ToString();
        //        }
        //        else
        //        {
        //            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先登录！');parent.location.href='../../default.aspx';", true);
        //            return null;
        //        }
        //    }
        //    set { Session["LoginID"] = value; }
        //}

        #endregion
        #endregion 属性区结束

        #region BasePage
        /// <summary>
        /// constructor
        /// </summary>
        public BasePage()
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
            if (Session["LoginID"] == null)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "alert('请先登录！');parent.location.href='" + LoginPage + "';", true);
            }
            else
            {
                loginId = Session["LoginID"].ToString();
            }
            url = HttpContext.Current.Request.Url.AbsoluteUri.ToString();
            if (Request.ServerVariables["SERVER_NAME"] != null && Request.ServerVariables["HTTP_REFERER"] != null && Request.ServerVariables["HTTP_HOST"] != null)
            {
                serverName = Request.ServerVariables["SERVER_NAME"].Trim();//服务器名称 
                httpReferer = Request.ServerVariables["HTTP_REFERER"].Trim();//http接收的名称 
                httpHost = Request.ServerVariables["HTTP_HOST"].Trim();//类似这样的格式www.ccopus.com 
            }
            else
            {
                serverName = "";
                httpReferer = "";
                httpHost = "";
            }
        }

        #endregion

        //public static bool OpVerifiction()
        //{
        //    //string s1 = Request.ServerVariables["SERVER_NAME"].Trim();//服务器名称 
        //    //if (!IsPostBack)
        //    //{
        //    bool result = false;
        //    if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(httpReferer) && !string.IsNullOrEmpty(httpHost))
        //    {
        //        //string s2 = Request.ServerVariables["HTTP_REFERER"].Trim();//http接收的名称 
        //        //string s4 = Request.ServerVariables["HTTP_HOST"].Trim();//类似这样的格式www.ccopus.com 
        //        int count = httpHost.Length + 1 + 7;
        //        string strFlorms = "Florms/FormSYS.aspx";
        //        string strGetUrl = httpReferer.Substring(count).ToLower();
        //        if (string.IsNullOrEmpty(strGetUrl))
        //        {
        //            result = true;
        //        }
        //        if (strGetUrl != strFlorms.ToLower().Trim() && strGetUrl != "default.aspx")
        //        {
        //            result = false;
        //            WriteLogs(url);
        //            //Response.Write("警告！你的IP已经被记录!不要使用敏感字符！");
        //            //Response.End();
        //        }
        //        else
        //        {
        //            result = true;
        //        }
        //    }
        //    else
        //    {
        //        WriteLogs(url);
        //    }
        //    return result;
        //}

        public static bool OpVerifiction()
        {
            //string s1 = Request.ServerVariables["SERVER_NAME"].Trim();//服务器名称 
            //if (!IsPostBack)
            //{
            bool result = false;
            if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(httpReferer) && !string.IsNullOrEmpty(httpHost))
            {
                //string s2 = Request.ServerVariables["HTTP_REFERER"].Trim();//http接收的名称 
                //string s4 = Request.ServerVariables["HTTP_HOST"].Trim();//类似这样的格式www.ccopus.com 

                string strFlorms = "Florms/FormSYS.aspx";
                int count = strFlorms.Length;
                string strGetUrl = httpReferer.Substring(httpReferer.Length - count).ToLower();
                if (string.IsNullOrEmpty(strGetUrl))
                {
                    result = true;
                }
                if (strGetUrl != strFlorms.ToLower().Trim() && strGetUrl != "default.aspx")
                {
                    result = false;
                    WriteLogs(url);
                    //Response.Write("警告！你的IP已经被记录!不要使用敏感字符！");
                    //Response.End();
                }
                else
                {
                    result = true;
                }
            }
            else
            {
                WriteLogs(url);
            }
            return result;
        }



        public static void WriteLogs(string geturl)
        {
            SeatManage.SeatManageComm.WriteLog.Write(string.Format("用户通过非法登录访问网站，访问页面地址为：{0},用户IP地址为：{1},登录名为：{2}", geturl, GetIP(), loginId));
        }

        /// <summary>
        /// 获得当前页面客户端的IP
        /// </summary>
        /// <returns>当前页面客户端的IP</returns>
        public static string GetIP()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (string.IsNullOrEmpty(result))
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            if (string.IsNullOrEmpty(result))
            {
                return "127.0.0.1";
            }
            return result;
        }


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
