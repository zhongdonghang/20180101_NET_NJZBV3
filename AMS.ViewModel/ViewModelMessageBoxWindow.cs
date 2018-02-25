using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AMS.Model;
using AMS.Model.Enum;
using System.ComponentModel;

namespace AMS.ViewModel
{
    public class ViewModelMessageBoxWindow : ViewModelObject
    {
        private static readonly string CLASSNAME = "ViewModelMessageBoxWindow";
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
                        _OKButtonVisibility = "Collapsed";
                        _CancelButtonVisibility = "Collapsed";
                        _CloseButtonVisibility = "Visible";
                        _ImageSource = "Image/MB_messageICO.png";
                        break;
                    case MessageBoxType.Success:
                        _OKButtonVisibility = "Collapsed";
                        _CancelButtonVisibility = "Collapsed";
                        _CloseButtonVisibility = "Visible";
                        _ImageSource = "Image/MB_okICO.png";
                        break;
                    case MessageBoxType.Error:
                        _OKButtonVisibility = "Collapsed";
                        _CancelButtonVisibility = "Collapsed";
                        _CloseButtonVisibility = "Visible";
                        _ImageSource = "Image/MB_erroeIOC.png";
                        break;
                    case MessageBoxType.Warning:
                        _OKButtonVisibility = "Visible";
                        _CancelButtonVisibility = "Visible";
                        _CloseButtonVisibility = "Collapsed";
                        _ImageSource = "Image/MB_WarningICO.png";
                        break;
                    case MessageBoxType.Ask:
                        _OKButtonVisibility = "Visible";
                        _CancelButtonVisibility = "Visible";
                        _CloseButtonVisibility = "Collapsed";
                        _ImageSource = "Image/MB_problemICO.png";
                        break;
                }
                OnPropertyChanged("Type");
                OnPropertyChanged("OKButtonVisibility");
                OnPropertyChanged("CancelButtonVisibility");
                OnPropertyChanged("CloseButtonVisibility");
                OnPropertyChanged("ImageSource");
            }
        }
        private string _Message = "";
        /// <summary>
        /// 消息类型
        /// </summary>
        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                int WordCount = 1;
                string[] msgs = _Message.Split('\n');
                foreach (string word in msgs)
                {
                    int wc = System.Text.Encoding.Default.GetByteCount(word);
                    if (WordCount < wc)
                    {
                        WordCount = wc;
                    }
                }
                if (WordCount > 14)
                {
                    _FontSize = ((200 / (WordCount / 2)) - 2);
                    if (_FontSize < 18)
                    {
                        _FontSize = 18;
                    }
                    if (_FontSize < 20)
                    {
                        _FormWidth = 300 + ((WordCount / 2) - 7) * 18;
                    }
                }
                if (((_FontSize + 4) * msgs.Length) > 72)
                {
                    _FormHeight = 200 + (_FontSize + 4) * msgs.Length - 72;
                }
                OnPropertyChanged("Message");
                OnPropertyChanged("FontSize");
                OnPropertyChanged("FormHeight");
                OnPropertyChanged("FormWidth");
            }
        }
        private bool _Result = false;
        /// <summary>
        /// 返回结果
        /// </summary>
        public bool Result
        {
            get { return _Result; }
            set { _Result = value; }
        }
        private string _OKButtonVisibility = "Collapsed";
        /// <summary>
        /// 确认按钮显示
        /// </summary>
        public string OKButtonVisibility
        {
            get { return _OKButtonVisibility; }
        }
        private string _CancelButtonVisibility = "Collapsed";
        /// <summary>
        /// 取消按钮显示
        /// </summary>
        public string CancelButtonVisibility
        {
            get { return _CancelButtonVisibility; }
        }
        private string _CloseButtonVisibility = "Collapsed";
        /// <summary>
        /// 关闭按钮显示
        /// </summary>
        public string CloseButtonVisibility
        {
            get { return _CloseButtonVisibility; }
        }
        private string _ImageSource = "";
        /// <summary>
        /// 提示图片显示
        /// </summary>
        public string ImageSource
        {
            get { return _ImageSource; }
        }
        private int _FontSize = 26;
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize
        {
            get { return _FontSize; }
        }
        private int _FormHeight = 200;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public int FormHeight
        {
            get { return _FormHeight; }
        }
        private int _FormWidth = 300;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public int FormWidth
        {
            get { return _FormWidth; }
        }

        #region 构造函数
        public ViewModelMessageBoxWindow()
        { }
        public ViewModelMessageBoxWindow(MessageBoxType type, string message)
        {
            Message = message;
            Type = type;
        }
        public ViewModelMessageBoxWindow(MessageBoxType type, AMS.Model.CustomerException customerEx)
        {
            string mag = customerEx.ErrorMessage;
            if (!string.IsNullOrEmpty(customerEx.ErrorSourcesClass))
            {
                mag += "\n出错类：" + customerEx.ErrorSourcesClass;
            }
            if (!string.IsNullOrEmpty(customerEx.ErrorSourcesFunction))
            {
                mag += "\n出错方法：" + customerEx.ErrorSourcesFunction;
            }
            Message = mag;
            Type = type;
        }
        #endregion
    }
}
