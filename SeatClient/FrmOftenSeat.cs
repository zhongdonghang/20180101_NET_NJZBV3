using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SeatClient.OperateResult;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using SeatManage.Bll;
using SeatManage.MyUserControl;
using SeatManage.SeatManageComm;
using SeatClient.Class;

namespace SeatClient
{
    public partial class FrmOftenSeat : Form
    {
        SystemObject clientobject = SystemObject.GetInstance();
        HandleResult operateResule = HandleResult.Failed;
        FormCloseCountdown countDown = null;
        public FrmOftenSeat()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.Location = new Point(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            countDown = new FormCloseCountdown(9);
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
                        this.Close();
                        countDown.Stop();
                    }
                }
                catch
                { }
            }));
        }


        private void FrmOftenSeat_Load(object sender, EventArgs e)
        {
            try
            {
                string cardNo = clientobject.EnterOutLogData.EnterOutlog.CardNo;
                int days = clientobject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.LengthDays;
                List<string> roomNums = clientobject.ClientSetting.DeviceSetting.Rooms;
                List<Seat> seats = T_SM_Seat.GetReaderOftenUsedSeat(cardNo, days, roomNums);
                InitiallizeOftenSeat(seats);
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("常坐座位窗体初始化遇到异常：{0}", ex.Message));
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                tip.ShowDialog();
            }
        }

        /// <summary>
        /// 初始化常坐座位按钮
        /// </summary>
        public void InitiallizeOftenSeat(List<Seat> seats)
        {
            try
            {
                int seatBtnX = 4;
                int seatBtnY = 53;
                if (seats.Count == 0)
                {
                    OftenUsedSeatButton seatBtn = new OftenUsedSeatButton();
                    seatBtn.Text = "暂无常坐座位";
                    seatBtn.Location = new Point(seatBtnX, seatBtnY);
                    this.Controls.Add(seatBtn);

                }
                else
                {
                    for (int i = 0; i < seats.Count; i++)
                    {

                        ReadingRoomInfo roomInfo = clientobject.ReadingRoomList[seats[i].ReadingRoomNum];
                        seats[i].ShortSeatNo = SeatComm.SeatNoToShortSeatNo(roomInfo.Setting.SeatNumAmount, seats[i].SeatNo);
                        OftenUsedSeatButton seatBtn = new OftenUsedSeatButton();
                        seatBtn.ReadingRoomNo = seats[i].ReadingRoomNum;
                        seatBtn.ReadingRoomName = roomInfo.Name;
                        seatBtn.SeatNo = seats[i].SeatNo;
                        seatBtn.ShortSeatNo = seats[i].ShortSeatNo;
                        seatBtn.Location = new Point(seatBtnX, seatBtnY);
                        seatBtn.Click += new EventHandler(seatBtn_Click);
                        this.Controls.Add(seatBtn);

                        seatBtnX += 165;
                        if ((i + 1) % 3 == 0)
                        {
                            seatBtnX = 4;
                            seatBtnY += 53;
                        }
                        if ((i + 1) > clientobject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.SeatCount)
                        {
                            break;
                        }
                    }
                }
            }
            catch
            {
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                tip.ShowDialog();
                clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
            }
        }

        void seatBtn_Click(object sender, EventArgs e)
        {
            try
            {
                OftenUsedSeatButton seatBtn = sender as OftenUsedSeatButton;
                if (string.IsNullOrEmpty(seatBtn.SeatNo))
                {
                    return;
                }
                ReadingRoomInfo roomInfo = clientobject.ReadingRoomList[seatBtn.ReadingRoomNo];
                if (seatBtn != null)
                {
                    #region 验证黑名单
                    if (SelectSeatProven.ProvenReaderState(clientobject.EnterOutLogData.Student, roomInfo, clientobject.RegulationRulesSet.BlacklistSet, clientobject.ClientSetting.DeviceSetting))
                    {
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
                        return;
                    }
                    #endregion

                    #region 检查阅览室当前状态
                    ReadingRoomStatus roomState = NowReadingRoomState.ReadingRoomOpenState(roomInfo.Setting.RoomOpenSet, ServiceDateTime.Now);
                    if (roomState == ReadingRoomStatus.Close || roomState == ReadingRoomStatus.BeforeClose)
                    {
                        SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.ReadingRoomClosing, 7);//显示提示窗体
                        tip.ShowDialog();
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
                        return;
                    }
                    #endregion


                    #region 锁定座位。
                    SeatManage.EnumType.SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
                    if (lockseat == SeatManage.EnumType.SeatLockState.Locked)//座位成功加锁
                    {
                        string roomName = seatBtn.ReadingRoomName;
                        string seatNo = seatBtn.ShortSeatNo;
                        clientobject.EnterOutLogData.Student.AtReadingRoom = roomInfo;
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
                        clientobject.EnterOutLogData.EnterOutlog.TerminalNum = clientobject.ClientSetting.ClientNo;
                        clientobject.EnterOutLogData.FlowControl = SeatManage.EnumType.ClientOperation.SelectSeat; //操作为选择座位 
                        clientobject.EnterOutLogData.EnterOutlog.ReadingRoomNo = seatBtn.ReadingRoomNo;
                        clientobject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}选择常坐座位，{1}，{2}号座位", clientobject.ClientSetting.ClientNo, roomName, seatNo);
                        this.Close();
                        this.Dispose();
                    }
                    else if (lockseat == SeatManage.EnumType.SeatLockState.UnLock)//没有成功加锁
                    {
                        SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.SeatLocking, 7);//显示提示窗体
                        tip.ShowDialog();
                        clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("选择常坐座位遇到异常：{0}", ex.Message));
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                tip.ShowDialog();
                clientobject.EnterOutLogData.EnterOutlog.SeatNo = "";
            }
        }




        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmOftenSeat_FormClosing(object sender, FormClosingEventArgs e)
        {
            countDown.EventCountdown -= countDown_EventCountdown;
            countDown.Stop();
            this.Dispose();
        }

    }
}