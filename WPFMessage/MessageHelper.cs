using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace WPFMessage
{
    public class MessageHelper
    {
        public const int WM_COPYDATA = 0x004A;

        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage
        (
        IntPtr hWnd,                   //目标窗体句柄12
        int Msg,                       //WM_COPYDATA
        int wParam,                                             //自定义数值
        ref  CopyDataStruct lParam             //结构体
        );

        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [StructLayout(LayoutKind.Sequential)]
        public struct CopyDataStruct
        {
            public IntPtr dwData;
            public int cbData;//字符串长度

            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;//字符串
        }

        /// <summary>
        /// SendMessage To Window
        /// </summary>
        /// <param name="windowName">window的title，建议加上GUID，不会重复</param>
        /// <param name="strMsg">要发送的字符串</param>
        public static void SendMessage(string windowName, SeatManage.EnumType.SendClentMessageType messageType, string strMsg)
        {

            string sendMsg = ((int)messageType).ToString() + ";" + strMsg;
            IntPtr hwnd = FindWindow(null, windowName);

            if (hwnd != IntPtr.Zero)
            {
                CopyDataStruct cds;

                cds.dwData = IntPtr.Zero;
                cds.lpData = sendMsg;

                //注意：长度为字节数
                cds.cbData = System.Text.Encoding.Default.GetBytes(sendMsg).Length + 1;
                // 消息来源窗体
                int fromWindowHandler = 0;
                SendMessage(hwnd, WM_COPYDATA, fromWindowHandler, ref  cds);

            }

        }
        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="strMsg"></param>
        /// <param name="messageType"></param>
        public static void SendMessage(string windowName, SeatManage.EnumType.SendClentMessageType messageType)
        {

            string sendMsg = ((int)messageType).ToString();

            IntPtr hwnd = FindWindow(null, windowName);

            if (hwnd != IntPtr.Zero)
            {
                CopyDataStruct cds;

                cds.dwData = IntPtr.Zero;
                cds.lpData = sendMsg;

                //注意：长度为字节数
                cds.cbData = System.Text.Encoding.Default.GetBytes(sendMsg).Length + 1;
                // 消息来源窗体
                int fromWindowHandler = 0;
                SendMessage(hwnd, WM_COPYDATA, fromWindowHandler, ref  cds);

            }

        }
        /// <summary>
        /// SendMessage To Window
        /// </summary>
        /// <param name="windowName">window的title，建议加上GUID，不会重复</param>
        /// <param name="strMsg">要发送的字符串</param>
        public static void SendMessageByProcess(string processName, string strMsg)
        {

            if (strMsg == null) return;

            var process = Process.GetProcessesByName(processName);
            if (process.FirstOrDefault() == null) return;
            var hwnd = process.FirstOrDefault().MainWindowHandle;
            if (hwnd == IntPtr.Zero) return;

            if (hwnd != IntPtr.Zero)
            {
                CopyDataStruct cds;

                cds.dwData = IntPtr.Zero;
                cds.lpData = strMsg;

                //注意：长度为字节数
                cds.cbData = System.Text.Encoding.Default.GetBytes(strMsg).Length + 1;
                // 消息来源窗体
                int fromWindowHandler = 0;
                SendMessage(hwnd, WM_COPYDATA, fromWindowHandler, ref  cds);

            }

        }
        public delegate void GetMessageEventHandler(object sender, string message);
        public event GetMessageEventHandler GetMessage;
        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="msg"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <param name="handled"></param>
        /// <returns></returns>
        public IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {

            if (msg == MessageHelper.WM_COPYDATA)
            {
                CopyDataStruct cds = (CopyDataStruct)System.Runtime.InteropServices.Marshal.PtrToStructure(lParam, typeof(CopyDataStruct));
                if (GetMessage != null)
                {
                    GetMessage(this, cds.lpData);
                }
            }
            return hwnd;
        }
    }
}
