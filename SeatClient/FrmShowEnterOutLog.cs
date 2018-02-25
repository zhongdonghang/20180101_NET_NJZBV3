using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;
using SeatClient.Class;
using SeatManage.Bll;
using System.Threading;
namespace SeatClient
{
    public partial class FrmShowEnterOutLog : Form
    {
        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown countDown = null;
        ReaderQueryLog readerLogQuery = new ReaderQueryLog();
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public FrmShowEnterOutLog()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.BackgroundImage = clientObject.BackgroundImagesResource["FrmShowEnterOutLog"]; //背景图片    
            this.btnReturn.BackgroundImage = clientObject.BackgroundImagesResource["Exit"];
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y);

            if (this.Size.Width == 1080)
            {
                Point btnLocation = new Point(885, 880);
                btnReturn.Location = btnLocation;
                Point labelCardNoLocation = new Point(142, 162);//姓名label的位置
                lblCardNo.Location = labelCardNoLocation;
                Point labelSeatNoLocation = new Point(948, 162);//座位号label的位置
                lblSeatNo.Location = labelSeatNoLocation;
                Point labelReadingRoomName = new Point(660, 162);//阅览室名称label的位置
                lblReadingRoomName.Location = labelReadingRoomName;
                Point labelNowState = new Point(435, 162);//显示当前状态的Label的位置
                lblNowState.Location = labelNowState;
            }
            else
            {
                label1.Location = new Point(422, 20);
                panel1.Size = new Size(940, 560);
                panel1.Location = new Point(41, 152);
                textBox1.Location = new Point(740, 55);
                button1.Location = new Point(885, 55);
                lblCardNo.Location = new Point(125, 121);
                lblSeatNo.Location = new Point(898, 121);
                lblReadingRoomName.Location = new Point(628, 121);
                lblNowState.Location = new Point(418, 121);
                btnReturn.Location = new Point(850, 660);
                btnReturn.BringToFront();
            }
            countDown = new FormCloseCountdown(int.Parse(label1.Text));
            countDown.EventCountdown += countdown_EventCountdown;
            InitoalizeComponent2();
        }
        void countdown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                try
                {
                    label1.Text = this.countDown.CountdownSceonds.ToString();
                    if (this.countDown.CountdownSceonds <= 0)
                    {
                        this.Close();
                    }
                }
                catch { }
            }));
        }
        public void InitoalizeComponent2()
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new EventPosCardNo(ObjCardReader_CardNoGeted);
                textBox1.Visible = false;
                button1.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
                button1.Visible = true;
            }
        }
        void ObjCardReader_CardNoGeted(object sender, CardEventArgs e)
        {
            clientObject.ObjCardReader.Stop();
            this.Invoke(new Action(() =>
            {
                if (e.PosResult)
                {
                    ShowEnterOutLogMessage(e.CardNo);
                }
                else
                {
                    WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
                }
            }));
            Thread.Sleep(2000);
            clientObject.ObjCardReader.Start();
        }


        private void FrmShowEnterOutLog_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 显示当前读者的详细进出记录
        /// </summary>
        /// <param name="cardNo"></param>
        private void ShowEnterOutLogMessage(string cardNo)
        {
            if (!string.IsNullOrEmpty(cardNo))
            {
                DateTime nowdt = ServiceDateTime.Now;
                List<EnterOutLogInfo> eollist = readerLogQuery.QueryEnterOugLogs(cardNo, nowdt);
                //List<BespeakLogInfo> blilist = readerLogQuery.QueryBespeakLogs(cardNo, nowdt);
                //List<WaitSeatLogInfo> wslilist = readerLogQuery.QueryWaitSeatLogs(cardNo, nowdt);
                lblCardNo.Text = cardNo;
                bool flag = false;
                for (int i = 0; i < eollist.Count; i++)
                {
                    if (eollist[i].EnterOutType == SeatManage.EnumType.LogStatus.Valid && eollist[i].EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                    {
                        ReadingRoomInfo room = T_SM_ReadingRoom.GetSingleRoomInfo(eollist[i].ReadingRoomNo);
                        lblReadingRoomName.Text = room.Name;
                        lblSeatNo.Text = SeatComm.SeatNoToShortSeatNo(room.Setting.SeatNumAmount, eollist[i].SeatNo);
                        lblNowState.Text = SeatComm.ConvertReaderState(eollist[i].EnterOutState);
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    lblReadingRoomName.Text = "没有选择座位";
                    lblNowState.Text = "无座";
                    lblSeatNo.Text = "无";
                }
                List<Label> labelList = readerLogQuery.GetEnterOutLogLabels(eollist);
                panel1.Controls.Clear();
                for (int i = 0; i < labelList.Count; i++)
                {
                    panel1.Controls.Add(labelList[i]);
                }
            }
        }




        private void button1_Click(object sender, EventArgs e)
        {
            ShowEnterOutLogMessage(textBox1.Text);
        }

        private void lstMessage_DrawItem(object sender, DrawItemEventArgs e)
        {

        }

        private void btnReturnWaiting_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void FrmShowEnterOutLog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
            countDown.Stop();
            countDown.EventCountdown -= countdown_EventCountdown;
            this.Dispose();
        }

    }
}