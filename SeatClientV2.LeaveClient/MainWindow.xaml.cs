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
using System.Windows.Shapes;
using ClientLeaveV2.Code;
using System.Windows.Media.Animation;
using ClientLeaveV2.OperateResult;
using System.Configuration;

namespace ClientLeaveV2
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;

        }
        ViewModel.MainWindow_ViewModel viewModel = new ViewModel.MainWindow_ViewModel();
        OperateResult.SystemObject clientObject;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeFormState();
            clientObject = OperateResult.SystemObject.GetInstance();
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                clientObject.ObjCardReader.Start();
            }
            clientObject.UpdateConfigError += new EventHandler(clientObject_UpdateConfigError);
            ReadCardOperator posCardHandle = ReadCardOperator.GetInstance();
            posCardHandle.popMessage += new ReadCardOperator.PopMessageEventHandler(posCardHandle_popMessage);
            viewModel.ImageChange += new EventHandler(viewModel_ImageChange);
            viewModel.ImageSwitch += new EventHandler(viewModel_ImageSwitch);
            viewModel.ImageChangeRun();
            viewModel.ShowTimeRun();
            viewModel.LastSeatRun();
        }

        void posCardHandle_popMessage(object sender, PopMessage e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (this.WindowState == System.Windows.WindowState.Minimized)
                {
                    if (e.PopType == SeatManage.EnumType.TipType.Exception)
                    {
                        notifyIcon.ShowBalloonTip(5000, "离开终端", e.Message, System.Windows.Forms.ToolTipIcon.Error);
                    }
                    else
                    {
                        notifyIcon.ShowBalloonTip(5000, "离开终端", e.Message, System.Windows.Forms.ToolTipIcon.Info);
                    }

                }
                else
                {
                    PopupWindow popWindow = new PopupWindow(e.PopType);
                    popWindow.Show();
                }

            }));
        }

        /// <summary>
        /// 连接丢失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void clientObject_UpdateConfigError(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                SystemObject obj = OperateResult.SystemObject.GetInstance();
                obj.StopUpdateConfig();
                viewModel.timeDateTimeSync.TimeStop();
                viewModel.MyLastSeatSumTime.TimeStop();
                if (obj.ObjCardReader != null)
                {
                    obj.ObjCardReader.Stop();
                }
                if (this.WindowState == System.Windows.WindowState.Maximized)
                {
                    AppLoadingWindow AppLoading = new AppLoadingWindow();
                    AppLoading.ShowDialog();
                    this.Hide();
                    if (AppLoading.viewModel.InitializeState == SeatManage.EnumType.HandleResult.Successed)
                    {
                        if (obj.ObjCardReader != null)
                        {
                            obj.ObjCardReader.Start();
                        }
                        viewModel.timeDateTimeSync.TimeStrat();
                        viewModel.MyLastSeatSumTime.TimeStrat();
                        obj.StartAutoUpdateConfig();
                        this.Show();
                    }
                    else
                    {
                        Application.Current.Shutdown();
                    }
                }
                else
                {
                    notifyIcon.ShowBalloonTip(5000, "离开终端", "连接丢失，正常重新连接...", System.Windows.Forms.ToolTipIcon.Error);
                    appint = new ViewModel.AppLoadingWindow_ViewModel();
                    appint.InitializeEnd += new EventHandler(appint_InitializeEnd);
                    appint.Run();
                }



            }));
        }
        ViewModel.AppLoadingWindow_ViewModel appint;
        void appint_InitializeEnd(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                SystemObject obj = OperateResult.SystemObject.GetInstance();
                appint.InitializeEnd -= new EventHandler(appint_InitializeEnd);
                notifyIcon.ShowBalloonTip(5000, "离开终端", "连接恢复", System.Windows.Forms.ToolTipIcon.Info);
                if (obj.ObjCardReader != null)
                {
                    obj.ObjCardReader.Start();
                }
                viewModel.timeDateTimeSync.TimeStrat();
                viewModel.MyLastSeatSumTime.TimeStrat();
                obj.StartAutoUpdateConfig();
            }));
        }
        /// <summary>
        /// 图片切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_ImageChange(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                switch (viewModel.NowTap)
                {
                    case SeatManage.EnumType.AdType.None: btn_Guide.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.PromotionAd: btn_Promotion.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.SchoolNotice: btn_Note.IsChecked = true; break;
                }
                MoveLeft();
            }));
        }
        /// <summary>
        /// 图片改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void viewModel_ImageSwitch(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                if (image_Canvas.Children.Count > 1)
                {
                    image_Canvas.Children.RemoveAt(0);
                }
                switch (viewModel.NowTap)
                {
                    case SeatManage.EnumType.AdType.None: btn_Guide.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.PromotionAd: btn_Promotion.IsChecked = true; break;
                    case SeatManage.EnumType.AdType.SchoolNotice: btn_Note.IsChecked = true; break;
                }
                Path image = image_Canvas.Children[0] as Path;
                image.Fill = new ImageBrush(viewModel.NowShowImage);
            }));
        }
        /// <summary>
        /// 读卡成功操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {

            clientObject.ObjCardReader.Stop();
            //操作
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                this.Dispatcher.Invoke(new Action(() =>
               {
                   viewModel.PosCardHandle(e.CardNo);
               }));
            }
            clientObject.ObjCardReader.Start();
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
                viewModel.PosCardHandle(txt_CardNo.Text);
            }
        }
        /// <summary>
        /// 广告切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tab_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            viewModel.ImageChangeStop();
        }
        /// <summary>
        /// 广告左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnlLeft_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ImageLeft())
            {
                MoveRight();
            }
        }
        /// <summary>
        /// 广告右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ImageRight())
            {
                MoveLeft();
            }
        }

        private void StartRead()
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.Stop();
                clientObject.ObjCardReader.CardNoGeted -= new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
            }
        }
        private void StopRead()
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                clientObject.ObjCardReader.Start();
            }
        }
        Storyboard storyboard;
        private void MoveRight()
        {
            if (image_Canvas.Children.Count > 1)
            {
                image_Canvas.Children.RemoveAt(0);
            }
            storyboard = new Storyboard();
            Path oldImage = image_Canvas.Children[0] as Path;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, 900, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 700;
            newImage.Width = 840;
            newImage.Style = (Style)this.FindResource("PathOut");
            Canvas.SetLeft(newImage, -900);
            Canvas.SetTop(newImage, 0);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(-900, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }

            //动画播放
            storyboard.Begin();
        }

        private void MoveLeft()
        {
            if (image_Canvas.Children.Count > 1)
            {
                image_Canvas.Children.RemoveAt(0);
            }
            storyboard = new Storyboard();
            Path oldImage = image_Canvas.Children[0] as Path;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, -900, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 700;
            newImage.Width = 840;
            newImage.Style = (Style)this.FindResource("PathOut");
            Canvas.SetLeft(newImage, 900);
            Canvas.SetTop(newImage, 0);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(900, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }

            //动画播放
            storyboard.Begin();
        }

        private void MoveUp()
        {
            if (image_Canvas.Children.Count > 1)
            {
                image_Canvas.Children.RemoveAt(0);
            }
            storyboard = new Storyboard();
            Path oldImage = image_Canvas.Children[0] as Path;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, 750, new Duration(TimeSpan.FromSeconds(0.3))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 700;
            newImage.Width = 840;
            newImage.Style = (Style)this.FindResource("PathOut");
            Canvas.SetLeft(newImage, 0);
            Canvas.SetTop(newImage, -750);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(-750, 0, new Duration(TimeSpan.FromSeconds(0.3))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }

            //动画播放
            storyboard.Begin();
        }

        private void MoveDown()
        {
            if (image_Canvas.Children.Count > 1)
            {
                image_Canvas.Children.RemoveAt(0);
            }
            storyboard = new Storyboard();
            Path oldImage = image_Canvas.Children[0] as Path;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, -750, new Duration(TimeSpan.FromSeconds(0.3))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 700;
            newImage.Width = 840;
            newImage.Style = (Style)this.FindResource("PathOut");
            Canvas.SetLeft(newImage, 0);
            Canvas.SetTop(newImage, 750);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(750, 0, new Duration(TimeSpan.FromSeconds(0.3))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Top)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }

            //动画播放
            storyboard.Begin();
        }

        /// <summary>
        /// 读卡器激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_ReCardRead_Click(object sender, RoutedEventArgs e)
        {
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.Reset();
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



        System.Windows.Forms.NotifyIcon notifyIcon = new System.Windows.Forms.NotifyIcon();
        System.Windows.Forms.MenuItem menuMax = new System.Windows.Forms.MenuItem("最大化");
        System.Windows.Forms.MenuItem menuMin = new System.Windows.Forms.MenuItem("最小化");
        System.Windows.Forms.MenuItem menuExit = new System.Windows.Forms.MenuItem("退出");
        private void InitializeFormState()
        {

            notifyIcon.Text = "座位管理系统程序";
            notifyIcon.Icon = SeatClientV2.LeaveClient.Properties.Resources.Logo;
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu();
            notifyIcon.ContextMenu.MenuItems.Add(menuMax);
            notifyIcon.ContextMenu.MenuItems.Add(menuMin);
            notifyIcon.ContextMenu.MenuItems.Add(menuExit);
            menuMax.Click += new EventHandler(menuMax_Click);
            menuMin.Click += new EventHandler(menuMin_Click);
            menuExit.Click += new EventHandler(menuExit_Click);
            notifyIcon.DoubleClick += new EventHandler(notifyIcon_DoubleClick);
            notifyIcon.Visible = true;
            notifyIcon.ShowBalloonTip(5000, "离开终端", "程序已启动", System.Windows.Forms.ToolTipIcon.Info);

            string windowModel = ConfigurationManager.AppSettings["windowState"];
            if (string.IsNullOrEmpty(windowModel) || windowModel == "0")
            {
                this.Hide();
                this.WindowState = System.Windows.WindowState.Minimized;
            }
        }

        void menuExit_Click(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        void menuMin_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.WindowState = System.Windows.WindowState.Minimized;
        }

        void menuMax_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = System.Windows.WindowState.Maximized;
        }

        void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Maximized)
            {
                this.Hide();
                this.WindowState = System.Windows.WindowState.Minimized;
            }
            else
            {
                this.Show();
                this.WindowState = System.Windows.WindowState.Maximized;
            }
        }

        private void Window_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.Hide();
            this.WindowState = System.Windows.WindowState.Minimized;
        }
    }
}
