using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SeatClientV3.Code;
using SeatClientV3.OperateResult;
using System.Windows.Media.Animation;

namespace SeatClientV3
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
            ShowSchoolNoticeOrPostCard();
        }

        ViewModel.MainWindow_ViewModel viewModel = new ViewModel.MainWindow_ViewModel();
        OperateResult.SystemObject clientObject;

        private void LogSearchButton_Click(object sender, RoutedEventArgs e)
        {
            //停止读卡注销事件
            StartRead();
            SeatClientV3.FunWindow.LogSearchWindow logWindow = new FunWindow.LogSearchWindow();
            logWindow.ShowDialog();
            //开始读卡注册事件
            StopRead();
        }
        /// <summary>
        /// 开始读卡
        /// </summary>
        private void StartRead()
        {
            if (viewModel.clientObject.ObjCardReader != null)
            {
                viewModel.clientObject.ObjCardReader.Stop();
                viewModel.clientObject.ObjCardReader.CardNoGeted -= new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
            }
        }
        /// <summary>4
        /// 停止读卡
        /// </summary>
        private void StopRead()
        {
            if (viewModel.clientObject.ObjCardReader != null)
            {
                viewModel.clientObject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                viewModel.clientObject.ObjCardReader.Start();
            }
        }
        /// <summary>
        /// 读卡成功操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {

            viewModel.clientObject.ObjCardReader.Stop();
            //操作
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                this.Dispatcher.Invoke(new Action(() =>
                {
                    viewModel.PosCardHandle(e.CardNo);
                }));
            }
            viewModel.clientObject.ObjCardReader.Start();
        }
        /// <summary>
        /// 预约激活按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivationButton_Click(object sender, RoutedEventArgs e)
        {
            //停止读卡注销事件
            StartRead();
            viewModel.ActiveBook();
            //开始读卡注册事件
            StopRead();
        }
        /// <summary>
        /// 测试模式输入座位号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGetNo_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txt_CardNo.Text))
            {
                viewModel.PosCardHandle(txt_CardNo.Text);
            }
        }
        /// <summary>
        /// 判断显示校园通知or刷卡图片
        /// </summary>
        private void ShowSchoolNoticeOrPostCard()
        {
            if (ViewModel.MainWindow_ViewModel.SchoolNotices > 0)
            {
                viewModel.ImgCardPostVisibility = "Collapsed";
                viewModel.UCSchoolNoticeVisibility = "Visible";
            }
            else
            {
                viewModel.ImgCardPostVisibility = "Visible";
                viewModel.UCSchoolNoticeVisibility = "Collapsed";
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            clientObject = OperateResult.SystemObject.GetInstance();
            if (clientObject.ObjCardReader != null)
            {
                clientObject.ObjCardReader.CardNoGeted += new SeatManage.ISystemTerminal.IPOS.EventPosCardNo(ObjCardReader_CardNoGeted);
                clientObject.ObjCardReader.Start();
            }
        }
    }
}
