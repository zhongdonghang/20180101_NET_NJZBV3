using System;
using System.Windows;
using SeatClientV3.MyUserControl;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;

namespace SeatClientV3
{
    /// <summary>
    /// PopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow()
        {
            InitializeComponent();
            DataContext = ViewModel;
            if (ViewModel.PopAd == null)
            {
                Height = 335;
                image_down.Visibility = Visibility.Collapsed;
                rec_down.Visibility = Visibility.Collapsed;
                rec_down_co.Visibility = Visibility.Visible;
            }
        }
        /// <summary>
        /// 显示窗体
        /// </summary>
        /// <param name="ucType"></param>
        public void ShowMessage(TipType ucType)
        {
            ViewModel.SetMessage(ucType);
            ShowUC_Type();
            ViewModel.CountDown = ucType == TipType.ActivationReadCard ? new FormCloseCountdown(15) : new FormCloseCountdown(7);
            ViewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            if (ucType == TipType.ActivationReadCard)
            {
                StartRead();
            }
            ViewModel.CountDown.Start();
            this.Topmost = true;
            ShowDialog();
        }
        private void StopRead()
        {
            if (ViewModel.ClientObject.ObjCardReader != null)
            {
                ViewModel.ClientObject.ObjCardReader.Stop();
                ViewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
        }
        private void StartRead()
        {
            if (ViewModel.ClientObject.ObjCardReader != null)
            {
                ViewModel.ClientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
                ViewModel.ClientObject.ObjCardReader.Start();
            }
        }
        /// <summary>
        /// 读卡器读到卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            StopRead();
            if (e.PosResult)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    ViewModel.OperateResule = HandleResult.Successed;
                    ViewModel.CardNo = e.CardNo;
                    WinClosing();
                }));
            }
            else
            {
                SeatManage.SeatManageComm.WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
            }
            System.Threading.Thread.Sleep(2000);
            StartRead();
        }
        public ViewModel.PopupWindow_ViewModel ViewModel = new ViewModel.PopupWindow_ViewModel();
        /// <summary>
        /// 触发窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (ViewModel.CountDown.CountdownSceonds <= 0)
            {
                ViewModel.OperateResule = HandleResult.Failed;
                Dispatcher.Invoke(new Action(WinClosing));
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WinClosing()
        {
            Hide();
            if (ViewModel.ClientObject.PopAdvert != null)
            {
                ViewModel.ClientObject.PopAdvert.Usage.WatchCount++;
            }
            if (ViewModel.ClientObject.TitleAdvert != null)
            {
                ViewModel.ClientObject.TitleAdvert.Usage.WatchCount++;
            }
            ViewModel.CountDown.Stop();
            ViewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            if (ViewModel.MessageType == TipType.ActivationReadCard)
            {
                StopRead();
            }
        }

        /// <summary>
        /// 显示控件
        /// </summary>
        private void ShowUC_Type()
        {
            UC_Cancas.Children.Clear();
            switch (ViewModel.MessageType)
            {
                case TipType.ContinuedTime:
                    {
                        if (string.IsNullOrEmpty(ViewModel.Tip_ViewModel.StartTime))
                        {
                            UC_Tip_ContinueTimeNoAgain UC_tip = new UC_Tip_ContinueTimeNoAgain(ViewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            UC_Cancas.Children.Add(UC_tip);
                        }
                        else
                        {
                            UC_Tip_ContinueTime UC_tip = new UC_Tip_ContinueTime(ViewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            UC_Cancas.Children.Add(UC_tip);
                        }
                    } break;
                case TipType.AutoContinuedTime:
                    {
                        if (string.IsNullOrEmpty(ViewModel.Tip_ViewModel.StartTime))
                        {
                            UC_Tip_ContinueTimeAutoNoAgain UC_tip = new UC_Tip_ContinueTimeAutoNoAgain(ViewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            UC_Cancas.Children.Add(UC_tip);
                        }
                        else
                        {
                            UC_Tip_ContinueTimeAuto UC_tip = new UC_Tip_ContinueTimeAuto(ViewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            UC_Cancas.Children.Add(UC_tip);
                        }
                    } break;
                case TipType.WaitSeatWithSeat:
                case TipType.AutoContinuedTimeNoCount:
                case TipType.ContinuedTimeNoCount:
                case TipType.BeapeatRoomNotExists:
                case TipType.BespeatSeatConfirmFild:
                case TipType.Exception:
                case TipType.ReadingRoomFull:
                case TipType.SeatUsing:
                case TipType.SeatLocking:
                case TipType.SeatStopping:
                case TipType.SeatNotExists:
                case TipType.SelectSeatFrequent:
                case TipType.ShortLeaveSeatOverTime:
                case TipType.WaitSeatFrequent:
                case TipType.NotReaderSelf:
                case TipType.IsBookingSeat:
                    {
                        UC_Tip_CommFailed UC_tip = new UC_Tip_CommFailed(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ContinuedTimeNotTime:
                    {
                        UC_Tip_ContinueTimeNoTime UC_tip = new UC_Tip_ContinueTimeNoTime(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ContinuedTimeWithout:
                    {
                        UC_Tip_ContinueTimeNoNeed UC_tip = new UC_Tip_ContinueTimeNoNeed(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.SelectSeatResult:
                case TipType.BespeatSeatConfirmSuccess:
                    {
                        UC_Tip_SelectSeatResult UC_tip = new UC_Tip_SelectSeatResult(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ComeToBack:
                    {
                        UC_Tip_ComeBack UC_tip = new UC_Tip_ComeBack(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.IsBlacklist:
                    {
                        UC_Tip_IsBlacklist UC_tip = new UC_Tip_IsBlacklist(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.Leave:
                    {
                        UC_Tip_Leave UC_tip = new UC_Tip_Leave();
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ReaderTypeInconformity:
                    {
                        UC_Tip_EnterNoType UC_tip = new UC_Tip_EnterNoType(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ReadingRoomClosing:
                    {
                        UC_Tip_ReadingRoomNoOpen UC_tip = new UC_Tip_ReadingRoomNoOpen(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ShortLeave:
                    {
                        UC_Tip_ShortLeave UC_tip = new UC_Tip_ShortLeave(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.WaitSeatSuccess:
                    {
                        UC_Tip_WaitSeat UC_tip = new UC_Tip_WaitSeat(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ActivationReadCard:
                    {

                        UC_Tip_CommWarm UC_tip = new UC_Tip_CommWarm(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ActivationSuccess:
                    {
                        UC_Tip_ActivationSuccess UC_tip = new UC_Tip_ActivationSuccess(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.CancelActivationWarn:
                    {
                        UC_Tip_CommQuestion UC_tip = new UC_Tip_CommQuestion(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.CancelActivationSuccess:
                case TipType.BookCancelSuccess:
                    {
                        UC_Tip_CommSuccess UC_tip = new UC_Tip_CommSuccess(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.BookConfirmWarn:
                    {
                        UC_Tip_BookConfirmWarn UC_tip = new UC_Tip_BookConfirmWarn(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.SetShortWarning:
                    {
                        UC_Tip_SetShortWarning UC_tip = new UC_Tip_SetShortWarning(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.SelectSeatConfinmed:
                    {
                        UC_Tip_SelectSeatConfinmed UC_tip = new UC_Tip_SelectSeatConfinmed(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.WaitSeatCancelWarn:
                    {
                        UC_Tip_WaitSeatCancelWarn UC_tip = new UC_Tip_WaitSeatCancelWarn(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.WaitSeatCancel:
                    {
                        UC_Tip_WaitSeatCancel UC_tip = new UC_Tip_WaitSeatCancel(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240; ;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.SelectBookingSeatWarn:
                    {
                        UC_Tip_SelectBookingSeatWarn UC_tip = new UC_Tip_SelectBookingSeatWarn(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.PrintConfIrm:
                    {
                        UC_Tip_PrintConfirm UC_tip = new UC_Tip_PrintConfirm(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case TipType.ContinueWithBookLog:
                    {
                        UC_Tip_ContinueWithBookLog UC_tip = new UC_Tip_ContinueWithBookLog(ViewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                default: break;
            }


        }
        /// <summary>
        /// 取消关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OperateResule = HandleResult.Failed;
            WinClosing();
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OperateResule = HandleResult.Successed;
            WinClosing();
        }
        /// <summary>
        /// 输入选号模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (txt_cardno.Text != "")
            {
                ViewModel.OperateResule = HandleResult.Successed;
                ViewModel.CardNo = txt_cardno.Text;
                WinClosing();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OperateResule = HandleResult.Failed;
            WinClosing();
        }

    }
}
