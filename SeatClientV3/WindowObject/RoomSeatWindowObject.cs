using System;
using System.Collections.Generic;
using SeatClientV3.OperateResult;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///�����Ҳ��ִ��嵥��
    /// </summary>
    public class RoomSeatWindowObject
    {

        /// <summary>
        /// ����
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// ��Ϣ��ʾ��
        /// </summary>
        Dictionary<string, RoomSeatWindow> _window;
        /// <summary>
        /// ʵ��
        /// </summary>
        private static RoomSeatWindowObject _windowObkect;

        /// <summary>
        /// ��Ϣ��ʾ��
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
        /// ���캯��
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
                SeatManage.SeatManageComm.WriteLog.Write("�����Ҳ��ִ����ʼ��ʧ�ܣ�" + ex.Message);
                throw ex;
            }
        }
    }
}