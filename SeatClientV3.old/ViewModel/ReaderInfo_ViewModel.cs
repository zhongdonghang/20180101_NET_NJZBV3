using SeatManage.ClassModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeatClientV3.OperateResult;

namespace SeatClientV3.ViewModel
{
    public class ReaderInfo_ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 读者状态
        /// </summary>
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
                    return "阅览室：" + _ReaderInfo.AtReadingRoom.Name;
                }
                else
                {
                    return "阅览室：暂无";
                }
            }
        }
        /// <summary>
        /// 学号
        /// </summary>
        public string CardNo
        {
            get
            {
                if (!string.IsNullOrEmpty(_ReaderInfo.CardNo))
                {
                    return "学号：" + _ReaderInfo.CardNo;
                }
                else
                {
                    return "学号：请刷卡！";
                }
            }
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
                    return "座位号：" + _ReaderInfo.EnterOutLog.ShortSeatNo;
                }
                else
                {
                    return "座位号：暂无";
                }
            }
        }
        /// <summary>
        /// 读者姓名
        /// </summary>
        public string ReaderName
        {
            get
            {
                return "姓名：" + _ReaderInfo.Name;
            }
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
                    return "状态：未选座";
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
                            return "状态：在座";
                        case SeatManage.EnumType.EnterOutLogType.ShortLeave:
                            return "状态：暂离";
                    }
                    return "状态：未选座";
                }
            }
        }
        /// <summary>
        /// 座位信息
        /// </summary>
        public string RoomSeat
        {
            get
            {
                if (_ReaderInfo.AtReadingRoom != null && _ReaderInfo.EnterOutLog != null)
                {
                    return "座位：" + _ReaderInfo.AtReadingRoom.Name + " " + _ReaderInfo.EnterOutLog.ShortSeatNo + "号";
                }
                else
                {
                    return "座位：暂无";
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
