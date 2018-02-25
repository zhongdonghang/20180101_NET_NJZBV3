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
    public partial class LeaveSeatForm : Form
    {
        SystemObject clientobject = SystemObject.GetInstance();
        SeatClient.OperateResult.FormCloseCountdown countdown = new FormCloseCountdown(7);
        SeatManage.EnumType.EnterOutLogType chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.None;
        /// <summary>
        /// 读者选择的进出记录状态
        /// </summary>
        public SeatManage.EnumType.EnterOutLogType ChooseEnterOutState
        {
            get { return chooseEnterOutState; }
            set { chooseEnterOutState = value; }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public LeaveSeatForm()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            lblTitleAd.Text = clientobject.ClientSetting.TitleAd;
            this.Location = new Point(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
           
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
            btnTimeDelay.Visible = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used;
        }

        void countdown_EventCountdown(object sender, EventArgs e)
        {
            FormCloseCountdown countdown = sender as FormCloseCountdown;
            this.Invoke(new Action(() =>
            {
                if (countdown.CountdownSceonds <= 0)
                {
                    this.Close();
                    this.Dispose();
                }

            }));
        }
 
        
        //暂离
        private void button1_Click(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
            this.Close();
            this.Dispose(); 
        }

        //永久
        private void button2_Click(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.Leave;
            this.Close();
            this.Dispose(); 
        }
         

        private void LeaveSeatForm_Load(object sender, EventArgs e)
        {
            IsDisplay();
        }


        /// <summary>
        /// 是否显示按钮
        /// 通过XML文件配置，1显示 0不显示
        /// </summary>
        public void IsDisplay()
        {
            //重新选座
            if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used && clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
            {
                btnTimeDelay.Visible = true;
            }
            else
            {
                btnTimeDelay.Visible = false;
            }

        }

        /// <summary>
        /// 重新选座
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.ReselectSeat;
            this.Close();
            this.Dispose();
        }
        /// <summary>
        /// 续时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTimeDelay_Click(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.ContinuedTime;
            this.Close();
            this.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.None;
            this.Close();
            this.Dispose();
        }

        private void LeaveSeatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //注销关闭的事件
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown);
        }

    }

}