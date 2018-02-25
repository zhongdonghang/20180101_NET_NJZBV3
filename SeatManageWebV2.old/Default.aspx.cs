using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2
{
    public partial class _Default : DefaultBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void cmdOK_Click(object sender, ImageClickEventArgs e)
        {
            if (txtUserName.Text.Trim() == ""||txtPassword.Text.Trim()=="")
            {
                Response.Write(@"<script language='javascript'>alert('用户名和密码不能为空！'); </script> ");
                return;
            }
            try
            {
                //验证用户登录
                string loginID = txtUserName.Text;
                string Password = txtPassword.Text;
                SeatManage.Bll.Users_ALL userinfocheck = new SeatManage.Bll.Users_ALL(); 
                loginID = userinfocheck.CheckUser(loginID, Password);
                //判断返回信息是否为空
                if (string.IsNullOrEmpty(loginID))
                {
                    Response.Write(@"<script language='javascript'>alert('用户或密码错误，请重新输入'); </script> ");
                }
                else
                {
                    SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(loginID);
                    if (user != null)
                    {
                        string userIP = GetLoginIp();
                        if (!string.IsNullOrEmpty(userIP) && !string.IsNullOrEmpty(user.LockIPAdress) && user.LockIPAdress != "0.0.0.0" && user.LockIPAdress != userIP)
                        {
                            Response.Write(@"<script language='javascript'>alert('对不起您登录的IP地址没有经过授权！'); </script> ");
                        }
                        else
                        {
                            this.LoginId = loginID;
                            Response.Redirect("Florms/FormSYS.aspx");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(@"<script language='javascript'>alert('数据库连接出错！'); </script> ");
            }
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
    }
}
