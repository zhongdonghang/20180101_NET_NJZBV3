using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SeatClientV3.Code;
using SeatClientV3.OperateResult;
using SeatClientV3.WindowObject;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatClientV3.ViewModel
{
    public delegate void EventHandlerImageMove(int x, int y);
    public delegate void EventHandlerCheckCodeChange();
    public class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public MainWindow_ViewModel()
        {
            try
            {
                posCardHandle = ReadCardOperator.GetInstance();
                WindowHeight = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y;
                WindowWidth = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X;
                WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X;
                WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y;
                Printer.PrinterException += Printer_PrinterException;
                if (!ClientObject.ClientSetting.DeviceSetting.UsingActiveBespeakSeat)
                {
                    ActiveBokkBtn = "Collapsed";
                }
                if (!ClientObject.UseCodeCheck)
                {
                    WeiCharBtn = "Collapsed";
                }
                if (ClientObject.ClientSetting.DeviceSetting.IsShowInitPOS)
                {
                    CardReaderBtn = "Visible";
                }
                if (ClientObject.ObjCardReader == null)
                {
                    TestMode = "Visible";
                    CardReaderBtn = "Collapsed";
                }
                string logoPath = AppDomain.CurrentDomain.BaseDirectory + ClientObject.ClientSetting.DeviceSetting.BackImgage["SchoolLogoImage"];
                if (File.Exists(logoPath))
                {
                    LogoImage = new BitmapImage(new Uri(logoPath, UriKind.RelativeOrAbsolute));
                }
                Apppath = AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\";
                GetAdvertImage();
            }
            catch (Exception ex)
            {
                //TODO:出错处理
            }
        }
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
        #region 属性


        private string Apppath;
        ReadCardOperator posCardHandle;
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }
        private PrintSlip _Printer;
        /// <summary>
        /// 打印类
        /// </summary>
        public PrintSlip Printer
        {
            get { return PrintSlip.GetInstance(); ; }
        }
        private double _WindowHeight;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }

        private DateTime _NowDateTime;
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime NowDateTime
        {
            get { return _NowDateTime; }
            set { _NowDateTime = value; OnPropertyChanged("NowDateTimeString"); }
        }
        /// <summary>
        /// 显示时间
        /// </summary>
        public string NowDateTimeString
        {
            get { return string.Format("{0}年{1}月{2}日 {3} {4}", _NowDateTime.Year, _NowDateTime.Month, _NowDateTime.Day, CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_NowDateTime.DayOfWeek), _NowDateTime.ToLongTimeString()); }
        }

        private int _LastSeatCount;
        /// <summary>
        /// 剩余座位数目
        /// </summary>
        public string LastSeat
        {
            get { return "剩 余\n座 位\n" + _LastSeatCount; }
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
        private string _Usage = "";
        /// <summary>
        /// 使用说明
        /// </summary>
        public string Usage
        {
            get { return _Usage; }
            set { _Usage = value; OnPropertyChanged("Usage"); }
        }

        private List<BitmapImage> _SchoolNoteImage = new List<BitmapImage>();
        /// <summary>
        /// 校园通知图片
        /// </summary>
        public List<BitmapImage> SchoolNoteImage
        {
            get { return _SchoolNoteImage; }
            set { _SchoolNoteImage = value; OnPropertyChanged("SchoolNoteImage"); }
        }

        private List<BitmapImage> _PromotionImage = new List<BitmapImage>();
        /// <summary>
        /// 推广广告图片
        /// </summary>
        public List<BitmapImage> PromotionImage
        {
            get { return _PromotionImage; }
            set { _PromotionImage = value; OnPropertyChanged("PromotionImage"); }
        }
        private List<BitmapImage> _UserGuideImage = new List<BitmapImage>();
        /// <summary>
        /// 使用手册图片
        /// </summary>
        public List<BitmapImage> UserGuideImage
        {
            get { return _UserGuideImage; }
            set { _UserGuideImage = value; OnPropertyChanged("UserGuideImage"); }
        }

        private AdType _NowTap = AdType.None;
        /// <summary>
        /// 当前显示标签
        /// </summary>
        public AdType NowTap
        {
            get { return _NowTap; }
            set { _NowTap = value; }
        }

        private BitmapImage _LogoImage = new BitmapImage();
        /// <summary>
        /// Logo
        /// </summary>
        public BitmapImage LogoImage
        {
            get { return _LogoImage; }
            set { _LogoImage = value; OnPropertyChanged("LogoImage"); }
        }

        /// <summary>
        /// 向左按钮隐藏
        /// </summary>
        private string _LeftBtn = "Collapsed";
        /// <summary>
        /// 
        /// </summary>
        public string LeftBtn
        {
            get { return _LeftBtn; }
            set { _LeftBtn = value; OnPropertyChanged("LeftBtn"); }
        }

        /// <summary>
        /// 向右按钮隐藏
        /// </summary>
        private string _RightBtn = "Collapsed";
        /// <summary>
        /// 
        /// </summary>
        public string RightBtn
        {
            get { return _RightBtn; }
            set { _RightBtn = value; OnPropertyChanged("RightBtn"); }
        }

        /// <summary>
        /// 读卡激活按钮隐藏
        /// </summary>
        private string _CardReaderBtn = "Collapsed";
        /// <summary>
        /// 
        /// </summary>
        public string CardReaderBtn
        {
            get { return _CardReaderBtn; }
            set { _CardReaderBtn = value; }
        }
        /// <summary>
        /// 预约激活按钮隐藏
        /// </summary>
        private string _ActiveBokkBtn = "Visible";
        /// <summary>
        /// 
        /// </summary>
        public string ActiveBokkBtn
        {
            get { return _ActiveBokkBtn; }
            set { _ActiveBokkBtn = value; }
        }
        /// <summary>
        /// 预约激活按钮隐藏
        /// </summary>
        private string _weicharBtn = "Visible";
        /// <summary>
        /// 
        /// </summary>
        public string WeiCharBtn
        {
            get { return _weicharBtn; }
            set { _weicharBtn = value; }
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
        /// <summary>
        /// 是否显示二维码
        /// </summary>
        public string ShowCode
        {
            get { return ClientObject.UseCodeCheck ? "Visible" : "Collapsed"; }
        }
        private ImageBrush _codeImage = new ImageBrush();
        /// <summary>
        /// Logo
        /// </summary>
        public ImageBrush CodeImage
        {
            get { return _codeImage; }
            set { _codeImage = value; OnPropertyChanged("CodeImage"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion


        #region 方法
        #region 时间更新
        public TimeLoop timeDateTimeShow;
        public TimeLoop timeDateTimeSync;
        Thread showTimeThread;
        Thread syncTimeThread;
        public void ShowTimeRun()
        {
            NowDateTime = ServiceDateTime.Now;
            timeDateTimeShow = new TimeLoop(1000);
            timeDateTimeShow.TimeTo += timeDateTimeShow_TimeTo;
            showTimeThread = new Thread(timeDateTimeShow.TimeStrat);
            showTimeThread.Start();
            timeDateTimeSync = new TimeLoop(300000);
            timeDateTimeSync.TimeTo += timeDateTimeSync_TimeTo;
            syncTimeThread = new Thread(timeDateTimeSync.TimeStrat);
            syncTimeThread.Start();
        }
        //一秒执行
        void timeDateTimeShow_TimeTo(object sender, EventArgs e)
        {
            NowDateTime = NowDateTime.AddSeconds(1);
        }
        //5min执行
        void timeDateTimeSync_TimeTo(object sender, EventArgs e)
        {
            NowDateTime = ServiceDateTime.Now;
        }
        #endregion

        #region 座位数

        Thread MyLastSeatSum;
        public TimeLoop MyLastSeatSumTime;
        public void LastSeatRun()
        {
            _LastSeatCount = TerminalOperatorService.LastSeatCount(ClientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeat");
            MyLastSeatSumTime = new TimeLoop(30 * 1000);
            MyLastSeatSumTime.TimeTo += MyLastSeatSumTime_TimeTo;
            MyLastSeatSum = new Thread(timeDateTimeShow.TimeStrat);
            MyLastSeatSum.Start();
        }

        void MyLastSeatSumTime_TimeTo(object sender, EventArgs e)
        {
            MyLastSeatSumTime.TimeStop();
            _LastSeatCount = TerminalOperatorService.LastSeatCount(ClientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeat");
            MyLastSeatSumTime.TimeStrat();
        }


        #endregion

        #region 签到二维码
        public TimeLoop MyCheckCodeTime;

        public event EventHandlerCheckCodeChange CodeChange;
        public void CheckCodeRun()
        {
            MyCheckCodeTime = new TimeLoop(10 * 1000);
            MyCheckCodeTime.TimeTo += MyCheckCodeTime_TimeTo;
            MyCheckCodeTime.TimeStrat();
        }


        void MyCheckCodeTime_TimeTo(object sender, EventArgs e)
        {
            MyCheckCodeTime.TimeStop();
            if (CodeChange != null)
            {
                CodeChange();
            }
            MyCheckCodeTime.TimeStrat();
        }
        #endregion


        #region 图片切换
        public event EventHandlerImageMove ImageChange;
        public event EventHandlerImageMove ImageSwitch;
        public TimeLoop ImgTime;
        public TimeLoop ImgTimeStop;
        int noticeNum = -1;
        int promotionNum = -1;
        int guideNum = -1;
        /// <summary>
        /// 执行图片切换
        /// </summary>
        private void GetAdvertImage()
        {
            try
            {
                if (ClientObject.UserGuide != null)
                {
                    for (int i = 0; i < ClientObject.UserGuide.ImageFilePath.Count; i++)
                    {
                        try
                        {
                            UserGuideImage.Add(new BitmapImage(new Uri(Apppath + "UserGuide\\" + ClientObject.UserGuide.ImageFilePath[i], UriKind.RelativeOrAbsolute)));
                        }
                        catch (Exception ex)
                        {
                            WriteLog.Write("加载使用手册图片" + ClientObject.UserGuide.ImageFilePath[i] + "失败：" + ex.Message);
                        }
                    }
                }
                for (int i = 0; i < ClientObject.PromotionAdvert.Count; i++)
                {
                    try
                    {
                        PromotionImage.Add(new BitmapImage(new Uri(Apppath + "PromotionImage\\" + ClientObject.PromotionAdvert[i].AdImagePath, UriKind.RelativeOrAbsolute)));
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write("加载校园推广图片" + ClientObject.PromotionAdvert[i].AdImagePath + "失败：" + ex.Message);
                        ClientObject.PromotionAdvert.RemoveAt(i);
                        i--;
                    }
                }
                for (int i = 0; i < ClientObject.SchoolNote.Count; i++)
                {
                    try
                    {
                        SchoolNoteImage.Add(new BitmapImage(new Uri(Apppath + "NoteImage\\" + ClientObject.SchoolNote[i].NoteImagePath, UriKind.RelativeOrAbsolute)));
                    }
                    catch (Exception ex)
                    {
                        WriteLog.Write("加载学校通知图片" + ClientObject.SchoolNote[i].NoteImagePath + "失败：" + ex.Message);
                        ClientObject.SchoolNote.RemoveAt(i);
                        i--;
                    }
                }
                NowTap = AdType.SchoolNotice;
                ImgTime = new TimeLoop(10 * 1000);
                ImgTime.TimeTo += ImgTime_TimeTo;
                ImgTimeStop = new TimeLoop(10 * 1000);
                ImgTimeStop.TimeTo += ImgTimeStop_TimeTo;

            }
            catch (Exception ex)
            {
                WriteLog.Write("获取广告图片失败：" + ex.Message);
            }
        }

        public void ImageChangeRun()
        {
            if (ImgTime != null)
            {
                ImgTime.TimeStrat();
            }
        }
        public void ImageChangeStop()
        {
            if (ImgTime != null)
            {
                ImgTime.TimeStop();
            }
            if (ImgTimeStop != null)
            {
                ImgTimeStop.TimeStop();
            }
        }
        public void ImageChangePause()
        {
            if (ImgTime != null)
            {
                ImgTime.TimeStop();
            }
            if (ImgTimeStop != null)
            {
                ImgTimeStop.TimeStop();
                ImgTimeStop.TimeStrat();
            }
        }
        /// <summary>
        /// 图片停止切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImgTimeStop_TimeTo(object sender, EventArgs e)
        {
            ImgTimeStop.TimeStop();
            ImgTime.TimeStrat();
        }
        /// <summary>
        /// 图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImgTime_TimeTo(object sender, EventArgs e)
        {
            try
            {
                ImgTime.TimeStop();
                switch (NowTap)
                {
                    case AdType.PromotionAd:
                        if (PromotionImage.Count == 0)
                        {
                            NowTap = AdType.SchoolNotice;
                            goto case AdType.SchoolNotice;
                        }
                        if (promotionNum + 1 >= PromotionImage.Count)
                        {
                            promotionNum = -1;
                            NowTap = AdType.SchoolNotice;
                            goto case AdType.SchoolNotice;
                        }
                        promotionNum++;
                        SendEvent(promotionNum, 2);
                        ClientObject.PromotionAdvert[promotionNum].Usage.PlayCount++;
                        break;
                    case AdType.SchoolNotice:
                        if (SchoolNoteImage.Count == 0)
                        {
                            NowTap = AdType.None;
                            goto case AdType.None;
                        }
                        if (noticeNum + 1 >= SchoolNoteImage.Count)
                        {
                            noticeNum = -1;
                            NowTap = AdType.PromotionAd;
                            goto case AdType.PromotionAd;
                        }
                        noticeNum++;
                        SendEvent(noticeNum, 1);
                        break;
                    case AdType.None:
                        if (PromotionImage.Count > 0)
                        {
                            NowTap = AdType.PromotionAd;
                            goto case AdType.PromotionAd;
                        }
                        if (UserGuideImage.Count > 0)
                        {
                            if (guideNum + 1 >= UserGuideImage.Count)
                            {
                                guideNum = -1;
                            }
                            guideNum++;
                            SendEvent(guideNum, 0);
                        }
                        break;
                }
                BtnVisible();
            }
            catch (Exception ex)
            {
                WriteLog.Write("图片切换遇到异常遇到异常" + ex.Message);
            }
            finally
            {
                ImgTime.TimeStrat();
            }
        }
        /// <summary>
        /// 事件通知
        /// </summary>
        /// <param name="c"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void SendEvent(int x, int y)
        {
            if (ImageChange != null)
            {
                ImageChange(x, y);
            }
        }
        //图片向左
        public void ImageLeft()
        {
            try
            {
                switch (NowTap)
                {
                    case AdType.PromotionAd:

                        promotionNum--;
                        SendEvent(promotionNum, 2);
                        ; break;
                    case AdType.SchoolNotice:

                        noticeNum--;
                        SendEvent(noticeNum, 1);
                        ; break;
                    case AdType.None:
                        guideNum--;
                        SendEvent(guideNum, 0);
                        break;
                }
                BtnVisible();
            }
            catch (Exception ex)
            {
                WriteLog.Write("切换图片遇到异常" + ex.Message);
            }
        }
        //图片向右
        public void ImageRight()
        {
            try
            {
                switch (NowTap)
                {
                    case AdType.PromotionAd:

                        promotionNum++;
                        SendEvent(promotionNum, 2);
                        ; break;
                    case AdType.SchoolNotice:
                        noticeNum++;
                        SendEvent(noticeNum, 1);
                        ; break;
                    case AdType.None:
                        guideNum++;
                        SendEvent(guideNum, 0);
                        break;
                }
                BtnVisible();
            }
            catch (Exception ex)
            {
                WriteLog.Write("切换图片遇到异常" + ex.Message);
            }
        }
        /// <summary>
        /// 图片切换
        /// </summary>
        /// <param name="newType"></param>
        /// <returns></returns>
        public void ImageUpDown(AdType newType)
        {
            try
            {
                if (newType == NowTap)
                {
                    return;
                }
                NowTap = newType;

                switch (NowTap)
                {
                    case AdType.PromotionAd:
                        promotionNum = 0;
                        SendEvent(promotionNum, 2);
                        if (PromotionImage.Count != 0)
                        {
                            ClientObject.PromotionAdvert[promotionNum].Usage.PlayCount++;
                        }
                        break;
                    case AdType.SchoolNotice:
                        noticeNum = 0;
                        SendEvent(noticeNum, 1);
                        break;
                    case AdType.None:
                        guideNum = 0;
                        SendEvent(guideNum, 0);
                        break;
                }
                BtnVisible();
            }
            catch (Exception ex)
            {
                WriteLog.Write("切换图片遇到异常" + ex.Message);
            }
        }
        /// <summary>
        /// 按钮显示
        /// </summary>
        private void BtnVisible()
        {
            switch (NowTap)
            {
                case AdType.None:
                    LeftBtn = guideNum > 0 ? "Visible" : "Collapsed";
                    RightBtn = guideNum < UserGuideImage.Count - 1 ? "Visible" : "Collapsed";
                    break;
                case AdType.PromotionAd:
                    LeftBtn = promotionNum > 0 ? "Visible" : "Collapsed";
                    RightBtn = promotionNum < PromotionImage.Count - 1 ? "Visible" : "Collapsed";
                    break;
                case AdType.SchoolNotice:
                    LeftBtn = noticeNum > 0 ? "Visible" : "Collapsed";
                    RightBtn = noticeNum < SchoolNoteImage.Count - 1 ? "Visible" : "Collapsed";
                    break;
            }
        }

        #endregion

        #region 刷卡处理
        public void PosCardHandle(string cardNo)
        {

            try
            {
                ClientObject.EnterOutLogData = new OperateResult.OperateResult();
                ClientObject.EnterOutLogData.Student = EnterOutOperate.GetReaderSeatState(cardNo);
                #region 判断当前读者状态
                EnterOutLogType nowReaderStatus = EnterOutLogType.Leave;
                if (ClientObject.EnterOutLogData.Student.EnterOutLog != null && ClientObject.EnterOutLogData.Student.EnterOutLog.EnterOutState != EnterOutLogType.Leave)
                {
                    //如果记录不为空，设置为当前记录状态
                    nowReaderStatus = ClientObject.EnterOutLogData.Student.EnterOutLog.EnterOutState;
                }
                else if (ClientObject.EnterOutLogData.Student.WaitSeatLog != null)
                {
                    nowReaderStatus = EnterOutLogType.Waiting;
                }
                else if (ClientObject.EnterOutLogData.Student.BespeakLog.Count > 0)
                {
                    nowReaderStatus = EnterOutLogType.BespeakWaiting;
                }

                #endregion
                //如果有未读的消息则显示消息窗口
                if (ClientObject.EnterOutLogData.Student.NoticeInfo.Count > 0)
                {
                    ReaderNoteWindowObject.GetInstance().Window.ShowMessage();
                }
                //根据读者状态进入不同操作
                switch (nowReaderStatus)
                {
                    case EnterOutLogType.Leave:
                        ClientObject.EnterOutLogData.EnterOutlog = new EnterOutLogInfo();
                        ClientObject.EnterOutLogData.EnterOutlog.CardNo = cardNo;
                        posCardHandle.ChooseSeat();
                        break;
                    case EnterOutLogType.BespeakWaiting:
                        posCardHandle.BespeakCheck();
                        break;
                    case EnterOutLogType.BookingConfirmation:
                    case EnterOutLogType.SelectSeat:
                    case EnterOutLogType.ContinuedTime:
                    case EnterOutLogType.ComeBack:
                    case EnterOutLogType.ReselectSeat:
                    case EnterOutLogType.WaitingSuccess:
                        ClientObject.EnterOutLogData.EnterOutlog = ClientObject.EnterOutLogData.Student.EnterOutLog;
                        posCardHandle.LeaveOperate();
                        break;
                    case EnterOutLogType.ShortLeave:
                        ClientObject.EnterOutLogData.EnterOutlog = ClientObject.EnterOutLogData.Student.EnterOutLog;
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
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
            }
            finally
            {
                ClientObject.EnterOutLogData = null;
            }
        }
        #endregion

        #region 预约激活
        public void ActiveBook()
        {
            try
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ActivationReadCard);
                if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
                {
                    ClientObject.EnterOutLogData = new OperateResult.OperateResult();
                    ClientObject.EnterOutLogData.Student = EnterOutOperate.GetReaderInfo(PopupWindowsObject.GetInstance().Window.ViewModel.CardNo);
                    if (ClientObject.EnterOutLogData.Student == null)
                    {
                        ClientObject.EnterOutLogData.Student = new ReaderInfo { CardNo = PopupWindowsObject.GetInstance().Window.ViewModel.CardNo };
                    }
                    UserInfo user = Users_ALL.GetUserInfo(PopupWindowsObject.GetInstance().Window.ViewModel.CardNo);
                    if (user != null)
                    {
                        if (user.IsUsing == LogStatus.Valid)//判断用户状态是否启用。
                        {
                            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.CancelActivationWarn);
                            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule == HandleResult.Successed)
                            {
                                user.IsUsing = LogStatus.Fail;//
                                user.Remark = "终端刷卡注销";
                                Users_ALL.UpdateUserOnlyInfo(user);
                                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.CancelActivationSuccess);
                            }
                        }
                        else
                        {//如果读者用户状态是失效，则重新激活。
                            user.IsUsing = LogStatus.Valid;
                            user.Password = MD5Algorithm.GetMD5Str32(PopupWindowsObject.GetInstance().Window.ViewModel.CardNo);
                            user.Remark = "终端刷卡重新激活";
                            if (Users_ALL.UpdateUserOnlyInfo(user))
                            {
                                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ActivationSuccess);
                            }
                        }
                    }
                    else
                    {
                        UserInfo newUser = new UserInfo();
                        newUser.IsUsing = LogStatus.Valid;
                        newUser.LoginId = PopupWindowsObject.GetInstance().Window.ViewModel.CardNo;
                        newUser.Password = MD5Algorithm.GetMD5Str32(PopupWindowsObject.GetInstance().Window.ViewModel.CardNo);
                        newUser.UserType = UserType.Reader;
                        newUser.UserName = ClientObject.EnterOutLogData.Student == null ? "" : ClientObject.EnterOutLogData.Student.Name;
                        newUser.Remark = "在终端刷卡激活";
                        if (Users_ALL.AddNewUser(newUser))
                        {
                            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ActivationSuccess);
                        }
                    }
                    //预约激活处理
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("预约激活遇到异常" + ex.Message);
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
            }
            finally
            {
                ClientObject.EnterOutLogData = null;
            }
        }
        #endregion
        #endregion
    }
}
