using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.BespeakStudyRoom
{
    public partial class BeskpeakStudyRoom : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.ServerVariables["HTTP_REFERER"] != null)
                {
                    string url = Request.ServerVariables["HTTP_REFERER"].Trim();
                    string pageName = SeatManage.SeatManageComm.SeatComm.GetPageName(url);
                    if (pageName != "StudyRoomList.aspx" && pageName != "FormSYS.aspx")
                    {
                        WriteLogs(url);
                        Response.Write("请通过正确方式访问网站！");
                        Response.End();
                        return;
                    }
                }
                else
                {
                    WriteLogs(HttpContext.Current.Request.Url.AbsoluteUri);
                    Response.Write("请通过正确方式访问网站！");
                    Response.End();
                    return;
                }
            }
            if (Request.QueryString["flag"] == "edit")
            {
                if (!IsPostBack)
                {
                    DataBind();
                }
                extform.Hidden = false;
                remarkform.Hidden = true;
                btnApp.Hidden = true;
                btnSubmit.Hidden = false;
            }
            else
            {
                string roomNo = Request.QueryString["roomNo"];
                if (string.IsNullOrEmpty(roomNo))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("数据获取失败！");
                }
                else
                {
                    SeatManage.ClassModel.StudyRoomInfo room = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(roomNo);
                    if (room == null)
                    {
                        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                        FineUI.Alert.ShowInTop("数据获取失败！");
                    }
                    else
                    {
                        if (!IsPostBack)
                        {
                            DateTime nowT = SeatManage.Bll.ServiceDateTime.Now;
                            tpBookingTime.MinTime = room.Setting.OpenTime;
                            tpBookingTime.MaxTime = room.Setting.CloseTime;
                            tpBookingTime.SelectedDate = room.Setting.OpenTime;
                            dpBookingDate.MinDate = nowT;
                            dpBookingDate.SelectedDate = nowT;
                            nbUseTime.MinValue = 1;
                            string[] cbul = room.Setting.CanUseFacilities.Split(';');
                            if (cbul.Length > 0)
                            {
                                cbUse.Items.Clear();
                                foreach (string item in cbul)
                                {
                                    if (string.IsNullOrEmpty(item))
                                    {
                                        continue;
                                    }
                                    cbUse.Items.Add(new CheckItem(item, item));
                                }
                            }
                            txtFacilitiesRenmark.InnerHtml = room.Setting.FacilitiesRenmark.Replace("\r\n", "<br/>").Replace(" ","&nbsp;");
                            txtPrecautions.InnerHtml = room.Setting.Precautions.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");
                            txtApplicationInfo.InnerHtml = room.Setting.ApplicationInfo.Replace("\r\n", "<br/>").Replace(" ", "&nbsp;");

                            imgRoomImage.ImageUrl = "~/StudyImage/" + room.RoomImage;
                            string u = Server.UrlEncode(imgRoomImage.ImageUrl);
                            btnImage.OnClientClick = WindowImage.GetShowReference(string.Format("BigImage.aspx?imageurl={0}", u), "大图");
                            extform.Hidden = true;
                            remarkform.Hidden = false;
                            btnSubmit.Hidden = true;
                            btnApp.Hidden = false;

                            SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(this.LoginId);
                            txtApplicantCardNo.Text = this.LoginId;
                            txtApplicantName.Text = user.UserName;

                        }

                    }
                }
            }
        }

        //protected void dpBookingDate_OnTextChanged(object sender, EventArgs e)
        //{
        //    string roomNo = Request.QueryString["roomNo"];
        //    if (string.IsNullOrEmpty(roomNo))
        //    {
        //        PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        //        FineUI.Alert.ShowInTop("数据获取失败！");
        //    }
        //    else
        //    {
        //        SeatManage.ClassModel.StudyRoomInfo room = SeatManage.Bll.StudyRoomOperation.GetSingleStudyRoonInfo(roomNo);
        //        if (room == null)
        //        {
        //            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        //            FineUI.Alert.ShowInTop("数据获取失败！");
        //        }
        //        else
        //        {
        //            DateTime nowT = SeatManage.Bll.ServiceDateTime.Now;
        //            tpBookingTime.MinTime = room.Setting.OpenTime;
        //            tpBookingTime.MaxTime = room.Setting.CloseTime;
        //            tpBookingTime.SelectedDate = room.Setting.OpenTime;
        //            dpBookingDate.MinDate = nowT;
        //            dpBookingDate.SelectedDate = nowT;
        //            if (nowT > room.Setting.CloseTime)
        //            {
        //                dpBookingDate.MinDate = nowT.AddDays(1);
        //                dpBookingDate.SelectedDate = nowT.AddDays(1);
        //            }
        //            else if (nowT > room.Setting.OpenTime)
        //            {
        //                tpBookingTime.MinTime = nowT.AddMinutes(-nowT.Minute).AddMinutes((nowT.Minute % 10 - 1) * 10);
        //                tpBookingTime.SelectedDate = nowT.AddMinutes(-nowT.Minute).AddMinutes((nowT.Minute % 10 - 1) * 10);
        //            }
        //        }
        //    }
        //}
        /// <summary>
        /// 数据绑定
        /// </summary>
        private void DataBind()
        {
            if (Request.QueryString["id"] == null)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("数据获取失败！");
            }
            int id = int.Parse(Request.QueryString["id"]);
            SeatManage.ClassModel.StudyBookingLog model = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogByID(id);
            if (model == null)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("数据获取失败！");
            }
            string[] cbul = model.RoomSetting.CanUseFacilities.Split(';');
            if (cbul.Length > 0)
            {
                foreach (string item in cbul)
                {
                    cbUse.Items.Add(new CheckItem(item, item));
                }
            }
            string[] useList = model.Application.UseProjector.Split(';');
            if (useList.Length > 0)
            {
                foreach (string item in useList)
                {
                    foreach (CheckItem ci in cbUse.Items)
                    {
                        if (ci.Value == item)
                        {
                            ci.Selected = true;
                        }
                    }
                }
            }

            txtApplicantCardNo.Text = model.Application.ApplicantCardNo;
            txtApplicantDept.Text = model.Application.ApplicantDept;
            txtApplicantName.Text = model.Application.ApplicantName;
            txtApplicantPhoneNum.Text = model.Application.ApplicantPhoneNum;
            txtApplicantType.Text = model.Application.ApplicantType;
            txtHeadPerson.Text = model.Application.HeadPerson;
            txtHeadPersonPhoneNum.Text = model.Application.HeadPersonPhoneNum;
            txtHeadPersonType.Text = model.Application.HeadPersonType;
            txtMeetingName.Text = model.Application.MeetingName;
            txtMembersCount.Text = model.Application.MembersCount.ToString();
            tpBookingTime.SelectedDate = model.BespeakTime;
            dpBookingDate.SelectedDate = model.BespeakTime;
            nbUseTime.Text = model.UseTime.ToString();
            txtemail.Text = model.Application.EmailAddress;
        }
        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            int id = -1;
            SeatManage.ClassModel.StudyBookingLog model = null; ;
            if (Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
                model = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogByID(id);
            }
            if (model == null)
            {
                model = new SeatManage.ClassModel.StudyBookingLog();
            }
            model.Application.ApplicantCardNo = txtApplicantCardNo.Text;
            model.Application.ApplicantDept = txtApplicantDept.Text;
            model.Application.ApplicantName = txtApplicantName.Text;
            model.Application.ApplicantPhoneNum = txtApplicantPhoneNum.Text;
            model.Application.ApplicantType = txtApplicantType.Text;
            model.Application.HeadPerson = txtHeadPerson.Text;
            model.Application.HeadPersonPhoneNum = txtHeadPersonPhoneNum.Text;
            model.Application.HeadPersonType = txtHeadPersonType.Text;
            model.Application.MeetingName = txtMeetingName.Text;
            model.Application.MembersCount = int.Parse(txtMembersCount.Text);
            model.Application.UseProjector = "";
            foreach (CheckItem ci in cbUse.Items)
            {
                if (ci.Selected)
                {
                    model.Application.UseProjector += ci.Value + ";";
                }
            }
            model.BespeakTime = DateTime.Parse(dpBookingDate.Text + " " + tpBookingTime.Text);
            model.UseTime = int.Parse(nbUseTime.Text);
            if (Request.QueryString["flag"] != "edit")
            {
                model.StudyRoomNo = Request.QueryString["roomNo"];
            }
            model.SubmitTime = SeatManage.Bll.ServiceDateTime.Now;
            model.CheckState = SeatManage.EnumType.CheckStatus.Checking;
            model.CardNo = this.LoginId;
            model.Application.EmailAddress = txtemail.Text;
            string errorMessage = SeatManage.Bll.StudyRoomOperation.CheckBookTime(model.BespeakTime, model.UseTime, model.StudyRoomNo, model.StudyID);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                FineUI.Alert.ShowInTop(errorMessage);
                return;
            }
            if (Request.QueryString["flag"] == "edit")
            {
                if (SeatManage.Bll.StudyRoomOperation.UpdateStudyBookingLog(model))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("提交成功！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("保存失败！");
                }
            }
            else
            {
                if (SeatManage.Bll.StudyRoomOperation.AddStudyBookingLog(model))
                {
                    PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                    FineUI.Alert.ShowInTop("提交成功！");
                }
                else
                {
                    FineUI.Alert.ShowInTop("保存失败！");
                }
            }

        }
        protected void btnApp_OnClick(object sender, EventArgs e)
        {
            extform.Hidden = false;
            remarkform.Hidden = true;
            btnApp.Hidden = true;
            btnSubmit.Hidden = false;
        }
    }
}