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
    public partial class ActivationClose : Form
    {
        SystemObject clientObject = SystemObject.GetInstance();
        SeatClient.OperateResult.FormCloseCountdown countdown = null;
        private bool _IsSure = false;

        public bool IsSure
        {
            get { return _IsSure; }
            
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public ActivationClose()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);

            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
             
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

        private void button2_Click(object sender, EventArgs e)
        {
            _IsSure = false;
            this.countdown.Stop();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _IsSure = true;
            this.countdown.Stop();
            this.Close();  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            _IsSure = false;
            this.countdown.Stop();
            this.Close();
        }

        private void ActivationClose_FormClosing(object sender, FormClosingEventArgs e)
        { 
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown);
            this.Dispose(); 
        }

        private void ActivationClose_Load(object sender, EventArgs e)
        {
            countdown = new FormCloseCountdown(9);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
        }

       
    }
}
