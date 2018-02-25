using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LastSeatClient.ViewModel
{
    public class VM_BasicModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        private string _ErrorMsg = "";

        /// <summary>
        /// 错误信息
        /// </summary>
        public string ErrorMsg
        {
            get { return _ErrorMsg; }
            set { _ErrorMsg = value; Changed("ErrorMsg"); }
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
}
