
using SeatClientV3.UCViewModel;
using SeatManage.EnumType;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using SeatClientV3.OperateResult;

namespace SeatClientV3.ViewModel
{
    public class MessageWindow_ViewModel : INotifyPropertyChanged
    {
        public MessageWindow_ViewModel(SeatManage.EnumType.MessageType ucType)
        {
            MessageType = ucType;
            clientObject = SystemObject.GetInstance();
            WindowWidth = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 620;
            WindowHeight = (double)clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X / 1080 * 325;
            WindowLeft = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = (clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + clientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;

            SetMessage(ucType);
        }
        #region 属性 成员
        SystemObject clientObject;
        /// <summary>
        /// 基础类
        /// </summary>
        public SystemObject Clientobject
        {
            get { return clientObject; }
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

        private Tip_ViewModel _Tip_ViewModel = new Tip_ViewModel();
        /// <summary>
        /// 提示消息
        /// </summary>
        public Tip_ViewModel Tip_ViewModel
        {
            get { return _Tip_ViewModel; }
            set { _Tip_ViewModel = value; OnPropertyChanged("Tip_ViewModel"); }
        }
        private SeatManage.EnumType.MessageType _MessageType = SeatManage.EnumType.MessageType.None;
        /// <summary>
        /// 窗口消息类型
        /// </summary>
        public SeatManage.EnumType.MessageType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; OnPropertyChanged("MessageType"); }
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

        HandleResult operateResule = HandleResult.None;
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

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 设置消息
        /// </summary>
        /// <param name="ucType"></param>
        /// <param name="nowDateTime"></param>
        private void SetMessage(SeatManage.EnumType.MessageType ucType)
        {
            DateTime nowDateTime = SeatManage.Bll.ServiceDateTime.Now;
            switch (ucType)
            {
                case SeatManage.EnumType.MessageType.ActivationSuccess:
                    {
                        Tip_ViewModel.CardNo = clientObject.EnterOutLogData.Student.CardNo;
                    }
                    break;
                case SeatManage.EnumType.MessageType.AutoContinueWhenNoCount: break;
                case SeatManage.EnumType.MessageType.AutoContinueWhenNotAgain:
                    {
                        Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.AutoContinueWhenSuccess:
                    {
                        DateTime dt = new DateTime();
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                        {
                            Tip_ViewModel.LastCount = (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - clientObject.EnterOutLogData.Student.ContinuedTimeCount - 1).ToString();
                        }
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                        {

                            dt = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);
                            //如果开启24小时模式 不受闭馆时间影响
                            if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                            {
                                Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                Tip_ViewModel.EndTime = dt.ToShortTimeString();
                            }
                            else
                            {
                                if (dt > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                {
                                    dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                                else
                                {
                                    Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                    Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (nowDateTime < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {
                                    if (i + 1 < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                        Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                        Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                    }
                                    else
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                    break;
                                }
                            }
                        }
                        Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        if (string.IsNullOrEmpty(Tip_ViewModel.StartTime))
                        {
                            MessageType = SeatManage.EnumType.MessageType.AutoContinueWhenNotAgain;
                        }
                    } break;
                case SeatManage.EnumType.MessageType.CancelBespeakSuccess:
                    {
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientObject.EnterOutLogData.Student.BespeakLog[0].SeatNo);
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SingleTime = nowDateTime.ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.CancelWaitConfirm:
                    {
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientObject.EnterOutLogData.Student.WaitSeatLog.SeatNo);
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case SeatManage.EnumType.MessageType.CancleWaitSuccess:
                    {
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.Student.WaitSeatLog.SeatNo;
                        int time = int.Parse((clientObject.EnterOutLogData.Student.WaitSeatLog.SeatWaitTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.NoManagement.OperatingInterval) - nowDateTime).TotalMinutes.ToString().Split('.')[0]);
                        if (time < 0)
                        {
                            time = 0;
                        }
                        Tip_ViewModel.LastCount = time.ToString();
                    } break;
                case SeatManage.EnumType.MessageType.CardDisable: break;
                case SeatManage.EnumType.MessageType.CheckBeapeakRoomNotExists: break;
                case SeatManage.EnumType.MessageType.CheckBespeakConfirm:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.BespeakLog[0].ReadingRoomName;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.Student.BespeakLog[0].ShortSeatNum;
                        Tip_ViewModel.StartTime = clientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.EndTime = clientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.CheckBespeakNotTime:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.BespeakLog[0].ReadingRoomName;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.Student.BespeakLog[0].ShortSeatNum;
                        Tip_ViewModel.StartTime = clientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.EndTime = clientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.CheckBespeakSuccess:
                    {
                        Tip_ViewModel.CardNo = clientObject.EnterOutLogData.EnterOutlog.CardNo;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                        DateTime dt = new DateTime();
                        if (clientObject.EnterOutLogData.BespeakLogInfo != null)
                        {
                            Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        }
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                        {
                            Tip_ViewModel.TipVisible = "Visible";
                            if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                            {

                                dt = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength);
                                if (!clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                                {
                                    if (dt > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                }

                            }
                            else
                            {
                                for (int i = 0; i < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                                {
                                    if (nowDateTime < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                    {
                                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime && nowDateTime > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime))
                                        {
                                            if (i + 1 < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                            {
                                                dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                            }
                                            else
                                            {
                                                dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                            }
                                        }
                                        else
                                        {
                                            dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i];
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tip_ViewModel.TipVisible = "Hidden";
                        }

                        if (string.IsNullOrEmpty(Tip_ViewModel.SingleTime) || DateTime.Parse(Tip_ViewModel.SingleTime) > dt)
                        {
                            Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        }
                    } break;
                case SeatManage.EnumType.MessageType.ComeBack:
                    {
                        Tip_ViewModel.LastCount = (SeatManage.Bll.ServiceDateTime.Now - clientObject.EnterOutLogData.EnterOutlog.EnterOutTime).TotalMinutes.ToString().Split('.')[0];
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                    } break;
                case SeatManage.EnumType.MessageType.ContinueWhenNoCount: break;
                case SeatManage.EnumType.MessageType.ContinueWhenNotAgain:
                    {
                        Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.ContinueWhenNotNeed:
                    {
                        Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.ContinueWhenNotSpan:
                    {
                        Tip_ViewModel.StartTime = clientObject.EnterOutLogData.Student.CanContinuedTime.ToShortTimeString();
                        Tip_ViewModel.EndTime = clientObject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.MessageType.ContinueWhenSuccess:
                    {
                        DateTime dt = new DateTime();
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                        {
                            Tip_ViewModel.LastCount = (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - clientObject.EnterOutLogData.Student.ContinuedTimeCount - 1).ToString();
                        }
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                        {

                            dt = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);
                            //如果开启24小时模式 不受闭馆时间影响
                            if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                            {
                                Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                Tip_ViewModel.EndTime = dt.ToShortTimeString();
                            }
                            else
                            {
                                if (dt > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                {
                                    dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                                else
                                {
                                    Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                    Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (nowDateTime < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {

                                    if (i + 1 < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                        Tip_ViewModel.StartTime = (dt.AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                        Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                    }
                                    else
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                    break;
                                }
                            }
                        }
                        Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        if (string.IsNullOrEmpty(Tip_ViewModel.StartTime))
                        {
                            MessageType = SeatManage.EnumType.MessageType.ContinueWhenNotAgain;
                        }
                    } break;
                case SeatManage.EnumType.MessageType.DeactivationComfrim: break;
                case SeatManage.EnumType.MessageType.DeactivationSuccess: break;
                case SeatManage.EnumType.MessageType.Exception: break;
                case SeatManage.EnumType.MessageType.Leave: break;
                case SeatManage.EnumType.MessageType.None: break;
                case SeatManage.EnumType.MessageType.NotReaderSelf: break;
                case SeatManage.EnumType.MessageType.PrintConfirm: break;
                case SeatManage.EnumType.MessageType.RoomBlacklist:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.LastCount = (clientObject.EnterOutLogData.Student.BlacklistLog[0].OutTime - nowDateTime).Days.ToString();
                    } break;
                case SeatManage.EnumType.MessageType.RoomFull: break;
                case SeatManage.EnumType.MessageType.RoomNotOpen:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UsedAdvancedSet && clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].Used)
                        {
                            foreach (SeatManage.ClassModel.TimeSpace space in clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].OpenTime)
                            {
                                Tip_ViewModel.SingleTime = space.BeginTime + "-" + space.EndTime + " ";
                            }
                        }
                        else
                        {
                            Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.BeginTime + "-" + clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime;
                        }
                    } break;
                case SeatManage.EnumType.MessageType.RoomNotReaderType:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.ReaderType = clientObject.EnterOutLogData.Student.ReaderType;
                    } break;
                case SeatManage.EnumType.MessageType.SeatIsBespaeked: break;
                case SeatManage.EnumType.MessageType.SeatIsLocked: break;
                case SeatManage.EnumType.MessageType.SeatIsStopping: break;
                case SeatManage.EnumType.MessageType.SeatIsUsing: break;
                case SeatManage.EnumType.MessageType.SeatNotExist: break;
                case SeatManage.EnumType.MessageType.SelectBespeakSeatConfrim:
                    {
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientObject.EnterOutLogData.BespeakLogInfo.SeatNo);
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.LastCount = (DateTime.Parse(Tip_ViewModel.SingleTime) - nowDateTime).TotalMinutes.ToString().Split('.')[0];
                    } break;
                case SeatManage.EnumType.MessageType.SelectBespeakSeatNoTime: break;
                case SeatManage.EnumType.MessageType.SelectSeatConfirm:
                    {
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case SeatManage.EnumType.MessageType.SelectSeatFrequent: break;
                case SeatManage.EnumType.MessageType.SelectSeatSuccess:
                    {
                        Tip_ViewModel.CardNo = clientObject.EnterOutLogData.EnterOutlog.CardNo;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                        DateTime dt = new DateTime();
                        if (clientObject.EnterOutLogData.BespeakLogInfo != null)
                        {
                            Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        }
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                        {
                            Tip_ViewModel.TipVisible = "Visible";
                            if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                            {

                                dt = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength);
                                if (!clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                                {
                                    if (dt > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                    {
                                        dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                }

                            }
                            else
                            {
                                for (int i = 0; i < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                                {
                                    if (nowDateTime < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                    {
                                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime && nowDateTime > clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime))
                                        {
                                            if (i + 1 < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                            {
                                                dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                            }
                                            else
                                            {
                                                dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                            }
                                        }
                                        else
                                        {
                                            dt = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i];
                                        }
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tip_ViewModel.TipVisible = "Hidden";
                        }

                        if (string.IsNullOrEmpty(Tip_ViewModel.SingleTime) || DateTime.Parse(Tip_ViewModel.SingleTime) > dt)
                        {
                            Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        }
                    } break;
                case SeatManage.EnumType.MessageType.SelectSeatWithoutAccess: break;
                case SeatManage.EnumType.MessageType.ShortLeave:
                    {
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.LastCount = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.GetSeatHoldTime(clientObject.EnterOutLogData.EnterOutlog.EnterOutTime).ToString();
                        Tip_ViewModel.SingleTime = clientObject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.GetSeatHoldTime(clientObject.EnterOutLogData.EnterOutlog.EnterOutTime)).ToShortTimeString();

                    } break;
                case SeatManage.EnumType.MessageType.ShortLeaveSeatOverTime: break;
                case SeatManage.EnumType.MessageType.WaitSeatConfirm:
                    {
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.WaitSeatLogModel.SeatNo;
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.LastCount = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.GetSeatHoldTime(nowDateTime).ToString().Split('.')[0];
                    } break;
                case SeatManage.EnumType.MessageType.WaitSeatFrequent:
                    {
                        Tip_ViewModel.LastCount = (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.NoManagement.OperatingInterval - (nowDateTime - clientObject.EnterOutLogData.Student.WaitSeatLog.SeatWaitTime).TotalMinutes).ToString().Split('.')[0];
                    }
                    break;
                case SeatManage.EnumType.MessageType.WaitSeatSuccess:
                    {
                        if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            for (int i = 0; i < clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                            {
                                if (clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                                {
                                    DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                                    DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                                    {
                                        Tip_ViewModel.LastCount = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                                        Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                                        break;
                                    }
                                    else
                                    {
                                        Tip_ViewModel.LastCount = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                                        Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tip_ViewModel.LastCount = clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                            Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(clientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        }
                        Tip_ViewModel.ReadingRoomName = clientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = clientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                    } break;
                case SeatManage.EnumType.MessageType.WaitSeatWithSeat: break;
                default: break;
            }
        }
        #endregion
    }
}
