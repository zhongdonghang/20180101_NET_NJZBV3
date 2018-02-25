using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace SeatManage.SeatManageComm
{
    public class CheckInternet
    {

        [DllImport("wininet")]
        public static extern bool InternetGetConnectedState(ref int lpdwFlags, int dwReserved);
        /// <summary>
        /// 检查本地连接
        /// </summary>
        /// <returns></returns>
        public static bool CheckLocal()
        {
            int Flag = 0;
            if (!InternetGetConnectedState(ref Flag, 0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 判断服务器是否联通
        /// </summary>
        /// <param name="serverIP"></param>
        /// <param name="serverPort"></param>
        /// <returns></returns>
        public static bool CheckPort(string serverIP, string serverPort)
        {
            if (serverIP.ToLower() == "localhost")
            {
                serverIP = "127.0.0.1";
            }
            IPAddress ip = IPAddress.Parse(serverIP);
            IPEndPoint point = new IPEndPoint(ip, int.Parse(serverPort));
            try
            {
                TcpClient tcp = new TcpClient();
                tcp.Connect(point);
                return true;
            }
            catch (Exception ex)
            {
                SeatManageComm.WriteLog.Write("连接服务器失败：" + ex.Message);
                return false;
            }

        }
        /// <summary>
        /// ping 目标主机
        /// </summary>
        /// <param name="serverIP"></param>
        public static void PingIp(string serverIP)
        {
            Process p = new Process(); p.StartInfo.FileName = "cmd.exe";//设定程序名
            p.StartInfo.UseShellExecute = false; //关闭Shell的使用
            p.StartInfo.RedirectStandardInput = true;//重定向标准输入
            p.StartInfo.RedirectStandardOutput = true;//重定向标准输出
            p.StartInfo.RedirectStandardError = true;//重定向错误输出
            p.StartInfo.CreateNoWindow = true;//设置不显示窗口
            string pingrst; p.Start(); p.StandardInput.WriteLine("ping " + serverIP+" -t -n 20");
            p.StandardInput.WriteLine("exit");
            string strRst = p.StandardOutput.ReadToEnd();

            //if (strRst.IndexOf("(0% loss)") != -1)
            //{
            //    pingrst = "连接";
            //}
            //else if (strRst.IndexOf("Destination host unreachable.") != -1)
            //{
            //    pingrst = "无法到达目的主机";
            //}
            //else if (strRst.IndexOf("Request timed out.") != -1)
            //{
            //    pingrst = "超时";
            //}
            //else if (strRst.IndexOf("Unknown host") != -1)
            //{
            //    pingrst = "无法解析主机";
            //}
            //else
            //{
            //    pingrst = strRst;
            //}
            p.Close();
            //return pingrst;
        }
    }
}
