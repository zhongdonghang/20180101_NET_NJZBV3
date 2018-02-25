using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class PecketWebSetting : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                binding();
            }
        }
        private void binding()
        {
            SeatManage.ClassModel.PecketBookWebSetting setting = SeatManage.Bll.T_SM_SystemSet.GetPecketWebSetting();
            if (setting == null)
            {
                setting = new SeatManage.ClassModel.PecketBookWebSetting();
            }
            cb_UseBookComfirm.Checked = setting.UseBookComfirm;
            cb_UseBookNextDaySeat.Checked = setting.UseBookNextDaySeat;
            cb_UseBookNowDaySeat.Checked = setting.UseBookNowDaySeat;
            cb_UseBookSeat.Checked = setting.UseBookSeat;
            cb_UseCancelBook.Checked = setting.UseCancelBook;
            cb_UseCancelWait.Checked = setting.UseCancelWait;
            cb_UseCanLeave.Checked = setting.UseCanLeave;
            cb_UseComeBack.Checked = setting.UseComeBack;
            cb_UseContinue.Checked = setting.UseContinue;
            cb_UseShortLeave.Checked = setting.UseShortLeave;
            cb_UseWaitSeat.Checked = setting.UseWaitSeat;
            cb_ChangeSeat.Checked = setting.UseChangeSeat;
            cb_SelectSeat.Checked = setting.UseSelectSeat;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.PecketBookWebSetting setting = new SeatManage.ClassModel.PecketBookWebSetting();
            setting.UseBookComfirm = cb_UseBookComfirm.Checked;
            setting.UseBookNextDaySeat = cb_UseBookNextDaySeat.Checked;
            setting.UseBookNowDaySeat = cb_UseBookNowDaySeat.Checked;
            setting.UseBookSeat = cb_UseBookSeat.Checked;
            setting.UseCancelBook = cb_UseCancelBook.Checked;
            setting.UseCancelWait = cb_UseCancelWait.Checked;
            setting.UseCanLeave = cb_UseCanLeave.Checked;
            setting.UseComeBack = cb_UseComeBack.Checked;
            setting.UseContinue = cb_UseContinue.Checked;
            setting.UseShortLeave = cb_UseShortLeave.Checked;
            setting.UseWaitSeat = cb_UseWaitSeat.Checked;
            setting.UseSelectSeat = cb_SelectSeat.Checked;
            setting.UseChangeSeat = cb_ChangeSeat.Checked;

            if (SeatManage.Bll.T_SM_SystemSet.UpdatePecketWebSetting(setting))
            {
                FineUI.Alert.Show("保存成功！");
            }
            else
            {
                FineUI.Alert.Show("保存失败！");
            }
        }
    }
}