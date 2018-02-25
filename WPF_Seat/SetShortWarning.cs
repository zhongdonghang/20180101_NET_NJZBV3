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
    public partial class SetShortWarning : Form
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
        public SetShortWarning(string seatNo)
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
            countDown = new FormCloseCountdown(9); 
            countDown.EventCountdown += new EventHandler(countDown_EventCountdown);
            this.lblSeatNo.Text = seatNo;
        }
        void countDown_EventCountdown(object sender, EventArgs e)
        {
            if (countDown.CountdownSceonds <= 0)
            {
                _IsTrue = false;
                this.Close();
            }
        }
        private void SetShortWarning_Load(object sender, EventArgs e)
        {
           
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            _IsTrue = false;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            _IsTrue = false;
            this.Close();
        }

        private void BtnSure_Click(object sender, EventArgs e)
        {
            _IsTrue = true;
            this.Close();
        }

        private void SetShortWarning_FormClosing(object sender, FormClosingEventArgs e)
        {
            countDown.Stop();
            countDown.EventCountdown -= new EventHandler(countDown_EventCountdown);
            this.Dispose();
        }
         
 
         

    }
}
