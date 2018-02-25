using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatManage.Bll;
using System.Threading;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Windows.Media;
using SeatManage.EnumType;
using SeatClientV3.Code;
using SeatManage.SeatManageComm;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.IO;
using SeatClientV3.FunWindow;

namespace SeatClientV3.ViewModel
{
    public class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public MainWindow_ViewModel()
        {
            try
            {
                //绑定学校logo
                string logoPath = AppDomain.CurrentDomain.BaseDirectory + clientObject.ClientSetting.DeviceSetting.BackImgage["SchoolLogoImage"];
                if (File.Exists(logoPath))
                {
                    LogoImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(logoPath, UriKind.RelativeOrAbsolute));
                }
                posCardHandle = ReadCardOperator.GetInstance();
                WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
                WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
                WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
                WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
                clientObject.UpdateForm += new EventHandler(clientObject_UpdateForm);
                Printer.PrinterException += new PrinterStatusEventHandle(Printer_PrinterException);
                if (!clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                {
                    ActiveBokkBtn = "Collapsed";
                }
                if (clientObject.ClientSetting.DeviceSetting.IsShowInitPOS)
                {
                    CardReaderBtn = "Visible";
                }
                if (clientObject.IsTestModel)
                {
                    TestMode = "Visible";
                    CardReaderBtn = "Collapsed";
                }
            }
            catch (Exception ex)
            {
                //TODO:出错处理
            }

        }

        /// <summary>
        /// 基础类
        /// </summary>
        public SeatClientV3.OperateResult.SystemObject clientObject
        {
            get { return SeatClientV3.OperateResult.SystemObject.GetInstance(); }
        }

        private System.Windows.Media.Imaging.BitmapImage _LogoImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// Logo
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage LogoImage
        {
            get { return _LogoImage; }
            set { _LogoImage = value; OnPropertyChanged("LogoImage"); }
        }
      
        //    try
        //    {
        //        posCardHandle = ReadCardOperator.GetInstance();
        //        WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
        //        WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
        //        WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
        //        WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
        //        clientObject.UpdateForm += new EventHandler(clientObject_UpdateForm);
        //        Printer.PrinterException += new PrinterStatusEventHandle(Printer_PrinterException);
        //        if (!clientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
        //        {
        //            ActiveBokkBtn = "Collapsed";
        //        }
        //        if (clientObject.ClientSetting.DeviceSetting.IsShowInitPOS)
        //        {
        //            CardReaderBtn = "Visible";
        //        }
        //        if (clientObject.IsTestModel)
        //        {
        //            TestMode = "Visible";
        //            CardReaderBtn = "Collapsed";
        //        }
        //        string logoPath = AppDomain.CurrentDomain.BaseDirectory + clientObject.ClientSetting.DeviceSetting.BackImgage["SchoolLogoImage"];
        //        if (File.Exists(logoPath))
        //        {
        //            LogoImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(logoPath, UriKind.RelativeOrAbsolute));
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        //TODO:出错处理
        //    }
        //}
        /// <summary>
        /// 打印机故障
        /// </summary>
        /// <param name="printerStatus"></param>
        void Printer_PrinterException(Printer printerStatus)
        {
            switch (printerStatus)
            {
                case SeatManage.EnumType.Printer.NoPaper: PrintError = "打印纸不足，请联系管理员"; break;
                case SeatManage.EnumType.Printer.NotExist: PrintError = "监测不到打印机"; break;
                case SeatManage.EnumType.Printer.Unusual: PrintError = "打印机故障，请联系管理员"; break;
                default: PrintError = ""; break;
            }
        }
        /// <summary>
        /// 更新窗口尺寸
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientObject_UpdateForm(object sender, EventArgs e)
        {
            if (WindowHeight != clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y)
            {
                WindowHeight = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
                WindowWidth = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
                WindowLeft = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
                WindowTop = clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
            }
        }
        //#region 属性
        ReadCardOperator posCardHandle;

        private PrintSlip _Printer;
        /// <summary>
        /// 打印类
        /// </summary>
        public PrintSlip Printer
        {
            get { return PrintSlip.GetInstance(); ; }
        }

        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }

        private string _PrintError = "";
        /// <summary>
        /// 打印凭条提示
        /// </summary>
        public string PrintError
        {
            get { return _PrintError; }
            set { _PrintError = value; OnPropertyChanged("PrintError"); }
        }
              
        private string _CardReaderBtn = "Collapsed"; 
        /// <summary>
        /// 读卡激活按钮隐藏
        /// </summary>
        public string CardReaderBtn
        {
            get { return _CardReaderBtn; }
            set { _CardReaderBtn = value; }
        }

        private string _ActiveBokkBtn = "Visible";
        /// <summary>
        /// 预约激活按钮隐藏
        /// </summary>
        public string ActiveBokkBtn
        {
            get { return _ActiveBokkBtn; }
            set { _ActiveBokkBtn = value; }
        }

        private string _TestMode = "Collapsed";
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; OnPropertyChanged("TestMode"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private static int _SchoolNotices;
        /// <summary>
        /// 校园通知条数
        /// </summary>
        public static int SchoolNotices
        {
            get { return _SchoolNotices; }
            set { _SchoolNotices = value; }
        }
        private string _ImgCardPostVisibility = "Hidden";
        /// <summary>
        /// 请刷卡图片显示影藏
        /// </summary>
        public string ImgCardPostVisibility
        {
            get { return _ImgCardPostVisibility; }
            set { _ImgCardPostVisibility = value; OnPropertyChanged("ImgCardPostVisibility"); }
        }
        private string _UCSchoolNoticeVisibility = "Collapsed";

        public string UCSchoolNoticeVisibility
        {
            get { return _UCSchoolNoticeVisibility; }
            set { _UCSchoolNoticeVisibility = value; OnPropertyChanged("UCSchoolNoticeVisibility"); }
        }
        //#endregion


        #region 刷卡处理
        public void PosCardHandle(string cardNo)
        {

            try
            {
                clientObject.EnterOutLogData = new OperateResult.OperateResult();
                clientObject.EnterOutLogData.Student = SeatManage.Bll.EnterOutOperate.GetReaderInfo(cardNo);
                #region 判断当前读者状态
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (clientObject.EnterOutLogData.Student.EnterOutLog != null && clientObject.EnterOutLogData.Student.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    //如果记录不为空，设置为当前记录状态
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
                //如果有未读的消息则显示消息窗口
                if (clientObject.EnterOutLogData.Student.NoticeInfo.Count > 0)
                {
                    ReaderNoticeWindow noteWindow = new ReaderNoticeWindow();
                    noteWindow.ShowDialog();
                }
                //根据读者状态进入不同操作
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
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("执行遇到错误：{0}", ex.Message));
                SeatClientV3.FunWindow.MessageWindow popWindow = new FunWindow.MessageWindow(SeatManage.EnumType.MessageType.Exception);
                popWindow.ShowDialog();
            }
            finally
            {
                clientObject.EnterOutLogData = null;
            }
        }
        #endregion

        #region 预约激活
        public void ActiveBook()
        {
            try
            {
                ActivationWindow activationWindow = new ActivationWindow();
                activationWindow.ShowDialog();
                if (activationWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    clientObject.EnterOutLogData = new OperateResult.OperateResult();
                    clientObject.EnterOutLogData.Student = SeatManage.Bll.EnterOutOperate.GetReaderInfo(activationWindow.viewModel.CardNo);
                    if (clientObject.EnterOutLogData.Student == null)
                    {
                        clientObject.EnterOutLogData.Student = new SeatManage.ClassModel.ReaderInfo() { CardNo = activationWindow.viewModel.CardNo };
                    }
                    SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(activationWindow.viewModel.CardNo);
                    if (user != null)
                    {
                        if (user.IsUsing == SeatManage.EnumType.LogStatus.Valid)//判断用户状态是否启用。
                        {
                            MessageWindow DeactivationComfrimWindow = new MessageWindow(MessageType.DeactivationComfrim);//启用中，则提示取消
                            DeactivationComfrimWindow.ShowDialog();
                            if (DeactivationComfrimWindow.viewModel.OperateResule == HandleResult.Successed)
                            {
                                user.IsUsing = SeatManage.EnumType.LogStatus.Fail;//
                                user.Remark = "终端刷卡注销";
                                SeatManage.Bll.Users_ALL.UpdateUserOnlyInfo(user);
                                MessageWindow activeCloseWindow = new MessageWindow(MessageType.DeactivationSuccess);//启用中，则提示取消
                                activeCloseWindow.ShowDialog();
                            }
                        }
                        else
                        {//如果读者用户状态是失效，则重新激活。
                            user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                            user.Password = MD5Algorithm.GetMD5Str32(activationWindow.viewModel.CardNo);
                            user.Remark = "终端刷卡重新激活";
                            if (SeatManage.Bll.Users_ALL.UpdateUserOnlyInfo(user))
                            {
                                MessageWindow activeSuccessWindow = new MessageWindow(MessageType.ActivationSuccess);//启用中，则提示取消
                                activeSuccessWindow.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        SeatManage.ClassModel.UserInfo newUser = new SeatManage.ClassModel.UserInfo();
                        newUser.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                        newUser.LoginId = activationWindow.viewModel.CardNo;
                        newUser.Password = MD5Algorithm.GetMD5Str32(activationWindow.viewModel.CardNo);
                        newUser.UserType = SeatManage.EnumType.UserType.Reader;
                        newUser.UserName = clientObject.EnterOutLogData.Student == null ? "" : clientObject.EnterOutLogData.Student.Name;
                        newUser.Remark = "在终端刷卡激活";
                        if (SeatManage.Bll.Users_ALL.AddNewUser(newUser))
                        {
                            MessageWindow activeSuccessWindow = new MessageWindow(MessageType.ActivationSuccess);//启用中，则提示取消
                            activeSuccessWindow.ShowDialog();
                        }
                    }
                    //预约激活处理
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("预约激活遇到异常" + ex.Message);
                MessageWindow errorWindow = new MessageWindow(MessageType.Exception);
                errorWindow.ShowDialog();
            }
            finally
            {
                clientObject.EnterOutLogData = null;
            }
        }
        #endregion

    }
}
