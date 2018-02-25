using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.Bll;

namespace SeatClient
{
    public partial class BookConfirmWarn : Form
    {
        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown countdown;
        public BookConfirmWarn()
        {
            InitializeComponent();

            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
            countdown = new FormCloseCountdown(9);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        void countdown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    if (this.countdown.CountdownSceonds <= 0)
                    {
                        this.Close();
                    }
                }
                catch { }
            }));
        }
        private string beginTime = "";
        /// <summary>
        /// 预约入座限制的开始时间
        /// </summary>
        public string BeginTime
        {
            get { return beginTime; }
            set { beginTime = value; }
        }
        private string endTime = "";
        /// <summary>
        /// 预约入座限制的结束时间
        /// </summary>
        public string EndTime
        {
            get { return endTime; }
            set { endTime = value; }
        }
        private void btnSure_Click(object sender, EventArgs e)
        {
            try
            {
                BespeakLogInfo bespeaklog = clientObject.EnterOutLogData.Student.BespeakLog[0];
                bespeaklog.BsepeakState = BookingStatus.Cencaled;
                bespeaklog.CancelPerson = Operation.Reader;
                bespeaklog.CancelTime = ServiceDateTime.Now;
                bespeaklog.Remark = string.Format("在终端{0}刷卡取消{1}，{2}号座位的预约。", clientObject.ClientSetting.ClientNo, bespeaklog.ReadingRoomName, bespeaklog.ShortSeatNum);
                int i = T_SM_SeatBespeak.UpdateBespeakList(bespeaklog);
                if (i > 0)
                {
                    this.btnSure.Visible = false;
                    this.panel1.Visible = false;
                    this.lblCancelMessage.Text = "  您已经取消预约的座位，请重新刷卡选选座。";
                }
            }
            catch
            {
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BookConfirmWarn_FormClosing(object sender, FormClosingEventArgs e)
        {
            countdown.EventCountdown -= countdown_EventCountdown;
            this.Dispose();
        }

        private void BookConfirmWarn_Load(object sender, EventArgs e)
        {
            lblAheadTime.Text = this.beginTime;
            lblDelayTime.Text = this.endTime;
        }
    }
}
