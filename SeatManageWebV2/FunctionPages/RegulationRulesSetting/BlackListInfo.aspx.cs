using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class BlackListInfo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!OpVerifiction())
                {
                    Response.Write("请使用正常方式访问网站！");
                    Response.End();
                }
                DataBind();
            }
        }
        private void DataBind()
        {
            SeatManage.ClassModel.BlacklistSetting blacklistset = SeatManage.Bll.T_SM_SystemSet.GetRegulationRulesSetting().BlacklistSet;
            IsBlUserd.Checked = blacklistset.Used;
            nbvrcont.Text = blacklistset.ViolateTimes.ToString();
            ddlleavemode.SelectedValue = ((int)blacklistset.LeaveBlacklist).ToString();
            nbleavetime.Text = blacklistset.LimitDays.ToString();
            nbvrovertime.Text = blacklistset.ViolateFailDays.ToString();
            cbBookOverTime.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.BookingTimeOut];
            //cbCancelWaitByAdmin.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.CancelWaitByAdmin]; 
            cbLeaveByAdmin.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin];
            cbSeatOverTime.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.SeatOutTime];
            cbShortLeaveByAdmin.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime];
            cbShortLeaveByReader.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime];
            cbShortLeaveOverTime.Checked = blacklistset.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime];
            CheckedChanged();
        }
        /// <summary>
        /// 提交设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.RegulationRulesSetting rulessetting = new SeatManage.ClassModel.RegulationRulesSetting();
            rulessetting.BlacklistSet.Used = IsBlUserd.Checked;
            rulessetting.BlacklistSet.ViolateTimes = int.Parse(nbvrcont.Text);
            rulessetting.BlacklistSet.LeaveBlacklist = (SeatManage.EnumType.LeaveBlacklistMode)int.Parse(ddlleavemode.SelectedValue);
            rulessetting.BlacklistSet.LimitDays = int.Parse(nbleavetime.Text);
            rulessetting.BlacklistSet.ViolateFailDays = int.Parse(nbvrovertime.Text);
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.BookingTimeOut] = cbBookOverTime.Checked;
            //rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.CancelWaitByAdmin] = cbCancelWaitByAdmin.Checked;
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.LeaveByAdmin] = cbLeaveByAdmin.Checked;
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.SeatOutTime] = cbSeatOverTime.Checked;
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByAdminOutTime] = cbShortLeaveByAdmin.Checked;
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveByReaderOutTime] = cbShortLeaveByReader.Checked;
            rulessetting.BlacklistSet.ViolateRoule[SeatManage.EnumType.ViolationRecordsType.ShortLeaveOutTime] = cbShortLeaveOverTime.Checked;
            if (SeatManage.Bll.T_SM_SystemSet.UpdateRegulationRulesSetting(rulessetting))
            {
                FineUI.Alert.Show("修改成功！");
            }
            else
            {
                FineUI.Alert.Show("修改失败！");
            }

        }
        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReset_Click(object sender, EventArgs e)
        {
            DataBind();
        }
        /// <summary>
        /// 隐藏设置选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsBlUserd_CheckedChanged(object sender, EventArgs e)
        {
            CheckedChanged();
        }

        private void CheckedChanged()
        {
            if (IsBlUserd.Checked)
            {
                label1.Hidden = false;
                label3.Hidden = false;
                label2.Hidden = false;
                nbvrcont.Hidden = false;
                ddlleavemode.Hidden = false;
                nbleavetime.Hidden = false;
                nbvrovertime.Hidden = false;
                cbBookOverTime.Hidden = false;
                cbLeaveByAdmin.Hidden = false;
                cbSeatOverTime.Hidden = false;
                cbShortLeaveByAdmin.Hidden = false;
                cbShortLeaveByReader.Hidden = false;
                cbShortLeaveOverTime.Hidden = false;
            }
            else
            {
                label1.Hidden = true;
                label2.Hidden = true;
                label3.Hidden = true;
                nbvrcont.Hidden = true;
                ddlleavemode.Hidden = true;
                nbleavetime.Hidden = true;
                nbvrovertime.Hidden = true;
                cbBookOverTime.Hidden = true;
                cbLeaveByAdmin.Hidden = true;
                cbSeatOverTime.Hidden = true;
                cbShortLeaveByAdmin.Hidden = true;
                cbShortLeaveByReader.Hidden = true;
                cbShortLeaveOverTime.Hidden = true;
            }
        }


    }
}