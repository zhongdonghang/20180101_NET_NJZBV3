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
    /// MessageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MessageWindow : Window
    {
        /// <summary>
        /// viewmodel
        /// </summary>
        public ViewModel.MessageWindow_ViewModel viewModel;
        public MessageWindow(SeatManage.EnumType.MessageType ucType)
        {
            InitializeComponent();
            viewModel = new ViewModel.MessageWindow_ViewModel(ucType);
            viewModel.Tip_ViewModel.WindowClose += ViewModel_WindowClose;
            this.DataContext = viewModel;
            viewModel.CountDown = new OperateResult.FormCloseCountdown(viewModel.Clientobject.ClientSetting.DeviceSetting.WinCountDown.MessageWindow);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            ShowUC_Type();
        }
        /// <summary>
        /// 时间到窗体关闭
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
        /// 窗口关闭时间
        /// </summary>
        /// <param name="e"></param>
        void ViewModel_WindowClose(SeatManage.EnumType.HandleResult e)
        {
            viewModel.OperateResule = e;
            this.Close();
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
            viewModel.Tip_ViewModel.WindowClose -= ViewModel_WindowClose;
        }
        /// <summary>
        /// 显示控件
        /// </summary>
        private void ShowUC_Type()
        {
            object tip_UC = null;
            try
            {
                tip_UC = Activator.CreateInstance(Type.GetType("SeatClientV3.TipUC.UC_Tip_" + viewModel.MessageType.ToString()), new object[] { viewModel.Tip_ViewModel });
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("创建提示框失败：" + ex.Message);
                tip_UC = null;
            }
            if (tip_UC == null)
            {
                tip_UC = new TipUC.UC_Tip_Exception(viewModel.Tip_ViewModel);
            }
            UserControl uc = tip_UC as UserControl;
            uc.Height = 240;
            uc.Width = 580;
            Canvas.SetLeft(uc, 10);
            Canvas.SetTop(uc, 50);
            cav_tip.Children.Add(uc);
            viewModel.CountDown.Start();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.OperateResule = SeatManage.EnumType.HandleResult.None;
            this.Close();
        }
    }
}
