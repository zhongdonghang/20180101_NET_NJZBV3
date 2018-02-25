using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SchoolPocketBookOnlineV2.StudyRoom
{
    public partial class BookStudyRoom : BasePage
    {
        public string deList = "";
        private string roomNo = "";
        private int logID = -1;
        SeatManage.IPocketBespeak.IBookStudyRoom handler = new SeatManage.PocketBespeak.PecketBespeak_BookStudyRoom();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
            }
            roomNo = Request.QueryString["No"];
            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                logID = int.Parse(Request.QueryString["ID"]);
            }
            if (!IsPostBack)
            {
                txtBookDate.Value = DateTime.Now.ToShortDateString();
                SeatManage.ClassModel.StudyRoomSetting setting;
                SeatManage.ClassModel.StudyBookingLog log;
                if (logID < 0)
                {
                    setting = handler.GetStudyRoomInfo(this.UserSchoolInfo, roomNo).Setting;
                    log = new SeatManage.ClassModel.StudyBookingLog();
                    log.Application.ApplicantCardNo = this.LoginUserInfo.CardNo;
                    log.Application.ApplicantName = this.LoginUserInfo.Name;
                    log.BespeakTime = DateTime.Now;
                }
                else
                {
                    log = handler.GetStudyLog(this.UserSchoolInfo, logID);
                    setting = log.RoomSetting;
                }
                BindUI(setting, log);
            }
            string cmd = Request.Form["subCmd"];
            if (IsPostBack)
            {
                switch (cmd)
                {
                    case "LoginOut":
                        Session.Clear();
                        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
                        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
                        Response.Redirect(LogoutUrl());
                        break;
                    case "query":
                        Submit();
                        break;
                }
            }
        }
        private void Submit()
        {

            SeatManage.ClassModel.StudyBookingLog log;
            SeatManage.ClassModel.StudyRoomSetting setting;
            if (logID < 0)
            {
                log = new SeatManage.ClassModel.StudyBookingLog();
                setting = handler.GetStudyRoomInfo(this.UserSchoolInfo, roomNo).Setting;
                log.StudyRoomNo = roomNo;
            }
            else
            {
                log = handler.GetStudyLog(this.UserSchoolInfo, logID);
                setting = log.RoomSetting;
            }
            if (!setting.IsUseStudyRoom)
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "本研习间暂不开放";
                ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('本研习间暂不开放！');location.href='StudyRoomList.aspx';", true);
                return;
            }
            log.Application.ApplicantCardNo = txtApplicantCardNo.Value;
            log.Application.ApplicantDept = txtApplicantDept.Value;
            log.Application.ApplicantName = txtApplicantName.Value;
            log.Application.ApplicantPhoneNum = txtApplicantPhoneNum.Value;
            log.Application.ApplicantType = txtApplicantType.Value;
            log.BespeakTime = DateTime.Parse(txtBookDate.Value + " " + slbookTime.Value);
            log.SubmitTime = DateTime.Now;
            log.Application.EmailAddress = txtEmailAddress.Value;
            log.Application.HeadPerson = txtHeadPerson.Value;
            log.Application.HeadPersonPhoneNum = txtHeadPersonPhoneNum.Value;
            log.Application.HeadPersonType = txtHeadPersonType.Value;
            log.Application.MeetingName = txtMeetingName.Value;
            log.Application.MembersCount = int.Parse(txtMembersCount.Value);
            log.UseTime = int.Parse(txtUseTime.Value);
            log.CardNo = txtApplicantCardNo.Value;
            log.CheckState = SeatManage.EnumType.CheckStatus.Checking;
            log.Application.UseProjector = "";
            string[] cbulog = derlist.Value.Split(';');
            List<string> cbul = new List<string>();
            if (cbulog.Length > 0)
            {
                foreach (string c in cbulog)
                {
                    if (string.IsNullOrEmpty(c) || (cbul.Find(u => u == c) != null))
                    {
                        continue;
                    }
                    cbul.Add(c);
                    log.Application.UseProjector += c + ";";
                }
            }
            string resultValue = handler.SubmitStudyLog(this.UserSchoolInfo, log);
            if (!string.IsNullOrEmpty(resultValue))
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = resultValue;
                BindUI(setting, log);
            }
            else
            {
                spanWarmInfo.Visible = true;
                spanWarmInfo.InnerText = "操作成功";
                ClientScript.RegisterStartupScript(GetType(), "closewindow", "alert('申请提交成功！');location.href='StudyFunChoose.aspx';", true);
            }
        }
        private void BindData()
        {


        }

        private void BindUI(SeatManage.ClassModel.StudyRoomSetting setting, SeatManage.ClassModel.StudyBookingLog log)
        {
            derlist.Value = "";
            DateTime sd = setting.OpenTime;
            while (true)
            {
                slbookTime.Items.Add(new ListItem(sd.ToShortTimeString(), sd.ToShortTimeString()));
                sd = sd.AddMinutes(10);
                if (sd >= setting.CloseTime)
                {
                    break;
                }
            }
            string[] cbul = setting.CanUseFacilities.Split(';');
            if (cbul.Length > 0)
            {
                for (int i = 0; i < cbul.Length; i++)
                {
                    if (string.IsNullOrEmpty(cbul[i]))
                    {
                        continue;
                    }
                    bool ischeck = false;
                    string[] cbulog = log.Application.UseProjector.Split(';');
                    if (cbulog.Length > 0)
                    {
                        foreach (string c in cbulog)
                        {
                            if (cbul[i] == c)
                            {
                                ischeck = true;
                                derlist.Value += c + ";";
                                break;
                            }
                        }
                    }
                    deList += string.Format("<tr>"
                            + "<td style=\"width: 20px\">"
                            + "<input id=\"{0}\" name=\"{0}\" type=\"checkbox\" value=\"{1}\" style=\"height: 15px;width: 15px; left: 0px; margin-top: -3px;\" {2} onclick=\"checkDei('cb_" + i + "')\" />"
                            + "</td>"
                            + "<td style=\"padding-bottom: 0px; padding-left: 0px; padding-right: 0px; padding-top: 10px\">{1}</td>"
                            + "</tr>", "cb_" + i, cbul[i], ischeck ? "checked=\"true\"" : "");
                }
            }
            txtApplicantCardNo.Value = log.Application.ApplicantCardNo;
            txtApplicantDept.Value = log.Application.ApplicantDept;
            txtApplicantName.Value = log.Application.ApplicantName;
            txtApplicantPhoneNum.Value = log.Application.ApplicantPhoneNum;
            txtApplicantType.Value = log.Application.ApplicantType;
            txtBookDate.Value = log.BespeakTime.ToShortDateString();
            txtEmailAddress.Value = log.Application.EmailAddress;
            txtHeadPerson.Value = log.Application.HeadPerson;
            txtHeadPersonPhoneNum.Value = log.Application.HeadPersonPhoneNum;
            txtHeadPersonType.Value = log.Application.HeadPersonType;
            txtMeetingName.Value = log.Application.MeetingName;
            txtMembersCount.Value = log.Application.MembersCount.ToString();
            txtUseTime.Value = log.UseTime.ToString();
            slbookTime.Value = log.BespeakTime.ToShortTimeString();
        }
    }
}