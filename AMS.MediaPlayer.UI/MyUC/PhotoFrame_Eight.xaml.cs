using System;
using System.Collections.Generic;
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
using Microsoft.Expression.Interactivity.Core;
using AMS.MediaPlayer.UI.Code;
using System.Timers;
using AMS.MediaPlayer.UI;

namespace WpfApplication10
{
    /// <summary>
    /// PhotoFrame.xaml 的互動邏輯
    /// </summary>
    public partial class PhotoFrame_Eight : UserControl
    {
        public PhotoFrame_Eight(SeatManage.ClassModel.CouponsInfo viewModel)
        {

            try
            {
                this.InitializeComponent();
                this.Width = 1080;
                this.Height = 156;
                this.Margin = new Thickness(2, 0, 2, 0);
                if (viewModel.Num == "")
                {
                    this.ImageUrl = viewModel.LogoImage;
                }
                else
                {
                    this.ImageUrl = string.Format("{0}{1}", AMS.MediaPlayer.Code.PlayerSetting.SysPath + "\\CouponsImage\\", viewModel.LogoImage);
                }
                this.SlipInfo = viewModel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public string ImageUrl
        {
            get
            {
                return (string)GetValue(ImageUrlProperty);
            }
            set
            {
                if (imgAnimation.Equals("ImageChanged1"))
                {
                    imgAnimation = "ImageChanged2";
                }
                else
                {
                    imgAnimation = "ImageChanged1";
                }
                this.Dispatcher.Invoke(new Action(() =>
                {
                    ExtendedVisualStateManager.GoToElementState(this.LayoutRoot, imgAnimation, true);
                    SetValue(ImageUrlProperty, value);
                    VisualState vs = new VisualState();

                }));

            }
        }

        private string imgAnimation = "ImageChanged1";

        public static readonly DependencyProperty ImageUrlProperty =
            DependencyProperty.Register("ImageUrl", typeof(string), typeof(PhotoFrame_Eight), new UIPropertyMetadata(string.Empty));



        public SeatManage.ClassModel.CouponsInfo SlipInfo
        {
            get { return (SeatManage.ClassModel.CouponsInfo)GetValue(SlipInfoProperty); }
            set { SetValue(SlipInfoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SlipInfo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SlipInfoProperty =
            DependencyProperty.Register("SlipInfo", typeof(SeatManage.ClassModel.CouponsInfo), typeof(PhotoFrame_Eight), new UIPropertyMetadata(new SeatManage.ClassModel.CouponsInfo()));
    }
}