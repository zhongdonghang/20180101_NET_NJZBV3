using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///主窗体单例
    /// </summary>
    public class MainWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private MainWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static MainWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public MainWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static MainWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new MainWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private MainWindowObject()
        {
            try
            {
                _window = new MainWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("主窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}