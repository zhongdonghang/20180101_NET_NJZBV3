using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Media;
using SeatClientV3.Code;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;

namespace SeatClientV3.ViewModel
{
    public class SeatUC_ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 选座操作
        /// </summary>
        public event EventHandler SelectSeat;
        /// <summary>
        /// 等待座位操作
        /// </summary>
        public event EventHandler WaitSeat;
        /// <summary>
        /// 选座预约座位的操作
        /// </summary>
        public event EventHandler SelectBespeakSeat;

        /// <summary>
        /// 完整座位编号
        /// </summary>
        private string _longSeatNo;
        /// <summary>
        /// 短座位编号
        /// </summary>
        private string _shortSeatNo;
        /// <summary>
        /// 阅览室编号
        /// </summary>
        private string _readingRoomNo;
        /// <summary>
        /// 座位状态
        /// </summary>
        private bool _isUsing;
        /// <summary>
        /// 是否开启预约
        /// </summary>
        private bool _isBespeak;
        /// <summary>
        /// 是否停用
        /// </summary>
        private bool _isStop;
        /// <summary>
        /// 是否有电源
        /// </summary>
        private bool _isPower;
        /// <summary>
        /// 是否暂离
        /// </summary>
        private bool _isShortLeave;
        /// <summary>
        /// 座位是否被等待
        /// </summary>
        private bool _isWaiting;
        /// <summary>
        /// 是否能够等待座位
        /// </summary>
        private bool _isCanWaitSeat;
        /// <summary>
        /// 是否能够选择预约的座位
        /// </summary>
        private bool _isCanSelectBespeakSeat;
  
        /// <summary>
        /// 基础类
        /// </summary>

        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }
        /// <summary>
        /// 完整座位编号
        /// </summary>
        public string LongSeatNo
        {
            get { return _longSeatNo; }
            set { _longSeatNo = value; }
        }

        /// <summary>
        /// 短座位编号
        /// </summary>
        public string ShortSeatNo
        {
            get { return _shortSeatNo; }
            set
            {
                _shortSeatNo = value;
                OnPropertyChanged("ShortSeatNo");
            }
        }

        
        /// <summary>
        /// 阅览室编号
        /// </summary>
        public string ReadingRoomNo
        {
            get { return _readingRoomNo; }
            set { _readingRoomNo = value; }
        }
        /// <summary>
        /// 座位状态
        /// </summary>
        public bool IsUsing
        {
            get { return _isUsing; }
            set
            {
                _isUsing = value;
                OnPropertyChanged("SeatStateImage");
                OnPropertyChanged("ReaderStateImage");
            }
        }

        /// <summary>
        /// 是否被预约
        /// </summary>
        public bool IsBespeak
        {
            get { return _isBespeak; }
            set
            {
                _isBespeak = value;
                OnPropertyChanged("BespeakImage");
            }
        }
        /// <summary>
        /// 是否停用
        /// </summary>
        public bool IsStop
        {
            get { return _isStop; }
            set
            {
                _isStop = value;
                OnPropertyChanged("ReaderStateImage");
                OnPropertyChanged("SeatNoVisibility");
            }
        }
        /// <summary>
        /// 是否暂离
        /// </summary>
        public bool IsShortLeave
        {
            get { return _isShortLeave; }
            set
            {
                _isShortLeave = value;
                OnPropertyChanged("ShortLeaveImage");
                OnPropertyChanged("ReaderStateImage");
            }
        }

        /// <summary>
        /// 座位是否被等待
        /// </summary>
        public bool IsWaiting
        {
            get { return _isWaiting; }
            set
            {
                _isWaiting = value;
                OnPropertyChanged("ReaderStateImage");
            }
        }
        /// <summary>
        /// 是否有电源
        /// </summary>
        public bool IsPower
        {
            get { return _isPower; }
            set
            {
                _isPower = value;
                OnPropertyChanged("PowerImage");
            }
        }
        /// <summary>
        /// 座位状态图片
        /// </summary>
        public ImageBrush SeatStateImage
        {
            get
            {
                if (_isUsing)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgSeatUse;
                }
                else
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgSeat;
                }
            }
        }

        /// <summary>
        /// 读者状态图片
        /// </summary>
        public ImageBrush ReaderStateImage
        {
            get
            {
                if (_isStop)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgStopUse;
                }
                if (_isUsing && !_isShortLeave || _isUsing && _isWaiting)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgReader;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 电源图片
        /// </summary>
        public ImageBrush PowerImage
        {
            get
            {
                if (_isPower)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgPower;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 暂离图片
        /// </summary>
        public ImageBrush ShortLeaveImage
        {
            get
            {
                if (_isShortLeave)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgShortLeave;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 预约图片
        /// </summary>
        public ImageBrush BespeakImage
        {
            get
            {
                if (_isBespeak)
                {
                    return SeatFormImageBrush.GetInstance(ClientObject.ClientSetting.DeviceSetting.BackImgage).ImgBook;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 是否能够等待座位
        /// </summary>
        public bool IsCanWaitSeat
        {
            get { return _isCanWaitSeat; }
            set { _isCanWaitSeat = value; }
        }

        /// <summary>
        /// 是否能够选择预约的座位
        /// </summary>
        public bool IsCanSelectBespeakSeat
        {
            get { return _isCanSelectBespeakSeat; }
            set { _isCanSelectBespeakSeat = value; }
        }

        public string SeatNoVisibility
        {
            get
            {
                if (_isStop)
                {
                    return "Collapsed";
                }
                else
                {
                    return "Visible";
                }
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
        /// <summary>
        /// 选座操作
        /// </summary>
        public void SelectSeatOperation()
        {
            if (_isStop || _isShortLeave)
            {
                return;
            }
            if (_isUsing)
            {
                if (_isCanWaitSeat && WaitSeat != null)
                {
                    WaitSeat(this, new EventArgs());
                }
            }
            else
            {
                if (_isBespeak)
                {
                    if (_isCanSelectBespeakSeat && SelectBespeakSeat != null)
                    {
                        SelectBespeakSeat(this, new EventArgs());
                    }
                }
                else
                {
                    if (SelectSeat != null)
                    {
                        SelectSeat(this, new EventArgs());
                    }
                }
            }
        }
    }
}
