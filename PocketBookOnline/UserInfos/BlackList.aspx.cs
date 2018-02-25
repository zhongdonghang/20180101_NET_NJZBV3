using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using System.Text;

namespace PocketBookOnline.UserInfos
{
    public partial class BlackList : BasePage
    {
        public string strBlacklistInfo = ""; 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect("../Login.aspx");
            } 
            spanWarmInfo.Visible = false; 
            if (IsPostBack)
            {
                string cmd = Request.Form["subCmd"];
                int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
                switch (cmd)
                {
                    case "query":
                        BindBlacklistInfo(date);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect("../Login.aspx");
                        break;
                }
            }
            else
            {
                int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
                BindBlacklistInfo(date);
            }
        }


        /// <summary>
        /// 显示黑名单信息
        /// </summary>
        private void BindBlacklistInfo(int intDate)
        { 
            string cardNo =this.LoginUserInfo.CardNo;
            List<BlackListInfo> blacklists = BespeakHandler.GetBlackList(this.UserSchoolInfo, cardNo, intDate);// blackListBll.GetBlackList(cardNo, intDate, Session["SchoolConnectionString"].ToString());
            StringBuilder strblacklist = new StringBuilder();
            if (blacklists.Count < 1)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "没有符合查询条件的黑名单信息";
            }
            else
            {
                for (int i = 0; i < blacklists.Count; i++)
                {
                    string logState = "";
                    if (blacklists[i].BlacklistState == SeatManage.EnumType.LogStatus.Valid)
                    {
                        logState = "有效";
                    }
                    else
                    {
                        logState = "已失效";
                    }
                    strblacklist.Append(string.Format("<li data-role='list-divider' role='heading'>黑名单记录 </li>"));
                    strblacklist.Append(string.Format("<li data-theme='c'>{0}<ul><li>{1}</li><li>{2}</li><li>失效时间：{3}</li><li>记录当前状态：{4}</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li></ul></li>", blacklists[i].AddTime, blacklists[i].ReadingRoomName, blacklists[i].ReMark, blacklists[i].OutTime, logState));
                }
                strBlacklistInfo = strblacklist.ToString();
            }
        }
    }
}