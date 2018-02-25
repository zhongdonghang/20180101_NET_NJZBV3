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
using SeatClientV2.Code;
using System.Windows.Media.Animation;
using SeatClientV2.OperateResult;

namespace SeatClientV2
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

            clientObject = OperateResult.SystemObject.GetInstance();
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                clientObject.ObjCardReader.Start();
            }
            clientObject.UpdateConfigError += new EventHandler(clientObject_UpdateConfigError);
            viewModel.ImageChange += new EventHandler(viewModel_ImageChange);
            viewModel.ImageSwitch += new EventHandler(viewModel_ImageSwitch);
            viewModel.ShowTimeRun();
            viewModel.LastSeatRun();
            viewModel.ImageChangeRun();
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
                SystemObject obj = sender as SystemObject;
                obj.StopUpdateConfig();
                viewModel.timeDateTimeSync.TimeStop();
                viewModel.MyLastSeatSumTime.TimeStop();
                if (obj.ObjCardReader != null)
                {
                    obj.ObjCardReader.Stop();
                }
                AppLoadingWindow AppLoading = new AppLoadingWindow();
                this.Hide();
                AppLoading.ShowDialog();
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
        /// <summary>
        /// 记录查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void logbtn_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //停止读卡注销事件
            StartRead();
            RecordTheQueryWindow logWindow = new RecordTheQueryWindow();
            logWindow.ShowDialog();
            //开始读卡注册事件
            StopRead();
        }
        /// <summary>
        /// 预约激活
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void activebtn_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //停止读卡注销事件
            StartRead();
            viewModel.ActiveBook();
            //开始读卡注册事件
            StopRead();
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
        /// 显示使用手册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void userGuide_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //停止读卡注销事件
            StartRead();
            UserGuideWindow userGuide = new UserGuideWindow();
            userGuide.ShowDialog();
            //开始读卡注册事件
            StopRead();
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
    }
}
