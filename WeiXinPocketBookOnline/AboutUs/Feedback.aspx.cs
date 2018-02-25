using System;

namespace WeiXinPocketBookOnline.AboutUs
{
    public partial class Feedback : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.LoginUserInfo == null || this.UserSchoolInfo == null)
            {
                Response.Redirect(LoginUrl());
                return;
            }
            string cmd = Request.Form["subCmd"];
            #region 暂时注释
            //switch (cmd)
            //{
            //    case "LoginOut":
            //        Session.Clear();
            //        Response.Cookies["userInfo"].Expires = DateTime.Now.AddDays(-1);
            //        SeatManage.SeatManageComm.CookiesManager.RefreshNum = 0;
            //        Response.Redirect(LoginUrl());
            //        break;
            //    case "addFeedback":
            //        string remark = txtFeedback.Value.Trim();
            //        IFeedbackBll feedbackBll = new PocketBespeak_Feedback();
            //        string cardNo = this.LoginUserInfo.StudentNo;
            //        string schoolId = this.UserSchoolInfo.Id.ToString();
            //        AMS.Model.AMS_Feedback model = new  AMS.Model.AMS_Feedback();
            //        model.CardNo = cardNo;
            //        model.Remark = remark;
            //        model.SchoolId = schoolId;
            //        AMS.Model.Enum.HandleResult result = feedbackBll.AddFeedback(model);
            //        if (result == AMS.Model.Enum.HandleResult.Successed)
            //        {
            //            spanWarmInfo.Visible = true;
            //            spanWarmInfo.InnerText = "提交成功，感谢您的建议，我们会不断改进！";
            //        }
            //        else
            //        {
            //            spanWarmInfo.Visible = true;
            //            spanWarmInfo.InnerText = "提交失败，当前网络可能不可用，请稍后再试！";
            //        }
            //        break;

            //}
            #endregion
        }
    }
}