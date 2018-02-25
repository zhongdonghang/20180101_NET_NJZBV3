using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using SeatManage.ClassModel; 

namespace WPF_ReaderInfo.Code
{
    public class ReaderInfoViewModel : INotifyPropertyChanged
    {
        public ReaderInfoViewModel()
        {

        }

        private ReaderInfo _ReaderInfo = new ReaderInfo();
        /// <summary>
        /// 读者类
        /// </summary>
        public ReaderInfo ReaderInfo
        {
            get { return _ReaderInfo; }
            set { _ReaderInfo = value; Changed("ReaderInfo"); }
        }

        /// <summary>
        /// 阅览室名称
        /// </summary>
        public string ReadingRoomName
        {
            get
            {
                if (_ReaderInfo.AtReadingRoom != null)
                {
                    return _ReaderInfo.AtReadingRoom.Name;
                }
                else
                {
                    return "暂无";
                }
            }
        }
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get { return _ReaderInfo.CardNo; }
        }
        /// <summary>
        /// 座位号
        /// </summary>
        public string SeatNo
        {
            get
            {
                if (_ReaderInfo.EnterOutLog != null && _ReaderInfo.EnterOutLog.EnterOutState != SeatManage.EnumType.EnterOutLogType.Leave)
                {
                    return _ReaderInfo.EnterOutLog.ShortSeatNo;
                }
                else
                {
                    return "暂无";
                }
            }
        }
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get { return _ReaderInfo.Name; }
        }
        /// <summary>
        /// 进出记录状态
        /// </summary>
        public string EnterOutState
        {
            get
            {
                if (_ReaderInfo.EnterOutLog == null)
                {
                    return "未选座";
                }
                else
                {
                    switch (_ReaderInfo.EnterOutLog.EnterOutState)
                    {
                        case SeatManage.EnumType.EnterOutLogType.BookingConfirmation:
                        case SeatManage.EnumType.EnterOutLogType.ReselectSeat:
                        case SeatManage.EnumType.EnterOutLogType.SelectSeat:
                        case SeatManage.EnumType.EnterOutLogType.WaitingSuccess:
                        case SeatManage.EnumType.EnterOutLogType.ComeBack:
                        case SeatManage.EnumType.EnterOutLogType.ContinuedTime:
                            return "在座";
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            return "暂离";
                    }
                    return "未选座";
                }
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
}
