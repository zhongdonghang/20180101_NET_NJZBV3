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
using AdvertManageTools.Code;
using AdvertManage.BLL;
using System.IO;

namespace AdvertManageTools.EditPage
{
    /// <summary>
    /// DeviceImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DeviceImageWindow : Window
    {
        public DeviceImageWindow()
        {
            InitializeComponent();
        }
        DeviceInfoViewModel _Device = new DeviceInfoViewModel();

        public DeviceInfoViewModel Device
        {
            get { return _Device; }
            set { _Device = value; }
        }
        /// <summary>
        /// 载入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(_Device.Caputrepath))
            {
                label.Visibility = Visibility.Collapsed;
                try
                {
                    string appdri = AppDomain.CurrentDomain.BaseDirectory;
                    DirectoryInfo dir = new DirectoryInfo(appdri + "\\" + "DeviceCaputre");
                    if (!dir.Exists)
                    {
                        dir.Create();
                    }
                    FileOperate fo = new FileOperate();
                    if (fo.FileDownLoad(dir.FullName + "\\" + _Device.Caputrepath, _Device.Caputrepath, SeatManage.EnumType.SeatManageSubsystem.Caputre))
                    {
                        BinaryReader binReader = new BinaryReader(File.Open(dir.FullName + "\\" + _Device.Caputrepath, FileMode.Open));
                        FileInfo fileInfo = new FileInfo(dir.FullName + "\\" + _Device.Caputrepath);
                        byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                        binReader.Close();

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(bytes);
                        bitmap.EndInit();

                        deviceimage.Source = bitmap;
                        
                    }
                    else
                    {
                        label.Content = "图片载入失败！";
                        label.Visibility = Visibility.Visible;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    label.Content = "图片载入失败！";
                    label.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
