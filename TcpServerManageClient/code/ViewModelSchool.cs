using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.ComponentModel;

namespace TcpServerManageClient
{
    public class ViewModelSchool : INotifyPropertyChanged
    {

        private string _SchoolNum;
        /// <summary>
        /// 学校Id
        /// </summary>
        public string SchoolNum
        {
            get { return _SchoolNum; }
            set
            {
                _SchoolNum = value;
                OnPropertyChanged("SchoolNum");
            }
        }
        private bool _IsOnline = false;
        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline
        {
            get { return _IsOnline; }
            set
            {
                _IsOnline = value;
                OnPropertyChanged("IsOnline");
            }
        }
        private string _SchoolName;
        /// <summary>
        /// 学校名字
        /// </summary>
        public string SchoolName
        {
            get { return _SchoolName; }
            set
            {
                _SchoolName = value;
                OnPropertyChanged("SchoolName");
            }
        }
        private string _Ip;

        public string Ip
        {
            get { return _Ip; }
            set
            {
                _Ip = value;
                OnPropertyChanged("Ip");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
