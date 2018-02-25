using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SeatManage.SeatManageComm;
using SeatManage.ISystemTerminal.IPOS;
using SeatClientLeave.Code;
using SeatManage.Bll;
using SeatManage.EnumType;

namespace SeatClientLeave
{
    public partial class MainForm : Form
    {
        private static string CardNo_Old = "";
        private DateTime LastCardTime = DateTime.Now;
        Code.LeaveClientObject clientObject = Code.LeaveClientObject.GetInstance();
        RouteResultHandle posCardHandle = RouteResultHandle.GetInstance();
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        /// <summary>
        /// 服务器时间处理
        /// </summary>
        ShowDateTime serviceDateTime = new ShowDateTime();
        public MainForm()
        {
            InitializeComponent();
            InitializeComponentPart2();
        }
        private void InitializeComponentPart2()
        {
            try
            {
                serviceDateTime.ShowHandle += new EventHandler(serviceDateTime_ShowHandle);
                posCardHandle.HandleResult += new HandleMessage(posCardHandle_HandleResult);
                this.BackgroundImage = clientObject.BackgroundImagesResource["EnterOutForm"]; //背景图片    
                this.Location = new Point(clientObject.ClientSetting.SystemResoultion.WindowSize.Location.X, clientObject.ClientSetting.SystemResoultion.WindowSize.Location.Y);
                this.Size = new System.Drawing.Size(clientObject.ClientSetting.SystemResoultion.WindowSize.Size.X, clientObject.ClientSetting.SystemResoultion.WindowSize.Size.Y);
                if (clientObject.ClientSetting.SystemResoultion.WindowSize.Size.X == 1080)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        try
                        {
                            picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                        }
                        catch { }
                    }
                    picBoxPartnersLogo.Size = new Size(1080, 520);
                    picBoxPartnersLogo.Location = new Point(0, 225);

                }
                else if (clientObject.ClientSetting.SystemResoultion.WindowSize.Size.X == 1024)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        try
                        {
                            picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                        }
                        catch { }
                    }
                    picBoxPartnersLogo.Size = new Size(1024, 406);
                    picBoxPartnersLogo.Location = new Point(0, 170);
                    lblDate.Location = new Point(350, 8);
                    lblTime.Location = new Point(595, 8);
                }

                #region 初始化读卡器接口对象
                if (clientObject.ObjCardReader != null)
                {
                    clientObject.ObjCardReader.CardNoGeted += new EventPosCardNo(ObjCardReader_CardNoGeted);
                    clientObject.ObjCardReader.Start();
                    textBox1.Visible = false;
                    button3.Visible = false;
                }
                else
                {
                    textBox1.Visible = true;
                    button3.Visible = true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("操作遇到异常：" + ex.Message);
                HandelMessage("   处理遇到异常，可能是因为网络原因导致，详细信息请查看错误日志。", EnumSimpleTipFormIco.Cry);
            }
        }


        MenuItem menuItem1 = new MenuItem("退出程序");
        MenuItem menuItem2 = new MenuItem("最小化");
        MenuItem menuItem3 = new MenuItem("最大化");
        private void InitializeFormState()
        {
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon();
            this.notifyIcon1.Icon = SeatClientLeave.Properties.Resources.标1;
            this.notifyIcon1.Text = "座位管理系统程序";
            menuItem1.Click += new EventHandler(menuItem_Click);
            menuItem2.Click += new EventHandler(menuItem2_Click);
            menuItem3.Click += new EventHandler(menuItem3_Click);
            this.notifyIcon1.DoubleClick += new EventHandler(notifyIcon1_DoubleClick);
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(5000, "离开终端", "程序已启动", ToolTipIcon.Info);
            if (SeatClientLeave.Code.LeaveClientSetting.WindowState != Code.FormWindowState.Minimized)
            {
                menuItem3.PerformClick();
            }
        }

        void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.Hide();
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem3, menuItem1 });
            }
            else
            {
                this.Show();
                this.WindowState = System.Windows.Forms.FormWindowState.Normal;
                notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem2, menuItem1 });
            }
        }

        void menuItem3_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem2, menuItem1 });
        }


        void menuItem2_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem3, menuItem1 });
        }

        void menuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        void serviceDateTime_ShowHandle(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                lblDate.Text = serviceDateTime.ServiceDate;
                lblTime.Text = serviceDateTime.ServiceTime;
            }));
        }

        /// <summary>
        /// 读卡器读卡方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, CardEventArgs e)
        {
            clientObject.ObjCardReader.Stop();
            try
            {
                this.Invoke(new Action(() =>
                {
                    if (e.PosResult)
                    {
                        if (e.CardNo != CardNo_Old || (DateTime.Now - LastCardTime).TotalSeconds > 5)
                        {
                            CardNo_Old = e.CardNo;
                            LastCardTime = DateTime.Now;
                            //根据读到的卡号获取相关的学生信息和读者状态 
                            PosCardHandle(e.CardNo);
                        }
                        //else
                        //{
                        //    HandelMessage("   刷卡频繁，请在" + (5 - (DateTime.Now - LastCardTime).TotalSeconds).ToString().Split('.')[0] + "秒后再试！", EnumSimpleTipFormIco.Small);
                        //}
                    }
                    else
                    {
                        WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
                    }
                }));
                clientObject.ObjCardReader.Start();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("操作遇到异常：" + ex.Message);
                HandelMessage("   处理遇到异常，可能是因为网络原因导致，详细信息请查看错误日志。", EnumSimpleTipFormIco.Cry);
            }
        }

        /// <summary>
        /// 刷卡结果处理
        /// </summary>
        private void PosCardHandle(string cardNo)
        {
            try
            {
                clientObject.ReaderInfo = EnterOutOperate.GetReaderInfo(cardNo);
                #region 判断当前读者状态
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (clientObject.ReaderInfo.EnterOutLog != null && clientObject.ReaderInfo.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    nowReaderStatus = clientObject.ReaderInfo.EnterOutLog.EnterOutState;
                }
                else if (clientObject.ReaderInfo.BespeakLog.Count > 0)
                {
                    nowReaderStatus = EnterOutLogType.BespeakWaiting;
                }
                else if (clientObject.ReaderInfo.WaitSeatLog != null)
                {
                    nowReaderStatus = EnterOutLogType.Waiting;
                }
                #endregion


                switch (nowReaderStatus)
                {
                    case EnterOutLogType.Leave:
                    case EnterOutLogType.BespeakWaiting:
                    case EnterOutLogType.Waiting:
                        HandelMessage("   没有座位，谢谢配合。", EnumSimpleTipFormIco.Small);
                        break;
                    //posCardHandle.BespeakSeatWait();
                    //break;
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                        LeaveHandle();//读者进行离开操作
                        break;
                    case EnterOutLogType.ShortLeave:
                        posCardHandle.CometoBack();//暂时离开回来
                        break;
                    default:
                        HandelMessage("   没有座位，谢谢配合。", EnumSimpleTipFormIco.Small);
                        break;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("操作遇到异常：" + ex.Message);
                HandelMessage("   处理遇到异常，可能是因为网络原因导致，详细信息请查看错误日志。", EnumSimpleTipFormIco.Cry);
            }
        }

        void posCardHandle_HandleResult(string m, SeatManage.EnumType.HandleResult r)
        {
            if (r == HandleResult.Failed)
            {
                HandelMessage(m, EnumSimpleTipFormIco.Cry);
            }
            else if (r == HandleResult.Successed)
            {
                HandelMessage(m, EnumSimpleTipFormIco.Small);
            }
        }
        /// <summary>
        /// 处理结果提示消息
        /// </summary>
        /// <param name="message"></param>
        private void HandelMessage(string message, EnumSimpleTipFormIco formIco)
        {
            if (WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.notifyIcon1.ShowBalloonTip(5000, "离开终端", message, ToolTipIcon.Info);
            }
            else
            {
                Tip_SimpleTip tip = new Tip_SimpleTip();
                tip.Message = message;
                tip.TipIcon = formIco;
                tip.Show();
            }
        }
        private void LeaveHandle()
        {
            EnterOutLogType leaveHandel = EnterOutLogType.None;
            if (LeaveClientSetting.LeaveState == LeaveState.Choose)
            {
                LeaveSeatForm leaveForm = new LeaveSeatForm();
                leaveForm.ShowDialog();
                leaveHandel = leaveForm.ChooseEnterOutState;
            }
            else if
               (LeaveClientSetting.LeaveState == LeaveState.FreeSeat)
            {
                leaveHandel = EnterOutLogType.Leave;
            }
            else if (LeaveClientSetting.LeaveState == LeaveState.ShortLeave)
            {
                leaveHandel = EnterOutLogType.ShortLeave;
            }
            else if (LeaveClientSetting.LeaveState == LeaveState.ContinuedTime)
            {
                leaveHandel = EnterOutLogType.ContinuedTime;
            }
            switch (leaveHandel)
            {
                case EnterOutLogType.Leave:
                    posCardHandle.Leave();
                    break;
                case EnterOutLogType.ShortLeave:
                    posCardHandle.ShortLeave();
                    break;
                case EnterOutLogType.ContinuedTime:
                    posCardHandle.ContinuedTime();
                    break;

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            InitializeFormState();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != CardNo_Old || (DateTime.Now - LastCardTime).TotalSeconds > 5)
            {
                CardNo_Old = textBox1.Text;
                LastCardTime = DateTime.Now;
                PosCardHandle(textBox1.Text);
            }
            //else
            //{
            //    HandelMessage("   刷卡频繁，请在" + (5-(DateTime.Now - LastCardTime).TotalSeconds).ToString().Split('.')[0]  + "秒后再试！", EnumSimpleTipFormIco.Small);
            //}
        }

        private void MainForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Normal)
            {
                this.Hide();
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
                notifyIcon1.ContextMenu = new ContextMenu(new MenuItem[] { menuItem3, menuItem1 });
            }
        }
    }
}
