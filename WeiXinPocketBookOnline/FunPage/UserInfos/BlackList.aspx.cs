using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;

namespace WeiXinPocketBookOnline.UserInfos
{
    public partial class BlackList : BasePage
    {
        public string strBlacklistInfo = "";
        readonly WeiXinService.IWeiXinService weiXinService = new WeiXinService.WeiXinServiceHepler();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            if (IsPostBack)
            {
                string cmd = Request.Form["subCmd"];
                int pageIndex = int.Parse(Request.Form["subPageIndex"]);
                int pageSize = int.Parse(Request.Form["subPageSize"]);
                if (cmd == "")
                {
                    cmd = "ViolateDiscipline";
                }
                switch (cmd)
                {
                    case "blacklist":
                        BindBlacklistInfo(pageIndex,pageSize);
                        break;
                    case "ViolateDiscipline":
                        ShowViolateDiscipline(pageIndex, pageSize);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                }
            }
            else
            {
                int pageIndex = int.Parse(Request.Form["subPageIndex"]);
                int pageSize = int.Parse(Request.Form["subPageSize"]);
                ShowViolateDiscipline(pageIndex,pageSize);
            }
        }
        /// <summary>
        /// 绑定违规记录的显示信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="rrId"></param>
        /// <param name="beforeDays"></param>
        private void ShowViolateDiscipline(int pageIndex,int pageSize)
        {
            int queryDate = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
            try
            {
                List<AJM_ViolationRecordsLogInfo> vrls = null;
                vrls = weiXinService.GetViolationLog(LoginUserInfo.StudentNo, pageIndex, pageSize, UserSchoolInfo.SchoolNo);
                
                StringBuilder sbListInfo = new StringBuilder();
                sbListInfo.Append("<li data-theme='d' data-role='list-divider' role='heading'>违规记录 </li>");
                if (vrls.Count < 1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "没有符合查询条件的违规记录信息";
                }
                else
                {
                    for (int i = 0; i < vrls.Count; i++)
                    {
                        sbListInfo.Append(string.Format("<li data-theme='d'>{0}<ul>  <li>{1}</li>   <li>{2}</li> </ul> </li>", vrls[i].EnterOutTime, vrls[i].RoomName, vrls[i].Remark));
                    }
                    strBlacklistInfo = sbListInfo.ToString();
                }
            }
            catch (Exception ex)
            {
                strBlacklistInfo = "查询出错" + ex.Message;
            }
            vLi.Attributes["class"] = "ui-btn-active";
            bLi.Attributes["class"] = "";
        }


        /// <summary>
        /// 显示黑名单信息
        /// </summary>
        private void BindBlacklistInfo(int pageIndex,int pageSize)
        {
            string cardNo = LoginUserInfo.StudentNo;
            List<AJM_BlacklistLog> blacklists = weiXinService.GetBlacklist(cardNo,pageIndex,pageSize,UserSchoolInfo.SchoolNo);
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
                    if (blacklists[i].IsValid)
                    {
                        logState = "有效";
                    }
                    else
                    {
                        logState = "已失效";
                    }
                    strblacklist.Append("<li data-role=\'list-divider\' role=\'heading\'>黑名单记录 </li>");
                    //此方法暂时注释
                    //strblacklist.Append(string.Format("<li data-theme='c'>{0}<ul><li>{1}</li><li>{2}</li><li>失效时间：{3}</li><li>记录当前状态：{4}</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li></ul></li>", blacklists[i].AddTime, blacklists[i].ReadingRoomName, blacklists[i].ReMark, blacklists[i].OutTime, logState));
                }
                strBlacklistInfo = strblacklist.ToString();
            }
            vLi.Attributes["class"] = "";
            bLi.Attributes["class"] = "ui-btn-active";
        }
    }
}