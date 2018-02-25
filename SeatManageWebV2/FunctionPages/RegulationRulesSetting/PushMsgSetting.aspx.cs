using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.RegulationRulesSetting
{
    public partial class PushMsgSetting : BasePage
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
            SeatManage.ClassModel.PushMsssageSetting setting = SeatManage.Bll.T_SM_SystemSet.GetMsgPushSet() ?? new SeatManage.ClassModel.PushMsssageSetting();
            cb_AdminOperation.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.AdminOperation];
            cb_EnterVr.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.EnterVR];
            cb_EnterBlack.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.EnterBlack];
            cb_LeaveVrBlack.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.LeaveVrBlack];
            cb_OtherUser.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.OtherUser];
            cb_TimeOut.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.TimeOut];
            cb_UserOperation.Checked = setting.PushSetting[SeatManage.EnumType.MsgPushType.UserOperation];
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.PushMsssageSetting setting = new SeatManage.ClassModel.PushMsssageSetting();
            setting.PushSetting[SeatManage.EnumType.MsgPushType.AdminOperation] = cb_AdminOperation.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.EnterVR] = cb_EnterVr.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.EnterBlack] = cb_EnterBlack.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.LeaveVrBlack] = cb_LeaveVrBlack.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.OtherUser] = cb_OtherUser.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.TimeOut] = cb_TimeOut.Checked;
            setting.PushSetting[SeatManage.EnumType.MsgPushType.UserOperation] = cb_UserOperation.Checked;
            if (SeatManage.Bll.T_SM_SystemSet.SaveMsgPushSet(setting))
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