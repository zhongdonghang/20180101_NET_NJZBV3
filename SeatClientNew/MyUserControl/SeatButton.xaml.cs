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
using SeatClientV2.Code;

namespace SeatClientV2.MyUserControl
{
    /// <summary>
    /// SeatButton.xaml 的交互逻辑
    /// </summary>
    public partial class SeatButton : UserControl
    {
        public SeatButton()
        {
            InitializeComponent();
            this.DataContext = this;
            SetBinding(lblSeatNo,  TextBlock.TextProperty, "ShortSeatNo", null);
            //SetBinding(seatElement, BackgroundProperty, "SeatState", new ConvertSeatImage());
           
        }

        private void SetBinding(FrameworkElement obj, DependencyProperty p, string path,IValueConverter valueConverter)
        {
            Binding b = new Binding(); 
            b.Source = this;
            b.Converter = valueConverter;
            b.Path = new PropertyPath(path);
            b.Mode = BindingMode.OneWay;
            obj.SetBinding(p, b);
        }


        /// <summary>
        /// 小人的图像
        /// </summary>
        public ImageBrush ReaderBackground
        {
            get { return (ImageBrush)GetValue(ReaderBackgroundProperty); }
            set { SetValue(ReaderBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReaderBackground.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReaderBackgroundProperty =
            DependencyProperty.Register("ReaderBackground", typeof(ImageBrush), typeof(SeatButton), new UIPropertyMetadata(null));


        /// <summary>
        /// 是否有电源
        /// </summary>
        public ImageBrush PowerImg
        {
            get { return (ImageBrush)GetValue(PowerImgProperty); }
            set { SetValue(PowerImgProperty, value); }
        }


        /// <summary>
        /// 暂离图标
        /// </summary>
        public ImageBrush ShowleaveImg
        {
            get { return (ImageBrush)GetValue(ShowleaveImgProperty); }
            set { SetValue(ShowleaveImgProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowleaveImg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowleaveImgProperty =
            DependencyProperty.Register("ShowleaveImg", typeof(ImageBrush), typeof(SeatButton), new UIPropertyMetadata(null));



        // Using a DependencyProperty as the backing store for PowerImg.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PowerImgProperty =
            DependencyProperty.Register("PowerImg", typeof(ImageBrush), typeof(SeatButton), new UIPropertyMetadata(null));



        /// <summary>
        /// 座位状态
        /// </summary>
        public SeatManage.EnumType.SeatUsedState SeatState
        {
            get { return (SeatManage.EnumType.SeatUsedState)GetValue(SeatStateProperty); }
            set { SetValue(SeatStateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeatState.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeatStateProperty =
            DependencyProperty.Register("SeatState", typeof(SeatManage.EnumType.SeatUsedState), typeof(SeatButton));




        public string SeatNo
        {
            get { return (string)GetValue(SeatNoProperty); }
            set { SetValue(SeatNoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeatNo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SeatNoProperty =
            DependencyProperty.Register("SeatNo", typeof(string), typeof(SeatButton));
        ///// <summary>
        ///// 旋转角度
        ///// </summary>
        //public double Angle
        //{
        //    get { return (double)GetValue(AngleProperty); }
        //    set { SetValue(AngleProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for SeatNo.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty AngleProperty =
        //    DependencyProperty.Register("Angle", typeof(double), typeof(SeatButton));

        /// <summary>
        /// 座位号
        /// </summary>
        public string ShortSeatNo
        {
            get { return (string)GetValue(ShortSeatNoProperty); }
            set { SetValue(ShortSeatNoProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SeatNo.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortSeatNoProperty =
            DependencyProperty.Register("ShortSeatNo", typeof(string), typeof(SeatButton));

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {

        }


        ///// <summary>
        ///// 如果是在座，赋值在做读者的学号
        ///// </summary>
        //public string CardNo
        //{
        //    get { return (string)GetValue(CardNoProperty); }
        //    set { SetValue(CardNoProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for CardNo.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty CardNoProperty =
        //    DependencyProperty.Register("CardNo", typeof(string), typeof(SeatButton));


    }
}
