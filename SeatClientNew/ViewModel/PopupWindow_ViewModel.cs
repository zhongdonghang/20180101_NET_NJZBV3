using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using SeatClientV2.OperateResult;
using System.Windows.Media;
using SeatManage.EnumType;

namespace SeatClientV2.ViewModel
{
    public class PopupWindow_ViewModel : INotifyPropertyChanged
    {

        public PopupWindow_ViewModel(SeatManage.EnumType.TipType ucType)
        {
            MessageType = ucType;

            clientobject = SystemObject.GetInstance();
            WindowHeight = clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.Y;
            WindowWidth = clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Size.X;
            WindowLeft = clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.X;
            WindowTop = clientobject.ClientSetting.DeviceSetting.SystemResoultion.TooltipSize.Location.Y;
            if (clientobject.PopAdvert != null)
            {
                //PopAd = clientobject.PopAdvert.PopImage;
                PopAd = new System.Windows.Media.Imaging.BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "images\\AdImage\\PopImage\\" + clientobject.PopAdvert.PopImagePath, UriKind.RelativeOrAbsolute));
                clientobject.PopAdvert.Usage.WatchCount++;
            }
            if (clientobject.TitleAdvert != null)
            {
                TitleAd = clientobject.TitleAdvert.TextContent;
                clientobject.TitleAdvert.Usage.WatchCount++;
            }
            else
            {
                TitleAd = "Juneberry提醒您";
            }
            if (clientobject.ObjCardReader == null && ucType == TipType.ActivationReadCard)
            {
                TestMode = "Visible";
            }
            SetMessage(ucType);
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
        private UC_Tip_ViewModel _Tip_ViewModel = new UC_Tip_ViewModel();
        /// <summary>
        /// 提示消息
        /// </summary>
        public UC_Tip_ViewModel Tip_ViewModel
        {
            get { return _Tip_ViewModel; }
            set { _Tip_ViewModel = value; OnPropertyChanged("Tip_ViewModel"); }
        }
        private SeatManage.EnumType.TipType _MessageType = SeatManage.EnumType.TipType.None;
        /// <summary>
        /// 窗口消息类型
        /// </summary>
        public SeatManage.EnumType.TipType MessageType
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
        /// <param name="nowDateTime"></param>
        private void SetMessage(SeatManage.EnumType.TipType ucType)
        {
            DateTime nowDateTime = SeatManage.Bll.ServiceDateTime.Now;
            switch (ucType)
            {
                case SeatManage.EnumType.TipType.ContinuedTime:
                case SeatManage.EnumType.TipType.AutoContinuedTime:
                    {
                        DateTime dt = new DateTime();
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes != 0)
                        {
                            Tip_ViewModel.LastCount = (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.ContinuedTimes - clientobject.EnterOutLogData.Student.ContinuedTimeCount - 1).ToString();
                        }
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                        {

                            dt = nowDateTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.DelayTimeLength);


                            if (dt > clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                            {
                                dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                            }
                            else
                            {
                                Tip_ViewModel.StartTime = (dt.AddMinutes(-clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                Tip_ViewModel.EndTime = dt.ToShortTimeString();
                            }
                        }
                        else
                        {
                            for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                            {
                                if (nowDateTime < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                {
                                    if (i + 1 < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                    {
                                        dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                        Tip_ViewModel.StartTime = (dt.AddMinutes(-clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime)).ToShortTimeString();
                                        Tip_ViewModel.EndTime = dt.ToShortTimeString();
                                    }
                                    else
                                    {
                                        dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                    }
                                    break;
                                }
                            }
                        }
                        Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                    } break;
                case SeatManage.EnumType.TipType.AutoContinuedTimeNoCount:
                    {
                        Tip_ViewModel.TitleMessage = "自动续时失败";
                        Tip_ViewModel.ShowMessage = "  您在暂离期间在座超时，由于续时次数已用完，系统自动续时失败，座位将自动释放";
                    } break;
                case SeatManage.EnumType.TipType.ContinuedTimeNoCount:
                    {
                        Tip_ViewModel.TitleMessage = "续时失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您的续时次数已用完";
                    } break;
                case SeatManage.EnumType.TipType.ContinuedTimeNotTime:
                    {
                        Tip_ViewModel.StartTime = clientobject.EnterOutLogData.Student.CanContinuedTime.ToShortTimeString();
                        Tip_ViewModel.EndTime = clientobject.EnterOutLogData.Student.CanContinuedTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime).ToShortTimeString();
                    } break;

                case SeatManage.EnumType.TipType.ContinuedTimeWithout:
                    {
                        Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.TipType.BeapeatRoomNotExists:
                    {
                        Tip_ViewModel.TitleMessage = "终端选择错误";
                        Tip_ViewModel.ShowMessage = "  对不起，您预约的座位不在此终端的管辖范围内，请去对应的终端刷卡确认";
                    } break;
                case SeatManage.EnumType.TipType.BespeatSeatConfirmFild:
                    {
                        Tip_ViewModel.TitleMessage = "预约确认失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您预约的座位确认失败";
                    } break;
                case SeatManage.EnumType.TipType.NotReaderSelf:
                    {
                        Tip_ViewModel.TitleMessage = "读卡失败";
                        Tip_ViewModel.ShowMessage = "  对不起，此卡片非本人卡片";
                    } break;
                case SeatManage.EnumType.TipType.SelectSeatResult:
                    {
                        Tip_ViewModel.ReaderNo = clientobject.EnterOutLogData.EnterOutlog.CardNo;
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                        if (clientobject.EnterOutLogData.BespeakLogInfo != null)
                        {
                            Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        }
                        else if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Used)
                        {
                            DateTime dt = new DateTime();
                            if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.Mode == "Free")
                            {

                                dt = nowDateTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.UsedTimeLength);
                                if (dt > clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime))
                                {
                                    dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count; i++)
                                {
                                    if (nowDateTime < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i])
                                    {
                                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.IsCanContinuedTime && nowDateTime > clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i].AddMinutes(-clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.CanDelayTime))
                                        {
                                            if (i + 1 < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes.Count)
                                            {
                                                dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i + 1];
                                            }
                                            else
                                            {
                                                dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.NowCloseTime(nowDateTime);
                                            }
                                        }
                                        else
                                        {
                                            dt = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatUsedTimeLimit.FixedTimes[i];
                                        }
                                        break;
                                    }
                                }
                            }
                            Tip_ViewModel.SingleTime = dt.ToShortTimeString();
                        }
                    } break;
                case SeatManage.EnumType.TipType.BespeatSeatConfirmSuccess:
                    {
                        Tip_ViewModel.ReaderNo = clientobject.EnterOutLogData.Student.CardNo;
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                    } break;
                case SeatManage.EnumType.TipType.ComeToBack:
                    {
                        Tip_ViewModel.LastCount = (SeatManage.Bll.ServiceDateTime.Now - clientobject.EnterOutLogData.EnterOutlog.EnterOutTime).TotalMinutes.ToString().Split('.')[0];
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.EnterOutlog.ReadingRoomName;
                    } break;
                case SeatManage.EnumType.TipType.Exception:
                    {
                        Tip_ViewModel.TitleMessage = "连接失败";
                        Tip_ViewModel.ShowMessage = "  对不起，系统出现故障或网络连接失败，请再次尝试操作";
                    } break;
                case SeatManage.EnumType.TipType.IsBlacklist:
                    {
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.LastCount = (clientobject.EnterOutLogData.Student.BlacklistLog[0].OutTime - nowDateTime).Days.ToString();
                    } break;
                case SeatManage.EnumType.TipType.Leave: break;
                case SeatManage.EnumType.TipType.ReaderTypeInconformity:
                    {
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.ReaderType = clientobject.EnterOutLogData.Student.ReaderType;
                    } break;
                case SeatManage.EnumType.TipType.ReadingRoomClosing:
                    {
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.UsedAdvancedSet && clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].Used)
                        {
                            foreach (SeatManage.ClassModel.TimeSpace space in clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.RoomOpenPlan[nowDateTime.DayOfWeek].OpenTime)
                            {
                                Tip_ViewModel.SingleTime = space.BeginTime + "-" + space.EndTime + " ";
                            }
                        }
                        else
                        {
                            Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.BeginTime + "-" + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.RoomOpenSet.DefaultOpenTime.EndTime;
                        }
                    } break;
                case SeatManage.EnumType.TipType.ReadingRoomFull:
                    {
                        Tip_ViewModel.TitleMessage = "阅览室已满";
                        Tip_ViewModel.ShowMessage = "  对不起，此阅览室人数已满，请选择其他阅览室";
                    } break;
                case SeatManage.EnumType.TipType.SeatUsing:
                    {
                        Tip_ViewModel.TitleMessage = "座位已被选择";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已被选择，请重新选择";
                    } break;
                case SeatManage.EnumType.TipType.SeatLocking:
                    {
                        Tip_ViewModel.TitleMessage = "座位正在被操作";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位正在被操作，请请重试或者重新选择座位";
                    } break;
                case SeatManage.EnumType.TipType.SeatStopping:
                    {
                        Tip_ViewModel.TitleMessage = "座位停用";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已停用重新选择座位";
                    } break;
                case SeatManage.EnumType.TipType.SeatNotExists:
                    {
                        Tip_ViewModel.TitleMessage = "座位不存在";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位不存在，请重新选择";
                    } break;
                case SeatManage.EnumType.TipType.SelectSeatFrequent:
                    {
                        Tip_ViewModel.TitleMessage = "操作频繁";
                        Tip_ViewModel.ShowMessage = "  对不起，您的选座次数过于频繁，请稍后再试";
                    } break;
                case SeatManage.EnumType.TipType.IsBookingSeat:
                    {
                        Tip_ViewModel.TitleMessage = "座位已被预约";
                        Tip_ViewModel.ShowMessage = "  对不起，此座位已到达签到时间，请重新选择";
                    } break;
                case SeatManage.EnumType.TipType.ShortLeave:
                    {
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                            {
                                if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                                {
                                    DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                                    DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                                    {
                                        Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                                        Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                            Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        }
                    } break;
                case SeatManage.EnumType.TipType.ShortLeaveSeatOverTime:
                    {
                        Tip_ViewModel.TitleMessage = "在座超时";
                        Tip_ViewModel.ShowMessage = "  对不起，您在暂离期间在座超时，系统将自动释放您的座位。";
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatFrequent:
                    {
                        Tip_ViewModel.TitleMessage = "等待座位失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您等待座位次数过于频繁，请稍后再试";
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatWithSeat:
                    {
                        Tip_ViewModel.TitleMessage = "已有座位";
                        Tip_ViewModel.ShowMessage = "  对不起，您已经有座位了，不能设置其他读者暂离。";
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatSuccess:
                    {
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                            {
                                if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                                {
                                    DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                                    DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                                    if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                                    {
                                        Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                                        Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                            Tip_ViewModel.SingleTime = nowDateTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        }
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                    } break;
                case SeatManage.EnumType.TipType.ActivationReadCard:
                    {
                        Tip_ViewModel.TitleMessage = "预约账号激活";
                        Tip_ViewModel.ShowMessage = "\n\t请刷卡！";
                    } break;
                case SeatManage.EnumType.TipType.ActivationSuccess:
                    {
                        Tip_ViewModel.ReaderNo = clientobject.EnterOutLogData.Student.CardNo;
                    } break;
                case SeatManage.EnumType.TipType.CancelActivationWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.TitleMessage = "预约账号注销";
                        Tip_ViewModel.ShowMessage = "  您已经激活账号，是否注销此账号？（再次激活会重置密码）";
                    } break;
                case SeatManage.EnumType.TipType.CancelActivationSuccess:
                    {
                        Tip_ViewModel.TitleMessage = "预约账号注销";
                        Tip_ViewModel.ShowMessage = "  您的账号已被注销，如需再次使用请重新激活";
                    } break;
                case SeatManage.EnumType.TipType.BookConfirmWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.StartTime = clientobject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.EndTime = clientobject.EnterOutLogData.Student.BespeakLog[0].BsepeakTime.AddMinutes(double.Parse(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.EndTime)).ToShortTimeString();
                    } break;
                case SeatManage.EnumType.TipType.BookCancelSuccess:
                    {
                        Tip_ViewModel.TitleMessage = "预约取消";
                        Tip_ViewModel.ShowMessage = "  您预约的座位已取消";
                    } break;
                case SeatManage.EnumType.TipType.SetShortWarning:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.WaitSeatLogModel.SeatNo;
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case SeatManage.EnumType.TipType.SelectSeatConfinmed:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatCancelWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientobject.EnterOutLogData.Student.WaitSeatLog.SeatNo);
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatCancel:
                    {
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.Student.WaitSeatLog.SeatNo;
                        int time = int.Parse((clientobject.EnterOutLogData.Student.WaitSeatLog.SeatWaitTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.NoManagement.OperatingInterval) - nowDateTime).TotalMinutes.ToString().Split('.')[0]);
                        if (time < 0)
                        {
                            time = 0;
                        }
                        Tip_ViewModel.LastCount = time.ToString();
                    } break;
                case SeatManage.EnumType.TipType.SelectBookingSeatWarn:
                    {
                        CloseBtnVisibility = "Collapsed";
                        OKCaneclBtnVisibility = "Visible";
                        Tip_ViewModel.SeatNo = SeatManage.SeatManageComm.SeatComm.SeatNoToShortSeatNo(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatNumAmount, clientobject.EnterOutLogData.BespeakLogInfo.SeatNo);
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.BespeakLogInfo.BsepeakTime.AddMinutes(-double.Parse(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatBespeak.ConfirmTime.BeginTime)).ToShortTimeString();
                        Tip_ViewModel.LastCount = (DateTime.Parse(Tip_ViewModel.SingleTime) - nowDateTime).TotalMinutes.ToString().Split('.')[0];
                    } break;
                default: break;
            }
        }
        #endregion
    }
}
