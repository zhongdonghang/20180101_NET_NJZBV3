using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;
using SeatManage.Bll;
using SeatClientV3.OperateResult;
using SeatClientV3.FunWindow;

namespace SeatClientV3.ViewModel
{
    public class KeyboardWindow_ViewModel : INotifyPropertyChanged
    {
        public KeyboardWindow_ViewModel()
        {
            clientobject = SystemObject.GetInstance();
            WindowWidth = (double)clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 720;
            WindowHeight = (double)clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 420;
            WindowLeft = (clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = (clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + clientobject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
        }
        #region 属性 成员
        SystemObject clientobject;
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject Clientobject
        {
            get { return clientobject; }
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
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
           _LongSeatNo = "";
            ReadingRoomInfo roomInfo = Clientobject.EnterOutLogData.Student.AtReadingRoom;
            string roomNo = roomInfo.No + "000";
            string seatHeader = SeatComm.SeatNoToSeatNoHeader(roomInfo.Setting.SeatNumAmount, roomNo);
            _LongSeatNo = seatHeader + SeatNo;
            //获取座位信息，并判断座位在该阅览室是否存在。
            Seat seat = T_SM_Seat.GetSeatInfoBySeatNo(_LongSeatNo);
            if (seat == null)
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
            if (seat.IsSuspended)
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsStopping);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
            if (seat.ReadingRoomNum != roomInfo.No)
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }

            SeatManage.EnumType.EnterOutLogType logType = SeatManage.Bll.T_SM_EnterOutLog.GetSeatUsedState(_LongSeatNo);

            //TODO:还需检测座位是否被预约 SeatManage.Bll.T_SM_EnterOutLog
            if (logType == SeatManage.EnumType.EnterOutLogType.None || logType == SeatManage.EnumType.EnterOutLogType.Leave)
            {
                //根据座位号获取进出记录的状态，如果为None或者为Leave，则锁定座位
                SeatManage.EnumType.SeatLockState lockResult = SeatManage.Bll.T_SM_Seat.LockSeat(_LongSeatNo);
                if (lockResult == SeatManage.EnumType.SeatLockState.NotExists)
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatNotExist);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    return false;
                }
                else if (lockResult == SeatManage.EnumType.SeatLockState.UnLock)
                {
                    MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsUsing);
                    CountDown.Pause();
                    popWindow.ShowDialog();
                    CountDown.Start();
                    return false;
                }
                else if (lockResult == SeatManage.EnumType.SeatLockState.Locked)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (logType == SeatManage.EnumType.EnterOutLogType.BespeakWaiting)
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsUsing);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
            else
            {
                MessageWindow popWindow = new MessageWindow(SeatManage.EnumType.MessageType.SeatIsUsing);
                CountDown.Pause();
                popWindow.ShowDialog();
                CountDown.Start();
                return false;
            }
            //} 
            //else
            //{
            //    toolTip1.SetToolTip(txtSeatNo, "请输入最后四位座位号");
            //    toolTip1.Show("请输入座位号",txtSeatNo,5000);  
            //}
        }
    }
}
