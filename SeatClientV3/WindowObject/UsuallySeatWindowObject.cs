using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///常坐座位窗体单例
    /// </summary>
    public class UsuallySeatWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private UsuallySeatWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static UsuallySeatWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public UsuallySeatWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static UsuallySeatWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new UsuallySeatWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private UsuallySeatWindowObject()
        {
            try
            {
                _window = new UsuallySeatWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("常坐座位窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}