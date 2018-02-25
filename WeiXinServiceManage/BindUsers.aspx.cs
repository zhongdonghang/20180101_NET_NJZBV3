using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.PocketBespeak;
using AMS.Model;
using SeatManage.ClassModel;
using SeatBespeakException;
using SeatManage.IPocketBespeak;
using AMS.IBllService;
using AMS.BllService;
using AMS.ServiceProxy;
using TcpClient_BespeakSeat;

namespace WeiXinServiceManage
{
    public partial class BindUsers : System.Web.UI.Page
    {
        private string cmd;
        SeatManage.IPocketBespeak.ILogin handler = new PocketBespeak_Login();
        SeatManage.IPocketBespeak.ILogin tcpHandler;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UserSchoolInfo = null;
            if (!IsPostBack)
            {
                BindSelSchool();
                if (Request.QueryString["WXID"] != null)
                {
                    BindStuData(Request.QueryString["WXID"]);
                }
            }
            if (Request.QueryString["WXID"] == null)
            {
                divcontent.Style.Add("display", "block");
                divstuInfo.Style.Add("display", "none");
                divSuccess.Style.Add("display", "none");
                return;
            }

            cmd = Request.Form["subCmd"];
            //通过页面验证，执行登录操作
            if (cmd == "Login")
            {
                WeiXinUsers user = new WeiXinUsers();
                UserInfo users = new UserInfo();
                string schoolId = selSchool.Items[selSchool.SelectedIndex].Value;
                if (schoolId == "-1")
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = string.Format("请选择学校");
                }
                users.LoginId = txt_LoginID.Value;
                users.Password = txt_Password.Value;
                if (loginHandle(users, schoolId))
                {
                    user.WeixinID = Request.QueryString["WXID"].ToString();
                    user.CardNo = txt_LoginID.Value;
                    user.SchoolInfo = new AMS.Model.AMS_School() { Id = Convert.ToInt32(schoolId) };

                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = WeiXinProxy.BindUserInfo(user);

                    AMS.Model.AMS_School school = handler.GetSingleSchoolInfo(schoolId);
                    SeatManage.SeatManageComm.CookiesManager.SetCookies(user.CardNo, txt_Password.Value, schoolId);
                }
                else
                {
                    //spanWarmInfo.Visible = true;
                    //spanWarmInfo.InnerText = "账号或密码不正确";
                }
            }
            else
            {
            }
        }


        #region 验证用户信息并把用户信息
        /// <summary>
        /// 验证用户信息并把用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="schoolId"></param>
        /// <returns></returns>
        private bool loginHandle(UserInfo user, string schoolId)
        {
            TcpClient_BespeakSeatAllMethod schoolMethod = null;
            try
            {
                this.UserSchoolInfo = handler.GetSingleSchoolInfo(schoolId);
                schoolMethod = new TcpClient_BespeakSeatAllMethod(UserSchoolInfo);
                AMS.Model.AMS_School school = this.UserSchoolInfo;
            }
            catch
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("获取学校信息失败。");
                Session.Clear();
                return false;
            }
            if (this.UserSchoolInfo == null)
            {
                return false;
            }
            try
            {
                this.LoginUserInfo = schoolMethod.CheckAndGetReaderInfo(user, this.UserSchoolInfo);
                divSuccess.Style.Add("display", "block");
                divcontent.Style.Add("display", "none");
                divstuInfo.Style.Add("display", "none");
                return true;
            }
            catch (RemoteServiceLinkFailed ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("连接学校服务器失败，可能是学校已经关闭了服务器的远程访问。");
                Session.Clear();
                return false;
            }
            catch (Exception ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("登录失败:{0}", ex.Message);
                Session.Clear();
                return false;
            }
        }
        #endregion

        #region 绑定界面学校下拉列表

        private void BindStuData(string uid)
        {
            try
            {
                WeiXinUsers user = WeiXinProxy.GetWeiXinUser(uid);
                if (user != null)
                {
                    tcpHandler = new TcpClient_BespeakSeatAllMethod(user.SchoolInfo);
                    this.LoginUserInfo = (tcpHandler as TcpClient_BespeakSeatAllMethod).GetReaderInfoByCardNofalse(user.CardNo, user.SchoolInfo);
                    this.lblMyName.InnerText = LoginUserInfo.Name;
                    this.lblMyStuCode.InnerText = LoginUserInfo.CardNo;
                    this.lblmySchool.InnerText = user.SchoolInfo.Name;
                    this.txt_LoginID.Value = LoginUserInfo.CardNo;
                    foreach (ListItem li in selSchool.Items)
                    {
                        if (li.Value == user.SchoolInfo.Id.ToString())
                        {
                            li.Selected = true;
                            break;
                        }
                    }

                    divcontent.Style.Add("display", "none");
                    divstuInfo.Style.Add("display", "block");
                    divSuccess.Style.Add("display", "none");

                }
                else
                {
                    divcontent.Style.Add("display", "block");
                    divstuInfo.Style.Add("display", "none");
                    divSuccess.Style.Add("display", "none");
                }
            }
            catch (Exception EX)
            {
                divcontent.Style.Add("display", "block");
                divstuInfo.Style.Add("display", "none");
                divSuccess.Style.Add("display", "none");
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format(EX.Message);
            }
        }
        /// <summary>
        /// 绑定界面学校下拉列表
        /// </summary>
        private void BindSelSchool()
        {
            List<AMS.Model.AMS_School> schoolList = new List<AMS.Model.AMS_School>();
            try
            {
                schoolList = handler.GetAllSchoolFromLocal();
            }
            catch
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = string.Format("获取学校信息失败。");
                Session.Clear();
                return;
                //Response.Redirect("ErrorPage.aspx");
            }
            ListItem item = new ListItem("-请选择学校-", "-1");
            selSchool.Items.Add(item);
            for (int i = 0; i < schoolList.Count; i++)
            {
                if (schoolList[i].IsSeatBespeak)
                {
                    ListItem items = new ListItem(schoolList[i].Name, schoolList[i].Id.ToString());
                    selSchool.Items.Add(items);
                }
            }
        }
        #endregion

        #region 当前登录的用户信息
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public SeatManage.ClassModel.ReaderInfo LoginUserInfo
        {
            get;
            set;
        }
        #endregion

        #region 当前用户所在的学校
        /// <summary>
        /// 当前用户所在的学校
        /// </summary>
        public AMS.Model.AMS_School UserSchoolInfo
        {
            get;
            set;
        }
        #endregion
    }
}