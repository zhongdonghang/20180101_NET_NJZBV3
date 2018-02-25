using SeatClientV3.OperateResult;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using SeatClientV3.Code;
using SeatClientV3.WindowObject;

namespace SeatClientV3.ViewModel
{
    public class UsuallySeatWindow_ViewModel : INotifyPropertyChanged
    {
        public UsuallySeatWindow_ViewModel()
        {
            WindowWidth = 810;
            WindowHeight = 470;
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            if (ClientObject.TitleAdvert != null)
            {
                TitleAd = ClientObject.TitleAdvert.TextContent;
                ClientObject.TitleAdvert.Usage.WatchCount++;
            }
            else
            {
                TitleAd = "Juneberry提醒您";
            }
            for (int i = 0; i < 12; i++)
            {
                OftenUsedSeats.Add(new UsuallySeatUC_ViewModel());
            }
        }

        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject ClientObject
        {
            get { return SystemObject.GetInstance(); }
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
        private ObservableCollection<UsuallySeatUC_ViewModel> _OftenUsedSeats = new ObservableCollection<UsuallySeatUC_ViewModel>();
        /// <summary>
        /// 常用座位LIST
        /// </summary>
        public ObservableCollection<UsuallySeatUC_ViewModel> OftenUsedSeats
        {
            get { return _OftenUsedSeats; }
            set { _OftenUsedSeats = value; OnPropertyChanged("OftenUsedSeats"); }
        }
        HandleResult operateResule = HandleResult.Failed;
        /// <summary>
        /// 操作结果，成功或者失败
        /// </summary>
        public HandleResult OperateResule
        {
            get { return operateResule; }
            set { operateResule = value; OnPropertyChanged("OperateResule"); }
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

        private int _CloseTime = 20;
        /// <summary>
        /// 窗口关闭时间
        /// </summary>
        public int CloseTime
        {
            get { return _CloseTime; }
            set { _CloseTime = value; OnPropertyChanged("CloseTime"); }
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
        /// 添加常坐座位
        /// </summary>
        public void AddOften()
        {
            try
            {
                List<Seat> seats = T_SM_Seat.GetReaderOftenUsedSeat(ClientObject.EnterOutLogData.EnterOutlog.CardNo, ClientObject.ClientSetting.DeviceSetting.UsingOftenUsedSeat.LengthDays, ClientObject.ClientSetting.DeviceSetting.Rooms);
                for (int i = 0; i < 12; i++)
                {
                    if (i > seats.Count - 1)
                    {
                        OftenUsedSeats[i].SeatNo = "";
                        OftenUsedSeats[i].ShortSeatNo = "";
                        OftenUsedSeats[i].ReadingRoomName = "";
                        OftenUsedSeats[i].ReadingRoomNo = "";
                        OftenUsedSeats[i].UCVisible = "Collapsed";
                    }
                    else
                    {
                        OftenUsedSeats[i].SeatNo = seats[i].SeatNo;
                        OftenUsedSeats[i].ShortSeatNo =SeatComm.SeatNoToShortSeatNo(ClientObject.ReadingRoomList[seats[i].ReadingRoomNum].Setting.SeatNumAmount,seats[i].SeatNo);
                        OftenUsedSeats[i].ReadingRoomName = ClientObject.ReadingRoomList[seats[i].ReadingRoomNum].Name;
                        OftenUsedSeats[i].ReadingRoomNo = seats[i].ReadingRoomNum;
                        OftenUsedSeats[i].UCVisible = "Visible";
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog.Write("加载阅览室遇到异常" + ex.Message);
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.Exception);
            }
        }
        /// <summary>
        /// 选座座位
        /// </summary>
        /// <param name="seatBtn"></param>
        /// <returns></returns>
        public bool SelectSeat(UsuallySeatUC_ViewModel seatBtn)
        {
            ReadingRoomInfo roomInfo = T_SM_ReadingRoom.GetSingleRoomInfo(seatBtn.ReadingRoomNo);
            ClientObject.EnterOutLogData.Student.AtReadingRoom = roomInfo;//给读者所在的阅览室赋值。

            //验证读者身份是否允许选择该阅览室。
            if (!SelectSeatProven.ProvenReaderType(ClientObject.EnterOutLogData.Student, roomInfo.Setting))
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.ReaderTypeInconformity);
                return false;
            }
            //验证读者黑名单，选座次数。
            if (SelectSeatProven.ProvenReaderState(ClientObject.EnterOutLogData.Student, roomInfo, ClientObject.RegulationRulesSet.BlacklistSet, ClientObject.ClientSetting.DeviceSetting))
            {
                //ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = "";
                return false;
            }
            SeatLockState lockseat = T_SM_Seat.LockSeat(seatBtn.SeatNo);
            if (lockseat != SeatLockState.Locked) //座位成功加锁
            {
                PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SeatLocking);
                return false;
            }

            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = seatBtn.ReadingRoomName;
            PopupWindowsObject.GetInstance().Window.ShowMessage(TipType.SelectSeatConfinmed);
            if (PopupWindowsObject.GetInstance().Window.ViewModel.OperateResule != HandleResult.Successed)
            {
                T_SM_Seat.UnLockSeat(seatBtn.SeatNo); //确认窗口点击取消或者自动关闭，则解锁。
                return false;
            }
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName = seatBtn.ReadingRoomName;
            ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomNo = seatBtn.ReadingRoomNo;
            ClientObject.EnterOutLogData.EnterOutlog.SeatNo = seatBtn.SeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo = seatBtn.ShortSeatNo;
            ClientObject.EnterOutLogData.EnterOutlog.TerminalNum = ClientObject.ClientSetting.ClientNo;
            ClientObject.EnterOutLogData.FlowControl = ClientOperation.SelectSeat; //操作为选择座位  
            ClientObject.EnterOutLogData.EnterOutlog.Remark = string.Format("在终端{0}选择常坐座位，{1}，{2}号座位", ClientObject.ClientSetting.ClientNo, ClientObject.EnterOutLogData.Student.AtReadingRoom.Name, seatBtn.ShortSeatNo);
            return true;
        }
    }
}
