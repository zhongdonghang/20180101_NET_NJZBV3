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
using SeatManage.SeatManageComm;

namespace SeatClientV3
{
    /// <summary>
    /// WeiCharOperationQRWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WeiCharOperationQRWindow : Window
    {
        public WeiCharOperationQRWindow()
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private ViewModel.WeiCharOperationQRWindow_ViewModel viewModel = new ViewModel.WeiCharOperationQRWindow_ViewModel();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            viewModel.CodeChange += viewModel_CodeChange;
            viewModel.CheckCodeRun();
        }
        /// <summary>
        /// 二维码显示
        /// </summary>
        void viewModel_CodeChange()
        {
            Dispatcher.Invoke(new Action(() =>
            {
                string url = viewModel.ClientObject.CodeUrl + "?param=";
                string AESCode = string.Format("schoolNo={0}&clientNo={1}&codeTime={2}", viewModel.ClientObject.ClientSetting.ClientNo.Substring(0, viewModel.ClientObject.ClientSetting.ClientNo.Length - 2), viewModel.ClientObject.ClientSetting.ClientNo, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                Bitmap bitmap = QRCode.GetDimensionalCode(url + AESAlgorithm.AESEncrypt(AESCode, "SeatManage_WeiCharCode"), 6, 8);
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
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        public void WinChange(int Top)
        {
            //viewModel.ChangeTop(Size);
            this.Top = Top - viewModel.WindowHeight;
        }

        public void WinChange()
        {
            viewModel.ChangeTop((int)viewModel.WindowHeight);
        }

        public void WinReset()
        {
            Top = viewModel.ClientObject.ClientSetting.DeviceSetting.SystemResoultion.WindowSize.Location.Y - viewModel.WindowHeight;
            //viewModel.ResetWin();
        }
    }
}
