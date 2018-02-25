using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SeatManage.ClassModel;
using SeatClient.OperateResult;
namespace SeatClient
{
    public partial class FrmViolate : Form
    {
        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown countdown;
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private List<ReaderNoticeInfo> readerNoticeList = new List<ReaderNoticeInfo>();
        /// <summary>
        /// 消息列表
        /// </summary>
        public List<ReaderNoticeInfo> ReaderNoticeList
        {
            get { return readerNoticeList; }
            set { readerNoticeList = value; }
        }
        public FrmViolate()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();

            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);

            countdown = new FormCloseCountdown(9);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
        }
        void countdown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    if (this.countdown.CountdownSceonds <= 0)
                    {

                        this.Close();

                    }
                }
                catch { }
            }));
        }
        public int turnTime = 0;
        private void FrmViolate_Load(object sender, EventArgs e)
        { 
            StringBuilder message = new StringBuilder();
            label2.Text = string.Format("您有{0}条提醒消息", readerNoticeList.Count);
            for (int i = 0; i < readerNoticeList.Count; i++)
            {
                message .Append( string.Format("    {0} {1}\r", readerNoticeList[i].AddTime,readerNoticeList[i].Note));
            }

            lblNotice.Text = message.ToString();
        }
         
        private void FrmViolate_FormClosing(object sender, FormClosingEventArgs e)
        { 
            countdown.Stop();
            countdown.EventCountdown -= countdown_EventCountdown;
            this.Dispose();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}