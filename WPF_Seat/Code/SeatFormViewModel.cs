using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace WPF_Seat.Code
{
    public class SeatFormViewModel : INotifyPropertyChanged
    {
        private int _SeatAmcountAll = 0;
        /// <summary>
        /// 座位总数
        /// </summary>
        public int SeatAmcountAll
        {
            get { return _SeatAmcountAll; }
            set
            {
                _SeatAmcountAll = value;
                Changed("SeatAmcountAll");
            }
        }
        private int _SeatAmcountUsed = 0;
        /// <summary>
        /// 座位使用数
        /// </summary>
        public int SeatAmcountUsed
        {
            get { return _SeatAmcountUsed; }
            set
            {
                _SeatAmcountUsed = value;
                Changed("SeatAmcountUsed");
            }
        }
        private int _SeatAmcountFree = 0;
        /// <summary>
        /// 空闲座位数
        /// </summary>
        public int SeatAmcountFree
        {
            get { return _SeatAmcountFree; }
            set { _SeatAmcountFree = value;
            Changed("SeatAmcountFree");
            }
        }
        private string _RoomName = "";
        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string RoomName
        {
            get { return _RoomName; }
            set { _RoomName = value;
            Changed("RoomName");
            }
        }
        private int _CountDownSeconds = 0;
        /// <summary>
        /// 要关闭的秒数
        /// </summary>
        public int CountDownSeconds
        {
            get { return _CountDownSeconds; }
            set { _CountDownSeconds = value;
            Changed("CountDownSeconds");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void Changed(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
