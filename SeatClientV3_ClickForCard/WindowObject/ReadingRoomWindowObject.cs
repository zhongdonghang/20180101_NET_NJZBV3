using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///短消息窗体单例
    /// </summary>
    public class ReadingRoomWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private ReadingRoomWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static ReadingRoomWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public ReadingRoomWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static ReadingRoomWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new ReadingRoomWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private ReadingRoomWindowObject()
        {
            try
            {
                _window = new ReadingRoomWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("阅览室窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}