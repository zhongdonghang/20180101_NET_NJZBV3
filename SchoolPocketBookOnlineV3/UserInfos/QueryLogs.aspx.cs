using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.UserInfos
{
    public partial class QueryLogs : BasePage
    {
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        public string listMessage = "";
        //private SeatManage.IPocketBespeak.IQueryLogs handler = new SeatManage.PocketBespeak.PocketBespeak_QueryLogs();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            spanWarmInfo.Visible = false;
            if (IsPostBack)
            {
                string cmd = Request.Form["subCmd"];
                string bookNo = Request.Form["subBookNo"];
                string rrId = ddlRoom.Items[ddlRoom.SelectedIndex].Value;
                int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
                if (cmd == "")
                {
                    cmd = "BookLogs";
                }
                //if (rrId == "-1" && (cmd == "EnterOutLog" || cmd == "ViolateDiscipline"))
                //{
                //    spanWarmInfo.Visible = true;
                //    spanWarmInfo.InnerText = "请选择阅览室";
                //    switch (cmd)
                //    {
                //        case "EnterOutLog":
                //            eLi.Attributes["class"] = "ui-btn-active";
                //            vLi.Attributes["class"] = "";
                //            bLi.Attributes["class"] = "";
                //            break;
                //        case "ViolateDiscipline":
                //            eLi.Attributes["class"] = "";
                //            vLi.Attributes["class"] = "ui-btn-active";
                //            bLi.Attributes["class"] = "";
                //            break;
                //        case "BookLogs":
                //            eLi.Attributes["class"] = "";
                //            vLi.Attributes["class"] = "";
                //            bLi.Attributes["class"] = "ui-btn-active";
                //            break;
                //    }
                //    return;
                //}
                switch (cmd)
                {
                    case "EnterOutLog":
                        ShowEnterOutLogs();
                        break;
                    case "ViolateDiscipline":
                        ShowViolateDiscipline();
                        break;
                    case "BookLogs":
                        ShowBookLogs();
                        break;
                    case "cancel":
                        CancelBookLog(bookNo);
                        break;
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
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
            List<ReadingRoomInfo>
            roomList = handler.GetAllReadingRoomInfo();
            for (int i = 0; i < roomList.Count; i++)
            {
                ListItem item = new ListItem { Text = roomList[i].Name, Value = roomList[i].No };
                ddlRoom.Items.Add(item);
            }
        }
        /// <summary>
        /// 绑定违规记录的显示信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="rrId"></param>
        /// <param name="beforeDays"></param>
        private void ShowViolateDiscipline()
        {
            string rrId = ddlRoom.Items[ddlRoom.SelectedIndex].Value;
            int queryDate = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
            try
            {
                List<ViolationRecordsLogInfo> vrls = null;
                if (rrId == "-1")
                {
                    vrls = handler.GetViolateDiscipline(LoginUserInfo.CardNo, null, queryDate);
                }
                else
                {
                    vrls = handler.GetViolateDiscipline(LoginUserInfo.CardNo, rrId, queryDate);
                }
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
                        sbListInfo.Append(string.Format("<li data-theme='d'>{0}<ul>  <li>{1}</li>   <li>{2}</li> <li>{3}</li> </ul> </li>", vrls[i].EnterOutTime, vrls[i].ReadingRoomName, vrls[i].Remark, vrls[i].Flag == LogStatus.Valid ? "有效记录" : "无效记录"));
                    }
                    listMessage = sbListInfo.ToString();
                }
            }
            catch (Exception ex)
            {
                listMessage = "查询出错" + ex.Message;
            }
            eLi.Attributes["class"] = "";
            vLi.Attributes["class"] = "ui-btn-active";
            bLi.Attributes["class"] = "";
        }
        /// <summary>
        /// 绑定进出记录的显示信息
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="rrId"></param>
        /// <param name="beforeDays"></param>
        private void ShowEnterOutLogs()
        {
            string rrId = ddlRoom.Items[ddlRoom.SelectedIndex].Value;
            int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
            try
            {
                List<EnterOutLogInfo> vrls = null;
                if (rrId == "-1")
                {
                    vrls = handler.GetEnterOutLogs(LoginUserInfo.CardNo, null, date);//.GetEnterOutLogs(cardNo, rrId, date, Session["SchoolConnectionString"].ToString());
                }
                else
                {
                    vrls = handler.GetEnterOutLogs(LoginUserInfo.CardNo, rrId, date);
                }
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
                        string mesage = vrls[i].EnterOutTime.GetDateTimeFormats('M')[0] + vrls[i].EnterOutTime.GetDateTimeFormats('t')[0] + " " + vrls[i].Remark;

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
            vLi.Attributes["class"] = "";
            bLi.Attributes["class"] = "";
        }
        /// <summary>
        /// 绑定预约记录信息
        /// </summary>
        /// <param name="cardNo">学号</param>
        /// <param name="rrId">阅览室编号</param>
        /// <param name="queryDate">查询日期</param>
        private void ShowBookLogs()
        {
            string rrId = ddlRoom.Items[ddlRoom.SelectedIndex].Value;
            int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
            try
            {
                List<BespeakLogInfo> bookLogList = null;
                if (rrId == "-1")
                {
                    bookLogList = handler.GetBookLogs(LoginUserInfo.CardNo, null, date);
                }
                else
                {
                    bookLogList = handler.GetBookLogs(LoginUserInfo.CardNo, rrId, date);
                }
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
                        sbListInfo.Append(string.Format("<li date-theme='d'>{0}：{1}<ul date-theme='d'><li>预约时间：{2}</li><li>提交时间：{3}</li><li>取消时间：{4}</li>", bookLogList[i].ReadingRoomName, bookLogList[i].ShortSeatNum, bookLogList[i].BsepeakTime, bookLogList[i].SubmitTime, bookLogList[i].CancelTime));
                        switch (bookLogList[i].BsepeakState)
                        {
                            case BookingStatus.Cencaled:
                                sbListInfo.Append("<li>预约状态：已取消</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li>");
                                break;
                            case BookingStatus.Confinmed:
                                sbListInfo.Append("<li>预约状态：已确认</li><li><div data-role='button' style='width:90px; height=50px' data-mini='true' class='ui-btn-left' onclick='javascript:history.go(-1);'>返回 </div></li>");
                                break;
                            case BookingStatus.Waiting:
                                sbListInfo.Append("<li>预约状态：等待确认</li>");
                                sbListInfo.Append(string.Format("<li><input data-inline='true' data-mini='false' value='取消' type='button' onclick='subCancel(&apos;{0}&apos;)' /></li>", bookLogList[i].BsepeaklogID));
                                break;
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
            vLi.Attributes["class"] = "";
            bLi.Attributes["class"] = "ui-btn-active";
        }

        /// <summary>
        /// 取消预约记录
        /// </summary>
        /// <param name="bookNo"></param>
        /// <param name="bookCancelPerson"></param>
        /// <param name="conn"></param>
        private void CancelBookLog(string bookNo)
        {
            try
            {
                bool result = handler.UpdateBookLogsState(int.Parse(bookNo));
                if (result)
                {
                    subCmd.Value = "";
                    ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('成功取消预约！');window.close();", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消预约失败！');window.close();", true);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}