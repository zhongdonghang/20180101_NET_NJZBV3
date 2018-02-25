using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClientLeave.Code;

namespace SeatClientLeave
{
    public partial class Tip_SimpleTip : Form
    {
        SeatClientLeave.Code.LeaveClientObject clientobject = Code.LeaveClientObject.GetInstance();
        SeatClient.OperateResult.FormCloseCountdown countdown = new SeatClient.OperateResult.FormCloseCountdown(3);
        public Tip_SimpleTip()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            if (clientobject.TitleAdvert != null)
            {
                lblTitleAd.Text = clientobject.TitleAdvert.TitleAdvert;
            }
            this.Location = new Point(clientobject.ClientSetting.SystemResoultion.TooltipSize.Location.X, clientobject.ClientSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientobject.ClientSetting.SystemResoultion.TooltipSize.Size.X, clientobject.ClientSetting.SystemResoultion.TooltipSize.Size.Y);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
        }
        void countdown_EventCountdown(object sender, EventArgs e)
        {
            SeatClient.OperateResult.FormCloseCountdown countdown = sender as SeatClient.OperateResult.FormCloseCountdown;
            this.Invoke(new Action(() =>
            {
                if (countdown.CountdownSceonds <= 0)
                {
                    this.Close();
                    this.Dispose();
                }

            }));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 要显示的消息
        /// </summary>
        public string Message
        {
            set
            {
                lblNotice.Text = value;
            }
        }
        /// <summary>
        /// 图标类型
        /// </summary>
        public EnumSimpleTipFormIco TipIcon
        {
            set
            {
                switch (value)
                {
                    case EnumSimpleTipFormIco.Cry:
                        this.pictureBox1.Image = SeatClientLeave.Properties.Resources.cry;
                        break;
                    case EnumSimpleTipFormIco.Question:
                        this.pictureBox1.Image = SeatClientLeave.Properties.Resources.question;
                        break;
                    case EnumSimpleTipFormIco.Small:
                        this.pictureBox1.Image = SeatClientLeave.Properties.Resources.small;
                        break;
                    case EnumSimpleTipFormIco.Warm:
                        this.pictureBox1.Image = SeatClientLeave.Properties.Resources.warm;
                        break;
                }
            }
        }

        private void Tip_SimpleTip_FormClosing(object sender, FormClosingEventArgs e)
        {
            //注销关闭的事件
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown); 
        }
    }
}
