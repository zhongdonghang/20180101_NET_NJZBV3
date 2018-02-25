using System;
using System.Collections.Generic;
using System.ComponentModel; 
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult; 

namespace WPF_Seat
{
    public partial class TipForm_SelectSeatConfinmed : Form
    {
        SeatClient.OperateResult.FormCloseCountdown countDown = null;
        SystemObject clientObject = SystemObject.GetInstance();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public TipForm_SelectSeatConfinmed(string roomName,string seatNo,int closeTime)
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);

            if (closeTime < 9)
            {
                countDown = new FormCloseCountdown(closeTime);
            }
            else
            {
                countDown = new FormCloseCountdown(9);
            }
            countDown.EventCountdown += new EventHandler(countDown_EventCountdown);
            lblRoomName.Text = roomName;
            lblSeatNo.Text = seatNo;
        }

        void countDown_EventCountdown(object sender, EventArgs e)
        {
            if (countDown.CountdownSceonds <= 0)
            {
                IsTrue = false;
                this.Close(); 
            }
        }

        private bool _IsTrue = false;
        /// <summary>
        /// 是否为True
        /// </summary>
        public bool IsTrue
        {
            get { return _IsTrue; }
            set { _IsTrue = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IsTrue = true;
            this.Close();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IsTrue = false;
            this.Close();
            this.Dispose();
        }

        private void SeatFormChoose_Load(object sender, EventArgs e)
        { 
       
        } 

        private void button3_Click(object sender, EventArgs e)
        {
            IsTrue = false;
            this.Close();
            this.Dispose();
        }

        private void SeatFormChoose_FormClosing(object sender, FormClosingEventArgs e)
        {
            countDown.Stop();
            countDown.EventCountdown -= new EventHandler(countDown_EventCountdown);
        }
    }
}
