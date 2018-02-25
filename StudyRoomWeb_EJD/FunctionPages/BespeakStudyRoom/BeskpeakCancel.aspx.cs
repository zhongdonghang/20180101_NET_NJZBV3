using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUI;

namespace StudyRoomWeb.FunctionPages.BespeakStudyRoom
{
    public partial class BeskpeakCancel : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

            }
        }
        protected void Submit_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRemark.Text) || txtRemark.Text.Length < 5)
            {
                FineUI.Alert.ShowInTop("取消原因不能少于5个字！");
                return;
            }
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
            model.Remark = txtRemark.Text;
            model.CheckState = SeatManage.EnumType.CheckStatus.Cancel;
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