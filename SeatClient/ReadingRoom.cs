using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using WPF_Seat;
using SeatClient.Class;
using SeatManage.InterfaceFactory;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.Bll;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;
using SeatManage.ClassModel;
using SeatManage;
using SeatManage.MyUserControl;
using SeatClient.OperateResult;
using SeatManage.SeatClient.Tip;

namespace SeatClient
{
    public partial class ReadingRoom : Form
    {
        private SelectSeatMode _RoomSelectSeatMethod = SelectSeatMode.None;
        /// <summary>
        /// ������ѡ����ʽ
        /// </summary>
        public SelectSeatMode RoomSelectSeatMethod
        {
            get { return _RoomSelectSeatMethod; }
            set { _RoomSelectSeatMethod = value; }
        }

        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown formClose = null;
        /// <summary>
        /// ���������ҵĵ�ǰʹ��״̬
        /// </summary>
        Dictionary<string, ReadingRoomSeatUsedState> roomSeatUsingState = null;
        public ReadingRoom()
        {
            InitializeComponent();
            InitializeComponentPart2();
        }

        /// <summary>
        /// ����ͼƬ��ʼ��
        /// </summary>
        private void InitializeComponentPart2()
        {
            #region ��ʼ�������С��λ�úͱ���
            this.BackgroundImage = clientObject.BackgroundImagesResource["ChooseReadingRoom"]; //����ͼƬ   
            this.btnExit.BackgroundImage = clientObject.BackgroundImagesResource["Exit"];

            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y);
            if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1080)
            {
                Point btnLocation = new Point(900, 880);
                btnExit.Size = new System.Drawing.Size(104, 54);
                btnExit.Location = btnLocation;
            }
            else if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1024)
            {
                Point btnLocation = new Point(850, 655);
                btnExit.Size = new System.Drawing.Size(104, 54);
                btnExit.Location = btnLocation;
                lblCountdown.Location = new Point(420, 24);
            }
            if (clientObject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.Used)
            {
                createOftenSeatButton();
            }
            #endregion
        }

        /// <summary>
        /// ����������λ��ť
        /// </summary>
        private void createOftenSeatButton()
        {
            int btnLocationX = 225;
            int btnLocationY = 500;
            if (this.Size.Width == 1080)
            {
                btnLocationY = 880;
            }
            if (this.Size.Width == 1024)
            {
                btnLocationY = 655;
                btnLocationX = 200;
            }
            Button btnOftenSeat = new Button();
            btnOftenSeat.Image = clientObject.BackgroundImagesResource["btnOftenSeat"];
            btnOftenSeat.FlatAppearance.BorderSize = 0;
            btnOftenSeat.ImageAlign = ContentAlignment.MiddleCenter;
            btnOftenSeat.BackColor = System.Drawing.Color.Transparent;
            btnOftenSeat.FlatAppearance.MouseOverBackColor = Color.Transparent;
            btnOftenSeat.FlatAppearance.MouseDownBackColor = Color.Transparent;
            btnOftenSeat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnOftenSeat.Location = new System.Drawing.Point(btnLocationX + 500, btnLocationY);
            btnOftenSeat.Name = "btnOftenSeat";
            btnOftenSeat.Size = new System.Drawing.Size(138, 54);
            btnOftenSeat.TabIndex = 110;
            btnLocationX += 150;
            btnOftenSeat.UseVisualStyleBackColor = true;
            btnOftenSeat.Click += new System.EventHandler(this.btnOftenSeat_Click);
            this.Controls.Add(btnOftenSeat);
        }

        private void ReadingRoomChooseForm_Load(object sender, EventArgs e)
        {
            #region ��ʾ�������������������
            int roomCationX = 120;//�����Һ�������
            int roomCationY = 140;//��������ֱ��������
            if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X != 1080)
            {
                roomCationX = 80;
                roomCationY = 130;
            }
            int roomCount = 0;
            List<string> roomNums = new List<string>();

            foreach (ReadingRoomInfo room in clientObject.ReadingRoomList.Values)
            {
                roomNums.Add(room.No);
            }
            //��ȡ��������λʹ��״̬
            roomSeatUsingState = NowReadingRoomState.GetRoomSeatUsedState(roomNums);
            foreach (string roomNum in clientObject.ClientSetting.DeviceSetting.Rooms) 
            {
                ReadingRoomInfo room = clientObject.ReadingRoomList[roomNum];
                ReadingRoomStatus roomStatus = NowReadingRoomState.ReadingRoomOpenState(room.Setting.RoomOpenSet, ServiceDateTime.Now);
                if (roomStatus == ReadingRoomStatus.Close && !clientObject.ClientSetting.DeviceSetting.IsShowClosedRoom)
                {
                    continue;
                }
                ReadingRoomButton btnRoom = InitDrawRoom(room, roomStatus);
                btnRoom.Location = new Point(roomCationX, roomCationY);
                roomCount++;
                if (roomCount % 4 != 0)
                {
                    roomCationX += 220;
                }
                else
                {
                    if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X != 1080)
                    {
                        roomCationX = 80;
                    }
                    else
                    {
                        roomCationX = 120;
                    }
                    roomCationY += 150;
                }
                this.Controls.Add(btnRoom);
            }
            #endregion

            #region �ر�������

            formClose = new FormCloseCountdown(int.Parse(lblCountdown.Text));
            formClose.EventCountdown += new EventHandler(formClose_EventCountdown);
            #endregion
        }
        /// <summary>
        /// �����ҹر��¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void formClose_EventCountdown(object sender, EventArgs e)
        {
            FormCloseCountdown obj = sender as FormCloseCountdown;
            if (obj != null)
            {
                this.Invoke(new Action(() =>
                            {
                                lblCountdown.Text = obj.CountdownSceonds.ToString();
                                if (obj.CountdownSceonds <= 0)
                                {
                                    this.clientObject.EnterOutLogData.FlowControl = ClientOperation.Exit;
                                    this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                                    this._RoomSelectSeatMethod = SelectSeatMode.None;
                                    this.Close();
                                }
                            }
                     ));
            }
        }



        private void btnOftenSeat_Click(object sender, System.EventArgs e)
        {
            FrmOftenSeat frm = new FrmOftenSeat();
            formClose.Pause();
            frm.ShowDialog();
            formClose.Start();
            if (!string.IsNullOrEmpty(clientObject.EnterOutLogData.EnterOutlog.SeatNo))
            {
                this.Close();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        public void btnViolateWaining_Click(object sender, EventArgs e)
        {

        }

        #region ������ѡ��ҳ���ʼ��


        /// <summary>
        /// ���ð�ť��ʽ
        /// </summary>
        /// <param name="btn">��ť</param>
        /// <param name="btnType">����</param>
        /// <param name="ReadRoonNo">�����ұ��</param>
        private ReadingRoomButton InitDrawRoom(ReadingRoomInfo room, ReadingRoomStatus roomStatus)
        {
            SeatManage.MyUserControl.ReadingRoomButton roomButton = new SeatManage.MyUserControl.ReadingRoomButton();
            try
            {
                roomButton.RoomNum = room.No;
                roomButton.Text = room.Name;
                int stopSeatCount = 0;
                foreach (KeyValuePair<string, Seat> item in room.SeatList.Seats)
                {
                    if (item.Value.IsSuspended)
                    {
                        stopSeatCount++;
                    }
                }
                roomButton.RoomUsedTip = roomSeatUsingState[room.No].SeatAmountUsed + "/" + (roomSeatUsingState[room.No].SeatAmountAll - stopSeatCount);
                roomButton.SeatUsingStatus = roomSeatUsingState[room.No].RoomSeatUsingState;//��λʹ��״̬��ֵ
                roomButton.RoomStatus = roomStatus;//������״̬��ֵ
                if (roomStatus != ReadingRoomStatus.Close && roomStatus != ReadingRoomStatus.BeforeClose)
                {
                    roomButton.Click += new EventHandler(roomButton_Click);
                }
                return roomButton;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// ѡ����λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void roomButton_Click(object sender, EventArgs e)
        {
            SeatManage.MyUserControl.ReadingRoomButton room = sender as ReadingRoomButton;
            this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = room.RoomNum;
            ReadingRoomInfo roomInfo = T_SM_ReadingRoom.GetSingleRoomInfo(room.RoomNum);
            if (room.SeatUsingStatus == ReadingRoomUsingStatus.Full && (!roomInfo.Setting.NoManagement.Used))
            {
                Tip_Framework tipForm = new Tip_Framework(TipType.ReadingRoomFull, 9);
                formClose.Pause();
                tipForm.ShowDialog();
                formClose.Start();
                return;
            }


            clientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;//���������ڵ������Ҹ�ֵ��

            //��֤��������Ƿ�����ѡ��������ҡ�
            if (!SelectSeatProven.ProvenReaderType(clientObject.EnterOutLogData.Student, roomInfo.Setting))
            {
                Tip_Framework tipForm = new Tip_Framework(TipType.ReaderTypeInconformity, 9);
                formClose.Pause();
                tipForm.ShowDialog();
                formClose.Start();
                return;
            }
            //��֤���ߺ�������ѡ��������
            if (SelectSeatProven.ProvenReaderState(clientObject.EnterOutLogData.Student, roomInfo, clientObject.RegulationRulesSet.BlacklistSet, clientObject.ClientSetting.DeviceSetting))
            {
                this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                return;
            }
            //TODO:��֤�ն�ѡ����ʽ
            if (room.SeatUsingStatus == ReadingRoomUsingStatus.Full && roomInfo.Setting.NoManagement.Used)
            {
                this._RoomSelectSeatMethod = SelectSeatMode.ManualMode;
                this.clientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = room.RoomNum;
                this.Close();
            }
            else
            {
                SelectSeatMode selectSeatMethod = SelectSeatProven.ProvenSelectSeatMethod(clientObject.ClientSetting.DeviceSetting, roomInfo.Setting.SeatChooseMethod);

                if (selectSeatMethod == SelectSeatMode.OptionalMode)
                {
                    ChooseSeatState frmChooseSeatState = new ChooseSeatState();
                    frmChooseSeatState.ShowDialog();
                    _RoomSelectSeatMethod = frmChooseSeatState.RoomSelectSeatMethod;
                    if (frmChooseSeatState.RoomSelectSeatMethod != SelectSeatMode.None)
                    {
                        this.clientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = room.RoomNum;
                        this.Close();
                    }
                    else
                    {
                        this.clientObject.EnterOutLogData.FlowControl = ClientOperation.None;
                        this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                    }
                }
                else
                {
                    this._RoomSelectSeatMethod = selectSeatMethod;
                    this.clientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat;
                    this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = room.RoomNum;
                    this.Close();
                }
            }

        }
        #endregion
        private void ReadingRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            formClose.EventCountdown -= new EventHandler(formClose_EventCountdown);
            formClose.Stop();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.clientObject.EnterOutLogData.FlowControl = ClientOperation.None;
            this.clientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
            this._RoomSelectSeatMethod = SelectSeatMode.None;
            this.clientObject.EnterOutLogData.FlowControl = ClientOperation.Exit;
            this.Hide();
        }

    }
}