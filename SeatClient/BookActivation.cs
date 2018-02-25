using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Security.Cryptography;
using SeatClient.OperateResult;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.SeatManageComm;
using SeatManage.Bll;
using SeatManage.ClassModel;
using System.Threading;

namespace SeatClient
{
    public partial class BookActivation : Form
    {
        SystemObject clientObject = SystemObject.GetInstance();
        FormCloseCountdown countdown;
        public BookActivation()
        {
            InitializeComponent();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitoalizeComponent2();
            this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y);
            this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y);
            lblTitleAd.Text = clientObject.ClientSetting.TitleAd;
            countdown = new FormCloseCountdown(9);
            countdown.EventCountdown += new EventHandler(countdown_EventCountdown);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
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
        public void InitoalizeComponent2()
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new EventPosCardNo(ObjCardReader_CardNoGeted);
                textBox1.Visible = false;
                btnActiveTest.Visible = false;
            }
            else
            {
                textBox1.Visible = true;
                btnActiveTest.Visible = true;
            }
        }

        void ObjCardReader_CardNoGeted(object sender, CardEventArgs e)
        {
            clientObject.ObjCardReader.Stop();
            this.Invoke(new Action(() =>
            {
                if (e.PosResult)
                {
                    activeBespeak(e.CardNo);
                }
                else
                {
                    WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
                }
            }));
            Thread.Sleep(2000);
            clientObject.ObjCardReader.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            countdown.Stop();
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            countdown.Stop();
            this.Close();
        }

        private void BookActivation_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
            countdown.EventCountdown -= countdown_EventCountdown;

            this.Dispose();
        }

        private void btnActiveTest_Click(object sender, EventArgs e)
        {

            activeBespeak(this.textBox1.Text);
        }
        /// <summary>
        /// 激活预约操作
        /// </summary>
        /// <param name="cardNo"></param>
        void activeBespeak(string cardNo)
        {
            if (string.IsNullOrEmpty(cardNo))
            {
                return;
            }
            ReaderInfo reader = EnterOutOperate.GetSimpleReaderInfo(cardNo);
            UserInfo user = Users_ALL.GetUserInfo(cardNo);
            if (user != null)
            {
                if (user.IsUsing == SeatManage.EnumType.LogStatus.Valid)//判断用户状态是否启用。
                {
                    ActivationClose formBespeakClose = new ActivationClose();//启用中，则提示取消
                    this.countdown.Pause();
                    formBespeakClose.ShowDialog();
                    this.countdown.Start();
                    if (formBespeakClose.IsSure)
                    {
                        user.IsUsing = SeatManage.EnumType.LogStatus.Fail;//
                        user.Remark = "终端刷卡注销";
                        Users_ALL.UpdateUserOnlyInfo(user);
                    }
                }
                else
                {//如果读者用户状态是失效，则重新激活。
                    user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                    user.Password = MD5Algorithm.GetMD5Str32(cardNo);
                    user.Remark = "终端刷卡重新激活";
                    if (Users_ALL.UpdateUserOnlyInfo(user))
                    {
                        ActivationSuccess successFrom = new ActivationSuccess(user.LoginId, cardNo);
                        successFrom.ShowDialog();
                    }
                }
            }
            else
            {
                UserInfo newUser = new UserInfo();
                newUser.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                newUser.LoginId = cardNo;
                newUser.Password = MD5Algorithm.GetMD5Str32(cardNo);
                newUser.UserType = SeatManage.EnumType.UserType.Reader;
                newUser.UserName = reader == null ? "" : reader.Name;
                newUser.Remark = "在终端刷卡激活";
                if (Users_ALL.AddNewUser(newUser))
                {
                    ActivationSuccess successFrom = new ActivationSuccess(newUser.LoginId, cardNo);
                    successFrom.ShowDialog();
                }
            }
            this.countdown.Stop();
            this.Close();
        }
    }
}
