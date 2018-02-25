using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using SeatManage.AppJsonModel;
using SeatManage.ClassModel;

namespace WeiXinPocketBookOnline.UserInfos
{
    public partial class QueryLogs : BasePage
    {
        public string listMessage = "";
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
                string bookNo = Request.Form["subBookNo"];
                int pageIndex = int.Parse(Request.Form["subPageIndex"]);
                int pageSize = int.Parse(Request.Form["subPageSize"]);
                if (cmd == "")
                {
                    cmd = "BookLogs";
                }
                switch (cmd)
                {
                    case "EnterOutLog":
                        ShowEnterOutLogs(pageIndex, pageSize);
                        break;
                    case "BookLogs":
                        ShowBookLogs(pageIndex, pageSize);
                        break;
                    case "cancel":
                        CancelBookLog(bookNo);
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
                BindReadingRoom();
            }
        }
        /// <summary>
        /// 绑定阅览室
        /// </summary>
        /// <param name="rrid"></param>
        /// <param name="intDate"></param>
        private void BindReadingRoom()
        {
            List<AJM_ReadingRoom>
                roomList = weiXinService.GetAllRoomInfo(UserSchoolInfo.SchoolNo);
            for (int i = 0; i < roomList.Count; i++)
            {
                ListItem item = new ListItem() { Text = roomList[i].RoomName, Value = roomList[i].RoomNo };
                ddlRoom.Items.Add(item);
            }
        }
        /// <summary>
        /// 绑定进出记录的显示信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="rrId"></param>
        /// <param name="beforeDays"></param>
        private void ShowEnterOutLogs(int pageIndex, int pageSize)
        {
            try
            {
                List<AJM_EnterOutLog> vrls = null;

                vrls = weiXinService.GetEnterOutLog(LoginUserInfo.StudentNo, pageIndex, pageSize,
                    UserSchoolInfo.SchoolNo);

                StringBuilder sbListInfo = new StringBuilder();
                sbListInfo.Append("<li data-theme='d' data-role='list-divider' role='heading'>进出记录 </li>");
                if (vrls.Count < 1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "没有符合查询条件的进出记录信息";
                }
                else
                {
                    for (int i = 0; i < vrls.Count; i++)
                    {
                        string mesage = Convert.ToDateTime(vrls[i].EnterOutTime).GetDateTimeFormats('M')[0].ToString() + Convert.ToDateTime(vrls[i].EnterOutTime).GetDateTimeFormats('t')[0].ToString() + " " + vrls[i].Remark;

                        sbListInfo.Append(string.Format("<li data-theme='d'>{0}</li>", mesage));
                    }
                    listMessage = sbListInfo.ToString();
                }
            }
            catch (Exception ex)
            {
                listMessage = "查询出错" + ex.Message;
            }
            eLi.Attributes["class"] = "ui-btn-active";
            bLi.Attributes["class"] = "";
        }
        /// <summary>
        /// 绑定预约记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="rrId">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        private void ShowBookLogs(int pageIndex, int pageSize)
        {
            try
            {
                List<AJM_BespeakLog> bookLogList = null;
                bookLogList = weiXinService.GetBesapsekLog(LoginUserInfo.StudentNo, pageIndex, pageSize, UserSchoolInfo.SchoolNo);

                StringBuilder sbListInfo = new StringBuilder();
                sbListInfo.Append("<li data-theme='d' data-role='list-divider' role='heading'>预约记录 </li>");
                if (bookLogList.Count < 1)
                {
                    spanWarmInfo.Visible = true;
                    spanWarmInfo.InnerText = "没有符合查询条件的预约记录信息";
                }
                else
                {
                    for (int i = 0; i < bookLogList.Count; i++)
                    {
                        sbListInfo.Append(string.Format("<li date-theme='d'>{0}：{1}<ul date-theme='d'><li>预约时间：{2}</li><li>提交时间：{3}</li><li>取消时间：{4}</li>", bookLogList[i].RoomName, bookLogList[i].SeatShortNo, bookLogList[i].BookTime, bookLogList[i].SubmitDateTime, bookLogList[i].CancelTime));
                        if (bookLogList[i].IsValid)
                        {
                            sbListInfo.Append("<li>预约状态：等待确认</li>");
                            sbListInfo.Append(string.Format("<li><input data-inline='true' data-mini='false' value='取消' type='button' onclick='subCancel(&apos;{0}&apos;)' /></li>", bookLogList[i].Id));
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(bookLogList[i].Operator))
                            {
                                sbListInfo.Append("<li>预约状态：已确认</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li>");
                            }
                            else
                            {
                                sbListInfo.Append("<li>预约状态：已取消</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li>");
                            }
                        }
                        sbListInfo.Append("</ul></li>");
                    }
                    listMessage = sbListInfo.ToString();
                }
            }
            catch (Exception ex)
            {
                listMessage = "查询出错" + ex.Message;
            }
            eLi.Attributes["class"] = "";
            bLi.Attributes["class"] = "ui-btn-active";
        }

        /// <summary>
        /// 取消预约记录
        /// </summary>
        /// <param name="bookNo"></param>
        private void CancelBookLog(string bookNo)
        {
            try
            {
                string result = weiXinService.CancelBesapeakById(int.Parse(bookNo), UserSchoolInfo.SchoolNo);
                subCmd.Value = "";
                ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('" + result + "！');window.close();", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}