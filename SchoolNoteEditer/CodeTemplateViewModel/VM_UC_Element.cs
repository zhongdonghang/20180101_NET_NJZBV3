using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace SchoolNoteEditer.ViewModel
{
    public class VM_UC_Element : INotifyPropertyChanged
    {
        public VM_UC_Element()
        {

        }
        public event EventHandler CanvalPointChange;

        private SeatManage.ClassModel.DimensionalElement _Model = new SeatManage.ClassModel.DimensionalElement();
        /// <summary>
        /// model
        /// </summary>
        public SeatManage.ClassModel.DimensionalElement Model
        {
            get { return _Model; }
            set { _Model = value; OnPropertyChanged("Model"); }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImagePath
        {
            get { return _Model.ImageFile; }
            set
            {
                _Model.ImageFile = value;
                Image = new System.Windows.Media.ImageBrush(new System.Windows.Media.Imaging.BitmapImage(new Uri(_Model.ImageFile, UriKind.RelativeOrAbsolute)));
                ElementHeight = Image.ImageSource.Height;
                ElementWidth = Image.ImageSource.Width;
            }
        }
        /// <summary>
        /// 高度
        /// </summary>
        public double ElementHeight
        {
            get { return _Model.Height; }
            set
            {
                _Model.Height = value;
                OnPropertyChanged("ElementHeight");
                OnPropertyChanged("ElementBottom");
                OnPropertyChanged("CenterY");
                OnPropertyChanged("ElementMessage");
            }
        }
        /// <summary>
        /// 宽度
        /// </summary>
        public double ElementWidth
        {
            get { return _Model.Width; }
            set
            {
                _Model.Width = value;
                OnPropertyChanged("ElementWidth");
                OnPropertyChanged("ElementRight");
                OnPropertyChanged("CenterX");
                OnPropertyChanged("ElementMessage");
            }
        }
        /// <summary>
        /// 纵坐标
        /// </summary>
        public double ElementTop
        {
            get { return _Model.PosintionY; }
            set
            {
                _Model.PosintionY = value;
                OnPropertyChanged("ElementTop");
                OnPropertyChanged("ElementBottom");
                OnPropertyChanged("CenterY");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }
        /// <summary>
        /// 横坐标
        /// </summary>
        public double ElementLeft
        {
            get { return _Model.PosintionX; }
            set
            {
                _Model.PosintionX = value;
                OnPropertyChanged("ElementLeft");
                OnPropertyChanged("ElementRight");
                OnPropertyChanged("Centerx");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }
        /// <summary>
        /// 底坐标
        /// </summary>
        public double ElementBottom
        {
            get { return _TemplateHeight - _Model.PosintionY - _Model.Height; }
            set
            {
                _Model.PosintionY = _TemplateHeight - value - _Model.Height;
                OnPropertyChanged("ElementBottom");
                OnPropertyChanged("ElementTop");
                OnPropertyChanged("CenterY");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }
        /// <summary>
        /// 右横坐标
        /// </summary>
        public double ElementRight
        {
            get { return _TemplateWidth - _Model.PosintionX - _Model.Width; }
            set
            {
                _Model.PosintionX = _TemplateWidth - value - Model.Width;
                OnPropertyChanged("ElementRight");
                OnPropertyChanged("ElementLeft");
                OnPropertyChanged("Centerx");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }
        /// <summary>
        /// 中心X坐标
        /// </summary>
        public double CenterX
        {
            get { return _Model.PosintionX + _Model.Width / 2; }
            set
            {
                _Model.PosintionX = value - _Model.Width / 2;
                OnPropertyChanged("CenterX");
                OnPropertyChanged("ElementLeft");
                OnPropertyChanged("ElementRight");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }
        /// <summary>
        /// 中心y坐标
        /// </summary>
        public double CenterY
        {
            get { return _Model.PosintionY + _Model.Height / 2; }
            set
            {
                _Model.PosintionY = value - _Model.Height / 2;
                OnPropertyChanged("CenterY");
                OnPropertyChanged("ElementTop");
                OnPropertyChanged("ElementBottom");
                OnPropertyChanged("ElementMessage");
                if (CanvalPointChange != null)
                {
                    CanvalPointChange(null, null);
                }
            }
        }

        private double _TemplateHeight = 0;
        /// <summary>
        /// 模板高
        /// </summary>
        public double TemplateHeight
        {
            get { return _TemplateHeight; }
            set { _TemplateHeight = value; OnPropertyChanged("ElementBottom"); }
        }
        private double _TemplateWidth = 0;
        /// <summary>
        /// 模板宽
        /// </summary>
        public double TemplateWidth
        {
            get { return _TemplateWidth; }
            set { _TemplateWidth = value; OnPropertyChanged("ElementRight"); }
        }
        private int _DPI = 300;
        /// <summary>
        /// DPI
        /// </summary>
        public int DPI
        {
            get { return _DPI; }
            set
            {
                _DPI = value;
                OnPropertyChanged("DPI");
                OnPropertyChanged("ElementMessage");
            }
        }
        public string ElementMessage
        {
            get { return " " + (int)ElementLeft + "," + (int)ElementTop + " " + (int)ElementWidth + "x" + (int)ElementHeight + " " + (ElementWidth * 2.54 / DPI).ToString("0.00") + "x" + (ElementHeight * 2.54 / DPI).ToString("0.00") + " "; }
        }
        /// <summary>
        /// 元素旋转角度
        /// </summary>
        public double Angle
        {
            get { return _Model.Angle; }
            set
            {
                if (Math.Abs(_Model.Angle - value) % 180 == 90)
                {
                    double tempH = ElementHeight;
                    ElementHeight = ElementWidth;
                    ElementWidth = tempH;
                }
                _Model.Angle = value;
                OnPropertyChanged("Angle");
            }
        }
        private int _ColorRed = 255;
        /// <summary>
        /// 红色
        /// </summary>
        public int ColorRed
        {
            get { return _ColorRed; }
            set
            {
                _ColorRed = value;
                _Model.FontColor = "#FF" + _ColorRed.ToString("X2") + _ColorGreen.ToString("X2") + _ColorBlue.ToString("X2");
                OnPropertyChanged("Color");
                OnPropertyChanged("ColorRed");
            }
        }
        private int _ColorGreen =255;
        /// <summary>
        /// 绿色
        /// </summary>
        public int ColorGreen
        {
            get { return _ColorGreen; }
            set
            {
                _ColorGreen = value;
                _Model.FontColor = "#FF" + _ColorRed.ToString("X2") + _ColorGreen.ToString("X2") + _ColorBlue.ToString("X2");
                OnPropertyChanged("Color");
                OnPropertyChanged("ColorGreen");
            }
        }
        private int _ColorBlue = 255;
        /// <summary>
        /// 蓝色
        /// </summary>
        public int ColorBlue
        {
            get { return _ColorBlue; }
            set
            {
                _ColorBlue = value;
                _Model.FontColor = "#FF" + _ColorRed.ToString("X2") + _ColorGreen.ToString("X2") + _ColorBlue.ToString("X2");
                OnPropertyChanged("Color");
                OnPropertyChanged("ColorBlue");
            }
        }
        public string Color
        {
            get { return _Model.FontColor; }
            set
            {
                if (value.Length < 9)
                {
                    return;
                }
                _Model.FontColor = value;
                _ColorRed = Convert.ToInt32(_Model.FontColor.Substring(3, 2), 16);
                _ColorGreen = Convert.ToInt32(_Model.FontColor.Substring(5, 2), 16);
                _ColorBlue = Convert.ToInt32(_Model.FontColor.Substring(7, 2), 16);
                OnPropertyChanged("ColorRed");
                OnPropertyChanged("ColorBlue");
                OnPropertyChanged("ColorGreen");
                OnPropertyChanged("Color");
            }
        }
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text
        {
            get { return _Model.Text; }
            set { _Model.Text = value; OnPropertyChanged("Text"); }
        }
        /// <summary>
        /// 字号
        /// </summary>
        public int FontSize
        {
            get { return _Model.FontSize; }
            set { _Model.FontSize = value; OnPropertyChanged("FontSize"); }
        }
        /// <summary>
        /// 文本对齐方式
        /// </summary>
        public SeatManage.ClassModel.ElementTextAlignment TextAlignment
        {
            get { return _Model.Alignment; }
            set { _Model.Alignment = value; OnPropertyChanged("TextAlignment"); OnPropertyChanged("Alignment"); }
        }
        public string Alignment
        {
            get { return _Model.Alignment.ToString(); }
        }
        /// <summary>
        /// 类型
        /// </summary>
        public SeatManage.ClassModel.DimensionalElementTye Type
        {
            get { return _Model.Type; }
            set { _Model.Type = value; OnPropertyChanged("Type"); }
        }
        private System.Windows.Media.ImageBrush _Image = new System.Windows.Media.ImageBrush();
        /// <summary>
        /// Logo
        /// </summary>
        public System.Windows.Media.ImageBrush Image
        {
            get { return _Image; }
            set { _Image = value; OnPropertyChanged("Image"); }
        }
        private ObservableCollection<VM_TextAlignment> _TextAlignmentList = new ObservableCollection<VM_TextAlignment>();
        /// <summary>
        /// 对齐方式
        /// </summary>
        public ObservableCollection<VM_TextAlignment> TextAlignmentList
        {
            get { return _TextAlignmentList; }
            set { _TextAlignmentList = value; OnPropertyChanged("TextAlignmentList"); }
        }
        /// <summary>
        /// 角度列表
        /// </summary>
        private ObservableCollection<VM_Angle> _AngleList = new ObservableCollection<VM_Angle>();
        /// <summary>
        /// 对齐方式
        /// </summary>
        public ObservableCollection<VM_Angle> AngleList
        {
            get { return _AngleList; }
            set { _AngleList = value; OnPropertyChanged("AngleList"); }
        }
        /// <summary>
        /// 排序号
        /// </summary>
        public int Order
        {
            get { return _Model.Order; }
            set { _Model.Order = value; }
        }

        #region INotifyPropertyChanged 成员

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
    /// <summary>
    /// 对齐方式
    /// </summary>
    public class VM_TextAlignment
    {
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private SeatManage.ClassModel.ElementTextAlignment _Value = SeatManage.ClassModel.ElementTextAlignment.Left;
        /// <summary>
        /// 值
        /// </summary>
        public SeatManage.ClassModel.ElementTextAlignment Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
    /// <summary>
    /// 角度
    /// </summary>
    public class VM_Angle
    {
        private string _Name = "";
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private double _Value = 0;
        /// <summary>
        /// 值
        /// </summary>
        public double Value
        {
            get { return _Value; }
            set { _Value = value; }
        }
    }
}
