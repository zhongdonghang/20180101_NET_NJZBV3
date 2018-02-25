using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SeatManage.Bll;
using SeatManage.EnumType;

namespace SeatManageWebV2.FunctionPages.SeatBespeak
{
    public partial class BespeakSelfSeat : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DateBind();
            }
        }

        private void DateBind()
        {
            SeatManage.ClassModel.ReaderInfo reader = EnterOutOperate.GetReaderInfo(this.LoginId);
            if (reader.AtReadingRoom == null)
            {
                lblSeatInfo.Text = "无";
                btnDelayTime.Enabled = false;
                btnLeave.Enabled = false;
                btnShortLeave.Enabled = false;
                return;
            }

            SeatManage.ClassModel.ReadingRoomSetting roomSet = reader.AtReadingRoom.Setting;
            if (roomSet.SeatUsedTimeLimit.Used && roomSet.SeatUsedTimeLimit.IsCanContinuedTime)
            {
                btnDelayTime.Enabled = roomSet.SeatBespeak.AllowDelayTime;
            }
            else
            {
                btnDelayTime.Enabled = false;
            }
            btnLeave.Enabled = roomSet.SeatBespeak.AllowLeave;
            btnShortLeave.Enabled = roomSet.SeatBespeak.AllowShortLeave;


            if (reader.EnterOutLog != null && reader.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
            {
                switch (reader.EnterOutLog.EnterOutState)
                {
                    case EnterOutLogType.ShortLeave:
                        lblSeatInfo.Text = string.Format("{0} {1}", reader.EnterOutLog.ReadingRoomName, reader.EnterOutLog.ShortSeatNo);
                        lblState.Text = "暂离";
                        lblHoldTime.Text = reader.EnterOutLog.EnterOutTime.ToString();
                        btnShortLeave.Enabled = false;
                        btnDelayTime.Enabled = false;
                        break;
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                        lblSeatInfo.Text = string.Format("{0} {1}", reader.EnterOutLog.ReadingRoomName, reader.EnterOutLog.ShortSeatNo);
                        lblState.Text = "在座";
                        lblHoldTime.Text = reader.EnterOutLog.EnterOutTime.ToString();
                        break;
                    default:
                        btnDelayTime.Enabled = false;
                        btnLeave.Enabled = false;
                        btnShortLeave.Enabled = false;
                        break;
                }
            }
            else
            {
                lblSeatInfo.Text = "无";
                btnDelayTime.Enabled = false;
                btnLeave.Enabled = false;
                btnShortLeave.Enabled = false;
            }
        }

        protected void btnDelayTime_OnClick(object sender, EventArgs e)
        {
            SeatManage.ClassModel.ReaderInfo reader = EnterOutOperate.GetReaderInfo(this.LoginId);
            reader.EnterOutLog.Remark = "通过预约网站延长座位使用时间";
            reader.EnterOutLog.EnterOutState = EnterOutLogType.ContinuedTime;
            string result = EnterOutOperate.DelaySeatUsedTime(reader);
            if (!string.IsNullOrEmpty(result))
            {
                lblMsg.Text = result;
            }
            else
            {
                lblMsg.Text = "操作成功";
            }
        }

        protected void btnShortLeave_OnClick(object sender, EventArgs e)
        {
            try
            {
                SeatManage.ClassModel.ReaderInfo reader = EnterOutOperate.GetReaderInfo(this.LoginId);
                int newLogId = -1;
                reader.EnterOutLog.EnterOutState = EnterOutLogType.ShortLeave;
                reader.EnterOutLog.Flag = Operation.Reader;
                reader.EnterOutLog.Remark = "通过预约网站设置暂离";
                HandleResult reault = EnterOutOperate.AddEnterOutLog(reader.EnterOutLog, ref newLogId);
                if (reault == HandleResult.Successed)
                {
                    lblMsg.Text = "操作成功";
                    DateBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "未知错误，操作失败";
            }
        }

        protected void btnLeave_OnClick(object sender, EventArgs e)
        {
            try
            {
                SeatManage.ClassModel.ReaderInfo reader = EnterOutOperate.GetReaderInfo(this.LoginId);
                int newLogId = -1;
                reader.EnterOutLog.EnterOutState = EnterOutLogType.Leave;
                reader.EnterOutLog.Flag = Operation.Reader;
                reader.EnterOutLog.Remark = "通过预约网站释放座位";
                HandleResult reault = EnterOutOperate.AddEnterOutLog(reader.EnterOutLog, ref newLogId);
                if (reault == HandleResult.Successed)
                {
                    lblMsg.Text = "操作成功";
                    DateBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = "未知错误，操作失败";
            }
        }
    }
}