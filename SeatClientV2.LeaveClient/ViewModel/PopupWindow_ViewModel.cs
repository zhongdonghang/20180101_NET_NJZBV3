using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using ClientLeaveV2.OperateResult;
using System.Windows.Media;
using SeatManage.EnumType;
using System.Windows.Forms;
using SeatManage.ClassModel;

namespace ClientLeaveV2.ViewModel
{
    public class PopupWindow_ViewModel : INotifyPropertyChanged
    {

        public PopupWindow_ViewModel(SeatManage.EnumType.TipType ucType)
        {
            MessageType = ucType;

            clientobject = SystemObject.GetInstance();
            WindowLeft = Screen.PrimaryScreen.Bounds.Width / 2 - 290;
            WindowTop = Screen.PrimaryScreen.Bounds.Height / 2 - 200;
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
                case SeatManage.EnumType.TipType.BespeatSeatConfirmFild:
                    {
                        Tip_ViewModel.TitleMessage = "预约确认失败";
                        Tip_ViewModel.ShowMessage = "  对不起，您预约的座位确认失败";
                    } break;
                case SeatManage.EnumType.TipType.SelectSeatResult:
                    {
                        Tip_ViewModel.TitleMessage = "暂无座位";
                        Tip_ViewModel.ShowMessage = "  对不起，您目前没有座位，请先去选座终端选择座位";
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
                case SeatManage.EnumType.TipType.Leave:
                    break;
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
                        Tip_ViewModel.ShowMessage = "  对不起，您的操作过于频繁，请稍后再试";
                    } break;
                case SeatManage.EnumType.TipType.ShortLeave:
                    {
                        Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                        Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        {
                            foreach (SeatHoldTimeOption option in from option in clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime where option.Used let startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.BeginTime) let endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + option.UsedTime.EndTime) where SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime) select option)
                            {
                                Tip_ViewModel.LastCount = option.HoldTimeLength.ToString();
                                Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(option.HoldTimeLength).ToShortTimeString();
                                break;
                            }
                        }


                        //Tip_ViewModel.ReadingRoomName = clientobject.EnterOutLogData.Student.AtReadingRoom.Name;
                        //Tip_ViewModel.SeatNo = clientobject.EnterOutLogData.EnterOutlog.ShortSeatNo;
                        //if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.UsedAdvancedSet)
                        //{
                        //    for (int i = 0; i < clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime.Count; i++)
                        //    {
                        //        if (clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].Used)
                        //        {
                        //            DateTime startDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.BeginTime);
                        //            DateTime endDate = DateTime.Parse(nowDateTime.ToShortDateString() + " " + clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].UsedTime.EndTime);
                        //            if (SeatManage.SeatManageComm.DateTimeOperate.DateAccord(startDate, endDate, nowDateTime))
                        //            {
                        //                Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength.ToString();
                        //                Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.AdvancedSeatHoldTime[i].HoldTimeLength).ToShortTimeString();
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    Tip_ViewModel.LastCount = clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength.ToString();
                        //    Tip_ViewModel.SingleTime = clientobject.EnterOutLogData.EnterOutlog.EnterOutTime.AddMinutes(clientobject.EnterOutLogData.Student.AtReadingRoom.Setting.SeatHoldTime.DefaultHoldTimeLength).ToShortTimeString();
                        //}
                    } break;
                case SeatManage.EnumType.TipType.ShortLeaveSeatOverTime:
                    {
                        Tip_ViewModel.TitleMessage = "在座超时";
                        Tip_ViewModel.ShowMessage = "  对不起，您在暂离期间在座超时，系统将自动释放您的座位。";
                    } break;
                case SeatManage.EnumType.TipType.BookConfirmWarn:
                    {
                       Tip_ViewModel.TitleMessage = "确认时间未到";
                        Tip_ViewModel.ShowMessage = "  您预约的座位确认时间未到，如果需要取消预约请去选座终端操作";
                    } break;
                case SeatManage.EnumType.TipType.BookCancelSuccess:
                    {
                        Tip_ViewModel.TitleMessage = "预约取消";
                        Tip_ViewModel.ShowMessage = "  您预约的座位已取消";
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatCancelWarn:
                    {
                        Tip_ViewModel.TitleMessage = "存在等待的座位";
                        Tip_ViewModel.ShowMessage = "  您正在等待座位中，如想取消等待请去选座终端操作";
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
                default: break;
            }
        }
        #endregion
    }
}
