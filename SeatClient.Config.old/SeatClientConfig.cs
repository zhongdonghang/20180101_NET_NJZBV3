using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.SeatManageComm;
using SeatManage.SeatClient.Config.Code;
using SeatManage.ClassModel;

namespace SeatClient.Config
{
    public partial class Form1 : Form
    {

        SeatManage.ClassModel.TerminalInfo terminal = null;
        public ClientBasicConfig baseConfig = new ClientBasicConfig();
        public Form1(ClientBasicConfig config)
        {
            baseConfig = config;
            InitializeComponent();
            int i = Screen.PrimaryScreen.Bounds.Width;
            if (i == 1080)
            {
                this.Location = new Point((i - 356) / 2, 1000);
            }
            InitializePart2();
        }
        void InitializePart2()
        {
            //baseConfig = ReadSeatClientConfig.GetSeatClientBaseConfig();
            this.txtTerminalNum.Text = baseConfig.TerminalNum;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtTerminalNum.Text))
            {
                toolTip1.SetToolTip(txtTerminalNum, "请输入终端号");
                toolTip1.Show("请输入终端号", txtTerminalNum, 5000);
                return;
            }
            if (terminal == null)
            {
                toolTip1.SetToolTip(txtTerminalNum, "终端编号错误");
                toolTip1.Show("终端编号错误", txtTerminalNum, 5000);
                return;
            }
            else if (terminal.TerminalMacAddress != GetMacAddress.GetLocalAddress()[0])
            {
                DialogResult result = MessageBox.Show("本机MAC地址和该编号绑定的MAC地址不一致，程序将只更新服务器上的配置，而不将编号保存到本地，是否继续？", "提示", MessageBoxButtons.YesNo);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    GetNewConfig();
                    if (SeatManage.Bll.ClientConfigOperate.UpdateTerminal(baseConfig.WCFConnString, terminal))
                    {
                        lblResultMessage.Text = "修改成功";
                    }
                    else
                    {
                        lblResultMessage.Text = "修改失败";
                    }
                }
            }
            else
            {
                baseConfig.TerminalNum = this.txtTerminalNum.Text;
                if (!ReadSeatClientConfig.SaveConfig(baseConfig))
                {
                    MessageBox.Show("文件保存失败，请检查配置文件的属性是否为只读。");
                }
                GetNewConfig();
                if (SeatManage.Bll.ClientConfigOperate.UpdateTerminal(baseConfig.WCFConnString, terminal))
                {
                    lblResultMessage.Text = "修改成功";
                }
                else
                {
                    lblResultMessage.Text = "修改失败，详情见错误日志";
                }
            }
        }




        private void Form1_Load(object sender, EventArgs e)
        {
            OldConfig();
        }

        private void btnAddRoomNums_Click(object sender, EventArgs e)
        {
            string[] nums = getRoomNumsArray();
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums[i].Length == 6)
                {//判断阅览室编号在列表中是否已经存在
                    bool isExists = false;
                    for (int j = 0; j < lstReadingRoomNums.Items.Count; j++)
                    {
                        if (lstReadingRoomNums.Items[j].ToString() == nums[i])
                        {
                            isExists = true;
                            break;
                        }
                    }
                    //如果不存在，就添加到listBox 中
                    if (!isExists)
                    {
                        this.lstReadingRoomNums.Items.Add(nums[i]);
                    }
                }
            }
            this.txtRooms.Text = "";
        }

        private void lstReadingRoomNums_DoubleClick(object sender, EventArgs e)
        {
            ListBox lsb = sender as ListBox;
            if (lsb.SelectedIndex != -1)
            {
                lstReadingRoomNums.Items.Remove(lsb.SelectedItem);
            }
        }



        #region 私有方法
        private void OldConfig()
        {
            terminal = SeatManage.Bll.ClientConfigOperate.GetClientConfig(baseConfig.WCFConnString, this.txtTerminalNum.Text);
            if (terminal != null)
            {
                btnGetConfig.Enabled = false;
                btnSubmit.Enabled = true;
            }
            else
            {
                btnGetConfig.Enabled = true;
                btnSubmit.Enabled = false;
                toolTip1.SetToolTip(txtTerminalNum, "终端编号错误，没有获取到终端信息");
                toolTip1.Show("终端编号错误，没有获取到终端信息", txtTerminalNum, 5000);
                return;
            }
            cb_pos.Checked = terminal.DeviceSetting.PosTimes.IsUsed;
            ckbShowClosedRoom.Checked = terminal.DeviceSetting.IsShowClosedRoom;
            ckbIsShowInitPOS.Checked = terminal.DeviceSetting.IsShowInitPOS;
            ckbActiveBespeakSeat.Checked = terminal.DeviceSetting.UsingActiveBespeakSeat;
            ckbEnterNoForSeat.Checked = terminal.DeviceSetting.UsingEnterNoForSeat;
            ckbOftenUsedSeat.Checked = terminal.DeviceSetting.UsingOftenUsedSeat.Used;
            ckbPrintSlip.Checked = terminal.DeviceSetting.UsingPrintSlip;
            if (terminal.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1024)
            {
                rdb1024.Checked = true;
            }
            else
            {
                rdb1080.Checked = true;
            }
            txtTimeLength.Text = terminal.DeviceSetting.PosTimes.Minutes.ToString();
            txtTimes.Text = terminal.DeviceSetting.PosTimes.Times.ToString();
            switch (terminal.DeviceSetting.SelectMethod)
            {
                case SeatManage.EnumType.SelectSeatMode.Default:
                    rdoChoseMethodDefault.Checked = true;
                    break;
                case SeatManage.EnumType.SelectSeatMode.AutomaticMode:
                    rdoAutomaticMode.Checked = true;
                    break;
                case SeatManage.EnumType.SelectSeatMode.ManualMode:
                    rdoManualMode.Checked = true;
                    break;
                case SeatManage.EnumType.SelectSeatMode.OptionalMode:
                    rdoOptionalMode.Checked = true;
                    break;
            }

            //阅览室编号
            lstReadingRoomNums.Items.Clear();
            for (int i = 0; i < terminal.DeviceSetting.Rooms.Count; i++)
            {
                lstReadingRoomNums.Items.Add(terminal.DeviceSetting.Rooms[i]);
            }
        }

        /// <summary>
        /// 获取新的配置信息
        /// </summary>
        private void GetNewConfig()
        {
            if (terminal == null)
            {
                return;
            }
            terminal.DeviceSetting.PosTimes.IsUsed = cb_pos.Checked;
            terminal.DeviceSetting.IsShowClosedRoom = ckbShowClosedRoom.Checked;
            terminal.DeviceSetting.IsShowInitPOS = ckbIsShowInitPOS.Checked;
            terminal.DeviceSetting.UsingActiveBespeakSeat = ckbActiveBespeakSeat.Checked;
            terminal.DeviceSetting.UsingEnterNoForSeat = ckbEnterNoForSeat.Checked;
            terminal.DeviceSetting.UsingOftenUsedSeat.Used = ckbOftenUsedSeat.Checked;
            terminal.DeviceSetting.UsingPrintSlip = ckbPrintSlip.Checked;
            if (rdb1024.Checked)
            {
                resoultion("1024");
            }
            else if (rdb1080.Checked)
            {
                resoultion("1080");
            }
            terminal.DeviceSetting.PosTimes.Minutes = int.Parse(txtTimeLength.Text);
            terminal.DeviceSetting.PosTimes.Times = int.Parse(txtTimes.Text);
            if (rdoChoseMethodDefault.Checked)
            {
                terminal.DeviceSetting.SelectMethod = SeatManage.EnumType.SelectSeatMode.Default;
            }
            else if (rdoAutomaticMode.Checked)
            {
                terminal.DeviceSetting.SelectMethod = SeatManage.EnumType.SelectSeatMode.AutomaticMode;
            }
            else if (rdoManualMode.Checked)
            {
                terminal.DeviceSetting.SelectMethod = SeatManage.EnumType.SelectSeatMode.ManualMode;
            }
            else if (rdoOptionalMode.Checked)
            {
                terminal.DeviceSetting.SelectMethod = SeatManage.EnumType.SelectSeatMode.OptionalMode;
            }
            terminal.DeviceSetting.Rooms.Clear();
            for (int i = 0; i < lstReadingRoomNums.Items.Count; i++)
            {
                if (!string.IsNullOrEmpty(lstReadingRoomNums.Items[i].ToString()))
                {
                    terminal.DeviceSetting.Rooms.Add(lstReadingRoomNums.Items[i].ToString());
                }
            }
        }

        void resoultion(string r)
        {
            switch (r)
            {
                case "1080":
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Location.X = 0;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Location.Y = 920;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Size.X = 1080;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Size.Y = 1000;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Location.X = 280;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Location.Y = 1242;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Size.X = 490;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Size.Y = 288;
                    break;
                case "1024":
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Location.X = 0;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Location.Y = 0;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Size.X = 1024;
                    terminal.DeviceSetting.SystemResoultion.WindowSize.Size.Y = 768;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Location.X = 214;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Location.Y = 202;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Size.X = 490;
                    terminal.DeviceSetting.SystemResoultion.TooltipSize.Size.Y = 288;
                    break;
            }
        }
        string[] getRoomNumsArray()
        {
            string[] numsArray = txtRooms.Text.Split(';');
            return numsArray;
        }
        #endregion

        private void txtTerminalNum_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (txt.Text.Length == 6)
            {
                btnGetConfig.Enabled = true;
            }

        }

        private void btnGetConfig_Click(object sender, EventArgs e)
        {
            OldConfig();
        }

    }

}
