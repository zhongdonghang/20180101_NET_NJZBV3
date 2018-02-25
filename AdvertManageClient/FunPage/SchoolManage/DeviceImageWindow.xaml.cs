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
using System.IO; 
using AMS.ViewModel;
using AMS.ServiceProxy;

namespace AdvertManageClient.FunPage.SchoolManage
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        DeviceInfo _Device = new DeviceInfo();
        /// <summary>
        /// 设备Model
        /// </summary>
        public DeviceInfo Device
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
            if (!string.IsNullOrEmpty(Device.CaputrePath))
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
                    if (string.IsNullOrEmpty(fo.FileDownLoad(dir.FullName + "\\" + Device.CaputrePath, Device.CaputrePath, SeatManage.EnumType.SeatManageSubsystem.Caputre)))
                    {
                        BinaryReader binReader = new BinaryReader(File.Open(dir.FullName + "\\" + Device.CaputrePath, FileMode.Open));
                        FileInfo fileInfo = new FileInfo(dir.FullName + "\\" + Device.CaputrePath);
                        byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                        binReader.Close();

                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(bytes);
                        bitmap.EndInit();

                        image1.Source = bitmap;

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
            this.WindowState = System.Windows.WindowState.Normal;
            this.WindowStyle = System.Windows.WindowStyle.None;
            this.ResizeMode = System.Windows.ResizeMode.NoResize;
            this.Topmost = true;
            //this.Left = 0.0;
            //this.Top = 0.0;
            //this.Width = System.Windows.SystemParameters.PrimaryScreenWidth;
            //this.Height = System.Windows.SystemParameters.PrimaryScreenHeight;
            //btnclose.Visibility = Visibility.Hidden;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //this.DragMove();
        }
        Point dragStart;
        /// <summary>
        /// 获取鼠标在root的指定位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dragStart = e.GetPosition(root);
           
        }
        /// <summary>
        /// 鼠标滚动放大图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image1_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            var mosePos = e.GetPosition(image1);

            var scale = transform.ScaleX * (e.Delta > 0 ? 1.2 : 1 / 1.2);
            scale = Math.Max(scale, 1);

            transform.ScaleX = scale;
            transform.ScaleY = scale;

            if (scale == 1)        //缩放率为1的时候，复位
            {
                translate.X = 0;
                translate.Y = 0;
            }
            else                //保持鼠标相对图片位置不变
            {
                var newPos = e.GetPosition(image1);

                translate.X += (newPos.X - mosePos.X);
                translate.Y += (newPos.Y - mosePos.Y);
            }

        }

     
        /// <summary>
        /// 鼠标移动拖拽图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Pressed)
            {
                return;
            }

            var current = e.GetPosition(root);

            translate.X += (current.X - dragStart.X) / transform.ScaleX;
            translate.Y += (current.Y - dragStart.Y) / transform.ScaleY;

            dragStart = current;

        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        } 
    }
}
