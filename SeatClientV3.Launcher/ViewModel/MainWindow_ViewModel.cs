using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows;
using System.Xml;
using SeatManage.Bll;
using SeatManage.ClassModel;
using SeatManage.SeatManageComm;


namespace SeatClientV3.Launcher.ViewModel
{
    public delegate void EventHandlerMessage(string message);
    public class MainWindow_ViewModel : INotifyPropertyChanged
    {
        public event EventHandlerMessage StratUpFinish;
        public MainWindow_ViewModel()
        {
            WindowHeight = Screen.PrimaryScreen.Bounds.Height;
            WindowWidth = Screen.PrimaryScreen.Bounds.Width;
            WindowLeft = 0;
            WindowTop = 0;
        }
        private string _mediaCLient = ConfigurationManager.AppSettings["MediaCLient"];
        private string _seatClient = ConfigurationManager.AppSettings["SeatClient"];
        private string _schoolNo = ConfigurationManager.AppSettings["SchoolNo"];
        private string _clientNo = ConfigurationManager.AppSettings["ClientNo"];
        private string _isCheckInternet = ConfigurationManager.AppSettings["IsCheckInternet"];
        private string _sendMessageInterval = ConfigurationManager.AppSettings["SendMessageInterval"];
        private bool _screenShots = ConfigurationManager.AppSettings["ScreenShots"] == "1";
        private string _showMessage;
        /// <summary>
        /// 显示的消息
        /// </summary>
        public string ShowMessage
        {
            get { return _showMessage; }
            set { _showMessage = value; OnPropertyChanged("ShowMessage"); }
        }
        private double _WindowHeight = 0;
        /// <summary>
        /// 窗体高度
        /// </summary>
        public double WindowHeight
        {
            get { return _WindowHeight; }
            set { _WindowHeight = value; OnPropertyChanged("WindowHeight"); }
        }

        private double _WindowWidth = 0;
        /// <summary>
        /// 窗体宽度
        /// </summary>
        public double WindowWidth
        {
            get { return _WindowWidth; }
            set { _WindowWidth = value; OnPropertyChanged("WindowWidth"); }
        }

        private double _WindowLeft = 0;
        /// <summary>
        /// 窗体左上角X轴
        /// </summary>
        public double WindowLeft
        {
            get { return _WindowLeft; }
            set { _WindowLeft = value; OnPropertyChanged("WindowLeft"); }
        }

        private double _WindowTop = 0;
        /// <summary>
        /// 窗体左上角Y轴
        /// </summary>
        public double WindowTop
        {
            get { return _WindowTop; }
            set { _WindowTop = value; OnPropertyChanged("WindowTop"); }
        }

        public bool IsDispose
        {
            get { return _isDispose; }
            set { _isDispose = value; }
        }

        public string MediaCLient
        {
            get { return _mediaCLient; }
            set { _mediaCLient = value; }
        }

        public string SeatClient
        {
            get { return _seatClient; }
            set { _seatClient = value; }
        }

        /// <summary>
        /// 是否停止
        /// </summary>
        private bool _isDispose = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        Thread myThread;
        /// <summary>
        /// 启动
        /// </summary>
        public void Run()
        {
            myThread = new Thread(initThread);
            myThread.Start();
        }

        /// <summary>
        /// 线程启动
        /// </summary>
        private void initThread()
        {
            while (!CheckNetWork())//如果能成功获取时间，说明网络正常。
            {
                Thread.Sleep(3000);//不能成功获取时间，则线程暂停3s，重新再获取
                if (_isDispose)
                {
                    myThread.Abort();
                }
            }
            if (DownloadUpdateFiles() && StratUpFinish != null)
            {
                StratUpFinish("");
                ProcessT = new System.Timers.Timer(10000);
                ProcessT.Elapsed += T_Elapsed;
                ProcessT.Start();
                ImageT = new System.Timers.Timer(int.Parse(_sendMessageInterval));
                ImageT.Elapsed += ImageT_Elapsed;
                ImageT.Start();
            }
        }

        void ImageT_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ImageT.Stop();
                ScreenShots();
            }
            catch (Exception ex)
            {
                WriteLog.Write("截图发生错误：" + ex.Message);
            }
            finally
            {
                ImageT.Start();
            }
        }

        void T_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                ProcessT.Stop();
                StartProcess();
            }
            catch (Exception ex)
            {
                WriteLog.Write("查询进程发生错误：" + ex.Message);
            }
            finally
            {
                ProcessT.Start();
            }
        }

        /// <summary>
        /// 通过获取服务器时间检查网络是否通畅，获取成功，返回true，否则返回false。
        /// </summary>
        /// <returns></returns>
        private bool CheckNetWork()
        {
            ShowMessage = "正在连接远程服务……";
            if (_isCheckInternet == "1")
            {
                if (!CheckInternet.CheckLocal())
                {
                    ShowMessage = "连接网络失败，请检查网卡状态或网线是否接好！";
                    return false;
                }
                if (ConfigurationManager.ConnectionStrings["EndpointAddress"] != null)
                {
                    string endpointAddress = AESAlgorithm.AESDecrypt(ConfigurationManager.ConnectionStrings["EndpointAddress"].ConnectionString);
                    //net.tcp://192.168.1.100:8201/
                    string straddress = endpointAddress.Replace("net.tcp://", "");
                    straddress = straddress.Substring(0, straddress.Length - 1);
                    if (!CheckInternet.CheckPort(straddress.Split(':')[0], straddress.Split(':')[1]))
                    {
                        ShowMessage = "服务器连接失败，请检查服务器设置或网络配置！";
                        return false;
                    }
                }
                else
                {
                    ShowMessage = "获取服务器地址失败，请检查终端配置！";
                    return false;
                }
            }
            try
            {
                DateTime dt = ServiceDateTime.Now;
            }
            catch (Exception ex)
            {

                WriteLog.Write("连接数据传输服务失败：" + ex.Message);
                ShowMessage = "连接远程服务遇到错误，请检查服务器服务是否启动。";
                return false;
            }
            return true;
        }

        private FileOperate DownloadFile = new FileOperate();
        private List<StartProgram_ViewModel> programs;
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="subsystemType"></param>
        public bool DownloadUpdateFiles()
        {
            List<string> startPrograms = new List<string>();
            XmlDocument doc = new XmlDocument();
            string path = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "ClientLauncher.exe.config");
            if (!File.Exists(path))
            {
                ShowMessage = "找不到配置文件请查看配置文件是否存在。";
                return false;
            }
            doc.Load(path);
            XmlNodeList nodes = doc.SelectNodes("//configuration/appSettings/add");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["key"] != null && node.Attributes["key"].Value == "StartUpProgram")
                {
                    startPrograms.Add(node.Attributes["value"].Value);
                }
            }
            if (startPrograms.Count < 1)
            {
                ShowMessage = "找不到配置的启动项，请检查配置文件是否正确。";
                return false;
            }
            programs = new List<StartProgram_ViewModel>();
            foreach (string s in startPrograms)
            {
                StartProgram_ViewModel p = StartProgram_ViewModel.Parse(s);
                if (p == null)
                {
                    ShowMessage = "配置的启动项查找失败，请检查配置文件是否正确。";
                    return false;
                }
                programs.Add(p);
            }
            foreach (StartProgram_ViewModel vm in programs)
            {
                if (vm.SubsystemType == SeatManage.EnumType.SeatManageSubsystem.None)
                {
                    continue;
                }
                string savedri = AppDomain.CurrentDomain.BaseDirectory + vm.DrictortyPath;
                if (!Directory.Exists(savedri))
                {
                    ShowMessage = "文件路径不存在，请检查配置。";
                    return false;
                }
                FileReadOnly.RemovingReadOnly(savedri);
                ShowMessage = "查找文件更新。";
                FileUpdateInfo serviceUpateInfo = FileTransportBll.GetUpdateInfo(vm.SubsystemType);
                if (serviceUpateInfo == null)
                {
                    continue;
                }
                ShowMessage = "开始更新文件。";
                List<FileSimpleInfo> isUpdateFiles = serviceUpateInfo.BuildSystemFileSilmpleList();
                foreach (FileSimpleInfo Fsi in isUpdateFiles)
                {
                    string sysDirectory = string.Format(@"{0}\\{1}", savedri, Fsi.Name);
                    if (!DownloadFile.FileDownLoad(sysDirectory, Fsi.Name, vm.SubsystemType))
                    {
                        break;
                    }
                }
            }
            foreach (StartProgram_ViewModel vm in programs)
            {
                string filepath = AppDomain.CurrentDomain.BaseDirectory + vm.DrictortyPath + "\\" + vm.StartProgramClient;
                if (!File.Exists(filepath))
                {
                    ShowMessage = "启动文件不存在，请检查配置。";
                    return false;
                }
                ShowMessage = "程序启动。";
                System.Diagnostics.Process.Start(filepath);
            }
            return true;
        }

        public System.Timers.Timer ProcessT;
        /// <summary>
        /// 启动进程
        /// </summary>
        public void StartProcess()
        {
            System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
            foreach (StartProgram_ViewModel vm in programs)
            {
                if (!processList.Any(u => u.ProcessName == vm.ProcessName))
                {
                    System.Diagnostics.Process.Start(AppDomain.CurrentDomain.BaseDirectory + vm.DrictortyPath + "\\" + vm.StartProgramClient);
                }
            }
        }
        public System.Timers.Timer ImageT;
        private string imageSavePath = AppDomain.CurrentDomain.BaseDirectory + "ScreenShots\\";
        /// <summary>
        /// 截图
        /// </summary>
        public void ScreenShots()
        {
            if (!_screenShots)
            {
                return;
            }
            if (!Directory.Exists(imageSavePath))
            {
                Directory.CreateDirectory(imageSavePath);
            }
            WPFMessage.MessageHelper.SendMessage(MediaCLient, SeatManage.EnumType.SendClentMessageType.ScreenShots, imageSavePath + "MeadiaClient.jpg");
            WPFMessage.MessageHelper.SendMessage(SeatClient, SeatManage.EnumType.SendClentMessageType.ScreenShots, imageSavePath + "SeatClient.jpg");
            Thread.Sleep(5000);
            if (!File.Exists(imageSavePath + "MeadiaClient.jpg") || !File.Exists(imageSavePath + "SeatClient.jpg"))
            {
                WriteLog.Write("获取截图失败！");
                return;
            }
            try
            {
                Bitmap image = new Bitmap(1080, 1920);
                Graphics bg = Graphics.FromImage(image);
                Image bgmc = Bitmap.FromFile(imageSavePath + "MeadiaClient.jpg");
                bg.DrawImage(bgmc, 0, 0, 1080, 920);
                Image scmc = Bitmap.FromFile(imageSavePath + "SeatClient.jpg");
                bg.DrawImage(scmc, 0, 921, 1080, 1000);
                image.Save(imageSavePath + _schoolNo + _clientNo + ".jpg");

                FileOperate fileOperate = new FileOperate();
                fileOperate.UpdateFile(imageSavePath + _schoolNo + _clientNo + ".jpg", string.Format("{0}{1}.jpg", _schoolNo, _clientNo), SeatManage.EnumType.SeatManageSubsystem.Caputre);
                //更新数据库状态
                TerminalInfoV2 terminal = TerminalOperatorService.GetTeminalSetting(_clientNo);
                terminal.ScreenshotPath = string.Format("{0}{1}.jpg", _schoolNo, _clientNo);
                terminal.StatusUpdateTime = ServiceDateTime.Now;
                TerminalOperatorService.UpdateTeminalSetting(terminal);
            }
            catch (Exception ex)
            {
                WriteLog.Write("发送设备状态失败：" + ex.Message);
            }
        }
    }
}
