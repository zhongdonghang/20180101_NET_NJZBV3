using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_WordToImage : INotifyPropertyChanged
    {
        private string _ErrorMessage = "";
        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            set { _ErrorMessage = value; OnPropertyChanged("ErrorMessage"); }
        }
        private string _SavePath = "";
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get { return _SavePath; }
            set { _SavePath = value; }
        }


        private string _Text = "";
        /// <summary>
        /// 文本信息
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { _Text = value; OnPropertyChanged("Text"); }
        }

        private int _TextFontSize = 25;
        /// <summary>
        /// 字号
        /// </summary>
        public int TextFontSize
        {
            get { return _TextFontSize; }
            set { _TextFontSize = value > 0 ? value : 1; OnPropertyChanged("TextFontSize"); }
        }

        private string _Title = "";
        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get { return _Title; }
            set { _Title = value; OnPropertyChanged("Title"); }
        }
        private int _TitleFontSize = 40;
        /// <summary>
        /// 标题字号
        /// </summary>
        public int TitleFontSize
        {
            get { return _TitleFontSize; }
            set { _TitleFontSize = value > 0 ? value : 1; OnPropertyChanged("TitleFontSize"); }
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


        public Bitmap GetBitmap()
        {
            try
            {
                Bitmap image = new Bitmap(840, 700);
                Graphics g = Graphics.FromImage(image);
                StringFormat titlesf = new StringFormat();
                titlesf.Alignment = StringAlignment.Center;
                g.DrawString(_Title, new Font("方正综艺简体", _TitleFontSize, FontStyle.Regular), Brushes.White, 420, 50, titlesf);
                StringFormat textsf = new StringFormat();

                Font textFont = new Font("方正综艺简体", _TextFontSize, FontStyle.Regular);
                string newString = "";
                string tempStr = "";
                double textheight = 0;
                for (int i = 0; i < _Text.Length; i++)
                {
                    tempStr += _Text.Substring(i, 1);
                    SizeF sf = g.MeasureString(tempStr, textFont);
                    if (sf.Width > 740)
                    {
                        newString += (newString != "" ? "\n\r" : "") + tempStr;
                        tempStr = "";
                        textheight += sf.Height;
                        if (textheight >= 530)
                        {
                            break;
                        }
                    }
                }
                newString += "\n\r" + tempStr;
                g.DrawString(newString, new Font("方正综艺简体", _TextFontSize, FontStyle.Regular), Brushes.White, 50, 150, textsf);
                return image;
            }
            catch
            {
                return null;
            }
        }
    }
}
