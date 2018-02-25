using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///键盘窗体单例
    /// </summary>
    public class KeyboardWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private KeyboardWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static KeyboardWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public KeyboardWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static KeyboardWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new KeyboardWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private KeyboardWindowObject()
        {
            try
            {
                _window = new KeyboardWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("键盘输入窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}