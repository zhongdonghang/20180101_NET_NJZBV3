using SeatClientV3.ViewModel;
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

namespace SeatClientV3.FunWindow
{
    /// <summary>
    /// ActivationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ActivationWindow : Window
    {
        public ActivationWindow()
        {
            InitializeComponent();
            viewModel = new ActivationWindow_ViewModel();
            this.DataContext = viewModel;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.Clientobject.ClientSetting.DeviceSetting.WinCountDown.AccessActive);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            if (viewModel.Clientobject.ObjCardReader != null)
            {
                viewModel.Clientobject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                viewModel.Clientobject.ObjCardReader.Start();
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OperateResule = SeatManage.EnumType.HandleResult.None;
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtCardNo.Text != "")
            {
                viewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
                viewModel.CardNo = txtCardNo.Text;
                this.Close();
            }
        }
        public ActivationWindow_ViewModel viewModel;
        /// <summary>
        /// 读卡器读到卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            viewModel.Clientobject.ObjCardReader.Stop();
            if (e.PosResult)
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    viewModel.OperateResule = SeatManage.EnumType.HandleResult.Successed;
                    viewModel.CardNo = e.CardNo;
                    this.Close();
                }));
            }
            else
            {
                SeatManage.SeatManageComm.WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
            }
            System.Threading.Thread.Sleep(2000);
            viewModel.Clientobject.ObjCardReader.Start();
        }
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
            if (viewModel.Clientobject.ObjCardReader != null)
            {
                viewModel.Clientobject.ObjCardReader.Stop();
                viewModel.Clientobject.ObjCardReader.CardNoGeted -= new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
            }
        }
    }
}
