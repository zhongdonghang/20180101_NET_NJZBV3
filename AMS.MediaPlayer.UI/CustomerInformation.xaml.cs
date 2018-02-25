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
using AMS.MediaPlayer.UI.Code;
using WpfApplication10;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace AMS.MediaPlayer.UI
{
    /// <summary>
    /// CustomerInformation.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerInformation : Window
    {
        public CustomerInformation()
        {
            InitializeComponent();
            this.DataContext = this;		
        }


        public string PrintBtnImage
        {
            get { return (string)GetValue(PrintBtnImageProperty); }
            set { SetValue(PrintBtnImageProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PrintBtnImage.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PrintBtnImageProperty =
            DependencyProperty.Register("PrintBtnImage", typeof(string), typeof(CustomerInformation), new UIPropertyMetadata("Image/print.png"));

        
        private SeatManage.ClassModel.AMS_SlipCustomer _SlipInfo = new SeatManage.ClassModel.AMS_SlipCustomer();

        public SeatManage.ClassModel.AMS_SlipCustomer SlipInfo
        {
            get { return _SlipInfo; }
            set { _SlipInfo = value; }
        }

        //打印
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (_SlipInfo.IsPrint)
            {
                if (!string.IsNullOrEmpty(_SlipInfo.SlipTemplate))
                {
                    PrintSlip printer = new PrintSlip(_SlipInfo.SlipTemplate);
                    printer.Print();
                }
                _SlipInfo.PrintAmount++;
                try
                {
                    SeatManage.Bll.AMS_SlipCustomer.AddSlipCustomerPrintCount(_SlipInfo.No);//修改打印次数
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
                this.Close();
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //System.Timers.Timer timer1 = null;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            { 
                if (!_SlipInfo.IsPrint)
                {
                    PrintBtnImage = "Image/noprint.png";
                }
                SeatManage.Bll.AMS_SlipCustomer.AddSlipCustomerLookCount(_SlipInfo.No);
                ShowImage();
                System.Timers.Timer timer1 = null;
                timer1 = new System.Timers.Timer(30000);
                timer1.Elapsed += (s, ea) => timer1_Elapsed(s, ea);
                timer1.AutoReset = true;
                timer1.Start();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
            }
        }
        void timer1_Elapsed(object s, System.Timers.ElapsedEventArgs ea)
        {
            //timer1.Dispose();//每次都要释放之前新建的time对象
            this.Dispatcher.Invoke(new Action(() =>
            {
                try
                {
                    this.Close();
                }
                catch (Exception ex)
                {
                    SeatManage.SeatManageComm.WriteLog.Write(ex.Message.TrimEnd('\0'));
                }
            }));

        }

        private void ShowImage()
        {
            //二进制流转换成图片，放入image控件中
            try
            {
                string imageName = string.Format("{0}{1}", AMS.MediaPlayer.Code.PlayerSetting.SysPath+"\\SlipImage\\", _SlipInfo.ImageName);
                BinaryReader binReader = new BinaryReader(File.Open(imageName, FileMode.Open));
                FileInfo fileInfo = new FileInfo(imageName);
                byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
                binReader.Close();

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = new MemoryStream(bytes);
                bitmap.EndInit();

                if (bitmap.Height < 880 && bitmap.Width < 880)
                {
                    image1.Stretch = Stretch.None;
                }
                else
                {
                    image1.Stretch = Stretch.Uniform;
                }

                image1.Source = bitmap;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
