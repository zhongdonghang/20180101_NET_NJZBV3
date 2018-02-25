using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;
using SeatManage.SeatClient.Tip;
using SeatManage.SeatManageComm;
using SeatManage.ClassModel;
using SeatManage.Bll;

namespace WPF_Seat
{
    public partial class NoKeyboard : Form
    {
        SeatClient.OperateResult.FormCloseCountdown countDown = null;
        SystemObject clientObject = SystemObject.GetInstance();
        /// <summary>
        /// 窗体关闭时间
        /// </summary>
        /// <param name="closeTime"></param>
        public NoKeyboard(int closeTime)
        {
           
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles(); 
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X  , clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y - 50);
            if (closeTime < 9)
            {
                countDown = new FormCloseCountdown(closeTime);
            }
            else
            {
                countDown = new FormCloseCountdown(9);
            }
            countDown.EventCountdown += new EventHandler(countDown_EventCountdown);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        void countDown_EventCountdown(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
                            {
                                try
                                {
                                    if (countDown.CountdownSceonds <= 0)
                                    {
                                        countDown.EventCountdown -= countDown_EventCountdown;
                                        countDown.Stop();
                                        this.Close();
                                    }
                                }
                                catch
                                { }
                            }));
        }

        private string _seatNo = "";
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get { return _seatNo; }
            set { _seatNo = value; }
        }

      
         

        private void btnBack_Click(object sender, EventArgs e)
        {
            string seatNo = txtSeatNo.Text;
            if (seatNo.Length > 0)
            {
                txtSeatNo.Text = seatNo.Substring(0, seatNo.Length - 1);
            }
            countDown.ReStartTime(5);
        }
        /// <summary>
        /// 判断是否加锁
        /// </summary>
        /// <param name="seatNo">座位编号</param>
        /// <returns></returns>
        private bool SeatIsLock(string seatNo)
        {
            return true;
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSeatNo.Text.Trim()))
            {
                toolTip1.SetToolTip(txtSeatNo, "请输入座位号！");
                toolTip1.Show("请输入座位号！", txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }
            string seatNo = "";
            ReadingRoomInfo roomInfo = clientObject.EnterOutLogData.Student.AtReadingRoom; 
            string roomNo = roomInfo.No + "000";
            string seatHeader = SeatComm.SeatNoToSeatNoHeader(roomInfo.Setting.SeatNumAmount, roomNo);
            seatNo = seatHeader + txtSeatNo.Text;
            //获取座位信息，并判断座位在该阅览室是否存在。
            Seat seat = T_SM_Seat.GetSeatInfoBySeatNo(seatNo);
            if (seat == null)
            {
                toolTip1.SetToolTip(txtSeatNo, "座位号输入有误，请输入正确的座位号！");
                toolTip1.Show("座位号输入有误，请输入正确的座位号！", txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }
            if (seat.IsSuspended)
            {
                toolTip1.SetToolTip(txtSeatNo, "您选择的座位，已暂停使用，请重新选择！");
                toolTip1.Show("您选择的座位，已暂停使用，请重新选择！", txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }
            if (seat.ReadingRoomNum != roomInfo.No)
            {
                toolTip1.SetToolTip(txtSeatNo, string.Format("座位{0}在该阅览室不存在",txtSeatNo.Text));
                toolTip1.Show(string.Format("座位{0}在该阅览室不存在", txtSeatNo.Text), txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }

            SeatManage.EnumType.EnterOutLogType logType = SeatManage.Bll.T_SM_EnterOutLog.GetSeatUsedState(seatNo);
  
            //TODO:还需检测座位是否被预约 SeatManage.Bll.T_SM_EnterOutLog
            if (logType == SeatManage.EnumType.EnterOutLogType.None || logType == SeatManage.EnumType.EnterOutLogType.Leave)
            {
                //根据座位号获取进出记录的状态，如果为None或者为Leave，则锁定座位
                SeatManage.EnumType.SeatLockState lockResult = SeatManage.Bll.T_SM_Seat.LockSeat(seatNo);
                if (lockResult == SeatManage.EnumType.SeatLockState.NotExists)
                { 
                    toolTip1.SetToolTip(txtSeatNo, "座位号不存在");
                    toolTip1.Show("座位号不存在", txtSeatNo, 5000); 
                    return;
                }
                else if (lockResult == SeatManage.EnumType.SeatLockState.UnLock)
                { 
                    toolTip1.SetToolTip(txtSeatNo, "座位正在被其他读者选择");
                    toolTip1.Show("座位正在被其他读者选择", txtSeatNo, 5000);
                    txtSeatNo.Text = "";
                    return;
                }
                else if (lockResult == SeatManage.EnumType.SeatLockState.Locked)
                {
                    this._seatNo = seatNo;
                    this.Close();
                    this.Dispose();
                }
            }
            else if (logType == SeatManage.EnumType.EnterOutLogType.BespeakWaiting)
            {
                toolTip1.SetToolTip(txtSeatNo, "已被其他读者预约");
                toolTip1.Show("已被其他读者预约", txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }
            else
            { 
                toolTip1.SetToolTip(txtSeatNo, "座位正在被使用");
                toolTip1.Show("座位正在被使用", txtSeatNo, 5000);
                txtSeatNo.Text = "";
                return;
            }
            //} 
            //else
            //{
            //    toolTip1.SetToolTip(txtSeatNo, "请输入最后四位座位号");
            //    toolTip1.Show("请输入座位号",txtSeatNo,5000);  
            //}
        }


        private void button1_Click(object sender, EventArgs e)
        {
            countDown.EventCountdown -= countDown_EventCountdown;
            countDown.Stop();
            this.Close();
        }

        private void NoKeyboard_FormClosing(object sender, FormClosingEventArgs e)
        {  
            this.Dispose();
        }

        private void myKeyboard_MyKeyDown(string keyCode)
        {
            this.txtSeatNo.Text += keyCode;
            countDown.ReStartTime(5);
        }

        private void myKeyboard_MyEnter(object sender, EventArgs e)
        {
            btnYes_Click(this, new EventArgs());
        }

        private void txtSeatNo_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
