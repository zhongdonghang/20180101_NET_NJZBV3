﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;

namespace AMS.WebView.Code
{
    public class KeyHook
    {
        /// <summary>
        /// 键盘消息
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct KeyboardMsg
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        /// <summary>
        /// 条形码
        /// </summary>
        public string BarCode = "";

        //public struct BarCodes
        //{
        //    public int VirtKey;      //虚拟码   
        //    public int ScanCode;     //扫描码   
        //    public string KeyName;   //键名   
        //    public uint AscII;       //AscII   
        //    public char Chr;         //字符   
        //    public string BarCode;   //条码信息   
        //    public bool IsValid;     //条码是否有效  
        //}

        /// <summary>
        /// 钩子实现的委托
        /// </summary>
        /// <param name="code"></param>
        /// <param name="wparam"></param>
        /// <param name="lparam"></param>
        /// <returns></returns>
        public delegate IntPtr HookProc(int code, IntPtr wparam, ref KeyboardMsg lParam);

        /// <summary>
        /// 记录钩子的编号
        /// </summary>
        IntPtr _nextHookPtr; //记录Hook编号

        /// <summary>
        /// 取得当前线程编号的API 
        /// </summary>
        /// <returns></returns>
        [DllImport("kernel32.dll")]
        static extern int GetCurrentThreadId();

        /// <summary>
        /// 取消Hook的API
        /// </summary>
        /// <param name="handle">钩子的标识</param>
        [DllImport("User32.dll")]
        internal extern static void UnhookWindowsHookEx(IntPtr handle);

        /// <summary>
        /// 设置Hook的API
        /// </summary>
        /// <param name="idHook">钩子类型</param>
        /// <param name="lpfn">指向“钩子”过程的指针</param>
        /// <param name="hinstance">“钩子”过程所在模块的句柄</param>
        /// <param name="threadID">“钩子”相关线程的标识</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        internal extern static IntPtr SetWindowsHookEx(int idHook, [MarshalAs(UnmanagedType.FunctionPtr)] HookProc lpfn, IntPtr hinstance, int threadID);

        /// <summary>
        /// 获取应用程序或动态链接库的模块句柄 
        /// </summary>
        /// <param name="lpModuleName">模块名称</param>
        /// <returns></returns>
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        /// <summary>
        /// 取得下一个Hook的API
        /// </summary>
        /// <param name="handle">当前”钩子”的句柄，由SetWindowsHookEx()函数返回</param>
        /// <param name="code">传给”钩子”过程的事件代码</param>
        /// <param name="wparam">传给”钩子”过程的wParam值，其具体含义与”钩子”类型有关</param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        private extern static IntPtr CallNextHookEx(IntPtr handle, int code, IntPtr wparam, ref KeyboardMsg lParam); //取得下一个Hook的API  
        private HookProc myhookProc;
        public void HookStart()
        {
            if (_nextHookPtr != IntPtr.Zero) //已经勾过了
            {
                return;
            }
            //线程钩子
            myhookProc = MyHookProc; //声明一个自己的Hook实现函数的委托对象

            //_nextHookPtr = SetWindowsHookEx((int)HookType.Keyboard, myhookProc, IntPtr.Zero, GetCurrentThreadId()); //加到Hook链中  

            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                //全局钩子
                _nextHookPtr = SetWindowsHookEx(13, myhookProc, GetModuleHandle(curModule.ModuleName), 0); //加到Hook链中  
            }
        }
        //钩子子程就是钩子所要做的事情。 
        //private int KeyboardHookProc(int nCode, IntPtr wParam, KeyboardMsg lParam)
        //{

        //    if (lParam.vkCode == (int)Key.RWin || lParam.vkCode == (int)Key.LWin || lParam.vkCode == (int)Key.LeftAlt || lParam.vkCode == (int)Key.RightAlt)
        //    {
        //        return 1;
        //    }
        //    else
        //    {
        //        return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
        //    }
        //}
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WH_KEYBOARD_LL = 13;
        private IntPtr MyHookProc(int code, IntPtr wparam, ref KeyboardMsg lParam)
        {
            if (lParam.vkCode == (int)Keys.LWin || lParam.vkCode == (int)Keys.RWin)
            {
                return (IntPtr)1;
            }
            if ((int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.LWin || (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.RWin)
            {
                return (IntPtr)1;
            }
            if (lParam.vkCode == (int)Keys.F4 && (int)System.Windows.Forms.Control.ModifierKeys == (int)Keys.Alt)
            {
                return (IntPtr)1;
            }
            return CallNextHookEx(_nextHookPtr, code, wparam, ref lParam);
        }
        // 卸载钩子 
        public void HookStop()
        {
            if (_nextHookPtr != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_nextHookPtr); //从Hook链中取消
                _nextHookPtr = IntPtr.Zero;
            }
        }

    }
}
