﻿using System;
using System.Collections.Generic;
using System.Text;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.IPocketBespeakBllServiceV2;
using SeatManage.PocketBespeakBllServiceV2;
using SeatManage.SeatManageComm;

namespace SchoolPocketBookWeb.StudyRoom
{
    public partial class StudyLogList : BasePage
    {
        public string logList = "";
        private IPocketBespeakBllService handler = new PocketBespeakBllService();
        //SeatManage.IPocketBespeak.IBookStudyRoom handler = new SeatManage.PocketBespeak.PecketBespeak_BookStudyRoom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (LoginUserInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            BindDataList();

            string cmd = Request.Form["subCmd"];
            if (IsPostBack)
            {
                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "cancel":
                        StudyBookingLog log = handler.GetStudyLog(int.Parse(logNo.Value));
                        string result = handler.CancelStudyLog(log);
                        if (string.IsNullOrEmpty(result))
                        {
                            ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消申请成功！');window.close();", true);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('取消申请失败！');window.close();", true);
                        }

                        BindDataList();
                        break;
                }
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roomList"></param>
        private void BindDataList()
        {
            int date = int.Parse(ddlDate.Items[ddlDate.SelectedIndex].Value);
            StringBuilder strRoomMessage = new StringBuilder();
            strRoomMessage.Append("<ul data-role='listview' data-divider-theme='d' data-inset='true'><li data-role='list-divider' role='heading'>申请记录列表</li>");
            List<StudyBookingLog> logs = handler.GetStudyLogList(LoginUserInfo.CardNo, date);// bll.GetReadingRoomSeatUsingState(roomNums, Session["SchoolConnectionString"].ToString());
            foreach (StudyBookingLog log in logs)
            {
                string status = "";
                switch (log.CheckState)
                {
                    case CheckStatus.Adopt: status = "通过审核"; break;
                    case CheckStatus.Cancel: status = "取消申请"; break;
                    case CheckStatus.Checking: status = "等待审核"; break;
                    case CheckStatus.Failure: status = "未通过审核"; break;
                }
                strRoomMessage.Append("<li data-theme='c'>" + log.BespeakTime.ToShortDateString() + " " + log.BespeakTime.ToShortTimeString() + " " + log.StudyRoomName
                    + "<ul><li>研习间信息</li>"
                    + "<li>编号：" + log.StudyRoomNo + "</li>"
                    + "<li>名称：" + log.StudyRoomName + "</li>"
                    + "<li>申请时间：" + log.BespeakTime.ToShortDateString() + " " + log.BespeakTime.ToShortTimeString() + "</li>"
                    + "<li>提交时间：" + log.SubmitTime.ToShortDateString() + " " + log.SubmitTime.ToShortTimeString() + "</li>"
                    + "<li>使用时长：" + log.UseTime + "</li>"
                    + "<li>申请状态：" + status + "</li>"
                    + "<li>会议名称：" + log.Application.MeetingName + "</li>"
                    + "<li>参与人数：" + log.Application.MembersCount + "</li>"
                    + "<li>设备需求：" + log.Application.UseProjector + "</li>"
                    + "<li>申请人信息</li>"
                    + "<li>申请人：" + log.Application.ApplicantName + "</li>"
                    + "<li>申请人类别：" + log.Application.ApplicantType + "</li>"
                    + "<li>证件号：" + log.Application.ApplicantCardNo + "</li>"
                    + "<li>申请人单位：" + log.Application.ApplicantDept + "</li>"
                    + "<li>联系电话：" + log.Application.ApplicantPhoneNum + "</li>"
                    + "<li>负责人信息</li>"
                    + "<li>负责人：" + log.Application.HeadPerson + "</li>"
                    + "<li>负责人类别：" + log.Application.HeadPersonType + "</li>"
                    + "<li>审核备注：" + log.Remark.Replace("\r\n", "<br/>") + "</li>"
                    + "<li>通知邮箱：" + log.Application.HeadPersonPhoneNum + "</li>"
                    + "<li>"
                    + "<div style=\"height:35px; text-align: center\">"
                    + "<div class=\"ui-block-a\" style=\"width: 33%; text-align: center\">"
                    + "<input data-inline=\"true\" value=\"返回\" data-mini=\"true\" type=\"button\" onclick=\"javascript:history.go(-1);\" />"
                    + "</div>");
                if (log.CheckState == CheckStatus.Failure || log.CheckState == CheckStatus.Checking)
                {
                    strRoomMessage.Append("<div class=\"ui-block-b\" style=\"width: 33%; text-align: center\">"
                    + "<input data-inline=\"true\" value=\"重新申请\" data-mini=\"true\" type=\"button\" onclick=\"location.href='BookStudyRoom.aspx?No=" + log.StudyRoomNo + "&ID=" + log.StudyID + "'\" />"
                    + "</div>"
                    + "<div class=\"ui-block-b\" style=\"width: 33%; text-align: center\">"
                    + "<input data-inline=\"true\" value=\"取消申请\" data-mini=\"true\" type=\"button\" onclick=\"location.href='CancelLog.aspx?ID=" + log.StudyID + "'\" />"
                    + "</div>");
                }
                strRoomMessage.Append("</div></li></ul></li>");
            }
            strRoomMessage.Append("</ul>");
            logList = strRoomMessage.ToString();
        }
    }
}