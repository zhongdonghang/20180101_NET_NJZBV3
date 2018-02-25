using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WPF_Seat.Code
{
    public class FromResolution : INotifyPropertyChanged
    {
        private static FromResolution instance;
        private static object _lock = new object();

        #region 界面元素实例化
        MyUIElement _Window = new MyUIElement();
        /// <summary>
        /// 主窗体
        /// </summary>
        public MyUIElement Window
        {
            get { return _Window; }
            set { _Window = value; Changed("Window"); }
        }
        MyUIElement _MainCanvas = new MyUIElement();
        /// <summary>
        /// 主窗体画布
        /// </summary>
        public MyUIElement MainCanvas
        {
            get { return _MainCanvas; }
            set { _MainCanvas = value; Changed("MainCanvas"); }
        }
        MyUIElement _LabTitle = new MyUIElement();
        /// <summary>
        /// 阅览室名称label
        /// </summary>
        public MyUIElement LabTitle
        {
            get { return _LabTitle; }
            set { _LabTitle = value; Changed("LabTitle"); }
        }
        MyUIElement _lblCloseTime = new MyUIElement();
        /// <summary>
        /// 关闭窗体描述label
        /// </summary>
        public MyUIElement LblCloseTime
        {
            get { return _lblCloseTime; }
            set { _lblCloseTime = value; Changed("LblCloseTime"); }
        }
        MyUIElement _LblAllSeatCount = new MyUIElement();
        /// <summary>
        /// 总座位数label
        /// </summary>
        public MyUIElement LblAllSeatCount
        {
            get { return _LblAllSeatCount; }
            set { _LblAllSeatCount = value; Changed("LblAllSeatCount"); }
        }
        MyUIElement _LblAtSeatCount = new MyUIElement();
        /// <summary>
        /// 在座数lable
        /// </summary>
        public MyUIElement LblAtSeatCount
        {
            get { return _LblAtSeatCount; }
            set { _LblAtSeatCount = value; Changed("LblAtSeatCount"); }
        }
        MyUIElement _LblFreeSeatCount = new MyUIElement();
        /// <summary>
        /// 空闲座位数lable
        /// </summary>
        public MyUIElement LblFreeSeatCount
        {
            get { return _LblFreeSeatCount; }
            set { _LblFreeSeatCount = value; Changed("LblFreeSeatCount"); }
        }
        MyUIElement _BtnBack = new MyUIElement();
        /// <summary>
        /// 返回按钮
        /// </summary>
        public MyUIElement BtnBack
        {
            get { return _BtnBack; }
            set { _BtnBack = value; Changed("BtnBack"); }
        }
        MyUIElement _Slt = new MyUIElement();

        public MyUIElement Slt
        {
            get { return _Slt; }
            set { _Slt = value; Changed("Slt"); }
        }
        MyUIElement _Thumbnail = new MyUIElement();

        public MyUIElement Thumbnail
        {
            get { return _Thumbnail; }
            set { _Thumbnail = value; Changed("Thumbnail"); }
        }
        MyUIElement _SeatWindow = new MyUIElement();

        public MyUIElement SeatWindow
        {
            get { return _SeatWindow; }
            set { _SeatWindow = value; Changed("SeatWindow"); }
        }
        MyUIElement _BtnKeyboard = new MyUIElement();
        /// <summary>
        /// 快速选座按钮
        /// </summary>
        public MyUIElement BtnKeyboard
        {
            get { return _BtnKeyboard; }
            set { _BtnKeyboard = value; Changed("BtnKeyboard"); }
        }
        #endregion

        private FromResolution()
        {

        }
        public static FromResolution GetFrmResolution(int frmWidth)
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new FromResolution(frmWidth);
                    }
                }
            }
            return instance;
        }

        private SeatFormViewModel _ViewModel = new SeatFormViewModel();

        public SeatFormViewModel ViewModel
        {
            get { return _ViewModel; }
            set
            {
                _ViewModel = value;
                Changed("ViewModel");
            }
        }
        private FromResolution(int frmWidth)
        {
            switch (frmWidth)
            {
                case 1080:
                    Window.Width = 1080;
                    Window.Height = 1000;
                    Window.Top = 920;
                    Window.Left = 0;

                    MainCanvas.Width = 1080;
                    MainCanvas.Height = 1000;

                    LabTitle.Width = 216;
                    LabTitle.Height = 54;
                    LabTitle.Top = 151;
                    LabTitle.Left = 53;

                    LblCloseTime.Width = 62;
                    LblCloseTime.Height = 68;
                    LblCloseTime.Top = 31;
                    LblCloseTime.Left = 451;

                    LblAllSeatCount.Width = 55;
                    LblAllSeatCount.Height = 39;
                    LblAllSeatCount.Top = 160;
                    LblAllSeatCount.Left = 355;

                    LblAtSeatCount.Width = 55;
                    LblAtSeatCount.Height = 39;
                    LblAtSeatCount.Top = 160;
                    LblAtSeatCount.Left = 490;

                    LblFreeSeatCount.Width = 55;
                    LblFreeSeatCount.Height = 39;
                    LblFreeSeatCount.Top = 160;
                    LblFreeSeatCount.Left = 626;

                    BtnBack.Width = 106;
                    BtnBack.Height = 56;
                    BtnBack.Top = 883;
                    BtnBack.Left = 896;

                    Slt.Width = 345;
                    Slt.Height = 370;
                    Slt.Top = 24;
                    Slt.Left = 688;

                    Thumbnail.Width = 294;
                    Thumbnail.Height = 292;
                    Thumbnail.Top = 81;
                    Thumbnail.Left = 717;

                    SeatWindow.Width = 983;
                    SeatWindow.Height = 749;
                    SeatWindow.Top = 203;
                    SeatWindow.Left = 47;

                    BtnKeyboard.Width = 138;
                    BtnKeyboard.Height = 55;
                    BtnKeyboard.Left = 720;
                    break;

                case 1024:
                    Window.Width = 1024;
                    Window.Height = 768;
                    Window.Top = 0;
                    Window.Left = 0;

                    MainCanvas.Width = 1024;
                    MainCanvas.Height = 768;

                    LabTitle.Width = 200;
                    LabTitle.Height = 46;
                    LabTitle.Top = 113;
                    LabTitle.Left = 52;

                    LblCloseTime.Width = 62;
                    LblCloseTime.Height = 68;
                    LblCloseTime.Top = 18;
                    LblCloseTime.Left = 428;

                    LblAllSeatCount.Width = 55;
                    LblAllSeatCount.Height = 31;
                    LblAllSeatCount.Top = 119;
                    LblAllSeatCount.Left = 336;

                    LblAtSeatCount.Width = 55;
                    LblAtSeatCount.Height = 31;
                    LblAtSeatCount.Top = 119;
                    LblAtSeatCount.Left = 466;

                    LblFreeSeatCount.Width = 55;
                    LblFreeSeatCount.Height = 31;
                    LblFreeSeatCount.Top = 119;
                    LblFreeSeatCount.Left = 595;

                    BtnBack.Width = 106;
                    BtnBack.Height = 56;
                    BtnBack.Top = 660;
                    BtnBack.Left = 847;

                    Slt.Width = 345;
                    Slt.Height = 370;
                    Slt.Top = 15;
                    Slt.Left = 657;

                    Thumbnail.Width = 294;
                    Thumbnail.Height = 292;
                    Thumbnail.Top = 72;
                    Thumbnail.Left = 686;

                    SeatWindow.Width = 941;
                    SeatWindow.Height = 581;
                    SeatWindow.Top = 154;
                    SeatWindow.Left = 41;

                    BtnKeyboard.Width = 138;
                    BtnKeyboard.Height = 55;
                    BtnKeyboard.Left = 700;
                    break;
            }

        }



        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    #region 界面元素基本属性
    /// <summary>
    /// 页面需要定位的元素
    /// </summary>
    public class MyUIElement : INotifyPropertyChanged
    {
        public MyUIElement()
        { }
        int _Height = 768;
        /// <summary>
        /// UI元素高度
        /// </summary>
        public int Height
        {
            get { return _Height; }
            set { _Height = value; Changed("Height"); }
        }
        int _Width = 1024;
        /// <summary>
        /// UI元素宽度
        /// </summary>
        public int Width
        {
            get { return _Width; }
            set { _Width = value; Changed("Width"); }
        }
        int _Top = 0;
        /// <summary>
        /// UI元素距离父容器顶部距离
        /// </summary>
        public int Top
        {
            get { return _Top; }
            set { _Top = value; Changed("Top"); }
        }
        int _Left = 0;
        /// <summary>
        /// UI元素距离父容器左边距
        /// </summary>
        public int Left
        {
            get { return _Left; }
            set { _Left = value; Changed("Left"); }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;

        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    #endregion
}
