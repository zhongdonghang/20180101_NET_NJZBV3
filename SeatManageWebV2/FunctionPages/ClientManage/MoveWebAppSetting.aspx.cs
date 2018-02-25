using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SeatManageWebV2.FunctionPages.ClientManage
{
    public partial class MoveWebAppSetting : BasePage
    {
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataBind();
            }
        }
        /// <summary>
        /// 数据保存
        /// </summary>
        private void DataBind()
        {
            AuthorizeVerify.FunctionAuthorizeInfo authorize = SeatManage.Bll.AuthorizationOperation.GetFunctionAuthorize();
            SeatManage.ClassModel.MoveWebAppSetting model = SeatManage.Bll.T_SM_SystemSet.GetMoveWebAppSetting();
            if (model == null)
            {
                model = new SeatManage.ClassModel.MoveWebAppSetting();
            }
            //预约配置
            BespeakSeat.Checked = model.IsUseBespeak;
            //if (!model.IsUseBespeak)
            //{
            //    BespeakSeat_Area.Style["display"] = "none";
            //}
            BespeakNextDay.Checked = model.IsUseNextDayBespeak;
            BespeakNowDay.Checked = model.IsUseNowDayBespeak;
            //if (authorize != null && !authorize.SystemFunction.Contains("Bespeak_NowDay"))
            //{
            //    nowDayTD.Style["display"] = "none";
            //}
            //座位操作配置
            ShortLeave.Checked = model.IsCanShortLeave;
            Leave.Checked = model.IsCanLeave;
            Dcode.Checked = model.IsUseDcode;
            //二维码配置
            //if (!model.IsUseDcode)
            //{
            //    Dcode_Area.Style["display"] = "none";
            //}
            DcodeCheck.Checked = model.IsCanDcodeCheckTime;
            DcodeComeBack.Checked = model.IsCanDcodeComeBack;
            DcodeContnueTime.Checked = model.IsCanDcodeContinueTime;
            DcodeLeave.Checked = model.IsCanDcodeLeave;
            DcodeReselectSeat.Checked = model.IsCanDcodeReselectSeat;
            DcodeSelectSeat.Checked = model.IsCanDcodeSelectSeat;
            //if (authorize != null && !authorize.SystemFunction.Contains("MoveClient_SeatSelect"))
            //{
            //    selectSeatTR.Style["display"] = "none";
            //}
            DcodeShortLeave.Checked = model.IsCanDcodeShortLeave;
            DcodeWaitSeat.Checked = model.IsCanDcodeWaitSeat;
            //if (authorize != null && !authorize.SystemFunction.Contains("MoveClient_SeatWait"))
            //{
            //    waitSeatTD.Style["display"] = "none";
            //}
            //if (authorize != null && !authorize.SystemFunction.Contains("MoveClient_QRcodeDecode"))
            //{
            //    dcodeT.Style["display"] = "none";
            //}
        }
        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Save_Click(object sender, EventArgs e)
        {
            SeatManage.ClassModel.MoveWebAppSetting model = new SeatManage.ClassModel.MoveWebAppSetting();
            model.IsUseBespeak = BespeakSeat.Checked;
            model.IsUseNextDayBespeak = BespeakNextDay.Checked;
            model.IsUseNowDayBespeak = BespeakNowDay.Checked;
            model.IsCanShortLeave = ShortLeave.Checked;
            model.IsCanLeave = Leave.Checked;
            model.IsUseDcode = Dcode.Checked;
            model.IsCanDcodeCheckTime = DcodeCheck.Checked;
            model.IsCanDcodeComeBack = DcodeComeBack.Checked;
            model.IsCanDcodeContinueTime = DcodeContnueTime.Checked;
            model.IsCanDcodeLeave = DcodeLeave.Checked;
            model.IsCanDcodeReselectSeat = DcodeReselectSeat.Checked;
            model.IsCanDcodeSelectSeat = DcodeSelectSeat.Checked;
            model.IsCanDcodeShortLeave = DcodeShortLeave.Checked;
            model.IsCanDcodeWaitSeat = DcodeWaitSeat.Checked;
            if (SeatManage.Bll.T_SM_SystemSet.SaveMoveWebAppSetting(model))
            {
                FineUI.Alert.ShowInTop("保存成功！");
            }
            else
            {
                FineUI.Alert.ShowInTop("保存失败！");
            }
        }

    }
}