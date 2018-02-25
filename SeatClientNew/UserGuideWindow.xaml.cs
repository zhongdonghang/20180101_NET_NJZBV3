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
using System.Windows.Media.Animation;

namespace SeatClientV2
{
    /// <summary>
    /// UserGuideWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UserGuideWindow : Window
    {
        public UserGuideWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.CloseTime = 20;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.CloseTime);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Start();
            Image oldImage = ImageCanvsa.Children[0] as Image;
            oldImage.Source = viewModel.Image;
        }
        ViewModel.UserGuideWindow_ViewModel viewModel = new ViewModel.UserGuideWindow_ViewModel();
        /// <summary>
        /// 倒计时窗口关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
            else
            {
                viewModel.CloseTime = viewModel.CountDown.CountdownSceonds;
            }
        }
        /// <summary>
        /// 退出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
            viewModel.CountDown.Stop();
        }
        /// <summary>
        /// 左移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_left_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.MoveLeft())
            {
                MoveRight();
            }
        }
        /// <summary>
        /// 右移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_right_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.MoveRight())
            {
                MoveLeft();
            }
        }


        Storyboard storyboard;
        private void MoveRight()
        {
            storyboard = new Storyboard();
            if (ImageCanvsa.Children.Count > 1)
            {
                ImageCanvsa.Children.RemoveAt(0);
            }

            Image oldImage = ImageCanvsa.Children[0] as Image;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, 1000, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Image newImage = new Image();
            newImage.Source = viewModel.Image;
            newImage.Height = 800;
            newImage.Width = 900;
            Canvas.SetLeft(newImage, -1000);
            Canvas.SetTop(newImage, 0);
            ImageCanvsa.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(-1000, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
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
            storyboard = new Storyboard();
            if (ImageCanvsa.Children.Count > 1)
            {
                ImageCanvsa.Children.RemoveAt(0);
            }

            Image oldImage = ImageCanvsa.Children[0] as Image;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, -1000, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Image newImage = new Image();
            newImage.Source = viewModel.Image;
            newImage.Height = 800;
            newImage.Width = 900;
            Canvas.SetLeft(newImage, 1000);
            Canvas.SetTop(newImage, 0);
            ImageCanvsa.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(1000, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }

            //动画播放
            storyboard.Begin();
        }
    }
}
