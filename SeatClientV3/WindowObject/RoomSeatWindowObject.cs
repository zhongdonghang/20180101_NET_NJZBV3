using System;
using System.Collections.Generic;
using SeatClientV3.OperateResult;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///阅览室布局窗体单例
    /// </summary>
    public class RoomSeatWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        Dictionary<string, RoomSeatWindow> _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static RoomSeatWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public Dictionary<string, RoomSeatWindow> Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static RoomSeatWindowObject GetInstance(string roomNo)
        {
            if (_windowObkect == null || !_windowObkect.Window.ContainsKey(roomNo))
            {
                lock (_object)
                {
                    if (_windowObkect == null || !_windowObkect.Window.ContainsKey(roomNo))
                    {
                        return _windowObkect = new RoomSeatWindowObject(roomNo);
                    }
                }
            }
            else
            {
                _windowObkect.Window[roomNo] = new RoomSeatWindow(roomNo);
            }
            return _windowObkect;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private RoomSeatWindowObject(string roomNo)
        {
            try
            {
                if (_window == null)
                {
                    _window = new Dictionary<string, RoomSeatWindow>();
                }
                if (!_window.ContainsKey(roomNo))
                {
                    _window.Add(roomNo, new RoomSeatWindow(roomNo));
                }
                else
                {
                    _window[roomNo] = new RoomSeatWindow(roomNo);
                }
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("阅览室布局窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}