using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SeatClientV3.OperateResult;
using SeatManage.EnumType;
using SeatManage.SeatManageComm;

namespace SeatClientV3
{
    /// <summary>
    /// BindWeiCharWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BindWeiCharWindow : Window
    {
        public BindWeiCharWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.CountDown = new FormCloseCountdown(60);
            viewModel.CountDown.EventCountdown += CountDown_EventCountdown;
            StartRead();
        }

        void CountDown_EventCountdown(object sender, EventArgs e)
        {
            if (viewModel.CountDown.CountdownSceonds <= 0)
            {
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
            viewModel.CountDown.Stop();
            viewModel.CountDown.EventCountdown -= CountDown_EventCountdown;
            StopRead();
            this.Close();
        }
        private ViewModel.BindWeiCharWindow_ViewModel viewModel = new ViewModel.BindWeiCharWindow_ViewModel();


        private void StopRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.Stop();
                viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
            }
        }
        private void StartRead()
        {
            if (viewModel.ClientObject.ObjCardReader != null)
            {
                viewModel.ClientObject.ObjCardReader.CardNoGeted += ObjCardReader_CardNoGeted;
                viewModel.ClientObject.ObjCardReader.Start();
            }
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        private void ObjCardReader_CardNoGeted(object sender, SeatManage.ISystemTerminal.IPOS.CardEventArgs e)
        {
            StopRead();
            if (!string.IsNullOrEmpty(e.CardNo))
            {
                Dispatcher.Invoke(new Action(() =>
                {
                    viewModel.CardNo = e.CardNo;
                    string AESCode = string.Format("schoolNo={0}&clientNo={1}&cardNo={2}", viewModel.ClientObject.ClientSetting.ClientNo.Substring(0, viewModel.ClientObject.ClientSetting.ClientNo.Length - 2), viewModel.ClientObject.ClientSetting.ClientNo, e.CardNo);
                    Bitmap bitmap = QRCode.GetDimensionalCode(AESAlgorithm.AESEncrypt(AESCode, "SeatManage_WeiCharCode"), 6, 8);
                    IntPtr hBitmap = bitmap.GetHbitmap();
                    BitmapSource bitmapImage = new BitmapImage();

                    try
                    {
                        bitmapImage = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                    }
                    finally
                    {
                        DeleteObject(hBitmap);
                    }
                    imgCode.Fill = new ImageBrush(bitmapImage);

                }));
            }
            else
            {
                SeatManage.SeatManageComm.WriteLog.Write("读卡出现错误：" + e.ErrorInfo);
            }
            //System.Threading.Thread.Sleep(2000);
            StartRead();
        }
        ///// <summary>
        ///// 上一步
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btn_LastStep_Click(object sender, RoutedEventArgs e)
        //{
        //    g_CardRead.Visibility = System.Windows.Visibility.Collapsed;
        //    g_info.Visibility = System.Windows.Visibility.Visible;
        //    if (viewModel.ClientObject.ObjCardReader != null)
        //    {
        //        viewModel.ClientObject.ObjCardReader.Stop();
        //        viewModel.ClientObject.ObjCardReader.CardNoGeted -= ObjCardReader_CardNoGeted;
        //    }
        //}

        private void btn_Close_Click(object sender, RoutedEventArgs e)
        {
            WinClosing();
        }

        private void btnRead_Click(object sender, RoutedEventArgs e)
        {
            if (txt_cardno.Text != "")
            {
                string AESCode = string.Format("schoolNo={0}&clientNo={1}&cardNo={2}", viewModel.ClientObject.ClientSetting.ClientNo.Substring(0, viewModel.ClientObject.ClientSetting.ClientNo.Length - 2), viewModel.ClientObject.ClientSetting.ClientNo, txt_cardno.Text);
                Bitmap bitmap = QRCode.GetDimensionalCode(AESAlgorithm.AESEncrypt(AESCode, "SeatManage_WeiCharCode"), 6, 8);
                IntPtr hBitmap = bitmap.GetHbitmap();
                BitmapSource bitmapImage = new BitmapImage();

                try
                {
                    bitmapImage = Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
                imgCode.Fill = new ImageBrush(bitmapImage);
            }
        }


    }
}
