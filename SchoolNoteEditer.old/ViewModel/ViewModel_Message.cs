using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SchoolNoteEditer.Code;

namespace SchoolNoteEditer.ViewModel
{
    public class ViewModel_Message : INotifyPropertyChanged
    {
        private string _Message = "";
        /// <summary>
        /// 消息
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set { _Message = value; OnPropertyChanged("Message"); }
        }
        private MessageBoxType _Type = MessageBoxType.None;
        /// <summary>
        /// 消息框类型
        /// </summary>
        public MessageBoxType Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                switch (_Type)
                {
                    case MessageBoxType.Informatsion:
                        _ImageSource = "Image/MB_messageICO.png";
                        break;
                    case MessageBoxType.Success:
                        _ImageSource = "Image/MB_okICO.png";
                        break;
                    case MessageBoxType.Error:
                        _ImageSource = "Image/MB_erroeIOC.png";
                        break;
                    case MessageBoxType.Warning:
                        _ImageSource = "Image/MB_WarningICO.png";
                        break;
                    case MessageBoxType.Ask:
                        _ImageSource = "Image/MB_problemICO.png";
                        break;
                }
                OnPropertyChanged("Type");
                OnPropertyChanged("ImageSource");
            }
        }
        private string _ImageSource = "";
        /// <summary>
        /// 提示图片显示
        /// </summary>
        public string ImageSource
        {
            get { return _ImageSource; }
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
}
