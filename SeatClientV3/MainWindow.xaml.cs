using SeatClientV3.OperateResult;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SeatClientV3.WindowObject;
using SeatManage.SeatManageComm;

namespace SeatClientV3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel;
            viewModel.ClientObject.UpdateConfigError += clientObject_UpdateConfigError;
            viewModel.ImageChange += viewModel_ImageChange;
            MessageHelper = new WPFMessage.MessageHelper();
            MessageHelper.GetMessage += messageHelper_GetMessage;
            //viewModel.CodeChange += viewModel_CodeChange;
            MoveCanvasBinding();
        }


        ViewModel.MainWindow_ViewModel viewModel = new ViewModel.MainWindow_ViewModel();
        protected WPFMessage.MessageHelper MessageHelper;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartRead();
            StopRead();
            StartRead();
            WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.Normal);
            viewModel.ShowTimeRun();
            viewModel.LastSeatRun();
            viewModel.ImageChangeRun();
            //viewModel.CheckCodeRun();
            (PresentationSource.FromVisual(this) as HwndSource).AddHook(MessageHelper.WndProc);
            if (viewModel.ClientObject.UseCodeCheck)
            {
                WeiCharOperationWindowObject.GetInstance().Window.Show();
                WeiCharOperationWindowObject.GetInstance().Window.Topmost = true;
            }
        }

        public void ShowMessage()
        {
            StopRead();
            StartRead();
            WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.Normal);
            viewModel.ShowTimeRun();
            viewModel.LastSeatRun();
            viewModel.ImageChangeRun();
            //viewModel.CheckCodeRun();
            Show();
            WeiCharOperationWindowObject.GetInstance().Window.WinReset();
        }
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        ///// <summary>
        ///// 二维码显示
        ///// </summary>
        //void viewModel_CodeChange()
        //{
        //    Dispatcher.Invoke(new Action(() =>
        //    {
        //        string url = viewModel.ClientObject.CodeUrl + "?param=";
        //        string AESCode = string.Format("clientNo={0}&codeTime={1}", viewModel.ClientObject.ClientSetting.ClientNo, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
        //        Bitmap bitmap = QRCode.GetDimensionalCode(url + AESAlgorithm.AESEncrypt(AESCode), 6, 8);
        //        IntPtr hBitmap = bitmap.GetHbitmap();
        //        BitmapSource bitmapImage = new BitmapImage();

        //        try
        //        {
        //            bitmapImage = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        //        }
        //        finally
        //        {
        //            DeleteObject(hBitmap);
        //        }
        //        //imgCode.Fill = new ImageBrush(bitmapImage);
        //    }));
        //}
        /// <summary>
        /// 位置移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        void messageHelper_GetMessage(object sender, string message)
        {
            string[] msg = message.Split(';');
            int type = 0;
            if (int.TryParse(msg[0], out type))
            {
                switch ((SeatManage.EnumType.SendClentMessageType)type)
                {
                    case SeatManage.EnumType.SendClentMessageType.MoveDown:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                Top += size;
                            }
                            WeiCharOperationWindowObject.GetInstance().Window.WinChange((int)Top);
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.MoveUp:
                        {
                            int size = 0;
                            if (msg.Length > 1 && int.TryParse(msg[1], out size))
                            {
                                Top -= size;
                            }
                            WeiCharOperationWindowObject.GetInstance().Window.WinChange((int)Top);
                        }
                        break;
                    case SeatManage.EnumType.SendClentMessageType.Close:
                        {
                            this.Close();
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
        /// 连接丢失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientObject_UpdateConfigError(object sender, EventArgs e)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.LauncherClient, SeatManage.EnumType.SendClentMessageType.ReStartUpSeatClient);
                //WinClosing();
                //AppLoadingWindowObject.GetInstance().Window.CheckConfigConnection(true);
                //if (AppLoadingWindowObject.GetInstance().Window.viewModel.InitializeState == SeatManage.EnumType.HandleResult.Successed)
                //{
                //    ShowMessage();
                //    viewModel.ClientObject.StartAutoUpdateConfig();
                //}
                //else
                //{
                //    WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.Close);
                //    this.Close();
                //}

            }));
        }
        /// <summary>
        /// 图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_ImageChange(int x, int y)
        {
            Dispatcher.Invoke(new Action(() =>
            {
                switch (viewModel.NowTap)
                {
                    case SeatManage.EnumType.AdType.None: btn_Guide.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.PromotionAd: btn_Promotion.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.SchoolNotice: btn_Note.IsChecked = true; break;
                }
                CanvasMove(x, y);
            }));
        }
        /// <summary>
        /// 读卡成功操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            StopRead();
            //操作
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                Dispatcher.Invoke(new Action(() =>
               {
                   WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.OnLock);
                   if (Height == 1000)
                   {
                       Top = 920;
                   }
                   //if (e.CardNo == "juneberryclose")
                   //{
                   //    WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.LauncherClient, SeatManage.EnumType.SendClentMessageType.Close);
                   //    this.Close();
                   //}
                   viewModel.PosCardHandle(e.CardNo);
                   WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.Normal);
               }));
            }
            StartRead();
        }
        /// <summary>
        /// 点击输入座位号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetNo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_CardNo.Text))
            {
                WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.OnLock);
                //WeiCharOperationWindowObject.GetInstance().Window.WinChange();
                if (Height == 1000)
                {
                    Top = 920;
                }
                viewModel.PosCardHandle(txt_CardNo.Text);
                WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.MediaClient, SeatManage.EnumType.SendClentMessageType.Normal);
                WeiCharOperationWindowObject.GetInstance().Window.WinReset();
            }
        }
        /// <summary>
        /// 广告切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.ImageChangePause();
        }
        /// <summary>
        /// 广告左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnlLeft_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImageLeft();
        }
        /// <summary>
        /// 广告右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImageRight();
        }
        /// <summary>
        /// 记录查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //停止读卡注销事件
            StopRead();
            RecordTheQueryWindowObject.GetInstance().Window.ShowMessage();
            //开始读卡注册事件
            StartRead();
        }
        /// <summary>
        /// 预约激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void activebtn_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //停止读卡注销事件
            StopRead();
            viewModel.ActiveBook();
            //开始读卡注册事件
            StartRead();
        }
        private void StopRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.Stop();
                viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
                viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
            //WeiCharOperationWindowObject.GetInstance().Window.WinChange();
        }
        private void StartRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
                viewModel.ClientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
                viewModel.ClientObject.ObjCardReader.Start();
            }
            //WeiCharOperationWindowObject.GetInstance().Window.WinReset();
        }
        /// <summary>
        /// 绑定移动广告位
        /// </summary>
        private void MoveCanvasBinding()
        {
            for (int i = 0; i < viewModel.UserGuideImage.Count; i++)
            {
                Path newImage = new Path();
                newImage.Fill = new ImageBrush(viewModel.UserGuideImage[i]);
                newImage.Height = 700;
                newImage.Width = 840;
                newImage.Style = (Style)FindResource("PathOut");
                Canvas.SetLeft(newImage, i * 900);
                Canvas.SetTop(newImage, 0);
                image_Move_Canvas.Children.Add(newImage);
            }
            for (int i = 0; i < viewModel.SchoolNoteImage.Count; i++)
            {
                Path newImage = new Path();
                newImage.Fill = new ImageBrush(viewModel.SchoolNoteImage[i]);
                newImage.Height = 700;
                newImage.Width = 840;
                newImage.Style = (Style)FindResource("PathOut");
                Canvas.SetLeft(newImage, i * 900);
                Canvas.SetTop(newImage, 800);
                image_Move_Canvas.Children.Add(newImage);
            }
            for (int i = 0; i < viewModel.PromotionImage.Count; i++)
            {
                Path newImage = new Path();
                newImage.Fill = new ImageBrush(viewModel.PromotionImage[i]);
                newImage.Height = 700;
                newImage.Width = 840;
                newImage.Style = (Style)FindResource("PathOut");
                Canvas.SetLeft(newImage, i * 900);
                Canvas.SetTop(newImage, 1600);
                image_Move_Canvas.Children.Add(newImage);
            }
            image_Move_Canvas.Width = viewModel.UserGuideImage.Count * 900 > image_Move_Canvas.Width ? viewModel.UserGuideImage.Count * 900 : image_Move_Canvas.Width;
            image_Move_Canvas.Width = viewModel.SchoolNoteImage.Count * 900 > image_Move_Canvas.Width ? viewModel.SchoolNoteImage.Count * 900 : image_Move_Canvas.Width;
            image_Move_Canvas.Width = viewModel.PromotionImage.Count * 900 > image_Move_Canvas.Width ? viewModel.PromotionImage.Count * 900 : image_Move_Canvas.Width;
        }
        protected Storyboard Storyboard;
        protected DoubleAnimation doubleAnimationX;
        protected DoubleAnimation doubleAnimationY;
        private void CanvasMove(int x, int y)
        {
            if (Storyboard == null)
            {
                Storyboard = new Storyboard();
                doubleAnimationX = new DoubleAnimation(x * -900, new Duration(TimeSpan.FromSeconds(0.5)));
                Storyboard.SetTarget(doubleAnimationX, image_Move_Canvas);
                Storyboard.SetTargetProperty(doubleAnimationX, new PropertyPath("(Canvas.Left)"));
                Storyboard.Children.Add(doubleAnimationX);

                doubleAnimationY = new DoubleAnimation(y * -800, new Duration(TimeSpan.FromSeconds(0.5)));
                Storyboard.SetTarget(doubleAnimationY, image_Move_Canvas);
                Storyboard.SetTargetProperty(doubleAnimationY, new PropertyPath("(Canvas.Top)"));
                Storyboard.Children.Add(doubleAnimationY);

                if (!Resources.Contains("rectAnimation"))
                {
                    Resources.Add("rectAnimation", Storyboard);
                }
            }
            else
            {
                doubleAnimationX.To = x * -900;
                doubleAnimationY.To = y * -800;
            }

            //动画播放
            Storyboard.Begin();

        }

        /// <summary>
        /// 读卡器激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReCardRead_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.Reset();
            }
        }
        /// <summary>
        /// 使用手册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Guide_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImageUpDown(SeatManage.EnumType.AdType.None);
        }
        /// <summary>
        /// 学校通知
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Note_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImageUpDown(SeatManage.EnumType.AdType.SchoolNotice);
        }
        /// <summary>
        /// 推广广告
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Promotion_Click(object sender, RoutedEventArgs e)
        {
            viewModel.ImageUpDown(SeatManage.EnumType.AdType.PromotionAd);
        }
        /// <summary>
        /// 隐藏关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_blankClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StopRead();
            if (e.ClickCount == 2)
            {
                PasswordCloseWindow pcw = new PasswordCloseWindow();
                pcw.ShowDialog();
                if (pcw.viewModel.OperateResule == SeatManage.EnumType.HandleResult.Successed)
                {
                    WPFMessage.MessageHelper.SendMessage(viewModel.ClientObject.LauncherClient, SeatManage.EnumType.SendClentMessageType.Close);
                    this.Close();
                }
            }
            StartRead();
        }
        /// <summary>
        /// 窗体隐藏
        /// </summary>
        private void WinClosing()
        {
            Hide();
            viewModel.ImageChangeStop();
            viewModel.ClientObject.StopUpdateConfig();
            viewModel.timeDateTimeSync.TimeStop();
            viewModel.MyLastSeatSumTime.TimeStop();
            //viewModel.MyCheckCodeTime.TimeStop();
            StopRead();
            //GC.Collect();
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.ImageChangeStop();
            viewModel.ClientObject.StopUpdateConfig();
            if (viewModel.timeDateTimeSync != null)
            {
                viewModel.timeDateTimeSync.TimeStop();
            }
            viewModel.MyLastSeatSumTime.TimeStop();
            if (viewModel.MyCheckCodeTime != null)
            {
                viewModel.MyCheckCodeTime.TimeStop();
            }
            StopRead();
            Application.Current.Shutdown();
        }
        /// <summary>
        /// 微信绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void weicharbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //停止读卡注销事件
            StopRead();
            BindWeiCharWindow bindWeiChar = new BindWeiCharWindow();
            bindWeiChar.ShowDialog();
            StartRead();
        }

    }
}
