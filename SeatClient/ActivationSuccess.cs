using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;

namespace SeatClient
{
    public partial class ActivationSuccess : Form
    {
        SystemObject clientobject = SystemObject.GetInstance();
        SeatClient.OperateResult.FormCloseCountdown countdown = null;
        public ActivationSuccess(string cardNo, string pwd)
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.Location = new Point(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            lblTitleAd.Text = clientobject.ClientSetting.TitleAd;
            this.lblCardNo.Text = cardNo;
            this.lblPwd.Text = pwd;
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
            FormCloseCountdown countdown = sender as FormCloseCountdown;
            this.Invoke(new Action(() =>
            {
                if (countdown.CountdownSceonds <= 0)
                {
                    this.countdown.Stop();
                    this.Close();
                    this.Dispose();
                }
            }));

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.countdown.Stop();
            this.Close();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            this.countdown.Stop();
            this.Close();
        }

        private void ActivationSuccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown);
            this.Dispose();
        }
    }
}
