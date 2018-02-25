using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace StudyRoomWeb.FunctionPages.StudyRoomManage
{
    public partial class StudyBookingLogCheck : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

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
            txtRemark.Hidden = true; ;
            lbRemark.Hidden = false;
            lbRemark.Text = model.Remark;

        }
    }
}