using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SeatClientLeave
{
    public partial class LeaveSeatForm : Form
    {
        SeatClientLeave.Code.LeaveClientObject clientobject =  Code.LeaveClientObject.GetInstance();
        SeatClient.OperateResult.FormCloseCountdown countdown = new SeatClient.OperateResult.FormCloseCountdown(7);
        SeatManage.EnumType.EnterOutLogType chooseEnterOutState = SeatManage.EnumType.EnterOutLogType.None;
        /// <summary>
        /// ����ѡ��Ľ�����¼״̬
        /// </summary>
        public SeatManage.EnumType.EnterOutLogType ChooseEnterOutState
        {
            get { return chooseEnterOutState; }
            set { chooseEnterOutState = value; }
        }
        public LeaveSeatForm()
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
            btnTimeDelay.Visible = clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.Used;
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
        ///  
        /// </summary>
        public void IsDisplay()
        {
            //����ѡ��
            if (clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.Used && clientobject.ReaderInfo.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime)
            {
                btnTimeDelay.Visible = true;
            }
            else
            {
                btnTimeDelay.Visible = false;
            } 
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