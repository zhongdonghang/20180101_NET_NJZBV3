using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ClientLeaveV2.MyUserControl;

namespace ClientLeaveV2
{
    /// <summary>
    /// PopupWindow.xaml 的交互逻辑
    /// </summary>
    public partial class PopupWindow : Window
    {
        public PopupWindow(SeatManage.EnumType.TipType ucType)
        {
            InitializeComponent();
            viewModel = new ViewModel.PopupWindow_ViewModel(ucType);
            this.DataContext = viewModel;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(3);
            viewModel.CountDown.EventCountdown += new EventHandler(CountDown_EventCountdown);
            ShowUC_Type();
            if (viewModel.PopAd == null)
            {
                this.Height = 335;
                image_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down.Visibility = System.Windows.Visibility.Collapsed;
                rec_down_co.Visibility = System.Windows.Visibility.Visible;
            }

        }

        public ViewModel.PopupWindow_ViewModel viewModel;
        /// <summary>
        /// 触发窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    this.Close();
                }));
            }
        }
        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= new EventHandler(CountDown_EventCountdown);
        }

        /// <summary>
        /// 显示控件
        /// </summary>
        private void ShowUC_Type()
        {
            switch (viewModel.MessageType)
            {
                case SeatManage.EnumType.TipType.ContinuedTime:
                    {
                        if (string.IsNullOrEmpty(viewModel.Tip_ViewModel.StartTime))
                        {
                            UC_Tip_ContinueTimeNoAgain UC_tip = new UC_Tip_ContinueTimeNoAgain(viewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            //Canvas.SetLeft(UC_tip, 30);
                            //Canvas.SetTop(UC_tip, 100);
                            UC_Cancas.Children.Add(UC_tip);
                        }
                        else
                        {
                            UC_Tip_ContinueTime UC_tip = new UC_Tip_ContinueTime(viewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            //Canvas.SetLeft(UC_tip, 30);
                            //Canvas.SetTop(UC_tip, 100);
                            UC_Cancas.Children.Add(UC_tip);
                        }
                    } break;
                case SeatManage.EnumType.TipType.AutoContinuedTime:
                    {
                        if (string.IsNullOrEmpty(viewModel.Tip_ViewModel.StartTime))
                        {
                            UC_Tip_ContinueTimeAutoNoAgain UC_tip = new UC_Tip_ContinueTimeAutoNoAgain(viewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            //Canvas.SetLeft(UC_tip, 30);
                            //Canvas.SetTop(UC_tip, 100);
                            UC_Cancas.Children.Add(UC_tip);
                        }
                        else
                        {
                            UC_Tip_ContinueTimeAuto UC_tip = new UC_Tip_ContinueTimeAuto(viewModel.Tip_ViewModel);
                            UC_tip.Width = 740;
                            UC_tip.Height = 240;
                            //Canvas.SetLeft(UC_tip, 30);
                            //Canvas.SetTop(UC_tip, 100);
                            UC_Cancas.Children.Add(UC_tip);
                        }
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatWithSeat:
                case SeatManage.EnumType.TipType.AutoContinuedTimeNoCount:
                case SeatManage.EnumType.TipType.ContinuedTimeNoCount:
                case SeatManage.EnumType.TipType.BeapeatRoomNotExists:
                case SeatManage.EnumType.TipType.BespeatSeatConfirmFild:
                case SeatManage.EnumType.TipType.Exception:
                case SeatManage.EnumType.TipType.ReadingRoomFull:
                case SeatManage.EnumType.TipType.SeatUsing:
                case SeatManage.EnumType.TipType.SeatLocking:
                case SeatManage.EnumType.TipType.SeatStopping:
                case SeatManage.EnumType.TipType.SeatNotExists:
                case SeatManage.EnumType.TipType.SelectSeatFrequent:
                case SeatManage.EnumType.TipType.ShortLeaveSeatOverTime:
                case SeatManage.EnumType.TipType.SelectSeatResult:
                case SeatManage.EnumType.TipType.BookConfirmWarn:
                case SeatManage.EnumType.TipType.WaitSeatCancelWarn:
                case SeatManage.EnumType.TipType.WaitSeatFrequent:
                    {
                        UC_Tip_CommFailed UC_tip = new UC_Tip_CommFailed(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.ContinuedTimeNotTime:
                    {
                        UC_Tip_ContinueTimeNoTime UC_tip = new UC_Tip_ContinueTimeNoTime(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.ContinuedTimeWithout:
                    {
                        UC_Tip_ContinueTimeNoNeed UC_tip = new UC_Tip_ContinueTimeNoNeed(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.BespeatSeatConfirmSuccess:
                    {
                        UC_Tip_SelectSeatResult UC_tip = new UC_Tip_SelectSeatResult(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.ComeToBack:
                    {
                        UC_Tip_ComeBack UC_tip = new UC_Tip_ComeBack(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;

                case SeatManage.EnumType.TipType.Leave:
                    {
                        UC_Tip_Leave UC_tip = new UC_Tip_Leave();
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.ShortLeave:
                    {
                        UC_Tip_ShortLeave UC_tip = new UC_Tip_ShortLeave(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;

                case SeatManage.EnumType.TipType.BookCancelSuccess:
                    {
                        UC_Tip_CommSuccess UC_tip = new UC_Tip_CommSuccess(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;

                case SeatManage.EnumType.TipType.SelectSeatConfinmed:
                    {
                        UC_Tip_SelectSeatConfinmed UC_tip = new UC_Tip_SelectSeatConfinmed(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                case SeatManage.EnumType.TipType.WaitSeatCancel:
                    {
                        UC_Tip_WaitSeatCancel UC_tip = new UC_Tip_WaitSeatCancel(viewModel.Tip_ViewModel);
                        UC_tip.Width = 740;
                        UC_tip.Height = 240;
                        //Canvas.SetLeft(UC_tip, 30);
                        //Canvas.SetTop(UC_tip, 100);
                        UC_Cancas.Children.Add(UC_tip);
                    } break;
                default: break;
            }
            viewModel.CountDown.Start();

        }
        /// <summary>
        /// 取消关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 确认按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ok_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
            this.Close();
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
                viewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
                viewModel.CardNo = txt_cardno.Text;
                this.Close();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
