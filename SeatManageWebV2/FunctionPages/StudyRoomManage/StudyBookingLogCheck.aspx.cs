using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace SeatManageWebV2.FunctionPages.StudyRoomManage
{
    public partial class StudyBookingLogCheck : BasePage
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

            if (!IsPostBack)
            {
                DataBind();
            }

        }


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
            cbIsUseProjector.Text = model.Application.UseProjector;
            tpBookingTime.Text = model.BespeakTime.ToShortTimeString();
            dpBookingDate.Text = model.BespeakTime.ToShortDateString();
            nbUseTime.Text = model.UseTime.ToString();
            if (Request.QueryString["flag"] == "edit")
            {
                txtRemark.Hidden = false;
                lbRemark.Hidden = true;
                txtRemark.Text = model.Remark;
            }
            else
            {
                txtRemark.Hidden = true; ;
                lbRemark.Hidden = false;
                lbRemark.Text = model.Remark;
                btnSubmit_NO.Hidden = true;
                btnSubmit_OK.Hidden = true;
            }
        }

        protected void btnSubmit_OK_OnClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            SeatManage.ClassModel.StudyBookingLog model = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogByID(id);
            if (model == null)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("保存数据获取失败！");
            }
            model.Remark = txtRemark.Text;
            model.CheckTime = SeatManage.Bll.ServiceDateTime.Now;
            model.CheckState = SeatManage.EnumType.CheckStatus.Adopt;
            model.ChecklPerson = this.LoginId;
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
        protected void btnSubmit_NO_OnClick(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            SeatManage.ClassModel.StudyBookingLog model = SeatManage.Bll.StudyRoomOperation.GetStudyBookingLogByID(id);
            if (model == null)
            {
                PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
                FineUI.Alert.ShowInTop("保存数据获取失败！");
            }
            model.Remark = txtRemark.Text;
            model.CheckTime = SeatManage.Bll.ServiceDateTime.Now;
            model.CheckState = SeatManage.EnumType.CheckStatus.Failure;
            model.ChecklPerson = this.LoginId;
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
    }
}