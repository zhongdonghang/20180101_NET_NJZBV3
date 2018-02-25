using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.EnumType;
using SeatClient.OperateResult;
using System.Drawing;

namespace SeatManage.SeatClient.Tip
{
    public partial class Tip_Framework : Form
    {

        SystemObject clientobject = SystemObject.GetInstance();
        HandleResult operateResule = HandleResult.Failed;
        /// <summary>
        /// 操作结果，成功或者失败
        /// </summary>
        public HandleResult OperateResule
        {
            get { return operateResule; }
            set { operateResule = value; }
        }
        FormCloseCountdown countDown = null;
        TipType tipType = TipType.None;
        int closeTime = 0;
        public Tip_Framework(TipType type, int closeTime)
        {
            InitializeComponent();

            lblTitleAd.Text = clientobject.ClientSetting.TitleAd;
            this.Location = new Point(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            tipType = type;
            this.closeTime = closeTime;
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        void countDown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    if (countDown.CountdownSceonds <= 0)
                    {
                        this.Close();
                        countDown.Stop();
                    }
                }
                catch
                { }
            }));
        }

        private void Tip_Framework_FormClosing(object sender, FormClosingEventArgs e)
        {
            countDown.Stop();
            countDown.EventCountdown -= new EventHandler(countDown_EventCountdown);
            this.Dispose();
        }

        private void Tip_Framework_Load(object sender, EventArgs e)
        {
            if (closeTime < 9)
            {
                countDown = new FormCloseCountdown(closeTime);
            }
            else
            {
                countDown = new FormCloseCountdown(9);
            }
            countDown.EventCountdown += new EventHandler(countDown_EventCountdown);
            //提示窗口初始化
            switch (tipType)
            {
                case TipType.SelectSeatFrequent:
                    GetMessage.SelectSeatFrequent(this);
                    return;
                case TipType.SelectSeatResult:
                    GetMessage.SelectSeatResult(this, OperateResule, tipType);
                    return;
                case TipType.BespeatSeatConfirmSuccess:
                    GetMessage.SelectSeatResult(this, HandleResult.Successed, tipType);
                    return;
                case TipType.IsBlacklist:
                    GetMessage.IsBlacklist(this);
                    return;
                case TipType.ShortLeave:
                    GetMessage.ShortLeavtTip(this);
                    return;
                case TipType.Leave:
                    GetMessage.LeaveTip(this);
                    break;
                case TipType.ComeToBack:
                    GetMessage.ComeToBack(this);
                    break;
                case TipType.SeatLocking:
                    GetMessage.SeatLocking(this);
                    break;
                case TipType.SeatNotExists:
                    GetMessage.SeatNotExists(this);
                    break;
                case TipType.SeatUsing:
                    GetMessage.SeatUsing(this, tipType);
                    break;
                case TipType.ReadingRoomClosing:
                    GetMessage.RoomClosing(this);
                    break;
                case TipType.WaitSeatSuccess:
                    GetMessage.WaitSeatSuccess(this);
                    break;
                case TipType.WaitSeatFrequent:
                case TipType.ReaderTypeInconformity:
                case TipType.ContinuedTime:
                case TipType.BeapeatRoomNotExists:
                case TipType.ContinuedTimeNoCount:
                case TipType.ContinuedTimeNotTime:
                case TipType.ContinuedTimeWithout:
                case TipType.WaitSeatWithSeat:
                case TipType.ReadingRoomFull:
                case TipType.AutoContinuedTime:
                case TipType.AutoContinuedTimeNoCount:
                case TipType.ShortLeaveSeatOverTime:
                case TipType.Exception:
                    GetMessage.WaitSeatFrequent(this, tipType);
                    break;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            countDown.Stop();
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            button1_Click(sender, e);
        }



    }
}
