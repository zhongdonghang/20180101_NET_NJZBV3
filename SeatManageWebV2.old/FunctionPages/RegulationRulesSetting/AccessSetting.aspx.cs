using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class AccessSetting : BasePage
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
                ASCheckChange();
            }
        }
        /// <summary>
        /// 隐藏设置选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsASUserd_CheckedChanged(object sender, EventArgs e)
        {
            ASCheckChange();
        }
        private void binding()
        {
            SeatManage.ClassModel.AccessSetting accset = SeatManage.Bll.T_SM_SystemSet.GetAccessSetting();
            if (accset == null)
            {
                accset = new SeatManage.ClassModel.AccessSetting();
            }
            IsELUserd.Checked = accset.EnterLib;
            IsOLUserd.Checked = accset.OutLib;
            IsASUserd.Checked = accset.IsUsed;
            cbBLIsUsed.Checked = accset.IsLimitBlackList;
            IsAddrv.Checked = accset.AddViolationRecords;
            LeaveTime.Text = accset.LeaveTimeSpan.ToString();
            ddlleavemode.SelectedValue = ((int)accset.LeaveMode).ToString();
            IsOnSeat.Checked = accset.IsReleaseOnSeat;
            IsShortLeave.Checked = accset.IsComeBack;
            IsBooking.Checked = accset.IsBookingConfinmed;

        }
        private void ASCheckChange()
        {
            if (IsASUserd.Checked)
            {
                IsELUserd.Hidden = false;
                IsOLUserd.Hidden = false;
                cbBLIsUsed.Hidden = false;
                ELChenkChange();
                OLChenkChange();
            }
            else
            {
                IsELUserd.Hidden = true;
                IsOLUserd.Hidden = true;
                cbBLIsUsed.Hidden = true;
                IsShortLeave.Hidden = true;
                IsOnSeat.Hidden = true;
                IsAddrv.Hidden = true;
                LeaveTime.Hidden = true;
                IsBooking.Hidden = true;
                ddlleavemode.Hidden = true;

            }
        }

        private void OLChenkChange()
        {
            if (IsOLUserd.Checked)
            {
                ddlleavemode.Hidden = false;
            }
            else
            {
                ddlleavemode.Hidden = true;
            }
        }

        private void ELChenkChange()
        {
            if (IsELUserd.Checked)
            {
                IsShortLeave.Hidden = false;
                IsOnSeat.Hidden = false;
                IsAddrv.Hidden = false;
                LeaveTime.Hidden = false;
                IsBooking.Hidden = false;
            }
            else
            {
                IsShortLeave.Hidden = true;
                IsOnSeat.Hidden = true;
                IsAddrv.Hidden = true;
                LeaveTime.Hidden = true;
                IsBooking.Hidden = true;
            }
        }
        /// <summary>
        /// 隐藏设置选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsELUserd_CheckedChanged(object sender, EventArgs e)
        {
            ELChenkChange();
        }
        /// <summary>
        /// 隐藏设置选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void IsOLUserd_CheckedChanged(object sender, EventArgs e)
        {
            OLChenkChange();
        }
        /// <summary>
        /// 提交设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.AccessSetting accset = new SeatManage.ClassModel.AccessSetting();
            accset.EnterLib = IsELUserd.Checked;
            accset.OutLib = IsOLUserd.Checked;
            accset.IsUsed = IsASUserd.Checked;
            accset.IsLimitBlackList = cbBLIsUsed.Checked;
            accset.AddViolationRecords = IsAddrv.Checked;
            accset.LeaveTimeSpan = int.Parse(LeaveTime.Text);
            accset.LeaveMode = (SeatManage.EnumType.EnterOutLogType)int.Parse(ddlleavemode.SelectedValue);
            accset.IsReleaseOnSeat = IsOnSeat.Checked;
            accset.IsComeBack = IsShortLeave.Checked;
            accset.IsBookingConfinmed = IsBooking.Checked;
            if (SeatManage.Bll.T_SM_SystemSet.UpdateAccessSetting(accset))
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