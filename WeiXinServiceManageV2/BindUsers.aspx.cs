using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using AMS.Model;
using AMS.ServiceProxy;
using SeatBespeakException;
using SeatManage.ClassModel;
using SeatManage.IPocketBespeak;
using SeatManage.PocketBespeak;
using SeatManage.SeatManageComm;
using TcpClient_BespeakSeat;
using WeiXinService;
using SeatManage.AppJsonModel;

namespace WeiXinServiceManage
{
    public partial class BindUsers : Page
    {
        private string cmd;
        readonly IWeiXinService weiXinSercive = new WeiXinServiceHepler();
        protected void Page_Load(object sender, EventArgs e)
        {
            UserSchoolInfo = null;
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
                    spanWarmInfo.InnerText = "请选择学校";
                }
                users.LoginId = txt_LoginID.Value;
                users.Password = txt_Password.Value;
                if (loginHandle(users, schoolId))
                {
                    user.WeixinID = Request.QueryString["WXID"];
                    user.CardNo = txt_LoginID.Value;
                    user.SchoolInfo = new AMS_School { Id = Convert.ToInt32(schoolId) };

                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = WeiXinProxy.BindUserInfo(user);

                    AMS_School school = AMS_SchoolProxy.GetSchoolById(int.Parse(schoolId));
                    CookiesManager.SetCookies(user.CardNo, txt_Password.Value, schoolId);
                }
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
            try
            {
                UserSchoolInfo = weiXinSercive.GetSingleSchoolInfoByID(schoolId);
            }
            catch
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "获取学校信息失败。";
                Session.Clear();
                return false;
            }
            if (UserSchoolInfo == null)
            {
                return false;
            }
            try
            {
                LoginUserInfo = weiXinSercive.CheckReader(user.LoginId, user.Password, UserSchoolInfo.Number);
                divSuccess.Style.Add("display", "block");
                divcontent.Style.Add("display", "none");
                divstuInfo.Style.Add("display", "none");
                return true;
            }
            catch (RemoteServiceLinkFailed ex)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "连接学校服务器失败，可能是学校已经关闭了服务器的远程访问。";
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

                    LoginUserInfo = weiXinSercive.GetReaderInfo(user.CardNo, user.SchoolInfo.Number);
                    lblMyName.InnerText = LoginUserInfo.Name;
                    lblMyStuCode.InnerText = LoginUserInfo.StudentNo;
                    lblmySchool.InnerText = user.SchoolInfo.Name;
                    txt_LoginID.Value = LoginUserInfo.StudentNo;
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
            List<AMS_School> schoolList = new List<AMS_School>();
            try
            {
                schoolList = weiXinSercive.GetWeCharSchoolList();
            }
            catch
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "获取学校信息失败。";
                Session.Clear();
                return;
                //Response.Redirect("ErrorPage.aspx");
            }
            ListItem item = new ListItem("-请选择学校-", "-1");
            selSchool.Items.Add(item);
            foreach (ListItem items in from t in schoolList where t.IsSeatBespeak select new ListItem(t.Name, t.Id.ToString()))
            {
                selSchool.Items.Add(items);
            }
        }
        #endregion

        #region 当前登录的用户信息
        /// <summary>
        /// 当前登录的用户信息
        /// </summary>
        public AJM_Reader LoginUserInfo
        {
            get;
            set;
        }
        #endregion

        #region 当前用户所在的学校
        /// <summary>
        /// 当前用户所在的学校
        /// </summary>
        public AMS_School UserSchoolInfo
        {
            get;
            set;
        }
        #endregion
    }
}