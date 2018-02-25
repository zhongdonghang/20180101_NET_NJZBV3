using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms; 
using System.Configuration;
using SeatClient.OperateResult;
using SeatClient.Class;
using SeatManage.EnumType;
namespace SeatClient
{
    public partial class ChooseSeatState : Form
    {
        private SelectSeatMode _RoomSelectSeatMethod = SelectSeatMode.None;
        /// <summary>
        /// 阅览室选座方式
        /// </summary>
        public SelectSeatMode RoomSelectSeatMethod
        {
            get { return _RoomSelectSeatMethod; }
            set { _RoomSelectSeatMethod = value; }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown countdown = null;
        public ChooseSeatState()
        {
            InitializeComponent(); 
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
       
            countdown = new FormCloseCountdown(7);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
            this.Size = new System.Drawing.Size(490, 288);
        }

        void countdown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                if (countdown.CountdownSceonds <= 0)
                {
                    this.Close(); 
                }
            }));
        }
    
        private void ChooseSeatState_Load(object sender, EventArgs e)
        {
           
        } 
        //选择自动选座
        private void btnAutomaticMode_Click(object sender, EventArgs e)
        {
            _RoomSelectSeatMethod = SelectSeatMode.AutomaticMode;
            this.Close();
        }
        //选择手动选座
        private void btnManualMode_Click(object sender, EventArgs e)
        {
            _RoomSelectSeatMethod = SelectSeatMode.ManualMode;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChooseSeatState_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown);
        }
    }
}