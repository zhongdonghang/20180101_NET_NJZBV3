using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Threading;
using System.Drawing.Printing;
using System.IO;
using System.Xml;
using System.Printing;
using WPF_Seat;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using SeatClient.Class;
using SeatManage.InterfaceFactory;
using SeatManage.ISystemTerminal.IPOS;
using SeatManage.Bll;
using SeatManage.SeatManageComm;
using SeatManage.EnumType;
using SeatClient.OperateResult;
using SeatManage.ClassModel;
namespace SeatClient
{
    public partial class EnterOutForm : Form
    {

        //初始化
        public EnterOutForm()
        {
            InitializeComponent();
            InitializeComponentPart2();
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        private void InitializeComponentPart2()
        {
            try
            {
                if (clientObject.ClientSetting == null)
                {
                    SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                    tip.ShowDialog();
                    return;
                }
                clientObject.UpdateConfigError += new EventHandler(clientObject_UpdateConfigError);
                clientObject.UpdateForm += new EventHandler(clientObject_UpdateForm);

                #region 初始化窗体大小、位置和背景
                this.BackgroundImage = clientObject.BackgroundImagesResource["EnterOutForm"]; //背景图片    
                this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y);
                this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y);
                if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1080)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                    }
                    labnotice.Location = new System.Drawing.Point(720, 860);

                    picBoxPartnersLogo.Size = new Size(1080, 520);
                    picBoxPartnersLogo.Location = new Point(0, 225);

                    BookActivation.Location = new Point(390, 850);
                    BookActivation.Size = new Size(137, 54);
                    BookActivation.BackgroundImage = clientObject.BackgroundImagesResource["BookActivation"];
                    BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                    if (clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                    {
                        btnQureyLog.Location = new Point(550, 850);
                    }
                    else
                    {
                        btnQureyLog.Location = new Point(470, 850);
                    }
                    btnQureyLog.Size = new Size(137, 54);
                    btnQureyLog.BackgroundImage = clientObject.BackgroundImagesResource["btnQureyLog"];

                }
                else if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1024)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                    }
                    labnotice.Location = new System.Drawing.Point(719, 627);

                    picBoxPartnersLogo.Size = new Size(1024, 406);
                    picBoxPartnersLogo.Location = new Point(0, 170);

                    BookActivation.Location = new Point(360, 650);
                    BookActivation.Size = new Size(137, 54);
                    BookActivation.BackgroundImage = clientObject.BackgroundImagesResource["BookActivation"];
                    BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                    if (clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                    {
                        btnQureyLog.Location = new Point(525, 650);
                    }
                    else
                    {
                        btnQureyLog.Location = new Point(445, 650);
                    }
                    btnQureyLog.Size = new Size(137, 54);
                    btnQureyLog.BackgroundImage = clientObject.BackgroundImagesResource["btnQureyLog"];
                    lblDate.Location = new Point(350, 8);
                    lblTime.Location = new Point(595, 8);
                }

                BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                btnResetPOS.Visible = clientObject.ClientSetting.DeviceSetting.IsShowInitPOS;
                #endregion
                #region 初始化读卡器接口对象

                try
                {
                    // ApplicationInitialize.CheckCardReader();
                    if (clientObject.ObjCardReader != null)
                    {
                        clientObject.ObjCardReader.CardNoGeted += new EventPosCardNo(ObjCardReader_CardNoGeted);
                        clientObject.ObjCardReader.Start();
                        btnResetPOS.Visible = clientObject.ClientSetting.DeviceSetting.IsShowInitPOS;
                        textBox1.Visible = false;
                        button3.Visible = false;
                    }
                    else
                    {
                        textBox1.Visible = true;
                        button3.Visible = true;
                    }
                }
                catch
                    (Exception ex)
                {
                    MessageBox.Show("读卡器初始化失败");
                }

                #endregion
            }
            catch (KeyNotFoundException ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("系统初始化错误：背景图片出现异常，" + ex.Message);
                return;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("系统初始化错误：" + ex.Message);
                //TODO:系统执行错误提示 
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                tip.ShowDialog();
                return;
            }
        }

        void clientObject_UpdateForm(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                #region 初始化窗体大小、位置和背景
                this.Location = new Point(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y);
                this.Size = new System.Drawing.Size(clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X, clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y);
                if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1080)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                    }
                    labnotice.Location = new System.Drawing.Point(720, 860);

                    picBoxPartnersLogo.Size = new Size(1080, 520);
                    picBoxPartnersLogo.Location = new Point(0, 225);

                    BookActivation.Location = new Point(390, 850);
                    BookActivation.Size = new Size(137, 54);
                    BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                    if (clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                    {
                        btnQureyLog.Location = new Point(550, 850);
                    }
                    else
                    {
                        btnQureyLog.Location = new Point(470, 850);
                    }
                    btnQureyLog.Size = new Size(137, 54);

                }
                else if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1024)
                {
                    if (clientObject.HardAdvert != null && clientObject.HardAdvert.AdvertImage != null)
                    {
                        picBoxPartnersLogo.BackgroundImage = ImageStream.StreamToImage(clientObject.HardAdvert.AdvertImage);
                    }
                    labnotice.Location = new System.Drawing.Point(719, 627);

                    picBoxPartnersLogo.Size = new Size(1024, 406);
                    picBoxPartnersLogo.Location = new Point(0, 170);

                    BookActivation.Location = new Point(360, 650);
                    BookActivation.Size = new Size(137, 54);
                    BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                    if (clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                    {
                        btnQureyLog.Location = new Point(525, 650);
                    }
                    else
                    {
                        btnQureyLog.Location = new Point(445, 650);
                    }
                    btnQureyLog.Size = new Size(137, 54);
                    lblDate.Location = new Point(350, 8);
                    lblTime.Location = new Point(595, 8);
                }
                BookActivation.Visible = clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat;
                btnResetPOS.Visible = clientObject.ClientSetting.DeviceSetting.IsShowInitPOS;
                #endregion

            }));
        }

        void clientObject_UpdateConfigError(object sender, EventArgs e)
        {
            this.Invoke(new Action(() =>
            {
                SystemObject obj = sender as SystemObject;
                obj.StopUpdateConfig();
                obj.ObjCardReader.Stop();
                AppSkin appSkin = new AppSkin();
                this.Hide();
                appSkin.ShowDialog();
                if (appSkin.InitializeState == HandleResult.Successed)
                {
                    obj.ObjCardReader.Start();
                    obj.StartAutoUpdateConfig();
                    this.Show();
                }
                else
                {
                    Application.Exit();
                }

            }));


        }

        #region  变量
        SystemObject clientObject = SystemObject.GetInstance();
        RouteResultHandle posCardHandle = RouteResultHandle.GetInstance();
        PrintSlip printer = PrintSlip.GetInstance();
        /// <summary>
        /// 错误界面窗体
        /// </summary>
        AppSkin error = null;
        /// <summary>
        /// 服务器时间处理
        /// </summary>
        ShowDateTime serviceDateTime = new ShowDateTime();
        #endregion

        private void EnterOutForm_Load(object sender, EventArgs e)
        {
            serviceDateTime.ShowHandle += new EventHandler(serviceDateTime_ShowHandle);
            printer.PrinterException += new PrinterStatusEventHandle(printer_PrinterException);
        }

        void printer_PrinterException(Printer printerStatus)
        {
            if (printerStatus == Printer.NoPaper)
            {
                this.Invoke(new Action(() =>
                {
                    labnotice.Visible = true;
                    labnotice.Text = "暂时无法提供座位凭条";
                }));
            }
            else
            {
                this.Invoke(new Action(() =>
                {
                    labnotice.Visible = false;
                }));
            }

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
            this.Invoke(new Action(() =>
            {
                if (e.PosResult)
                {
                    //根据读到的卡号获取相关的学生信息和读者状态 
                    PosCardHandle(e.CardNo);
                }
                else
                {
                    WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
                }
            }));
            clientObject.ObjCardReader.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PosCardHandle(textBox1.Text);
        }

        private void btnResetPOS_Click(object sender, EventArgs e)
        {
            clientObject.ObjCardReader.Reset();
        }

        #region 刷卡处理
        /// <summary>
        /// 刷卡结果处理
        /// </summary>
        private void PosCardHandle(string cardNo)
        {
            WPF_ReaderInfo.MainWindow read = new WPF_ReaderInfo.MainWindow();


            try
            {
                clientObject.EnterOutLogData.Student = EnterOutOperate.GetReaderInfo(cardNo);
                #region 判断当前读者状态
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (clientObject.EnterOutLogData.Student.EnterOutLog != null && clientObject.EnterOutLogData.Student.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    nowReaderStatus = clientObject.EnterOutLogData.Student.EnterOutLog.EnterOutState;
                }
                else if (clientObject.EnterOutLogData.Student.BespeakLog.Count > 0)
                {
                    nowReaderStatus = EnterOutLogType.BespeakWaiting;
                }
                else if (clientObject.EnterOutLogData.Student.WaitSeatLog != null)
                {
                    nowReaderStatus = EnterOutLogType.Waiting;
                }
                #endregion

                posCardHandle.ShowNotice();
                read.reader.ReaderInfo = clientObject.EnterOutLogData.Student;
                if (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X == 1080)
                {
                    read.Left = 0;
                    read.Top = 720;
                }
                else
                {
                    read.Left = 1024;
                    read.Top = 0;
                }
                read.Width = 350;
                read.Height = 200;
                read.Show();

                switch (nowReaderStatus)
                {
                    case EnterOutLogType.Leave:
                        clientObject.EnterOutLogData.EnterOutlog = new SeatManage.ClassModel.EnterOutLogInfo();
                        clientObject.EnterOutLogData.EnterOutlog.CardNo = cardNo;
                        posCardHandle.ChooseSeat();
                        break;
                    case EnterOutLogType.BespeakWaiting:
                        posCardHandle.BespeakSeatWait();
                        break;
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                        clientObject.EnterOutLogData.EnterOutlog = clientObject.EnterOutLogData.Student.EnterOutLog;
                        posCardHandle.LeaveOperate();
                        break;
                    case EnterOutLogType.ShortLeave:
                        clientObject.EnterOutLogData.EnterOutlog = clientObject.EnterOutLogData.Student.EnterOutLog;
                        posCardHandle.CometoBack();
                        break;
                    case EnterOutLogType.Waiting:
                        posCardHandle.WaitingSeat();
                        break;
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write(string.Format("执行遇到错误：{0}", ex.Message));
                SeatManage.SeatClient.Tip.Tip_Framework tip = new SeatManage.SeatClient.Tip.Tip_Framework(SeatManage.EnumType.TipType.Exception, 7);//显示提示窗体
                tip.ShowDialog();
            }
            finally
            {
                read.Close();
                posCardHandle.Resetting();//执行初始化操作

            }
        }

        #endregion

        private void EnterOutForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            clientObject.StopUpdateConfig();
            clientObject.UpdateConfigError -= clientObject_UpdateConfigError;
            clientObject.UpdateForm -= clientObject_UpdateForm;
            serviceDateTime.ShowHandle -= new EventHandler(serviceDateTime_ShowHandle);
            serviceDateTime.Stop();
            Application.ExitThread();

        }
        /// <summary>
        /// 激活预约功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BookActivation_Click(object sender, EventArgs e)
        {
            //注销刷卡事件监听
            if (clientObject.ObjCardReader != null)
            {
                BookActivation bespeakActive = new SeatClient.BookActivation();
                clientObject.ObjCardReader.CardNoGeted -= new EventPosCardNo(ObjCardReader_CardNoGeted);
                bespeakActive.ShowDialog();
                clientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
            }
            else
            {
                BookActivation bespeakActive = new SeatClient.BookActivation();
                bespeakActive.ShowDialog();
            }

        }

        private void btnQureyLog_Click(object sender, EventArgs e)
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
                FrmShowEnterOutLog formEnterOutLog = new FrmShowEnterOutLog();
                formEnterOutLog.ShowDialog();
                clientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
            }
            else
            {
                FrmShowEnterOutLog formEnterOutLog = new FrmShowEnterOutLog();
                formEnterOutLog.ShowDialog();
            }
        }

        private void picBoxPartnersLogo_Click(object sender, EventArgs e)
        {

        }
    }
}