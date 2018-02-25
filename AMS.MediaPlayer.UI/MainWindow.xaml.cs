using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Timers;
using AMS.MediaPlayer.UI.Code;
using System.ComponentModel;
using System.Threading;
using WpfApplication10;
using AMS.MediaPlayer.Code;
using System.IO;
using System.Windows.Media.Animation;
using System.Windows.Interop;

namespace AMS.MediaPlayer.UI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        GetPlaylist get;

        #region 界面主逻辑
        private string _Warning = "";

        public string Warning
        {
            get { return _Warning; }
            set
            {
                _Warning = value;
                try
                {
                    NotifyPropertyChanged("Warning");
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message);
                }
            }
        }
        private WPFMessage.MessageHelper messageHelper;
        System.Threading.Thread playListThread;
        System.Threading.Thread couponsThread;
        System.Threading.Thread SendStatus;
        GetCoupons coupons;
        //System.Threading.Thread ADThread;
        //AdLogic ad;
        //GetRollTitlse roll;

        /// <summary>
        /// 窗体构造函数
        /// </summary>
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                this.Left = 0;
                this.Top = 0;
                this.Width = 1080;
                this.Height = 960;
                this.Closing += (sender, e) => Window_Closing(sender, e);
                txtWarning.DataContext = this;

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //播放默认的视频文件
                playDefautlVideo();

                //获取新的播放列表
                get = new GetPlaylist();
                get.PlayVideo += new PlayVideoEventHandler(get_PlayVideo);//通知播放时间
                get.PlayListHandleEvent += (s, arges) => get_ShowMessage(s, arges);
                get.PlayListHandleOver += (s, arges) => get_HandleOver(s, arges);
                //ad = new AdLogic();
                //ad.ImageUrlChanage += new EventHandler(ad_ImageUrlChange);
                //ad.SCStart += new EventHandler(ad_SCStart);
                //roll = new GetRollTitlse();
                //roll.RollStart += new EventHandler(roll_RollStart);
                //roll.Get();
                playListThread = new Thread(new ThreadStart(get.Run));
                playListThread.Start();

                coupons = new GetCoupons();
                coupons.LogoChanage += new EventHandler(coupons_LogoChanage);
                coupons.ScrollStart += new EventHandler(coupons_ScrollStart);
                couponsThread = new Thread(new ThreadStart(coupons.GetCustomer));
                couponsThread.Start();
                //if (PlayerSetting.IsOffline == "1")//服务器不脱机
                //{
                //    SendStatus = new Thread(new ThreadStart(SendStatus_GetPlayList));
                //    SendStatus.Start();
                //    //ADThread = new Thread(new ThreadStart(ad.Start));
                //    //ADThread.Start();

                //}
                messageHelper = new WPFMessage.MessageHelper();
                messageHelper.GetMessage += new WPFMessage.MessageHelper.GetMessageEventHandler(messageHelper_GetMessage);
                (PresentationSource.FromVisual(this) as HwndSource).AddHook(new HwndSourceHook(messageHelper.WndProc));
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }
        SeatManage.EnumType.SendClentMessageType nowState = SeatManage.EnumType.SendClentMessageType.Normal;
        /// <summary>
        /// 消息处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void messageHelper_GetMessage(object sender, string message)
        {
            string[] msg = message.Split(';');
            int type = 0;
            if (int.TryParse(msg[0], out type))
            {
                nowState = (SeatManage.EnumType.SendClentMessageType)type;
                switch (nowState)
                {
                    case SeatManage.EnumType.SendClentMessageType.MoveDown:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                this.Top += size;
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.MoveUp:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                this.Top -= size;
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Normal:
                        {
                            this.Top = 0;
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.OnLock:
                        {
                            this.Top = 0;
                            if (showWindow != null)
                            {
                                showWindow.Close();
                                showWindow = null;
                            }
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Close:
                        {
                            Application.Current.Shutdown();
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.ScreenShots:
                        {
                            try
                            {
                                Code.ScreenShots.SaveWindowContent(this, msg[1]);
                            }
                            catch (Exception ex)
                            {
                                
                            }
                        }
                        break;
                }
            }
        }
        /// <summary>
        /// 开始滚动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void coupons_ScrollStart(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (coupons.CouponsList[1][0].Num != "" || coupons.CouponsList[2][0].Num != "" || coupons.CouponsList[3][0].Num != "" || coupons.CouponsList[4][0].Num != "" || coupons.CouponsList[5][0].Num != "" || coupons.CouponsList[6][0].Num != "" || coupons.CouponsList[7][0].Num != "")
                    {
                        InitImgpanel();
                        coupons.imageUrlChange();
                        imgPanel1.Visibility = Visibility.Visible;

                        mediaPlayHost.Height = 763;
                        myMediaPlayer.Height = 763;
                        imgCanv.Height = 763;
                        Canvas.SetTop(imgPanel1, 763);

                        if (coupons.CouponsList[8][0].Num != "")
                        {
                            imgPanel2.Visibility = Visibility.Visible;

                            mediaPlayHost.Height = 607;
                            myMediaPlayer.Height = 607;
                            imgCanv.Height = 607;
                            Canvas.SetTop(imgPanel1, 607);
                        }
                    }
                    else if (coupons.CouponsList[8][0].Num != "")
                    {
                        InitImgpanel();
                        coupons.imageUrlChange();
                        imgPanel2.Visibility = Visibility.Visible;

                        mediaPlayHost.Height = 763;
                        myMediaPlayer.Height = 763;
                        imgCanv.Height = 763;
                    }
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
            }));
        }
        /// <summary>
        /// logo变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void coupons_LogoChanage(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    if (coupons.Index < 8 && coupons.CouponsList[coupons.Index][0].Num != "")
                    {
                        (imgPanel1.Children[coupons.Index - 1] as PhotoFrame).ImageUrl = string.Format("{0}{1}", PlayerSetting.SysPath + "\\CouponsImage\\", coupons.CouponsList[coupons.Index][0].LogoImage);
                        (imgPanel1.Children[coupons.Index - 1] as PhotoFrame).SlipInfo = coupons.CouponsList[coupons.Index][0];
                    }
                    else if (coupons.Index == 8 && coupons.CouponsList[coupons.Index][0].Num != "")
                    {
                        (imgPanel2.Children[0] as PhotoFrame_Eight).ImageUrl = string.Format("{0}{1}", PlayerSetting.SysPath + "\\CouponsImage\\", coupons.CouponsList[coupons.Index][0].LogoImage);
                        (imgPanel2.Children[0] as PhotoFrame_Eight).SlipInfo = coupons.CouponsList[coupons.Index][0];
                    }
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
            }));
        }

        //void roll_RollStart(object sender, EventArgs e)
        //{
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        try
        //        {
        //            if (!string.IsNullOrEmpty(roll.RollText.Trim()))
        //            {
        //                textBlock1.Text = roll.RollText;
        //                CeaterAnimation(textBlock1);
        //                canva1.Visibility = Visibility.Visible;
        //                if (imgPanel2.Visibility == Visibility.Visible)
        //                {
        //                    mediaPlayHost.Height = 607;
        //                    myMediaPlayer.Height = 607;
        //                    imgCanv.Height = 607;
        //                }
        //                else
        //                {
        //                    Canvas.SetTop(canva1, 763);
        //                    mediaPlayHost.Height = 763;
        //                    myMediaPlayer.Height = 763;
        //                    imgCanv.Height = 763;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
        //        }
        //    }));
        //}



        /// <summary>
        /// 注册播放器状态改变事件
        /// </summary>
        private void MediaPlayEventReg()
        {
            myMediaPlayer.Player.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(myMediaPlayer_Player_PlayStateChange);
        }

        private void myMediaPlayer_Player_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == (int)WMPLib.WMPPlayState.wmppsMediaEnded)
            {
                playDefautlVideo();
            }
        }


        //void ad_SCStart(object sender, EventArgs e)
        //{
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        try
        //        {
        //            if (ad.SCModel.Count > 0)
        //            {
        //                InitImgpanel();
        //                //ad.imageUrlChange();//广告每隔4秒进行一次切换
        //                //imgPanel1.Visibility = Visibility.Visible;
        //                imgPanel2.Visibility = Visibility.Visible;

        //                if (canva1.Visibility == Visibility.Visible)
        //                {
        //                    mediaPlayHost.Height = 607;
        //                    myMediaPlayer.Height = 607;
        //                    imgCanv.Height = 607;
        //                    Canvas.SetTop(canva1, 607);
        //                }
        //                else
        //                {
        //                    mediaPlayHost.Height = 763;
        //                    myMediaPlayer.Height = 763;
        //                    imgCanv.Height = 763;
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
        //        }
        //    }));
        //}

        /// <summary>
        /// 上下滑动窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void InitImgpanel()
        {
            try
            {
                for (int i = 0; i < 7; i++)
                {
                    PhotoFrame pf = new PhotoFrame(coupons.CouponsList[i + 1][0]);
                    if (coupons.CouponsList[i + 1][0].Num != "")
                    {
                        pf.MouseLeftButtonUp += new MouseButtonEventHandler(imageMouseLeftButtonUp);
                    }
                    pf.Name = "imagePF" + i;
                    imgPanel1.Children.Add(pf);
                }

                PhotoFrame_Eight pf8 = new PhotoFrame_Eight(coupons.CouponsList[8][0]);
                pf8.Name = "imagePF8";
                imgPanel2.Children.Add(pf8);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// 上下滑动
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void ad_ImageUrlChange(object sender, EventArgs e)
        //{
        //    this.Dispatcher.Invoke(new Action(() =>
        //    {
        //        try
        //        {
        //            if (image_no < 7 && coupons.CouponsList[image_no + 1][0].Num != "")
        //            {
        //                //(imgPanel1.Children[image_no] as PhotoFrame).ImageUrl = string.Format("{0}{1}", PlayerSetting.SysPath + "\\CouponsImage\\", coupons.CouponsList[image_no + 1][0].LogoImage);
        //                (imgPanel1.Children[image_no] as PhotoFrame).SlipInfo = coupons.CouponsList[image_no + 1][0];
        //            }
        //            else if (image_no == 8 && coupons.CouponsList[image_no + 1][0].Num != "")
        //            {
        //                //(imgPanel2.Children[image_no] as PhotoFrame_Eight).ImageUrl = string.Format("{0}{1}", PlayerSetting.SysPath + "\\CouponsImage\\", coupons.CouponsList[image_no + 1][0].LogoImage);
        //                (imgPanel2.Children[image_no] as PhotoFrame_Eight).SlipInfo = coupons.CouponsList[image_no + 1][0];
        //            }
        //            image_no++;
        //            if (image_no > 7)
        //            {
        //                image_no = 0;
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
        //        }
        //    }));
        //}

        /// <summary>
        /// 联机模式下获取截图和状态以及更新播放列表
        /// </summary>
        private void SendStatus_GetPlayList()
        {
            //发送设备状态
            SendTouchScreenStatus stss = new SendTouchScreenStatus();
            stss.Run();
            ////监控播放列表更新
            UpDatePlayList udpl = new UpDatePlayList();
            udpl.Error += new MessageEventHandler(udpl_Error);
            udpl.UpdatePlaylist += new MessageEventHandler(udpl_UpdatePlaylist);
            udpl.Run();
        }

        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void udpl_UpdatePlaylist(object sender, string e)
        {
            UpdatePlayList();
        }

        private void get_HandleOver(object s, EventArgs arges)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                Warning = "";
            }));
        }

        private void get_ShowMessage(object s, string arges)
        {
            /*************跨线程访问窗体控件，方法一************/
            //MethodInvoker myInvoker = new MethodInvoker(() =>
            //{
            //    try
            //    {
            //        lblMessage.Text = arges.PlayVideoPath;
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //});
            //lblMessage.Invoke(myInvoker);

            /*************跨线程访问窗体控件，方法二************/
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    Warning = arges;
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
            }));
        }
        private delegate void MessageHandle();

        /// <summary>
        /// 更新播放列表
        /// </summary>
        /// <param name="listNo"></param>
        private void UpdatePlayList()
        {
            try
            {
                playListThread.Abort();
                playListThread = new Thread(new ThreadStart(get.Run));
                playListThread.Start();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }
        //错误处理
        void udpl_Error(object sender, string e)
        {
            SeatManage.SeatManageComm.WriteLog.Write(e);
        }
        /// <summary>
        /// 播放默认的视频文件
        /// </summary>
        private void playDefautlVideo()
        {
            try
            {
                mediaPlay(string.Format("{0}{1}", PlayerSetting.DefaultVideosPath, PlayerSetting.DefaultVideo.Split(';')[0]));
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(string.Format("加载默认播放列表失败：", ex.Message.TrimEnd('\0')));
            }
        }

        void get_PlayVideo(object sender, string e)
        {
            try
            {
                this.Dispatcher.Invoke(new Action(() => { mediaPlay(e); }));
                // this.txtWarning.Dispatcher.Invoke(new Action(() => { this.txtWarning.Visibility = System.Windows.Visibility.Collapsed; }));
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        private void mediaPlay(string e)
        {

            GC.Collect();
            myMediaPlayer.Player.Ctlcontrols.stop();
            myMediaPlayer.Player.URL = "";
            try
            {
                string fileType = e.Substring(e.LastIndexOf(".") + 1);
                string filename = e.Substring(e.LastIndexOf("\\") + 1);
                switch (fileType)
                {
                    case "JPG":
                    case "PNG":
                    case "BMP":
                    case "GIF":
                    case "jpg":
                    case "png":
                    case "bmp":
                    case "gif":
                        if (File.Exists(e))
                        {
                            myMediaPlayer.Player.Ctlcontrols.stop();
                            mediaPlayHost.Visibility = System.Windows.Visibility.Collapsed;
                            imgCanv.Visibility = System.Windows.Visibility.Visible;
                            BitmapImage img = GetVideoFile.InitImage(e);
                            if (img != null)
                            {
                                imgCanv.Source = img;
                                img.Freeze();
                                txtWarning.Visibility = System.Windows.Visibility.Collapsed;
                                //Warning = string.Format("当前播放：{0}", filename);
                            }
                            else
                            {
                                txtWarning.Visibility = System.Windows.Visibility.Visible;
                                Warning = "默认播放的文件不存在！";
                            }

                        }
                        else
                        {
                            Warning = string.Format("文件{0}不存在", filename);
                        }

                        break;
                    default:
                        if (File.Exists(e))
                        {
                            imgCanv.Visibility = System.Windows.Visibility.Collapsed;
                            mediaPlayHost.Visibility = System.Windows.Visibility.Visible;
                            myMediaPlayer.Player.URL = e;
                            myMediaPlayer.Player.Ctlcontrols.play();
                            txtWarning.Visibility = System.Windows.Visibility.Collapsed;
                            //Warning = string.Format("当前播放：{0}", filename);
                        }
                        else
                        {
                            txtWarning.Visibility = System.Windows.Visibility.Visible;
                            Warning = string.Format("文件{0}不存在", filename);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }
        delegate void PlayVideo(string path);

        /// <summary>
        /// 文件载入失败，播放默认的播放列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mediaElement1_MediaFailed(object sender, System.Windows.ExceptionRoutedEventArgs e)
        {
            try
            {
                playDefautlVideo();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SeatManage.SeatManageComm.WriteLog.Write("程序关闭!");

            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// 属性改变
        /// </summary>
        /// <param name="propertyName"></param>
        private void NotifyPropertyChanged(string propertyName)
        {
            try
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }

        #endregion
        #endregion


        #region 上下滚动单击打印
        private void imageMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Click(sender);
        }
        CouponsShowWindow showWindow;
        private void Click(object sender)
        {
            PhotoFrame p = sender as PhotoFrame;
            if (showWindow != null && nowState != SeatManage.EnumType.SendClentMessageType.Normal)
            {
                showWindow.Close();
                showWindow = null;
            }
            showWindow = new CouponsShowWindow();
            showWindow.viewModel.CouponsModel = p.SlipInfo;
            showWindow.DataBinding();
            showWindow.Width = 1080;
            showWindow.Height = 920;
            showWindow.Top = 0;
            showWindow.Left = 0;
            showWindow.Show();
            //showWindow = null;
        }
        #endregion


        #region 滚动文字方法
        private double MeasureTextWidth(string text, double fontSize, string fontFamily)
        {
            FormattedText formattedText = new FormattedText(
            text,
            System.Globalization.CultureInfo.InvariantCulture,
            FlowDirection.LeftToRight,
            new Typeface(fontFamily.ToString()),
            fontSize,
            Brushes.Black
            );
            return formattedText.WidthIncludingTrailingWhitespace;
        }

        private void CeaterAnimation(TextBlock text)
        {
            //创建动画资源
            Storyboard storyboard = new Storyboard();

            double lenth = MeasureTextWidth(text.Text, text.FontSize, text.FontFamily.Source);

            //移动动画
            {
                DoubleAnimationUsingKeyFrames WidthMove = new DoubleAnimationUsingKeyFrames();
                Storyboard.SetTarget(WidthMove, text);
                DependencyProperty[] propertyChain = new DependencyProperty[]
                {
                    TextBlock.RenderTransformProperty,
                    TransformGroup.ChildrenProperty,
                    TranslateTransform.XProperty,
                };

                Storyboard.SetTargetProperty(WidthMove, new PropertyPath("(0).(1)[3].(2)", propertyChain));
                WidthMove.KeyFrames.Add(new EasingDoubleKeyFrame(canva1.Width, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0))));
                WidthMove.KeyFrames.Add(new EasingDoubleKeyFrame(-lenth, KeyTime.FromTimeSpan(new TimeSpan(0, 0, 0, 10))));
                storyboard.Children.Add(WidthMove);
            }

            storyboard.RepeatBehavior = RepeatBehavior.Forever;
            storyboard.Begin();
        }
        #endregion
    }
}
