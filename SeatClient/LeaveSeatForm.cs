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
        /// ����ѡ��Ľ�����¼״̬
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
 
        
        //����
        private void button1_Click(object sender, EventArgs e)
        {
            chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.ShortLeave;
            this.Close();
            this.Dispose(); 
        }

        //����
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
        /// �Ƿ���ʾ��ť
        /// ͨ��XML�ļ����ã�1��ʾ 0����ʾ
        /// </summary>
        public void IsDisplay()
        {
            //����ѡ��
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
        /// ����ѡ��
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
        /// ��ʱ
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
            //ע���رյ��¼�
            this.countdown.EventCountdown -= new EventHandler(countdown_EventCountdown);
        }

    }

}