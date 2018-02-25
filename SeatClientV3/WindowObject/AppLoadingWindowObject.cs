using System;

namespace SeatClientV3.WindowObject
{
    /// <summary>
    ///启动窗体单例
    /// </summary>
    public class AppLoadingWindowObject
    {

        /// <summary>
        /// 锁定
        /// </summary>
        private static object _object = new object();
        /// <summary>
        /// 消息提示窗
        /// </summary>
        private AppLoadingWindow _window;
        /// <summary>
        /// 实例
        /// </summary>
        private static AppLoadingWindowObject _windowObkect;

        /// <summary>
        /// 消息提示窗
        /// </summary>
        public AppLoadingWindow Window
        {
            get { return _window; }
            set { _window = value; }
        }

        public static AppLoadingWindowObject GetInstance()
        {
            if (_windowObkect == null)
            {
                lock (_object)
                {
                    if (_windowObkect == null)
                    {
                        return _windowObkect = new AppLoadingWindowObject();
                    }
                }
            }
            return _windowObkect;
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        private AppLoadingWindowObject()
        {
            try
            {
                _window = new AppLoadingWindow();
            }
            catch (Exception ex)
            {
                SeatManage.SeatManageComm.WriteLog.Write("窗体初始化失败：" + ex.Message);
                throw ex;
            }
        }
    }
}