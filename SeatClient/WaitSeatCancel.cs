using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;

namespace SeatClient
{
    public partial class WaitSeatCancel : Form
    {
        FormCloseCountdown countDown = null;
        SystemObject clientObject = SystemObject.GetInstance();
        private bool _IsTrue = false;
        /// <summary>
        /// 是否为True
        /// </summary>
        public bool IsTrue
        {
            get { return _IsTrue; }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public WaitSeatCancel(string roomName,string shortSeatNo)
        {
            InitializeComponent();

            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
            countDown = new FormCloseCountdown(9);
            countDown.EventCountdown += new EventHandler(countDown_EventCountdown);

            this.lblSeatInfo.Text = string.Format("    {0} {1}",roomName,shortSeatNo);
            
        }
        void countDown_EventCountdown(object sender, EventArgs e)
        {
            if (countDown.CountdownSceonds <= 0)
            {
                _IsTrue = false;
                this.Close();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _IsTrue = false;
            this.Close();
        }

        private void BtnSure_Click(object sender, EventArgs e)
        {
            _IsTrue = true;
            this.Close();
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            _IsTrue = false;
            this.Close();
        }

        private void WaitSeatCancel_FormClosing(object sender, FormClosingEventArgs e)
        {
            countDown.EventCountdown -= new EventHandler(countDown_EventCountdown);
            countDown.Stop();
            this.Dispose();
        }
    }
}
