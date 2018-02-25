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
using SeatClientV2.Code;
using SeatManage.SeatManageComm;
using System.Windows.Media.Animation;
using System.Windows.Controls;
using System.IO;

namespace SeatClientV2.ViewModel
{
    public class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public MainWindow_ViewModel()
        {
            try
            {
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
                string logoPath = AppDomain.CurrentDomain.BaseDirectory + clientObject.ClientSetting.DeviceSetting.BackImgage["SchoolLogoImage"];
                if (File.Exists(logoPath))
                {
                    LogoImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(logoPath, UriKind.RelativeOrAbsolute));
                }

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
        #region 属性
        private string Apppath = AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\";
        ReadCardOperator posCardHandle;
        /// <summary>
        /// 基础类
        /// </summary>
        public SeatClientV2.OperateResult.SystemObject clientObject
        {
            get { return SeatClientV2.OperateResult.SystemObject.GetInstance(); }
        }
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

        private DateTime _NowDateTime = new DateTime();
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
            get { return string.Format("{0}年{1}月{2}日 {3} {4}", _NowDateTime.Year, _NowDateTime.Month, _NowDateTime.Day, System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(_NowDateTime.DayOfWeek), _NowDateTime.ToLongTimeString()); }
        }

        private int _LastSeatCount = 0;
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

        private System.Windows.Media.Imaging.BitmapImage _SchoolNoteImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 校园通知图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage SchoolNoteImage
        {
            get { return _SchoolNoteImage; }
            set { _SchoolNoteImage = value; OnPropertyChanged("SchoolNoteImage"); }
        }

        private System.Windows.Media.Imaging.BitmapImage _PromotionImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 推广广告图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage PromotionImage
        {
            get { return _PromotionImage; }
            set { _PromotionImage = value; OnPropertyChanged("PromotionImage"); }
        }
        private System.Windows.Media.Imaging.BitmapImage _UserGuideImage = new System.Windows.Media.Imaging.BitmapImage();
        /// <summary>
        /// 使用手册图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage UserGuideImage
        {
            get { return _UserGuideImage; }
            set { _UserGuideImage = value; OnPropertyChanged("UserGuideImage"); }
        }

        private System.Windows.Media.Imaging.BitmapImage _NowShowImage;
        /// <summary>
        /// 当前显示图片
        /// </summary>
        public System.Windows.Media.Imaging.BitmapImage NowShowImage
        {
            get { return _NowShowImage; }
            set { _NowShowImage = value; OnPropertyChanged("NowShowImage"); }
        }

        private SeatManage.EnumType.AdType _NowTap = AdType.None;
        /// <summary>
        /// 当前显示标签
        /// </summary>
        public SeatManage.EnumType.AdType NowTap
        {
            get { return _NowTap; }
            set { _NowTap = value; }
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
        private string _TestMode = "Collapsed";
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; OnPropertyChanged("TestMode"); }
        }
        public event EventHandler ImageChange;
        public event EventHandler ImageSwitch;
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
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeShow = null;
        public SeatManage.SeatManageComm.TimeLoop timeDateTimeSync = null;
        Thread showTimeThread = null;
        Thread syncTimeThread = null;
        public void ShowTimeRun()
        {
            NowDateTime = ServiceDateTime.Now;
            timeDateTimeShow = new SeatManage.SeatManageComm.TimeLoop(1000);
            timeDateTimeShow.TimeTo += new EventHandler(timeDateTimeShow_TimeTo);
            showTimeThread = new Thread(new ThreadStart(timeDateTimeShow.TimeStrat));
            showTimeThread.Start();
            timeDateTimeSync = new SeatManage.SeatManageComm.TimeLoop(300000);
            timeDateTimeSync.TimeTo += new EventHandler(timeDateTimeSync_TimeTo);
            syncTimeThread = new Thread(new ThreadStart(timeDateTimeSync.TimeStrat));
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

        Thread MyLastSeatSum = null;
        public SeatManage.SeatManageComm.TimeLoop MyLastSeatSumTime = null;
        public void LastSeatRun()
        {
            _LastSeatCount = SeatManage.Bll.TerminalOperatorService.LastSeatCount(clientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeat");
            MyLastSeatSumTime = new SeatManage.SeatManageComm.TimeLoop(30 * 1000);
            MyLastSeatSumTime.TimeTo += new EventHandler(MyLastSeatSumTime_TimeTo);
            MyLastSeatSum = new Thread(new ThreadStart(timeDateTimeShow.TimeStrat));
            MyLastSeatSum.Start();
        }

        void MyLastSeatSumTime_TimeTo(object sender, EventArgs e)
        {
            MyLastSeatSumTime.TimeStop();
            _LastSeatCount = SeatManage.Bll.TerminalOperatorService.LastSeatCount(clientObject.ClientSetting.DeviceSetting.Rooms);
            OnPropertyChanged("LastSeat");
            MyLastSeatSumTime.TimeStrat();
        }

        #endregion

        #region 图片切换

        SeatManage.SeatManageComm.TimeLoop ImgTime = null;
        SeatManage.SeatManageComm.TimeLoop ImgTimeStop = null;
        private List<SeatManage.ClassModel.SchoolNoteInfo> SchoolNotices;
        private List<SeatManage.ClassModel.PromotionAdvertInfo> PromotionAd;
        private SeatManage.ClassModel.UserGuideInfo UserGuide;
        int noticeNum;
        int promotionNum;
        int guideNum;
        /// <summary>
        /// 执行图片切换
        /// </summary>
        public void ImageChangeRun()
        {
            SchoolNotices = OperateResult.SystemObject.GetInstance().SchoolNote;
            PromotionAd = OperateResult.SystemObject.GetInstance().PromotionAdvert;
            UserGuide = OperateResult.SystemObject.GetInstance().UserGuide;
            noticeNum = 0;
            promotionNum = 0;
            guideNum = 0;
            if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
            {
                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "UserGuide\\" + UserGuide.ImageFilePath[guideNum], UriKind.RelativeOrAbsolute));
                NowTap = AdType.None;
            }
            if (PromotionAd.Count > 0)
            {
                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "PromotionImage\\" + PromotionAd[promotionNum].AdImagePath, UriKind.RelativeOrAbsolute));
                NowTap = AdType.PromotionAd;
            }
            if (SchoolNotices.Count > 0)
            {
                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                NowTap = AdType.SchoolNotice;
            }
            BtnVisible();
            if (ImageSwitch != null)
            {
                ImageSwitch(this, new EventArgs());
            }


            ImgTime = new SeatManage.SeatManageComm.TimeLoop(10 * 1000);
            ImgTime.TimeTo += new EventHandler(ImgTime_TimeTo);
            ImgTimeStop = new SeatManage.SeatManageComm.TimeLoop(10 * 1000);
            ImgTimeStop.TimeTo += new EventHandler(ImgTimeStop_TimeTo);
            ImgTime.TimeStrat();
        }
        public void ImageChangeStop()
        {
            ImgTime.TimeStop();
            ImgTimeStop.TimeStop();
            ImgTimeStop.TimeStrat();
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
                        {
                            if (PromotionAd.Count > 0)
                            {
                                if (promotionNum >= PromotionAd.Count - 1)
                                {
                                    if (SchoolNotices.Count > 0)
                                    {
                                        noticeNum = -1;
                                        promotionNum = 0;
                                        NowTap = AdType.SchoolNotice;
                                        goto case AdType.SchoolNotice;
                                    }
                                    else
                                    {
                                        promotionNum = -1;
                                    }
                                }
                                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    promotionNum++;
                                    NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "PromotionImage\\" + PromotionAd[promotionNum].AdImagePath, UriKind.RelativeOrAbsolute));
                                    BtnVisible();

                                    if (ImageChange != null && PromotionAd.Count > 1)
                                    {
                                        ImageChange(this, new EventArgs());
                                    }
                                    else if (ImageSwitch != null)
                                    {
                                        ImageSwitch(this, new EventArgs());
                                    }
                                }));

                            }
                            else if (PromotionAd.Count > 0)
                            {
                                NowTap = AdType.SchoolNotice;
                                goto case AdType.PromotionAd;
                            }
                            else if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
                            {
                                NowTap = AdType.None;
                                goto case AdType.None;
                            }
                        }; break;
                    case AdType.SchoolNotice:
                        {
                            if (SchoolNotices.Count > 0)
                            {
                                if (noticeNum >= SchoolNotices.Count - 1)
                                {
                                    if (PromotionAd.Count > 0)
                                    {
                                        promotionNum = -1;
                                        noticeNum = 0;
                                        NowTap = AdType.PromotionAd;
                                        goto case AdType.PromotionAd;
                                    }
                                    else
                                    {
                                        noticeNum = -1;
                                    }
                                }
                                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    noticeNum++;
                                    NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                                    BtnVisible();

                                    if (ImageChange != null && SchoolNotices.Count > 1)
                                    {
                                        ImageChange(this, new EventArgs());
                                    }
                                    else if (ImageSwitch != null)
                                    {
                                        ImageSwitch(this, new EventArgs());
                                    }
                                }));
                            }
                            else if (PromotionAd.Count > 0)
                            {
                                NowTap = AdType.PromotionAd;
                                goto case AdType.PromotionAd;
                            }
                            else if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
                            {
                                NowTap = AdType.None;
                                goto case AdType.None;
                            }
                        }; break;
                    case AdType.None:
                        {
                            if (SchoolNotices.Count > 0)
                            {
                                NowTap = AdType.SchoolNotice;
                                goto case AdType.SchoolNotice;
                            }
                            else if (PromotionAd.Count > 0)
                            {
                                NowTap = AdType.PromotionAd;
                                goto case AdType.PromotionAd;
                            }
                            else if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
                            {
                                if (guideNum >= UserGuide.ImageFilePath.Count - 1)
                                {
                                    guideNum = -1;

                                }
                                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                                {
                                    guideNum++;
                                    NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "UserGuide\\" + UserGuide.ImageFilePath[guideNum], UriKind.RelativeOrAbsolute));
                                    BtnVisible();
                                    if (ImageChange != null && UserGuide.ImageFilePath.Count > 1)
                                    {
                                        ImageChange(this, new EventArgs());
                                    }
                                    else if (ImageSwitch != null)
                                    {
                                        ImageSwitch(this, new EventArgs());
                                    }
                                }));
                            }
                        } break;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("查询进出记录遇到异常" + ex.Message);
            }
            finally
            {
                ImgTime.TimeStrat();
            }
        }
        //图片向左
        public bool ImageLeft()
        {
            try
            {
                switch (NowTap)
                {
                    case AdType.PromotionAd:
                        {
                            if (promotionNum > 0)
                            {
                                promotionNum--;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "PromotionImage\\" + PromotionAd[promotionNum].AdImagePath, UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }
                        }; break;
                    case AdType.SchoolNotice:
                        {

                            if (noticeNum > 0)
                            {
                                noticeNum--;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }

                        }; break;
                    case AdType.None:
                        {

                            if (guideNum > 0)
                            {
                                guideNum--;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "UserGuide\\" + UserGuide.ImageFilePath[guideNum], UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }
                        } break;
                }
                return false;

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("切换图片遇到异常" + ex.Message);
                return false;
            }
        }
        //图片向右
        public bool ImageRight()
        {
            try
            {
                switch (NowTap)
                {
                    case AdType.PromotionAd:
                        {
                            if (promotionNum < PromotionAd.Count - 1)
                            {
                                promotionNum++;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "PromotionImage\\" + PromotionAd[promotionNum].AdImagePath, UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }
                        }; break;
                    case AdType.SchoolNotice:
                        {

                            if (noticeNum < SchoolNotices.Count - 1)
                            {
                                noticeNum++;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }

                        }; break;
                    case AdType.None:
                        {

                            if (guideNum < UserGuide.ImageFilePath.Count - 1)
                            {
                                guideNum++;
                                NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "UserGuide\\" + UserGuide.ImageFilePath[guideNum], UriKind.RelativeOrAbsolute));
                                BtnVisible();
                                return true;
                            }
                        } break;
                }
                return false;
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("切换图片遇到异常" + ex.Message);
                return false;
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
                else
                {
                    NowTap = newType;
                }
                switch (NowTap)
                {
                    case AdType.None:

                        if (UserGuide != null && UserGuide.ImageFilePath.Count > 0)
                        {
                            guideNum = 0;
                            NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "UserGuide\\" + UserGuide.ImageFilePath[guideNum], UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NowShowImage = null;
                        }
                        if (ImageSwitch != null)
                        {
                            ImageSwitch(this, new EventArgs());
                        }
                        break;
                    case AdType.PromotionAd:

                        if (PromotionAd.Count > 0)
                        {
                            promotionNum = 0;
                            NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "PromotionImage\\" + PromotionAd[promotionNum].AdImagePath, UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NowShowImage = null;
                        }
                        if (ImageSwitch != null)
                        {
                            ImageSwitch(this, new EventArgs());
                        }
                        break;
                    case AdType.SchoolNotice:
                        if (SchoolNotices.Count > 0)
                        {
                            noticeNum = 0;
                            NowShowImage = new System.Windows.Media.Imaging.BitmapImage(new Uri(Apppath + "NoteImage\\" + SchoolNotices[noticeNum].NoteImagePath, UriKind.RelativeOrAbsolute));
                        }
                        else
                        {
                            NowShowImage = null;
                        }
                        if (ImageSwitch != null)
                        {
                            ImageSwitch(this, new EventArgs());
                        }
                        break;
                }
                BtnVisible();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("切换图片遇到异常" + ex.Message);
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
                    if (guideNum > 0)
                    {
                        LeftBtn = "Visible";
                    }
                    else
                    {
                        LeftBtn = "Collapsed";
                    }
                    if (UserGuide != null && guideNum < UserGuide.ImageFilePath.Count - 1)
                    {
                        RightBtn = "Visible";
                    }
                    else
                    {
                        RightBtn = "Collapsed";
                    }
                    break;
                case AdType.PromotionAd:
                    if (promotionNum > 0)
                    {
                        LeftBtn = "Visible";
                    }
                    else
                    {
                        LeftBtn = "Collapsed";
                    }
                    if (promotionNum < PromotionAd.Count - 1)
                    {
                        RightBtn = "Visible";
                    }
                    else
                    {
                        RightBtn = "Collapsed";
                    } break;
                case AdType.SchoolNotice:
                    if (noticeNum > 0)
                    {
                        LeftBtn = "Visible";
                    }
                    else
                    {
                        LeftBtn = "Collapsed";
                    }
                    if (noticeNum < SchoolNotices.Count - 1)
                    {
                        RightBtn = "Visible";
                    }
                    else
                    {
                        RightBtn = "Collapsed";
                    } break;
            }
        }

        #endregion

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
                    ReaderNoteWindow noteWindow = new ReaderNoteWindow();
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
                SeatClientV2.PopupWindow popWindow = new PopupWindow(TipType.Exception);
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
                PopupWindow activeWindow = new PopupWindow(SeatManage.EnumType.TipType.ActivationReadCard);
                activeWindow.ShowDialog();
                if (activeWindow.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    clientObject.EnterOutLogData = new OperateResult.OperateResult();
                    clientObject.EnterOutLogData.Student = SeatManage.Bll.EnterOutOperate.GetReaderInfo(activeWindow.viewModel.CardNo);
                    if (clientObject.EnterOutLogData.Student == null)
                    {
                        clientObject.EnterOutLogData.Student = new SeatManage.ClassModel.ReaderInfo() { CardNo = activeWindow.viewModel.CardNo };
                    }
                    SeatManage.ClassModel.UserInfo user = SeatManage.Bll.Users_ALL.GetUserInfo(activeWindow.viewModel.CardNo);
                    if (user != null)
                    {
                        if (user.IsUsing == SeatManage.EnumType.LogStatus.Valid)//判断用户状态是否启用。
                        {
                            PopupWindow bespeakCloseWindow = new PopupWindow(TipType.CancelActivationWarn);//启用中，则提示取消
                            bespeakCloseWindow.ShowDialog();
                            if (bespeakCloseWindow.viewModel.OperateResule == HandleResult.Successed)
                            {
                                user.IsUsing = SeatManage.EnumType.LogStatus.Fail;//
                                user.Remark = "终端刷卡注销";
                                SeatManage.Bll.Users_ALL.UpdateUserOnlyInfo(user);
                                PopupWindow activeCloseWindow = new PopupWindow(TipType.CancelActivationSuccess);//启用中，则提示取消
                                activeCloseWindow.ShowDialog();
                            }
                        }
                        else
                        {//如果读者用户状态是失效，则重新激活。
                            user.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                            user.Password = MD5Algorithm.GetMD5Str32(activeWindow.viewModel.CardNo);
                            user.Remark = "终端刷卡重新激活";
                            if (SeatManage.Bll.Users_ALL.UpdateUserOnlyInfo(user))
                            {
                                PopupWindow activeSuccessWindow = new PopupWindow(TipType.ActivationSuccess);//启用中，则提示取消
                                activeSuccessWindow.ShowDialog();
                            }
                        }
                    }
                    else
                    {
                        SeatManage.ClassModel.UserInfo newUser = new SeatManage.ClassModel.UserInfo();
                        newUser.IsUsing = SeatManage.EnumType.LogStatus.Valid;
                        newUser.LoginId = activeWindow.viewModel.CardNo;
                        newUser.Password = MD5Algorithm.GetMD5Str32(activeWindow.viewModel.CardNo);
                        newUser.UserType = SeatManage.EnumType.UserType.Reader;
                        newUser.UserName = clientObject.EnterOutLogData.Student == null ? "" : clientObject.EnterOutLogData.Student.Name;
                        newUser.Remark = "在终端刷卡激活";
                        if (SeatManage.Bll.Users_ALL.AddNewUser(newUser))
                        {
                            PopupWindow activeSuccessWindow = new PopupWindow(TipType.ActivationSuccess);//启用中，则提示取消
                            activeSuccessWindow.ShowDialog();
                        }
                    }
                    //预约激活处理
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("预约激活遇到异常" + ex.Message);
                PopupWindow errorWindow = new PopupWindow(SeatManage.EnumType.TipType.Exception);
                errorWindow.ShowDialog();
            }
            finally
            {
                clientObject.EnterOutLogData = null;
            }
        }
        #endregion
        #endregion
    }
}
