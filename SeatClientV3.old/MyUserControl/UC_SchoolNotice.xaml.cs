using SeatClientV3.UCViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatClientV3.MyUserControl
{
    /// <summary>
    /// UC_SchoolNotice.xaml 的交互逻辑
    /// </summary>
    public partial class UC_SchoolNotice : UserControl
    {
        public UC_SchoolNotice()
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
            viewModel.ImageChange += viewModel_ImageChange;
            viewModel.ImageChangeRun();
            Path image = image_Canvas.Children[0] as Path;
            image.Fill = new ImageBrush(viewModel.NowShowImage);
        }

        void viewModel_ImageChange(object sender, EventArgs e)
        {
            MoveLeft();
        }
        UC_SchoolNotice_ViewModel viewModel = new UC_SchoolNotice_ViewModel();

        #region 图片左右切换
        Storyboard storyboard;
        private void MoveRight()
        {
            if (image_Canvas.Children.Count > 1)
            {
                image_Canvas.Children.RemoveAt(0);
            }
            storyboard = new Storyboard();
            Path oldImage = image_Canvas.Children[0] as Path;
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, 770, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 450;
            newImage.Width = 770;
            newImage.Data = Geometry.Parse("M0,0 770,0 770,450 0,450 0,0");
            Canvas.SetLeft(newImage, -770);
            Canvas.SetTop(newImage, 0);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(-770, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
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
            DoubleAnimation doubleAnimationOld = new DoubleAnimation(0, -770, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationOld, oldImage);
            Storyboard.SetTargetProperty(doubleAnimationOld, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationOld);

            Path newImage = new Path();
            newImage.Fill = new ImageBrush(viewModel.NowShowImage);
            newImage.Height = 450;
            newImage.Width = 770;
            newImage.Data = Geometry.Parse("M0,0 770,0 770,450 0,450 0,0");
            Canvas.SetLeft(newImage, 770);
            Canvas.SetTop(newImage, 0);
            image_Canvas.Children.Add(newImage);
            DoubleAnimation doubleAnimationNew = new DoubleAnimation(770, 0, new Duration(TimeSpan.FromSeconds(0.5))); Storyboard.SetTarget(doubleAnimationNew, newImage);
            Storyboard.SetTargetProperty(doubleAnimationNew, new System.Windows.PropertyPath("(Canvas.Left)"));
            storyboard.Children.Add(doubleAnimationNew);
            if (!Resources.Contains("rectAnimation"))
            {
                Resources.Add("rectAnimation", storyboard);
            }
            //动画播放
            storyboard.Begin();
        }
        #endregion

        #region 点击事件
        private void btnlLeft_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ImageLeft())
            {
                MoveRight();
            }
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel.ImageRight())
            {
                MoveLeft();
            }
        }
        #endregion
    }
}