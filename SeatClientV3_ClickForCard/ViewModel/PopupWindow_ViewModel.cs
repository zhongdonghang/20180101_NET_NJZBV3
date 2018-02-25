using SeatClientV3.OperateResult;
using SeatManage.EnumType;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Media;
using SeatManage.ClassModel;

namespace SeatClientV3.ViewModel
{
    public class PopupWindow_ViewModel : INotifyPropertyChanged
    {

        public PopupWindow_ViewModel()
        {
            WindowWidth = 590;
            WindowHeight = 470;
            if (ClientObject.PopAdvert != null)
            {
                WindowHeight = 335;
                PopAd = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\PopImage\\" + ClientObject.PopAdvert.PopImagePath, UriKind.RelativeOrAbsolute));
            }
            WindowLeft = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.X + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.X - WindowWidth) / 2;
            WindowTop = ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y + (ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Size.Y - WindowHeight) / 2;
            TitleAd = ClientObject.TitleAdvert != null ? ClientObject.TitleAdvert.TextContent : "Juneberry提醒您";
        }
        #region 属性 成员
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
        private UC_Tip_ViewModel _Tip_ViewModel = new UC_Tip_ViewModel();
        /// <summary>
        /// 提示消息
        /// </summary>
        public UC_Tip_ViewModel Tip_ViewModel
        {
            get { return _Tip_ViewModel; }
            set { _Tip_ViewModel = value; OnPropertyChanged("Tip_ViewModel"); }
        }
        private TipType _MessageType = TipType.None;
        /// <summary>
        /// 窗口消息类型
        /// </summary>
        public TipType MessageType
        {
            get { return _MessageType; }
            set { _MessageType = value; OnPropertyChanged("MessageType"); }
        }
        private ImageSource _PopAd;
        /// <summary>
        /// 弹窗广告
        /// </summary>
        public ImageSource PopAd
        {
            get { return _PopAd; }
            set { _PopAd = value; OnPropertyChanged("PopAd"); }
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
        private string _OKCaneclBtnVisibility = "Collapsed";
        /// <summary>
        /// 按钮隐藏
        /// </summary>
        public string OKCaneclBtnVisibility
        {
            get { return _OKCaneclBtnVisibility; }
            set { _OKCaneclBtnVisibility = value; OnPropertyChanged("OKCaneclBtnVisibility"); }
        }
        private string _CloseBtnVisibility = "Visible";
        /// <summary>
        /// 
        /// </summary>
        public string CloseBtnVisibility
        {
            get { return _CloseBtnVisibility; }
            set { _CloseBtnVisibility = value; OnPropertyChanged("CloseBtnVisibility"); }
        }
        private string _CardNo = "";
        /// <summary>
        /// 读卡的卡号
        /// </summary>
        public string CardNo
        {
            get { return _CardNo; }
            set { _CardNo = value; OnPropertyChanged("CardNo"); }
        }
        private string _TestMode = "Collapsed";
        /// <summary>
        /// 测试模式
        /// </summary>
        public string TestMode
        {
            get { return _TestMode; }
            set { _TestMode = value; OnPropertyChanged("TestMode"); }
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
        public void SetMessage(TipType ucType)
        {
            MessageType = ucType;

            TestMode = "Collapsed";
            CloseBtnVisibility = "Visible";
            OKCaneclBtnVisibility = "Collapsed";
            DateTime nowDateTime = SeatManage.Bll.ServiceDateTime.Now;
            switch (ucType)
            {
                case TipType.ContinuedTime:
                case TipType.AutoContinuedTime:
                    {
                        DateTime dt = new DateTime();
                        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                        {
                            Tip_ViewModel.LastCount = (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - ClientObject.EnterOutLogData.Student.ContinuedTimeCount - 1).ToString();
                        }
                        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                        {

                            dt = nowDateTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);
                            //如果开启24小时模式 不受闭馆时间影响
                            if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                            {
                                Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString(); Tip_ViewModel.EndTime = dt.ToShortTimeString();
                            }
                            else
                            {
                                if (dt > ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                {
                                    dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                                else
                                {
                                    Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString(); Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (nowDateTime >= ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {
                                    continue;
                                }
                                if (i + 1 < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                {
                                    dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                    Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                    Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                }
                                else
                                {
                                    dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                                break;
                            }
                        }
                        Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                    } break;
                case TipType.AutoContinuedTimeNoCount:
                    {
                        Tip_ViewModel.TitleMessage = "自动续时失败";
                        Tip_ViewModel.ShowMessage = "  您在暂离期间在座超时，由于续时次数已用完，系统自动续时失败，座位将自动释放";
                    } break;
                case TipType.ContinuedTimeNoCount:
                    {
                        Tip_ViewModel.TitleMessage = "续时失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您的续时次数已用完";
                    } break;
                case TipType.ContinuedTimeNotTime:
                    {
                        Tip_ViewModel.StartTime = ClientObject.EnterOutLogData.Student.CanContinuedTime.ToShortTimeString();
                        Tip_ViewModel.EndTime = ClientObject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime).ToShortTimeString();
                    } break;

                case TipType.ContinuedTimeWithout:
                    {
                        Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString();
                    } break;
                case TipType.BeapeatRoomNotExists:
                    {
                        Tip_ViewModel.TitleMessage = "终端选择错误";
                        Tip_ViewModel.ShowMessage = "  对不起，您预约的座位不在此终端的管辖范围内，请去对应的终端刷卡确认";
                    } break;
                case TipType.BespeatSeatConfirmFild:
                    {
                        Tip_ViewModel.TitleMessage = "预约确认失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您预约的座位确认失败";
                    } break;
                case TipType.NotReaderSelf:
                    {
                        Tip_ViewModel.TitleMessage = "读卡失败";
                        Tip_ViewModel.ShowMessage = "  对不起，此卡片非本人卡片";
                    } break;
                case TipType.SelectSeatResult:
                    {
                        Tip_ViewModel.ReaderNo = ClientObject.EnterOutLogData.EnterOutlog.CardNo;
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                        if (ClientObject.EnterOutLogData.BespeakLogInfo != null)
                        {
                            Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        }
                        else if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                        {
                            Tip_ViewModel.TipVisible = "Visible";
                            DateTime dt = new DateTime();
                            //if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                            //{

                            //    dt = nowDateTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength);
                            //    if (dt > ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                            //    {
                            //        dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                            //    }
                            //}
                            //else
                            //{
                            //    for (int i = 0; i < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            //    {
                            //        if (nowDateTime >= ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                            //        {
                            //            continue;
                            //        }
                            //        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime && nowDateTime > ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime))
                            //        {
                            //            dt = i + 1 < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count ? ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1] : ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                            //        }
                            //        else
                            //        {
                            //            dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i];
                            //        }
                            //        break;
                            //    }
                            //}
                            if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                            {

                                dt = nowDateTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength);
                                //如果开启24小时模式 不受闭馆时间影响
                                if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UninterruptibleModel)
                                {
                                    Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString(); Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                }
                                else
                                {
                                    if (dt > ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                    {
                                        dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                    else
                                    {
                                        Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString(); Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                    }
                                }
                            }
                            else
                            {
                                for (int i = 0; i < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                                {
                                    if (nowDateTime >= ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                    {
                                        continue;
                                    }
                                    if (i + 1 < ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                        Tip_ViewModel.StartTime = (dt.AddMinutes(-ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                        Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                    }
                                    else
                                    {
                                        dt = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                    break;
                                }
                            }
                            Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        }
                        else
                        {
                            Tip_ViewModel.TipVisible = "Hidden";
                        }
                    } break;
                case TipType.BespeatSeatConfirmSuccess:
                    {
                        Tip_ViewModel.ReaderNo = ClientObject.EnterOutLogData.Student.CardNo;
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                    } break;
                case TipType.ComeToBack:
                    {
                        Tip_ViewModel.LastCount = (SeatManage.Bll.ServiceDateTime.Now - ClientObject.EnterOutLogData.EnterOutlog.EnterOutTime).TotalMinutes.ToString().Split('.')[0];
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                    } break;
                case TipType.Exception:
                    {
                        Tip_ViewModel.TitleMessage = "连接失败";
                        Tip_ViewModel.ShowMessage = "  对不起，系统出现故障或网络连接失败，请再次尝试操作";
                    } break;
                case TipType.IsBlacklist:
                    {
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        if (ClientObject.EnterOutLogData.Student.BlacklistLog[0].OutBlacklistMode == LeaveBlacklistMode.AutomaticMode)
                        {
                            Tip_ViewModel.LastCount = (ClientObject.EnterOutLogData.Student.BlacklistLog[0].OutTime - nowDateTime).Days.ToString();
                        }
                        else
                        {
                            Tip_ViewModel.LastCount = "N";
                        }
                    } break;
                case TipType.Leave: break;
                case TipType.ReaderTypeInconformity:
                    {
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.ReaderType = ClientObject.EnterOutLogData.Student.ReaderType;
                    } break;
                case TipType.ReadingRoomClosing:
                    {
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UsedAdvancedSet && ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].Used)
                        {
                            foreach (TimeSpace space in ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].OpenTime)
                            {
                                Tip_ViewModel.SingleTime = space.BeginTime + "-" + space.EndTime + " ";
                            }
                        }
                        else
                        {
                            Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.BeginTime + "-" + ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime;
                        }
                    } break;
                case TipType.ReadingRoomFull:
                    {
                        Tip_ViewModel.TitleMessage = "阅览室已满";
                        Tip_ViewModel.ShowMessage = "  对不起，此阅览室人数已满，请选择其他阅览室";
                    } break;
                case TipType.SeatUsing:
                    {
                        Tip_ViewModel.TitleMessage = "座位已被选择";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已被选择，请重新选择";
                    } break;
                case TipType.SeatLocking:
                    {
                        Tip_ViewModel.TitleMessage = "座位正在被操作";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位正在被操作，请重试或者重新选择座位";
                    } break;
                case TipType.SeatStopping:
                    {
                        Tip_ViewModel.TitleMessage = "座位停用";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已停用重新选择座位";
                    } break;
                case TipType.SeatNotExists:
                    {
                        Tip_ViewModel.TitleMessage = "座位不存在";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位不存在，请重新选择";
                    } break;
                case TipType.SelectSeatFrequent:
                    {
                        Tip_ViewModel.TitleMessage = "操作频繁";
                        Tip_ViewModel.ShowMessage = "  对不起，您的选座次数过于频繁，请稍后再试";
                    } break;
                case TipType.IsBookingSeat:
                    {
                        Tip_ViewModel.TitleMessage = "座位已被预约";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已到达签到时间，请重新选择";
                    } break;
                case TipType.ShortLeave:
                    {
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.LastCount = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                        Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            foreach (SeatHoldTimeOption option in from option in ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime where option.Used let startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.BeginTime) let endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.EndTime) where SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime) select option)
                            {
                                Tip_ViewModel.LastCount = option.HoldTimeLength.ToString();
                                Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(option.HoldTimeLength).ToShortTimeString();
                                break;
                            }
                        }
                    } break;
                case TipType.ShortLeaveSeatOverTime:
                    {
                        Tip_ViewModel.TitleMessage = "在座超时";
                        Tip_ViewModel.ShowMessage = "  对不起，您在暂离期间在座超时，系统将自动释放您的座位。";
                    } break;
                case TipType.WaitSeatFrequent:
                    {
                        Tip_ViewModel.TitleMessage = "等待座位失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您等待座位次数过于频繁，请稍后再试";
                    } break;
                case TipType.WaitSeatWithSeat:
                    {
                        Tip_ViewModel.TitleMessage = "已有座位";
                        Tip_ViewModel.ShowMessage = "  对不起，您已经有座位了，不能设置其他读者暂离。";
                    } break;
                case TipType.WaitSeatSuccess:
                    {
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.LastCount = ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                        Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();

                        if (ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            foreach (SeatHoldTimeOption option in from option in ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime where !option.Used let startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.BeginTime) let endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.EndTime) where SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime) select option)
                            {
                                Tip_ViewModel.LastCount = option.HoldTimeLength.ToString();
                                Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(option.HoldTimeLength).ToShortTimeString();
                                break;
                            }
                        }

                    } break;
                case TipType.ActivationReadCard:
                    {
                        Tip_ViewModel.TitleMessage = "预约账号激活";
                        Tip_ViewModel.ShowMessage = "\n\t请刷卡！";
                        if (ClientObject.ObjCardReader == null)
                        {
                            TestMode = "Visible";
                        }
                    } break;
                case TipType.ActivationSuccess:
                    {
                        Tip_ViewModel.ReaderNo = ClientObject.EnterOutLogData.Student.CardNo;
                    } break;
                case TipType.CancelActivationWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.TitleMessage = "预约账号注销";
                        Tip_ViewModel.ShowMessage = "  您已经激活账号，是否注销此账号？（再次激活会重置密码）";
                    } break;
                case TipType.PrintConfIrm:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                    } break;
                case TipType.CancelActivationSuccess:
                    {
                        Tip_ViewModel.TitleMessage = "预约账号注销";
                        Tip_ViewModel.ShowMessage = "  您的账号已被注销，如需再次使用请重新激活";
                    } break;
                case TipType.BookConfirmWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.StartTime = ClientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.EndTime = ClientObject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    } break;
                case TipType.BookCancelSuccess:
                    {
                        Tip_ViewModel.TitleMessage = "预约取消";
                        Tip_ViewModel.ShowMessage = "  您预约的座位已取消";
                    } break;
                case TipType.SetShortWarning:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.WaitSeatLogModel.SeatNo;
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case TipType.SelectSeatConfinmed:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case TipType.WaitSeatCancelWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, ClientObject.EnterOutLogData.Student.WaitSeatLog.SeatNo);
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case TipType.WaitSeatCancel:
                    {
                        Tip_ViewModel.SeatNo = ClientObject.EnterOutLogData.Student.WaitSeatLog.SeatNo;
                        int time = int.Parse((ClientObject.EnterOutLogData.Student.WaitSeatLog.SeatWaitTime.AddMinutes(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.NoManagement.OperatingInterval) - nowDateTime).TotalMinutes.ToString().Split('.')[0]);
                        if (time < 0)
                        {
                            time = 0;
                        }
                        Tip_ViewModel.LastCount = time.ToString();
                    } break;
                case TipType.SelectBookingSeatWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, ClientObject.EnterOutLogData.BespeakLogInfo.SeatNo);
                        Tip_ViewModel.ReadingRoomName = ClientObject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SingleTime = ClientObject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(ClientObject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.LastCount = (DateTime.Parse(Tip_ViewModel.SingleTime) - nowDateTime).TotalMinutes.ToString().Split('.')[0];
                    } break;
                default: break;
            }
        }
        #endregion
    }
}
