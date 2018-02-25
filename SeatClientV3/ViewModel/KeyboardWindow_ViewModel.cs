using SeatClientV3.OperateResult;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using System.ComponentModel;
using System.Windows.Forms;
using SeatClientV3.Code;
using SeatClientV3.WindowObject;

namespace SeatClientV3.ViewModel
{
    public class KeyboardWindow_ViewModel : INotifyPropertyChanged
    {
        public KeyboardWindow_ViewModel()
        {
            WindowWidth = 810;
            WindowHeight = 450;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            TitleAd = ClientObject.TitleAdvert != null ? ClientObject.TitleAdvert.TextContent : "Juneberry提醒您";

        }
        #region 属性

        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
        }
        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }
        private string _TitleAd;
        /// <summary>
        /// 标题广告
        /// </summary>
        public string TitleAd
        {
            get { return _TitleAd; }
            set { _TitleAd = value; OnPropertyChanged("TitleAd"); }
        }
        private int _CloseTime = 0;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
        }

        FormCloseCountdown _CountDown = null;
        /// <summary>
        /// 窗体关闭倒计时
        /// </summary>
        public FormCloseCountdown CountDown
        {
            get { return _CountDown; }
            set { _CountDown = value; }
        }
        private string _SeatNo = "";
        /// <summary>
        /// 座位编号
        /// </summary>
        public string SeatNo
        {
            get { return _SeatNo; }
            set { _SeatNo = value; OnPropertyChanged("SeatNo"); }
        }
        private string _LongSeatNo = "";
        /// <summary>
        /// 长编号
        /// </summary>
        public string LongSeatNo
        {
            get { return _LongSeatNo; }
            set { _LongSeatNo = value; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
        /// <summary>
        /// 确认座位号
        /// </summary>
        /// <returns></returns>
        public bool CheckSeatNumber()
        {
            if (string.IsNullOrEmpty(SeatNo))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatNotExists);
                return false;
            }
            _LongSeatNo = "";
            ReadingRoomInfo roomInfo = ClientObject.EnterOutLogData.Student.AtReadingRoom;
            string roomNo = roomInfo.No + "000";
            string seatHeader = SeatComm.SeatNoToSeatNoHeader(roomInfo.Setting.SeatNumAmount, roomNo);
            _LongSeatNo = seatHeader + SeatNo;
            //获取座位信息，并判断座位在该阅览室是否存在。
            Seat seat = T_SM_Seat.GetSeatInfoBySeatNo(_LongSeatNo);
            if (seat == null)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatNotExists);
                return false;
            }
            if (seat.IsSuspended)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatStopping);
                return false;
            }
            if (seat.ReadingRoomNum != roomInfo.No)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatNotExists);
                return false;
            }

            SeatManage.EnumType.EnterOutLogType logType = T_SM_EnterOutLog.GetSeatUsedState(_LongSeatNo);

            if (logType == SeatManage.EnumType.EnterOutLogType.None || logType == SeatManage.EnumType.EnterOutLogType.Leave)
            {
                //根据座位号获取进出记录的状态，如果为None或者为Leave，则锁定座位
                SeatManage.EnumType.SeatLockState lockResult = T_SM_Seat.LockSeat(_LongSeatNo);
                if (lockResult == SeatManage.EnumType.SeatLockState.NotExists)
                {
                    PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatNotExists);
                    return false;
                }
                if (lockResult == SeatManage.EnumType.SeatLockState.Locked)
                {
                    return true;
                }
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatUsing);
                return false;
            }
            else if (logType == SeatManage.EnumType.EnterOutLogType.BespeakWaiting)
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatUsing);
                return false;
            }
            else
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(SeatManage.EnumType.TipType.SeatUsing);
                return false;
            }
        }
    }
}
