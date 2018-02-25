using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskScheduler;
using System.IO;
using Microsoft.Win32;
using System.Management;
using System.Runtime.InteropServices;

namespace SeatManage.SeatClient.Config.Code
{
    public class DeviceSetting
    {
        public static bool CreateShutDown(ShutDownConfig config)
        {
            try
            {
                //创建任务计划类
                ScheduledTasks st = new ScheduledTasks();
                string[] taskNames = st.GetTaskNames();
                //删除原有计划
                foreach (string name in taskNames)
                {
                    if (name == "AutoShutDown.job")
                    {
                        st.DeleteTask("AutoShutDown.job");
                        break;
                    }
                }
                //读取路径
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                StringBuilder shutdownbat = new StringBuilder();
                string path = fileDircetoryPath + "gjjh.bat";
                //判断是否启用
                if (config.IsUsed)
                {
                    //创建关机批处理
                    shutdownbat.AppendFormat("at {0}:{1} shutdown -s -t {2}", config.ShutDownHour, config.ShutDownMin, config.ShutDownWaitSec);
                    if (!File.Exists(path))
                    {
                        FileStream fs = File.Create(path);
                        fs.Close();
                    }
                    StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
                    sw.Write(shutdownbat);
                    sw.Flush();
                    sw.Close();
                    //创建任务计划
                    Task task = st.CreateTask("AutoShutDown");
                    DailyTrigger dt = new DailyTrigger(short.Parse(config.ShutDownHour), short.Parse((int.Parse(config.ShutDownMin) - 10).ToString()));
                    task.Triggers.Add(dt);
                    task.SetAccountInformation(Environment.UserName, (string)null);
                    task.Flags = TaskFlags.RunOnlyIfLoggedOn | TaskFlags.SystemRequired;
                    task.ApplicationName = fileDircetoryPath + "gjjh.bat";
                    task.Comment = "触摸屏终端自动关机";
                    task.Save();
                    task.Close();
                }
                else if (File.Exists(path))
                {
                    //删除批处理
                    File.Delete(path);
                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("添加关机计划失败！" + ex.Message);
                return false;
            }

        }
        public static bool GetShotDownSetting(ref ShutDownConfig config)
        {
            try
            {
                //创建任务计划类
                ScheduledTasks st = new ScheduledTasks();
                string[] taskNames = st.GetTaskNames();
                config.IsUsed = false;
                foreach (string name in taskNames)
                {
                    if (name == "AutoShutDown.job")
                    {
                        config.IsUsed = true;
                        break;
                    }
                }
                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory; StringBuilder shutdownbat = new StringBuilder();
                string path = fileDircetoryPath + "gjjh.bat";
                if (File.Exists(path))
                {
                    StreamReader sr = new StreamReader(path);
                    string str = sr.ReadLine();
                    sr.Close();
                    config.ShutDownHour = str.Substring(3, str.IndexOf(':') - 3);
                    config.ShutDownMin = str.Substring(str.IndexOf(':') + 1, 2).Trim();
                    config.ShutDownWaitSec = str.Substring(str.LastIndexOf(' ') + 1);
                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("获取关机计划失败！" + ex.Message);
                return false;
            }
        }
        public static bool GetDeviceSetting(ref DeviceSettingConfig config)
        {
            RegistryKey myRKCN = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Services\\Tcpip\\Parameters");
            foreach (string myCpName in myRKCN.GetValueNames())
            {
                if (myCpName == "NV Hostname")
                {
                    config.PCName = myRKCN.GetValue(myCpName).ToString();
                    break;
                }
            }
            ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
            ManagementObjectCollection nics = mc.GetInstances();
            foreach (ManagementObject nic in nics)
            {
                if (!(bool)nic["IPEnabled"])
                    continue;
                if (nic["IPAddress"] != null)
                {
                    config.IP = (nic["IPAddress"] as String[])[0];
                }
                if (nic["IPSubnet"] != null)
                {
                    config.Mask = (nic["IPSubnet"] as String[])[0];
                }
                if (nic["DefaultIPGateway"] != null)
                {
                    config.Gateway = (nic["DefaultIPGateway"] as String[])[0];
                }
                if (nic["DNSServerSearchOrder"] != null)
                {
                    config.DNS = (nic["DNSServerSearchOrder"] as String[])[0];
                }
                break;
            }
            if (config.IP == "" || config.IP == "0.0.0.0")
            {
                config.IsStaticIP = false;
            }
            return true;
        }
        public static bool SaveDeviceSetting(DeviceSettingConfig config)
        {
            try
            {

                string fileDircetoryPath = AppDomain.CurrentDomain.BaseDirectory;
                StringBuilder shutdownbat = new StringBuilder();
                string path = fileDircetoryPath + "DeviceSetting.bat";
                shutdownbat.AppendLine("rem 修改用户密码");
                shutdownbat.AppendLine("net user Administrator jsddgx");
                shutdownbat.AppendLine("rem 设置自动登录");
                shutdownbat.AppendLine("reg add \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v DefaultUserName /t reg_sz /d Administrator /f");
                shutdownbat.AppendLine("reg add  \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v DefaultPassword /t reg_sz /d jsddgx /f");
                shutdownbat.AppendLine("reg add  \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\" /v AutoAdminLogon /t reg_sz /d 1 /f");
                shutdownbat.AppendLine("rem 结束未响应程序");
                shutdownbat.AppendLine("reg add \"HKEY_USERS\\.DEFAULT\\Control Panel\\Desktop\\\" /v AutoEndTasks /t reg_sz /d 1 /f ");
                shutdownbat.AppendLine("reg add \"HKEY_CURRENT_USER\\Control Panel\\Desktop\" /v AutoEndTasks /t reg_sz /d 1 /f");
                shutdownbat.AppendLine("rem 清空屏幕信息......");
                shutdownbat.AppendLine("cls");
                shutdownbat.AppendLine("rem 思路是：直接删除气球键，最后添加该键值");
                shutdownbat.AppendLine("echo 删除气球注册键......");
                shutdownbat.AppendLine("reg delete \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"EnableBalloonTips\" /f");
                shutdownbat.AppendLine("echo 删除气球注册键成功......");
                shutdownbat.AppendLine("echo 添加气球注册键及键值......");
                shutdownbat.AppendLine("reg add \"HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Advanced\" /v \"EnableBalloonTips\" /t REG_DWORD /d 00000000 /f");
                shutdownbat.AppendLine("echo 添加气球注册键成功......");
                shutdownbat.AppendLine("echo 去除气球提示成功......");
                shutdownbat.AppendLine("echo 开启远程桌面");
                shutdownbat.AppendLine("reg add \"HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Control\\Terminal Server\"  /v fDenyTSConnections /t REG_DWORD /d 0  /f");
                if (!File.Exists(path))
                {
                    FileStream fs = File.Create(path);
                    fs.Close();
                }
                StreamWriter sw = new StreamWriter(path, false, Encoding.GetEncoding("GB2312"));
                sw.Write(shutdownbat);
                sw.Flush();
                sw.Close();
                System.Diagnostics.Process.Start(fileDircetoryPath + "DeviceSetting.bat");

                IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
                IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup) + "\\" + "终端启动程序.lnk");
                shortcut.TargetPath = fileDircetoryPath + "终端启动程序.exe";
                shortcut.WindowStyle = 1;
                shortcut.Save();

                RegistryKey myRKCN = Registry.LocalMachine.OpenSubKey("SYSTEM\\ControlSet001\\Services\\Tcpip\\Parameters", true);
                foreach (string site in myRKCN.GetValueNames())
                {
                    if (site == "NV Hostname")
                    {
                        myRKCN.DeleteValue(site, false);
                        myRKCN.SetValue("NV Hostname", config.PCName);
                        break;
                    }
                }
                ManagementBaseObject inPar = null;
                ManagementBaseObject outPar = null;
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (!(bool)mo["IPEnabled"])
                        continue;
                    if (config.IsStaticIP)
                    {
                        //设置ip地址和子网掩码 
                        inPar = mo.GetMethodParameters("EnableStatic");
                        inPar["IPAddress"] = new string[] { config.IP };
                        inPar["SubnetMask"] = new string[] { config.Mask };
                        outPar = mo.InvokeMethod("EnableStatic", inPar, null);

                        //设置网关地址 
                        inPar = mo.GetMethodParameters("SetGateways");
                        inPar["DefaultIPGateway"] = new string[] { config.Gateway };
                        outPar = mo.InvokeMethod("SetGateways", inPar, null);

                        //设置DNS 
                        inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        inPar["DNSServerSearchOrder"] = new string[] { config.DNS };
                        outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                        break;
                    }
                    else
                    {
                        inPar = mo.GetMethodParameters("EnableDHCP");
                        outPar = mo.InvokeMethod("EnableDHCP", inPar, null);
                        inPar = mo.GetMethodParameters("SetDNSServerSearchOrder");
                        inPar["DNSServerSearchOrder"] = null;
                        outPar = mo.InvokeMethod("SetDNSServerSearchOrder", inPar, null);
                        break;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("保存终端设置失败！" + ex.Message);
                return false;
            }
        }
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        public struct APPBARDATA
        {
            public int cbSize;
            public int hwnd;
            public int uCallbackMessage;
            public int uEdge;
            public RECT rc;
            public int lParam;
        }

        public const int ABS_ALWAYSONTOP = 0x002;
        public const int ABS_AUTOHIDE = 0x001;
        public const int ABS_BOTH = 0x003;
        public const int ABM_ACTIVATE = 0x006;
        public const int ABM_GETSTATE = 0x004;
        public const int ABM_GETTASKBARPOS = 0x005;
        public const int ABM_NEW = 0x000;
        public const int ABM_QUERYPOS = 0x002;
        public const int ABM_SETAUTOHIDEBAR = 0x008;
        public const int ABM_SETSTATE = 0x00A;

        /// 
        /// 向系统任务栏发送消息
        /// 
        [DllImport("shell32.dll")]
        public static extern int SHAppBarMessage(int dwmsg, ref APPBARDATA app);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);

        /// 
        /// 设置系统任务栏是否自动隐藏
        /// 
        ///

        //True 设置为自动隐藏，False 取消自动隐藏
        public static bool SetAppBarAutoDisplay(bool IsAuto)
        {
            try
            {
                APPBARDATA abd = new APPBARDATA();
                abd.hwnd = FindWindow("Shell_TrayWnd", "");
                //abd.lParam = ABS_ALWAYSONTOP Or ABS_AUTOHIDE   '自动隐藏,且位于窗口前
                //abd.lParam = ABS_ALWAYSONTOP                   '不自动隐藏,且位于窗口前
                //abd.lParam = ABS_AUTOHIDE                       '自动隐藏,且不位于窗口前
                if (IsAuto)
                {
                    abd.lParam = ABS_AUTOHIDE;
                    SHAppBarMessage(ABM_SETSTATE, ref abd);
                }
                else
                {
                    abd.lParam = ABS_ALWAYSONTOP;
                    SHAppBarMessage(ABM_SETSTATE, ref abd);
                }
                return true;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("隐藏任务栏失败！" + ex.Message);
                return false;
            }
        }
        
    }
    public class DeviceSettingConfig
    {
        private bool _IsStaticIP = true;
        /// <summary>
        /// 是否启用静态IP
        /// </summary>
        public bool IsStaticIP
        {
            get { return _IsStaticIP; }
            set { _IsStaticIP = value; }
        }
        private string _IP = "";
        /// <summary>
        /// ip
        /// </summary>
        public string IP
        {
            get { return _IP; }
            set { _IP = value; }
        }
        private string _Mask = "";
        /// <summary>
        /// 子网掩码
        /// </summary>
        public string Mask
        {
            get { return _Mask; }
            set { _Mask = value; }
        }
        private string _Gateway = "";
        /// <summary>
        /// 网关
        /// </summary>
        public string Gateway
        {
            get { return _Gateway; }
            set { _Gateway = value; }
        }
        private string _DNS = "";
        /// <summary>
        /// dns
        /// </summary>
        public string DNS
        {
            get { return _DNS; }
            set { _DNS = value; }
        }
        private string _PCName = "";
        /// <summary>
        /// 主机名
        /// </summary>
        public string PCName
        {
            get { return _PCName; }
            set { _PCName = value; }
        }

    }
    public class ShutDownConfig
    {
        private string _ShutDownHour = "10";
        /// <summary>
        /// 关机小时
        /// </summary>
        public string ShutDownHour
        {
            get { return _ShutDownHour; }
            set { _ShutDownHour = value; }
        }
        private string _ShutDownMin = "00";
        /// <summary>
        /// 关机分钟
        /// </summary>
        public string ShutDownMin
        {
            get { return _ShutDownMin; }
            set { _ShutDownMin = value; }
        }
        private string _ShutDownWaitSec = "10";
        /// <summary>
        /// 延迟秒
        /// </summary>
        public string ShutDownWaitSec
        {
            get { return _ShutDownWaitSec; }
            set { _ShutDownWaitSec = value; }
        }
        private bool _IsUsed = true;
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsUsed
        {
            get { return _IsUsed; }
            set { _IsUsed = value; }
        }
    }
}
